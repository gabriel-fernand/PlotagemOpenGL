﻿using Accord.Math;
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

            for (int i = 0; i < qtdGraf; i++)
            {
                if ((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] && GlobVar.codSelected[i] == 67 || GlobVar.codSelected[i] == 66 || GlobVar.codSelected[i] == 14)
                {

                    int h = GlobVar.indice;
                    float[] color = new float[3];

                    color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[i]]["Cor"]));
                    string txtEmTela = $"";
                    float espacoEntreNumeros = GlobVar.sizeOpenGl.X / (GlobVar.maximaNumero - GlobVar.indiceNumero);
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha

                    for (int j = GlobVar.indiceNumero; j < GlobVar.maximaNumero; j++)
                    {
                        int x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);

                        if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                        else
                        {
                            if (GlobVar.codSelected[i] == 66 || GlobVar.codSelected[i] == 67)
                            {
                                gl.End();
                                gl.Begin(OpenGL.GL_2D);
                                int y;

                                int aux = 0;
                                float me = 0;
                                for (int g = j; g < j + 8; g++)
                                {
                                    //if(GlobVar.matrizCanal[GlobVar.grafSelected[i], g] < 0) { GlobVar.matrizCanal[GlobVar.grafSelected[i], g] *= -1;  }
                                    aux += GlobVar.matrizCanal[GlobVar.grafSelected[i], g];
                                }
                                me = aux / 8;

                                txtEmTela = $" {me} ";
                                float writeX = (j - GlobVar.indiceNumero) * espacoEntreNumeros;
                                x += (int)writeX;
                                y = (int)desenhoLoc[des] - 5;
                                gl.DrawText(0, 0, color[0], color[1], color[2], "Arial Narrow", 18, ""); //Nao entendi o pq mas precisa desse para o outro mostrar na tela
                                gl.DrawText(x, y, color[0], color[1], color[2], "Arial Narrow", 20, txtEmTela);

                                h++; //aqui tem plotar 3 graficos diferentes                                
                                j += 7;
                                gl.Flush();
                            }
                            if (GlobVar.codSelected[i] == 14)
                            {
                                x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);
                                gl.End();
                                gl.Begin(OpenGL.GL_2D);

                                int y;

                                int posi = GlobVar.matrizCanal[GlobVar.grafSelected[i], j];

                                if(posi <= (21502 - 2110) && posi >= (21502 + 2110)) // CIMA
                                {
                                    txtEmTela = "ã";
                                }
                                else if (posi <= (-4070 - 2110) && posi >= (-4070 + 2110)) // DIREITA
                                {
                                    txtEmTela = "á";
                                }
                                else if (posi <= (-16887 - 2110) && posi >= (-16887 + 2110)) //BAIXO
                                {
                                    txtEmTela = "ä";
                                }
                                else if (posi <= (-14031 - 2110) && posi >= (-14031 + 2110)) // ESQUERDA
                                {
                                    txtEmTela = "â";
                                }
                                else
                                {
                                    txtEmTela = "ã";

                                }


                                txtEmTela = "ã";
                                float writeX = (j - GlobVar.indiceNumero) * espacoEntreNumeros;
                                x += (int)writeX;
                                y = (int)desenhoLoc[des] - 5;
                                gl.DrawText(0, 0, color[0], color[1], color[2], "Wingdings 3", 18, ""); //Nao entendi o pq mas precisa desse para o outro mostrar na tela
                                gl.DrawText(x, y, color[0], color[1], color[2], "Wingdings 3", 22, txtEmTela);

                                h++; //aqui tem plotar 3 graficos diferentes                                
                                j += 7;
                                gl.Flush();

                            }
                        }
                    }
                    gl.End();

                }

                des--;

            }
        } 
    }
           
}
