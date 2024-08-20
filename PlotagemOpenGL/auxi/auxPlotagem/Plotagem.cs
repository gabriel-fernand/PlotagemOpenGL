using SharpGL;
using SharpGL.SceneGraph.Assets;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;


namespace PlotagemOpenGL.auxi.auxPlotagem
{
    public class Plotagem
    {
        private OpenGL gl;
        private string[] Canula;
        private float[] margem;
        public static float[] traco;
        public static float startX, startY, endX, endY;
        public float writeX = -500;
        public float writeY = -500;
        public static float[] StartY;
        public static float[] EndY;



        public Plotagem(OpenGL gl)
        {
            this.gl = gl;
        }
        public void Margem(int qtdGraf, int altura)
        {
            if (qtdGraf == 1)
            {
                margem = new float[qtdGraf];
                margem[0] = 0;
                if (qtdGraf == 0)
                {
                    float[] nada = new float[qtdGraf];
                    margem = nada;
                }
            }
            else
            {
                margem = new float[qtdGraf];
                float loc = altura / (float)qtdGraf;
                float aux = loc;

                for (int i = 0; i < qtdGraf; i++)
                {

                    margem[i] = aux;
                    aux += loc;
                }
            }

        }
        public void Traco(int qtdGraf, int altura)
        {
            if (qtdGraf == 1)
            {
                traco = new float[qtdGraf];
                traco[0] = altura / 2;
                if (qtdGraf == 0)
                {
                    float[] nada = new float[qtdGraf];
                    traco = nada;
                }
            }
            else
            {
                float loc = altura / (float)qtdGraf / 2;
                float aux = loc;
                traco = new float[qtdGraf];

                for (int i = 0; i < qtdGraf; i++)
                {
                    traco[i] = aux;
                    aux += loc * 2;
                }
            }
        }



        public void DesenhaGrafico(int altura, int qtdGraf)
        {
            Tela_Plotagem.plotanu = true;
            Canula = new string[] { "Apneia", "Apneia central", "Hipopneia", "Dessaturacao", "Hera", "Ronco" };
            float loc = Canais.pnSizes[1] / 2;
            float aux = loc;
            float[] desenhoLoc = new float[qtdGraf];
            GlobVar.desenhoLoc = new float[desenhoLoc.Length];
            GlobVar.desenhoLoc = desenhoLoc;
            float locY = altura / (float)qtdGraf;
            float auxY = 0;
            GlobVar.StartY = new float[qtdGraf];
            GlobVar.EndY = new float[qtdGraf];
            for (int i = 0; i < qtdGraf; i++)
            {
                desenhoLoc[i] = aux;
                aux += Canais.pnSizes[i + 1] - Canais.pnSizes[i];
            }
            for (int i = 0; i < GlobVar.StartY.Length; i++)
            {
                GlobVar.StartY[i] = Canais.pnSizes[i];
                auxY += locY;
                GlobVar.EndY[i] = Canais.pnSizes[i + 1];
            }

            // Supondo que GlobVar.FundoColor[3] seja uma instância da struct Color (ou similar) com propriedades R, G, B.
            float red = GlobVar.FundoColor[0] / 255.0f;
            float green = GlobVar.FundoColor[1] / 255.0f;
            float blue = GlobVar.FundoColor[2] / 255.0f;
            float alpha = GlobVar.FundoColor[3] / 255.0f;

            // Defina a cor de fundo com os valores RGB normalizados
            gl.ClearColor(red, green, blue, alpha); // A última variável é o alpha (opacidade), 1.0f para opaco

            // Continue com as configurações normais do OpenGL
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Viewport(0, 0, (int)GlobVar.sizeOpenGl.X, (int)GlobVar.sizeOpenGl.Y);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(0, GlobVar.tmpEmTela, 0, GlobVar.sizeOpenGl.Y, -2, 2);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
            gl.PointSize(3.0f);
            gl.Color(0.0f, 0.0f, 0.0f);
            gl.Scale(1, 1, 1);



            /*for (int i = 0; i < qtdGraf; i++)
            {
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(0, margem[i]);
                gl.Vertex(GlobVar.canalA.Length, margem[i]);
                gl.End();
            }*/

            if (Tela_Plotagem.Linha1Seg.Checked)
            {
                int ind = 0;
                for (int i = GlobVar.indice; i < GlobVar.maximaVect;)
                {
                    gl.Color(0.0f, 0.0f, 0.0f); // Define a cor das linhas (preto)

                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(i, 0);
                    gl.Vertex(i, GlobVar.sizeOpenGl.Y);
                    gl.End();

                    i += GlobVar.namos;
                    ind++;
                }
            }
            if(Tela_Plotagem.Pontilhado200Mili.Checked)
            {
                float mili200seg = 102.4f;
                for (float i = GlobVar.indice; i < GlobVar.maximaVect;)
                {
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(i, 0);
                    gl.Vertex(i, GlobVar.sizeOpenGl.Y);
                    gl.End();
                    i += mili200seg;
                }
            }
            if (Tela_Plotagem.LinhaZeroCanais.Checked) 
            {

                int des = qtdGraf - 1;
                for (int i = 0; i < qtdGraf; i++)
                {
                    gl.Color(127, 125, 125);
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(GlobVar.indice, GlobVar.desenhoLoc[des]);
                    gl.Vertex(GlobVar.maximaVect, GlobVar.desenhoLoc[des]);
                    gl.End();
                    des--;
                }

            }
            //Faz o desenho da regua com base no tamanho da tela
            if (Tela_Plotagem.Regua.Checked){
                int ind = 0;
                for (int i = GlobVar.indice; i < GlobVar.maximaVect;)
                {
                    if (ind % 10 == 0)
                    {
                        gl.Begin(OpenGL.GL_LINE_STRIP);
                        gl.Vertex(i, 0);
                        gl.Vertex(i, 10);
                        gl.End();

                        gl.Begin(OpenGL.GL_LINE_STRIP);
                        gl.Vertex(i, GlobVar.sizeOpenGl.Y);
                        gl.Vertex(i, GlobVar.sizeOpenGl.Y - 10);
                        gl.End();
                    }
                    else
                    {
                        gl.Begin(OpenGL.GL_LINE_STRIP);
                        gl.Vertex(i, 0);
                        gl.Vertex(i, 5);
                        gl.End();

                        gl.Begin(OpenGL.GL_LINE_STRIP);
                        gl.Vertex(i, GlobVar.sizeOpenGl.Y);
                        gl.Vertex(i, GlobVar.sizeOpenGl.Y - 5);
                        gl.End();
                    }

                    i += GlobVar.namos;
                    ind++;
                }
                float mili200seg = 102.4f;
                for (float i = GlobVar.indice; i < GlobVar.maximaVect;)
                {
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(i, 0);
                    gl.Vertex(i, 3);
                    gl.End(); 

                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(i, GlobVar.sizeOpenGl.Y);
                    gl.Vertex(i, GlobVar.sizeOpenGl.Y - 3);
                    gl.End();
                    i += mili200seg;
                }
                int des = qtdGraf - 1;
                for (int i = 0; i < qtdGraf; i++)
                {
                    gl.Color(127, 125, 125);
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(GlobVar.indice, GlobVar.desenhoLoc[des]);
                    gl.Vertex(GlobVar.indice + 150, GlobVar.desenhoLoc[des]);
                    gl.End();
                    des--;
                }

            }

            //classe para fazer o desenho do grafico
            //plotGrafico.DesenhaGrafico(qtdGraf, gl, desenhoLoc);

            //plotEventos.DesenhaEventos(qtdGraf, gl, desenhoLoc);


            plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, desenhoLoc);



