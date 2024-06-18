using System;
using Accord.Audio.Filters;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Numerics;
using System.Text;
using DSP;
using PlotagemOpenGL.auxi;
using Accord.Audio;
using static OpenTK.Graphics.OpenGL.GL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Accord.Math;
using System.Data;

namespace PlotagemOpenGL
{
    public class filtrosSinais
    {
        const double w0 = 0.5;
        const double w1 = -0.5;
        const double s0 = 0.5;
        const double s1 = 0.5;
        public static double[] alterado;

        

        public static void FWT(double[] valorout)
        {
            alterado = new double[valorout.Length];
            double[] temp = new double[valorout.Length];


            for (int itrt = 0; itrt <= 1; itrt++)
            {
                int h = valorout.Length >> 1;
                for (int i = 0; i < h; i++)
                {
                    int k = (i << 1);
                    temp[i] = valorout[k] * s0 + valorout[k + 1] * s1;
                    temp[i + h] = valorout[k] * w0 + valorout[k + 1] * w1;
                }

            }
            alterado = temp;
        }
        public static void VoltaMatriz(Int16 select)
        {
            int colunaCanalIndex = 0;
            int ponteiro = GlobVar.codCanal.IndexOf(select);
            for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0); linhaComp++)
            {
                // Percorre as colunas da matrizCompleta no intervalo especificado por pontI e pontF 
                for (int colunaComp = GlobVar.ponteiroI[ponteiro]; colunaComp < GlobVar.ponteiroF[ponteiro]; colunaComp++)
                {
                    // Certifique-se de não exceder os limites da matrizCanais
                    if (colunaCanalIndex < GlobVar.matrizCanal.GetLength(1))
                    {
                        // Copia o valor de matrizCompleta para matrizCanal
                        GlobVar.matrizCanal[GlobVar.codSelected.IndexOf(select), colunaCanalIndex] = Convert.ToInt16(GlobVar.matrizCompleta[linhaComp, colunaComp]);
                        colunaCanalIndex++;
                    }
                    else
                    {
                        // Se exceder os limites da matrizCanal, saia do loop
                        break;
                    }
                }
            }

            int codCanal1 = select;
            int codCanal2 = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[(GlobVar.codSelected.IndexOf(select))]["CodCanal2"]);
            if (codCanal2 != -1)
            {
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(codCanal1), LeituraEmMatrizTeste.SetReferencia(codCanal1, codCanal2));
            }
            /*for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                if (GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_Montagem.Rows[i]["CodCanal1"]))] < 512)
                {
                    int aux = 512 / GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_Montagem.Rows[i]["CodCanal1"]))];
                    GlobVar.matrizCanal.SetRow<short>(i, LeituraEmMatrizTeste.RemoverMetadeParaFrente(GlobVar.matrizCanal.GetRow<short>(i), aux));
                }
            }*/

        }

    }
}
