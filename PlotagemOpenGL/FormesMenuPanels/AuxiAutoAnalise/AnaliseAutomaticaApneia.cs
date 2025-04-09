using Accord.Audio;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra.Factorization;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.BD;
using PlotagemOpenGL.Filtros;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow.Operations;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    internal class AnaliseAutomaticaApneia
    {
        float[] dados;

        int LimiarAp = 80;
        int LimiarHip = 50;
        int DurMin = 10;
        int intervaloEvent = 3;
        int TamanhoJanBasal = 30;
        int TamanhoJanelaEventos = 5;
        int AnalisarCanFluxo = 0;
        int low = 10;
        int high = 50;
        AnaliseAuto owner;

        bool excluirEvento = false;

        public AnaliseAutomaticaApneia(int limiarAp = 0, int LimiarHip = 0, int DurMin = 0, int intervaloEvent = 0, int TamanhoJanBasal = 0, int TamanhoJanEventos = 0, int AnalisarCanFluxo = 0, bool excluirEvento = false, AnaliseAuto owner = null)
        {
            this.owner = owner;
            this.LimiarAp = limiarAp;
            this.LimiarHip = LimiarHip;
            this.DurMin = DurMin;
            this.intervaloEvent = intervaloEvent;
            this.TamanhoJanBasal = TamanhoJanBasal;
            this.TamanhoJanelaEventos = TamanhoJanEventos;
            this.AnalisarCanFluxo = AnalisarCanFluxo;
            this.excluirEvento = excluirEvento;

            if (!verificaExistenciaDoCanalNaMontagem(AnalisarCanFluxo)) return;
            int indexx = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
            int Taxa = GlobVar.txPorCanal[indexx];
            int qtdDados = Taxa * GlobVar.npagin; // Mesmo cálculo; ajuste se necessário

            dados = new float[qtdDados];
            verificaFiltroEPegaDados();
            
            this.excluirEvento = excluirEvento;
            tbl_ParametrosParaAnalise();
        }

        private bool verificaExistenciaDoCanalNaMontagem(int CodCanFLuxo)
        {
            return GlobVar.tbl_MontagemSelecionada
                .AsEnumerable()
                .Any(row => row.Field<int>("CodCanal1") == CodCanFLuxo);
        }
        private void verificaFiltroEPegaDados()
        {
            int indexCod = GlobVar.codSelected.IndexOf(AnalisarCanFluxo);

            var row = GlobVar.tbl_MontagemSelecionada.Rows[indexCod];

            int lowtab = row["PassaBaixa"] == DBNull.Value ? 0 : Convert.ToInt32(row["PassaBaixa"]);

            if(lowtab == low)
            {
                captaDados();
            }
            else
            {
                int indexx = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
                int Taxa = GlobVar.txPorCanal[indexx];
                // Verifica se o índice existe
                int startCol = GlobVar.ponteiroI[indexx];
                int endCol = GlobVar.ponteiroF[indexx];

                // Copia os valores de matrizCompleta para o array referencia
                int pontRef = 0;
                for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0) && pontRef < dados.Length; linhaComp++)
                {
                    for (int colunaComp = startCol; colunaComp < endCol && pontRef < dados.Length; colunaComp++)
                    {
                        dados[pontRef] = (int)GlobVar.matrizCompleta[linhaComp, colunaComp];
                        pontRef++;
                    }
                }
                dados = PaissaBaixa.ApplyFilter((dados), (float)low, Taxa);

            }
        }

        private void captaDados()
        {
            int indexx = GlobVar.codSelected.IndexOf(AnalisarCanFluxo);

            for (int i = 0; i < dados.Length; i++)
            {
                dados[i] = GlobVar.matrizCanal[indexx, i];
            }
        }

        private void tbl_ParametrosParaAnalise()
        {
            int g_porc = 0;
            int porc_aux = 0;
            // Índice do canal a ser analisado
            int indexx = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
            int Taxa = GlobVar.txPorCanal[indexx];
            int qtdDadosAux = Taxa * GlobVar.npagin;
            int qtdDados = Taxa * GlobVar.npagin; // Mesmo cálculo; ajuste se necessário

            int CodEventoHipopneia = 5;
            int CodEventoApneia = 2;

            // Obtém os dados da tabela tbl_Paginas (não utilizado posteriormente neste trecho)
            var tbl_Pagina = GlobVar.tbl_Paginas;
            if (owner.cancellationToken.IsCancellationRequested) return;

            if (excluirEvento)
            {
                // Exclui eventos no DataTable `GlobVar.eventosUpdate`
                List<int> codigosExcluir = new List<int> { 2, 5, 1, 3, 4, 6, 101 };

                foreach(int a in codigosExcluir)
                {
                    if (owner.cancellationToken.IsCancellationRequested) return;
                    ExcluiEvento(a, AnalisarCanFluxo);
                }
                foreach (DataRow row in GlobVar.eventosUpdate.Rows.Cast<DataRow>().ToList())
                {
                    if (owner.cancellationToken.IsCancellationRequested) return;
                    if (codigosExcluir.Contains(Convert.ToInt32(row["CodEvento"])) && Convert.ToInt16(row["CodCanal1"]) == AnalisarCanFluxo)
                    {
                        GlobVar.eventosUpdate.Rows.Remove(row);
                    }
                }

                GlobVar.eventosUpdate.AcceptChanges();
            }
            // Obtém os parâmetros de análise
            var rs = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];
            if (rs == null)
            {
                return;
            }
            owner.AtualizarProgresso(3);

            int BoaNoite = 0;
            int BomDia = 0;
            var rwBoaNoite = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 18).FirstOrDefault();
            var rwBomDia = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 19).FirstOrDefault();

            if (rwBoaNoite != null)
            {
                BoaNoite = Convert.ToInt32(rwBoaNoite["NumPag"]) * Taxa;
            }
            if (rwBomDia != null)
            {
                BomDia = Convert.ToInt32(rwBomDia["NumPag"]) * Taxa;
            }
            else
            {
                BomDia = dados.Length;
            }
            if (owner.cancellationToken.IsCancellationRequested) return;

            if (rs["ApHip_LimiarAp"] == DBNull.Value ||
                rs["ApHip_LimiarHip"] == DBNull.Value ||
                rs["ApHip_DuracaoMin"] == DBNull.Value ||
                rs["ApHip_Dur_Max_Ev"] == DBNull.Value ||
                rs["ApHip_Dur_Jan_Basal"] == DBNull.Value ||
                rs["ApHip_Dur_Jan_Evento"] == DBNull.Value ||
                rs["ApHip_IntervMin"] == DBNull.Value)
            {
                return;
            }
            if (owner.cancellationToken.IsCancellationRequested) return;

            // Calcula os limites (converte porcentagens para valores normalizados)
            float LimiarAp = 1 - (Convert.ToSingle(rs["ApHip_LimiarAp"]) / 100);
            float LimiarHip = 1 - (Convert.ToSingle(rs["ApHip_LimiarHip"]) / 100);
            int Dur_min_ev = Convert.ToInt32(rs["ApHip_DuracaoMin"]) * Taxa;
            int Dur_Max_Ev = Convert.ToInt32(rs["ApHip_Dur_Max_Ev"]) * Taxa;
            int Dur_Jan_Basal = Convert.ToInt32(rs["ApHip_Dur_Jan_Basal"]) * Taxa;
            int Dur_Jan_Evento = Convert.ToInt32(rs["ApHip_Dur_Jan_Evento"]) * Taxa;
            int Interv_Min_Entre_Ev = Convert.ToInt32(rs["ApHip_IntervMin"]) * Taxa;

            // Inicializa arrays de eventos e dados auxiliares
            float[] jan_basal = new float[Dur_Jan_Basal];
            float[] jan_basal_aux = new float[Dur_Jan_Basal];
            float[] jan_evento = new float[Dur_Jan_Evento];
            float[] jan_evento_aux = new float[Dur_Jan_Evento];
            // Aqui usamos um array de inteiros para eventos; ajuste o tamanho conforme a sua necessidade.
            int[] eventos = new int[qtdDados];
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(6);

            g_porc = 7;
            for(int a = 0; a < dados.Length; a++)
            {
                if (dados[a] > 32000) dados[a] = 32000;
                else dados[a] = Math.Abs(dados[a]);
            }
            
            // Copia os dados para o buffer basal e para o buffer de evento
            // Assume-se que 'dados' é um array float[] previamente preenchido.
            Array.Copy(dados, BoaNoite, jan_basal, 0, Dur_Jan_Basal);
            // Copia a partir do índice Dur_Jan_Basal para o buffer de evento, iniciando no índice 1
            Array.Copy(dados,BoaNoite + Dur_Jan_Basal, jan_evento, 0, Dur_Jan_Evento);
            if (owner.cancellationToken.IsCancellationRequested) return;
            owner.AtualizarProgresso(9);

            g_porc = 10;
            int total = Math.Abs((BoaNoite + Dur_Jan_Basal + Dur_Jan_Evento) - BomDia);
            int loc = 0;
            int ultimoValor = -1;

            int totalEtapas = 5;
            int tamanhoEtapa = (100 - 9) / totalEtapas;
            int progressoAtual = 9;

            // PASSO 1 - Detecção de eventos
            float soma_evento = F_Somatoria(jan_evento, 0, Dur_Jan_Evento);
            float soma_basal = F_Somatoria(jan_basal, 0, Dur_Jan_Basal);
            float media_evento, media_basal;
            int desloc = 0; // Verifique se desloc precisa ser atualizado em outro momento
            int Evento = 0;

            for (int i = BoaNoite + Dur_Jan_Basal + Dur_Jan_Evento; i < BomDia; i++)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;

                // Atualize o buffer de evento: "shift" à esquerda
                int evento_0 = (int)jan_evento[0];
                Array.Copy(jan_evento, 1, jan_evento, 0, jan_evento.Length - 1);
                // Preenche a última posição com o valor atual de 'dados'
                jan_evento[Dur_Jan_Evento - 1] = (int)dados[i];

                soma_evento = soma_evento - evento_0 + dados[i];
                // Verificação de cancelamento se necessário (ex.: if (g_cancel) return;)

                media_evento = soma_evento / Dur_Jan_Evento;
                media_basal = Math.Max(1, soma_basal / Dur_Jan_Basal);

                float ValorLimiarHip = media_basal * LimiarHip;
                float ValorLimiarAp = media_basal * LimiarAp;

                if (media_basal == 0)
                    media_basal = 1;

                // Detecta Apneia ou Hipopneia
                if (media_evento < ValorLimiarHip)
                {
                    if (media_evento < ValorLimiarAp)
                        Evento = CodEventoApneia;
                    else
                        Evento = CodEventoHipopneia;

                    // Marca o evento para as amostras correspondentes
                    for (int h = i - ((int)Dur_Jan_Evento); h < i + Dur_Jan_Evento && h < eventos.Length; h++) // Garante que j esteja dentro do limite
                    {
                        eventos[h] = Evento; // Altera corretamente a sequência de eventos
                    }


                }
                else
                {
                    // "Shift" no buffer basal
                    float basal_0 = jan_basal[0];
                    Array.Copy(jan_basal, 1, jan_basal, 0, (int)Dur_Jan_Basal - 1);
                    jan_basal[(int)Dur_Jan_Basal - 1] = dados[i];

                    // Atualizar soma da janela basal
                    soma_basal = soma_basal - basal_0 + dados[i];
                    /*
                    // Atualize o buffer de evento: "shift" à esquerda
                    int evento_0 = (int)jan_evento[0];
                    Array.Copy(jan_evento, 1, jan_evento, 0, jan_evento.Length - 1);
                    // Preenche a última posição com o valor atual de 'dados'
                    jan_evento[Dur_Jan_Evento - 1] = (int)dados[i];

                    soma_evento = soma_evento - evento_0 + dados[i];
                    */
                }
            }

            g_porc = 30;
            g_porc = 40;
            total = Math.Abs((BoaNoite + Dur_Jan_Basal) - BomDia); loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            // PASSO 2 - Junta eventos cujo intervalo entre eles seja menor que o intervalo mínimo
            int ini1 = -1;
            int fim1 = -1;
            bool busca_ini2 = false;
            porc_aux = 10;
            for (int i = BoaNoite + Dur_Jan_Basal; i < BomDia; i++)
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
                        // Se o intervalo entre os eventos for pequeno e a duração válida, une os eventos
                        if (((fim1 + Interv_Min_Entre_Ev > i) && (fim1 - ini1 > Dur_min_ev / 2.5)) ||
                            (i - fim1 < Taxa))
                        {
                            // Calcula médias para verificar a condição de junção
                            media_evento = F_Somatoria(dados, ini1, fim1 - ini1 + 1) / (fim1 - ini1 + 1);
                            media_basal = F_Somatoria(dados, fim1 + 1, i - fim1 + 1) / (i - fim1 + 1);

                            if (i - fim1 < Taxa ||
                                media_basal <= media_evento * 1.7 ||
                                ((fim1 - ini1 > Dur_min_ev) && (i - fim1 < Interv_Min_Entre_Ev)))
                            {
                                for (int b = fim1 - 1; b <= i; b++)
                                {
                                    eventos[b] = Evento;
                                }
                                fim1 = i;
                            }
                            else
                            {
                                ini1 = i;
                                fim1 = i;
                                Evento = eventos[i];
                            }
                        }
                        else
                        {
                            ini1 = i;
                            fim1 = i;
                            Evento = eventos[i];
                        }
                    }
                    else if (ini1 < 0)
                    {
                        // Primeira ocorrência de um evento
                        ini1 = i;
                        fim1 = i;
                        Evento = eventos[i];
                    }
                    else
                    {
                        fim1 = i;
                    }
                }
                else
                {
                    if (ini1 >= 0)
                    {
                        // Se já havia um evento, sinaliza que próximo dado pode ser início de novo evento
                        busca_ini2 = true;
                    }
                    else
                    {
                        busca_ini2 = false;
                    }
                }
            }
            total = Math.Abs((BoaNoite) - BomDia); loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            // PASSO 3 - Elimina eventos com duração menor que Dur_min_ev ou maior que Dur_Max_Ev
            ini1 = -1;
            fim1 = -1;
            porc_aux = 10;
            for (int i = BoaNoite; i < BomDia; i++)
            {
                /*
                if (i % (qtdDados / 10) == 0)
                {
                    g_porc = 60 + (porc_aux / 11) * (i / (qtdDados / 10));
                }
                */
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
                    if (ini1 > 0)
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
                        if ((Math.Abs(ini1 - fim1) < Dur_min_ev) || (Math.Abs(ini1 - fim1) > Dur_Max_Ev))
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

            g_porc = 70;
            g_porc = 90;

            // Inicializa variáveis
            ini1 = -1; fim1 = 0;
            porc_aux = 10;
            int qtd_ap = 0, qtd_hip = 0;
            bool Est_0 = false;
            int pag_ini = Convert.ToInt32(GlobVar.tbl_Paginas.Rows[0]["NumPag"]);
            int indexxs = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
            int taxas = GlobVar.txPorCanal[indexxs];

            List<(int inicio, int fim, int evento, int canal, int taxa)> eventosTemporarios = new();
            total = Math.Abs((BoaNoite + Dur_Jan_Basal) - BomDia); loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;

            for (int i = BoaNoite + Dur_Jan_Basal; i < BomDia; i++)
            {
                loc++;
                int progressoCalculado = progressoAtual + (int)(((double)loc / total) * tamanhoEtapa);
                if (progressoCalculado != ultimoValor)
                {
                    owner.AtualizarProgresso(progressoCalculado);
                    ultimoValor = progressoCalculado;
                }
                if (owner.cancellationToken.IsCancellationRequested) return;
                Application.DoEvents();
                //if (g_cancel)
                 //   return -1;  // Substituindo o GoTo ErrorHandler
                /*
                if (i % (qtdDados / 10) == 0)
                    g_porc = (int)(90 + porc_aux / 11 * (i / (qtdDados / 10.0)));
                */
                if (eventos[i] > 0)
                {
                    if (eventos[i] == CodEventoApneia)
                        qtd_ap++;
                    else
                        qtd_hip++;

                    if (ini1 < 0)
                    {
                        ini1 = i;
                        fim1 = i;
                        Evento = eventos[i];
                    }
                    else
                    {
                        fim1 = i;
                    }
                }
                else if (ini1 >= 0)
                {
                    Evento = (qtd_ap > (qtd_ap + qtd_hip) * 0.3) ? CodEventoApneia : CodEventoHipopneia;

                    int npag = pag_ini + (int)(ini1 / Taxa);
                    int pos = (ini1 % Taxa * Taxa);
                    int duracao = (fim1 - ini1) * Taxa;

                    if (ini1 > 0)
                    {
                        int inicio = ini1;
                        int final = fim1;

                        eventosTemporarios.Add((inicio, final, Evento, AnalisarCanFluxo, Taxa));
                    }

                    ini1 = -1;
                    qtd_ap = 0;
                    qtd_hip = 0;
                }
            }

            total = eventosTemporarios.Count; loc = 0; ultimoValor = -1; progressoAtual += tamanhoEtapa;
            cnn = new OdbcConnection(connectionStringDatBd);
            connectionDatBd = new OdbcConnection(connectionStringDatBd);
            connectionDatBd.Open();

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


            g_porc = 100;
            Application.DoEvents();
            owner.AtualizarProgresso(99);

        }
        static OdbcConnection cnn;
        static OdbcConnection connectionDatBd;
        private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";

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

                    inicio = inicio * (GlobVar.namos / taxa);
                    termino = termino * (GlobVar.namos / taxa);

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
                                MenorSat = Convert.ToInt16(rs.Rows[0]["MenorSat"]);
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

        public int F_Somatoria(float[] dados, int start, int maximo)
        {
            int retorno = 0;

            // Verifica se o start é dentro dos limites do array
            if (start < 0 || start >= dados.Length)
                return retorno;

            // Soma os valores dentro do intervalo desejado
            for (int i = start; i < maximo && i < dados.Length; i++)
            {
                retorno += (int)dados[i];
            }

            return retorno;
        }
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

    }
}
