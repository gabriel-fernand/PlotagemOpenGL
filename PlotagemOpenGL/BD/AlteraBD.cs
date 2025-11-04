using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Data.OleDb;
using System.Collections.Generic;

namespace PlotagemOpenGL.BD
{
    internal class AlteraBD
    {
        public static string ConnectionAlterarStringDatBd = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Caminho\\seuarquivo.mdb;";

        public static int ExcluiEvento(int Seq)
        {
            try{
                int i = -1;
                //connectionDatBd.Open();

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE Seq = {Seq};";
                if (GlobVar.ConnectionBDdat.State == ConnectionState.Closed)
                {
                    GlobVar.ConnectionBDdat.Open();
                }

                using var DeleteCommand = new OleDbCommand(queryDelete, GlobVar.ConnectionBDdat);

                DeleteCommand.ExecuteNonQuery();

                // Exclui do DataTable
                DataRow[] rows = GlobVar.eventosUpdate.Select($"Seq = {Seq}");

                // Verifica se encontrou algum registro antes de tentar remover
                if (rows.Length > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        int seq = row.Field<int?>("seq") ?? 0;                        

                        // Verifica se já existe essa combinação na tabela eventosUpdate
                        bool alreadyExists = GlobVar.eventosUpdate.AsEnumerable().Any(evRow =>
                            evRow.Field<int>("seq") == seq);

                        if (alreadyExists)
                        {
                            GlobVar.eventosUpdate.Rows.Remove(row);
                        }
                    }
                }
                // Se quiser garantir atualização visual de DataGridView vinculado, pode chamar AcceptChanges se necessário:
                GlobVar.eventosUpdate.AcceptChanges();

                if (GlobVar.ConnectionBDdat.State == ConnectionState.Closed)
                {
                    GlobVar.ConnectionBDdat.Close();
                }
                return i;
            }
            catch { int i = 0; return i; }
        }
        public static void GravaEvento(
            int seq, int NumPag, int CodEvento, int CodCanal1, int CodCanal2,
            int Inicio, int duracao, int sizepag, int LasPag,
            int MenorSat = 0, string Posicao = ".")
        {
            //await Task.Run(() =>
            //{
                try
                {
                    int seqOriginal = seq;
                    int NumPagOriginal = NumPag;
                    int CodEventoOriginal = CodEvento;
                    int CodCanal1Original = CodCanal1;
                    int InicioOriginal = Inicio;
                    int duracaoOriginal = duracao;
                    int sizepagOriginal = sizepag;
                    int LasPagOriginal = LasPag;
                    int MenorSatOriginal = MenorSat;
                    string PosicaoOriginal = Posicao;

                    long seq_aux;
                    int codret = 0;
                    duracao = (duracao - Inicio);
                    int auxInicio = Inicio / sizepag;
                    Inicio = Inicio - (auxInicio * sizepag);

                    string strSQL = $"SELECT * FROM tbl_Eventos WHERE Seq = {seq}";

                    if (GlobVar.ConnectionBDdat.State == ConnectionState.Closed)
                    {
                        GlobVar.ConnectionBDdat.Open();
                    }

                    OleDbDataAdapter adapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat);
                    
                    DataTable rs = new DataTable();
                    adapter.Fill(rs);

                    if (seq == -1)
                    {
                        // Buscar o próximo sequencial de evento
                        strSQL = "SELECT * FROM tbl_SeqEvento";
                        DataTable rs_seq = new DataTable();
                        using (OleDbDataAdapter seqAdapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
                        {
                            seqAdapter.Fill(rs_seq);
                            if (rs_seq.Rows.Count == 0)
                            {
                                seq_aux = 1;
                                using (OleDbCommand cmdInsert = new OleDbCommand("INSERT INTO tbl_SeqEvento (ProxSeqEvento) VALUES (2)", GlobVar.ConnectionBDdat))
                                {
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                seq_aux = (long)rs_seq.Rows[0]["ProxSeqEvento"];
                                using (OleDbCommand cmdUpdate = new OleDbCommand(
                                    "UPDATE tbl_SeqEvento SET ProxSeqEvento = ProxSeqEvento + 1", GlobVar.ConnectionBDdat))
                                {
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }

                        // Verifica se não existe um evento idêntico incluído
                        strSQL = $"SELECT * FROM tbl_Eventos WHERE CodEvento = {CodEvento} AND CodCanal1 = {CodCanal1} AND CodCanal2 = {CodCanal2} AND NumPag = {NumPag} AND Inicio = {Inicio}";
                        DataTable rs_aux = new DataTable();
                        using (OleDbDataAdapter auxAdapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
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
                            if (MenorSat != 0)
                            {
                                MenorSat = Convert.ToInt32(rs.Rows[0]["MenorSat"]);
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
                        if (MenorSat != 0) newRow["MenorSat"] = MenorSat;
                        newRow["Posicao"] = Posicao;

                        rs.Rows.Add(newRow);

                        Inicio = 0;
                        duracao -= (int)newRow["Duracao"];
                        NumPag++;
                    }

                    // Atualiza o Banco com as alterações
                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
                    adapter.Update(rs);

                    if (GlobVar.ConnectionBDdat.State == ConnectionState.Closed)
                    {
                        GlobVar.ConnectionBDdat.Close();
                    }
                    GlobVar.eventosUpdate.Rows.Add(seqOriginal, NumPagOriginal, CodEventoOriginal, CodCanal1Original, InicioOriginal, duracaoOriginal, MenorSatOriginal, PosicaoOriginal);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao gravar evento: {ex.Message}");
                }
            //});
        }
        public static int ExcluiComentario(int Seq)
        {
            try
            {
                int i = -1;
                //using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                //connectionDatBd.Open();

                string queryDelete = $"DELETE FROM tbl_Comentarios WHERE Seq = {Seq};";

                using var DeleteCommand = new OleDbCommand(queryDelete, GlobVar.ConnectionBDdat);

                DeleteCommand.ExecuteNonQuery();

                //connectionDatBd.Close();
                return i;
            }
            catch { int i = 0; return i; }
        }
        public static long GravaComentario(int seq,  string Comentario, int CodMontagem, int NumPag, int xi, int yi, int DuracaoX, int DuracaoY)
        {
            try
            {
                //using var cnn = new OleDbConnection(connectionStringDatBd);

                int seq_aux;
                string strSQL;

                // Consulta para verificar se o comentário já existe com o Seq fornecido
                strSQL = $"SELECT * FROM tbl_Comentarios WHERE Seq = {seq}";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
                {
                    DataTable rs = new DataTable();
                    adapter.Fill(rs);

                    if (seq == -1)
                    {
                        // Buscar o próximo sequencial de evento
                        strSQL = "SELECT * FROM tbl_SeqEvento";
                        DataTable rs_seq = new DataTable();
                        using (OleDbDataAdapter seqAdapter = new OleDbDataAdapter(strSQL, GlobVar.ConnectionBDdat))
                        {
                            seqAdapter.Fill(rs_seq);
                            if (rs_seq.Rows.Count == 0)
                            {
                                seq_aux = 1;
                                using (OleDbCommand cmdInsert = new OleDbCommand("INSERT INTO tbl_SeqEvento (ProxSeqEvento) VALUES (2)", GlobVar.ConnectionBDdat))
                                {
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                seq_aux = Convert.ToInt32(rs_seq.Rows[0]["ProxSeqEvento"]);
                                using (OleDbCommand cmdUpdate = new OleDbCommand("UPDATE tbl_SeqEvento SET ProxSeqEvento = ProxSeqEvento + 1", GlobVar.ConnectionBDdat))
                                {
                                    cmdUpdate.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    else
                    {
                        seq_aux = seq;
                        ExcluiComentario(seq);

                    }

                    // Verifica se existe registro, caso contrário, cria um novo
                    DataRow newRow = rs.NewRow();
                    //if (rs.Rows.Count == 0)
                    //{
                    //    newRow = rs.NewRow();
                    //    newRow["Seq"] = seq_aux;
                    //}
                    //else
                    //{
                    //    newRow = rs.Rows[0];
                    //}

                    //int NumPag, string Comentario, int CodMontagem, int xi, int yi, int DuracaoX, int DuracaoY

                    // Atribui os valores para as colunas
                    newRow["Seq"] = seq_aux;
                    newRow["Comentario"] = Comentario;
                    newRow["CodMontagem"] = CodMontagem;                    
                    newRow["NumPag"] = NumPag;
                    newRow["Xi"] = xi;
                    newRow["Yi"] = yi;
                    newRow["DuracaoX"] = DuracaoX;
                    newRow["DuracaoY"] = DuracaoY;

                    // Se for uma nova linha, adicione ao DataTable
                    if (rs.Rows.Count == 0)
                    {
                    }
                    rs.Rows.Add(newRow);

                    OleDbCommandBuilder commandBuilder = new OleDbCommandBuilder(adapter);
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
        public static void AlteraEstagioDaEpoca(List<(int numPag, int estagio)> updates)
        {
            //Task.Run(() =>
            //{
                try
                {
                // Crie uma conexão nova a cada operação!
                if (GlobVar.ConnectionBDdat.State == ConnectionState.Closed)
                {
                    GlobVar.ConnectionBDdat.Open();
                }

                using (var transaction = GlobVar.ConnectionBDdat.BeginTransaction())
                    {
                        try
                        {
                            string sql = "UPDATE tbl_Paginas SET Estagio = @NovoEstagio WHERE NumPag = @NumPag";
                            var command = new OleDbCommand(sql, GlobVar.ConnectionBDdat, transaction);
                            
                                command.Parameters.Add("@NovoEstagio", OleDbType.Integer);
                                command.Parameters.Add("@NumPag", OleDbType.Integer);

                                foreach (var update in updates)
                                {
                                    command.Parameters["@NovoEstagio"].Value = update.estagio;
                                    command.Parameters["@NumPag"].Value = update.numPag;
                                    command.ExecuteNonQuery();
                                }
                            
                            transaction.Commit();
                            GlobVar.Atualizados.Clear();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                if (GlobVar.ConnectionBDdat.State == ConnectionState.Closed)
                {
                    GlobVar.ConnectionBDdat.Close();
                }

            }
            catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show($"Erro ao atualizar o banco de dados: {ex.Message}");
                }
            //});
        }
        public static void AdicionarLinhasNoBancoDeDados(DataTable telaSelect, int attSeq)
        {
            // String de conexão com o banco de dados Access
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";

            try
            {
                // Abrir a conexão com o banco de dados
                //connection.Open();

                // Percorrer cada linha do DataTable telaSelect
                foreach (DataRow row in telaSelect.Rows)
                {
                    // Construir a lista de colunas e valores dinamicamente
                    string columns = string.Join(", ", telaSelect.Columns.Cast<DataColumn>().Select(col => col.ColumnName));
                    string parameterNames = string.Join(", ", telaSelect.Columns.Cast<DataColumn>().Select(col => "@" + col.ColumnName));

                    // Preparar a query INSERT com os parâmetros
                    string query = $"INSERT INTO tbl_SelImpressao ({columns}) VALUES ({parameterNames})";

                    // Criar o comando para inserir os dados
                    using (OleDbCommand command = new OleDbCommand(query, GlobVar.ConnectionBDdat))
                    {
                        // Adicionar os parâmetros ao comando dinamicamente
                        foreach (DataColumn column in telaSelect.Columns)
                        {
                            var value = row[column] ?? DBNull.Value;
                            command.Parameters.AddWithValue("@" + column.ColumnName, value);
                        }

                        // Executar o comando
                        command.ExecuteNonQuery();
                    }

                    string querySeq = "UPDATE tbl_SeqEvento SET ProxPagImp = @ProxPagImp";

                    using (OleDbCommand command = new OleDbCommand(querySeq, GlobVar.ConnectionBDdat))
                    {
                        command.Parameters.AddWithValue("@ProxPagImp", attSeq);

                        command.ExecuteNonQuery();
                    }
                }

                // Fechar a conexão
                //connection.Close();
                //System.Windows.Forms.MessageBox.Show("Dados inseridos com sucesso no banco de dados!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro, exibir a mensagem de erro
                //System.Windows.Forms.MessageBox.Show($"Erro ao inserir dados no banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void SalvarAlteracoes()
        {
            if (GlobVar.tbl_DadosExame == null || GlobVar.tbl_DadosExame.Rows.Count == 0)
                return;

            string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";


            // Garante que as colunas existam no banco
            string checkSql = "SELECT * FROM tbl_DadosExame";
            using (OleDbCommand checkCmd = new OleDbCommand(checkSql, GlobVar.ConnectionBDdat))
            using (OleDbDataReader reader = checkCmd.ExecuteReader(CommandBehavior.SchemaOnly))
            {
                DataTable schemaTable = reader.GetSchemaTable();
                HashSet<string> existingColumns = new HashSet<string>();

                foreach (DataRow rw in schemaTable.Rows)
                {
                    existingColumns.Add(rw["ColumnName"].ToString());
                }

                foreach (DataColumn col in GlobVar.tbl_DadosExame.Columns)
                {
                    if (!existingColumns.Contains(col.ColumnName))
                    {
                        string columnType = GetOleDbTypeFromSystemType(col.DataType);
                        string alterSql = $"ALTER TABLE tbl_DadosExame ADD COLUMN [{col.ColumnName}] {columnType}";
                        using (OleDbCommand cmd = new OleDbCommand(alterSql, GlobVar.ConnectionBDdat))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Monta o UPDATE manualmente
            DataRow row = GlobVar.tbl_DadosExame.Rows[0];

            List<string> assignments = new List<string>();
            List<OleDbParameter> parameters = new List<OleDbParameter>();

            foreach (DataColumn col in GlobVar.tbl_DadosExame.Columns)
            {
                assignments.Add($"[{col.ColumnName}] = ?");
                parameters.Add(new OleDbParameter("@" + col.ColumnName, row[col.ColumnName] ?? DBNull.Value));
            }

            string updateSql = $"UPDATE tbl_DadosExame SET {string.Join(", ", assignments)}";

            using (OleDbCommand updateCmd = new OleDbCommand(updateSql, GlobVar.ConnectionBDdat))
            {
                updateCmd.Parameters.AddRange(parameters.ToArray());
                updateCmd.ExecuteNonQuery();
            }
        }
        private static string GetOleDbTypeFromSystemType(Type type)
        {
            if (type == typeof(string))
                return "TEXT";
            if (type == typeof(int))
                return "INTEGER";
            if (type == typeof(double) || type == typeof(float))
                return "DOUBLE";
            if (type == typeof(bool))
                return "YESNO";
            if (type == typeof(DateTime))
                return "DATETIME";

            // Fallback
            return "TEXT";
        }
    }
}
