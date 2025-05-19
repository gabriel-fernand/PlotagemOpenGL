using Accord.Math;
using ADODB;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.BD;
using PlotagemOpenGL.Filtros;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    internal class AnaliseAutomaticaRonco
    {
        float[] sinal;
        int codCanal;
        bool excluirEvento;
        int low = 40;
        int high = 120;
        int notch = 60;
        AnaliseAuto owner;
        float taxaAmos;
        float DurMinEv;
        float DurMaxEv;
        float DistanciaMin;
        float DistanciaMax;
        float DurJanBasal;
        float DurJanEvento;
        float IntervaloMinEntreEv;
        float FatorAmplituide;
        float NumVezesDurMinEv;
        float NumVezesAmplitudeBasal;
        float tipoAmplBasal;
        float FatorMultAmpEstagio;

        public AnaliseAutomaticaRonco(int CodCanal, bool excluirEvento,AnaliseAuto owner)
        {
            this.owner = owner;
            sinal = new float[GlobVar.matrizCanal.GetLength(1)];
            this.codCanal = CodCanal;
            if (!verificaExistenciaDoCanalNaMontagem(CodCanal)) return;
            verificaFiltroEPegaDados();

            this.excluirEvento = excluirEvento;
            ParametrosParaAnalisar();
        }
        private void captaDados()
        {
            int indexx = GlobVar.codSelected.IndexOf(codCanal);

            for(int i = 0; i < sinal.Length; i++)
            {
                sinal[i] = GlobVar.matrizCanal[indexx, i];
            }
        }

        private void verificaFiltroEPegaDados()
        {
            int indexCod = GlobVar.codSelected.IndexOf(codCanal);

            var row = GlobVar.tbl_MontagemSelecionada.Rows[indexCod];

            int lowtab = row["PassaBaixa"] == DBNull.Value ? 0 : Convert.ToInt32(row["PassaBaixa"]);
            int hightab = row["PassaAlta"] == DBNull.Value ? 0 : Convert.ToInt32(row["PassaAlta"]);
            int notchtag = row["Notch"] == DBNull.Value ? 0 : Convert.ToInt32(row["Notch"]);

            if (lowtab == low && hightab == high && notchtag == notch)
            {
                captaDados();
            }
            else
            {
                int indexx = GlobVar.codCanal.IndexOf(codCanal);
                int taxa = GlobVar.txPorCanal[indexx];

                // Verifica se o índice existe
                int startCol = GlobVar.ponteiroI[indexx];
                int endCol = GlobVar.ponteiroF[indexx];

                // Copia os valores de matrizCompleta para o array referencia
                int pontRef = 0;
                for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0) && pontRef < sinal.Length; linhaComp++)
                {
                    for (int colunaComp = startCol; colunaComp < endCol && pontRef < sinal.Length; colunaComp++)
                    {
                        sinal[pontRef] = (int)GlobVar.matrizCompleta[linhaComp, colunaComp];
                        pontRef++;
                    }
                }
                sinal = BandPass.ApplyFilter(sinal, (float)low, (float)high, taxa);
                sinal = Notch.ApplyFilter(sinal, (float)notch, 0, taxa);
            }
        }

        private bool verificaExistenciaDoCanalNaMontagem(int CodCanFLuxo)
        {
            return GlobVar.tbl_MontagemSelecionada
                .AsEnumerable()
                .Any(row => row.Field<int>("CodCanal1") == CodCanFLuxo);
        }
        private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
        public static int ExcluiEvento(int codEvento, int codCanal)
        {
            try
            {
                int i = -1;
                using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                connectionDatBd.Open();

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE CodCanal1 = {codCanal} AND CodEvento = {codEvento};";

                using var DeleteCommand = new OdbcCommand(queryDelete, connectionDatBd);

                DeleteCommand.ExecuteNonQuery();

                connectionDatBd.Close();
                return i;
            }
            catch { int i = 0; return i; }
        }

        private void ParametrosParaAnalisar()
        {
            if (GlobVar.tbl_ParametrosParaAnalisar == null) { return; }
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(3);

            if (excluirEvento)
            {
                // Exclui eventos no DataTable `GlobVar.eventosUpdate`
                List<int> codigosExcluir = new List<int> { 13 };

                foreach (int a in codigosExcluir)
                {
                    if (owner.cancellationToken.IsCancellationRequested) return;
                    ExcluiEvento(a, codCanal);
                }
                foreach (DataRow rw in GlobVar.eventosUpdate.Rows.Cast<DataRow>().ToList())
                {
                    if (codigosExcluir.Contains(Convert.ToInt32(rw["CodEvento"])) && Convert.ToInt32(rw["CodCanal1"]) == codCanal)
                    {
                        if (owner.cancellationToken.IsCancellationRequested) return;
                        GlobVar.eventosUpdate.Rows.Remove(rw);
                    }
                }

                GlobVar.eventosUpdate.AcceptChanges();
            }
            int BoaNoite = 0;
            int BomDia = 0;
            var rwBoaNoite = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 18).FirstOrDefault();
            var rwBomDia = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 19).FirstOrDefault();

            if (rwBoaNoite != null)
            {
                BoaNoite = Convert.ToInt32(rwBoaNoite["NumPag"]) * GlobVar.namos;
            }
            if (rwBomDia != null)
            {
                BomDia = Convert.ToInt32(rwBomDia["NumPag"]) * GlobVar.namos;
            }
            else
            {
                BomDia = sinal.Length;
            }


            var row = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

            if (row["Ronco_Dur_Min_Ev"] == DBNull.Value || row["Ronco_Dur_Max_Ev"] == DBNull.Value || row["Ronco_Dur_Jan_Basal"] == DBNull.Value
                || row["Ronco_Dur_Jan_Evento"] == DBNull.Value || row["Ronco_Interv_Min_Entre_Ev"] == DBNull.Value || row["Ronco_Fator_Amplitude"] == DBNull.Value
                || row["Ronco_Tipo_Ampl_Basal"] == DBNull.Value || row["Ronco_Num_Vezes_Dur_Min_Ev"] == DBNull.Value || row["Ronco_Num_Vezes_Amplitude_Basal"] == DBNull.Value) { return; }
            else
            {
                int indexx = GlobVar.codCanal.IndexOf(codCanal);
                int taxa = GlobVar.txPorCanal[indexx];

                DurMinEv = (float)Convert.ToDouble(row["Ronco_Dur_Min_Ev"]) * taxa;
                DurMaxEv = Convert.ToInt32(row["Ronco_Dur_Max_Ev"]) * taxa;
                //DistanciaMin = Convert.ToInt32(row["Ronco_Distancia_Min"]) * taxa;
                //DistanciaMax = Convert.ToInt32(row["Ronco_Distancia_Max"]) * taxa;
                DurJanBasal = Convert.ToInt32(row["Ronco_Dur_Jan_Basal"]) * taxa;
                DurJanEvento = (float)Convert.ToDouble(row["Ronco_Dur_Jan_Evento"]) * taxa;
                IntervaloMinEntreEv = Convert.ToInt32(row["Ronco_Interv_Min_Entre_Ev"]) * taxa;

                FatorAmplituide = Convert.ToInt32(row["Ronco_Fator_Amplitude"]);
                NumVezesDurMinEv = Convert.ToInt32(row["Ronco_Num_Vezes_Dur_Min_Ev"]); ;
                NumVezesAmplitudeBasal = Convert.ToInt32(row["Ronco_Num_Vezes_Amplitude_Basal"]); ;
                tipoAmplBasal = Convert.ToInt32(row["Ronco_Tipo_Ampl_Basal"]);
                //FatorMultAmpEstagio = Convert.ToInt32(row["Ronco_Fator_Mult_Ampl_Estagio"]);

            }
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(6);

            float[] jan_Basal = new float[(int)DurJanBasal];
            float[] jan_Basal_Aux = new float[(int)DurJanBasal];

            float[] jan_evento = new float[(int)DurJanEvento];
            float[] jan_evento_Aux = new float[(int)DurJanEvento];

            int[] eventos = new int[sinal.Length];

            Array.Copy(sinal, BoaNoite, jan_Basal, 0, (int)DurJanBasal);
            Array.Copy(sinal, BoaNoite + (int)DurJanBasal, jan_evento, 0, (int)DurJanEvento);
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(9);
            int total = Math.Abs((BoaNoite + (int)DurJanBasal + (int)DurJanEvento) - BomDia);
            int loc = 0;
            int ultimoValor = -1;

            int totalEtapas = 5;
            int tamanhoEtapa = (100 - 9) / totalEtapas;
            int progressoAtual = 9;

            float soma_evento = jan_evento.Sum();
            float soma_basal = jan_Basal.Sum();

            //Passo 1 Percorrer os dados e detectar eventos
            for (int i = BoaNoite + (int)DurJanBasal + (int)DurJanEvento; i < BomDia; i++)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;

                float media_evento = soma_evento / DurJanEvento;
                float media_basal = soma_basal / DurJanBasal;
                if (media_basal == 0) media_basal = 1;

                if ((media_evento / FatorAmplituide) >= media_basal)
                {
                    for (int j = i - ((int)DurJanEvento); j < i + DurJanEvento && j < eventos.Length; j++) // Garante que j esteja dentro do limite
                    {
                        eventos[j] = 1; // Altera corretamente a sequência de eventos
                    }
                    float evento_0 = jan_evento[0];
                    Array.Copy(jan_evento, 1, jan_evento, 0, (int)DurJanEvento - 1);
                    jan_evento[(int)DurJanEvento - 1] = sinal[i];

                    // Atualizar soma da janela do evento
                    soma_evento = soma_evento - evento_0 + sinal[i];

                }
                else
                {
                    float basal_0 = jan_Basal[0];
                    Array.Copy(jan_Basal, 1, jan_Basal, 0, (int)DurJanBasal - 1);
                    jan_Basal[(int)DurJanBasal - 1] = sinal[i];

                    // Atualizar soma da janela basal
                    soma_basal = soma_basal - basal_0 + sinal[i];

                    float evento_0 = jan_evento[0];
                    Array.Copy(jan_evento, 1, jan_evento, 0, (int)DurJanEvento - 1);
                    jan_evento[(int)DurJanEvento - 1] = sinal[i];

                    // Atualizar soma da janela do evento
                    soma_evento = soma_evento - evento_0 + sinal[i];

                }
            }

            int qtdDados = sinal.Length;
            int ini1 = -1, fim1 = -1, ini2 = -1, fim2 = -1;
            float min, max, ampl_evento;
            total = Math.Abs((BoaNoite + (int)DurJanBasal) - BomDia); loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            // Passo 2: Unir eventos próximos
            ini1 = -1;
            fim1 = -1;
            bool busca_ini2 = false;

            for (int i = BoaNoite + (int)DurJanBasal; i < BomDia; i++)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;

                if (eventos[i] > 0)
                {
                    if (busca_ini2)
                    {
                        busca_ini2 = false;
                        ini2 = i;
                        // Se o intervalo entre eventos for menor que o mínimo, une os eventos
                        if ((fim1 + IntervaloMinEntreEv > i) && (fim1 - ini1 > DurMinEv / 2.5))
                        {
                            for (int j = fim1 - 1; j <= i; j++)
                            {
                                eventos[j] = 1;
                            }
                            fim1 = i;
                        }
                        else
                        {
                            // Se o evento anterior foi muito curto, remove ele
                            if (fim1 - ini1 < DurMinEv)
                            {
                                for (int j = ini1; j <= fim1; j++)
                                {
                                    eventos[j] = 0;
                                }
                            }
                            ini1 = i;
                            fim1 = i;
                        }
                    }
                    else if (ini1 < 0)
                    {
                        // Primeiro evento detectado
                        ini1 = i;
                        fim1 = i;
                    }
                    else
                    {
                        fim1 = i;
                    }
                }
                else if(ini1 > 0)
                { 
                        // Encontramos um zero depois de um evento, buscamos o próximo evento
                        busca_ini2 = true;
                }
            }

            ini1 = -1; fim1 = -1;
            int npag = 0, ultPagEv = -1;
            int pos, duracao, Evento;
            total = Math.Abs((BoaNoite + (int)DurJanBasal) - BomDia); loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            // PASSO 3 - Remove eventos com duração menor que DurMinEv ou maior que DurMaxEv
            for (int i = BoaNoite + (int)DurJanBasal; i < BomDia; i++)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;

                if (eventos[i] > 0)
                {
                    if(ini1 > 0)
                    {
                        fim1 = i;
                    }
                    else
                    {
                        ini1 = i;
                        fim1 = i;
                    }
                }
                else
                {
                    if (ini1 > 0)
                    {
                        if ((Math.Abs(ini1 - fim1) < DurMinEv) || (Math.Abs(ini1 - fim1) > DurMaxEv))
                        {
                            for (int j = ini1; j <= fim1; j++)
                            {
                                eventos[j] = 0; // Elimina evento inválido
                            }
                        }
                        ini1 = -1;
                        fim1 = -1;
                    }
                }
            }

            int indexxs = GlobVar.codCanal.IndexOf(codCanal);
            int taxas = GlobVar.txPorCanal[indexxs];
            Evento = 13;

            List<(int inicio, int fim, int evento, int canal, int taxa)> eventosTemporarios = new();
            total = Math.Abs((BoaNoite + (int)DurJanBasal) - BomDia); loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            // PASSO 4 - Inclui eventos no banco de dados
            ini1 = -1;
            for (int i = BoaNoite + (int)DurJanBasal; i < BomDia; i++)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;

                if (eventos[i] > 0)
                {
                    if (ini1 < 0)
                    {
                        ini1 = i;
                        fim1 = i;
                        //Evento = eventos[i];
                        Evento = 13;

                    }
                    else
                    {
                        fim1 = i;
                    }
                }
                else if(ini1 > 0)
                {

                    eventosTemporarios.Add((ini1, fim1, Evento, codCanal, taxas));

                    ini1 = -1;
                }
            }
            cnn = new OdbcConnection(connectionStringDatBd);
            connectionDatBd = new OdbcConnection(connectionStringDatBd);
            connectionDatBd.Open();

            total = eventosTemporarios.Count; loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;
            // Inserir todos os eventos no banco de uma vez
            foreach (var ev in eventosTemporarios)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;
                AdicionarEventoAoDataTable(ev.inicio, ev.fim, ev.evento, ev.canal, ev.taxa);
            }

            string query = "SELECT * FROM tbl_Eventos";
            using var command = new OdbcCommand(query, connectionDatBd);
            using var adapterEventosDtNormal = new OdbcDataAdapter(command);
            GlobVar.eventos.Clear();
            adapterEventosDtNormal.Fill(GlobVar.eventos);
            connectionDatBd.Close();
            owner.AtualizarProgresso(99);

            ultPagEv = npag;
        }
        public static void AdicionarEventoAoDataTable(int inicio, int termino, int codEvento, int codcanal1, int taxa)
        {
            try
            {
                if (GlobVar.lastEvent != null)
                {
                    DataTable eventos = GlobVar.eventosUpdate;

                    //int loc = EncontrarValorMaisProximo(desenhoLoc, startY);

                    // Adicionar colunas ao DataTable se não existirem
                    if (eventos.Columns.Count == 0)
                    {
                        eventos.Columns.Add("Seq", typeof(int));
                        eventos.Columns.Add("NumPag", typeof(string));
                        eventos.Columns.Add("CodEvento", typeof(int));
                        eventos.Columns.Add("CodCanal1", typeof(int));
                        eventos.Columns.Add("Inicio", typeof(int));
                        eventos.Columns.Add("Duracao", typeof(int));
                        eventos.Columns.Add("MenorSat", typeof(int));
                        eventos.Columns.Add("Posicao", typeof(string));

                    }

                    // Calcular NumPag para início e término
                    int numPagInicio = inicio / taxa;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    int numPagTermino = termino / taxa;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    string numPag = $"{numPagInicio} -- {numPagTermino}";
                    // Obter o próximo valor de Seq
                    int seq = plotComentatios.AtualizarProxSeqEvento();

                    plotEventos.minSaturacao(numPagInicio, numPagTermino);
                    int minSat = GlobVar.minSat.Min();
                    string posi = plotEventos.Posicao(numPagInicio, numPagTermino);

                    // Adicionar dados ao DataTable
                    GlobVar.eventosUpdate.Rows.Add(seq, numPag, codEvento, codcanal1, inicio, termino, minSat, posi);
                    GravaEvento(seq, numPagInicio, codEvento, codcanal1, -1, inicio, termino, GlobVar.namos, numPagTermino, minSat, posi);
                    // Exportar DataTable para Excel
                    string excelFilePath = @"C:\Teste\Teste";
                    //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
                    eventos.Dispose();
                }
            }
            catch { }
        }
        static OdbcConnection cnn;
        static OdbcConnection connectionDatBd;
        OdbcCommand command;
        OdbcDataAdapter adapterEventosDtNormal;

        public static long GravaEvento(int seq, int NumPag, int CodEvento, int CodCanal1, int CodCanal2, int Inicio, int duracao, int sizepag, int LasPag, int MenorSat = 0, string Posicao = ".")
        {
            try
            {
                long seq_aux;
                int codret = 0;
                //string Posicao = ".";
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
                                codret = ExcluiEventoSeq((int)rs_aux.Rows[0]["Seq"]);
                            }
                        }
                    }
                    else
                    {
                        seq_aux = seq;
                        if (rs.Rows.Count > 0)
                        {
                            if (MenorSat != null)
                            {
                                MenorSat = Convert.ToInt32(rs.Rows[0]["MenorSat"]);
                            }
                            Posicao = rs.Rows[0]["Posicao"].ToString();
                        }

                        ExcluiEventoSeq(seq);
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

                    // Atualiza o DataTable com as alterações
                    OdbcCommandBuilder commandBuilder = new OdbcCommandBuilder(adapter);
                    adapter.Update(rs);
                    /*
                    string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
                    using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                    string query = "SELECT * FROM tbl_Eventos";
                    using var command = new OdbcCommand(query, connectionDatBd);
                    using var adapterEventosDtNormal = new OdbcDataAdapter(command);
                    GlobVar.eventos.Clear();
                    adapterEventosDtNormal.Fill(GlobVar.eventos);
                    connectionDatBd.Close();
                    */
                }

                return seq_aux;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return -1;
            }
        }

        public static int ExcluiEventoSeq(int Seq)
        {
            try
            {
                int i = -1;

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE Seq = {Seq};";

                using var DeleteCommand = new OdbcCommand(queryDelete, connectionDatBd);

                DeleteCommand.ExecuteNonQuery();

                return i;
            }
            catch { int i = 0; return i; }
        }

    }
}
