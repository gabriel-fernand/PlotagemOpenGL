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
using System.Text.RegularExpressions;
using PlotagemOpenGL.auxi;
using Accord.Audio.Filters;

namespace PlotagemOpenGL
{
    public partial class Tela_Plotagem : Form
    {
        private OpenGL gl;
        Plotagem plotagem;

        private Size formOriginalSize;
        private Size painelOriginalSize;
        private Size painelComandoOriginalSize;

        private Rectangle comando;
        private Rectangle exExam;
        private Rectangle telaGl;
        private Rectangle OGLS;

        private Rectangle ptTela;
        private Rectangle inTela;
        private Rectangle fnTela;
        private Rectangle tpTela;
        private Rectangle vlScroll;
        private Rectangle qtGraf;
        private Rectangle play;
        private Rectangle box3;
        private Rectangle slLabel;
        private Rectangle fil;
        private Rectangle playFil;

        private Rectangle lb1;
        private Rectangle lb2;
        private Rectangle lb3;
        private Rectangle lb4;
        private Rectangle lb5;
        private Rectangle lb6;
        private Rectangle lb7;
        private Rectangle lb8;
        private Rectangle lb9;
        private Rectangle lb10;
        private Rectangle lb11;
        private Rectangle lb12;
        private Rectangle lb13;
        private Rectangle lb14;
        private Rectangle lb15;
        private Rectangle lb16;
        private Rectangle lb17;
        private Rectangle lb18;
        private Rectangle lb19;
        private Rectangle lb20;
        private Rectangle lb21;
        private Rectangle lb22;
        private Rectangle lb23;
        private Rectangle btPlusLb1;
        private Rectangle btMinusLb1;
        private Rectangle btPlusLb2;
        private Rectangle btMinusLb2;
        private Rectangle btPlusLb3;
        private Rectangle btMinusLb3;
        private Rectangle btPlusLb4;
        private Rectangle btMinusLb4;
        private Rectangle btPlusLb5;
        private Rectangle btMinusLb5;
        private Rectangle btPlusLb6;
        private Rectangle btMinusLb6;
        private Rectangle btPlusLb7;
        private Rectangle btMinusLb7;
        private Rectangle btPlusLb8;
        private Rectangle btMinusLb8;
        private Rectangle btPlusLb9;
        private Rectangle btMinusLb9;
        private Rectangle btPlusLb10;
        private Rectangle btMinusLb10;
        private Rectangle btPlusLb11;
        private Rectangle btMinusLb11;
        private Rectangle btPlusLb12;
        private Rectangle btMinusLb12;
        private Rectangle btPlusLb13;
        private Rectangle btMinusLb13;
        private Rectangle btPlusLb14;
        private Rectangle btMinusLb14;
        private Rectangle btPlusLb15;
        private Rectangle btMinusLb15;
        private Rectangle btPlusLb16;
        private Rectangle btMinusLb16;
        private Rectangle btPlusLb17;
        private Rectangle btMinusLb17;
        private Rectangle btPlusLb18;
        private Rectangle btMinusLb18;
        private Rectangle btPlusLb19;
        private Rectangle btMinusLb19;
        private Rectangle btPlusLb20;
        private Rectangle btMinusLb20;
        private Rectangle btPlusLb21;
        private Rectangle btMinusLb21;
        private Rectangle btPlusLb22;
        private Rectangle btMinusLb22;
        private Rectangle btPlusLb23;
        private Rectangle btMinusLb23;

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
            Canais.LerCanais();
            Leitura.LerArquivo();
            SetStyle(ControlStyles.DoubleBuffer, true);
            this.Resize += Tela_Plotagem_Resiz;
            this.Resize += painelComando_Resiz;
            this.Resize += Painel_resiz;
            formOriginalSize = this.Size;
            painelOriginalSize = painelExames.Size;
            painelComandoOriginalSize = painelComando.Size;
            UpdateStyles();
            qtdGraficos.Text = "1";

            comando = new Rectangle(painelComando.Location, painelComando.Size);
            exExam = new Rectangle(painelExames.Location, painelExames.Size);
            telaGl = new Rectangle(painelTelaGl.Location, painelTelaGl.Size);
            OGLS = new Rectangle(openglControl1.Location, openglControl1.Size);

