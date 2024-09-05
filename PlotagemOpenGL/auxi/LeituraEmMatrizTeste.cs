using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Accord.Math;
using System.Data;
using System.Linq;
using PlotagemOpenGL.Filtros;


namespace PlotagemOpenGL.auxi
{
    internal class LeituraEmMatrizTeste
    {
        public static void LeituraDat()
        {
            int ntotal = 0;
            string[] dadoscanal = new string[47];
            byte[] buffer0 = new byte[395];
            byte[] buffer1 = new byte[8];
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[47];
            GlobVar.nomeCanais = new string[GlobVar.qtdCanais.Length];
            GlobVar.txPorCanal = new int[GlobVar.qtdCanais.Length];
            GlobVar.ponteiroI = new int[GlobVar.qtdCanais.Length];
            GlobVar.ponteiroF = new int[GlobVar.qtdCanais.Length];
            GlobVar.scale = new double[GlobVar.qtdCanais.Length];
            GlobVar.codCanal = new int[GlobVar.qtdCanais.Length];
            GlobVar.grafSelected = new int[GlobVar.qtdCanais.Length];
            GlobVar.codSelected = new int[GlobVar.qtdCanais.Length];
            GlobVar.SomenteNums = new bool[2];
            int jj = 0;


            using (FileStream fs = new FileStream(GlobVar.textFile, FileMode.Open, FileAccess.Read))
            {
                fs.Read(buffer0, 0, buffer0.Length);

                GlobVar.cabecalho = (Encoding.UTF8.GetString(buffer0));

                fs.Read(buffer1, 0, buffer1.Length);

                string npag1 = (Encoding.UTF8.GetString(buffer1));

                GlobVar.npagin = Convert.ToInt32(npag1);

                GlobVar.npag = npag1.Replace(" ", "");

                fs.Read(buffer2, 0, buffer2.Length);

                string tipocanais1 = (Encoding.UTF8.GetString(buffer2));

                GlobVar.tipocanais = tipocanais1.Replace(" ", "");

                int ncanint = Convert.ToInt16(tipocanais1.Replace(" ", "")); //Int16.Parse(tipocanais, System.Globalization.NumberStyles.HexNumber);
                GlobVar.qtdCanais = new string[ncanint];
                int txPorSeg = 0;
                for (int ich = 0; ich < ncanint; ich++)
                {
                    fs.Read(buffer3, 0, buffer3.Length);

                    dadoscanal[ich] = (Encoding.UTF8.GetString(buffer3));

                    string cod = dadoscanal[ich].Substring(0, 2);
                    GlobVar.qtdCanais[ich] = cod;
                    string phrase = dadoscanal[ich];
                    GlobVar.codCanal[ich] = Convert.ToInt16(phrase.Substring(0, 3).Trim()); //Faz a leitura do codigo do canal
                    GlobVar.nomeCanais[ich] = phrase.Substring(11, 10).Trim(); //Faz a leitura dos nomes de cada canal e armazena em um array
                    GlobVar.amos = Convert.ToInt16(phrase.Substring(8, 4));
                    string sizesample3 = phrase.Substring(8, 4);
                    string aux = sizesample3.Replace(" ", "");
                    int auxx = Convert.ToInt16(aux);
                    GlobVar.txPorCanal[ich] = auxx;

                    GlobVar.startpos = Convert.ToInt32(ntotal);
                    string sizesample1 = phrase.Substring(8, 4);
                    string banana = sizesample1.Replace(" ", "");

                    GlobVar.sizesample = (Convert.ToInt16(banana) * 2);
                    ntotal = ntotal + (GlobVar.amos * 2);

                    int ponteirostr = Convert.ToInt16(fs.Position);
                    GlobVar.ponteiroI[ich] = txPorSeg;
                    txPorSeg += auxx;
                    GlobVar.ponteiroF[ich] = txPorSeg;

                }

                GlobVar.matrizCompleta = new short[GlobVar.npagin, txPorSeg];

                GlobVar.size = Convert.ToInt32(GlobVar.npag);

                int recntotal = ntotal;

                fs.Position = fs.Position - 1;

                for (Int16 ich1 = 0; ich1 < GlobVar.size; ich1++) //leitura de 1 segundo size e igual a quantos segundos tem no arquivo dat, trazendo a Matriz completa de todos canais juntos
                {

                    byte[] buffer4 = new byte[ntotal];

                    fs.Read(buffer4, 0, buffer4.Length);

                    short[] bitstring = new short[GlobVar.sizesample / 2];

                    var limite = GlobVar.startpos + GlobVar.sizesample;

                    for (Int16 i = 0, j = 0; i < ntotal; i += 2, j++)
                    {
                        GlobVar.matrizCompleta[ich1, j] = BitConverter.ToInt16(buffer4, i);

                    }
                }
                fs.Close();

                GlobVar.indiceDat = GlobVar.npagin * GlobVar.amos * 2;
                //int[] pontF = GlobVar.ponteiroF;
                GlobVar.matrizCanal = new short[GlobVar.tbl_MontagemSelecionada.Rows.Count, GlobVar.indiceDat];

                //Separa os valores de cada canal para a MatrizCanal, para poder desenhar eles separadamente
                for (int linhaCanais = 0; linhaCanais < GlobVar.tbl_MontagemSelecionada.Rows.Count; linhaCanais++)
                {
                    int colunaCanalIndex = 0;

                    // Percorre as linhas da matrizCompleta
                    for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0); linhaComp++)
                    {
                        // Percorre as colunas da matrizCompleta no intervalo especificado por pontI e pontF 
                        for (int colunaComp = GlobVar.ponteiroI[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]))]; colunaComp < GlobVar.ponteiroF[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]))]; colunaComp++)
                        {
                            // Certifique-se de não exceder os limites da matrizCanais
                            if (colunaCanalIndex < GlobVar.matrizCanal.GetLength(1))
                            {
                                // Copia o valor de matrizCompleta para matrizCanal
                                GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = Convert.ToInt16(GlobVar.matrizCompleta[linhaComp, colunaComp]);
                                colunaCanalIndex++;
                            }
                            //else
                            //{
                                // Se exceder os limites da matrizCanal, saia do loop
                            //    break;
                            //}
                        }
                    }
                }
                reorganize();
                foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    if(GlobVar.tbl_MontagemSelecionada.Rows[jj]["CodCanal2"] == DBNull.Value){
                        GlobVar.tbl_MontagemSelecionada.Rows[jj]["CodCanal2"] = -1;
                    }
                    jj++;
                }
                    //Verifica se o canal da montagem tem referencia a outro canal do exame
                    foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    int codCanal1 = row.Field<int>("CodCanal1");
                    int codCanal2 = Convert.ToInt32(row.Field<int>("CodCanal2"));
                    if (codCanal2 != -1)
                    {
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(codCanal1), SetReferencia(codCanal1, codCanal2));
                    }
                }

                //Verificacao se a filtro no canal
                int ind = 0;
                foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    
                    double? lowHertz = null;
                    double? highHertz = null;
                    double? notchHertz = null;

                    if (!row.IsNull("PassaBaixa"))
                    {
                        double passaBaixaValue = row.Field<double>("PassaBaixa");
                        if (passaBaixaValue != 0)
                        {
                            lowHertz = passaBaixaValue;
                        }
                    }

                    if (!row.IsNull("PassaAlta"))
                    {
                        double passaAltaValue = row.Field<double>("PassaAlta");
                        if (passaAltaValue != 0)
                        {
                            highHertz = passaAltaValue;
                        }
                    }

                    if (!row.IsNull("Notch"))
                    {
                        double notchValue = row.Field<double>("Notch");
                        if (notchValue != 0)
                        {
                            notchHertz = notchValue;
                        }
                    }

                    if (lowHertz != null)
                    {
                        if (highHertz != null)
                        {
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                            ShortToFloat(BandPass.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))),
                            (float)lowHertz,
                            (float)highHertz,
                            GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])]
                            )));
                        }
                        else
                        {
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                                ShortToFloat(PaissaBaixa.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))), (float)lowHertz, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])] )));
                        }
                    }
                    if (lowHertz == null && highHertz != null)
                    {
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                                    ShortToFloat(PaissaAlta.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))), (float)highHertz, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])])));
                        
                    }

                    if (notchHertz != null)
                    {
                        if(lowHertz == null) { lowHertz = 0; }
                        if(highHertz == null) {  highHertz = 0; }

                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])),
                                ShortToFloat(Notch.ApplyFilter(FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[ind]["CodCanal1"])))), (float)notchHertz, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[ind])])));
                         
                    }
                    if (notchHertz != null || highHertz != null || lowHertz != null)
                    {
                        if (lowHertz == null) { lowHertz = 0; }
                        if (highHertz == null) { highHertz = 0; }
                        if (notchHertz == null) { notchHertz = 0; }
                        Tela_Plotagem.UpdateBeforeLoad(ind + 1, (double)lowHertz, (double)highHertz, (double)notchHertz);
                    }
                    ind++;
                }

                for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    float scala = (float)( Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]) / Ampli(CodTipo(i))) ;
                    GlobVar.scale[i] = scala;
                }
                
            }
        }
        public static short[] SetReferencia(int principal, int referencia)
        {
            short[] novoArray = new short[GlobVar.matrizCanal.GetLength(1)];
            short[] Sla = new short[novoArray.Length];
            Sla = Referencia(referencia);
            for (int i = 0; i < novoArray.Length; i++)
            {
                //Verifica se o numero de referencia e negativo para fazer o calculo
                int aux = Sla[i];
                if (aux < 0)
                {
                    aux *= 1;
                }
                novoArray[i] = (short)(GlobVar.matrizCanal[GlobVar.codSelected.IndexOf(principal), i] - aux);
            }
            return novoArray;
        }
        public static short[] Referencia(int codReferencia)
        {
            // Tamanho do array referencia baseado no número de linhas na matrizCompleta
            short[] referencia = new short[GlobVar.matrizCanal.GetLength(1)];

            // Encontra os índices de início e fim para a coluna com base em codReferencia
            int startCol = GlobVar.ponteiroI[GlobVar.codCanal.IndexOf(codReferencia)];
            int endCol = GlobVar.ponteiroF[GlobVar.codCanal.IndexOf(codReferencia)];

            // Percorre as linhas da matrizCompleta
            for (int pontRef = 0; pontRef < referencia.Length;)
            {
                // Percorre as colunas da matrizCompleta no intervalo especificado por startCol e endCol
                for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0); linhaComp++)
                {
                    for (int colunaComp = startCol; colunaComp < endCol; colunaComp++)
                    {
                        // Copia o valor de matrizCompleta para o array linhaValores
                        referencia[pontRef] = Convert.ToInt16(GlobVar.matrizCompleta[linhaComp, colunaComp]);
                        pontRef++;
                    }
                }
            }
            return referencia;
        }
        public static short[] RemoverMetadeParaFrente(short[] array, int vezes)
        {
            int novaTamanho = array.Length / vezes;
            short[] novoArray = new short[novaTamanho];

            for (int i = 0; i < novaTamanho; i++)
            {
                novoArray[i] = array[i];
            }
            novoArray = DuplicarArray(novoArray, vezes);
            return novoArray;
        }
        public static short[] DuplicarArray(short[] array, int multiplicacao)
        {
            // Cria um novo array com o tamanho necessário
            short[] novoArray = new short[array.Length * multiplicacao];

            // Itera sobre os elementos do array original
            for (int i = 0; i < array.Length; i++)
            {
                // Duplica o valor do elemento atual no novo array
                for (int j = 0; j < multiplicacao; j++)
                {
                    novoArray[i * multiplicacao + j] = array[i];
                }
            }

            return novoArray;
        }
        public static void reorganize()
        {
            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                GlobVar.grafSelected[i] = i;
                GlobVar.codSelected[i] = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["CodCanal1"]);
            }
        }
        public static float[] FloatToShort(short[] input)
        {
            float[] output = new float[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input[i];
            }

            return output;
        }
        public static short[] ShortToFloat(float[] input)
        {
            short[] output = new short[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (short)input[i];
            }

            return output;
            
        }
        public static string CodTipo(int Index)
        {
            string output = "";
            int codTipo = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[Index]["CodTipoCanal"]);
            var row = GlobVar.tbl_CadTipoCanal.AsEnumerable().Where(r => r.Field<int>("CodTipo") == codTipo).FirstOrDefault();
            string codSigla = row.Field<string>("Sigla");
            

            if (codSigla.Equals("AIRFLOW"))
                    output = "g_airflow";
                else if (codSigla.Equals("CANULA")) 
            output = "g_canula";
                else if (codSigla.Equals("BODYPOS")) 
            output = "g_bodypos";
                else if (codSigla.Equals("SAO2")) 
            output = "g_sao2";
                else if (codSigla.Equals("CINTA_ABDOM")) 
            output = "g_abdom";
               else if (codSigla.Equals("CINTA_TORAX")) 
            output = "g_torax";
               else if (codSigla.Equals("PES")) 
            output = "g_pes";
               else if (codSigla.Equals("MICROFONE")) 
            output = "g_microf";
               else if (codSigla.Equals("FOTO_ESTIM")) 
            output = "g_foto";
               else if (codSigla.Equals("CPAP")) 
            output = "g_cpap";
               else if (codSigla.Equals("BPAP")) 
            output = "g_epap";
               else if (codSigla.Equals("CAP_RR")) 
            output = "g_cap_RR";
              else if (codSigla.Equals("CAP_CO2")) 
            output = "g_cap_CO2";
              else if (codSigla.Equals("CAP_ETCO2")) 
            output = "g_cap_EtCO2";
              else if (codSigla.Equals("SAO2_SERIAL")) 
            output = "g_sao2_ser";
             else if (codSigla.Equals("FC_SERIAL")) 
            output = "g_fc_ser";
              else if (codSigla.Equals("EMG_PERNA")) 
            output = "g_perna";
              else if (codSigla.Equals("PA_MAX")) 
            output = "g_pamax";
              else if (codSigla.Equals("PA_MIN")) 
            output = "g_pamin";
             else if (codSigla.Equals("PH_1")) 
            output = "g_ph1";
              else if (codSigla.Equals("PH_2")) 
            output = "g_ph2";
             else if (codSigla.Equals("PH_3")) 
            output = "g_ph3";
              else if (codSigla.Equals("CPAP_VAZ")) 
            output = "g_cpap_vaz";
              else if (codSigla.Equals("CO2_EXALADADO")) 
            output = "g_co2_exal";
              else if (codSigla.Equals("CO2_INSPIRADO")) 
            output = "g_co2_insp";
              else if (codSigla.Equals("FREQ_RESP")) 
            output = "g_fresp";
             else if (codSigla.Equals("PLET_OXIM")) 
            output = "g_plet";
             else if (codSigla.Equals("EEG")) 
            output = "g_eeg";
              else if (codSigla.Equals("ECG")) 
            output = "g_ecg";
              else if (codSigla.Equals("EMG_QUEIXO")) 
            output = "g_queixo";
              else if (codSigla.Equals("OCULOGRAMA")) 
           output = "g_olhos"; 
              else if (codSigla.Equals("CAN_RONCO")) 
            output = "g_can_ronco";
              else if (codSigla.Equals("PTT")) 
            output = "g_PTT";

            return output;
        }
        public static float Ampli(string codTipo)
        {
            float output = 1;
            try
            {
                float g_amp_eeg = 1.4f;
                float g_amp_emg = 5.5f;
                float g_amp_ecg = 1f;
                float g_amp_ronco = 7f;
                float g_amp_fx = 20f;
                float g_amp_af = 45f;
                float g_amp_canulaFL = 10f;
                float g_amp_canulaRC = 2.2f;

                

                if (codTipo.Equals("g_eeg") || codTipo.Equals("g_olhos"))
                {
                    output = g_amp_eeg;
                }
                else if (codTipo.Equals("g_ecg"))
                    output = g_amp_ecg;
                else if (codTipo.Equals("g_perna") || codTipo.Equals("g_queixo"))
                    output = g_amp_emg;
                else if (codTipo.Equals("g_airflow"))
                    output = g_amp_af;
                else if (codTipo.Equals("g_abdom") || codTipo.Equals("g_torax"))
                    output = g_amp_fx;
                else if (codTipo.Equals("g_canula"))
                    output = g_amp_canulaFL;
                else if (codTipo.Equals("g_microf"))
                    output = g_amp_ronco;
                else if (codTipo.Equals("g_can_ronco"))
                    output = g_amp_canulaRC;
                else if (codTipo.Equals("g_plet"))
                    output = 12f;
                else if (codTipo.Equals("g_sao2_ser"))
                    output = output;

                
            }
            catch { }
            return output;
        }
    }
}

