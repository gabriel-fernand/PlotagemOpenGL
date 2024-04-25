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
        }

        private void porcentagemSizes()
        {
            var size = Screen.PrimaryScreen.WorkingArea.Size;
            var glSize = openglControl1.Size;
            float[] sizes = new float[2];

            sizes[0] = ((float)glSize.Width / (float)size.Width) * 100;
            sizes[1] = ((float)glSize.Height / (float)size.Height) * 100;
        }
        private void InitializeGLControl()
        {
            openglControl1.Load += OpenglControl1_Load;
            openglControl1.Resize += OpenglControl1_Resize;
            openglControl1.Paint += OpenglControl1_Paint1;
        }
        private void OpenglControl1_Paint1(object sender, PaintEventArgs e)
        {
            OpenGL gl = openglControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //Plotagem plotagem = new Plotagem();
            Camera camera = new Camera();
            camera.InitCamera();
            //plotagem.DesenhaGrafico();
        }
        private void OpenglControl1_Load(object sender, EventArgs e)
        {
            gl = openglControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.ClearColor(1.0f, 1.0f, 1.0f, 1.0f);

            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }
        private void OpenglControl1_Resize(object sender, EventArgs e)
        {
            gl.Viewport(0, 0, openglControl1.Width, openglControl1.Height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void qtdGraficos_TextChanged(object sender, EventArgs e)
        {
        }

        private void Play_Click(object sender, EventArgs e)
        {
            int qtd = Convert.ToInt32(qtdGraficos.Text);
            int alturaTela = (int)this.Height;

            this.gl = openglControl1.OpenGL;
            plotagem = new Plotagem(gl);
            openglControl1.DoRender();
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            plotagem.DesenhaGrafico(alturaTela,qtd);

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

        private void Tela_Plotagem_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        
    }

    public class GlobVar
    {
        public static int[] canalA;
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

    public class Camera
    {
        private static float eyeX, eyeY, eyeZ;
        private static float centerX, centerY, centerZ;
        OpenGL gl = new OpenGL();

        public void InitCamera()
        {
            eyeX = 0f;
            eyeY = 0f;
            eyeZ = 0f;
            centerX = 0;
            centerY = 0;
            centerZ = 0;
            Look();
        }

        public void Look()
        {
            gl = Tela_Plotagem.openglControl1.OpenGL;
            gl.Translate(0, 0, 0);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(150, 0, 0);
        }

    }
    public class GraphState
    {
        public float yScale { get; set; } = 0.1f;
    }

    public class Plotagem
    {
        private OpenGL gl;
        private GraphState state = new GraphState();
        public Plotagem(OpenGL gl)
        {
            this.gl = gl;
        }
        public Plotagem(OpenGL gl, GraphState initalState)
        {
            this.gl = gl;
            this.state = initalState;

        }
        public void DesenhaGrafico(int altura, int qtdGraf)
        {
            int top = altura/2;
            int x = (altura / qtdGraf);
            int qtd = x * 2;
            int loc = altura - qtd;

            gl.ClearColor(1, 1, 1, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.PointSize(3.0f); // Define o tamanho dos pontos
            gl.Color(0.0f, 0.0f, 0.0f); // Define a cor das linhas (preto)
            gl.Viewport(0, 0, Tela_Plotagem.openglControl1.Width, altura);
            gl.Scale(1, state.yScale, 1);

            
            for (int i = 0; i < qtdGraf; i++)
            {
                // Define a primeira projeção ortográfica para o primeiro conjunto de pontos (canalA)
                gl.MatrixMode(OpenGL.GL_PROJECTION);
                gl.LoadIdentity();
                gl.Ortho(0, 5600, -top, top, -1, 1); // Define a projeção
                                                                      // 
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = 0; j < 5600; j++)
                {
                    gl.Vertex(j, GlobVar.canalA[j] + loc); // Define cada ponto do gráfico
                }
                gl.End(); // Finaliza o desenho da linha
                loc -= x;
            }
            gl.End();
            gl.Flush();

        }
        public void UpdateYScale(float newyScale)
        {
            state.yScale = newyScale;
        }
    }

}
