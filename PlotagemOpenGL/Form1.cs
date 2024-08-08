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
using System.Linq;
using PlotagemOpenGL.Filtros;
using SharpGL.SceneGraph;
using System.Linq.Expressions;
using System.Diagnostics;
using Input = UnityEngine.Input;
using PlotagemOpenGL.auxi.auxPlotagem;
using OpenTK.Windowing.Common.Input;
using PlotagemOpenGL.auxi.FormsAuxi;
using System.Windows.Media;
using ClassesBDNano;
//using KeyCode = UnityEngine.KeyCode;


namespace PlotagemOpenGL
{
    public partial class Tela_Plotagem : Form
    {
        public static OpenGL gl;
        public static Plotagem plotagem;
        public static Canais canais;

        private Stopwatch stopwatch;

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
            LeitorDiretorio.LeituraDiretorio();

            LeituraBanco.BancoRead();
            LeituraBanco.AlteraTable();
            LeituraBanco.AjustaMontagem();
            //Canais.LerCanais();
            //Leitura.LerArquivo();           
            Leitura.QuantidadeCanais();
            //Leitura.LeituraDat();
            LeituraBanco.AjustaCadEvent(); // Esta ajustando os valores das teclas rapida para -1 caso o valor seja null, pois estava atrapalhando quando era null

            LeituraEmMatrizTeste.LeituraDat();
            SetStyle(ControlStyles.DoubleBuffer, true);
            rectangleLoad();
            this.Resize += Tela_Plotagem_Resiz;
            this.Resize += painelComando_Resiz;
            this.Resize += Painel_resiz;
            this.Resize += panelLb_Resiz;
            this.Controls.Add(openglControl1);
            toolTip1 = new CustomToolTip();
            formOriginalSize = this.Size;
            painelOriginalSize = painelExames.Size;
            painelComandoOriginalSize = painelComando.Size;
            UpdateStyles();
            qtdGraficos.Text = $"{GlobVar.tbl_MontagemSelecionada.Rows.Count.ToString()}";


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
            GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;


            load();
            camera.X = 0.0f;
            camera.Y = 0.0f;
            camera.Z = 1.0f;
            tempoEmTela.SelectedIndex = 5;
            GlobVar.Amplitude = [5, 25, 50, 75, 80, 100, 150, 200, 250, 300, 350, 400, 450, 500, 600, 650, 700, 800, 900, 1000, 1250, 1500, 1750, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 15000, 20000];
            velocidadeScroll.SelectedIndex = 0;
            stopwatch = new Stopwatch();
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += openglControl1_MouseMove;
            //InitializeContextMenu();
            //this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            //var backlog = new backLog();
            //backlog.Show();

            toolTip1.SetToolTip(openglControl1, "Teste");
            Play_OpenGl();

            timer3.Start();
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
            UpdateFilterStates();
        }

        private void UpdateFilterStates()
        {
            int panelIndex = 1;
            foreach (var row in GlobVar.tbl_MontagemSelecionada.AsEnumerable())
            {
                double? lowHertz = row.Field<double?>("PassaBaixa");
                double? highHertz = row.Field<double?>("PassaAlta");
                double? notchHertz = row.Field<double?>("Notch");

                FieldInfo panelField = typeof(Tela_Plotagem).GetField($"panel{panelIndex}", BindingFlags.Static | BindingFlags.Public);
                if (panelField != null)
                {
                    Panel pn = (Panel)panelField.GetValue(this);
                    if (pn != null)
                    {
                        UpdateLowFilterState(pn, lowHertz);
                        UpdateHighFilterState(pn, highHertz);
                        UpdateNotchFilterState(pn, notchHertz);
                    }
                }
                panelIndex++;
            }
        }

        private void UpdateLowFilterState(Panel panel, double? lowHertz)
        {
            if (lowHertz.HasValue)
            {
                foreach (var key in panelLowFilterStates[panel].Keys.ToList())
                {
                    panelLowFilterStates[panel][key] = false;
                }
                if (lowHertz.Value == 70) panelLowFilterStates[panel]["hertz70"] = true;
                else if (lowHertz.Value == 50) panelLowFilterStates[panel]["hertz50"] = true;
                else if (lowHertz.Value == 40) panelLowFilterStates[panel]["hertz40"] = true;
                else if (lowHertz.Value == 35) panelLowFilterStates[panel]["hertz35"] = true;
                else if (lowHertz.Value == 30) panelLowFilterStates[panel]["hertz30"] = true;
                else if (lowHertz.Value == 25) panelLowFilterStates[panel]["hertz25"] = true;
                else if (lowHertz.Value == 20) panelLowFilterStates[panel]["hertz20"] = true;
                else if (lowHertz.Value == 15) panelLowFilterStates[panel]["hertz15"] = true;
                else if (lowHertz.Value == 10) panelLowFilterStates[panel]["hertz10"] = true;
                else if (lowHertz.Value == 5) panelLowFilterStates[panel]["hertz5"] = true;
                else panelLowFilterStates[panel]["OutroLow"] = true;
            }
            else
            {
                panelLowFilterStates[panel]["NenhumLow"] = true;
            }
        }

        private void UpdateHighFilterState(Panel panel, double? highHertz)
        {
            if (highHertz.HasValue)
            {
                foreach (var key in panelHighFilterStates[panel].Keys.ToList())
                {
                    panelHighFilterStates[panel][key] = false;
                }
                if (highHertz.Value == 10) panelHighFilterStates[panel]["hertz10H"] = true;
                else if (highHertz.Value == 7) panelHighFilterStates[panel]["Hertz7"] = true;
                else if (highHertz.Value == 5) panelHighFilterStates[panel]["hertz5H"] = true;
                else if (highHertz.Value == 3) panelHighFilterStates[panel]["hertz3"] = true;
                else if (highHertz.Value == 1) panelHighFilterStates[panel]["hertz1"] = true;
                else if (highHertz.Value == 0.7) panelHighFilterStates[panel]["hertz07"] = true;
                else if (highHertz.Value == 0.5) panelHighFilterStates[panel]["hertz05"] = true;
                else if (highHertz.Value == 0.3) panelHighFilterStates[panel]["hertz03"] = true;
                else panelHighFilterStates[panel]["outroHigh"] = true;
            }
            else
            {
                panelHighFilterStates[panel]["NenhumHigh"] = true;
            }
        }

