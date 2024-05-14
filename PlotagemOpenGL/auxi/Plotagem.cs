using SharpGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlotagemOpenGL.auxi
{
    public class Plotagem
    {
        private OpenGL gl;
        private float[] margem;
        private float[] traco;
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
                    this.margem = nada;
                }
            }
            else
            {
                margem = new float[qtdGraf];
                float loc = (float)altura / (float)qtdGraf;
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
                    this.traco = nada;
                }
            }
            else
            {
                float loc = ((float)altura / (float)qtdGraf) / 2;
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
            float loc = ((float)altura / (float)qtdGraf) / 2;
            float aux = loc;
            float[] desenhoLoc = new float[qtdGraf];

            for (int i = 0; i < qtdGraf; i++)
            {
                desenhoLoc[i] = aux;
                aux += margem[0];
            }


            gl.Viewport(0, 0, (int)GlobVar.sizeOpenGl.X, (int)GlobVar.sizeOpenGl.Y);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            //gl.Perspective(0, 0, 0.1, 50.0);
            //gl.LookAt(0, 0, 0, 0, 0, 0, 0, 0, 0);
            gl.Ortho(0, GlobVar.tmpEmTela, 0, GlobVar.sizeOpenGl.Y, -1, 1); // Define a projeção

            // Define a matriz de modelo-visualização
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity(); // Carrega a matriz identidade
            // Aplica transformações da câmera
            gl.Translate(-Tela_Plotagem.camera.X, 0, 1);

            gl.ClearColor(1, 1, 1, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.PointSize(3.0f); // Define o tamanho dos pontos
            gl.Color(0.0f, 0.0f, 0.0f); // Define a cor das linhas (preto)
            gl.Scale(1, 1, 1);// state.yScale, 1);
                              // Define a primeira projeção ortográfica para o primeiro conjunto de pontos (canalA)             




            /*for (int i = 0; i < qtdGraf; i++)
            {
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(0, margem[i]);
                gl.Vertex(GlobVar.canalA.Length, margem[i]);
                gl.End();
            }*/
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


            switch (qtdGraf)
            {
                case 1:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j <= 0 || j >= GlobVar.canalA.Length) gl.Vertex(j, desenhoLoc[0]); // Define cada ponto do gráfico
                        else gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[0]);                                                                                  //aqui tem plotar 3 graficos diferentes
                    }
                    gl.End();
                    break;
                case 2:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j <= 0 || j >= GlobVar.canalA.Length) gl.Vertex(j, desenhoLoc[1]); // Define cada ponto do gráfico
                        else gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[1]);                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 3:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 4:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 5:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;

                case 6:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 7:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 8:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 9:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 10:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 11:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 12:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 13:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 14:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 15:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 16:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 17:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 18:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                          //aqui tem plotar 3 graficos diferentes
                                                                                                          //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 19:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 20:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 21:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalU[j] * GlobVar.escalaLb21) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 22:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[21]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[11]); ; // Define cada ponto do gráfico
                                                                                                              //aqui tem plotar 3 graficos diferentes
                                                                                                              //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalU[j] * GlobVar.escalaLb21) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalV[j] * GlobVar.escalaLb22) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 23:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[22]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[21]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();

                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalU[j] * GlobVar.escalaLb21) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalV[j] * GlobVar.escalaLb22) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalW[j] * GlobVar.escalaLb23) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                           //aqui tem plotar 3 graficos diferentes
                                                                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
            }


            gl.Flush();
            for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.Begin(OpenGL.GL_LINES);
                gl.Color(0.752941f, 0.752941f, 0.752941f);
                gl.Vertex(1, traco[i]);
                gl.Vertex(GlobVar.canalA.Length, traco[i]);
                gl.End();
            }

            gl.Disable(OpenGL.GL_LINE_STIPPLE);




            //System.Windows.MessageBox.Show("Tamanho da janela openGl " + Tela_Plotagem.openglControl1.Height + " x " + Tela_Plotagem.openglControl1.Width);

        }
    }
}
