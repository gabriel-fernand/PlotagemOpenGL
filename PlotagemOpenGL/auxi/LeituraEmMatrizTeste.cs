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
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;


namespace PlotagemOpenGL.auxi
{
    internal class LeituraEmMatrizTeste
    {
        public static void LeituraDat()
        {
            int index = 0;
            if (GlobVar.qtdCanais == null && GlobVar.tbl_CanaisAdquiridos != null)
            {
                index = GlobVar.tbl_CanaisAdquiridos.Rows.Count;
            }
            else
            {
                index = GlobVar.qtdCanais.Length;
            }

            int ntotal = 0;
            string[] dadoscanal = new string[47];
            byte[] WATec = new byte[19];
            byte[] buffer0;// = new byte[376];
            byte[] buffer1 = new byte[8];
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[47];
            GlobVar.nomeCanais = new string[index];
            GlobVar.txPorCanal = new int[index];
            GlobVar.ponteiroI = new int[index];
            GlobVar.ponteiroF = new int[index];
            GlobVar.scale = new double[index];
            GlobVar.codCanal = new int[index];
            GlobVar.grafSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];
            GlobVar.codSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];
            GlobVar.SomenteNums = new bool[2];
            int jj = 0;


            using (FileStream fs = new FileStream(GlobVar.textFile, FileMode.Open, FileAccess.Read))
            {
                fs.Read(WATec, 0, WATec.Length);

                string tipo = (Encoding.UTF8.GetString(WATec));
                // Remove nulos e espaços no fim para evitar pegar '\0'
                tipo = tipo.TrimEnd('\0', ' ', '\t', '\r', '\n');

                char o = tipo.Length > 0 ? tipo[tipo.Length - 1] : '\0';

                if (o == 'O') // use char, não string
                {
                    buffer0 = new byte[387];
                }
                else
                {
                    buffer0 = new byte[376];
                }

                fs.Read(buffer0, 0, buffer0.Length);

                
                GlobVar.cabecalho = (Encoding.UTF8.GetString(buffer0));

                fs.Read(buffer1, 0, buffer1.Length);

                string npag1 = (Encoding.UTF8.GetString(buffer1));

                var match = Regex.Match(npag1 ?? string.Empty, @"\d+");
                // Converte de forma segura (sem exception). Se não tiver número, fica 0 (ajuste se quiser outro padrão).
                GlobVar.npagin = int.TryParse(match.Value, out var numero) ? numero : 0;

                GlobVar.npag = npag1.Replace(" ", "");

                fs.Read(buffer2, 0, buffer2.Length);

                string tipocanais1 = (Encoding.UTF8.GetString(buffer2));

                GlobVar.tipocanais = tipocanais1.Replace(" ", "");

                match = Regex.Match(tipocanais1 ?? string.Empty, @"\d+");
                int ncanint = int.TryParse(match.Value, out numero) ? numero : 0; //Int32.Parse(tipocanais, System.Globalization.NumberStyles.HexNumber);
                GlobVar.qtdCanais = new string[ncanint];
                int txPorSeg = 0;
                for (int ich = 0; ich < ncanint; ich++)
                {
                    fs.Read(buffer3, 0, buffer3.Length);

                    dadoscanal[ich] = (Encoding.UTF8.GetString(buffer3));

                    string cod = dadoscanal[ich].Substring(0, 2);
                    GlobVar.qtdCanais[ich] = cod;
                    string phrase = dadoscanal[ich];
                    GlobVar.codCanal[ich] = Convert.ToInt16(phrase.Substring(0, 3)); //Faz a leitura do codigo do canal
                    GlobVar.nomeCanais[ich] = phrase.Substring(11, 10).Trim(); //Faz a leitura dos nomes de cada canal e armazena em um array
                    GlobVar.amos = Convert.ToInt32(phrase.Substring(8, 4));
                    string sizesample3 = phrase.Substring(8, 4);
                    string aux = sizesample3.Replace(" ", "");
                    int auxx = Convert.ToInt32(aux);
                    GlobVar.txPorCanal[ich] = auxx;

                    GlobVar.startpos = Convert.ToInt32(ntotal);
                    string sizesample1 = phrase.Substring(8, 4);
                    string banana = sizesample1.Replace(" ", "");

                    GlobVar.sizesample = (Convert.ToInt32(banana) * 2);
                    ntotal = ntotal + (GlobVar.amos * 2);

                    int ponteirostr = Convert.ToInt32(fs.Position);
                    GlobVar.ponteiroI[ich] = txPorSeg;
                    txPorSeg += auxx;
                    GlobVar.ponteiroF[ich] = txPorSeg;

                }

                GlobVar.matrizCompleta = new short[GlobVar.npagin, txPorSeg];

                GlobVar.size = Convert.ToInt32(GlobVar.npag);
                int recntotal = ntotal;

                fs.Position = fs.Position - 1;

                for (Int32 ich1 = 0;( ich1 < GlobVar.size && ich1 < GlobVar.matrizCompleta.GetLength(0)) && ich1 >= 0; ich1++)
                {
                    byte[] buffer4 = new byte[ntotal];
                    int lidos = fs.Read(buffer4, 0, buffer4.Length);
                    if (lidos != ntotal)
                    {
                        MessageBox.Show($"Erro ao ler o segundo {ich1}. Esperado {ntotal} bytes, lido {lidos}.");
                        break;
                    }

                    for (int i = 0, j = 0; i + 1 < buffer4.Length && j < GlobVar.matrizCompleta.GetLength(1); i += 2, j++)
                    {
                        try
                        {
                            GlobVar.matrizCompleta[ich1, j] = BitConverter.ToInt16(buffer4, i);
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show($"Erro de índice: ich1={ich1}, j={j}, matrizCompleta[{GlobVar.matrizCompleta.GetLength(0)}, {GlobVar.matrizCompleta.GetLength(1)}], buffer4.Length={buffer4.Length}, ntotal={ntotal}");
                            throw;
                        }
                    }
                }
                fs.Close();
                GlobVar.indiceDat = GlobVar.npagin * GlobVar.amos * 2;
                //int[] pontF = GlobVar.ponteiroF;
                GlobVar.matrizCanal = new short[GlobVar.tbl_MontagemSelecionada.Rows.Count, GlobVar.indiceDat];
                int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
                int segmentLength = GlobVar.matrizCanal.GetLength(1);
                LeituraBanco.AlteraMontagem(GlobVar.codMont);

                int linhaCanais = 0;
                int codMont = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);
                if(codMont == 181)
                {
                    foreach(DataRow rw in GlobVar.tbl_MontagemSelecionada.Rows)
                    {
                        int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(rw["CodCanal1"]));
                        int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(rw["CodCanal2"]));
                        if (canalIndex == -1 && (Convert.ToInt32(rw["CodCanal1"]) != 100 && Convert.ToInt32(rw["CodCanal1"]) != 101 && Convert.ToInt32(rw["CodCanal1"]) != 102)) return;

