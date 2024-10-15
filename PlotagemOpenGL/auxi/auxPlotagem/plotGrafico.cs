using Accord.Math;
using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            try{
                bool text = false;
                //gl.Color(0.0f, 0.0f, 0.0f);
                int des = qtdGraf - 1;
                for (int i = 0; i < qtdGraf; i++)
                {
                    bool verTx = false;
                    int ponteiroDesenho = 0;
                    int h = 0;
                    float[] color = new float[3];
                    bool faixaAmplitude = false;
                    if (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] != DBNull.Value)
                    {
                        faixaAmplitude = true;
                    }
                    int top = 0;
                    int butt = 0;

                    color = ObterComponentesRGB(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["Cor"]));
                    gl.Color(color[0], color[1], color[2]);
                    if (((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] && (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] == DBNull.Value)) && (GlobVar.codSelected[i] == 67 || GlobVar.codSelected[i] == 66 || GlobVar.codSelected[i] == 14))
                    {
                        if (!(bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] && (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] == DBNull.Value) && GlobVar.codSelected[i] == 14)
                        {
                            int loc = -7000;

                            int codCanal1 = GlobVar.codSelected[i];
                            int locaux;
                            int areaMaxima = 0;
                            int areaMinima = 0;
                            foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                            {
                                int tagCod = (int)pn.Tag;
                                if (tagCod == -1)
                                {
                                    continue;
                                }
                                if (tagCod == codCanal1)
                                {
                                    int topPn = pn.Top;
                                    int aux = Math.Abs((pn.Top + pn.Height) - Tela_Plotagem.painelExames.Height);
                                    areaMinima = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);                                   
                                    areaMaxima = pn.Height;
                                    loc = aux;// - (int)meioPn;
                                }
                            }

                            if (!GlobVar.codCanal.Contains(codCanal1))
                            {
                                // Pula para a próxima iteração se codCanal1 não estiver em GlobVar.codCanal
                                continue;
                            }

                            int index = GlobVar.codCanal.IndexOf(codCanal1);

                            if (GlobVar.txPorCanal[index] != 512)
                            {
                                verTx = true;
                                ponteiroDesenho = 512 / GlobVar.txPorCanal[index];
                                h = GlobVar.indice / ponteiroDesenho;
                            }
                            gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                            for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                            {
                                //if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                                //else
                                //{
                                if (verTx)
                                {
                                    //if (h < 0 || h >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(h - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                                    //else
                                    int valormatriz = verTx ? GlobVar.matrizCanal[GlobVar.grafSelected[i], h] : GlobVar.matrizCanal[GlobVar.grafSelected[i], j];

                                    // Valores mínimo e máximo possíveis para valormatriz (ajuste conforme necessário)
                                    int minVal = -16887 - 2110; // Exemplo: ajuste conforme os limites reais dos dados
                                    int maxVal = 21502 + 2110;  // Exemplo: ajuste conforme os limites reais dos dados

                                    // Normaliza valormatriz usando os quatro valores de referência e ajustando na área de desenho
                                    valormatriz = NormalizeValue(valormatriz, GlobVar.PosCima, GlobVar.PosBaixo, GlobVar.PosEsquerda, GlobVar.PosDireita, areaMinima, areaMaxima);

                                    gl.Vertex(j, (valormatriz + loc));
                                    h++; //aqui tem plotar 3 graficos diferentes
                                    j += ponteiroDesenho - 1;
                                    //}
                                }
                                else
                                {
                                    gl.Vertex(j, (GlobVar.matrizCanal[GlobVar.grafSelected[i], j] + loc)); //aqui tem plotar 3 graficos diferentes
                                }
                                //}
                            }
                        }

                    }
                    else if(((!(bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] && (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] == DBNull.Value)) && (GlobVar.codSelected[i] == 14)))
                    {
                        int loc = -7000;

                        int codCanal1 = GlobVar.codSelected[i];
                        int locaux;
                        int areaMaxima = 0;
                        int areaMinima = 0;
                        foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                        {
                            int tagCod = (int)pn.Tag;
                            if (tagCod == -1)
                            {
                                continue;
                            }

                            if (tagCod == codCanal1)
                            {
                                int topPn = pn.Top;
                                int aux = Math.Abs((pn.Top + pn.Height) - Tela_Plotagem.painelExames.Height);
                                areaMaxima = pn.Height;
                                loc = aux;// - (int)meioPn;
                            }
                        }

                        if (!GlobVar.codCanal.Contains(codCanal1))
                        {
                            // Pula para a próxima iteração se codCanal1 não estiver em GlobVar.codCanal
                            continue;
                        }

                        int index = GlobVar.codCanal.IndexOf(codCanal1);

                        if (GlobVar.txPorCanal[index] != 512)
                        {
                            verTx = true;
                            ponteiroDesenho = 512 / GlobVar.txPorCanal[index];
                            h = GlobVar.indice / ponteiroDesenho;
                        }
                        gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                        for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                        {
                            //if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            //else
                            //{
                            if (verTx)
                            {
                                //if (h < 0 || h >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(h - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                                //else
                                int valormatriz = verTx ? GlobVar.matrizCanal[GlobVar.grafSelected[i], h] : GlobVar.matrizCanal[GlobVar.grafSelected[i], j];

                                // Valores mínimo e máximo possíveis para valormatriz (ajuste conforme necessário)
                                int minVal = GlobVar.PosBaixo - GlobVar.PosIncremento; // Exemplo: ajuste conforme os limites reais dos dados
                                int maxVal = GlobVar.PosCima + GlobVar.PosIncremento;  // Exemplo: ajuste conforme os limites reais dos dados

                                // Normaliza valormatriz dentro dos limites de areaMinima e areaMaxima
                                //valormatriz = NormalizeValue(valormatriz, minVal, maxVal, areaMinima, areaMaxima);

                                // Chamada ajustada da função de normalização
                                valormatriz = NormalizeValue(valormatriz, GlobVar.PosCima, GlobVar.PosBaixo, GlobVar.PosEsquerda, GlobVar.PosDireita, areaMinima, areaMaxima);

                                gl.Vertex(j, (valormatriz + loc));
                                h++; //aqui tem plotar 3 graficos diferentes
                                j += ponteiroDesenho - 1;
                                //}
                            }
                            else
                            {
                                gl.Vertex(j, (GlobVar.matrizCanal[GlobVar.grafSelected[i], j] + loc)); //aqui tem plotar 3 graficos diferentes
                            }
                            //}
                        }

                    }
                    else if ((!(bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"] || (GlobVar.tbl_MontagemSelecionada.Rows[i]["EliminaFreqInf"] != DBNull.Value)) && (GlobVar.codSelected[i] == 67 || GlobVar.codSelected[i] == 66))
                    {
                        int loc = -7000;

                        int codCanal1 = GlobVar.codSelected[i];
                        int locaux;
                        foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                        {
                            int tagCod = (int)pn.Tag;
                            if (tagCod == -1)
                            {
                                continue;
                            }

                            if (tagCod == codCanal1)
                            {
                                int topPn = pn.Top;
                                int aux = Math.Abs((pn.Top + pn.Height) - Tela_Plotagem.painelExames.Height);

                                double meioPn = pn.Height / 2;
                                loc = aux;// - (int)meioPn;
                            }
                        }
                        int LimiteInferior = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["LimiteInferior"]);
                        int LimiteSuperior = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["LimiteSuperior"]);

                        if (!GlobVar.codCanal.Contains(codCanal1))
                        {
                            // Pula para a próxima iteração se codCanal1 não estiver em GlobVar.codCanal
                            continue;
                        }

                        int index = GlobVar.codCanal.IndexOf(codCanal1);

                        if (GlobVar.txPorCanal[index] != 512)
                        {
                            verTx = true;
                            ponteiroDesenho = 512 / GlobVar.txPorCanal[index];
                            h = GlobVar.indice / ponteiroDesenho;
                        }
                        gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                        for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                        {
                            //if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            //else
                            //{
                            if (verTx)
                            {
                                //if (h < 0 || h >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(h - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                                //else
                                //{
                                int valormatriz = 0;
                                if (GlobVar.matrizCanal[GlobVar.grafSelected[i], h] < LimiteInferior)
                                {
                                    valormatriz = 0;
                                }
                                else if(GlobVar.matrizCanal[GlobVar.grafSelected[i], h] > LimiteSuperior)
                                {
                                    valormatriz = LimiteSuperior - LimiteInferior;
                                }else
                                {
                                    valormatriz = (int)((double)GlobVar.matrizCanal[GlobVar.grafSelected[i], h] - LimiteInferior);
                                }
                                gl.Vertex(j, (valormatriz + loc));
                                h++; //aqui tem plotar 3 graficos diferentes
                                j += ponteiroDesenho - 1;
                                //}
                            }
                            else
                            {
                                gl.Vertex(j, (GlobVar.matrizCanal[GlobVar.grafSelected[i], j] + loc)); //aqui tem plotar 3 graficos diferentes
                            }
                            //}
                        }
                    }
                    else if (GlobVar.codSelected[i] == 14)
                    {
                        continue;
                    }
                    else
                    {
                        int inverteSinal = 1;
                        if ((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["InverteSinal"])
                        {
                            inverteSinal = -1;
                        }
                        int loc = -7000;

                        int codCanal1 = GlobVar.codSelected[i];
                        int locaux;
                        foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                        {
                            int tagCod = (int)pn.Tag;
                            if (tagCod == -1)
                            {
                                continue;
                            }

                            if (tagCod == codCanal1)
                            {
                                int topPn = pn.Top;
                                int aux = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);
                                top = aux;
                                butt = aux - pn.Height;
                                double meioPn = pn.Height / 2;
                                loc = aux - (int)meioPn;
                            }
                        }
                        double scala = GlobVar.scale[i];
                        if ((bool)GlobVar.tbl_MontagemSelecionada.Rows[i]["AutoEscala"])
                        {
                            scala = verificaAutoEscala(i, butt, top, loc);
                        }


                        if (!GlobVar.codCanal.Contains(codCanal1))
                        {
                            // Pula para a próxima iteração se codCanal1 não estiver em GlobVar.codCanal
                            continue;
                        }

                        int index = GlobVar.codCanal.IndexOf(codCanal1);

                        if (GlobVar.txPorCanal[index] != 512)
                        {
                            verTx = true;
                            ponteiroDesenho = 512 / GlobVar.txPorCanal[index];
                            h = GlobVar.indice / ponteiroDesenho;
                        }
                        gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                        for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                        {
                            //if (j < 0 || j >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(j - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                            //else
                            //{
                            if (verTx)
                            {
                                //if (h < 0 || h >= GlobVar.matrizCanal.GetLength(1)) gl.Vertex(h - 1, desenhoLoc[des]); // Define cada ponto do gráfico
                                //else
                                //{
                                gl.Vertex(j, ((GlobVar.matrizCanal[GlobVar.grafSelected[i], h] / scala * inverteSinal) + loc) ) ;
                                h++; //aqui tem plotar 3 graficos diferentes
                                j += ponteiroDesenho - 1;
                                //}
                            }
                            else
                            {
                                gl.Vertex(j, (GlobVar.matrizCanal[GlobVar.grafSelected[i], j] / scala * inverteSinal) + loc); //aqui tem plotar 3 graficos diferentes
                            }
                            //}
                        }
                    }
                    des--;
                    des--;
                    gl.End();
                    if (faixaAmplitude)
                    {
                        gl.Begin(OpenGL.GL_LINE_LOOP);
                        gl.PointSize(10.0f); // Define o tamanho dos pontos
                        gl.Color(255, 0, 0, 1f);
                        //gl.ColorMask(3, 6, 7, alpha);
                        gl.Vertex(GlobVar.indice, butt, -1.8f);
                        gl.Vertex(GlobVar.maximaVect, butt, -1.8f);
                        gl.Vertex(GlobVar.maximaVect, top, -1.8f);
                        gl.Vertex(GlobVar.indice, top, -1.8f);
                        gl.End();
                        gl.Flush();
                    }
                }
            }
            catch { }
        }

        public static double verificaAutoEscala(int indice, int areaMin, int areaMaxima, int loc)
        {
            float scala = 0;
            bool verTx = false;
            int ponteiroDesenho = 0;
            int h = 0;

            // Verifica se o valor txPorCanal não é 512 e ajusta a variável verTx
            if (GlobVar.txPorCanal[indice] != 512)
            {
                verTx = true;
                ponteiroDesenho = 512 / GlobVar.txPorCanal[indice];
                h = GlobVar.indice / ponteiroDesenho;
            }

            // Loop pelas amplitudes para encontrar a melhor escala
            for (int ampli = 0; ampli < GlobVar.Amplitude.Length; ampli++)
            {
                // Calcula a escala atual
                scala = (float)(GlobVar.Amplitude[ampli]) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(indice));

                bool escalaValida = true; // Flag para verificar se todos os valores estão dentro dos limites

                // Itera sobre os valores da matriz
                for (int j = 0; j < GlobVar.matrizCompleta.GetLength(1); j++)
                {
                    float value = 0;

                    value = (GlobVar.matrizCanal[GlobVar.grafSelected[indice], j] / scala) + loc;

                    // Se o valor está fora dos limites, a escala é inválida
                    if (value < areaMin || value > areaMaxima)
                    {
                        escalaValida = false;
                        break; // Não precisa continuar verificando se um valor já está fora dos limites
                    }
                }

                // Se todos os valores estão dentro dos limites, retorna a escala encontrada
                if (escalaValida)
                {
                    return scala;
                }
            }

            // Se nenhuma escala válida for encontrada, retorna um valor padrão ou a última escala
            return scala;
        }

        public static void TracejadoLinhaZero(OpenGL gl, int qtdGraf)
        {
            for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Begin(OpenGL.GL_LINES);
                gl.Color(0.752941f, 0.752941f, 0.752941f);
                gl.Vertex(1, Plotagem.traco[i]);
                gl.Vertex(GlobVar.matrizCanal.GetLength(1), Plotagem.traco[i]);
                gl.End();
            }
            gl.Disable(OpenGL.GL_LINE_STIPPLE);

        }
        public static float[] ObterComponentesRGB(int formatoRGB)
        {
            string hexa = formatoRGB.ToString("X");
            int hexValue = int.Parse(hexa, System.Globalization.NumberStyles.HexNumber);

            float[] colorRGB = new float[3];
            int red = (hexValue >> 16) & 0xFF;
            int green = (hexValue >> 8) & 0xFF;
            int blue = hexValue & 0xFF;
            float a = 1.0f; // Fully opaque

            // Normalizar os componentes RGB para o intervalo [0, 1]
            colorRGB[0] = blue / 255f;
            colorRGB[1] = green / 255f;
            colorRGB[2] = red / 255f;

            // Converter os valores para floats
                         string color = $"RGB({colorRGB[0]}, {colorRGB[1]}, {colorRGB[2]})";
            // Retornar os componentes RGB como um array de floats
            return colorRGB;
        }
        private static int NormalizeValue(int value, int posCima, int posBaixo, int posEsquerda, int posDireita, int areaMin, int areaMax)
        {
            // Calcular os limites de normalização com base nos quatro valores
            int minVal = Math.Min(Math.Min(posCima, posBaixo), Math.Min(posEsquerda, posDireita));
            int maxVal = Math.Max(Math.Max(posCima, posBaixo), Math.Max(posEsquerda, posDireita));

            // Verifica se os limites são válidos para evitar divisão por zero
            if (maxVal == minVal)
            {
                return areaMin; // Evita divisão por zero
            }

            // Aplica a normalização linear para ajustar o valor dentro do intervalo desejado
            double normalized = areaMin + ((double)(value - minVal) * (areaMax - areaMin) / (maxVal - minVal));

            // Converte para inteiro e retorna
            return (int)Math.Round(normalized);
        }
    }
}