            ptTela = new Rectangle(ptsEmTela.Location, ptsEmTela.Size);
            inTela = new Rectangle(inicioTela.Location, inicioTela.Size);
            fnTela = new Rectangle(fimTela.Location, fimTela.Size);
            tpTela = new Rectangle(tempoEmTela.Location, tempoEmTela.Size);
            vlScroll = new Rectangle(velocidadeScroll.Location, velocidadeScroll.Size);
            qtGraf = new Rectangle(qtdGraficos.Location, qtdGraficos.Size);
            play = new Rectangle(Play.Location, Play.Size);
            box3 = new Rectangle(comboBox3.Location, comboBox3.Size);
            slLabel = new Rectangle(selectLabel.Location, selectLabel.Size);
            fil = new Rectangle(Filters.Location, Filters.Size);
            playFil = new Rectangle(playFilter.Location, playFilter.Size);

            lb1 = new Rectangle(label1.Location, label1.Size);
            btPlusLb1 = new Rectangle(plusLb1.Location, plusLb1.Size);
            btMinusLb1 = new Rectangle(minusLb1.Location, minusLb1.Size);
            lb2 = new Rectangle(label2.Location, label2.Size);
            btPlusLb2 = new Rectangle(plusLb2.Location, plusLb2.Size);
            btMinusLb2 = new Rectangle(minusLb2.Location, minusLb2.Size);
            lb3 = new Rectangle(label4.Location, label4.Size);
            btPlusLb3 = new Rectangle(plusLb4.Location, plusLb4.Size);
            btMinusLb3 = new Rectangle(minusLb4.Location, minusLb4.Size);
            lb4 = new Rectangle(label3.Location, label3.Size);
            btPlusLb4 = new Rectangle(plusLb3.Location, plusLb3.Size);
            btMinusLb4 = new Rectangle(minusLb3.Location, minusLb3.Size);
            lb5 = new Rectangle(label5.Location, label5.Size);
            btPlusLb5 = new Rectangle(plusLb5.Location, plusLb5.Size);
            btMinusLb5 = new Rectangle(minusLb5.Location, minusLb5.Size);
            lb6 = new Rectangle(label6.Location, label6.Size);
            lb7 = new Rectangle(label7.Location, label7.Size);
            lb8 = new Rectangle(label8.Location, label8.Size);
            lb9 = new Rectangle(label9.Location, label9.Size);
            lb10 = new Rectangle(label10.Location, label10.Size);
            lb11 = new Rectangle(label11.Location, label11.Size);
            lb12 = new Rectangle(label12.Location, label12.Size);
            lb13 = new Rectangle(label13.Location, label13.Size);
            lb14 = new Rectangle(label14.Location, label14.Size);
            lb15 = new Rectangle(label15.Location, label15.Size);
            lb16 = new Rectangle(label16.Location, label16.Size);
            lb17 = new Rectangle(label17.Location, label17.Size);
            lb18 = new Rectangle(label18.Location, label18.Size);
            lb19 = new Rectangle(label19.Location, label19.Size);
            lb20 = new Rectangle(label20.Location, label20.Size);
            lb21 = new Rectangle(label21.Location, label21.Size);
            lb22 = new Rectangle(label22.Location, label22.Size);
            lb23 = new Rectangle(label23.Location, label23.Size);

            btPlusLb6 = new Rectangle(plusLb6.Location, plusLb6.Size);
            btPlusLb7 = new Rectangle(plusLb7.Location, plusLb7.Size);
            btPlusLb8 = new Rectangle(plusLb8.Location, plusLb8.Size);
            btPlusLb9 = new Rectangle(plusLb9.Location, plusLb9.Size);
            btPlusLb10 = new Rectangle(plusLb10.Location, plusLb10.Size);
            btPlusLb11 = new Rectangle(plusLb11.Location, plusLb11.Size);
            btPlusLb12 = new Rectangle(plusLb12.Location, plusLb12.Size);
            btPlusLb13 = new Rectangle(plusLb13.Location, plusLb13.Size);
            btPlusLb14 = new Rectangle(plusLb14.Location, plusLb14.Size);
            btPlusLb15 = new Rectangle(plusLb15.Location, plusLb15.Size);
            btPlusLb16 = new Rectangle(plusLb16.Location, plusLb16.Size);
            btPlusLb17 = new Rectangle(plusLb17.Location, plusLb17.Size);
            btPlusLb18 = new Rectangle(plusLb18.Location, plusLb18.Size);
            btPlusLb19 = new Rectangle(plusLb19.Location, plusLb19.Size);
            btPlusLb20 = new Rectangle(plusLb20.Location, plusLb20.Size);
            btPlusLb21 = new Rectangle(plusLb21.Location, plusLb21.Size);
            btPlusLb22 = new Rectangle(plusLb22.Location, plusLb22.Size);
            btPlusLb23 = new Rectangle(plusLb23.Location, plusLb23.Size);

