using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using SharpGL;
using Size = System.Drawing.Size;
using Point = System.Drawing.Point;
using Vector3 = System.Numerics.Vector3;
using System.Text.RegularExpressions;
using PlotagemOpenGL.auxi;
using System.Collections.Generic;
using System.Reflection;
using MessageBox = System.Windows.MessageBox;
using System.ComponentModel;
using Accord.Math;
using Accord;
using System.Data;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Diagnostics.Eventing.Reader;


namespace PlotagemOpenGL
{
    public partial class Tela_Plotagem : Form
    {
        public static OpenGL gl;
        public static Plotagem plotagem;
        public static Canais canais;

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
        private Rectangle mtg;

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

        private Rectangle lbScale1;
        private Rectangle lbScale2;
        private Rectangle lbScale3;
        private Rectangle lbScale4;
        private Rectangle lbScale5;
        private Rectangle lbScale6;
        private Rectangle lbScale7;
        private Rectangle lbScale8;
        private Rectangle lbScale9;
        private Rectangle lbScale10;
        private Rectangle lbScale11;
        private Rectangle lbScale12;
        private Rectangle lbScale13;
        private Rectangle lbScale14;
        private Rectangle lbScale15;
        private Rectangle lbScale16;
        private Rectangle lbScale17;
        private Rectangle lbScale18;
        private Rectangle lbScale19;
        private Rectangle lbScale20;
        private Rectangle lbScale21;
        private Rectangle lbScale22;
        private Rectangle lbScale23;

        private Rectangle pn1;
        private Rectangle pn2;
        private Rectangle pn3;
        private Rectangle pn4;
        private Rectangle pn5;
        private Rectangle pn6;
        private Rectangle pn7;
        private Rectangle pn8;
        private Rectangle pn9;
        private Rectangle pn10;
        private Rectangle pn11;
        private Rectangle pn12;
        private Rectangle pn13;
        private Rectangle pn14;
        private Rectangle pn15;
        private Rectangle pn16;
        private Rectangle pn17;
        private Rectangle pn18;
        private Rectangle pn19;
        private Rectangle pn20;
        private Rectangle pn21;
        private Rectangle pn22;
        private Rectangle pn23;

        private Dictionary<Panel, Dictionary<string, bool>> panelLowFilterStates = new Dictionary<Panel, Dictionary<string, bool>>();
        private Dictionary<Panel, Dictionary<string, bool>> panelHighFilterStates = new Dictionary<Panel, Dictionary<string, bool>>();
        private Dictionary<Panel, Dictionary<string, bool>> panelNotchFilterStates = new Dictionary<Panel, Dictionary<string, bool>>();


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
            LeituraBanco.BancoRead();
            LeituraBanco.AlteraTable();
            LeituraBanco.AjustaMontagem();
            //Canais.LerCanais();
            //Leitura.LerArquivo();           
            Leitura.QuantidadeCanais();
            load();
            //Leitura.LeituraDat();

            LeituraEmMatrizTeste.LeituraDat();
            SetStyle(ControlStyles.DoubleBuffer, true);
            rectangleLoad();
            this.Resize += Tela_Plotagem_Resiz;
            this.Resize += painelComando_Resiz;
            this.Resize += Painel_resiz;
            this.Resize += panelLb_Resiz;
            this.Controls.Add(openglControl1);
            formOriginalSize = this.Size;
            painelOriginalSize = painelExames.Size;
            painelComandoOriginalSize = painelComando.Size;
            UpdateStyles();
            qtdGraficos.Text = "1";


            openglControl1.Focus();
            GlobVar.colors = new Vector3[]
            {
                new Vector3(175 / 255.0f, 238 / 255.0f, 238 / 255.0f),
                new Vector3(152 / 255.0f, 251 / 255.0f, 152 / 255.0f),
                new Vector3(224 / 255.0f, 255 / 255.0f, 255 / 255.0f),
                new Vector3(147 / 255.0f, 112 / 255.0f, 219 / 255.0f),
                new Vector3(216 / 255.0f, 191 / 255.0f, 216 / 255.0f),
                new Vector3(230 / 255.0f, 230 / 255.0f, 250 / 255.0f),
                new Vector3(175 / 255.0f, 238 / 255.0f, 238 / 255.0f),
                new Vector3(152 / 255.0f, 251 / 255.0f, 152 / 255.0f),
                new Vector3(224 / 255.0f, 255 / 255.0f, 255 / 255.0f),
                new Vector3(147 / 255.0f, 112 / 255.0f, 219 / 255.0f),
                new Vector3(216 / 255.0f, 191 / 255.0f, 216 / 255.0f),
                new Vector3(230 / 255.0f, 230 / 255.0f, 250 / 255.0f),
                new Vector3(175 / 255.0f, 238 / 255.0f, 238 / 255.0f),
                new Vector3(152 / 255.0f, 251 / 255.0f, 152 / 255.0f),
                new Vector3(224 / 255.0f, 255 / 255.0f, 255 / 255.0f),
                new Vector3(147 / 255.0f, 112 / 255.0f, 219 / 255.0f),
                new Vector3(216 / 255.0f, 191 / 255.0f, 216 / 255.0f),
                new Vector3(230 / 255.0f, 230 / 255.0f, 250 / 255.0f),
                new Vector3(175 / 255.0f, 238 / 255.0f, 238 / 255.0f),
                new Vector3(152 / 255.0f, 251 / 255.0f, 152 / 255.0f),
                new Vector3(224 / 255.0f, 255 / 255.0f, 255 / 255.0f),
                new Vector3(147 / 255.0f, 112 / 255.0f, 219 / 255.0f),
                new Vector3(216 / 255.0f, 191 / 255.0f, 216 / 255.0f),
                new Vector3(230 / 255.0f, 230 / 255.0f, 250 / 255.0f),
                new Vector3(230 / 255.0f, 230 / 255.0f, 250 / 255.0f)
            };
            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;
            GlobVar.sizePainelExams.X = painelExames.Width;
            GlobVar.sizePainelExams.Y = painelExames.Height;

