using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Markup;
using ClassesBDNano;

namespace PlotagemOpenGL.BD
{
    internal class AlteraBD
    {
        static DataTable sele = new DataTable();
        private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
        private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";

        public static int ExcluiEvento(int Seq)
        {
            try{
                int i = -1;
                using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                connectionDatBd.Open();

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE Seq = {Seq};";

                using var DeleteCommand = new OdbcCommand(queryDelete, connectionDatBd);

                DeleteCommand.ExecuteNonQuery();

                connectionDatBd.Close();
                return i;
            }
            catch { int i = 0; return i; }
        }
        public static long GravaEvento(int seq, int NumPag, int CodEvento, int CodCanal1, int CodCanal2, int Inicio, int duracao, int sizepag, int LasPag, int? MenorSat = null)
        {
            try
            {
                using var cnn = new OdbcConnection(connectionStringDatBd);

                long seq_aux;
                int codret = 0;
                string Posicao = ".";
                duracao = (duracao - Inicio);// / LasPag;
                int auxInicio = Inicio / sizepag;
                Inicio = Inicio - (auxInicio * sizepag);

                string strSQL = $"SELECT * FROM tbl_Eventos WHERE Seq = {seq}";

                // Cria e abre o DataAdapter
                using (OdbcDataAdapter adapter = new OdbcDataAdapter(strSQL, cnn))
                {
                    DataTable rs = new DataTable();
                    adapter.Fill(rs);

                    if (seq == -1)
                    {
                        // Buscar o próximo sequencial de evento
                        strSQL = "SELECT * FROM tbl_SeqEvento";
                        DataTable rs_seq = new DataTable();
                        using (OdbcDataAdapter seqAdapter = new OdbcDataAdapter(strSQL, cnn))
                        {
                            seqAdapter.Fill(rs_seq);
                            if (rs_seq.Rows.Count == 0)
                            {
                                seq_aux = 1;
                                using (OdbcCommand cmdInsert = new OdbcCommand("INSERT INTO tbl_SeqEvento (ProxSeqEvento) VALUES (2)", cnn))
                                {
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                seq_aux = (long)rs_seq.Rows[0]["ProxSeqEvento"];
                                using (OdbcCommand cmdUpdate = new OdbcCommand("UPDATE tbl_SeqEvento SET ProxSeqEvento = ProxSeqEvento + 1", cnn))
                                {
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }

                        // Verifica se não existe um evento idêntico ao que está sendo incluído
                        strSQL = $"SELECT * FROM tbl_Eventos WHERE CodEvento = {CodEvento} AND CodCanal1 = {CodCanal1} AND CodCanal2 = {CodCanal2} AND NumPag = {NumPag} AND Inicio = {Inicio}";
                        DataTable rs_aux = new DataTable();
                        using (OdbcDataAdapter auxAdapter = new OdbcDataAdapter(strSQL, cnn))
                        {
                            auxAdapter.Fill(rs_aux);
                            if (rs_aux.Rows.Count > 0)
                            {
                                codret = ExcluiEvento((int)rs_aux.Rows[0]["Seq"]);
                            }
                        }
                    }
                    else
                    {
                        seq_aux = seq;
                        if (rs.Rows.Count > 0)
                        {
                            if (MenorSat != null) {
                                MenorSat = (int)rs.Rows[0]["MenorSat"];
                            }
                            Posicao = rs.Rows[0]["Posicao"].ToString();
                        }

                        ExcluiEvento(seq);
                    }

                    // Inserção de novos registros
                    while (duracao > 0)
                    {
                        DataRow newRow = rs.NewRow();
                        newRow["Seq"] = seq_aux;
                        newRow["NumPag"] = NumPag;
                        newRow["CodEvento"] = CodEvento;
                        newRow["CodCanal1"] = CodCanal1;
                        newRow["CodCanal2"] = CodCanal2;
                        newRow["Inicio"] = Inicio;
                        if (Inicio + duracao > sizepag)
                        {
                            newRow["Duracao"] = sizepag - Inicio;
                        }
                        else
                        {
                            newRow["Duracao"] = duracao;
                        }
                        if (MenorSat.HasValue) newRow["MenorSat"] = MenorSat;
                        newRow["Posicao"] = Posicao;

                        rs.Rows.Add(newRow);

                        Inicio = 0;
                        duracao -= (int)newRow["Duracao"];
                        NumPag++;
                    }

                    // Atualiza o DataTable com as alterações
                    OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(adapter);
                    adapter.Update(rs);
                }

                return seq_aux;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return -1;
            }
        }

    }
}