        private void UpdateNotchFilterState(Panel panel, double? notchHertz)
        {
            if (notchHertz.HasValue)
            {
                foreach (var key in panelNotchFilterStates[panel].Keys.ToList())
                {
                    panelNotchFilterStates[panel][key] = false;
                }
                if (notchHertz.Value == 60) panelNotchFilterStates[panel]["hertz60N"] = true;
                else if (notchHertz.Value == 50) panelNotchFilterStates[panel]["hertz50N"] = true;
                else panelNotchFilterStates[panel]["OutroNotch"] = true;
            }
            else
            {
                panelNotchFilterStates[panel]["NenhumNotch"] = true;
            }
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

                TelaClearAndReload();

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
            Play_OpenGl();

        }
        private void MontagemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ideia para alterar a montagem que esta selecionada, ao testar precisa ser feito muita alteracao ou somente a releitura de algumas coisas
            //int CodMont = Convert.ToInt16(GlobVar.tbl_Montagem.Rows[MontagemBox.Items.IndexOf(MontagemBox.Text)]["CodMontagem"]);
            //LeituraBanco.AlteraMontagem(CodMont);

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

                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                                
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

        public static bool isDrawing = false;
        public static bool isDrawingRectangle = false;
        // Variável para rastrear o painel anterior
        private Panel previousPanel = null;