            GlobVar.sizeButtons.X = plusLb1.Width;
            GlobVar.sizeButtons.Y = plusLb1.Height;
            GlobVar.sizeLabelExams.X = label1.Width;
            GlobVar.sizeLabelExams.Y = label1.Height;
            GlobVar.sizePanelLb.X = panel1.Width;
            GlobVar.sizePanelLb.Y = panel1.Height;

            GlobVar.locBut.X = plusLb1.Location.X;
            GlobVar.locScale.X = scalaLb1.Location.X;

            camera.X = 0.0f;
            camera.Y = 0.0f;
            camera.Z = 1.0f;
            tempoEmTela.SelectedIndex = 2;
            velocidadeScroll.SelectedIndex = 0;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            
            //InitializeContextMenu();
            //this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            //var backlog = new backLog();
            //backlog.Show();

            toolTip1.SetToolTip(openglControl1, "Teste");
            Play_OpenGl();
            GlobVar.auxL = new float[GlobVar.matrizCanal.GetLength(1)];
            GlobVar.auxH = new float[GlobVar.matrizCanal.GetLength(1)];
        }
        //Metodo para inicializar os rectangle para fazer a realoc deles quando maximizado a tela
        private void rectangleLoad()
        {
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
            box3 = new Rectangle(MontagemBox.Location, MontagemBox.Size);
            mtg = new Rectangle(playSelect.Location, playSelect.Size);

            btPlusLb1 = new Rectangle(plusLb1.Location, plusLb1.Size);
            btMinusLb1 = new Rectangle(minusLb1.Location, minusLb1.Size);
            btPlusLb2 = new Rectangle(plusLb2.Location, plusLb2.Size);
            btMinusLb2 = new Rectangle(minusLb2.Location, minusLb2.Size);
            btPlusLb3 = new Rectangle(plusLb4.Location, plusLb4.Size);
            btMinusLb3 = new Rectangle(minusLb4.Location, minusLb4.Size);
            btPlusLb4 = new Rectangle(plusLb3.Location, plusLb3.Size);
            btMinusLb4 = new Rectangle(minusLb3.Location, minusLb3.Size);
            btPlusLb5 = new Rectangle(plusLb5.Location, plusLb5.Size);
            btMinusLb5 = new Rectangle(minusLb5.Location, minusLb5.Size);

            lbScale1 = new Rectangle(scalaLb1.Location, scalaLb1.Size);
            lbScale2 = new Rectangle(scalaLb2.Location, scalaLb2.Size);
            lbScale3 = new Rectangle(scalaLb3.Location, scalaLb3.Size);
            lbScale4 = new Rectangle(scalaLb4.Location, scalaLb4.Size);
            lbScale5 = new Rectangle(scalaLb5.Location, scalaLb5.Size);
            lbScale6 = new Rectangle(scalaLb6.Location, scalaLb6.Size);
            lbScale7 = new Rectangle(scalaLb7.Location, scalaLb7.Size);
            lbScale8 = new Rectangle(scalaLb8.Location, scalaLb8.Size);
            lbScale9 = new Rectangle(scalaLb9.Location, scalaLb9.Size);
            lbScale10 = new Rectangle(scalaLb10.Location, scalaLb10.Size);
            lbScale11 = new Rectangle(scalaLb11.Location, scalaLb11.Size);
            lbScale12 = new Rectangle(scalaLb12.Location, scalaLb12.Size);
            lbScale13 = new Rectangle(scalaLb13.Location, scalaLb13.Size);
            lbScale14 = new Rectangle(scalaLb14.Location, scalaLb14.Size);
            lbScale15 = new Rectangle(scalaLb15.Location, scalaLb15.Size);
            lbScale16 = new Rectangle(scalaLb16.Location, scalaLb16.Size);
            lbScale17 = new Rectangle(scalaLb17.Location, scalaLb17.Size);
            lbScale18 = new Rectangle(scalaLb18.Location, scalaLb18.Size);
            lbScale19 = new Rectangle(scalaLb19.Location, scalaLb19.Size);
            lbScale20 = new Rectangle(scalaLb20.Location, scalaLb20.Size);
            lbScale21 = new Rectangle(scalaLb21.Location, scalaLb21.Size);
            lbScale22 = new Rectangle(scalaLb22.Location, scalaLb22.Size);
            lbScale23 = new Rectangle(scalaLb23.Location, scalaLb23.Size);


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

            pn1 = new Rectangle(panel1.Location, panel1.Size);
            pn2 = new Rectangle(panel2.Location, panel2.Size);
            pn3 = new Rectangle(panel3.Location, panel3.Size);
            pn4 = new Rectangle(panel4.Location, panel4.Size);
            pn5 = new Rectangle(panel5.Location, panel5.Size);
            pn6 = new Rectangle(panel6.Location, panel6.Size);
            pn7 = new Rectangle(panel7.Location, panel7.Size);
            pn8 = new Rectangle(panel8.Location, panel8.Size);
            pn9 = new Rectangle(panel9.Location, panel9.Size);
            pn10 = new Rectangle(panel10.Location, panel10.Size);
            pn11 = new Rectangle(panel11.Location, panel11.Size);
            pn12 = new Rectangle(panel12.Location, panel12.Size);
            pn13 = new Rectangle(panel13.Location, panel13.Size);
            pn14 = new Rectangle(panel14.Location, panel14.Size);
            pn15 = new Rectangle(panel15.Location, panel15.Size);
            pn16 = new Rectangle(panel16.Location, panel16.Size);
            pn17 = new Rectangle(panel17.Location, panel17.Size);
            pn18 = new Rectangle(panel18.Location, panel18.Size);
            pn19 = new Rectangle(panel19.Location, panel19.Size);
            pn20 = new Rectangle(panel20.Location, panel20.Size);
            pn21 = new Rectangle(panel21.Location, panel21.Size);
            pn22 = new Rectangle(panel22.Location, panel22.Size);
            pn23 = new Rectangle(panel23.Location, panel23.Size);

        }
        private void load()
        {
            MontagemBox.Items.Clear();
            foreach (DataRow row in GlobVar.tbl_Montagem.Rows)
            {
                MontagemBox.Items.Add(row["DescrMontagem"].ToString());
            }
            string selecao = GlobVar.tbl_MontGrav.Rows[0]["NomeMontagem"].ToString();
            MontagemBox.SelectedIndex = MontagemBox.Items.IndexOf(selecao);

            for (int i = 1; i <= 25; i++)
            {
                FieldInfo panel = typeof(Tela_Plotagem).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                if (panel != null)
                {
                    Panel pn = (Panel)panel.GetValue(this);
                    if (pn != null)
                    {
                        pn.ContextMenuStrip = contextMenuStrip1;
                        //pn.ContextMenuStrip.MouseClick += Form1_MouseUp;

                        // Initialize the dictionary for each panel
                        panelLowFilterStates[pn] = new Dictionary<string, bool>
                        {
                            { "NenhumLow", false },
                            { "hertz70", false },
                            { "hertz50", false },
                            { "hertz40", false },
                            { "hertz35", false },
                            { "hertz30", false },
                            { "hertz25", false },
                            { "hertz20", false },
                            { "hertz15", false },
                            { "hertz10", false },
                            { "hertz5", false },
                            { "OutroLow", false }
                        };

                        panelHighFilterStates[pn] = new Dictionary<string, bool>
                        {
                            { "NenhumHigh", false },
                            { "outroHigh", false },
                            { "hertz10H", false },
                            { "Hertz7", false },
                            { "hertz5H", false },
                            { "hertz3", false },
                            { "hertz1", false },
                            { "hertz07", false },
                            { "hertz05", false },
                            { "hertz03", false },
                            { "hertz01", false }
                        };
                        panelNotchFilterStates[pn] = new Dictionary<string, bool>
                        {
                            {"NenhumNotch",false },
                            {"hertz60N",false },
                            {"hertz50N",false },
                            {"OutroNotch",false }
                        };
                    }
                }
            }
            int auxxx = GlobVar.maximaVect - GlobVar.indice;


        }
        private void Play_OpenGl()
        {
            LeituraEmMatrizTeste.reorganize();
            if (String.IsNullOrEmpty(GlobVar.tbl_MontagemSelecionada.Rows.Count.ToString()))
            {
                System.Windows.MessageBox.Show("Por favor, informe a quantidade de graficos a serem mostradas.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {

                int alturaTela = (int)openglControl1.Height;

                canais = new Canais(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                canais.RealocButton();
                canais.PainelLb_Resize();
                canais.RealocPanel(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                canais.quantidadeGraf(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                canais.reloc();


                gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(GlobVar.tbl_MontagemSelecionada.Rows.Count, alturaTela);
                plotagem.Traco(GlobVar.tbl_MontagemSelecionada.Rows.Count, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, GlobVar.tbl_MontagemSelecionada.Rows.Count);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0, 0, 1);

                hScrollBar1.Maximum = (GlobVar.matrizCanal.GetLength(1));
                hScrollBar1.Refresh();
                UpdateInicioTela();
                click = true;
            }

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
            click = false;
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
            int newX = r.X;
            int newY = (int)(r.Y * yRatio);
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
            painelComando_Resize_Control(MontagemBox, box3);
            painelComando_Resize_Control(playSelect, mtg);
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
            painel_Resize_Control(panel1, pn1);
            painel_Resize_Control(panel2, pn2);
            painel_Resize_Control(panel3, pn3);
            painel_Resize_Control(panel4, pn4);
            painel_Resize_Control(panel5, pn5);
            painel_Resize_Control(panel6, pn6);
            painel_Resize_Control(panel7, pn7);
            painel_Resize_Control(panel8, pn8);
            painel_Resize_Control(panel9, pn9);
            painel_Resize_Control(panel10, pn10);
            painel_Resize_Control(panel11, pn11);
            painel_Resize_Control(panel12, pn12);
            painel_Resize_Control(panel13, pn13);
            painel_Resize_Control(panel14, pn14);
            painel_Resize_Control(panel15, pn15);
            painel_Resize_Control(panel16, pn16);
            painel_Resize_Control(panel17, pn17);
            painel_Resize_Control(panel18, pn18);
            painel_Resize_Control(panel19, pn19);
            painel_Resize_Control(panel20, pn20);
            painel_Resize_Control(panel21, pn21);
            painel_Resize_Control(panel22, pn22);
            painel_Resize_Control(panel23, pn23);

        }

        private void panelLb_Resiz(object sender, EventArgs e)
        {
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

            painel_Resize_Control(scalaLb1, lbScale1);
            painel_Resize_Control(scalaLb2, lbScale2);
            painel_Resize_Control(scalaLb3, lbScale3);
            painel_Resize_Control(scalaLb4, lbScale4);
            painel_Resize_Control(scalaLb5, lbScale5);
            painel_Resize_Control(scalaLb6, lbScale6);
            painel_Resize_Control(scalaLb7, lbScale7);
            painel_Resize_Control(scalaLb8, lbScale8);
            painel_Resize_Control(scalaLb9, lbScale9);
            painel_Resize_Control(scalaLb10, lbScale10);
            painel_Resize_Control(scalaLb11, lbScale11);
            painel_Resize_Control(scalaLb12, lbScale12);
            painel_Resize_Control(scalaLb13, lbScale13);
            painel_Resize_Control(scalaLb14, lbScale14);
            painel_Resize_Control(scalaLb15, lbScale15);
            painel_Resize_Control(scalaLb16, lbScale16);
            painel_Resize_Control(scalaLb17, lbScale17);
            painel_Resize_Control(scalaLb18, lbScale18);
            painel_Resize_Control(scalaLb19, lbScale19);
            painel_Resize_Control(scalaLb20, lbScale20);
            painel_Resize_Control(scalaLb21, lbScale21);
            painel_Resize_Control(scalaLb22, lbScale22);
            painel_Resize_Control(scalaLb23, lbScale23);

            GlobVar.locBut.X = plusLb1.Location.X;
            GlobVar.locScale.X = scalaLb1.Location.X;
        }
        private void MontagemBox_SelectedIndexChanged(object sender, EventArgs e) 
        {

        }
        private void qtdGraficos_TextChanged(object sender, EventArgs e)
        {
            string texto = qtdGraficos.Text;
            int numero;

            if (int.TryParse(texto, out numero))
            {
                qtdGrafics = Convert.ToInt32(texto);
                if (qtdGrafics <= 0 || qtdGrafics > 25)
                {
                    System.Windows.MessageBox.Show("Por favor, digite um número válido entre 1 e 20.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Por favor, digite um número inteiro válido.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }

        }
        static bool click = false;
        private void Play_Click(object sender, EventArgs e)
        {
            LeituraEmMatrizTeste.reorganize();
            if (String.IsNullOrEmpty(qtdGraficos.Text))
            {
                System.Windows.MessageBox.Show("Por favor, informe a quantidade de graficos a serem mostradas.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {

                int alturaTela = (int)openglControl1.Height;

                canais = new Canais(qtdGrafics);
                canais.RealocButton();
                canais.PainelLb_Resize();
                canais.RealocPanel(qtdGrafics);
                canais.quantidadeGraf(qtdGrafics);
                canais.reloc();


                gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(qtdGrafics, alturaTela);
                plotagem.Traco(qtdGrafics, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0, 0, 1);

                hScrollBar1.Maximum = (GlobVar.matrizCanal.GetLength(1));
                hScrollBar1.Refresh();
                UpdateInicioTela();
                click = true;
            }
        }

        private void Tela_Plotagem_Load(object sender, EventArgs e)
        {
            openglControl1.Focus();


        }

        private bool isDrawing = false;
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
        }

        private void OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (click)
            {
                if (e.Button == MouseButtons.Left)
                {
                    isDrawing = true;

                    // Convert window coordinates to OpenGL coordinates
                    ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.startX, out Plotagem.startY);

                    // Set the initial end coordinates to the starting coordinates
                    Plotagem.endX = Plotagem.startX;
                    Plotagem.endY = Plotagem.startY;

                    // Redraw the control
                    openglControl1.DoRender();
                    plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                }
                if (e.Button == MouseButtons.Right)
                {
                    isDrawing = false;

                    // Update end coordinates when the mouse button is released
                    ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);

                }
            }
        }

        private void OpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            Vector2 a;

            if (isDrawing)
            {
                // Update end coordinates as mouse moves
                ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);

                // Redraw the control
                openglControl1.DoRender();
                plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
            }
            if (click)
            {
                ConvertToOpenGLCoordinates(e.X, e.Y, out a.X, out a.Y);
                UpdateLoc(Canais.UpMouseLoc(a.Y, GlobVar.desenhoLoc));
            }
        }

        private void OpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;

                // Update end coordinates when the mouse button is released
                ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);

