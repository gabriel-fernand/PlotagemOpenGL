using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using Accord.Math;


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
            GlobVar.txPorCanal = new int[GlobVar.qtdCanais.Length];
            GlobVar.ponteiro = new int[GlobVar.qtdCanais.Length];

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

                    GlobVar.amos = Convert.ToInt16(phrase.Substring(8, 4));
                    string sizesample3 = phrase.Substring(8, 4);
                    string aux = sizesample3.Replace(" ", "");
                    int auxx = Convert.ToInt16(aux);
                    GlobVar.txPorCanal[ich] = auxx;

                        //string[] hexValuesSplit = dadoscanal[ich].Split(' ');

                        GlobVar.startpos = Convert.ToInt32(ntotal);
                        string sizesample1 = phrase.Substring(8, 4);
                        string banana = sizesample1.Replace(" ", "");

                        GlobVar.sizesample = (Convert.ToInt16(banana) * 2);
                    ntotal = ntotal + (GlobVar.amos * 2);

                    int ponteirostr = Convert.ToInt16(fs.Position);
                    txPorSeg += auxx;
                    GlobVar.ponteiro[ich] = txPorSeg;

                }

                GlobVar.matrizCompleta = new float[GlobVar.npagin, txPorSeg];


                GlobVar.size = Convert.ToInt32(GlobVar.npag);

                int recntotal = ntotal;

                fs.Position = fs.Position - 1;

                for (int ich1 = 0; ich1 < GlobVar.size; ich1++) //leitura de 1 segundo size e igual a quantos segundos tem no arquivo dat
                {

                    byte[] buffer4 = new byte[ntotal];

                    fs.Read(buffer4, 0, buffer4.Length);

                    Int16[] bitstring = new Int16[GlobVar.sizesample / 2];

                    var limite = GlobVar.startpos + GlobVar.sizesample;

                    for (int i = 0, j = 0; i < ntotal; i += 2, j++)
                    {
                        GlobVar.matrizCompleta[ich1, j] = BitConverter.ToInt16(buffer4, i);

                    }
                }
            }

        }
    }
}

