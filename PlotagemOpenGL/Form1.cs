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

namespace PlotagemOpenGL
{
    public partial class Tela_Plotagem : Form
    {
        private OpenGL gl;
        private GraphState state = new GraphState();
        Plotagem plotagem;

        private Size formOriginalSize;

        private Rectangle comando;
        private Rectangle exExam;
        private Rectangle telaGl;

        private Rectangle OGLS;

        public static Point? prevPosition = null;
        public static ToolTip tooltip = new ToolTip();


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
        private void qtdGraficos_TextChanged(object sender, EventArgs e) { }
        private void Play_Click(object sender, EventArgs e)
        {
            int qtd = 3;
            if (!String.IsNullOrEmpty(qtdGraficos.Text))
            {
                qtd = Convert.ToInt32(qtdGraficos.Text);
            }
            int alturaTela = (int)openglControl1.Height;
            Border border = new Border(Color.Blue, 5f);

            this.gl = openglControl1.OpenGL;
            plotagem = new Plotagem(gl);
            openglControl1.DoRender();

            plotagem.DesenhaGrafico(alturaTela, qtd);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(150, 0, 1);
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
    }

    public class GlobVar
    {
        public static int[] canalA;
        public static Vector2 sizeOpenGl;
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
    public class GraphState
    {
        public float yScale { get; set; } = 0.1f;
    }
    public class Border
    {
        public Color Color { get; set; }
        public float Thickness { get; set; }

        public Border(Color color, float thickness)
        {
            Color = color;
            Thickness = thickness;
        }
    }

    public class Plotagem
    {
        private OpenGL gl;
        private GraphState state = new GraphState();
        public Border Border { get; set; }
        public Plotagem(OpenGL gl)
        {
            this.gl = gl;
        }
        public Plotagem(OpenGL gl, Border border)
        {
            this.gl = gl;
            Border = border;
        }
        public void DesenhaGrafico(int altura, int qtdGraf)
        {

            int top = altura;
            int x = (top / qtdGraf);
            int qtd = x / 2;
            int loc = altura - qtd;

            int[] vect = new int[3] { 0, 270, 540 };

            gl.Viewport(0, 0, (int)GlobVar.sizeOpenGl.X, (int)GlobVar.sizeOpenGl.Y);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            //gl.Perspective(0, 0, 0.1, 50.0);
            //gl.LookAt(0, 0, 0, 0, 0, 0, 0, 0, 0);
            gl.Ortho(0, 300, 0, GlobVar.sizeOpenGl.Y, -1, 1); // Define a projeção


            gl.ClearColor(1, 1, 1, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.PointSize(3.0f); // Define o tamanho dos pontos
            gl.Color(0.0f, 0.0f, 0.0f); // Define a cor das linhas (preto)
            gl.Scale(1, 1, 1);// state.yScale, 1);
            // Define a primeira projeção ortográfica para o primeiro conjunto de pontos (canalA)            


            gl.LineStipple(1, 0xAAAA);
            gl.Enable(OpenGL.GL_LINE_STIPPLE);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(0,670);
            gl.Vertex(GlobVar.sizeOpenGl.X, 670);
            gl.End();

            gl.LineStipple(1, 0xAAAA);
            gl.Enable(OpenGL.GL_LINE_STIPPLE);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(0, 400);
            gl.Vertex(GlobVar.sizeOpenGl.X, 400);
            gl.End();

            gl.LineStipple(1, 0xAAAA);
            gl.Enable(OpenGL.GL_LINE_STIPPLE);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(0, 135);
            gl.Vertex(GlobVar.sizeOpenGl.X, 135);
            gl.End();



            gl.Disable(OpenGL.GL_LINE_STIPPLE);
            gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
            for (int j = 0; j < 300; j++)
            {
                gl.Vertex(j+5, GlobVar.canalA[j] + 670); // Define cada ponto do gráfico
                                                           //aqui tem plotar 3 graficos diferentes
                                                           //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
            }
            gl.End();
            gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
            for (int j = 0; j < 300; j++)
            {
                gl.Vertex(j+5, GlobVar.canalA[j] + 400 ); // Define cada ponto do gráfico
                                                     //aqui tem plotar 3 graficos diferentes
                                                      //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
            }
            gl.End();
            gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
            for (int j = 0; j < 300; j++)
            {
                gl.Vertex(j+5, GlobVar.canalA[j] + 135); // Define cada ponto do gráfico
                                                     //aqui tem plotar 3 graficos diferentes
                                                     //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
            }
            gl.End();
            
            gl.Flush();

            //System.Windows.MessageBox.Show("Tamanho da janela openGl " + Tela_Plotagem.openglControl1.Height + " x " + Tela_Plotagem.openglControl1.Width);
            
        }
    }
}