                // Redraw the control
                openglControl1.DoRender();
                plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

            }
            if (e.Button == MouseButtons.Right)
            {
                isDrawing = false;

                // Update end coordinates when the mouse button is released
                ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);

            }
        }
        private void UpdateLoc(string canal)
        {
            MouseLoc.Text = canal;
        }

        private void ConvertToOpenGLCoordinates(int mouseX, int mouseY, out float openGLX, out float openGLY)
        {
            var gl = openglControl1.OpenGL;

            // Get the viewport and projection/modelview matrices
            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

            double[] modelview = new double[16];
            gl.GetDouble(OpenGL.GL_MODELVIEW_MATRIX, modelview);

            double[] projection = new double[16];
            gl.GetDouble(OpenGL.GL_PROJECTION_MATRIX, projection);

            // Convert mouse coordinates to OpenGL coordinates
            float winX = (float)mouseX;
            float winY = (float)viewport[3] - (float)mouseY; // invert Y coordinate
            double objX, objY, objZ;
            objX = 0;
            objY = 0;
            objZ = 0;
            gl.UnProject(winX, winY, 0, modelview, projection, viewport, ref objX, ref objY, ref objZ);

            openGLX = (float)objX;
            openGLY = (float)objY;
        }

        private void plusAll_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;
            if (GlobVar.scale[0] < 0.09)
            {
                for (int i = 0; i < GlobVar.scale.Length; i++)
                {
                    GlobVar.scale[i] += 0.01f;
                }

                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
            }
            else
            {
                for (int i = 0; i < GlobVar.scale.Length; i++)
                {
                    GlobVar.scale[i] += 0.1f;
                }

                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
            }

            UpdateInicioTela();
        }
        private void minusAll_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;
            if (GlobVar.scale[0] <= 0.1)
            {
                for (int i = 0; i < GlobVar.scale.Length; i++)
                {
                    GlobVar.scale[i] -= 0.01f;
                }
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
            }
            else
            {
                for (int i = 0; i < GlobVar.scale.Length; i++)
                {
                    GlobVar.scale[i] -= 0.1f;
                }
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
            }
            UpdateInicioTela();
        }

        private void plusLb1_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;
            if (GlobVar.scale[0] < 0.09)
            {
                GlobVar.scale[0] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[0] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb1_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;
            if (GlobVar.scale[0] <= 0.1)
            {
                GlobVar.scale[0] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[0] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb2_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;
            if (GlobVar.scale[1] < 0.09)
            {
                GlobVar.scale[1] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[1] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb2_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[1] <= 0.1)
            {
                GlobVar.scale[1] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[1] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb3_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;
            if (GlobVar.scale[2] < 0.09)
            {
                GlobVar.scale[2] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[2] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb3_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[2] <= 0.1)
            {
                GlobVar.scale[2] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[2] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb4_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[3] < 0.09)
            {
                GlobVar.scale[3] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[3] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb4_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[3] <= 0.1)
            {
                GlobVar.scale[3] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[3] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb5_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[4] < 0.09)
            {
                GlobVar.scale[4] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[4] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb5_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[4] <= 0.1)
            {
                GlobVar.scale[4] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[4] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb6_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[5] < 0.09)
            {
                GlobVar.scale[5] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[5] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb6_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[5] <= 0.1)
            {
                GlobVar.scale[5] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[5] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb7_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[6] < 0.09)
            {
                GlobVar.scale[6] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[6] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb7_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[6] <= 0.1)
            {
                GlobVar.scale[6] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[6] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb8_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[7] < 0.09)
            {
                GlobVar.scale[7] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[7] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb8_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[7] <= 0.1)
            {
                GlobVar.scale[7] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[7] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb9_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[8] < 0.09)
            {
                GlobVar.scale[8] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[8] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb9_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[8] <= 0.1)
            {
                GlobVar.scale[8] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[8] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb10_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[9] < 0.09)
            {
                GlobVar.scale[9] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[9] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb10_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[9] <= 0.1)
            {
                GlobVar.scale[9] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[9] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb11_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[10] < 0.09)
            {
                GlobVar.scale[10] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[10] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb11_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[10] <= 0.1)
            {
                GlobVar.scale[10] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[10] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb12_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[11] < 0.09)
            {
                GlobVar.scale[11] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[11] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb12_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[11] <= 0.1)
            {
                GlobVar.scale[11] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[11] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb13_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[12] < 0.09)
            {
                GlobVar.scale[12] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[12] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb13_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[12] <= 0.1)
            {
                GlobVar.scale[12] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[12] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb14_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[13] < 0.09)
            {
                GlobVar.scale[13] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[13] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb14_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[13] <= 0.1)
            {
                GlobVar.scale[13] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[13] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb15_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[14] < 0.09)
            {
                GlobVar.scale[14] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[14] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb15_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[14] <= 0.1)
            {
                GlobVar.scale[14] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[14] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb16_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[15] < 0.09)
            {
                GlobVar.scale[15] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[15] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb16_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[15] <= 0.1)
            {
                GlobVar.scale[15] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[15] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb17_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[16] < 0.09)
            {
                GlobVar.scale[16] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[16] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb17_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[16] <= 0.1)
            {
                GlobVar.scale[16] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[16] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb18_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[17] < 0.09)
            {
                GlobVar.scale[17] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[17] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb18_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[17] <= 0.1)
            {
                GlobVar.scale[17] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[17] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb19_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[18] < 0.09)
            {
                GlobVar.scale[18] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[18] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb19_Click(object sender, EventArgs e)
        {

            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[18] <= 0.1)
            {
                GlobVar.scale[18] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[18] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb20_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[19] < 0.09)
            {
                GlobVar.scale[19] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[19] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb20_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[19] <= 0.1)
            {
                GlobVar.scale[19] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[19] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb21_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[20] < 0.09)
            {
                GlobVar.scale[20] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[20] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb21_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[20] <= 0.1)
            {
                GlobVar.scale[20] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[20] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb22_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[21] < 0.09)
            {
                GlobVar.scale[21] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[21] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb22_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[21] <= 0.1)
            {
                GlobVar.scale[21] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[21] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }

        private void plusLb23_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[22] < 0.09)
            {
                GlobVar.scale[22] += 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[22] += 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
        }
        private void minusLb23_Click(object sender, EventArgs e)
        {
            int alturaTela = (int)openglControl1.Height;

            if (GlobVar.scale[22] <= 0.1)
            {
                GlobVar.scale[22] -= 0.01f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            else
            {
                GlobVar.scale[22] -= 0.1f;
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);
                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
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
            //if (GlobVar.segundos >= 120)
            //{
            //    GlobVar.maximaVect *= 10;
            //}
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

        public static void UpdateInicioTela()
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

            scalaLb1.Text = GlobVar.scale[0].ToString("0.00");
            scalaLb2.Text = GlobVar.scale[1].ToString("0.00");
            scalaLb3.Text = GlobVar.scale[2].ToString("0.00");
            scalaLb4.Text = GlobVar.scale[3].ToString("0.00");
            scalaLb5.Text = GlobVar.scale[4].ToString("0.00");
            scalaLb6.Text = GlobVar.scale[5].ToString("0.00");
            scalaLb7.Text = GlobVar.scale[6].ToString("0.00");
            scalaLb8.Text = GlobVar.scale[7].ToString("0.00");
            scalaLb9.Text = GlobVar.scale[8].ToString("0.00");
            scalaLb10.Text = GlobVar.scale[9].ToString("0.00");
            scalaLb11.Text = GlobVar.scale[10].ToString("0.00");
            scalaLb12.Text = GlobVar.scale[11].ToString("0.00");
            scalaLb13.Text = GlobVar.scale[12].ToString("0.00");
            scalaLb14.Text = GlobVar.scale[13].ToString("0.00");
            scalaLb15.Text = GlobVar.scale[14].ToString("0.00");
            scalaLb16.Text = GlobVar.scale[15].ToString("0.00");
            scalaLb17.Text = GlobVar.scale[16].ToString("0.00");
            scalaLb18.Text = GlobVar.scale[17].ToString("0.00");
            scalaLb19.Text = GlobVar.scale[18].ToString("0.00");
            scalaLb20.Text = GlobVar.scale[19].ToString("0.00");
            scalaLb21.Text = GlobVar.scale[20].ToString("0.00");
            scalaLb22.Text = GlobVar.scale[21].ToString("0.00");
            scalaLb23.Text = GlobVar.scale[22].ToString("0.00");

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Tela_Plotagem_ResizeBegin(object sender, EventArgs e)
        {

        }

        public static int aux;
        private Rectangle htz;

        //Metodo que faz com que plote com base na montagem
        public static void elementoX()
        {

            qtdGraficos.Text = $"{aux}";
            int qtdGrafic = aux;
            if (String.IsNullOrEmpty(qtdGraficos.Text))
            {
                System.Windows.MessageBox.Show("Por favor, informe a quantidade de graficos a serem mostradas.", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
            else
            {

                int alturaTela = (int)openglControl1.Height;

                canais = new Canais(qtdGrafic);
                canais.RealocButton();
                canais.PainelLb_Resize();
                canais.RealocPanel(qtdGrafic);
                canais.quantidadeGraf(qtdGrafic);
                canais.reloc();


                gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(qtdGrafic, alturaTela);
                plotagem.Traco(qtdGrafic, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, qtdGrafic);
                gl.MatrixMode(OpenGL.GL_MODELVIEW);
                gl.LoadIdentity();
                gl.Translate(0, 0, 1);

                hScrollBar1.Maximum = (GlobVar.matrizCanal.GetLength(1));
                hScrollBar1.Refresh();
                UpdateInicioTela();
                click = true;
            }

        }

        private void playSelect_Click(object sender, EventArgs e)
        {

            montagem mont = new montagem();
            mont.Show();
        }

        private Panel clickedPanel;

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Verifica se o clique do mouse ocorreu dentro de algum controle
                Control clickedControl = this.GetChildAtPoint(e.Location);

                // Se o controle clicado for um Panel, realiza ações
                if (clickedControl is Panel panel)
                {
                    // Armazena o Panel clicado
                    clickedPanel = panel;

                    // Procura pelo Label dentro do Panel
                    foreach (Control innerControl in panel.Controls)
                    {
                        if (innerControl is Label label)
                        {
                            string labelText = label.Text;
                            MessageBox.Show($"Label text: {labelText}");
                            break; // Encontrou o primeiro Label e parou de procurar
                        }
                    }

                    // Abre o ContextMenuStrip na posição do mouse
                    if (panel.ContextMenuStrip != null)
                    {
                        panel.ContextMenuStrip.Show(panel, panel.PointToClient(e.Location));
                    }
                }
            }
        }

        private void ContextMenuStripOpenGl_Opening(object sender, CancelEventArgs e)
        {
            int YAdjusted = Plotagem.EncontrarValorMaisProximo(GlobVar.desenhoLoc, Plotagem.endY);
            //GlobVar.nomeCanais[GlobVar.grafSelected[YAdjusted]];
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            timer1.Start();

            ContextMenuStrip menu = sender as ContextMenuStrip;
            if (menu != null)
            {
                Control sourceControl = menu.SourceControl;
                if (sourceControl is Panel panel)
                {

                    clickedPanel = panel;
                    UpdateMenuItems(panel, menu.Items, panelLowFilterStates[panel]);
                    UpdateMenuItems(panel, menu.Items, panelHighFilterStates[panel]);
                    UpdateMenuItems(panel, menu.Items, panelNotchFilterStates[panel]);
                    // Verifica se o Panel tem um Label dentro dele
                    foreach (Control control in panel.Controls)
                    {
                        if (control is Label label)
                        {
                            if (label.Name.StartsWith("label"))
                            {
                                selectedLabelValue = label.Text;
                            }
                        }
                    }
                }
            }
        }
        public static void UpdateBeforeLoad(int indice, double low, double high, double notch)
        {
            FieldInfo panel = typeof(Tela_Plotagem).GetField($"panel{indice}", BindingFlags.Static | BindingFlags.Public);
            if (panel != null)
            {
                if (low != 0)
                {
                    if (low > 70)
                    {
                        string outro = "OutroLow";
                    }
                    else
                    {
                        string clicked = $"hertz{low}";
                    }
                }
                if (high != 0)
                {
                    if (high > 10)
                    {
                        string outro = "OutroHigh";
                    }
                    else if(high == 10 || high == 5)
                    {
                        string clicked = $"hertz{high}H";
                    }
                    else
                    {
                        string clicked = $"hertz{high}";
                    }


                }
                if (notch != 0)
                {
                    if (notch != 60 || notch != 50)
                    {
                        string outro = "OutroNotch";
                    }
                    else
                    {
                        string clicked = $"hertz{notch}";
                    }

                }
            }

        }
        private void ContextMenuStrip1_Opened(object sender, EventArgs e)
        {
        }

        private void LowPassFilter_DropDownOpened(object sender, System.EventArgs e)
        {

        }
        private void HighPassFilter_DropDownOpened(object sender, System.EventArgs e)
        {
        }
        private void UpdateMenuItems(Panel panel, ToolStripItemCollection items, Dictionary<string, bool> filterStates)
        {

            foreach (ToolStripMenuItem item in items)
            {
                if (filterStates.ContainsKey(item.Name))
                {
                    item.Checked = filterStates[item.Name];
                }
            }
        }
        private void toolTripItemDropDown_OpeningLow(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedPanel is Panel panel)
            {
                UpdateMenuItems(panel, clickedItem.DropDownItems, panelLowFilterStates[panel]);
            }
        }
        private void toolTripItemDropDown_OpeningHigh(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedPanel is Panel panel)
            {
                UpdateMenuItems(panel, clickedItem.DropDownItems, panelHighFilterStates[panel]);

            }
        }


        private string selectedLabelValue;
        private bool ContainsFilter(Dictionary<Panel, Dictionary<string, bool>> panelFilterStates, string filterName)
        {
            foreach (var innerDict in panelFilterStates.Values)
            {
                if (innerDict.ContainsKey(filterName))
                {
                    return true;
                }
            }
            return false;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;

            if (clickedPanel is Panel panel)
            {
                Dictionary<string, bool> filterStates;

                //Faz a verificacao de qual filtro esta sendo aplicado
                if (panelLowFilterStates.ContainsKey(panel) && ContainsFilter(panelLowFilterStates, clickedItem.Name))
                {
                    filterStates = panelLowFilterStates[panel];
                    if (clickedItem.CheckOnClick == true)
                    {
                        if (clickedItem.Text.Equals("Nenhum"))
                        {
                            int sele = GlobVar.nomeCanais.IndexOf(selectedLabelValue);
                            int selec = GlobVar.grafSelected.IndexOf(sele);
                            filtrosSinais.VoltaMatriz((short)GlobVar.grafSelected[index]);
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                        }
                        else if (clickedItem.Text.Equals("Outro"))
                        {
                            using (var inputForm = new InputForm("Insira o valor para o filtro"))
                            {
                                if (inputForm.ShowDialog() == DialogResult.OK)
                                {
                                    string inputValue = inputForm.InputValue;
                                    if (float.TryParse(inputValue, out float hertzSelect))
                                    {
                                        // Use o valor inserido pelo usuário
                                        hertzSelect = hertzSelect / 1000;
                                        if (PlotagemOpenGL.LowPassFilter.auxLow != 0)
                                        {
                                            PlotagemOpenGL.LowPassFilter.auxLow = 0;
                                            int sele = GlobVar.nomeCanais.IndexOf(selectedLabelValue);
                                            int selec = GlobVar.grafSelected.IndexOf(sele);
                                            filtrosSinais.VoltaMatriz((short)GlobVar.grafSelected[index]);
                                        }
                                        //double[] aux = new double[auxxx];
                                        //for (int i = GlobVar.indice; i < GlobVar.maximaVect; i++) { aux[i] = GlobVar.matrizCanal[(GlobVar.nomeCanais.IndexOf(selectedLabelValue)), i]; }
                                        //GlobVar.auxL = PlotagemOpenGL.LowPassFilter.ApplyLowPassFilter(GlobVar.auxL, hertzSelect, GlobVar.txPorCanal[GlobVar.nomeCanais.IndexOf(selectedLabelValue)]);
                                        int j = 0;
                                        for (int i = 0 /*GlobVar.indice*/; i < GlobVar.auxL.Length /*GlobVar.maximaVect*/; i++) { GlobVar.matrizCanal[GlobVar.nomeCanais.IndexOf(selectedLabelValue), i] = (short)GlobVar.auxL[j]; j++; }
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Por favor, insira um valor válido.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            float hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(0, 3));
                            hertzSelect = hertzSelect / 1000;
                            if (PlotagemOpenGL.LowPassFilter.auxLow != 0)
                            {
                                PlotagemOpenGL.LowPassFilter.auxLow = 0;
                                int sele = GlobVar.nomeCanais.IndexOf(selectedLabelValue);
                                int selec = GlobVar.grafSelected.IndexOf(sele);
                                filtrosSinais.VoltaMatriz((short)GlobVar.grafSelected[index]);
                            }
                            //double[] aux = new double[auxxx];
                            //for (int i = GlobVar.indice; i < GlobVar.maximaVect; i++) { aux[i] = GlobVar.matrizCanal[(GlobVar.nomeCanais.IndexOf(selectedLabelValue)), i]; }
                            GlobVar.auxL = PlotagemOpenGL.LowPassFilter.ApplyLowPassFilter(GlobVar.auxL, hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])]);
                            int j = 0;

                            for (int i = 0 /*GlobVar.indice*/; i < GlobVar.auxL.Length /*GlobVar.maximaVect*/; i++) { GlobVar.matrizCanal[index, i] = (short)GlobVar.auxL[i]; i++; }
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                            PlotagemOpenGL.LowPassFilter.auxLow += 1;
                        }
                        GlobVar.auxL.Clear();
                    }
                }
                else if (panelHighFilterStates.ContainsKey(panel) && ContainsFilter(panelHighFilterStates, clickedItem.Name))
                {
                    filterStates = panelHighFilterStates[panel];
                    if (clickedItem.CheckOnClick == true)
                    {
                        //int auxxx = GlobVar.maximaVect - GlobVar.indice;
                        if (clickedItem.Text.Equals("Nenhum"))
                        {
                            int sele = GlobVar.nomeCanais.IndexOf(selectedLabelValue);
                            int selec = GlobVar.grafSelected.IndexOf(sele);
                            filtrosSinais.VoltaMatriz((short)GlobVar.grafSelected[index]);
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                        }
                        else if (clickedItem.Text.Equals("Outro"))
                        {
                            using (var inputForm = new InputForm("Insira o valor para o filtro"))
                            {
                                if (inputForm.ShowDialog() == DialogResult.OK)
                                {
                                    string inputValue = inputForm.InputValue;
                                    if (float.TryParse(inputValue, out float hertzSelect))
                                    {
                                        // Use o valor inserido pelo usuário
                                        hertzSelect = hertzSelect / 1000;
                                        if (PlotagemOpenGL.HighPassFilter.auxHigh != 0)
                                        {
                                            PlotagemOpenGL.HighPassFilter.auxHigh = 0;
                                            int sele = GlobVar.nomeCanais.IndexOf(selectedLabelValue);
                                            int selec = GlobVar.grafSelected.IndexOf(sele);
                                            filtrosSinais.VoltaMatriz((short)GlobVar.grafSelected[index]);
                                        }
                                        //double[] aux = new double[auxxx];
                                        //for (int i = GlobVar.indice; i < GlobVar.maximaVect; i++) { aux[i] = GlobVar.matrizCanal[(GlobVar.nomeCanais.IndexOf(selectedLabelValue)), i]; }
                                        GlobVar.auxH = PlotagemOpenGL.HighPassFilter.ApplyHighPassFilter(GlobVar.auxH, hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])]);
                                        int j = 0;
                                        for (int i = 0 /*GlobVar.indice*/; i < GlobVar.auxH.Length /*GlobVar.maximaVect*/; i++) { GlobVar.matrizCanal[GlobVar.nomeCanais.IndexOf(selectedLabelValue), i] = (short)GlobVar.auxH[j]; j++; }
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                        PlotagemOpenGL.HighPassFilter.auxHigh += 1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Por favor, insira um valor válido.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            float hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(0, 3));
                            hertzSelect = hertzSelect / 1000;

                            if (PlotagemOpenGL.HighPassFilter.auxHigh != 0)
                            {
                                PlotagemOpenGL.HighPassFilter.auxHigh = 0;
                                int sele = GlobVar.nomeCanais.IndexOf(selectedLabelValue);
                                int selec = GlobVar.grafSelected.IndexOf(sele);
                                filtrosSinais.VoltaMatriz((short)GlobVar.grafSelected[selec]);
                            }
                            //double[] aux = new double[auxxx];
                            //for (int i = GlobVar.indice; i < GlobVar.maximaVect; i++) { aux[i] = GlobVar.matrizCanal[(GlobVar.nomeCanais.IndexOf(selectedLabelValue)), i]; }
                            GlobVar.auxH = PlotagemOpenGL.HighPassFilter.ApplyHighPassFilter(GlobVar.auxH, hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])]);
                            int j = 0;
                            for (int i = 0 /*GlobVar.indice*/; i < GlobVar.auxH.Length /*GlobVar.maximaVect*/; i++) { GlobVar.matrizCanal[GlobVar.nomeCanais.IndexOf(selectedLabelValue), i] = (short)GlobVar.auxH[j]; j++; }
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                            PlotagemOpenGL.HighPassFilter.auxHigh += 1;
                        }
                        GlobVar.auxH.Clear();
                    }
                }
                else
                {
                    return; // No filter state found for this panel
                }

                UncheckAllItemsExceptSelected(clickedItem, filterStates);
            }
        }

        private void UncheckAllItemsExceptSelected(ToolStripMenuItem selectedItem, Dictionary<string, bool> filterStates)
        {
            foreach (ToolStripMenuItem item in selectedItem.GetCurrentParent().Items)
            {
                if (filterStates.ContainsKey(item.Name))
                {
                    filterStates[item.Name] = item == selectedItem;
                    item.Checked = item == selectedItem;
                }
            }
        }

        //Metodo para deselecionar os itens que tem dentro de cada filtro
        private void UncheckAllItemsExceptSelectedLowFilter(ToolStripMenuItem selectedItem)
        {
            foreach (ToolStripMenuItem item in LowPassFilter.DropDownItems)
            {
                if (item != selectedItem)
                {
                    item.Checked = false;
                }
            }
        }
        private void UncheckAllItemsExceptSelectedHighFilter(ToolStripMenuItem selectedItem)
        {
            foreach (ToolStripMenuItem item in HighPassFilter.DropDownItems)
            {
                if (item != selectedItem)
                {
                    item.Checked = false;
                }
            }
        }
        //Opcoes dentro do LowPassFilter
        private void NenhumLow_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (NenhumLow.Checked == true)
            {
                MessageBox.Show($"NenhumLow clicked. Panel contains label with text: {selectedLabelValue}");
                UncheckAllItemsExceptSelectedLowFilter(clickedItem);
            }

        }
        int index = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int? rowIndex = null;
            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                if (GlobVar.tbl_MontagemSelecionada.Rows[i].Field<string>("Legenda") == selectedLabelValue)
                {
                    rowIndex = i;
                    break;
                }
            }
            if (rowIndex != null)
            {
                index = (int)rowIndex;
                for (int i = 0 /*GlobVar.indice*/; i < GlobVar.auxL.Length /*GlobVar.maximaVect*/; i++) { GlobVar.auxL[i] = GlobVar.matrizCanal[index, i]; }
                for (int i = 0 /*GlobVar.indice*/; i < GlobVar.auxL.Length /*GlobVar.maximaVect*/; i++) { GlobVar.auxH[i] = GlobVar.matrizCanal[index, i]; }
            }
            timer1.Stop();
        }

        
    }
}