                        int ponteiroI = GlobVar.ponteiroI[canalIndex];
                        int ponteiroF = GlobVar.ponteiroF[canalIndex];


                        int colunaCanalIndex = 0;

                        if(canal2Index == -1)
                        {
                            int canal1Index = GlobVar.codCanal.IndexOf(19);
                            int canal2bIndex = GlobVar.codCanal.IndexOf(43);

                            int inicio_can1 = GlobVar.ponteiroI[canal1Index];
                            int Fim_can1 = GlobVar.ponteiroF[canal1Index];

                            int inicio_can2 = GlobVar.ponteiroI[canal2bIndex];
                            int Fim_can2 = GlobVar.ponteiroF[canal2bIndex];

                            if (canalIndex == 100)
                            {
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = inicio_can1;
                                    int colunaComp2 = inicio_can2;
                                    while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)((GlobVar.matrizCompleta[linha, colunaComp] + GlobVar.matrizCompleta[linha, colunaComp2])/ 2);
                                        colunaComp++;
                                        colunaComp2++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                            else if(canalIndex == 101)
                            {
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = inicio_can1;
                                    int colunaComp2 = inicio_can2;
                                    while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]) + GlobVar.matrizCompleta[linha, colunaComp]) / - 2);
                                        colunaComp++;
                                        colunaComp2++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                            else if(canalIndex == 102)
                            {
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = inicio_can1;
                                    int colunaComp2 = inicio_can2;
                                    while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]) - GlobVar.matrizCompleta[linha, colunaComp2]) / 2);
                                        colunaComp++;
                                        colunaComp2++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                            else
                            {
                                // Caso sem segundo canal
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = ponteiroI;
                                    while (colunaComp < ponteiroF)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                        colunaComp++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int inicio_can1 = GlobVar.ponteiroI[canalIndex];
                            int Fim_can1 = GlobVar.ponteiroF[canalIndex];

                            int inicio_can2 = GlobVar.ponteiroI[canal2Index];
                            int Fim_can2 = GlobVar.ponteiroF[canal2Index];
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = inicio_can1;
                                int colunaComp2 = inicio_can2;
                                while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]));
                                    colunaComp++;
                                    colunaComp2++;
                                    colunaCanalIndex++;
                                }
                            }

                        }
                        linhaCanais++;
                    }
                }
                else
                {
                    foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                    {
                        int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(row["CodCanal1"]));
                        int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(row["CodCanal2"]));
                        if (canalIndex == -1) return;

                        int ponteiroI = GlobVar.ponteiroI[canalIndex];
                        int ponteiroF = GlobVar.ponteiroF[canalIndex];
                        int colunaCanalIndex = 0;

                        if (canal2Index == -1)
                        {
                            // Caso sem segundo canal
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = ponteiroI;
                                while (colunaComp < ponteiroF)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    colunaComp++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        else
                        {
                            // Caso com segundo canal
                            int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = ponteiroI;
                                int colunaCan2 = ponteiroI2;
                                while (colunaComp < ponteiroF)
                                {
                                    // Calcular a diferença entre valores das colunas de canais
                                    short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                                    // Atribuir a diferença para a matriz de destino
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);

                                    colunaComp++;
                                    colunaCan2++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        linhaCanais++;
                    }
                }
                    reorganize();
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    if (Convert.ToInt32(row["CodTipoCanal"]) == 15 || Convert.ToInt32(row["CodTipoCanal"]) == 28 || Convert.ToInt32(row["CodTipoCanal"]) == 29 || Convert.ToInt32(row["CodTipoCanal"]) == 38)
                    {
                        if (Convert.ToInt32(row["CodCanal1"]) != 65)
                        {
                            int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                            int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                            if (canalIndex == -1) return;

                            int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                            if (selectedIndex == -1) return;

                            var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                            int txPorCanal = GlobVar.txPorCanal[canalIndex];
                            var dataToFilter = canalData;

                            int lmAnaloInf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 38 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteInf_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Anal"]);
                            int lmAnaloSup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 38 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteSup_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Anal"]);

                            int lminf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 38 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteInf_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Valor"]);
                            int lmsup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 38 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteSup_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Valor"]);

                            dataToFilter = DigiToAnalo(canalData, lminf, lmsup, lmAnaloInf, lmAnaloSup, txPorCanal, Convert.ToInt32(row["CodTipoCanal"]));

                            Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                            GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                        }
                    }
                }
                foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
                {
                    if (row["CodCanal2"] == DBNull.Value)
                    {
                        row["CodCanal2"] = -1;
                    }
                }
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {

                    int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                    int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                    if (canalIndex == -1) return;

                    int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                    if (selectedIndex == -1) return;

                    double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                    double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                    double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {
                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];
                        var dataToFilter = canalData;

                        if (lowHertz.HasValue && lowHertz.Value != 0)
                        {
                            if (highHertz.HasValue && highHertz.Value != 0)
                            {
                                dataToFilter = ShortToFloat(
                                    BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                                );
                            }
                            else
                            {
                                dataToFilter = ShortToFloat(
                                    PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                                );
                            }
                        }
                        else if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                            );
                        }

                        if (notchHertz.HasValue && notchHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                            );
                        }

                        Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                }

                for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    float scala = (float)( Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]) / Ampli(CodTipo(i))) ;
                    GlobVar.scale[i] = scala;
                }
                
            }
        }
        public static short[] DigiToAnalo(short[] toUp, int lmInf, int lmSup, int lmAnaloInf, int lmAnaloSup, int taxa, int codTip)
        {
            // Verifica se os limites analógicos são iguais para evitar divisão por zero
            if (lmAnaloInf == lmAnaloSup)
            {
                throw new ArgumentException("Erro: lmAnaloInf e lmAnaloSup não podem ser iguais, pois isso resultaria em uma divisão por zero.");
            }
            int length = toUp.Length / taxa;
            double[] medias = new double[length];
            short[] aoba = new short[toUp.Length];
            int pont = 0;
            for (int i = 0; i < length; i++)
            {
                double med = 0;
                for (int j = pont; j < pont + taxa && j < toUp.Length; j += taxa) // Garante que j não ultrapasse o tamanho do vetor
                {
                    med += toUp[j];
                    
                }
                //med /= taxa;

                med *= -1;
                // Normaliza dentro dos limites fornecidos
                double value = lmInf + Math.Abs(med - lmAnaloInf) / Math.Abs((lmAnaloSup - lmAnaloInf) / (lmSup - lmInf));
                medias[i] = Math.Round(value * 100);
                pont += taxa;
            }

            // Preenchendo `aoba` corretamente sem pular índices
            int index = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < taxa && index < aoba.Length; j++)
                {
                    if(codTip != 15)
                    {
                        aoba[index++] = (short)((short)medias[i] / 100);
                    }
                    else
                    {
                        aoba[index++] = (short)medias[i];
                    }
                }
            }/*
            for (int i = 0; i < aoba.Length; i ++)
            {
                // Correção: Pegando o valor correto de toUp[i]
                toUp[i] *= -1;
                double y;//= ((toUp[i] - lmAnaloInf) * (lmSup - lmInf) / (double)(lmAnaloSup - lmAnaloInf)) + lmInf;
                y = lmInf + Math.Abs(toUp[i] - lmAnaloInf) / Math.Abs((lmAnaloSup - lmAnaloInf) / (lmSup - lmInf));

                //y = lmInf + Math.Abs((toUp[i] - lmAnaloInf)*(lmSup - lmInf)) / Math.Abs(lmAnaloSup - lmAnaloInf);

                // Garantindo que o valor convertido caiba em um short
                y = Math.Max(short.MinValue, Math.Min(short.MaxValue, y));

                aoba[i] = (short)y;// (short)Math.Round( y , 4);
            }
            */
            return aoba;
        }
        public static void montagemSelecionadaAlterada()
        {
            int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
            int startLength = GlobVar.indice;
            int segmentLength = 512 * 600; // GlobVar.matrizCanal.GetLength(1);
            int ln = startLength / GlobVar.namos;
            GlobVar.areaCarregadaAltMont = startLength + segmentLength;


            Tela_Plotagem.cronometro1.Reset();
            Tela_Plotagem.cronometro1.Start();

            int linhaCanais = 0;
            int codMont = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);
            if (codMont == 181)
            {
                foreach (DataRow rw in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(rw["CodCanal1"]));
                    int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(rw["CodCanal2"]));
                    if (canalIndex == -1 && (Convert.ToInt32(rw["CodCanal1"]) != 100 && Convert.ToInt32(rw["CodCanal1"]) != 101 && Convert.ToInt32(rw["CodCanal1"]) != 102)) return;

                    int ponteiroI = 0;
                    int ponteiroF = 0;

                    if (canalIndex != -1){
                        ponteiroI = GlobVar.ponteiroI[canalIndex];
                        ponteiroF = GlobVar.ponteiroF[canalIndex];
                    }

                    int colunaCanalIndex = 0;

                    if (canal2Index == -1)
                    {
                        int canal1Index = GlobVar.codCanal.IndexOf(19);
                        int canal2bIndex = GlobVar.codCanal.IndexOf(43);

                        int inicio_can1 = GlobVar.ponteiroI[canal1Index];
                        int Fim_can1 = GlobVar.ponteiroF[canal1Index];

                        int inicio_can2 = 0;
                        int Fim_can2 = 0;
                        if (canal2bIndex != -1){
                            inicio_can2 = GlobVar.ponteiroI[canal2bIndex];
                            Fim_can2 = GlobVar.ponteiroF[canal2bIndex];
                        }
                        if (Convert.ToInt32(rw["CodCanal1"]) == 100)
                        {
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = inicio_can1;
                                int colunaComp2 = inicio_can2;
                                while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)((GlobVar.matrizCompleta[linha, colunaComp] + GlobVar.matrizCompleta[linha, colunaComp2]) / 2);
                                    colunaComp++;
                                    colunaComp2++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        else if (Convert.ToInt32(rw["CodCanal1"]) == 101)
                        {
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = inicio_can1;
                                int colunaComp2 = inicio_can2;
                                while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]) + GlobVar.matrizCompleta[linha, colunaComp]) / -2);
                                    colunaComp++;
                                    colunaComp2++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        else if (Convert.ToInt32(rw["CodCanal1"]) == 102)
                        {
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = inicio_can1;
                                int colunaComp2 = inicio_can2;
                                while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]) - GlobVar.matrizCompleta[linha, colunaComp2]) / 2);
                                    colunaComp++;
                                    colunaComp2++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        else
                        {
                            // Caso sem segundo canal
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = ponteiroI;
                                while (colunaComp < ponteiroF)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    colunaComp++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                    }
                    else
                    {
                        int inicio_can1 = GlobVar.ponteiroI[canalIndex];
                        int Fim_can1 = GlobVar.ponteiroF[canalIndex];

                        int inicio_can2 = GlobVar.ponteiroI[canal2Index];
                        int Fim_can2 = GlobVar.ponteiroF[canal2Index];
                        for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = inicio_can1;
                            int colunaComp2 = inicio_can2;
                            while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                            {
                                GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]));
                                colunaComp++;
                                colunaComp2++;
                                colunaCanalIndex++;
                            }
                        }

                    }
                    linhaCanais++;
                }
            }
            else{
                // Paralelizar a cópia de dados para GlobVar.matrizCanal
                Parallel.For(0, rowCount, linhaCanais =>
                {
                    int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]));
                    int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal2"]));
                    if (canalIndex == -1) return;

                    int ponteiroI = GlobVar.ponteiroI[canalIndex];
                    int ponteiroF = GlobVar.ponteiroF[canalIndex];

                    int txPorCanal = GlobVar.txPorCanal[canalIndex];
                    segmentLength = txPorCanal * 600;

                    int Start = ((startLength / GlobVar.namos)) * (int)txPorCanal;
                    int colunaCanalIndex = Start;


                    if (canal2Index == -1)
                    {
                        // Caso sem segundo canal
                        for (int linha = ln; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroI;
                            while (colunaComp < ponteiroF)
                            {
                                GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                colunaComp++;
                                colunaCanalIndex++;
                                if (colunaCanalIndex > startLength + segmentLength) break;
                            }
                        }
                    }
                    else
                    {
                        // Caso com segundo canal
                        int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                        for (int linha = ln; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroI;
                            int colunaCan2 = ponteiroI2;

                            while (colunaComp < ponteiroF)
                            {
                                // Calcular a diferença entre valores das colunas de canais
                                short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                                // Atribuir a diferença para a matriz de destino
                                GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);
                                //DoEvents em vb
                                colunaComp++;
                                colunaCan2++;
                                colunaCanalIndex++;
                                if (colunaCanalIndex > startLength + segmentLength) break;

                            }
                        }
                    }
                });
            }
            Tela_Plotagem.cronometro1.Stop();


            reorganize();

            Tela_Plotagem.cronometro3.Reset();
            Tela_Plotagem.cronometro3.Start();

            // Atualizar CodCanal2 em paralelo
            Parallel.ForEach(GlobVar.tbl_MontagemSelecionada.AsEnumerable(), row =>
            {
                if (row["CodCanal2"] == DBNull.Value)
                {
                    row["CodCanal2"] = -1;
                }
            });


            Tela_Plotagem.cronometroBand.Reset();
            Tela_Plotagem.cronometroBaixa.Reset();
            Tela_Plotagem.cronometroAlta.Reset();
            Tela_Plotagem.cronometroNotch.Reset();
            int ind = 0;
            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                if (Convert.ToInt32(row["CodTipoCanal"]) == 15 || Convert.ToInt32(row["CodTipoCanal"]) == 28 || Convert.ToInt32(row["CodTipoCanal"]) == 29)
                {
                    if (Convert.ToInt32(row["CodCanal1"]) != 65)
                    {
                        int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                        int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                        if (canalIndex == -1) return;

                        int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                        if (selectedIndex == -1) return;

                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];
                        var dataToFilter = canalData;

                        int lmAnaloInf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Anal"]);
                        int lmAnaloSup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Anal"]);

                        int lminf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Valor"]); ;
                        int lmsup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Valor"]); ;

                        dataToFilter = DigiToAnalo(canalData, lminf, lmsup, lmAnaloInf, lmAnaloSup, txPorCanal, Convert.ToInt32(row["CodTipoCanal"]));

                        Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                }
            }

            // Aplicar filtros paralelamente
            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);

                if (canalIndex == -1) return;

                int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                if (selectedIndex == -1) return;

                double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                {


                    var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                    int txPorCanal = GlobVar.txPorCanal[canalIndex];
                    segmentLength = txPorCanal * 600;

                    int skipLength = ((startLength / GlobVar.namos)) * (int)txPorCanal;
                    int startFiltLength = ((startLength / GlobVar.namos)) * (int)txPorCanal;

                    int endIndex = Math.Min(segmentLength, canalData.Length);
                    var dataToFilter = canalData.Skip(skipLength).Take(endIndex).ToArray();

                    if (lowHertz.HasValue && lowHertz.Value != 0)
                    {
                        if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                            );
                        }
                        else
                        {
                            dataToFilter = ShortToFloat(
                                PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                            );
                        }
                    }
                    else if (highHertz.HasValue && highHertz.Value != 0)
                    {
                        dataToFilter = ShortToFloat(
                            PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                        );
                    }

                    if (notchHertz.HasValue && notchHertz.Value != 0)
                    {
                        dataToFilter = ShortToFloat(
                            Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                        );
                    }

                    // Atualizando apenas a parte específica em canalData
                    Array.Copy(dataToFilter, 0, canalData, startFiltLength, dataToFilter.Length);

                    // Agora, usamos SetRow para definir a linha inteira novamente
                    GlobVar.matrizCanal.SetRow(selectedIndex, canalData);

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {
                        Tela_Plotagem.UpdateBeforeLoad(ind + 1, lowHertz ?? 0, highHertz ?? 0, notchHertz ?? 0);
                    }
                }
            }

            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                if (Convert.ToInt32(row["CodTipoCanal"]) == 15 || Convert.ToInt32(row["CodTipoCanal"]) == 28 || Convert.ToInt32(row["CodTipoCanal"]) == 29)
                {
                    if (Convert.ToInt32(row["CodCanal1"]) != 65)
                    {
                        int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                        int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                        if (canalIndex == -1) return;

                        int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                        if (selectedIndex == -1) return;

                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];
                        var dataToFilter = canalData;

                        int lmAnaloInf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Anal"]);
                        int lmAnaloSup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Anal"]);

                        int lminf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Valor"]); ;
                        int lmsup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Valor"]); ;

                        dataToFilter = DigiToAnalo(canalData, lminf, lmsup, lmAnaloInf, lmAnaloSup, txPorCanal, Convert.ToInt32(row["CodTipoCanal"]));

                        Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                }
            }
            Tela_Plotagem.cronometro3.Stop();

            // Paralelizar a atualização do array GlobVar.scale
            GlobVar.scale = new double[rowCount];
            Parallel.For(0, rowCount, i =>
            {
                float scala = (float)(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]) / Ampli(CodTipo(i)));
                GlobVar.scale[i] = scala;
            });
        }

        public static async void montagemSelecionadaAlteradaTudo(CancellationToken token = default)
        {
            try {
                Tela_Plotagem.conc = false;

                int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
                int segmentLength = GlobVar.matrizCanal.GetLength(1);

                Tela_Plotagem.cronometro1.Reset();
                Tela_Plotagem.cronometro1.Start();
                GlobVar.LastRowLoaded = 0;
                GlobVar.MatrizCompleta = false;
                GlobVar.FiltroCompleto = false;

                int linhaCanais = 0;
                int codMont = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);
                if (codMont == 181)
                {
                    foreach (DataRow rw in GlobVar.tbl_MontagemSelecionada.Rows)
                    {
                        int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(rw["CodCanal1"]));
                        int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(rw["CodCanal2"]));
                        if (canalIndex == -1 && (Convert.ToInt32(rw["CodCanal1"]) != 100 && Convert.ToInt32(rw["CodCanal1"]) != 101 && Convert.ToInt32(rw["CodCanal1"]) != 102)) return;

                        int ponteiroI = GlobVar.ponteiroI[canalIndex];
                        int ponteiroF = GlobVar.ponteiroF[canalIndex];


                        int colunaCanalIndex = 0;

                        if (canal2Index == -1)
                        {
                            int canal1Index = GlobVar.codCanal.IndexOf(19);
                            int canal2bIndex = GlobVar.codCanal.IndexOf(43);

                            int inicio_can1 = GlobVar.ponteiroI[canal1Index];
                            int Fim_can1 = GlobVar.ponteiroF[canal1Index];

                            int inicio_can2 = GlobVar.ponteiroI[canal2bIndex];
                            int Fim_can2 = GlobVar.ponteiroF[canal2bIndex];

                            if (Convert.ToInt32(rw["CodCanal1"]) == 100)
                            {
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = inicio_can1;
                                    int colunaComp2 = inicio_can2;
                                    while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)((GlobVar.matrizCompleta[linha, colunaComp] + GlobVar.matrizCompleta[linha, colunaComp2]) / 2);
                                        colunaComp++;
                                        colunaComp2++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                            else if (Convert.ToInt32(rw["CodCanal1"]) == 101)
                            {
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = inicio_can1;
                                    int colunaComp2 = inicio_can2;
                                    while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]) + GlobVar.matrizCompleta[linha, colunaComp]) / -2);
                                        colunaComp++;
                                        colunaComp2++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                            else if (Convert.ToInt32(rw["CodCanal1"]) == 102)
                            {
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = inicio_can1;
                                    int colunaComp2 = inicio_can2;
                                    while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]) - GlobVar.matrizCompleta[linha, colunaComp2]) / 2);
                                        colunaComp++;
                                        colunaComp2++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                            else
                            {
                                // Caso sem segundo canal
                                for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                                {
                                    int colunaComp = ponteiroI;
                                    while (colunaComp < ponteiroF)
                                    {
                                        GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                        colunaComp++;
                                        colunaCanalIndex++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            int inicio_can1 = GlobVar.ponteiroI[canalIndex];
                            int Fim_can1 = GlobVar.ponteiroF[canalIndex];

                            int inicio_can2 = GlobVar.ponteiroI[canal2Index];
                            int Fim_can2 = GlobVar.ponteiroF[canal2Index];
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                int colunaComp = inicio_can1;
                                int colunaComp2 = inicio_can2;
                                while (colunaComp < Fim_can1 && colunaComp2 < Fim_can2)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)((GlobVar.matrizCompleta[linha, colunaComp] - GlobVar.matrizCompleta[linha, colunaComp2]));
                                    colunaComp++;
                                    colunaComp2++;
                                    colunaCanalIndex++;
                                }
                            }

                        }
                        linhaCanais++;
                    }
                }
                else{
                    // Paralelizar a cópia de dados para GlobVar.matrizCanal
                    Parallel.For(0, rowCount, (linhaCanais, state) =>
                    {
                        _pauseEvent.Wait(token);

                        // Verificar o token no início do loop paralelo
                        if (token.IsCancellationRequested)
                        {
                            state.Stop();
                            return;
                        }

                        int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]));
                        int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal2"]));
                        if (canalIndex == -1) return;

                        int ponteiroI = GlobVar.ponteiroI[canalIndex];
                        int ponteiroF = GlobVar.ponteiroF[canalIndex];
                        int colunaCanalIndex = 0;

                        if (canal2Index == -1)
                        {
                            // Caso sem segundo canal
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                // Verificar o token dentro do loop de processamento mais intenso
                                if (token.IsCancellationRequested)
                                    return;

                                int colunaComp = ponteiroI;
                                while (colunaComp < ponteiroF)
                                {
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    colunaComp++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        else
                        {
                            // Caso com segundo canal
                            int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                            for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                            {
                                // Verificar o token dentro do loop de processamento mais intenso
                                if (token.IsCancellationRequested)
                                    return;

                                int colunaComp = ponteiroI;
                                int colunaCan2 = ponteiroI2;
                                while (colunaComp < ponteiroF)
                                {
                                    // Calcular a diferença entre valores das colunas de canais
                                    short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                    short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                                    // Atribuir a diferença para a matriz de destino
                                    GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);

                                    colunaComp++;
                                    colunaCan2++;
                                    colunaCanalIndex++;
                                }
                            }
                        }
                        GlobVar.LastRowLoaded++;

                    });
                }
                GlobVar.MatrizCompleta = true;
            
                // Mais uma verificação de cancelamento após a cópia
                if (token.IsCancellationRequested)
                    return;

                Tela_Plotagem.cronometro1.Stop();
                reorganize();

                _pauseEvent.Wait(token);
                if (token.IsCancellationRequested)
                    return;
                GlobVar.LastRowLoaded = 0;
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    if (Convert.ToInt32(row["CodTipoCanal"]) == 15 || Convert.ToInt32(row["CodTipoCanal"]) == 28 || Convert.ToInt32(row["CodTipoCanal"]) == 29)
                    {
                        if (Convert.ToInt32(row["CodCanal1"]) != 65)
                        {
                            int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                            int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                            if (canalIndex == -1) return;

                            int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                            if (selectedIndex == -1) return;

                            var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                            int txPorCanal = GlobVar.txPorCanal[canalIndex];
                            var dataToFilter = canalData;

                            int lmAnaloInf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Anal"]);
                            int lmAnaloSup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Anal"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Anal"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Anal"]);

                            int lminf = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteInf_Valor"]); ;
                            int lmsup = Convert.ToInt32(row["CodTipoCanal"]) == 15 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Valor"]) : Convert.ToInt32(row["CodTipoCanal"]) == 28 ? Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Valor"]) : Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Volume_LimiteSup_Valor"]); ;

                            dataToFilter = DigiToAnalo(canalData, lminf, lmsup, lmAnaloInf, lmAnaloSup, txPorCanal, Convert.ToInt32(row["CodTipoCanal"]));

                            Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                            GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                        }
                    }
                }

                // Aplicar filtros paralelamente
                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    _pauseEvent.Wait(token);

                    if (token.IsCancellationRequested)
                        return;

                    int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                    int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);
                    if (canalIndex == -1) return;

                    int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                    if (selectedIndex == -1) return;

                    double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                    double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                    double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {
                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];
                        var dataToFilter = canalData;

                        if (lowHertz.HasValue && lowHertz.Value != 0)
                        {
                            if (highHertz.HasValue && highHertz.Value != 0)
                            {
                                dataToFilter = ShortToFloat(
                                    BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                                );
                            }
                            else
                            {
                                dataToFilter = ShortToFloat(
                                    PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                                );
                            }
                        }
                        else if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                            );
                        }

                        if (notchHertz.HasValue && notchHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                            );
                        }

                        Array.Copy(dataToFilter, 0, canalData, 0, dataToFilter.Length);
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                        GlobVar.LastRowLoaded++;

                }

                GlobVar.areaCarregadaAltMont = GlobVar.matrizCanal.GetLength(1);
                    GlobVar.FiltroCompleto = true;
                    Tela_Plotagem.conc = true;

                    //Tela_Plotagem.TelaClearAndReload();
                }
            catch { return; }
        }

        // Controlador para pausar e retomar o loop
        public static ManualResetEventSlim _pauseEvent = new ManualResetEventSlim(true);
        public static void Pause() => _pauseEvent.Reset();
        public static void Resume() => _pauseEvent.Set();


        public static async Task CarregamentoMontagemRapido(int iniFim, int areaCarregar)
        {

            int rowCount = GlobVar.tbl_MontagemSelecionada.Rows.Count;
            int startLength = iniFim == 3 ? GlobVar.indice - (512 * 30) : GlobVar.indice;
            int segmentLength = iniFim == 0 ? 512 * 300 : iniFim == 1 ? GlobVar.matrizCanal.GetLength(1) - (512 * 300) : iniFim == 2 ? areaCarregar - GlobVar.indice : 512 * 300; // GlobVar.matrizCanal.GetLength(1);
            int ln = startLength / GlobVar.namos;
            GlobVar.areaCarregadaAltMont = startLength + segmentLength;


            if (!GlobVar.MatrizCompleta)
            {
                // Paralelizar a cópia de dados para GlobVar.matrizCanal
                Parallel.For(GlobVar.LastRowLoaded, rowCount, linhaCanais =>
                {
                    int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal1"]));
                    int canal2Index = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[linhaCanais]["CodCanal2"]));
                    if (canalIndex == -1) return;

                    int ponteiroI = GlobVar.ponteiroI[canalIndex];
                    int ponteiroF = GlobVar.ponteiroF[canalIndex];

                    int txPorCanal = GlobVar.txPorCanal[canalIndex];

                    int Start = ((startLength / GlobVar.namos)) * (int)txPorCanal;
                    int colunaCanalIndex = Start;


                    if (canal2Index == -1)
                    {
                        // Caso sem segundo canal
                        for (int linha = ln; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroI;
                            while (colunaComp < ponteiroF)
                            {
                                GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                colunaComp++;
                                colunaCanalIndex++;
                                if (colunaCanalIndex > startLength + segmentLength) break;
                            }
                        }
                    }
                    else
                    {
                        // Caso com segundo canal
                        int ponteiroI2 = GlobVar.ponteiroI[canal2Index];
                        for (int linha = ln; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroI;
                            int colunaCan2 = ponteiroI2;

                            while (colunaComp < ponteiroF)
                            {
                                // Calcular a diferença entre valores das colunas de canais
                                short valorColunaComp = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                short valorColunaCan2 = (short)GlobVar.matrizCompleta[linha, colunaCan2];

                                // Atribuir a diferença para a matriz de destino
                                GlobVar.matrizCanal[linhaCanais, colunaCanalIndex] = (short)(valorColunaComp - valorColunaCan2);
                                //DoEvents em vb
                                colunaComp++;
                                colunaCan2++;
                                colunaCanalIndex++;
                                if (colunaCanalIndex > startLength + segmentLength) break;

                            }
                        }
                    }
                });
                GlobVar.LastRowLoaded = 0;
            }
            reorganize();
            if(!GlobVar.FiltroCompleto)
            {
                // Aplicar filtros paralelamente
                for (int i = GlobVar.LastRowLoaded; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    DataRow row = GlobVar.tbl_MontagemSelecionada.Rows[i];
                    int codCanal1 = Convert.ToInt32(row["CodCanal1"]);
                    int canalIndex = GlobVar.codCanal.IndexOf(codCanal1);

                    if (canalIndex == -1) return;

                    int selectedIndex = GlobVar.codSelected.IndexOf(codCanal1);
                    if (selectedIndex == -1) return;

                    double? lowHertz = row.IsNull("PassaBaixa") ? (double?)null : row.Field<double>("PassaBaixa");
                    double? highHertz = row.IsNull("PassaAlta") ? (double?)null : row.Field<double>("PassaAlta");
                    double? notchHertz = row.IsNull("Notch") ? (double?)null : row.Field<double>("Notch");

                    if (lowHertz.HasValue || highHertz.HasValue || notchHertz.HasValue)
                    {


                        var canalData = GlobVar.matrizCanal.GetRow(selectedIndex);
                        int txPorCanal = GlobVar.txPorCanal[canalIndex];

                        int skipLength = iniFim == 0 ? 0 : ((startLength / GlobVar.namos)) * (int)txPorCanal;
                        int startFiltLength = iniFim == 0 ? 0 : ((startLength / GlobVar.namos)) * (int)txPorCanal;

                        skipLength = skipLength < 0 ? 0 : skipLength;
                        startFiltLength = startFiltLength < 0 ? 0 : startFiltLength;

                        int endIndex = Math.Min(segmentLength, canalData.Length);
                        var dataToFilter = canalData.Skip(skipLength).Take(Math.Abs(endIndex)).ToArray();

                        if (lowHertz.HasValue && lowHertz.Value != 0)
                        {
                            if (highHertz.HasValue && highHertz.Value != 0)
                            {
                                dataToFilter = ShortToFloat(
                                    BandPass.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, (float)highHertz.Value, txPorCanal)
                                );
                            }
                            else
                            {
                                dataToFilter = ShortToFloat(
                                    PaissaBaixa.ApplyFilter(FloatToShort(dataToFilter), (float)lowHertz.Value, txPorCanal)
                                );
                            }
                        }
                        else if (highHertz.HasValue && highHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                PaissaAlta.ApplyFilter(FloatToShort(dataToFilter), (float)highHertz.Value, txPorCanal)
                            );
                        }

                        if (notchHertz.HasValue && notchHertz.Value != 0)
                        {
                            dataToFilter = ShortToFloat(
                                Notch.ApplyFilter(FloatToShort(dataToFilter), (float)notchHertz.Value, 10, txPorCanal)
                            );
                        }

                        // Atualizando apenas a parte específica em canalData
                        Array.Copy(dataToFilter, 0, canalData, startFiltLength, dataToFilter.Length);

                        // Agora, usamos SetRow para definir a linha inteira novamente
                        GlobVar.matrizCanal.SetRow(selectedIndex, canalData);
                    }
                }
            }
        }

        public static short[] SetReferencia(int principal, int referencia)
        {
            int numColunas = GlobVar.matrizCanal.GetLength(1);
            short[] novoArray = new short[numColunas];
            short[] sla = Referencia(referencia);

            // Se o primeiro elemento for zero, retorna o novoArray vazio
            if (sla[0] == 0)
            {
                return novoArray;
            }

            // Obtem o índice do canal principal uma vez, em vez de chamá-lo repetidamente dentro do loop
            int indexPrincipal = GlobVar.codSelected.IndexOf(principal);

            // Calcula o novoArray com a referência
            for (int i = 0; i < numColunas && i < novoArray.Length; i++)
            {
                novoArray[i] = (short)(GlobVar.matrizCanal[indexPrincipal, i] - sla[i]);
            }

            return novoArray;
        }

        public static short[] Referencia(int codReferencia)
        {
            int numColunas = GlobVar.matrizCanal.GetLength(1);
            short[] referencia = new short[numColunas];

            // Verifica se o código de referência existe
            int indexCodReferencia = GlobVar.codCanal.IndexOf(codReferencia);
            if (indexCodReferencia == -1)
            {
                return referencia;
            }

            // Pega os índices de início e fim uma vez
            int startCol = GlobVar.ponteiroI[indexCodReferencia];
            int endCol = GlobVar.ponteiroF[indexCodReferencia];

            // Copia os valores de matrizCompleta para o array referencia
            int pontRef = 0;
            for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0) && pontRef < numColunas; linhaComp++)
            {
                for (int colunaComp = startCol; colunaComp < endCol && pontRef < numColunas; colunaComp++)
                {
                    referencia[pontRef++] = (short)GlobVar.matrizCompleta[linhaComp, colunaComp];
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
            GlobVar.grafSelected.Clear();
            GlobVar.codSelected.Clear();

            GlobVar.grafSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];
            GlobVar.codSelected = new int[GlobVar.tbl_MontagemSelecionada.Rows.Count];

            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                GlobVar.grafSelected[i] = i;
                GlobVar.codSelected[i] = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["CodCanal1"]);
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
        public static float[] IntToFloat(int[] input)
        {
            float[] output = new float[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (short)input[i];
            }

            return output;

        }
        public static int[] FloatToInt(float[] input)
        {
            int[] output = new int[input.Length];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (int)input[i];
            }

            return output;

        }

        public static string CodTipo(int Index)
        {
            string output = "";
            int codTipo = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[Index]["CodTipoCanal"]);
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

        public static void referencias()
        {
            // Usa um HashSet para garantir que os valores sejam únicos
            HashSet<int> referenciasUnicas = new HashSet<int>();

            // Itera pelas linhas do DataTable
            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                // Converte o valor da coluna "CodCanal2" para int
                int valor = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["CodCanal2"]);

                // Apenas adiciona valores que não sejam -1
                if (valor != -1)
                {
                    referenciasUnicas.Add(valor);
                }
            }

            // Converte o HashSet para um array e armazena em GlobVar.canaisReferencia
            GlobVar.canaisReferencia = referenciasUnicas.ToArray();
            GlobVar.nomeReferencia = new string[GlobVar.canaisReferencia.Length];

            for(int i = 0; i < GlobVar.nomeReferencia.Length ; i++)
            {
                var row = GlobVar.tbl_CadCanal.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodCanal") == GlobVar.canaisReferencia[i]);
                GlobVar.nomeReferencia[i] = row["NomeCanal"].ToString();
            }

        }
    }
}

