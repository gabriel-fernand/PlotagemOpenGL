using Accord.Math;
using SharpGL;
using System;
using System.Drawing.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Linq.Expressions;
using System.Windows.Forms;

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
            try
            {
                int des = qtdGraf - 1;

                for (int i = 0; i < qtdGraf; i++)
                {
                    if (((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] || (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] != DBNull.Value)) && (GlobVar.codSelected[i] == 67 || GlobVar.codSelected[i] == 66 || GlobVar.codSelected[i] == 14))
                    {

                        int h = GlobVar.indice;
                        float[] color = new float[3];

                        color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["Cor"]));
                        string txtEmTela = $"";
                        float espacoEntreNumeros = GlobVar.sizeOpenGl.X / (GlobVar.maximaNumero - GlobVar.indiceNumero);

                        for (int j = GlobVar.indiceNumero; j < GlobVar.maximaNumero; j++)
                        {
                            int x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);

                            if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            else
                            {
                                if (GlobVar.codSelected[i] == 66 || GlobVar.codSelected[i] == 67)
                                {
                                    gl.End();
                                    int y = -7000;

                                    bool HorizontalOuVertical = (bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["AutoEscala"];

                                    int codCanal1 = GlobVar.codSelected[i];
                                    int locaux;
                                    foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                                    {
                                        int tagCod = (int)pn.Tag;
                                        if (tagCod == codCanal1)
                                        {
                                            int topPn = pn.Top;
                                            int auxloc = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                                            double meioPn = pn.Height / 2;
                                            y = auxloc - (int)meioPn;
                                        }
                                    }

                                    int aux = 0;
                                    float me = 0;
                                    for (int g = j; g < j + 8;)
                                    {
                                        //if(GlobVar.matrizCanal[GlobVar.grafSelected[i], g] < 0) { GlobVar.matrizCanal[GlobVar.grafSelected[i], g] *= -1;  }
                                        aux += GlobVar.matrizCanal[GlobVar.grafSelected[i], g];
                                        g += 8;
                                    }
                                    me = aux;
                                    txtEmTela = $" {me} ";

                                    int meh = 14;
                                    int fontsize = 19;
                                    //if ((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"])
                                    //{
                                    //    //meh += 2;
                                    //    fontsize += 3;
                                    //}
                                    float writeX = (j - GlobVar.indiceNumero) * espacoEntreNumeros;
                                    x += (int)writeX;
                                    if (HorizontalOuVertical) // if(GlobVar.segundos >= 60)
                                    {
                                        int yTop = y + 5;
                                        int yBot = y - 5;
                                        meh = 6;
                                        fontsize = 12;

                                        // Lógica para definir 'top' e 'bot' de acordo com o tamanho da string
                                        string top, bot;
                                        if (txtEmTela.Length > 4)
                                        {
                                            // Pega os dois primeiros caracteres para o 'top' e o último para o 'bot'
                                            top = txtEmTela.Substring(1, 2); // dois primeiros caracteres
                                            bot = txtEmTela.Substring(3, 1); // último caractere
                                        }
                                        else
                                        {
                                            // Caso tenha 3 caracteres ou menos, pega o primeiro e o último
                                            top = txtEmTela.Substring(1, 1); // primeiro caractere
                                            bot = txtEmTela.Substring(2, 1); // último caractere
                                        }

                                        if(GlobVar.segundos >= 60)
                                        {
                                            // Desenhar o texto no topo
                                            gl.DrawText(0, yTop, color[0], color[1], color[2], "Calibri", meh, ""); // Necessário para o próximo texto aparecer
                                            gl.DrawText(x, yTop, color[0], color[1], color[2], "Calibri", fontsize, top);

                                            // Desenhar o texto no rodapé
                                            gl.DrawText(0, yTop, color[0], color[1], color[2], "Calibri", meh, ""); // Necessário para o próximo texto aparecer
                                            gl.DrawText(x, yBot, color[0], color[1], color[2], "Calibri", fontsize, bot);
                                        }
                                        else
                                        {
                                            yTop = y + 7;
                                            yBot = y - 8;

                                            meh = 14;
                                            fontsize = 19;

                                            // Desenhar o texto no topo
                                            gl.DrawText(0, yTop, color[0], color[1], color[2], "Calibri", meh, ""); // Necessário para o próximo texto aparecer
                                            gl.DrawText(x, yTop, color[0], color[1], color[2], "Calibri", fontsize, top);

                                            // Desenhar o texto no rodapé
                                            gl.DrawText(0, yTop, color[0], color[1], color[2], "Calibri", meh, ""); // Necessário para o próximo texto aparecer
                                            gl.DrawText(x, yBot, color[0], color[1], color[2], "Calibri", fontsize, bot);
                                        }
                                    }
                                    else
                                    {
                                        if(GlobVar.segundos >= 60)
                                        {
                                            meh = 6;
                                            fontsize = 12;
                                            gl.DrawText(0, y, color[0], color[1], color[2], "Arial Narrow", meh, ""); //Nao entendi o pq mas precisa desse para o outro mostrar na tela
                                            gl.DrawText(x, y, color[0], color[1], color[2], "Arial Narrow", fontsize, txtEmTela);
                                        }
                                        else
                                        {
                                            gl.DrawText(0, y, color[0], color[1], color[2], "Arial Narrow", meh, ""); //Nao entendi o pq mas precisa desse para o outro mostrar na tela
                                            gl.DrawText(x, y, color[0], color[1], color[2], "Arial Narrow", fontsize, txtEmTela);
                                        }
                                    }
                                    h++; //aqui tem plotar 3 graficos diferentes                                
                                    j += 7;
                                    gl.End();
                                    gl.Flush();
                                    
                                }

                            }
                        }
                        gl.Flush();

                    }

                    des--;
                }
            }
            catch { }
        }

        public static void PlotSetas(int qtdGraf, OpenGL gl, float[] desenhoLoc)
        {
            try{
                int des = qtdGraf - 1;
                ValoresPosicoes();
                for (int i = 0; i < qtdGraf; i++)
                {
                    if (((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] && (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] == DBNull.Value)) && (GlobVar.codSelected[i] == 14))
                    {
                        int incremento = GlobVar.segundos <= 60 ? 0 : GlobVar.segundos <= 120 ? 7 : 14;
                        int h = GlobVar.indice;
                        float[] color = new float[3];

                        color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["Cor"]));
                        string txtEmTela = $"";
                        float incEspaco = GlobVar.segundos <= 60 ? 1 : GlobVar.segundos <= 120 ? 1.5f : 2;
                        float espacoEntreNumeros = (GlobVar.sizeOpenGl.X / (GlobVar.maximaNumero - GlobVar.indiceNumero)) * incEspaco;

                        for (int j = GlobVar.indiceNumero; j < GlobVar.maximaNumero; j++)
                        {
                            int x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);

                            if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            else
                            {
                                if (GlobVar.codSelected[i] == 14)
                                {
                                    gl.End();
                                    x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);
                                    gl.Begin(OpenGL.GL_2D);

                                    int y = -7000;
                                    int codCanal1 = GlobVar.codSelected[i];
                                    int locaux;
                                    foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                                    {
                                        int tagCod = (int)pn.Tag;
                                        if (tagCod == codCanal1)
                                        {
                                            int topPn = pn.Top;
                                            int auxloc = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                                            double meioPn = pn.Height / 2;
                                            y = auxloc - (int)meioPn;
                                        }
                                    }

                                    int posi = GlobVar.matrizCanal[GlobVar.grafSelected[i], j];
                                    int[] aloa = new int[8];
                                    int aoba = 0;
                                    for(int ao  = j; aoba < 8; ao++)
                                    {
                                        aloa[aoba] = GlobVar.matrizCanal[GlobVar.grafSelected[i], ao];
                                        aoba++;
                                    }

                                    if (posi >= (GlobVar.PosCima - GlobVar.PosIncremento) && posi <= (GlobVar.PosCima + GlobVar.PosIncremento)) // CIMA
                                    {
                                        txtEmTela = "ã";
                                    }
                                    else if (posi >= (GlobVar.PosDireita - GlobVar.PosIncremento) && posi <= (GlobVar.PosDireita + GlobVar.PosIncremento)) // DIREITA
                                    {
                                        txtEmTela = "á";
                                    }
                                    else if (posi >= (GlobVar.PosBaixo - GlobVar.PosIncremento) && posi <= (GlobVar.PosBaixo + GlobVar.PosIncremento)) //BAIXO
                                    {
                                        txtEmTela = "ä";
                                    }
                                    else if (posi >= (GlobVar.PosEsquerda - GlobVar.PosIncremento) && posi <= (GlobVar.PosEsquerda + GlobVar.PosIncremento)) // ESQUERDA
                                    {
                                        txtEmTela = "â";
                                    }
                                    else
                                    {
                                        txtEmTela = "ã";

                                    }
                                    //txtEmTela = "ã";
                                    float writeX = (j - GlobVar.indiceNumero) * espacoEntreNumeros;
                                    x += (int)writeX;
                                    y -= 5;
                                    gl.DrawText(0, 0, color[0], color[1], color[2], "Wingdings 3", 18, ""); //Nao entendi o pq mas precisa desse para o outro mostrar na tela
                                    gl.DrawText(x, y, color[0], color[1], color[2], "Wingdings 3", 22, txtEmTela);
                                    //gl.DrawText3D("Wingdings 3", x, y, 0.0f,txtEmTela);
                                    h++; //aqui tem plotar 3 graficos diferentes                                
                                    j += 7 + incremento;
                                    gl.End();
                                    gl.Flush();
                                }
                            }
                        }
                        gl.Flush();
                    }
                    else if ((!(bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] && (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] != DBNull.Value)) && (GlobVar.codSelected[i] == 14))
                    {
                        int h = GlobVar.indice;
                        float[] color = new float[3];

                        color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["Cor"]));
                        string txtEmTela = $"";
                        float espacoEntreNumeros = GlobVar.sizeOpenGl.X / (GlobVar.maximaNumero - GlobVar.indiceNumero);

                        for (int j = GlobVar.indiceNumero; j < GlobVar.maximaNumero; j++)
                        {
                            int x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);

                            if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            else
                            {
                                if (GlobVar.codSelected[i] == 14)
                                {
                                    gl.End();
                                    x = (int)((GlobVar.sizeOpenGl.X / GlobVar.segundos) / 3);
                                    gl.Begin(OpenGL.GL_2D);

                                    int y = -7000;
                                    int codCanal1 = GlobVar.codSelected[i];
                                    int locaux;
                                    foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                                    {
                                        int tagCod = (int)pn.Tag;
                                        if (tagCod == codCanal1)
                                        {
                                            int topPn = pn.Top;
                                            int auxloc = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                                            double meioPn = pn.Height / 2;
                                            y = auxloc - (int)meioPn;
                                        }
                                    }

                                    int posi = GlobVar.matrizCanal[GlobVar.grafSelected[i], j];

                                    txtEmTela = $"{posi}";

                                    //txtEmTela = "ã";
                                    float writeX = (j - GlobVar.indiceNumero) * espacoEntreNumeros;
                                    x += (int)writeX;
                                    y -= 5;
                                    gl.DrawText(0, 0, color[0], color[1], color[2], "Arial Narrow", 14, ""); //Nao entendi o pq mas precisa desse para o outro mostrar na tela
                                    gl.DrawText(x, y, color[0], color[1], color[2], "Arial Narrow", 19, txtEmTela);
                                    //gl.DrawText3D("Wingdings 3", x, y, 0.0f,txtEmTela);
                                    h++; //aqui tem plotar 3 graficos diferentes                                
                                    j += 7;
                                    gl.End();
                                    gl.Flush();
                                }
                            }
                        }

                    }
                    des--;
                }
            }
            catch { }
        }

        public static void ValoresPosicoes()
        {
            if(GlobVar.tbl_DadosExame != null)
            {
                if(GlobVar.tbl_DadosExame.Rows[0]["SensorPosCima"] != null)
                {
                    GlobVar.PosCima = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["SensorPosCima"]);
                }
                else
                {
                    GlobVar.PosCima = 21502;
                }
                if(GlobVar.tbl_DadosExame.Rows[0]["SensorPosDireita"] != null)
                {
                    GlobVar.PosDireita = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["SensorPosDireita"]);
                }
                else
                {
                    GlobVar.PosDireita = -4070;
                }
                if(GlobVar.tbl_DadosExame.Rows[0]["SensorPosEsquerda"] != null)
                {
                    GlobVar.PosEsquerda = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["SensorPosEsquerda"]);
                }
                else
                {
                    GlobVar.PosEsquerda = -14031;
                }
                if(GlobVar.tbl_DadosExame.Rows[0]["SensorPosBaixo"] != null)
                {
                    GlobVar.PosBaixo = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["SensorPosBaixo"]);
                }
                else
                {
                    GlobVar.PosBaixo = -16887;
                }
                if (GlobVar.tbl_DadosExame.Rows[0]["SensorPosIncremento"] != null){
                    GlobVar.PosIncremento = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["SensorPosIncremento"]);
                }
                else
                {
                    GlobVar.PosIncremento = 2110;
                }
            }
            else
            {
                GlobVar.PosCima = 21502;
                GlobVar.PosDireita = -4070;
                GlobVar.PosEsquerda = -14031;
                GlobVar.PosBaixo = -16887;
                GlobVar.PosIncremento = 2110;
            }
        }
    }

}