            //Metodo para fazer o desenho da linha x0 de cada grafico
            //plotGrafico.TracejadoLinhaZero(gl, qtdGraf);
            int YAdjusted = EncontrarValorMaisProximo(desenhoLoc, startY);

            /*if (Tela_Plotagem.isDrawingRectangle)
            {
                gl.Color(0.0f, 0.0f, 0.0f);

                gl.Begin(OpenGL.GL_QUADS);
                gl.PointSize(3.0f); // Define o tamanho dos pontos
                gl.Color(GlobVar.colors[YAdjusted].X, GlobVar.colors[YAdjusted].Y, GlobVar.colors[YAdjusted].Z, 0.001);
                //gl.ColorMask(3, 6, 7, alpha);
                gl.Vertex(startX, GlobVar.StartY[YAdjusted] + 5, -1.5f);
                gl.Vertex(endX, GlobVar.StartY[YAdjusted] + 5, -1.5f);
                gl.Vertex(endX, GlobVar.EndY[YAdjusted] - 5, -1.5f);
                gl.Vertex(startX, GlobVar.EndY[YAdjusted] - 5, -1.5f);
                gl.End();
                //startX = 0;
            }*/

            GlobVar.canal = YAdjusted;

            //plotEventos.AdicionarEventoAoDataTable((int)startX, (int)endX, YAdjusted, desenhoLoc, startY);

            //Classe para testes para fazer os numeros dos canais de respiracao e SP02
            //plotNumerico.PlotNumerico(qtdGraf, gl, desenhoLoc);

            double time = (endX - startX) / GlobVar.namos; //Faz o calculo para mostrar quantos segundos o quadrado de evento ta captando
            writeX = startX / GlobVar.tmpEmTela * GlobVar.sizeOpenGl.X + 5; //Faz o mapeamento para fazer a escrita no quadrado com base no tamanho da tela
            writeY = (int)GlobVar.EndY[YAdjusted] - 25;
            //gl.DrawText((int)writeX, (int)writeY, 0.0f, 0.0f, 0.0f, "Arial Narrow", 18, Canula[YAdjusted] + $" {time:F2} seg");



            gl.Flush();
            //System.Windows.MessageBox.Show("Tamanho da janela openGl " + Tela_Plotagem.openglControl1.Height + " x " + Tela_Plotagem.openglControl1.Width);
            Tela_Plotagem.plotanu = false;
        }
        public static int EncontrarValorMaisProximo(float[] valores, float y)
        {
            float valorMaisProximo = valores[0];
            float menorDiferenca = Math.Abs(valores[0] - y);

            foreach (float valor in valores)
            {
                float diferencaAtual = Math.Abs(valor - y);

                if (diferencaAtual < menorDiferenca)
                {
                    menorDiferenca = diferencaAtual;
                    valorMaisProximo = valor;
                }
            }
            int index = Array.IndexOf(valores, valorMaisProximo);
            return index;
        }


    }
}
