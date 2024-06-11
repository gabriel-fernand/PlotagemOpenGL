using Accord.Math;
using SharpGL;
using System;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PlotagemOpenGL.auxi.auxPlotagem
{
    internal class plotNumerico
    {
        private OpenGL gl;
        private string[] Canula;
        private float[] margem;
        private float[] traco;

        public static void PlotNumerico(int qtdGraf, OpenGL gl, float[] desenhoLoc)
        {
            int des = qtdGraf - 1;
            int telaInicio = 0;
            telaInicio += GlobVar.indice;
            int telaFim = 0;
            telaFim += (int)GlobVar.sizeOpenGl.X;
            for (int i = 0; i < qtdGraf; i++)
            {
                int ponteiroDesenho = 0;
                int h = GlobVar.indice;
                float[] color = new float[3];

                color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["Cor"]));
                if (GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[i]]["CodCanal1"]))] != 512)
                {
                    ponteiroDesenho = 512 / GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[i]]["CodCanal1"]))];

                }
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha

                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    
                    if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                    else
                    {
                        if (GlobVar.codSelected[i] == 66 || GlobVar.codSelected[i] == 67)
                        {
                            gl.End();
                            gl.Begin(OpenGL.GL_2D);
                            int y;
                            int x = j;
                            string media = $"{GlobVar.matrizCanal[GlobVar.grafSelected[i], h] :F0}";

                            y = (int)desenhoLoc[des];
                            float writeX = (j / GlobVar.tmpEmTela * GlobVar.sizeOpenGl.X) + 7; //Faz o mapeamento para fazer a escrita no quadrado com base no tamanho da tela
                            x += (int)writeX;
                            gl.DrawText(x, y, color[0], color[1], color[2], "Arial Narrow", 15, media);

                            h++; //aqui tem plotar 3 graficos diferentes
                            j += ponteiroDesenho - 1;
                            gl.Flush();
                        }
                    }
                }
                des--;

                gl.End();
            }
        } 
    }
           
}
