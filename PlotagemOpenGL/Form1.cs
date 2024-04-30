using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using SharpGL;
using System.Runtime.InteropServices;
using Size = System.Drawing.Size;
using Point = System.Drawing.Point;
using System.Data;
using System.Text.RegularExpressions;
using static System.Runtime.CompilerServices.RuntimeHelpers;

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

        [System.Runtime.InteropServices.DllImport("nvapi.dll", EntryPoint = "fake")]
        static extern int LoadNvApi64();

        [System.Runtime.InteropServices.DllImport("nvapi.dll", EntryPoint = "fake")]
        static extern int LoadNvApi32();


        private void InitializeDedicatedGraphics()
        {
            try
            {
                if (Environment.Is64BitProcess)
                    LoadNvApi64();
                else
                    LoadNvApi32();
            }
            catch { } // will always fail since 'fake' entry point doesn't exists
        }


        public Tela_Plotagem()
        {
            InitializeComponent();
            InitializeDedicatedGraphics();

            Leitura.LerArquivo();
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Resize += Tela_Plotagem_Resiz;
            formOriginalSize = this.Size;
            UpdateStyles();
            qtdGraficos.Text = "1";
            comando = new Rectangle(painelComando.Location, painelComando.Size);
            exExam = new Rectangle(painelExames.Location, painelExames.Size);
            telaGl = new Rectangle(painelTelaGl.Location, painelTelaGl.Size);
            OGLS = new Rectangle(openglControl1.Location, openglControl1.Size);
            openglControl1.Focus();
            //openglControl1.KeyDown += TelaPlotagem_KeyDown;

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
                if (qtdGrafics <= 0 || qtdGrafics > 23)
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

                hScrollBar1.Maximum = (GlobVar.canalA.Length);
                hScrollBar1.Refresh();
                UpdateInicioTela();
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
            //openglControl1.KeyDown += TelaPlotagem_KeyDown;
            //hScrollBar1.Focus();
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

        private bool isScrollingRight = true;


        private void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    camera.X += GlobVar.saltoTelas * GlobVar.SPEED;
                    //if (camera.X > 0 ) hScrollBar1.Value += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    //UpdateInicioTela();
                    break;
                case Keys.A:
                    camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;
                    //if (camera.X > 0) hScrollBar1.Value -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    //UpdateInicioTela();
                    break;
            }
            int alturaTela = (int)openglControl1.Height;
            openglControl1.DoRender();
            plotagem.Margem(qtdGrafics, alturaTela);
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(camera.X, 0, 1); 
            UpdateInicioTela();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            /*bool isRight = e.NewValue > hScrollBar1.Value;
            if (!isRight) // Se estiver indo para a esquerda
            {
                camera.X -= e.NewValue;

                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
            }
            else // Se estiver indo para a direita
            {
                camera.X += e.NewValue;

                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
            }
            int alturaTela = (int)openglControl1.Height;
            openglControl1.DoRender();
            plotagem.Margem(qtdGrafics, alturaTela);
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(camera.X, 0, 1);*/

        }

        private void tempoEmTela_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmpTela = tempoEmTela.Text;
            Match match = Regex.Match(tmpTela, @"\d+");
            if (match.Success)
            {
                GlobVar.segundos = int.Parse(match.Value);
            }

            GlobVar.tmpEmTela = GlobVar.namos * GlobVar.segundos;
            GlobVar.saltoTelas = GlobVar.tmpEmTela - 50;
            GlobVar.inicioTela = 0;
            GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
        }

        private void velocidadeScroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            string velo = velocidadeScroll.Text;
            switch (velo)
            {
                case "1.0x":
                    GlobVar.SPEED = 1;
                    break;
                case "1.5x":
                    GlobVar.SPEED = 1.5f;
                    break;
                case "2.0x":
                    GlobVar.SPEED = 2;
                    break;
                case "2.5x":
                    GlobVar.SPEED = 2.5f;
                    break;
                case "5.0x":
                    GlobVar.SPEED = 5;
                    break;
            }
        }

        private void UpdateInicioTela()
        {
            int inicio = GlobVar.inicioTela;
            TimeSpan tempo = TimeSpan.FromSeconds(inicio);
            string horasI = tempo.Hours.ToString().PadLeft(2, '0');
            string minutosI = tempo.Minutes.ToString().PadLeft(2, '0');
            string segundosI = tempo.Seconds.ToString().PadLeft(2, '0');

            inicioTela.Text = $"{horasI}:{minutosI}:{segundosI}";

            int final = GlobVar.finalTela;
            TimeSpan tempoF = TimeSpan.FromSeconds(final);

            string horasF = tempoF.Hours.ToString().PadLeft(2, '0');
            string minutosF = tempoF.Minutes.ToString().PadLeft(2, '0');
            string segundosF = tempoF.Seconds.ToString().PadLeft(2, '0');
            fimTela.Text = $"{horasF}:{minutosF}:{segundosF}";
        }
    }

    public class GlobVar
    {
        public static int[] canalA;
        public static int[] canalB;
        public static int[] canalC;
        public static int[] canalD;
        public static int[] canalE;
        public static int[] canalF;
        public static int[] canalG;
        public static int[] canalH;
        public static int[] canalI;
        public static int[] canalJ;
        public static int[] canalK;
        public static int[] canalL;
        public static int[] canalM;
        public static int[] canalN;
        public static int[] canalO;
        public static int[] canalP;
        public static int[] canalQ;
        public static int[] canalR;
        public static int[] canalS;
        public static int[] canalT;
        public static int[] canalU;
        public static int[] canalV;
        public static int[] canalW;


        public static Vector2 sizeOpenGl;

        public static int maximaVect = 2000;
        public static int indice = 0;

        public static float saltoTelas;
        public static float SPEED = 1.0f;

        public static int namos = 8;
        public static int segundos = 30;
        public static int tmpEmTela = 240;

        public static int inicioTela;
        public static int finalTela;

        public static float escalaCanula = 1.0f;
        public static float escalaFluxo = 1.0f;
        public static float escalaAbdomen = 1.0f;
        public static float escalaRonco = 1.0f;
        public static float escalaSatO2 = 1.0f;
    }
    public class Optimus
    {
        [DllImport("nvapi.dll")]
        public static extern int NvAPI_Initialize();
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
                    GlobVar.canalB = new int[values.Length];
                    GlobVar.canalC = new int[values.Length];
                    GlobVar.canalD = new int[values.Length];
                    GlobVar.canalE = new int[values.Length];
                    GlobVar.canalF = new int[values.Length];
                    GlobVar.canalG = new int[values.Length];
                    GlobVar.canalH = new int[values.Length];
                    GlobVar.canalI = new int[values.Length];
                    GlobVar.canalJ = new int[values.Length];
                    GlobVar.canalK = new int[values.Length];
                    GlobVar.canalL = new int[values.Length];
                    GlobVar.canalM = new int[values.Length];
                    GlobVar.canalN = new int[values.Length];
                    GlobVar.canalO = new int[values.Length];
                    GlobVar.canalP = new int[values.Length];
                    GlobVar.canalQ = new int[values.Length];
                    GlobVar.canalR = new int[values.Length];
                    GlobVar.canalS = new int[values.Length];
                    GlobVar.canalT = new int[values.Length];
                    GlobVar.canalU = new int[values.Length];
                    GlobVar.canalV = new int[values.Length];
                    GlobVar.canalW = new int[values.Length];

                    for (int i = 0; i < values.Length; i++)
                    {
                        GlobVar.canalA[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalB[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalC[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalD[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalE[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalF[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalG[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalH[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalI[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalJ[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalK[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalL[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalM[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalN[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalO[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalP[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalQ[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalR[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalS[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalT[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalU[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalV[i] = Convert.ToInt32(values[i]);
                        GlobVar.canalW[i] = Convert.ToInt32(values[i]);
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

            for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);

                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(0, margem[i]);
                gl.Vertex(GlobVar.canalA.Length, margem[i]);
                gl.End();
            }
            gl.Disable(OpenGL.GL_LINE_STIPPLE);

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
                        if (j > 0 ) gl.Vertex(j, ((float)GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 2:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if(j > 0 ) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                   if(j > 0 )  gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 3:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 4:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 5:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;

            case 6:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 7:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 8:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 9:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 10:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 11:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 12:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 13:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 14:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 15:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                { 
                    gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 16:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 17:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 18:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaCanula) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 19:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalS[j] * GlobVar.escalaCanula) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaCanula) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 20:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j, (GlobVar.canalT[j] * GlobVar.escalaCanula) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaCanula) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaCanula) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 21:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalU[j] * GlobVar.escalaCanula) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalT[j] * GlobVar.escalaCanula) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaCanula) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaCanula) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
            case 22:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalV[j] * GlobVar.escalaCanula) + desenhoLoc[21]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalU[j] * GlobVar.escalaCanula) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalT[j] * GlobVar.escalaCanula) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaCanula) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaCanula) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); ; // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
            case 23:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalW[j] * GlobVar.escalaCanula) + desenhoLoc[22]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalV[j] * GlobVar.escalaCanula) + desenhoLoc[21]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalU[j] * GlobVar.escalaCanula) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalT[j] * GlobVar.escalaCanula) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaCanula) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaCanula) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaCanula) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaCanula) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaCanula) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaCanula) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalM[j] * GlobVar.escalaCanula) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaCanula) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalK[j] * GlobVar.escalaCanula) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalJ[j] * GlobVar.escalaCanula) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaCanula) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaCanula) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalG[j] * GlobVar.escalaCanula) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaCanula) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaCanula) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaFluxo) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaAbdomen) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaRonco) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaSatO2) + desenhoLoc[0]); // Define cada ponto do gráfico
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