            btMinusLb6 = new Rectangle(minusLb6.Location, minusLb6.Size);
            btMinusLb7 = new Rectangle(minusLb7.Location, minusLb7.Size);
            btMinusLb8 = new Rectangle(minusLb8.Location, minusLb8.Size);
            btMinusLb9 = new Rectangle(minusLb9.Location, minusLb9.Size);
            btMinusLb10 = new Rectangle(minusLb10.Location, minusLb10.Size);
            btMinusLb11 = new Rectangle(minusLb11.Location, minusLb11.Size);
            btMinusLb12 = new Rectangle(minusLb12.Location, minusLb12.Size);
            btMinusLb13 = new Rectangle(minusLb13.Location, minusLb13.Size);
            btMinusLb14 = new Rectangle(minusLb14.Location, minusLb14.Size);
            btMinusLb15 = new Rectangle(minusLb15.Location, minusLb15.Size);
            btMinusLb16 = new Rectangle(minusLb16.Location, minusLb16.Size);
            btMinusLb17 = new Rectangle(minusLb17.Location, minusLb17.Size);
            btMinusLb18 = new Rectangle(minusLb18.Location, minusLb18.Size);
            btMinusLb19 = new Rectangle(minusLb19.Location, minusLb19.Size);
            btMinusLb20 = new Rectangle(minusLb20.Location, minusLb20.Size);
            btMinusLb21 = new Rectangle(minusLb21.Location, minusLb21.Size);
            btMinusLb22 = new Rectangle(minusLb22.Location, minusLb22.Size);
            btMinusLb23 = new Rectangle(minusLb23.Location, minusLb23.Size);

            openglControl1.Focus();

            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;
            GlobVar.sizePainelExams.X = painelExames.Width;
            GlobVar.sizePainelExams.Y = painelExames.Height;

