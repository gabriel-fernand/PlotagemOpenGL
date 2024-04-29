using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using SharpGL;
using SharpGL.WPF;
using System.Runtime.InteropServices;

using Size = System.Drawing.Size;
using Point = System.Drawing.Point;
using System.DirectoryServices.ActiveDirectory;
using System.Windows.Media.Media3D;

namespace PlotagemOpenGL
{
    public partial class Tela_Plotagem : Form
    {
        private OpenGL gl;
        Plotagem plotagem;

        private Size formOriginalSize;

        private Rectangle comando;
        private Rectangle exExam;
        private Rectangle telaGl;

        private Rectangle OGLS;

        public static Point? prevPosition = null;
        public static ToolTip tooltip = new ToolTip();

        public static int qtdGrafics;

        public static Vector3 camera;
        



        public Tela_Plotagem()
        {
            InitializeComponent();
            Leitura.LerArquivo();
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Resize += Tela_Plotagem_Resiz;
            formOriginalSize = this.Size;
            UpdateStyles();

            comando = new Rectangle(painelComando.Location, painelComando.Size);
            exExam = new Rectangle(painelExames.Location, painelExames.Size);
            telaGl = new Rectangle(painelTelaGl.Location, painelTelaGl.Size);
            OGLS = new Rectangle(openglControl1.Location, openglControl1.Size);

            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;
            camera.X = 0.0f;
            camera.Y = 0.0f;
            camera.Z = 1.0f;
            //Play.PerformClick();
            //InitializeGLControl();
            //this.WindowState = FormWindowState.Maximized;
            //var size = Screen.PrimaryScreen.WorkingArea.Size;
            //var glSize = openglControl1.Size;

            //ClientSize = new System.Drawing.Size(size.Width, size.Height);
            //this.MaximizeBox = false;
        }
        private void resize_Control(Control c, Rectangle r)
        {

            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        private void Tela_Plotagem_Resiz(object sender, EventArgs e)
        {
            resize_Control(painelComando, comando);
            resize_Control(painelExames, exExam);

            resize_Control(painelTelaGl, telaGl);
            resize_Control(openglControl1, OGLS);

            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) { }
        private void qtdGraficos_TextChanged(object sender, EventArgs e)
        {
            string texto = qtdGraficos.Text;
            int numero;

            if (int.TryParse(texto, out numero))
            {
                qtdGrafics = Convert.ToInt32(texto);
                if (qtdGrafics <= 0 || qtdGrafics > 20)
                {
                    System.Windows.MessageBox.Show("Por favor, digite um número válido entre 1 e 20.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Por favor, digite um número inteiro válido.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }

        }
        private void Play_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(qtdGraficos.Text))
            {
                System.Windows.MessageBox.Show("Por favor, informe a quantidade de graficos a serem mostradas.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {

                int alturaTela = (int)openglControl1.Height;

                this.gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(qtdGrafics, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0, 0, 1);

                hScrollBar1.Maximum = GlobVar.canalA.Length / 300;
                hScrollBar1.Refresh();
            }
        }

        private void Tela_Plotagem_Load(object sender, EventArgs e)
        {/*
            Thread.Sleep(1000);
            this.gl = openglControl1.OpenGL;
            plotagem = new Plotagem(gl);
            openglControl1.DoRender();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            plotagem.DesenhaGrafico();
            */
            openglControl1.Focus();

        }

        private void openglControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);
                Cursor.Clip = new Rectangle(this.Location, this.Size);
                tooltip.Show("X=" + (Cursor.Position.X) + ", Y=" + (Cursor.Position.Y), openglControl1, 500, 0);

                // sample code

            }
            catch (Exception ee)
            {
                string message = Convert.ToString(ee);
                System.Windows.MessageBox.Show(message);
            }
        }

        private void plusCanula_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaCanula += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);
        }
        private void minusCanula_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaCanula -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);
        }
        private void plusFluxo_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaFluxo += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void minusFluxo_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaFluxo -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void plusAbdomen_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaAbdomen += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void minusAbdomen_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaAbdomen -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void plusRonco_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaRonco += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void minusRonco_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaRonco -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void plusSatu_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaSatO2 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }
        private void minusSatu_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaSatO2 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);

        }

        private void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    camera.X += 200;
                    break;
                case Keys.D:
                    camera.X -= 200;
                    break;
            }

            openglControl1.DoRender();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(camera.X, 0, 1);
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            int deslocamento = (e.NewValue * 300);

            int alturaTela = (int)openglControl1.Height;
            this.gl = openglControl1.OpenGL;
            plotagem = new Plotagem(gl);
            openglControl1.DoRender();
            plotagem.Margem(qtdGrafics, alturaTela);
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(deslocamento, 0, 1);
            
        }
    }

    public class GlobVar
    {
        public static int[] canalA;
        public static Vector2 sizeOpenGl;

        public static float escalaCanula = 1.0f;
        public static float escalaFluxo = 1.0f;
        public static float escalaAbdomen = 1.0f;
        public static float escalaRonco = 1.0f;
        public static float escalaSatO2 = 1.0f;
    }

    public class Leitura
    {
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
                    GlobVar.canalA = new int[values.Length];
                    for (int i = 0; i < values.Length; i++)
                    {
                        GlobVar.canalA[i] = Convert.ToInt32(values[i]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ocorreu um erro ao ler o arquivo: {ex.Message}");
            }
        }
    }

    public class Plotagem
    {
        private OpenGL gl;
        private float[] margem;
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
                if (qtdGraf == 0) {
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
            gl.Ortho(0, 300, 0, GlobVar.sizeOpenGl.Y, -1, 1); // Define a projeção

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

            for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(0, margem[i]);
                gl.Vertex(GlobVar.sizeOpenGl.X, margem[i]);
                gl.End();
            }

            gl.Disable(OpenGL.GL_LINE_STIPPLE);
            switch (qtdGraf)
            {
                case 1:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, ((float)GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 2:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 3:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 4:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
                case 5:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                               //aqui tem plotar 3 graficos diferentes
                                                                               //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = 0; j < GlobVar.canalA.Length; j++)
                    {
                        gl.Vertex(j + 5, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;

            }

            gl.Flush();

            //System.Windows.MessageBox.Show("Tamanho da janela openGl " + Tela_Plotagem.openglControl1.Height + " x " + Tela_Plotagem.openglControl1.Width);
            
        }
    }
}
