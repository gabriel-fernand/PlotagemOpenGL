using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
//using Accord.Math;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Threading;
//using CenterSpace.NMath.Core;
using System.Reflection;

namespace PlotagemOpenGL.auxi
{
    internal class Leitura
    {
        public static void LeituraDat()
        {
            int ntotal = 0;
            int numeroAVG = 8;
            double[] valor = new double[] { };
            string[] dadoscanal = new string[47];
            byte[] buffer0 = new byte[395];
            byte[] buffer1 = new byte[8];
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[47];

            try
            {
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

                    for (int ich = 0; ich < ncanint; ich++)
                    {
                        fs.Read(buffer3, 0, buffer3.Length);

                        dadoscanal[ich] = (Encoding.UTF8.GetString(buffer3));

                        string cod = dadoscanal[ich].Substring(0, 2);

                        string phrase = dadoscanal[ich];

                        GlobVar.amos = Convert.ToInt16(phrase.Substring(8, 4));



                        if (cod.Equals(GlobVar.COD))
                        {
                            //string[] hexValuesSplit = dadoscanal[ich].Split(' ');

                            GlobVar.startpos = Convert.ToInt16(ntotal);
                            string sizesample1 = phrase.Substring(8, 4);
                            string banana = sizesample1.Replace(" ", "");

                            GlobVar.sizesample = (Convert.ToInt16(banana) * 2);

                        }
                        ntotal = ntotal + (GlobVar.amos * 2);

                        int ponteirostr = Convert.ToInt16(fs.Position);
                    }

                    GlobVar.size = Convert.ToInt32(GlobVar.npag);

                    int recntotal = ntotal;

                    fs.Position = fs.Position - 1;

                    GlobVar.indiceDat = GlobVar.npagin * GlobVar.amos * 2;

                    valor = new double[GlobVar.indiceDat];

                    GlobVar.valorout = new Double[GlobVar.indiceDat];

                    GlobVar.avg1 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg2 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg3 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg4 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg5 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg6 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg7 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.avg8 = new Double[GlobVar.amos / numeroAVG];

                    GlobVar.mean = new Double[GlobVar.npagin * numeroAVG];



                    int amostraMin = GlobVar.amos / GlobVar.numeroAmos;

                    GlobVar.indiceAmostra1 = 0;
                    GlobVar.indiceAmostra2 = amostraMin * 1;
                    GlobVar.indiceAmostra3 = amostraMin * 2;
                    GlobVar.indiceAmostra4 = amostraMin * 3;
                    GlobVar.indiceAmostra5 = amostraMin * 4;
                    GlobVar.indiceAmostra6 = amostraMin * 5;
                    GlobVar.indiceAmostra7 = amostraMin * 6;
                    GlobVar.indiceAmostra8 = amostraMin * 7;
                    GlobVar.indiceAmostra9 = amostraMin * 8;

                    GlobVar.lastcall = 0;

                    int i2r2 = 0;

                    GlobVar.metadeavg = GlobVar.sizesample / 16;



                    for (int ich1 = 0; ich1 < GlobVar.size; ich1++) //leitura de 1 segundo
                    {

                        byte[] buffer4 = new byte[ntotal];

                        fs.Read(buffer4, 0, buffer4.Length);

                        Int16[] bitstring = new Int16[GlobVar.sizesample / 2];

                        var limite = GlobVar.startpos + GlobVar.sizesample;

                        for (int i = GlobVar.startpos, j = 0; i < limite; i += 2, j++)
                        {
                            bitstring[j] = BitConverter.ToInt16(buffer4, i);

                            //this.chart1.Series["Series1"].Points.AddXY(i, bitstring[j]);
                            //uint result2 = BitConverter.ToUInt16(buffer4, i);
                            //this.chart1.Series["Series1"].Points.AddXY(bitstring[j], j);
                        }
                        for (int i2r = 0, i2r1 = 0; i2r < GlobVar.sizesample / 2; i2r++, i2r1++)
                        {
                            //this.chart2.Series["Series1"].Points.AddXY(sizesample, valorout[lastcall]);
                            //valor[lastcall] = Convert.ToDouble(bitstring[i2r]);
                            GlobVar.valorout[GlobVar.lastcall] = Convert.ToDouble(bitstring[i2r]);

                            GlobVar.lisup = GlobVar.amos - 1;


                            GlobVar.lastcall++;


                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra1) < GlobVar.indiceAmostra2; i++)
                        {
                            GlobVar.avg1[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra1]);
                            //indiceAmostra1++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra2) < GlobVar.indiceAmostra3; i++)
                        {
                            GlobVar.avg2[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra2]);
                            //indiceAmostra2++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra3) < GlobVar.indiceAmostra4; i++)
                        {
                            GlobVar.avg3[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra3]);
                            //indiceAmostra3++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra4) < GlobVar.indiceAmostra5; i++)
                        {
                            GlobVar.avg4[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra4]);
                            //indiceAmostra4++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra5) < GlobVar.indiceAmostra6; i++)
                        {
                            GlobVar.avg5[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra5]);
                            //indiceAmostra5++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra6) < GlobVar.indiceAmostra7; i++)
                        {
                            GlobVar.avg6[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra6]);
                            //indiceAmostra6++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra7) < GlobVar.indiceAmostra8; i++)
                        {
                            GlobVar.avg7[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra7]);
                            //indiceAmostra7++;

                        }

                        for (int i = 0; (i + GlobVar.indiceAmostra8) < GlobVar.indiceAmostra9; i++)
                        {
                            GlobVar.avg8[i] = Convert.ToDouble(bitstring[i + GlobVar.indiceAmostra8]);
                            //indiceAmostra8++;

                        }

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg1), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg1), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg2), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg2), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg3), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg3), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg4), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg4), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg5), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg5), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg6), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg6), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg7), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg7), 2));
                        i2r2++;

                        //globVar.mean[i2r2] = Math.Abs(Math.Round(Accord.Statistics.Measures.Mean(globVar.avg8), 2));
                        GlobVar.mean[i2r2] = Convert.ToInt32(Math.Round(Accord.Statistics.Measures.Mean(GlobVar.avg8), 2));
                        i2r2++;

                        Array.Clear(GlobVar.avg1, 0, GlobVar.avg1.Length);
                        Array.Clear(GlobVar.avg2, 0, GlobVar.avg2.Length);
                        Array.Clear(GlobVar.avg3, 0, GlobVar.avg3.Length);
                        Array.Clear(GlobVar.avg4, 0, GlobVar.avg4.Length);
                        Array.Clear(GlobVar.avg5, 0, GlobVar.avg5.Length);
                        Array.Clear(GlobVar.avg6, 0, GlobVar.avg6.Length);
                        Array.Clear(GlobVar.avg7, 0, GlobVar.avg7.Length);
                        Array.Clear(GlobVar.avg8, 0, GlobVar.avg8.Length);

                        //this.chart1.Series["Series1"].Points.AddXY(ich1, valorout[ich1]);
                    }



                    fs.Close();

                    GlobVar.indice1 = (GlobVar.amos * GlobVar.npagin);

                    var setpoint = GlobVar.npagin; //tamanho do buffer de escrita

                    GlobVar.valorout1 = new String[setpoint];



                }

                GlobVar.indicetotal = GlobVar.indice1 * 2;

                GlobVar.qtdgrafico = GlobVar.npagin * 8;



            }
            catch (System.UnauthorizedAccessException ex)
            {
                MessageBox.Show("Erro Fatal" + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro Fatal" + ex.Message);
                return;
            }



        }


        public static void LerArquivo()
        {
            string filePath = @"C:\Users\dev_i\source\repos\PlotagemOpenGL\PlotagemOpenGL\Txt's\Serie.txt";

            try
            {
                string[] file = File.ReadAllLines(filePath);
                string[] values;

                foreach (string files in file)
                {
                    values = files.Split(',');
                    GlobVar.canalA = new double[values.Length];
                    GlobVar.canalB = new double[values.Length];
                    GlobVar.canalC = new double[values.Length];
                    GlobVar.canalD = new double[values.Length];
                    GlobVar.canalE = new double[values.Length];
                    GlobVar.canalF = new double[values.Length];
                    GlobVar.canalG = new int[values.Length];
                    GlobVar.canalH = new int[values.Length];
                    GlobVar.canalI = new int[values.Length];
                    GlobVar.canalJ = new int[values.Length];
                    GlobVar.canalK = new int[values.Length];
                    GlobVar.canalL = new int[values.Length];
                    GlobVar.canalM = new int[values.Length];
                    GlobVar.canalN = new int[values.Length];
                    GlobVar.canalO = new int[values.Length];
                    GlobVar.canalP = new int[values.Length];
                    GlobVar.canalQ = new int[values.Length];
                    GlobVar.canalR = new int[values.Length];
                    GlobVar.canalS = new int[values.Length];
                    GlobVar.canalT = new int[values.Length];
                    GlobVar.canalU = new int[values.Length];
                    GlobVar.canalV = new int[values.Length];
                    GlobVar.canalW = new int[values.Length];

                    for (int i = 0; i < values.Length; i++)
                    {
                        GlobVar.canalA[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalB[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalC[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalD[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalE[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalF[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalG[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalH[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalI[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalJ[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalK[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalL[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalM[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalN[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalO[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalP[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalQ[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalR[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalS[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalT[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalU[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalV[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalW[i] = Convert.ToInt32(values[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ocorreu um erro ao ler o arquivo: {ex.Message}");
            }
        }
    }
}