            camera.X = 0.0f;
            camera.Y = 0.0f;
            camera.Z = 1.0f;
            tempoEmTela.SelectedIndex = 2;
            velocidadeScroll.SelectedIndex = 0;
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
        private void painel_Resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(painelExames.Width) / (float)(painelOriginalSize.Width);
            float yRatio = (float)(painelExames.Height) / (float)(painelOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        private void painelComando_Resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(painelComando.Width) / (float)(painelComandoOriginalSize.Width);
            float yRatio = (float)(painelComando.Height) / (float)(painelComandoOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Location = new Point(newX, newY);
            //c.Size = new Size(newWidth, newHeight);
        }
        private void painelComando_Resiz(object sender, EventArgs e)
        {
            painelComando_Resize_Control(ptsEmTela, ptTela);
            painelComando_Resize_Control(inicioTela, inTela);
            painelComando_Resize_Control(fimTela, fnTela);
            painelComando_Resize_Control(tempoEmTela, tpTela);
            painelComando_Resize_Control(velocidadeScroll, vlScroll);
            painelComando_Resize_Control(qtdGraficos, qtGraf);
            painelComando_Resize_Control(Play, play);
            painelComando_Resize_Control(comboBox3, box3);
            painelComando_Resize_Control(selectLabel, slLabel);
            painelComando_Resize_Control(Filters, fil);
            painelComando_Resize_Control(playFilter, playFil);
        }

        private void Tela_Plotagem_Resiz(object sender, EventArgs e)
        {
            resize_Control(painelComando, comando);
            resize_Control(painelExames, exExam);

            resize_Control(painelTelaGl, telaGl);
            resize_Control(openglControl1, OGLS);

            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;

            GlobVar.sizePainelExams.X = painelExames.Width;
            GlobVar.sizePainelExams.Y = painelExames.Height;
        }
        private void Painel_resiz(object sender, EventArgs e)
        {

            painel_Resize_Control(label1, lb1);
            painel_Resize_Control(label2, lb2);
            painel_Resize_Control(label3, lb3);
            painel_Resize_Control(label4, lb4);
            painel_Resize_Control(label5, lb5);
            painel_Resize_Control(label6, lb6);
            painel_Resize_Control(label7, lb7);
            painel_Resize_Control(label8, lb8);
            painel_Resize_Control(label9, lb9);
            painel_Resize_Control(label10, lb10);
            painel_Resize_Control(label11, lb11);
            painel_Resize_Control(label12, lb12);
            painel_Resize_Control(label13, lb13);
            painel_Resize_Control(label14, lb14);
            painel_Resize_Control(label15, lb15);
            painel_Resize_Control(label16, lb16);
            painel_Resize_Control(label17, lb17);
            painel_Resize_Control(label18, lb18);
            painel_Resize_Control(label19, lb19);
            painel_Resize_Control(label20, lb20);
            painel_Resize_Control(label21, lb21);
            painel_Resize_Control(label22, lb22);
            painel_Resize_Control(label23, lb23);

            painel_Resize_Control(plusLb1, btPlusLb1);
            painel_Resize_Control(plusLb2, btPlusLb2);
            painel_Resize_Control(plusLb3, btPlusLb3);
            painel_Resize_Control(plusLb4, btPlusLb4);
            painel_Resize_Control(plusLb5, btPlusLb5);
            painel_Resize_Control(plusLb6, btPlusLb6);
            painel_Resize_Control(plusLb7, btPlusLb7);
            painel_Resize_Control(plusLb8, btPlusLb8);
            painel_Resize_Control(plusLb9, btPlusLb9);
            painel_Resize_Control(plusLb10, btPlusLb10);
            painel_Resize_Control(plusLb11, btPlusLb11);
            painel_Resize_Control(plusLb12, btPlusLb12);
            painel_Resize_Control(plusLb13, btPlusLb13);
            painel_Resize_Control(plusLb14, btPlusLb14);
            painel_Resize_Control(plusLb15, btPlusLb15);
            painel_Resize_Control(plusLb16, btPlusLb16);
            painel_Resize_Control(plusLb17, btPlusLb17);
            painel_Resize_Control(plusLb18, btPlusLb18);
            painel_Resize_Control(plusLb19, btPlusLb19);
            painel_Resize_Control(plusLb20, btPlusLb20);
            painel_Resize_Control(plusLb21, btPlusLb21);
            painel_Resize_Control(plusLb22, btPlusLb22);
            painel_Resize_Control(plusLb23, btPlusLb23);

            painel_Resize_Control(minusLb1, btMinusLb1);
            painel_Resize_Control(minusLb2, btMinusLb2);
            painel_Resize_Control(minusLb3, btMinusLb3);
            painel_Resize_Control(minusLb4, btMinusLb4);
            painel_Resize_Control(minusLb5, btMinusLb5);
            painel_Resize_Control(minusLb6, btMinusLb6);
            painel_Resize_Control(minusLb7, btMinusLb7);
            painel_Resize_Control(minusLb8, btMinusLb8);
            painel_Resize_Control(minusLb9, btMinusLb9);
            painel_Resize_Control(minusLb10, btMinusLb10);
            painel_Resize_Control(minusLb11, btMinusLb11);
            painel_Resize_Control(minusLb12, btMinusLb12);
            painel_Resize_Control(minusLb13, btMinusLb13);
            painel_Resize_Control(minusLb14, btMinusLb14);
            painel_Resize_Control(minusLb15, btMinusLb15);
            painel_Resize_Control(minusLb16, btMinusLb16);
            painel_Resize_Control(minusLb17, btMinusLb17);
            painel_Resize_Control(minusLb18, btMinusLb18);
            painel_Resize_Control(minusLb19, btMinusLb19);
            painel_Resize_Control(minusLb20, btMinusLb20);
            painel_Resize_Control(minusLb21, btMinusLb21);
            painel_Resize_Control(minusLb22, btMinusLb22);
            painel_Resize_Control(minusLb23, btMinusLb23);


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

        private void playFilter_Click(object sender, EventArgs e)
        {
            string filtro;
            filtro = Filters.Text;

            if (filtro.Equals("")) System.Windows.MessageBox.Show("Por favor, selecione algum filtro.");

            if (filtro.Equals("Low Pass"))
            {
                GlobVar.canalC = LowPassFilter.ApplyLowPassFilter(GlobVar.canalC, 0.6);
                int alturaTela = (int)openglControl1.Height;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            }
            else if (filtro.Equals("High Pass"))
            {
                GlobVar.canalD = HighPassFilter.ApplyHighPassFilter(GlobVar.canalD, 0.6);
                int alturaTela = (int)openglControl1.Height;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            }
            else if (filtro.Equals("Band Pass"))
            {
                GlobVar.canalE = BandPassFilter.ApplyBandPassFilter(GlobVar.canalE, 10, 50);
                int alturaTela = (int)openglControl1.Height;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            }
            else if (filtro.Equals("Band Reject"))
            {
                GlobVar.canalF = BandRejectFilter.ApplyBandRejectFilter(GlobVar.canalF, 0.1, 50);
                int alturaTela = (int)openglControl1.Height;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            }
            else if (filtro.Equals("FWT"))
            {
                filtrosSinais.FWT(GlobVar.canalA);
                GlobVar.canalA = filtrosSinais.alterado;
                int alturaTela = (int)openglControl1.Height;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);

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

                Canais.RealocLabel(qtdGrafics, 24);
                Canais.RealocButton(qtdGrafics, 13);
                Canais.quantidadeGraf(qtdGrafics);


                this.gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(qtdGrafics, alturaTela);
                plotagem.Traco(qtdGrafics, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0, 0, 1);

                hScrollBar1.Maximum = (GlobVar.canalA.Length);
                hScrollBar1.Refresh();
                UpdateInicioTela();
            }
            selectLabel.Items.Clear();
            for (int i = 1; i <= qtdGrafics; i++)
            {
                string qt = Convert.ToString(i);
                selectLabel.Items.Add(qt);
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
        private void OpenglControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void plusAll_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb1 += 0.1f;
            GlobVar.escalaLb2 += 0.1f;
            GlobVar.escalaLb3 += 0.1f;
            GlobVar.escalaLb4 += 0.1f;
            GlobVar.escalaLb5 += 0.1f;
            GlobVar.escalaLb6 += 0.1f;
            GlobVar.escalaLb7 += 0.1f;
            GlobVar.escalaLb8 += 0.1f;
            GlobVar.escalaLb9 += 0.1f;
            GlobVar.escalaLb10 += 0.1f;
            GlobVar.escalaLb11 += 0.1f;
            GlobVar.escalaLb12 += 0.1f;
            GlobVar.escalaLb13 += 0.1f;
            GlobVar.escalaLb14 += 0.1f;
            GlobVar.escalaLb15 += 0.1f;
            GlobVar.escalaLb16 += 0.1f;
            GlobVar.escalaLb17 += 0.1f;
            GlobVar.escalaLb18 += 0.1f;
            GlobVar.escalaLb19 += 0.1f;
            GlobVar.escalaLb20 += 0.1f;
            GlobVar.escalaLb21 += 0.1f;
            GlobVar.escalaLb22 += 0.1f;
            GlobVar.escalaLb23 += 0.1f;


            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void minusAll_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb1 -= 0.1f;
            GlobVar.escalaLb2 -= 0.1f;
            GlobVar.escalaLb3 -= 0.1f;
            GlobVar.escalaLb4 -= 0.1f;
            GlobVar.escalaLb5 -= 0.1f;
            GlobVar.escalaLb6 -= 0.1f;
            GlobVar.escalaLb7 -= 0.1f;
            GlobVar.escalaLb8 -= 0.1f;
            GlobVar.escalaLb9 -= 0.1f;
            GlobVar.escalaLb10 -= 0.1f;
            GlobVar.escalaLb11 -= 0.1f;
            GlobVar.escalaLb12 -= 0.1f;
            GlobVar.escalaLb13 -= 0.1f;
            GlobVar.escalaLb14 -= 0.1f;
            GlobVar.escalaLb15 -= 0.1f;
            GlobVar.escalaLb16 -= 0.1f;
            GlobVar.escalaLb17 -= 0.1f;
            GlobVar.escalaLb18 -= 0.1f;
            GlobVar.escalaLb19 -= 0.1f;
            GlobVar.escalaLb20 -= 0.1f;
            GlobVar.escalaLb21 -= 0.1f;
            GlobVar.escalaLb22 -= 0.1f;
            GlobVar.escalaLb23 -= 0.1f;

            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void plusLb1_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb1 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb1_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb1 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb2_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb2 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb2_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb2 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb3_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb3 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb3_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb3 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb4_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb4 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb4_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb4 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb5_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb5 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb5_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb5 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb6_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb6 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb6_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb6 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb7_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb7 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb7_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb7 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb8_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb8 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb8_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb8 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb9_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb9 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb9_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb9 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb10_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb10 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb10_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb10 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb11_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb11 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb11_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb11 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb12_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb12 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb12_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb12 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb13_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb13 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb13_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb13 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb14_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb14 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb14_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb14 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb15_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb15 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb15_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb15 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb16_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb16 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb16_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb16 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb17_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb17 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb17_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb17 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb18_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb18 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb18_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb18 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb19_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb19 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }
        private void minusLb19_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb19 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);
        }

