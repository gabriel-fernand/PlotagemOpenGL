using Accord.Math;
using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    internal class RegraPLM
    {
        private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";

        DataTable DtMovimentoPnPlm = new DataTable();
        int codCanal;

        float taxaAmos;
        float DurMinEv;
        float DurMaxEv;
        float DistanciaMin;
        float DistanciaMax;
        float DurJanBasal;
        float DurJanEvento;
        float IntervaloMinEntreEv;
        float FatorAmplituide;
        bool Est_0 = false;
        float QtdMinParaSerPLM;
        float NumVezesDurMinEv;
        float NumVezesAmplitudeBasal;
        float tipoAmplBasal;
        float FatorMultAmpEstagio;

        public RegraPLM(int codCanal)
        {
            if (!VerificaSeExisteEvento()) return;
            this.codCanal = codCanal;
            EncheDt();
            AnalisaEventos();
        }
        private bool VerificaSeExisteEvento()
        {
            bool tem = false;
            
            if(GlobVar.eventos == null)
            {
                tem = false;
            }
            else
            {
                tem = GlobVar.eventos.AsEnumerable().Any(row => row.Field<int>("CodEvento") == 12 || row.Field<int>("CodEvento") == 22);
            }
            return tem;
        }
        private void EncheDt()
        {
            DtMovimentoPnPlm = GlobVar.eventosUpdate.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 12 || row.Field<int>("CodEvento") == 22).CopyToDataTable();
        }
        private void AnalisaEventos()
        {
            if (GlobVar.tbl_ParametrosParaAnalisar == null) return;

            var row = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];
            int indexx = GlobVar.codCanal.IndexOf(codCanal);
            int taxa = GlobVar.txPorCanal[indexx];

            if (row["PLM_Dur_Min_Ev"] == DBNull.Value || row["PLM_Dur_Max_Ev"] == DBNull.Value || row["PLM_Distancia_Min"] == DBNull.Value
                || row["PLM_Distancia_Max"] == DBNull.Value || row["PLM_Dur_Jan_Basal"] == DBNull.Value || row["PLM_Dur_Jan_Evento"] == DBNull.Value
                || row["PLM_Interv_Min_Entre_Ev"] == DBNull.Value || row["PLM_Fator_Amplitude"] == DBNull.Value || row["PLM_Est_0"] == DBNull.Value || row["PLM_Qtd_Min_Para_Ser_PLM"] == DBNull.Value
                || row["PLM_Tipo_Ampl_Basal"] == DBNull.Value || row["PLM_Num_Vezes_Dur_Min_Ev"] == DBNull.Value || row["PLM_Num_Vezes_Amplitude_Basal"] == DBNull.Value || row["PLM_Fator_Mult_Ampl_Estagio"] == DBNull.Value) { return; }
            else
            {

                DurMinEv = (float)Convert.ToDouble(row["PLM_Dur_Min_Ev"]) * taxa;
                DurMaxEv = Convert.ToInt32(row["PLM_Dur_Max_Ev"]) * taxa;
                DistanciaMin = Convert.ToInt32(row["PLM_Distancia_Min"]) * taxa;
                DistanciaMax = Convert.ToInt32(row["PLM_Distancia_Max"]) * taxa;
                DurJanBasal = Convert.ToInt32(row["PLM_Dur_Jan_Basal"]) * taxa;
                DurJanEvento = (float)Convert.ToDouble(row["PLM_Dur_Jan_Evento"]) * taxa;
                IntervaloMinEntreEv = (float)Convert.ToDouble(row["PLM_Interv_Min_Entre_Ev"]) * taxa;
                FatorAmplituide = Convert.ToInt32(row["PLM_Fator_Amplitude"]);
                Est_0 = Convert.ToBoolean(row["PLM_Est_0"]);
                QtdMinParaSerPLM = Convert.ToInt32(row["PLM_Qtd_Min_Para_Ser_PLM"]);
                NumVezesDurMinEv = Convert.ToInt32(row["PLM_Num_Vezes_Dur_Min_Ev"]);
                tipoAmplBasal = Convert.ToInt32(row["PLM_Num_Vezes_Amplitude_Basal"]);
                FatorMultAmpEstagio = Convert.ToInt32(row["PLM_Fator_Mult_Ampl_Estagio"]);

            }
            int lastIni = -1;
            int lastFim = -1;
            int lasCodCanal = 0;
            int primeiraSeq = 0;
            int eventosProximos = 0;

            List<(int inicio, int fim)> eventosEncontrados = new();
            List<DataRow> eventosDeletar = new();
            List<(int inicio, int duracao, int codEvento, int codCanal, int taxa)> eventosAdicionar = new();

            //PASSO 1 - junta eventos cujo intervalo entre eles seja menor que o intervalo minimo
            foreach (DataRow rw in DtMovimentoPnPlm.Rows)
            {
                if (lastIni < 0)
                {
                    lasCodCanal = Convert.ToInt32(rw["CodCanal1"]);
                    lastIni = Convert.ToInt32(rw["Inicio"]);
                    lastFim = Convert.ToInt32(rw["Duracao"]);
                    primeiraSeq = Convert.ToInt32(rw["Seq"]);
                    eventosEncontrados.Add((lastIni, lastFim));
                    eventosDeletar.Add(rw);
                }
                else
                {
                    if(lastFim + DistanciaMin > Convert.ToInt32(rw["Inicio"]) && lasCodCanal == Convert.ToInt32(rw["CodCanal1"]))
                    {
                        eventosProximos++;
                        lastIni = Convert.ToInt32(rw["Inicio"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        eventosEncontrados.Add((lastIni, lastFim));
                        eventosDeletar.Add(rw);
                    }
                    else if(eventosProximos >= QtdMinParaSerPLM)
                    {
                        int inicio = eventosEncontrados[0].inicio;
                        int final = eventosEncontrados[eventosEncontrados.Count - 1].fim;
                        int codEvento = 22;
                        int codCanal = lasCodCanal;
                        int tx = taxa;

                        eventosAdicionar.Add((inicio, final, codEvento, codCanal, tx));

                        eventosEncontrados.Clear();
                        eventosProximos = 0;
                        lasCodCanal = Convert.ToInt32(rw["CodCanal1"]);
                        lastIni = Convert.ToInt32(rw["Inicio"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        primeiraSeq = Convert.ToInt32(rw["Seq"]);
                        eventosEncontrados.Add((lastIni, lastFim));
                        eventosDeletar.Add(rw);

                    }
                    else
                    {
                        eventosEncontrados.Clear();
                        eventosProximos = 0;
                        lasCodCanal = Convert.ToInt32(rw["CodCanal1"]);
                        lastIni = Convert.ToInt32(rw["Inicio"]);
                        lastFim = Convert.ToInt32(rw["Duracao"]);
                        primeiraSeq = Convert.ToInt32(rw["Seq"]);
                        eventosEncontrados.Add((lastIni, lastFim));
                        eventosDeletar.Add(rw);
                    }
                }
            }
        }
    }
}
