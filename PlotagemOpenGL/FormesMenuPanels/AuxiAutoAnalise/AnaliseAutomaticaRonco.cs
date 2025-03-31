using Accord.Math;
using ADODB;
using ClassesBDNano;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.BD;
using PlotagemOpenGL.Filtros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public AnaliseAutomaticaRonco(int CodCanal, bool excluirEvento)
        {
            sinal = new float[GlobVar.matrizCompleta.GetLength(1)];
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

        private void ParametrosParaAnalisar()
        {
            if (GlobVar.tbl_ParametrosParaAnalisar == null) { return; }

            var row = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];

            if (row["Ronco_Dur_Min_Ev"] == DBNull.Value || row["Ronco_Dur_Max_Ev"] == DBNull.Value || row["Ronco_Dur_Jan_Basal"] == DBNull.Value
                || row["Ronco_Dur_Jan_Evento"] == DBNull.Value || row["Ronco_Interv_Min_Entre_Ev"] == DBNull.Value || row["Ronco_Fator_Amplitude"] == DBNull.Value
                || row["Ronco_Tipo_Ampl_Basal"] == DBNull.Value || row["Ronco_Num_Vezes_Dur_Min_Ev"] == DBNull.Value || row["Ronco_Num_Vezes_Amplitude_Basal"] == DBNull.Value) { return; }
            else
            {
                int indexx = GlobVar.codCanal.IndexOf(codCanal);
                int taxa = GlobVar.txPorCanal[indexx];

                DurMinEv = Convert.ToInt32(row["Ronco_Dur_Min_Ev"]) * taxa;
                DurMaxEv = Convert.ToInt32(row["Ronco_Dur_Max_Ev"]) * taxa;
                DistanciaMin = Convert.ToInt32(row["Ronco_Distancia_Min"]) * taxa;
                DistanciaMax = Convert.ToInt32(row["Ronco_Distancia_Max"]) * taxa;
                DurJanBasal = Convert.ToInt32(row["Ronco_Dur_Jan_Basal"]) * taxa;
                DurJanEvento = Convert.ToInt32(row["Ronco_Dur_Jan_Evento"]) * taxa;
                IntervaloMinEntreEv = Convert.ToInt32(row["Ronco_Interv_Min_Entre_Ev"]) * taxa;

                FatorAmplituide = Convert.ToInt32(row["Ronco_Fator_Amplitude"]);
                NumVezesDurMinEv = Convert.ToInt32(row["Ronco_Num_Vezes_Dur_Min_Ev"]); ;
                NumVezesAmplitudeBasal = Convert.ToInt32(row["Ronco_Num_Vezes_Amplitude_Basal"]); ;
                tipoAmplBasal = Convert.ToInt32(row["Ronco_Tipo_Ampl_Basal"]);
                FatorMultAmpEstagio = Convert.ToInt32(row["Ronco_Fator_Mult_Ampl_Estagio"]);

            }

            float[] jan_Basal = new float[(int)DurJanBasal];
            float[] jan_Basal_Aux = new float[(int)DurJanBasal];

            float[] jan_evento = new float[(int)DurJanEvento];
            float[] jan_evento_Aux = new float[(int)DurJanEvento];

            int[] eventos = new int[sinal.Length];

            Array.Copy(sinal, 0, jan_Basal, 0, (int)DurJanBasal);
            Array.Copy(sinal, (int)DurJanBasal, jan_evento, 0, (int)DurJanEvento);

            float soma_evento = jan_evento.Sum();
            float soma_basal = jan_Basal.Sum();

            // Percorrer os dados e detectar eventos
            for (int i = (int)(DurJanBasal + DurJanEvento - 1); i < sinal.Length; i++)
            {
                //if (g_cancel) return;  // Interromper se necessário

                // Atualizar janela do evento (deslocamento para a esquerda)
                float evento_0 = jan_evento[0];
                Array.Copy(jan_evento, 1, jan_evento, 0, (int)DurJanEvento - 1);
                jan_evento[(int)DurJanEvento - 1] = sinal[i];

                // Atualizar soma da janela do evento
                soma_evento = soma_evento - evento_0 + sinal[i];

                // Calcular médias
                float media_evento = soma_evento / DurJanEvento;
                float media_basal = soma_basal / DurJanBasal;
                if (media_basal == 0) media_basal = 1;

                // Verificar se é um evento
                if ((media_evento / FatorAmplituide) >= media_basal)
                {
                    for (int j = 0; j < DurJanEvento; j++)
                    {
                        eventos[j + i - ((int)DurJanEvento - 1)] = 1;
                    }
                }
                else
                {
                    // Atualizar janela basal (deslocamento para a esquerda)
                    float basal_0 = jan_Basal[0];
                    Array.Copy(jan_Basal, 1, jan_Basal, 0, (int)DurJanBasal - 1);
                    jan_Basal[(int)DurJanBasal - 1] = sinal [i];

                    // Atualizar soma da janela basal
                    soma_basal = soma_basal - basal_0 + sinal[i];
                }
            }

            int qtdDados = sinal.Length;
            int ini1 = -1, fim1 = -1;
            float min, max, ampl_evento;

            // Passo 1: Incluir eventos válidos
            for (int i = 0; i < qtdDados; i++)
            {
                //if (g_cancel) return; // Interromper se necessário

                if (eventos[i] > 0)
                {
                    if (ini1 < 0)
                    {
                        ini1 = i;
                        fim1 = i;
                    }
                    else
                    {
                        fim1 = i;
                    }
                }
                else if (ini1 >= 0)
                {
                    // Calcular amplitude do evento detectado
                    min = 512;
                    max = -512;
                    for (int j = ini1; j <= fim1; j++)
                    {
                        if (sinal[j] < min) min = sinal[j];
                        if (sinal[j] > max) max = sinal[j];
                    }

                    ampl_evento = Math.Abs(min) + Math.Abs(max);

                    // Verificar se o evento deve ser descartado
                    if (ampl_evento < tipoAmplBasal ||
                        ((fim1 - ini1 < DurJanEvento * NumVezesDurMinEv) &&
                        (ampl_evento < tipoAmplBasal * NumVezesAmplitudeBasal)))
                    {
                        for (int j = ini1; j <= fim1; j++)
                        {
                            eventos[j] = 0; // Remove o evento
                        }
                    }

                    ini1 = -1;
                }
            }

            // Passo 2: Unir eventos próximos
            ini1 = -1;
            fim1 = -1;
            bool busca_ini2 = false;

            for (int i = 0; i < qtdDados; i++)
            {
                //if (g_cancel) return; // Interromper se necessário

                if (eventos[i] > 0)
                {
                    if (busca_ini2)
                    {
                        busca_ini2 = false;
                        // Se o intervalo entre eventos for menor que o mínimo, une os eventos
                        if ((fim1 + IntervaloMinEntreEv > i) && (fim1 - ini1 > DurMinEv / 2.5))
                        {
                            for (int j = fim1 + 1; j < i; j++)
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
                else
                {
                    if (ini1 >= 0)
                    {
                        // Encontramos um zero depois de um evento, buscamos o próximo evento
                        busca_ini2 = true;
                    }
                }
            }

            qtdDados = eventos.Length;
            ini1 = -1; fim1 = -1;
            int npag = 0, ultPagEv = -1;
            int pos, duracao, Evento;

            // PASSO 3 - Remove eventos com duração menor que DurMinEv ou maior que DurMaxEv
            for (int i = 0; i < qtdDados; i++)
            {
                //if (g_cancel) return 0;

                if (eventos[i] == 0)
                {
                    if (ini1 >= 0)
                    {
                        if ((fim1 - ini1 < DurMinEv) || (fim1 - ini1 > DurMaxEv))
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
                else
                {
                    if (ini1 < 0) ini1 = i;
                    fim1 = i;
                }
            }

            // Verifica o último evento ao sair do loop
            if (ini1 >= 0 && ((fim1 - ini1 < DurMinEv) || (fim1 - ini1 > DurMaxEv)))
            {
                for (int j = ini1; j <= fim1; j++)
                {
                    eventos[j] = 0;
                }
            }
            int indexxs = GlobVar.codCanal.IndexOf(codCanal);
            int taxas = GlobVar.txPorCanal[indexxs];
            Evento = 13;

            // PASSO 4 - Inclui eventos no banco de dados
            ini1 = -1;
            for (int i = 0; i < qtdDados; i++)
            {
                //if (g_cancel) return 0;

                if (eventos[i] > 0)
                {
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

                    AdicionarEventoAoDataTable(ini1, fim1, Evento, codCanal, taxas);

                    ini1 = -1;
                }
            }
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

    }
}