        private void plusLb20_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb20 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb20_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb20 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb21_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb21 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb21_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb21 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb22_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb22 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb22_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb22 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }

        private void plusLb23_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb23 += 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }
        private void minusLb23_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            GlobVar.escalaLb23 -= 0.1f;
            openglControl1.DoRender();
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(0, 0, 1);

        }


        private bool isScrollingRight = true;


        private void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.D:
                    camera.X += GlobVar.saltoTelas * GlobVar.SPEED;
                    if (camera.X > 0) hScrollBar1.Value += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    //UpdateInicioTela();
                    break;
                case Keys.A:
                    camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;
                    if (camera.X > 0) hScrollBar1.Value -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

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
            //hScrollBar1.Value = (int)e.NewValue;
            bool isRight = e.NewValue > hScrollBar1.Value;

            if (!isRight) // Se estiver indo para a esquerda
            {
                camera.X -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
            }
            else // Se estiver indo para a direita
            {
                camera.X += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
            }

            int alturaTela = (int)openglControl1.Height;
            openglControl1.DoRender();
            plotagem.Margem(qtdGrafics, alturaTela);
            plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
            gl.Translate(camera.X, 0, 1);
            UpdateInicioTela();
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
            GlobVar.saltoTelas = GlobVar.tmpEmTela;
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

            string ptEmTela = Convert.ToString(GlobVar.tmpEmTela);
            ptsEmTela.Text = ptEmTela;
        }

        private void Tela_Plotagem_ResizeBegin(object sender, EventArgs e)
        {

        }

        
    }

    public class GlobVar
    {
        public static double[] canalA;
        public static double[] canalB;
        public static double[] canalC;
        public static double[] canalD;
        public static double[] canalE;
        public static double[] canalF;
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
        public static Vector2 sizePainelExams;

        public static int maximaVect = 2000;
        public static int indice = 0;

        public static float saltoTelas;
        public static float SPEED = 1.0f;

        public static int namos = 8;
        public static int segundos = 30;
        public static int tmpEmTela = 240;

        public static int inicioTela;
        public static int finalTela;

        public static float escalaLb1 = 1.0f;
        public static float escalaLb2 = 1.0f;
        public static float escalaLb3 = 1.0f;
        public static float escalaLb4 = 1.0f;
        public static float escalaLb5 = 1.0f;
        public static float escalaLb6 = 1.0f;
        public static float escalaLb7 = 1.0f;
        public static float escalaLb8 = 1.0f;
        public static float escalaLb9 = 1.0f;
        public static float escalaLb10 = 1.0f;
        public static float escalaLb11 = 1.0f;
        public static float escalaLb12 = 1.0f;
        public static float escalaLb13 = 1.0f;
        public static float escalaLb14 = 1.0f;
        public static float escalaLb15 = 1.0f;
        public static float escalaLb16 = 1.0f;
        public static float escalaLb17 = 1.0f;
        public static float escalaLb18 = 1.0f;
        public static float escalaLb19 = 1.0f;
        public static float escalaLb20 = 1.0f;
        public static float escalaLb21 = 1.0f;
        public static float escalaLb22 = 1.0f;
        public static float escalaLb23 = 1.0f;

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
                    GlobVar.canalA = new double[values.Length];
                    GlobVar.canalB = new double[values.Length];
                    GlobVar.canalC = new double[values.Length];
                    GlobVar.canalD = new double[values.Length];
                    GlobVar.canalE = new double [values.Length];
                    GlobVar.canalF = new double [values.Length];
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
                float[] taco = new float[qtdGraf];

                for (int i = 0; i < qtdGraf; i++)
                {
                    taco[i] = aux;
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

            for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);

                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(0, margem[i]);
                gl.Vertex(GlobVar.canalA.Length, margem[i]);
                gl.End();
            }

            /*for (int i = 0; i < qtdGraf; i++)
            {
                gl.LineStipple(1, 0xAAAA);
                gl.Enable(OpenGL.GL_LINE_STIPPLE);

                gl.Begin(OpenGL.GL_LINES);
                gl.Vertex(1, traco[i]);
                gl.Vertex(GlobVar.canalA.Length, traco[i]);
                gl.End();
            }*/

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
                    if (j > 0 ) gl.Vertex(j, ((float)GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 2:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if(j > 0 ) gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                   if(j > 0 )  gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 3:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 4:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 5:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;

            case 6:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 7:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 8:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 9:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 10:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 11:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 12:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                   if (j > 0 ) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 13:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 14:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 15:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                { 
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 16:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 17:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 18:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 19:
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                   if (j > 0 ) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j, (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                    //aqui tem plotar 3 graficos diferentes
                                                                                                    //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0 )gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[0]); // Define cada ponto do gráfico
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
                    if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                {
                    if (j > 0) gl.Vertex(j , (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                            //aqui tem plotar 3 graficos diferentes
                                                                                                            //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                }
                gl.End();
                break;
            case 21:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[11]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalU[j] * GlobVar.escalaLb21) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
            case 22:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[21]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[16]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[15]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalH[j] * GlobVar.escalaLb8) + desenhoLoc[14]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalI[j] * GlobVar.escalaLb9) + desenhoLoc[13]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalJ[j] * GlobVar.escalaLb10) + desenhoLoc[12]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalK[j] * GlobVar.escalaLb11) + desenhoLoc[11]); ; // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalL[j] * GlobVar.escalaLb12) + desenhoLoc[10]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalM[j] * GlobVar.escalaLb13) + desenhoLoc[9]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalN[j] * GlobVar.escalaLb14) + desenhoLoc[8]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalO[j] * GlobVar.escalaLb15) + desenhoLoc[7]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalP[j] * GlobVar.escalaLb16) + desenhoLoc[6]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalQ[j] * GlobVar.escalaLb17) + desenhoLoc[5]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalR[j] * GlobVar.escalaLb18) + desenhoLoc[4]); // Define cada ponto do gráfico
                                                                                                                 //aqui tem plotar 3 graficos diferentes
                                                                                                                 //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalS[j] * GlobVar.escalaLb19) + desenhoLoc[3]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalT[j] * GlobVar.escalaLb20) + desenhoLoc[2]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalU[j] * GlobVar.escalaLb21) + desenhoLoc[1]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalV[j] * GlobVar.escalaLb22) + desenhoLoc[0]); // Define cada ponto do gráfico
                                                                                                                //aqui tem plotar 3 graficos diferentes
                                                                                                                //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    break;
            case 23:
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalA[j] * GlobVar.escalaLb1) + desenhoLoc[22]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalB[j] * GlobVar.escalaLb2) + desenhoLoc[21]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalC[j] * GlobVar.escalaLb3) + desenhoLoc[20]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalD[j] * GlobVar.escalaLb4) + desenhoLoc[19]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalE[j] * GlobVar.escalaLb5) + desenhoLoc[18]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalF[j] * GlobVar.escalaLb6) + desenhoLoc[17]); // Define cada ponto do gráfico
                                                                                                                  //aqui tem plotar 3 graficos diferentes
                                                                                                                  //por mais que o canal A, B, C sejam o mesmo valor, tem que plotar sinais diferentes
                    }
                    gl.End();
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    for (int j = GlobVar.indice; j < GlobVar.maximaVect; j++)
                    {
                        if (j > 0) gl.Vertex(j , (GlobVar.canalG[j] * GlobVar.escalaLb7) + desenhoLoc[16]); // Define cada ponto do gráfico
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


            //System.Windows.MessageBox.Show("Tamanho da janela openGl " + Tela_Plotagem.openglControl1.Height + " x " + Tela_Plotagem.openglControl1.Width);

        }
    }
}
