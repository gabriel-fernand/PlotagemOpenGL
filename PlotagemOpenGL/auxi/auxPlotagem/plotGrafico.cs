using Accord.Math;
using SharpGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.auxi.auxPlotagem
{
    internal class plotGrafico
    {
        private OpenGL gl;
        private string[] Canula;
        private float[] margem;
        private float[] traco;

        public plotGrafico(OpenGL gl)
        {
            this.gl = gl;
        }
        public static void DesenhaGrafico(int qtdGraf, OpenGL gl, float[] desenhoLoc)
        {
            gl.Color(0.0f, 0.0f, 0.0f);
            int des = qtdGraf - 1;
            for (int i = 0; i < qtdGraf; i++)
            {
                bool verTx = false;
                int ponteiroDesenho = 0;
                int h = GlobVar.indice;
                if (GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[i]]["CodCanal1"]))] != 512)
                {
                    verTx = true;
                    ponteiroDesenho = 512 / GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[i]]["CodCanal1"]))];

                }
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {

                    if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                    else
                    {
                        if (verTx)
                        {
                            if (h < 0 || h >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(h - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            else
                            {
                                gl.Vertex(j, (GlobVar.matrizCanal[GlobVar.grafSelected[i], h] * GlobVar.scale[i]) + desenhoLoc[des]);
                                h++; //aqui tem plotar 3 graficos diferentes
                                j += ponteiroDesenho - 1;
                            }
                        }
                        else
                        {
                            gl.Vertex(j, (GlobVar.matrizCanal[GlobVar.grafSelected[i], j] * GlobVar.scale[i]) + desenhoLoc[des]); //aqui tem plotar 3 graficos diferentes
                        }
                    }
                }
                des--;

                gl.End();
            }


        }
        public static void TracejadoLinhaZero(OpenGL gl, int qtdGraf)
        {
            for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.Begin(OpenGL.GL_LINES);
                gl.Color(0.752941f, 0.752941f, 0.752941f);
                gl.Vertex(1, Plotagem.traco[i]);
                gl.Vertex(GlobVar.matrizCanal.GetLength(1), Plotagem.traco[i]);
                gl.End();
            }
            gl.Disable(OpenGL.GL_LINE_STIPPLE);

        }

    }
}
