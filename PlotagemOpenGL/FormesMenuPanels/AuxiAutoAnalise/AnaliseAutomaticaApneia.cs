using Accord.Math;
using ClassesBDNano;
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

        bool excluirEvento = false;

        public AnaliseAutomaticaApneia(int limiarAp = 0, int LimiarHip = 0, int DurMin = 0, int intervaloEvent = 0, int TamanhoJanBasal = 0, int TamanhoJanEventos = 0, int AbalisarCanFLuxo = 0, bool excluirEvento = false)
        {
            this.LimiarAp = limiarAp;
            this.LimiarHip = LimiarHip;
            this.DurMin = DurMin;
            this.intervaloEvent = intervaloEvent;
            this.TamanhoJanBasal = TamanhoJanBasal;
            this.TamanhoJanelaEventos = TamanhoJanEventos;
            this.AnalisarCanFluxo = AbalisarCanFLuxo;
            this.excluirEvento = excluirEvento;

            if (!verificaExistenciaDoCanalNaMontagem(AbalisarCanFLuxo)) return;

            dados = new float[GlobVar.indiceDat];
            verificaFiltroEPegaDados();

            tbl_ParametrosParaAnalise();
            this.excluirEvento = excluirEvento;
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
            int hightab = row["PassaAlta"] == DBNull.Value ? 0 : Convert.ToInt32(row["PassaAlta"]);

            if(lowtab == low && hightab == high)
            {
                captaDados();
            }
            else
            {
                int codcanal = AnalisarCanFluxo;
                int codindex = GlobVar.codSelected.IndexOf(codcanal);
                int indexx = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
                int Taxa = GlobVar.txPorCanal[indexx];

                // Verifica se o índice existe
                if (codindex >= 0 && codindex < GlobVar.grafSelected.Length)
                {
                    // Loop para acumular valores
                    int h = 0;
                    for (int g = 0; g < GlobVar.matrizCanal.GetLength(1); g += GlobVar.namosNumerico)
                    {
                        if (h < dados.Length)
                        {
                            // Acumula os valores correspondentes
                            dados[h] = GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
                        }
                        h++;
                    }
                }
                dados = BandPass.ApplyFilter((dados), (float)low, (float)high, Taxa);

            }
        }

        private void captaDados()
        {
            int ponteiroI = GlobVar.ponteiroI[AnalisarCanFluxo];
            int ponteiroF = GlobVar.ponteiroF[AnalisarCanFluxo];

            int indexx = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
            int Taxa = GlobVar.txPorCanal[indexx];

            int h = 0;
            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
            {
                int colunaComp = ponteiroI;
                while (colunaComp < ponteiroF)
                {
                    dados[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                    colunaComp ++;
                    h++;
                }
            }
        }

        private void tbl_ParametrosParaAnalise()
        {
            int g_porc = 0;
            int porc_aux = 0;
            int j;
            // Índice do canal a ser analisado
            int indexx = GlobVar.codCanal.IndexOf(AnalisarCanFluxo);
            int Taxa = GlobVar.txPorCanal[indexx];
            int qtdDadosAux = Taxa * GlobVar.npagin;
            int qtdDados = Taxa * GlobVar.npagin; // Mesmo cálculo; ajuste se necessário

            int CodEventoHipopneia = 5;
            int CodEventoApneia = 2;

            // Obtém os dados da tabela tbl_Paginas (não utilizado posteriormente neste trecho)
            var tbl_Pagina = GlobVar.tbl_Paginas;

            if (excluirEvento)
            {
                // Exclui eventos no DataTable `GlobVar.eventosUpdate`
                List<int> codigosExcluir = new List<int> { 2, 5, 1, 3, 4, 6, 101 };

                foreach(int a in codigosExcluir)
                {
                    ExcluiEvento(a);
                }
                foreach (DataRow row in GlobVar.eventosUpdate.Rows.Cast<DataRow>().ToList())
                {
                    if (codigosExcluir.Contains(Convert.ToInt32(row["CodEvento"])))
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

            g_porc = 7;
            for(int a = 0; a < dados.Length; a++)
            {
                if (dados[a] > 32000) dados[a] = 32000;
                else dados[a] = Math.Abs(dados[a]);
            }
            // Copia os dados para o buffer basal e para o buffer de evento
            // Assume-se que 'dados' é um array float[] previamente preenchido.
            Array.Copy(dados, 0, jan_basal, 0, Dur_Jan_Basal);
            // Copia a partir do índice Dur_Jan_Basal para o buffer de evento, iniciando no índice 1
            Array.Copy(dados, Dur_Jan_Basal, jan_evento, 1, Dur_Jan_Evento - 1);

            g_porc = 10;

            // PASSO 1 - Detecção de eventos
            float soma_evento = F_Somatoria(jan_evento, 0, Dur_Jan_Evento);
            float soma_basal = F_Somatoria(jan_basal, 0, Dur_Jan_Basal);
            float media_evento, media_basal;
            int desloc = 0; // Verifique se desloc precisa ser atualizado em outro momento
            int Evento = 0;

            for (int i = Dur_Jan_Basal + Dur_Jan_Evento - 1; i < qtdDadosAux; i++)
            {
                soma_evento = F_Somatoria(jan_evento, 0, Dur_Jan_Evento);
                soma_basal = F_Somatoria(jan_basal, 0, Dur_Jan_Basal);

                // Verificação de cancelamento se necessário (ex.: if (g_cancel) return;)

                // Atualize o buffer de evento: "shift" à esquerda
                int evento_0 = (int)jan_evento[0];
                Array.Copy(jan_evento, 1, jan_evento_aux, 0, jan_evento.Length - 1);
                Array.Copy(jan_evento_aux, 0, jan_evento, 0, jan_evento.Length - 1);
                // Preenche a última posição com o valor atual de 'dados'
                jan_evento[Dur_Jan_Evento - 1] = (int)dados[i];

                soma_evento = soma_evento - evento_0 + dados[i];

                media_evento = (int)soma_evento / Dur_Jan_Evento;
                media_basal = (int)(soma_basal / Dur_Jan_Basal);
                if (media_basal == 0)
                    media_basal = 1;

                // Detecta Apneia ou Hipopneia
                if (media_evento < media_basal * LimiarHip || media_evento < media_basal * LimiarAp)
                {
                    if (media_evento < media_basal * LimiarAp)
                        Evento = CodEventoApneia;
                    else
                        Evento = CodEventoHipopneia;

                    // Marca o evento para as amostras correspondentes
                    for (int h = 0; h < Dur_Jan_Evento; h++)
                    {
                        eventos[desloc + h + i - (Dur_Jan_Evento - 1)] = Evento;
                    }
                }
                else
                {
                    // "Shift" no buffer basal
                    int basal_0 = (int)jan_basal[0];
                    Array.Copy(jan_basal, 1, jan_basal_aux, 0, jan_basal.Length - 1);
                    Array.Copy(jan_basal_aux, 0, jan_basal, 0, jan_basal.Length - 1);
                    jan_basal[Dur_Jan_Basal - 1] = (int)dados[i];
                    soma_basal = soma_basal - basal_0 + dados[i];
                }
            }

            g_porc = 30;
            g_porc = 40;

            // PASSO 2 - Junta eventos cujo intervalo entre eles seja menor que o intervalo mínimo
            int ini1 = -1;
            int fim1 = -1;
            bool busca_ini2 = false;
            porc_aux = 10;
            for (int i = 0; i < qtdDadosAux; i++)
            {
                // Atualiza o progresso (se necessário)
                if (i % (qtdDadosAux / 10) == 0)
                {
                    g_porc = 40 + (porc_aux / 11) * (i / (qtdDadosAux / 10));
                }

                if (eventos[desloc + i] > 0)
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
                                ((fim1 - ini1 > Dur_min_ev / 2) && (i - fim1 < Interv_Min_Entre_Ev / 2)))
                            {
                                for (int b = 1; b <= i - fim1; b++)
                                {
                                    eventos[desloc + b + fim1] = Evento;
                                }
                                fim1 = i;
                            }
                            else
                            {
                                // Inicia um novo evento se a duração for inválida
                                if (fim1 - ini1 < Dur_min_ev)
                                {
                                    for (int b = 0; b <= fim1 - ini1; b++)
                                    {
                                        eventos[desloc + b + ini1] = 0;
                                    }
                                }
                                ini1 = i;
                                fim1 = i;
                                Evento = eventos[desloc + i];
                            }
                        }
                        else
                        {
                            // Inicia um novo evento
                            if (fim1 - ini1 < Dur_min_ev)
                            {
                                for (int b = 0; b <= fim1 - ini1; b++)
                                {
                                    eventos[desloc + b + ini1] = 0;
                                }
                            }
                            ini1 = i;
                            fim1 = i;
                            Evento = eventos[desloc + i];
                        }
                    }
                    else if (ini1 < 0)
                    {
                        // Primeira ocorrência de um evento
                        ini1 = i;
                        fim1 = i;
                        Evento = eventos[desloc + i];
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

            // PASSO 3 - Elimina eventos com duração menor que Dur_min_ev ou maior que Dur_Max_Ev
            ini1 = -1;
            fim1 = -1;
            porc_aux = 10;
            for (int i = 0; i < qtdDados; i++)
            {
                if (i % (qtdDados / 10) == 0)
                {
                    g_porc = 60 + (porc_aux / 11) * (i / (qtdDados / 10));
                }

                if (eventos[i] == 0)
                {
                    if (ini1 >= 0)
                    {
                        if ((fim1 - ini1 < Dur_min_ev) || (fim1 - ini1 > Dur_Max_Ev))
                        {
                            for (int b = 0; b <= fim1 - ini1; b++)
                            {
                                eventos[b + ini1] = 0;
                            }
                        }
                        ini1 = -1;
                        fim1 = -1;
                    }
                }
                else if (ini1 < 0)
                {
                    ini1 = i;
                    fim1 = i;
                }
                else
                {
                    fim1 = i;
                }
            }

            // Verifica se há um último evento não finalizado
            if (ini1 >= 0 && ((fim1 - ini1 < Dur_min_ev) || (fim1 - ini1 > Dur_Max_Ev)))
            {
                for (int b = 0; b <= fim1 - ini1; b++)
                {
                    eventos[b + ini1] = 0;
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
            for (int i = 0; i < qtdDados; i++)
            {
                Application.DoEvents();
                //if (g_cancel)
                 //   return -1;  // Substituindo o GoTo ErrorHandler

                if (i % (qtdDados / 10) == 0)
                    g_porc = (int)(90 + porc_aux / 11 * (i / (qtdDados / 10.0)));

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
                        int inicio = ini1 * (GlobVar.namos / Taxa);
                        int final = fim1 * (GlobVar.namos / Taxa);

                        AdicionarEventoAoDataTable(inicio, final, Evento, AnalisarCanFluxo);
                    }

                    ini1 = -1;
                    qtd_ap = 0;
                    qtd_hip = 0;
                }
            }

            g_porc = 100;
            Application.DoEvents();

        }
        public static void AdicionarEventoAoDataTable(int inicio, int termino,int codEvento, int codcanal1)
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
                    int numPagInicio = inicio / 512;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    int numPagTermino = termino / 512;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    string numPag = $"{numPagInicio} -- {numPagTermino}";
                    // Obter o próximo valor de Seq
                    int seq = plotComentatios.AtualizarProxSeqEvento();

                    plotEventos.minSaturacao(numPagInicio, numPagTermino);
                    int minSat = GlobVar.minSat.Min();
                    string posi = plotEventos.Posicao(numPagInicio, numPagTermino);

                    // Adicionar dados ao DataTable
                    GlobVar.eventosUpdate.Rows.Add(seq, numPag, codEvento, codcanal1, inicio, termino, minSat, posi);
                    AlteraBD.GravaEvento(seq, numPagInicio, codEvento, codcanal1, -1, inicio, termino, GlobVar.namos, numPagTermino, minSat, posi);
                    // Exportar DataTable para Excel
                    string excelFilePath = @"C:\Teste\Teste";
                    //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
                    eventos.Dispose();
                }
            }
            catch { }
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
        private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
        public static int ExcluiEvento(int codEvento)
        {
            try
            {
                int i = -1;
                using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                connectionDatBd.Open();

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE CodEvento = {codEvento};";

                using var DeleteCommand = new OdbcCommand(queryDelete, connectionDatBd);

                DeleteCommand.ExecuteNonQuery();

                connectionDatBd.Close();
                return i;
            }
            catch { int i = 0; return i; }
        }

    }
}
