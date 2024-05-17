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
using SharpGL.SceneGraph.Raytracing;

namespace PlotagemOpenGL.auxi
{
    internal class Leitura
    {
        public static void QuantidadeCanais()
        {
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
                    GlobVar.qtdCanais = new string[ncanint];
                    for (int ich = 0; ich < ncanint; ich++)
                    {
                        fs.Read(buffer3, 0, buffer3.Length);

                        dadoscanal[ich] = (Encoding.UTF8.GetString(buffer3));

                        string cod = dadoscanal[ich].Substring(0, 2);
                        GlobVar.qtdCanais[ich] = cod;
                        string phrase = dadoscanal[ich];

                        switch (ich)
                        {
                            case 0:
                                GlobVar.nameCanalA = phrase.Substring(11, 10);
                                break;
                            case 1:
                                GlobVar.nameCanalB = phrase.Substring(11, 10);
                                break;
                            case 2:
                                GlobVar.nameCanalC = phrase.Substring(11, 10);
                                break;
                            case 3:
                                GlobVar.nameCanalD = phrase.Substring(11, 10);
                                break;
                            case 4:
                                GlobVar.nameCanalE = phrase.Substring(11, 10);
                                break;
                            case 5:
                                GlobVar.nameCanalF = phrase.Substring(11, 10);
                                break;
                            case 6:
                                GlobVar.nameCanalG = phrase.Substring(11, 10);
                                break;
                            case 7:
                                GlobVar.nameCanalH = phrase.Substring(11, 10);
                                break;
                            case 8:
                                GlobVar.nameCanalI = phrase.Substring(11, 10);
                                break;
                            case 9:
                                GlobVar.nameCanalJ = phrase.Substring(11, 10);
                                break;
                            case 10:
                                GlobVar.nameCanalK = phrase.Substring(11, 10);
                                break;
                            case 11:
                                GlobVar.nameCanalL = phrase.Substring(11, 10);
                                break;
                            case 12:
                                GlobVar.nameCanalM = phrase.Substring(11, 10);
                                break;
                            case 13:
                                GlobVar.nameCanalN = phrase.Substring(11, 10);
                                break;
                            case 14:
                                GlobVar.nameCanalO = phrase.Substring(11, 10);
                                break;
                            case 15:
                                GlobVar.nameCanalP = phrase.Substring(11, 10);
                                break;
                            case 16:
                                GlobVar.nameCanalQ = phrase.Substring(11, 10);
                                break;
                            case 17:
                                GlobVar.nameCanalR = phrase.Substring(11, 10);
                                break;
                            case 18:
                                GlobVar.nameCanalS = phrase.Substring(11, 10);
                                break;
                            case 19:
                                GlobVar.nameCanalT = phrase.Substring(11, 10);
                                break;
                            case 20:
                                GlobVar.nameCanalU = phrase.Substring(11, 10);
                                break;
                            case 21:
                                GlobVar.nameCanalV = phrase.Substring(11, 10);
                                break;
                            case 22:
                                GlobVar.nameCanalW = phrase.Substring(11, 10);
                                break;
                        }

                    }
                    fs.Close();
                }
                }
            catch { }
        }
        public static void LeituraDat()
        {
            string[] dadoscanal = new string[47];
            byte[] buffer0 = new byte[395];
            byte[] buffer1 = new byte[8];
            byte[] buffer2 = new byte[8];
            byte[] buffer3 = new byte[47];
            GlobVar.nomeCanais = new string[GlobVar.qtdCanais.Length];
            GlobVar.txPorCanal = new int[GlobVar.qtdCanais.Length];
            for (int ind = 0; ind < GlobVar.qtdCanais.Length; ind++)
            {
                int ntotal = 0;

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
                        int txPorSeg = 0;
                        for (int ich = 0; ich < ncanint; ich++)
                        {
                            fs.Read(buffer3, 0, buffer3.Length);

                            dadoscanal[ich] = (Encoding.UTF8.GetString(buffer3));

                            string cod = dadoscanal[ich].Substring(0, 2);

                            string phrase = dadoscanal[ich];
                            GlobVar.nomeCanais[ich] = phrase.Substring(11, 10);


                            GlobVar.amos = Convert.ToInt16(phrase.Substring(8, 4));
                            string sizesample3 = phrase.Substring(8, 4);
                            string aux = sizesample3.Replace(" ", "");
                            int auxx = Convert.ToInt16(aux);
                            GlobVar.txPorCanal[ich] = auxx;

                            if (cod.Equals(GlobVar.qtdCanais[ind]))
                            {
                                //string[] hexValuesSplit = dadoscanal[ich].Split(' ');

                                GlobVar.startpos = Convert.ToInt32(ntotal);
                                string sizesample1 = phrase.Substring(8, 4);
                                string banana = sizesample1.Replace(" ", "");

                                GlobVar.sizesample = (Convert.ToInt16(banana) * 2);

                            }
                            ntotal = ntotal + (GlobVar.amos * 2);

                            int ponteirostr = Convert.ToInt16(fs.Position);
                            txPorSeg += auxx;
                        }

                        GlobVar.size = Convert.ToInt32(GlobVar.npag);

                        int recntotal = ntotal;

                        fs.Position = fs.Position - 1;

                        GlobVar.indiceDat = GlobVar.npagin * GlobVar.amos * 2;

                        GlobVar.valorout = new Double[GlobVar.indiceDat];


                        GlobVar.lastcall = 0;


                        for (int ich1 = 0; ich1 < GlobVar.size; ich1++) //leitura de 1 segundo size e igual a quantos segundos tem no arquivo dat
                        {

                            byte[] buffer4 = new byte[ntotal];

                            fs.Read(buffer4, 0, buffer4.Length);

                            Int16[] bitstring = new Int16[GlobVar.sizesample / 2];

                            var limite = GlobVar.startpos + GlobVar.sizesample;

                            for (int i = GlobVar.startpos, j = 0; i < limite; i += 2, j++)
                            {
                                bitstring[j] = BitConverter.ToInt16(buffer4, i);
                                 
                            }
                            for (int i2r = 0, i2r1 = 0; i2r < GlobVar.sizesample / 2; i2r++, i2r1++)
                            {
                                GlobVar.valorout[GlobVar.lastcall] = Convert.ToDouble(bitstring[i2r]);
                                GlobVar.lastcall++;


                            }
                        }
                        //for (int i = 0; i < GlobVar.valorout.Length; i++)
                        //{
                        //    GlobVar.valorout[i] *= -1;
                        //}
                        
                        if (GlobVar.txPorCanal[ind] == 256)
                        {
                            double[] aux = GlobVar.valorout;
                            aux = RemoverMetadeParaFrente(aux);
                            GlobVar.valorout = DuplicarArray(aux);
                        }else if (GlobVar.txPorCanal[ind] == 8)
                        {
                            double[] aux = GlobVar.valorout;
                            aux = AjustarArray8Tx(aux);
                            GlobVar.valorout = DuplicarArray64Vezes(aux);
                            
                        }
                        //for (int i = 0; i < GlobVar.valorout.Length; i++)
                        //{
                        //    GlobVar.valorout[i] = Math.Abs(GlobVar.valorout[i]);
                        //}
                        switch (ind)
                        {
                            case 0:
                                GlobVar.canalA = new double[GlobVar.valorout.Length];
                                GlobVar.canalA = GlobVar.valorout;
                                break;
                            case 1:
                                GlobVar.canalB = new double[GlobVar.valorout.Length];
                                GlobVar.canalB = GlobVar.valorout;
                                break;
                            case 2:
                                GlobVar.canalC = new double[GlobVar.valorout.Length];
                                GlobVar.canalC = GlobVar.valorout;
                                break;
                            case 3:
                                GlobVar.canalD = new double[GlobVar.valorout.Length];
                                GlobVar.canalD = GlobVar.valorout;
                                break;
                            case 4:
                                GlobVar.canalE = new double[GlobVar.valorout.Length];
                                GlobVar.canalE = GlobVar.valorout;
                                break;
                            case 5:
                                GlobVar.canalF = new double[GlobVar.valorout.Length];
                                GlobVar.canalF = GlobVar.valorout;
                                break;
                            case 6:
                                GlobVar.canalG = new double[GlobVar.valorout.Length];
                                GlobVar.canalG = GlobVar.valorout;
                                break;
                            case 7:
                                GlobVar.canalH = new double[GlobVar.valorout.Length];
                                GlobVar.canalH = GlobVar.valorout;
                                break;
                            case 8:
                                GlobVar.canalI = new double[GlobVar.valorout.Length];
                                GlobVar.canalI = GlobVar.valorout;
                                break;
                            case 9:
                                GlobVar.canalJ = new double[GlobVar.valorout.Length];
                                GlobVar.canalJ = GlobVar.valorout;
                                break;
                            case 10:
                                GlobVar.canalK = new double[GlobVar.valorout.Length];
                                GlobVar.canalK = GlobVar.valorout;
                                break;
                            case 11:
                                GlobVar.canalL = new double[GlobVar.valorout.Length];
                                GlobVar.canalL = GlobVar.valorout;
                                break;
                            case 12:
                                GlobVar.canalM = new double[GlobVar.valorout.Length];
                                GlobVar.canalM = GlobVar.valorout;
                                break;
                            case 13:
                                GlobVar.canalN = new double[GlobVar.valorout.Length];
                                GlobVar.canalN = GlobVar.valorout;
                                break;
                            case 14:
                                GlobVar.canalO = new double[GlobVar.valorout.Length];
                                GlobVar.canalO = GlobVar.valorout;
                                break;
                            case 15:
                                GlobVar.canalP = new double[GlobVar.valorout.Length];
                                GlobVar.canalP = GlobVar.valorout;
                                break;
                            case 16:
                                GlobVar.canalQ = new double[GlobVar.valorout.Length];
                                GlobVar.canalQ = GlobVar.valorout;
                                break;
                            case 17:
                                GlobVar.canalR = new double[GlobVar.valorout.Length];
                                GlobVar.canalR = GlobVar.valorout;
                                break;
                            case 18:
                                GlobVar.canalS = new double[GlobVar.valorout.Length];
                                GlobVar.canalS = GlobVar.valorout;
                                break;
                            case 19:
                                GlobVar.canalT = new double[GlobVar.valorout.Length];
                                GlobVar.canalT = GlobVar.valorout;
                                break;
                            case 20:
                                GlobVar.canalU = new double[GlobVar.valorout.Length];
                                GlobVar.canalU = GlobVar.valorout;
                                break;
                            case 21:
                                GlobVar.canalV = new double[GlobVar.valorout.Length];
                                GlobVar.canalV = GlobVar.valorout;
                                break;
                            case 22:
                                GlobVar.canalW = new double[GlobVar.valorout.Length];
                                GlobVar.canalW = GlobVar.valorout;
                                break;
                            case 23:
                                GlobVar.canalW = new double[GlobVar.valorout.Length];
                                GlobVar.canalW = GlobVar.valorout;
                                break;
                            default:
                                // Lidar com o caso padrão, se necessário
                                break;
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



        }
        public static double[] DuplicarArray(double[] array)
        {
            double[] novoArray = new double[array.Length * 2];

            for (int i = 0; i < array.Length; i++)
            {
                novoArray[i * 2] = novoArray[i * 2 + 1] = array[i];
            }

            return novoArray;
        }
        public static double[] DuplicarArray64Vezes(double[] array)
        {
            double[] novoArray = new double[array.Length * 64];

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < 64; j++)
                {
                    novoArray[i * 64 + j] = array[i];
                }
            }

            return novoArray;
        }


        public static double[] RemoverMetadeParaFrente(double[] array)
        {
            int novaTamanho = array.Length / 2;
            double[] novoArray = new double[novaTamanho];

            for (int i = 0; i < novaTamanho; i++)
            {
                novoArray[i] = array[i];
            }

            return novoArray;
        }
        public static double[] AjustarArray8Tx(double[] array)
        {
            int novaTamanho = array.Length / 64;
            double[] novoArray = new double[novaTamanho];
            for(int i = 0;i < novaTamanho; i++)
            {
                novoArray[i] = array[i];
            }

            return novoArray;
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
                    GlobVar.canalG = new double[values.Length];
                    GlobVar.canalH = new double[values.Length];
                    GlobVar.canalI = new double[values.Length];
                    GlobVar.canalJ = new double[values.Length];
                    GlobVar.canalK = new double[values.Length];
                    GlobVar.canalL = new double[values.Length];
                    GlobVar.canalM = new double[values.Length];
                    GlobVar.canalN = new double[values.Length];
                    GlobVar.canalO = new double[values.Length];
                    GlobVar.canalP = new double[values.Length];
                    GlobVar.canalQ = new double[values.Length];
                    GlobVar.canalR = new double[values.Length];
                    GlobVar.canalS = new double[values.Length];
                    GlobVar.canalT = new double[values.Length];
                    GlobVar.canalU = new double[values.Length];
                    GlobVar.canalV = new double[values.Length];
                    GlobVar.canalW = new double[values.Length];

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