        // Evento para mostrar o botão quando o mouse estiver sobre ele
        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is Button button)
                    {
                        if (button.ClientRectangle.Contains(button.PointToClient(Cursor.Position)))
                        {
                            button.Show();
                        }
                        else
                        {
                            button.Hide();
                        }
                    }
                }
            }
        }

        // Evento para esconder os botões quando o mouse sai do painel
        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control is Button button)
                    {
                        button.Hide();
                    }
                }
            }
        }

        //Metodo que faz a plotagem, e a replotagem quando precisa
        public static void TelaClearAndReload()
        {
            openglControl1.DoRender();
            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
            //plotNumerico.PlotNumerico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);

            plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
            plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
            plotNumerico.PlotNumerico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
            plotNumerico.PlotSetas(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
            //plotEventos.DrawTexts(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc); - Metodo para escrever o Bom Dia e os tipos de eventos aonde o evento esta localizado.
        }

        private void openglControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                /*this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y);
                Cursor.Clip = new Rectangle(this.Location, this.Size);

                // Flag to determine if the mouse is over any panel
                bool mouseOverAnyPanel = false;

                // Iterate over all controls within the form or a specific container
                foreach (Control control in this.Controls) // or openglControl1.Controls if they are children of openglControl1
                {
                    if (control is Panel panel)
                    {
                        // Check if the cursor is within the bounds of the panel
                        if (panel.ClientRectangle.Contains(panel.PointToClient(Cursor.Position)))
                        {
                            // Show buttons within the panel
                            foreach (Control panelControl in panel.Controls)
                            {
                                if (panelControl is Button)
                                {
                                    panelControl.Visible = true;
                                }
                            }
                            mouseOverAnyPanel = true;
                        }
                        else
                        {
                            // Hide buttons within the panel
                            foreach (Control panelControl in panel.Controls)
                            {
                                if (panelControl is Button)
                                {
                                    panelControl.Visible = false;
                                }
                            }
                        }
                    }
                }

                // If the mouse is not over any panel, reset visibility if necessary
                if (!mouseOverAnyPanel)
                {
                    foreach (Control control in this.Controls) // or openglControl1.Controls
                    {
                        if (control is Panel panel)
                        {
                            foreach (Control panelControl in panel.Controls)
                            {
                                if (panelControl is Button)
                                {
                                    panelControl.Visible = false;
                                }
                            }
                        }
                    }
                }*/
                if (this.Cursor == Cursors.SizeAll)
                {
                    toolTip1.SetToolTip(openglControl1, GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%");
                    return;
                }
            }
            catch (Exception ee)
            {
                string message = Convert.ToString(ee);
                System.Windows.MessageBox.Show(message);
            }
        }
        private void OpenglControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta != 0)
                {
                    if (e.Delta > 0)
                    {
                        if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1)) {
                            camera.X += GlobVar.saltoTelas * GlobVar.SPEED;
                            if (camera.X > 0) hScrollBar1.Value += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                            GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            //UpdateInicioTela();
                        }
                    }
                    else
                    {
                        if (GlobVar.indice > 0)
                        {
                            camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;
                            if (camera.X > 0 && hScrollBar1.Value != 0) hScrollBar1.Value -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                            GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            //UpdateInicioTela();
                        }
                    }
                    int alturaTela = (int)openglControl1.Height;
                    TelaClearAndReload();
                    gl.Translate(camera.X, 0, 1);
                    hScrollBar1.Maximum = (GlobVar.matrizCanal.GetLength(1));
                    hScrollBar1.Refresh();
                    UpdateInicioTela();
                    //Update();
                }
            }
            catch { }
        }

        private Point lastMousePosition;
        public Point initialMousePosition;
        public Point musezin;

        private Cursor originalCursor;
        private bool isMouseDown = false;
        public bool isAnEvent = false;
        public bool isAnStartEvent = false;
        public bool isAnEndEvent = false;
        public bool isA_BN_CPAP_BD = false;
        private bool isTelaClearAndReloadExecuted;

        private void OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (click)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Armazena o cursor original e marca que o mouse está pressionado
                    originalCursor = this.Cursor;
                    isMouseDown = true;
                                        
                    if (!isAnEvent && !isAnStartEvent && !isAnEndEvent)
                    {
                        timer2.Start();
                        isDrawing = true;
                        isDrawingRectangle = true;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.startX, out Plotagem.startY);
                        if(!crtlAtivo){
                            GlobVar.endX = (int)Plotagem.startX;
                            GlobVar.endY = (int)Plotagem.startY;
                            GlobVar.startX = (int)Plotagem.startX;
                            GlobVar.startY = (int)Plotagem.startY;
                            openglControl1.DoRender();
                            plotEventos.DrawingAnEvent(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                        }
                    }
                    else if (this.isAnEvent)
                    {
                        if (crtlAtivo)
                        {
                            timer2.Start();
                            lastMousePosition = e.Location;
                            GlobVar.startX = GlobVar.iniEventoMove;
                            openglControl1.DoRender();
                            plotEventos.DeleteEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                            //TelaClearAndReload();
                        }
                        else
                        {
                            float aux;
                            timer2.Start();
                            isDrawing = true;
                            lastMousePosition = e.Location;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out Plotagem.startY);
                            initialMousePosition.X = (int)aux;
                            //initialMousePosition.X = e.X;
                            initialMousePosition.Y = (int)aux;
                            initialMousePosition.Y = e.X;
                            GlobVar.startX = GlobVar.iniEventoMove;
                            openglControl1.DoRender();
                            //TelaClearAndReload();
                        }

                    }
                    else if (this.isAnStartEvent || this.isAnEndEvent)
                    {
                        if (crtlAtivo)
                        {
                            timer2.Start();
                            lastMousePosition = e.Location;
                            GlobVar.startX = GlobVar.iniEventoMove;
                            openglControl1.DoRender();
                            plotEventos.DeleteEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                            //TelaClearAndReload();
                        }
                        else{
                            float aux; 
                            timer2.Start();


                            isDrawing = true;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out Plotagem.startY);
                            initialMousePosition.X = (int)aux;

                            lastMousePosition = e.Location;
                            if (isAnStartEvent)
                            {
                                GlobVar.startX = GlobVar.iniEventoMove;
                            }
                            else if (isAnEndEvent)
                            {
                                GlobVar.endX = GlobVar.durEventoMove;
                            }

                            openglControl1.Refresh();
                            plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    GlobVar.rightClickSave = e.Location;
                    isDrawing = false;
                    ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);
                    toolTip1.RemoveAll();

                }
            }
        }

        private void OpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    this.musezin.X = e.X;
                    this.musezin.Y = e.Y;

                    ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.startY);
                    int startY = (int)Plotagem.startY;
                    float endX = Plotagem.endX;
                    plotEventos.LastEvent(GlobVar.desenhoLoc, startY);

                    if (!isDrawing) {
                        this.isAnEvent = plotEventos.IsThereAnEvent((int)endX, GlobVar.desenhoLoc, startY);
                        this.isAnStartEvent = plotEventos.IsInAnEventStart((int)endX, GlobVar.desenhoLoc, startY);
                        this.isAnEndEvent = plotEventos.IsInAnEventEnd((int)endX, GlobVar.desenhoLoc, startY);
                        if (this.isAnEvent)
                        {
                            isA_BN_CPAP_BD = false;
                        }
                    }
                    if (!isDrawing)
                    {
                        if (!this.isAnEvent)
                        {
                            this.isA_BN_CPAP_BD = plotEventos.EUmBoaNoite_Cpap_BomDia((int)endX);
                        }
                    }
                    GlobVar.drawBordenInAnEvent = this.isAnEvent;

                    if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent))
                    {
                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.Default;
                            if (!isTelaClearAndReloadExecuted)
                            {
                                TelaClearAndReload();
                                isTelaClearAndReloadExecuted = true;
                            }
                        }
                        toolTip1.RemoveAll();
                        if (isDrawing)
                        {
                            timerClick.Start();
                            isTelaClearAndReloadExecuted = false;
                            if(crtlAtivo)
                            {
                                if (e.X >= openglControl1.Size.Width)
                                {

                                    camera.X += GlobVar.namos;
                                    if (camera.X > 0) hScrollBar1.Value += GlobVar.namos;

                                    GlobVar.indiceNumero += 8 * 1; //(int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED; // 8 * 1;
                                    GlobVar.maximaNumero += 8 * 1;

                                    GlobVar.maximaVect += GlobVar.namos;
                                    GlobVar.indice += GlobVar.namos;

                                    GlobVar.inicioTela += GlobVar.namos / GlobVar.namos;
                                    GlobVar.finalTela += GlobVar.namos / GlobVar.namos;
                                    //TelaClearAndReload();
                                    gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
                                    UpdateInicioTela();
                                }

                            }
                            else{
                            if (GlobVar.endX >= GlobVar.startX)
                            {
                                if(e.X <= musezin.X)
                                {
                                    GlobVar.endX = (int)endX;
                                    //TelaClearAndReload();
                                }
                                else
                                {
                                    GlobVar.endX = (int)endX;
                                }
                            }
                                if (e.X >= openglControl1.Size.Width)
                                {
                                    
                                    camera.X += GlobVar.namos;
                                    if (camera.X > 0) hScrollBar1.Value += GlobVar.namos;

                                    GlobVar.indiceNumero += 8 * 1; //(int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED; // 8 * 1;
                                    GlobVar.maximaNumero += 8 * 1;

                                    GlobVar.maximaVect += GlobVar.namos;
                                    GlobVar.indice += GlobVar.namos;

                                    GlobVar.inicioTela += GlobVar.namos / GlobVar.namos;
                                    GlobVar.finalTela += GlobVar.namos / GlobVar.namos;
                                    //TelaClearAndReload();
                                    gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
                                    UpdateInicioTela();
                                }
                            }
                            openglControl1.Invalidate();
                            plotEventos.DrawingAnEvent(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                        }
                    }
                    else if (isAnEvent && (!this.isAnStartEvent || !this.isAnEndEvent))
                    {
                        timerClick.Start();

                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeAll;
                            openglControl1.DoRender();
                            plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                            toolTip1.SetToolTip(openglControl1, GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%");
                        }
                        else
                        {
                            float outX = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);

                            float deltaX = outX - initialMousePosition.X;
                            //float deltaX = e.X - initialMousePosition.X;
                            if (e.X != initialMousePosition.Y)
                            {
                                if (e.X < initialMousePosition.Y)
                                {
                                    GlobVar.iniEventoMove -= ((int)Math.Abs(deltaX));
                                    GlobVar.durEventoMove -= ((int)Math.Abs(deltaX));
                                }
                                else
                                {
                                    GlobVar.iniEventoMove += ((int)Math.Abs(deltaX));
                                    GlobVar.durEventoMove += ((int)Math.Abs(deltaX));
                                }
                            }
                            //TelaClearAndReload();

                            openglControl1.Refresh();
                            //plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                            initialMousePosition.Y = e.X;
                            //initialMousePosition.X = e.X;
                            initialMousePosition.X = (int)outX;
                        }
                    }
                    else if ((!isAnEvent && this.isAnStartEvent) || (!isAnEvent && this.isAnEndEvent))
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeWE;
                        }
                        

                        if (isDrawing)
                        {
                            toolTip1.RemoveAll();

                            float outX = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);

                            float deltaX = outX - initialMousePosition.X;

                            if (e.X != lastMousePosition.X)
                            {
                                if (this.isAnStartEvent)
                                {
                                    deltaX = GlobVar.iniEventoMove - outX;

                                    if (e.X < lastMousePosition.X)
                                    {
                                        GlobVar.iniEventoMove -= ((int)Math.Abs(deltaX));
                                    }
                                    else
                                    {
                                        GlobVar.iniEventoMove += ((int)Math.Abs(deltaX));
                                    }
                                    GlobVar.endX = GlobVar.durEventoMove;

                                }


                                else if (this.isAnEndEvent)
                                {
                                    deltaX = GlobVar.durEventoMove - outX;

                                    if (e.X < lastMousePosition.X)
                                    {
                                        GlobVar.durEventoMove -= ((int)Math.Abs(deltaX));
                                    }
                                    else
                                    {
                                        GlobVar.durEventoMove += ((int)Math.Abs(deltaX));
                                    }

                                    GlobVar.startX = GlobVar.iniEventoMove;

                                    if (e.X >= openglControl1.Size.Width)
                                    {

                                        camera.X += GlobVar.namos;
                                        if (camera.X > 0) hScrollBar1.Value += GlobVar.namos;

                                        GlobVar.indiceNumero += 8 * 1; //(int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED; // 8 * 1;
                                        GlobVar.maximaNumero += 8 * 1;

                                        GlobVar.maximaVect += GlobVar.namos;
                                        GlobVar.indice += GlobVar.namos;

                                        GlobVar.inicioTela += GlobVar.namos / GlobVar.namos;
                                        GlobVar.finalTela += GlobVar.namos / GlobVar.namos;
                                        //TelaClearAndReload();
                                        gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
                                        UpdateInicioTela();
                                    }

                                }

                                initialMousePosition.X = (int)outX;

                                lastMousePosition = e.Location;
                                //TelaClearAndReload();


                                openglControl1.Refresh();
                                //plotEventos.DrawBordenInAnEvent(isDrawing, gl, GlobVar.desenhoLoc);

                            }
                        }
                    }
                }

                //openglControl1.DoRender();
                //plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        int lastStartX;
        private void OpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                plotanu = false;

                if (e.Button == MouseButtons.Left)
                {
                    if (!isAnEvent && !isAnStartEvent && !isAnEndEvent)
                    {
                        isDrawing = false;
                        isDrawingRectangle = false;

                        if (!crtlAtivo){
                            UpdateLoc();
                            isDrawingRectangle = false;
                            isDrawing = false;

                            ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);
                            GlobVar.endX = (int)Plotagem.endX;

                            if(GlobVar.startX == GlobVar.endX)
                            {
                                GlobVar.startX = null;
                                isDrawing = false;
                            }

                            if (GlobVar.startX != null)
                            {
                                int LimiteAux = Math.Abs(GlobVar.endX - (int)GlobVar.startX);

                                if (LimiteAux < GlobVar.MinimumValueEvent)
                                {
                                    GlobVar.endX = (int)GlobVar.startX + GlobVar.MinimumValueEvent;
                                }

                                if (GlobVar.endX != GlobVar.startX)
                                {
                                    if (GlobVar.endX < GlobVar.startX)
                                    {
                                        plotEventos.AdicionarEventoAoDataTable(GlobVar.endX, (int)GlobVar.startX, GlobVar.canal, GlobVar.desenhoLoc, GlobVar.startY);
                                    }
                                    else
                                    {
                                        plotEventos.AdicionarEventoAoDataTable((int)GlobVar.startX, GlobVar.endX, GlobVar.canal, GlobVar.desenhoLoc, GlobVar.startY);
                                    }
                                }
                            }
                            timerClick.Stop();

                            openglControl1.DoRender();
                            plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                        }
                    }
                    else if (isAnEvent)
                    {
                        if (crtlAtivo)
                        {
                            isDrawing = false;
                            TelaClearAndReload();

                        }else
                        {
                            float outX = 0 ;
                            isDrawing = false;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);


                            GlobVar.endX = GlobVar.durEventoMove;

                            if (GlobVar.endX != GlobVar.startX)
                            {
                                plotEventos.UpdateEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                            }
                            lastMousePosition = e.Location;
                            TelaClearAndReload();
                            timerClick.Stop();

                        }
                    }
                    else if (this.isAnStartEvent || this.isAnEndEvent)
                    {

                        if (crtlAtivo)
                        {
                            isDrawing = false;
                            TelaClearAndReload();

                        }
                        else
                        {
                            isDrawing = false;

                            if (this.isAnStartEvent)
                            {
                                GlobVar.endX = GlobVar.durEventoMove;

                                if (GlobVar.endX != GlobVar.startX)
                                {

                                    int LimiteAux = Math.Abs(GlobVar.durEventoMove - GlobVar.iniEventoMove);

                                    if(LimiteAux < GlobVar.MinimumValueEvent)
                                    {
                                        GlobVar.durEventoMove = GlobVar.iniEventoMove + GlobVar.MinimumValueEvent;
                                    }

                                    if (GlobVar.iniEventoMove > GlobVar.durEventoMove)
                                    {
                                        plotEventos.UpdateEvent(GlobVar.durEventoMove, GlobVar.iniEventoMove,  GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                                    }
                                    else
                                    {
                                        plotEventos.UpdateEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                                    }
                                }
                            }
                            else if (this.isAnEndEvent)
                            {

                                GlobVar.startX = GlobVar.iniEventoMove;

                                int LimiteAux = Math.Abs(GlobVar.durEventoMove - GlobVar.iniEventoMove);

                                if (LimiteAux < GlobVar.MinimumValueEvent)
                                {
                                    GlobVar.durEventoMove = GlobVar.iniEventoMove + GlobVar.MinimumValueEvent;
                                }

                                if (GlobVar.iniEventoMove > GlobVar.durEventoMove)
                                {
                                    plotEventos.UpdateEvent(GlobVar.durEventoMove, GlobVar.iniEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                                }
                                else
                                {
                                    plotEventos.UpdateEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                                }
                            }
                            timerClick.Stop();
                            TelaClearAndReload();
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    isDrawing = false;
                    ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);

                }
                clickCount = 0;
                timer2.Stop();

                // Restaurar o cursor original e marca que o mouse foi liberado
                this.Cursor = originalCursor;
                isMouseDown = false;
            }
            catch
            {
            }
        }


        private void OpenglControl1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if(this.Cursor == Cursors.SizeAll)
                {
                    //toolTip1.SetToolTip(openglControl1, GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%");
                    //toolTip1.Show(GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%", openglControl1, lastMousePosition.X, lastMousePosition.Y);
                }
            }
            catch { }
        }

        private void UpdateLoc(string canal = null)
        {
            if (timer2.Enabled) {
                MouseLoc.Text = $"{stopwatch.Elapsed.TotalSeconds} seconds"; 
            }

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
            try
            {
                int alturaTela = (int)openglControl1.Height;

                for(int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    int ampli = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]);
                    int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) - 1;
                    int newAmpli = Convert.ToInt16(GlobVar.Amplitude[indexAmpli]);
                    GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"] = newAmpli;
                    float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(i));

                    GlobVar.scale[i] = scala;
                }
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);

                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);


                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            catch
            {
            }

        }
        private void minusAll_Click(object sender, EventArgs e)
        {
            try
            {
                int alturaTela = (int)openglControl1.Height;

                for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    int ampli = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]);
                    int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) + 1;
                    int newAmpli = Convert.ToInt16(GlobVar.Amplitude[indexAmpli]);
                    GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"] = newAmpli;
                    float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(i));

                    GlobVar.scale[i] = scala;
                }
                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);

                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);


                gl.Translate(0, 0, 1);
                UpdateInicioTela();
            }
            catch
            {
            }
        }

        private void plusLb1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSelected(sender);
                
                //timer1.Start();
                int alturaTela = (int)openglControl1.Height;

                int ampli = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"]);
                int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) - 1;
                int newAmpli = Convert.ToInt16(GlobVar.Amplitude[indexAmpli]);
                GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"] = newAmpli;
                float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(index));

                GlobVar.scale[index] = scala;

                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);

                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);


                gl.Translate(0, 0, 1);
                UpdateInicioTela();

                
            }
            catch
            {

            }
        }
        private void minusLb1_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateSelected(sender);
                //timer1.Start();
                int alturaTela = (int)openglControl1.Height;

                int ampli = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"]);
                int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) + 1;
                int newAmpli = Convert.ToInt16(GlobVar.Amplitude[indexAmpli]);
                GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"] = newAmpli;
                float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(index));

                GlobVar.scale[index] = scala;

                openglControl1.DoRender();
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);

                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);


                gl.Translate(0, 0, 1);
                UpdateInicioTela();

            }
            catch
            {

            }
        }

        private bool isScrollingRight = true;
        private bool crtlAtivo = false;
        private bool telaMovi = false;

        //Comecando a mexer nos KeyDown para alterar os eventos, usar o KeyUp para "replotar a tela"
        private void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        //private void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        //private void TelaPlotagem_KeyDown()
        {
            try
            {

                if (isAnEvent || isAnEndEvent || isAnStartEvent)
                {
                    if(e.KeyValue == 46)
                    {
                        plotEventos.DeleteEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                    }
                    else{
                    var rowteclaRapida = GlobVar.tbl_CadEvento.AsEnumerable()
                                                                .Where(row => row.Field<int>("TeclaRapida") == e.KeyValue);
                        if (rowteclaRapida.Any())
                        {
                            var tableTeclaRapida = rowteclaRapida.CopyToDataTable();
                            for (int i = 0; i < tableTeclaRapida.Rows.Count; i++)
                            {
                                if (!GlobVar.EventHasChange)
                                {
                                    int NewCodEvento = Convert.ToInt16(tableTeclaRapida.Rows[i]["CodEvento"]);
                                    plotEventos.ChangeEventType(GlobVar.CodCanalEvent, GlobVar.seqEvento, NewCodEvento);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (e.KeyValue == 162 || e.KeyValue == 163 || e.KeyValue == 131072 || e.KeyValue == 17)
                    {
                        crtlAtivo = true;
                    }
                    if(e.KeyValue == 37)
                    {
                        this.Close();
                    }
                    switch (e.KeyData)
                    {
                        case Keys.Left:
                            this.Close();
                            break;

                        case Keys.Escape:
                            this.Close();
                            break;


                        case Keys.D:
                            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                            {
                                camera.X += GlobVar.saltoTelas * GlobVar.SPEED;
                                if (camera.X > 0) hScrollBar1.Value += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                //UpdateInicioTela();
                                //TelaClearAndReload();

                            }

                            break;
                        case Keys.A:

                            if (GlobVar.indice > 0)
                            {
                                camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;
                                if (camera.X > 0 && hScrollBar1.Value != 0) hScrollBar1.Value -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                if (GlobVar.indiceNumero < 0)
                                {
                                    GlobVar.indiceNumero = 0;
                                    GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                                }

                                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                if(GlobVar.indice < 0)
                                {
                                    GlobVar.indice = 0;
                                    camera.X = 0;
                                }

                                GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                //UpdateInicioTela();
                                //TelaClearAndReload();

                            }
                        break;
                    }
                }
                int alturaTela = (int)openglControl1.Height;
                //gl.Translate(camera.X, 0, 1);
                TelaClearAndReload();
                hScrollBar1.Maximum = (GlobVar.matrizCanal.GetLength(1));
                hScrollBar1.Refresh();
                UpdateInicioTela();
                //Update();
            }
            catch (Exception ex)
            {
            }
        }
        private void TelaPlotagem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(e != null)
                {
                    if (isAnEvent || isAnEndEvent || isAnStartEvent)
                    {
                        if (e.KeyValue == 46)
                        {
                            TelaClearAndReload();
                        }
                        else
                        {
                            var rowteclaRapida = GlobVar.tbl_CadEvento.AsEnumerable()
                                                                    .Where(row => row.Field<int>("TeclaRapida") == e.KeyValue);
                            if (rowteclaRapida.Any())
                            {
                                if (GlobVar.EventHasChange)
                                {
                                    TelaClearAndReload();
                                    GlobVar.EventHasChange = false;
                                }
                            }
}
                    }

                    if (e.KeyValue == 162 || e.KeyValue == 163 || e.KeyValue == 131072 || e.KeyValue == 17)
                    {
                        crtlAtivo = false;
                    }
                }
            }
            catch
            {
            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                //hScrollBar1.Value = (int)e.NewValue;
                bool isRight = e.NewValue > hScrollBar1.Value;

                if (!isRight) // Se estiver indo para a esquerda
                {
                    camera.X -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                    GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                    GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                }
                else // Se estiver indo para a direita
                {
                    camera.X += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                    GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                    GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                }

                int alturaTela = (int)openglControl1.Height;
                TelaClearAndReload();
                gl.Translate(camera.X, 0, 1);
                UpdateInicioTela();
            }
            catch { }
        }

        private void tempoEmTela_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmpTela = tempoEmTela.Text;
            Match match = Regex.Match(tmpTela, @"\d+");
            if (match.Success)
            {
                GlobVar.segundos = int.Parse(match.Value);
            }
            GlobVar.tmpEmTelaNumerico = GlobVar.namosNumerico * GlobVar.segundos;
            GlobVar.finalTelaNumerico = (int)GlobVar.tmpEmTelaNumerico / (int)GlobVar.namosNumerico;
            GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;

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

        public void UpdateInicioTela()
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

            int indexLabel = 0;
            for (int i = 1; i <= GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                FieldInfo Label = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                if (Label != null)
                {
                    Label lb = (Label)Label.GetValue(this);
                    if (lb != null)
                    {
                        lb.Text = GlobVar.tbl_MontagemSelecionada.Rows[indexLabel]["AmplitudeMin"].ToString() + "μV";
                        indexLabel++;
                    }
                }
            }

         }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Tela_Plotagem_ResizeBegin(object sender, EventArgs e)
        {

        }

        public static int aux;

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
                //UpdateInicioTela();
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
            timer1.Start();
            UpdateSelected(sender);
            if (e.Button == MouseButtons.Right)
            {
                // Verifica se o clique do mouse ocorreu dentro de algum controle
                Control clickedControl = this.GetChildAtPoint(e.Location);
                UpdateLoc();
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

        //Menuzinho para mudar os eventos ou mudar na tela
        private void ContextMenuStripOpenGl_Opening(object sender, CancelEventArgs e)
        {
            try{
                //Toda vez que o context e aberto, ele "da um clear nos itens que ele tem e altera com base no que ele vai fazer"
                contextMenuStripOpenGl.Items.Clear();
                
                //Quero colocar para quando ele abrir dentro de um evento ele saber os tipos de evento que esse canal pode ter, e assim apenas mostrar esses 
                //ToolStripMenuItem dos eventos que podem ter, porem nao quero criar no form um para cada tipo de evento que podem ter, quero algo mais dinamico
                
                if (isAnEvent || isAnEndEvent || isAnStartEvent)
                {
                    plotEventos.ListOfEvent(); //Faz a lista dinamica
                    //Aqui eu coloquei as variaveis globais que eu preciso usar para pegar a informacao dos eventos que podem ser nesse canal
                    //GlobVar.CodEvento; GlobVar.seqEvento; GlobVar.CodCanalEvent; GlobVar.CodTipoCanalEvent;
                    for(int i = 0; i < GlobVar.listEventsCanHave.Count; i++)
                    {
                        var newMenuItem = new ToolStripMenuItem();
                        newMenuItem.Text = GlobVar.listEventsCanHave[i];
                        //newMenuItem.Size = new System.Drawing.Size(100, 27);
                        newMenuItem.Name = GlobVar.listEventsCanHave[i];
                        //newMenuItem.Click += NewMenuItem_Click;

                        contextMenuStripOpenGl.Items.Add(newMenuItem.Name);
                    }

                    for (int i = 0; i < contextMenuStripOpenGl.Items.Count; i++)
                    {
                        contextMenuStripOpenGl.Items[i].Size = new System.Drawing.Size(100, 27);
                        contextMenuStripOpenGl.Items[i].Click += NewMenuItem_Click;
                    }

                        contextMenuStripOpenGl.Items.Add(toolStripSeparator1); // separador
                        contextMenuStripOpenGl.Items.Add(Excluir);
                }
                else if (isA_BN_CPAP_BD){
                    if (GlobVar.nomeEvento.Equals("Bom dia"))
                    {
                        contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] {BomDiaExclui, toolStripSeparator1, LowPassFilterGl, HighPassFilterGl });
                    }
                    else if (GlobVar.nomeEvento.Equals("Boa noite"))
                    {
                        contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] {BoaNoiteExclui, toolStripSeparator1, LowPassFilterGl, HighPassFilterGl });
                    }
                    else if (GlobVar.nomeEvento.Equals("Início CPAP"))
                    {
                        contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] {InicioCPAPExclui, toolStripSeparator1, LowPassFilterGl, HighPassFilterGl });
                    }

                }
                else
                {
                    //Else seria quando ele abre fora de um evento
                    contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] { BomDia, BoaNoite, InicioCPAP, ExcluirBdBnCpap, toolStripSeparator1, LowPassFilterGl, HighPassFilterGl });
                }
            }
            catch { }

        }

        private void NewMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
                if (clickedItem != null)
                {
                    string nameEvent = clickedItem.Text;
                    var TableAux = GlobVar.tbl_CadEvento.AsEnumerable().Where(row => row.Field<string>("DescrEvento").Equals(nameEvent)).CopyToDataTable();
                    int newCodevento = Convert.ToInt16(TableAux.Rows[0]["CodEvento"]);

                    plotEventos.ChangeEventType(GlobVar.CodCanalEvent, GlobVar.seqEvento, newCodevento);

                    TelaClearAndReload();
                }
            }
            catch { }
        }

        private void BomDiaCpapBoaNoite_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
                if(clickedItem != null)
                {
                    int? valueCodCanal = null;

                    if (clickedItem.Text.Equals("Bom Dia"))
                    {
                        valueCodCanal = 19;
                    }
                    else if (clickedItem.Text.Equals("Boa Noite"))
                    {
                        valueCodCanal = 18;
                    }
                    else if (clickedItem.Text.Equals("Início CPAP"))
                    {
                        valueCodCanal = 50;
                    }
                    float aux = 0;
                    float auy = 0;
                    ConvertToOpenGLCoordinates(GlobVar.rightClickSave.X, GlobVar.rightClickSave.Y, out aux, out auy);
                    if(valueCodCanal != null)
                    {
                        plotEventos.CreatBomDiaCpapBoaNoite((int)aux, (int)valueCodCanal);
                    }

                    TelaClearAndReload();
                }
            }
            catch { }
        }
        private void ExcluiBomDiaCpapBoaNoite_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
                if (clickedItem != null)
                {
                    int? valueCodCanal = null;

                    if (clickedItem.Text.Equals("Excluir Bom Dia"))
                    {
                        valueCodCanal = 19;
                    }
                    else if (clickedItem.Text.Equals("Excluir Boa Noite"))
                    {
                        valueCodCanal = 18;
                    }
                    else if (clickedItem.Text.Equals("Excluir Inicio CPAP"))
                    {
                        valueCodCanal = 50;
                    }
                    if (valueCodCanal != null)
                    {
                        plotEventos.DeleteBomDiaCpapBoaNoite((int)valueCodCanal);
                    }

                    TelaClearAndReload();
                }
            }
            catch { }
        }


        private void DeletEventClick(object sender, EventArgs e)
        {
            try
            {
                plotEventos.DeleteEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                TelaClearAndReload();
            }
            catch { }
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
        private void UpdateSelected(object sender)
        {
            Control menu = sender as Control;
            if (menu != null)
            {
                string teste = menu.Name.Substring(6);
                FieldInfo labelText = typeof(Tela_Plotagem).GetField($"label{teste}", BindingFlags.Static | BindingFlags.Public);

                if(labelText != null)
                {
                    Label labell = (Label)labelText.GetValue(this);
                    if (labell is Label label)
                    {
                        if (label.Name.StartsWith("label"))
                        {
                            selectedLabelValue = label.Text;
                        }
                    }
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
        private void toolTripItemDropDown_OpeningNotch(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            if (clickedPanel is Panel panel)
            {
                UpdateMenuItems(panel, clickedItem.DropDownItems, panelNotchFilterStates[panel]);

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
        private string GetSelectedFilterItem(Panel panel, Dictionary<string, bool> filterStates)
        {

            if (filterStates.ContainsValue(true))
            {
                foreach (var kvp in filterStates)
                {
                    if (kvp.Value) // Se o valor no dicionário é true, o item está selecionado
                    {
                        return kvp.Key; // Retorna o nome do item selecionado
                    }
                }
            }
            return null; // Retorna null se nenhum item estiver selecionado ou se o painel não estiver no dicionário
        }

        private bool IsAnyFilterSelected(Dictionary<string, bool> filterStates)
        {
            foreach (var state in filterStates.Values)
            {
                if (state)
                {
                    return true;
                }
            }
            return false;
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            //try { 
                ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;

                if (clickedPanel is Panel panel)
                {
                    Dictionary<string, bool> filterStates;

                    //Faz a verificacao de qual filtro esta sendo aplicado
                    if (panelLowFilterStates.ContainsKey(panel) && ContainsFilter(panelLowFilterStates, clickedItem.Name))
                    {
                        Dictionary<string, bool> filterStatesHigh;

                        filterStates = panelLowFilterStates[panel];
                        filterStatesHigh = panelHighFilterStates[panel];
                        bool isAnyFilterSelected = IsAnyFilterSelected(filterStatesHigh);
                        //faz uma verificacao para saber se o outro dictionary tem alguma selecao, e caso tenha faca o filtro com essa informacao
                        if (isAnyFilterSelected)
                        {
                            string value = GetSelectedFilterItem(panel, filterStatesHigh);

                            if (value.Equals("NenhumHigh"))
                            {
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = DBNull.Value;

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
                                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                    LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                        LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                            //para pegar a informacao de um valor que esteja sendo aplicado no filtro que seja em Outro, pega do banco de dados, para aplicar o filtro corretamente
                            else if (value.Equals("outroHigh"))
                            {
                                Int16 hertzSelectH = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"]);
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = DBNull.Value;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                            else
                            {
                                float hertzSelectH;
                                if (value.Substring(6, 1).IsEqual("H"))
                                {
                                    hertzSelectH = Convert.ToInt16(value.Substring(5, 1));
                                }
                                else if (value.Substring(5, 1).IsEqual("0"))
                                {
                                    hertzSelectH = Convert.ToInt16(value.Substring(5, 2));
                                    hertzSelectH = hertzSelectH / 10;
                                }
                                else
                                {
                                    hertzSelectH = Convert.ToInt16(value.Substring(5, 2));
                                }
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = DBNull.Value;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }

                            }
                        }
                        if (GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] != DBNull.Value)
                        {
                            float NotchHertz = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"]);
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))),
                                NotchHertz,
                                10,
                                GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                        }
                    }
                    else if (panelHighFilterStates.ContainsKey(panel) && ContainsFilter(panelHighFilterStates, clickedItem.Name))
                    {
                        Dictionary<string, bool> filterStatesLow;

                        filterStates = panelHighFilterStates[panel];
                        filterStatesLow = panelLowFilterStates[panel];


                        bool isAnyFilterSelected = IsAnyFilterSelected(filterStatesLow);

                        if (isAnyFilterSelected)
                        {
                            string value = GetSelectedFilterItem(panel, filterStatesLow);

                            if (value.Equals("NenhumLow"))
                            {
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = DBNull.Value;
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
                                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                        LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                        float hertzSelect;
                                        if (clickedItem.Text.Substring(1, 1).Equals(","))
                                        {
                                            hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(2, 1));
                                            hertzSelect /= 10;
                                        }
                                        else
                                        {
                                            hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(0, 3));
                                        }
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                            else if (value.Equals("OutroLow"))
                            {
                                Int16 hertzSelectL = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"]);
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = DBNull.Value;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                        float hertzSelect;
                                        if (clickedItem.Text.Substring(1, 1).Equals(","))
                                        {
                                            hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(2, 1));
                                            hertzSelect /= 10;
                                        }
                                        else
                                        {
                                            hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(0, 3));
                                        }
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }

                            }
                            else
                            {
                                float hertzSelectL = Convert.ToInt16(value.Substring(5, 2));
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = DBNull.Value;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                        float hertzSelect;
                                        if (clickedItem.Text.Substring(1, 1).Equals(","))
                                        {
                                            hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(2, 1));
                                            hertzSelect /= 10;
                                        }
                                        else
                                        {
                                            hertzSelect = Convert.ToInt16(clickedItem.Text.Substring(0, 3));
                                        }
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                        }
                        if (GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] != DBNull.Value)
                        {
                        float NotchHertz = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"]);
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))),
                                NotchHertz,
                                10,
                                GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                        }

                    }
                    else if (panelNotchFilterStates.ContainsKey(panel) && ContainsFilter(panelNotchFilterStates, clickedItem.Name))
                    {
                        Dictionary<string, bool> filterStatesHigh;
                        Dictionary<string, bool> filterStatesLow;

                        filterStates = panelNotchFilterStates[panel];
                        filterStatesLow = panelLowFilterStates[panel];
                        float hertzSelectLow = 0;
                        bool isAnyFilterSelectedLow = IsAnyFilterSelected(filterStatesLow);

                        filterStatesHigh = panelHighFilterStates[panel];
                        float hertzSelectHigh = 0;
                        bool isAnyFilterSelectedHigh = IsAnyFilterSelected(filterStatesHigh);

                        if (isAnyFilterSelectedLow)
                        {
                            string value = GetSelectedFilterItem(panel, filterStatesLow);
                            if (value.Equals("NenhumLow"))
                            {
                                hertzSelectLow = 0;

                            }
                            else if (value.Equals("OutroLow"))
                            {
                                hertzSelectLow = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"]);

                            }
                            else
                            {
                                hertzSelectLow = Convert.ToInt16(value.Substring(5, 2));

                            }

                        } //Verifica se tem algum filtro aplicado de LowPass
                        if (isAnyFilterSelectedHigh)
                        {
                            string value = GetSelectedFilterItem(panel, filterStatesHigh);
                            if (value.Equals("NenhumHigh"))
                            {
                                hertzSelectHigh = 0;

                            }
                            else if (value.Equals("outroHigh"))
                            {
                                hertzSelectHigh = Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"]);
                            }
                            else
                            {
                                if (value.Substring(6, 1).IsEqual("H"))
                                {
                                    hertzSelectHigh = Convert.ToInt16(value.Substring(5, 1));
                                }
                                else if (value.Substring(5, 1).IsEqual("0"))
                                {
                                    hertzSelectHigh = Convert.ToInt16(value.Substring(5, 2));
                                    hertzSelectHigh = hertzSelectHigh / 10;
                                }
                                else
                                {
                                    hertzSelectHigh = Convert.ToInt16(value.Substring(5, 2));
                                }

                            }
                        } //Verifica se tem algum filtro aplicado de HighPass

                        if (hertzSelectLow == 0 && hertzSelectHigh == 0)
                        {
                            if (clickedItem.CheckOnClick == true)
                            {
                                int selec = GlobVar.grafSelected.IndexOf(index);
                                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                                if (clickedItem.Text.Equals("Nenhum"))
                                {
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = DBNull.Value;
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
                                                GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                    openglControl1.DoRender();
                                    plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                }
                            }
                        }
                        else if (hertzSelectLow != 0 && hertzSelectHigh == 0)
                        {
                            if (clickedItem.CheckOnClick == true)
                            {
                                int selec = GlobVar.grafSelected.IndexOf(index);
                                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectLow, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));


                                if (clickedItem.Text.Equals("Nenhum"))
                                {
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = DBNull.Value;
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
                                                GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                    openglControl1.DoRender();
                                    plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                }
                            }

                        }
                        else if (hertzSelectLow == 0 && hertzSelectHigh != 0)
                        {
                            if (clickedItem.CheckOnClick == true)
                            {
                                int selec = GlobVar.grafSelected.IndexOf(index);
                                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));


                                if (clickedItem.Text.Equals("Nenhum"))
                                {
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = DBNull.Value;
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
                                                GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                    openglControl1.DoRender();
                                    plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                }
                            }

                        }
                        else
                        {
                            if (clickedItem.CheckOnClick == true)
                            {
                                int selec = GlobVar.grafSelected.IndexOf(index);
                                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectLow, (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                if (clickedItem.Text.Equals("Nenhum"))
                                {
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = DBNull.Value;
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
                                                GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                    openglControl1.DoRender();
                                    plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                }
                            }

                        }
                    }

                    else
                    {
                        return;
                    }

                    UncheckAllItemsExceptSelected(clickedItem, filterStates);
                }               


            //}catch { }
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

        int index = 0;
        private void timerClick_Tick(object sender, EventArgs e)
        {
            if (isDrawing)
            {
                if(Cursor.Position.X >= (openglControl1.Width + openglControl1.Location.X))
                {
                    Cursor.Position = new Point(openglControl1.Width + openglControl1.Location.X, Cursor.Position.Y);
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            TelaClearAndReload();

            if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent))
            {
                if (isDrawing)
                {
                    plotEventos.DrawingAnEvent(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                }
            }
            else if (isAnEvent && (!this.isAnStartEvent || !this.isAnEndEvent))
            {
                if (isDrawing)
                {
                    plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                }
            }
            else if ((!isAnEvent && this.isAnStartEvent) || (!isAnEvent && this.isAnEndEvent))
            {
                if (isDrawing)
                {
                    plotEventos.DrawBordenInAnEvent(isDrawing, gl, GlobVar.desenhoLoc);
                }
            }
            //plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
            //openglControl1.Update();

        }

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

            }
            timer1.Stop();
        }
        public static int clickCount = 0;
        public static bool plotanu = false;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (isDrawing)
            {
                clickCount++;
            }
            int YAdjusted = Plotagem.EncontrarValorMaisProximo(GlobVar.desenhoLoc, musezin.Y);

            Stringao.Text = $"X: {musezin.X}Y: {musezin.Y}| Canal: {GlobVar.tbl_MontagemSelecionada.Rows[YAdjusted]["Legenda"]} | CodCanal: {GlobVar.CodCanal} | CodTipoCanal: {GlobVar.CodTipoCanalEvent} | InicioEvento: {isAnStartEvent}| Evento: {isAnEvent}| FimEvento: {isAnEndEvent} | Bd: {isA_BN_CPAP_BD}| MouseClick: {isDrawing} | Plotando: {plotanu} | Contador: {clickCount} | Ultimo Evento: {GlobVar.lastEvent} | SeqEvent: {GlobVar.seqEvento}";
        }
    }
}
