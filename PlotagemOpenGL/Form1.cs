using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using SharpGL;
using Size = System.Drawing.Size;
using Point = System.Drawing.Point;
using Vector3 = System.Numerics.Vector3;
using Pen = System.Drawing.Pen;
using Color = System.Drawing.Color;
using System.Text.RegularExpressions;
using PlotagemOpenGL.auxi;
using System.Collections.Generic;
using System.Reflection;
using MessageBox = System.Windows.MessageBox;
using System.ComponentModel;
using Accord.Math;
using Accord;
using System.Data;
using System.Linq;
using PlotagemOpenGL.Filtros;
using System.Diagnostics;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.auxi.FormsAuxi;
using PlotagemOpenGL.auxi.FormComentario;
using PlotagemOpenGL.auxi.FormLegenda;
using PlotagemOpenGL.FormesMenuPanels.InferiorSuperior;
using PlotagemOpenGL.FormesMenuPanels;
using Cyotek.Windows.Forms;
using System.Data.OleDb;
using Image = System.Drawing.Image;
using MessageBoxIcon = System.Windows.Forms.MessageBoxIcon;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PlotagemOpenGL.BD;
using System.Threading;
using PlotagemOpenGL.Hipnograma;
using PlotagemOpenGL.LaudoForm;
using System.Runtime.InteropServices;
//using KeyCode = UnityEngine.KeyCode;


namespace PlotagemOpenGL
{
    public partial class Tela_Plotagem : Form
    {
        public static OpenGL gl;
        public static Plotagem plotagem;
        public static Canais canais;
        private bool isInitialized = false;
        private Stopwatch stopwatch;
        public static string AdInf;

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

        private Rectangle pnGr;
        private Rectangle pnesta;
        private Rectangle pnimpre;
        private Rectangle pnPeri;
        private Rectangle hScrol;

        private Dictionary<Panel, Dictionary<string, bool>> panelLowFilterStates = new Dictionary<Panel, Dictionary<string, bool>>();
        private Dictionary<Panel, Dictionary<string, bool>> panelHighFilterStates = new Dictionary<Panel, Dictionary<string, bool>>();
        private Dictionary<Panel, Dictionary<string, bool>> panelNotchFilterStates = new Dictionary<Panel, Dictionary<string, bool>>();
        public static System.ComponentModel.ComponentResourceManager resources;

        // Declarando três cronômetros
        public static Stopwatch cronometro1 = new Stopwatch();
        public static Stopwatch cronometro2 = new Stopwatch();
        public static Stopwatch cronometro3 = new Stopwatch();
        public static Stopwatch cronometro4 = new Stopwatch();
        public static Stopwatch cronometroBand = new Stopwatch();
        public static Stopwatch cronometroBaixa = new Stopwatch();
        public static Stopwatch cronometroAlta = new Stopwatch();
        public static Stopwatch cronometroNotch = new Stopwatch();


        public static Point? prevPosition = null;
        public static ToolTip tooltip = new ToolTip();

        public static int qtdGrafics;

        public static Vector3 camera;

        [System.Runtime.InteropServices.DllImport("nvapi.dll", EntryPoint = "fake")]
        static extern int LoadNvApi64();

        [System.Runtime.InteropServices.DllImport("nvapi.dll", EntryPoint = "fake")]
        static extern int LoadNvApi32();
        // Constantes Win32
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_MAXIMIZE = 0xF030;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Chame este método para maximizar como se fosse o usuário clicando no botão!
        private void MaximizarComoBotao()
        {
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
        }
        private KeyChecker keyChecker;
        public static Tela_Plotagem Instance { get; private set; }
        public static bool videoIni = false;
        private System.Windows.Forms.Timer timerAposAbrir;

        public Tela_Plotagem()
        {
            try
            {
                // Obtém as dimensões da tela principal
                int larguraTela = 1920;
                int alturaTela = 1080;

                // Define o tamanho e a posição inicial do formulário
                this.StartPosition = FormStartPosition.Manual;
                this.Size = new Size((int)(larguraTela * 0.8), (int)(alturaTela * 0.8)); // 80% da largura e altura da tela
                this.Location = new Point((larguraTela - this.Width) / 2, (alturaTela - this.Height) / 2); // Centraliza o formulário na tela

                using (CarregandoAltMontagem telaLoad = new CarregandoAltMontagem())
                {
                    InitializeComponent();
                    AjustaNomesBut();
                    ConfigurarTooltips(painelComando); // Configura os tooltips para todos os botões do painel principal
                    toolTip2.SetToolTip(QuatroAnterior, "Voltar 1 página");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e}", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }

        public async Task InitializeAsync()
        {
            try {
                using (CarregandoAltMontagem telaLoad = new CarregandoAltMontagem())
                {
                    telaLoad.label1.Text = "Abrindo Exame";
                    telaLoad.label1.TextAlign = ContentAlignment.MiddleCenter;
                    telaLoad.realoctxt();
                    telaLoad.Show();

                    await Task.Delay(45);
                    telaLoad.AtualizarProgresso(10);

                    LeitorDiretorio.LeituraDiretorio();

                    await Task.Delay(45);
                    telaLoad.AtualizarProgresso(20);
                    LeituraBanco.BancoRead();

                    await Task.Delay(45);
                    telaLoad.AtualizarProgresso(30);
                    LeituraBanco.AlteraTable();
                    LeituraBanco.AjustaMontagem();
                    Leitura.QuantidadeCanais();
                    //Canais.LerCanais();
                    //Leitura.LerArquvo();
                    await Task.Delay(45);
                    telaLoad.AtualizarProgresso(40);

                    //Leitura.LeituraDat();
                    LeituraBanco.AjustaCadEvent(); // Esta ajustando os valores das teclas rapida para -1 caso o valor seja null, pois estava atrapalhando quando era null
                    await Task.Delay(45);

                    LeituraEmMatrizTeste.LeituraDat();
                    LeituraBanco.ArrumaTbl_Paginas();

                    LeituraEmMatrizTeste.referencias();
                    await Task.Delay(45);
                    telaLoad.AtualizarProgresso(50);

                    AdInf = GlobVar.tbl_DadosExame.Rows[0]["AdInf"].ToString();
                    ArrumarTbl_Estagios();
                    AjustaBotoesParaTipoExadeAdInf();

                    atualizaJanelaResumo();
                    SetStyle(ControlStyles.DoubleBuffer, true);
                    rectangleLoad();
                    await Task.Delay(45);

                    this.Resize += Tela_Plotagem_Resiz;
                    this.Resize += painelComando_Resiz;
                    this.Resize += Painel_resiz;
                    this.Resize += panelLb_Resiz;
                    this.Resize += DownPainel_Resiz;
                    this.Controls.Add(openglControl1);
                    painelExames.Paint += painelExames_Paint;
                    toolTip1 = new CustomToolTip();
                    await Task.Delay(45);
                    telaLoad.AtualizarProgresso(60);

                    formOriginalSize = this.Size;
                    painelOriginalSize = painelExames.Size;
                    painelComandoOriginalSize = painelComando.Size;
                    UpdateStyles();
                    await Task.Delay(25);
                    telaLoad.AtualizarProgresso(70);

                    qtdGraficos.Text = $"{GlobVar.tbl_MontagemSelecionada.Rows.Count.ToString()}";
                    GlobVar.ponteiroVideo = GlobVar.namos * (GlobVar.segundos / 2);
                    GlobVar.FundoColor = new int[] { 255, 255, 255, 255 };
                    openglControl1.Focus();
                    await Task.Delay(25);
                    telaLoad.AtualizarProgresso(80);

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
                    GlobVar.Amplitude = [5, 25, 50, 75, 80, 100, 150, 200, 250, 300, 350, 400, 450, 500, 600, 650, 700, 800, 900, 1000, 1250, 1500, 1750, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000, 15000, 20000];
                    GlobVar.ultimaPag = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["Ultima_Pagina"]) - 1; //ERROO ESTA AQ
                    await Task.Delay(25);
                    GlobVar.posiValues = LeituraEmMatrizTeste.Referencia(14);
                    GlobVar.fcValues = LeituraEmMatrizTeste.Referencia(67);
                    telaLoad.AtualizarProgresso(90);
                    //GlobVar.ultimaPag = 2;
                    load();
                    camera.X = 0.0f;
                    camera.Y = 0.0f;
                    camera.Z = 1.0f;
                    velocidadeScroll.SelectedIndex = 0;
                    stopwatch = new Stopwatch();
                    this.MouseUp += Form1_MouseUp;
                    this.MouseMove += openglControl1_MouseMove;
                    painelExames.MouseLeave += panel_MouseLeave;
                    painelComando.MouseEnter += panel_MouseLeave;
                    openglControl1.MouseEnter += panel_MouseLeave;
                    timerAvanca.Tick += TimerAvanca_Tick;
                    timerAndaUmaPag.Tick += TimerAndaUmaPag_Tick;
                    timerVoltaUmaPag.Tick += TimerVoltaUmaPag_Tick;
                    timerRetrocede.Tick += TimerRetrocede_Tick;
                    await Task.Delay(25);
                    telaLoad.AtualizarProgresso(91);

                    MontagemBox.Items.Clear();
                    foreach (DataRow row in GlobVar.tbl_Montagem.Rows)
                    {
                        MontagemBox.Items.Add(row["DescrMontagem"].ToString());
                    }
                    string selecao = GlobVar.tbl_MontGrav.Rows[0]["NomeMontagem"].ToString();
                    int intdex = MontagemBox.Items.IndexOf(selecao);
                    if (intdex != -1) 
                    {
                        MontagemBox.SelectedIndex = MontagemBox.Items.IndexOf(selecao);
                    }
                    else
                    {
                        int codiguin = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);
                        // Encontrar a linha correspondente na tabela
                        DataRow rowmt = GlobVar.tbl_Montagem.AsEnumerable()
                            .FirstOrDefault(row => row.Field<int>("CodMontagem") == codiguin);

                        // Verificar se encontrou uma linha antes de acessar
                        if (rowmt != null)
                        {
                            string seleca = rowmt["DescrMontagem"].ToString();
                            MontagemBox.SelectedIndex = MontagemBox.Items.IndexOf(seleca);

                        }
                    }
                    foreach (Control panel in painelExames.Controls)
                    {
                        if (panel is Panel)
                        {
                            panel.MouseDown += Panel_MouseDown;
                            panel.MouseMove += Panel_MouseMove;
                            panel.MouseUp += Panel_MouseUp;
                            panel.MouseEnter += panel_MouseEnter;
                            //panel.MouseLeave += panel_MouseLeave;
                            panel.Click += Panel_Click;
                            foreach (Control lable in panel.Controls)
                            {
                                if (lable is Label)
                                {
                                    lable.MouseEnter += panel_MouseEnter;
                                    lable.MouseDown += Panel_MouseDown;
                                    lable.MouseMove += Panel_MouseMove;
                                    lable.MouseUp += Panel_MouseUp;
                                }
                            }
                        }
                    }
                    await Task.Delay(25);
                    telaLoad.AtualizarProgresso(93);

                    toolTip1.SetToolTip(openglControl1, "Teste");
                    this.KeyPreview = true; // Necessário para capturar as teclas no nível do formulário.

                    Play_OpenGl();
                    falsoClick();
                    AjustarFonteDosLabels();
                    AjustarBotoesMinusEPlus();
                    InicializarButtonForm();
                    await Task.Delay(25);
                    telaLoad.AtualizarProgresso(94);

                    timer3.Start();
                    tempoEmTela.SelectedIndex = 5;
                    isInitialized = true;

                    if (GlobVar.ultimaPag != 0)
                    {
                        abreUltimaPaginaFechada();
                    }
                    Instance = this; // Define a instância estática
                    this.FormClosing += Tela_Plotagem_FormClosed;
                    GlobVar.areaCarregadaAltMont = GlobVar.matrizCanal.GetLength(1);
                    await Task.Delay(25);
                    telaLoad.AtualizarProgresso(100);

                    chamarTelinhaVid();
                    AnaliseCO2.GetPrimeiroCO2(Path.GetFileNameWithoutExtension(GlobVar.textFile));
                    F_Idioma();

                    this.WindowState = FormWindowState.Maximized;
                    // Instancia o Timer
                    timerAposAbrir = new System.Windows.Forms.Timer();
                    timerAposAbrir.Interval = 2; // 2 msft
                    timerAposAbrir.Tick += TimerAposAbrir_Tick;
                    // Obtém as dimensões da tela principal
                    timerAposAbrir.Start();


                    GlobVar.ConnectionBDdat = new OleDbConnection($@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};");
                    GlobVar.ConnectionBDdat.Open();


                    Canais.ajustaIniFimEx();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.ToString()}", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                this.Close();
                GlobVar.ConnectionBDdat.Close();

            }
        }
        private void TimerAposAbrir_Tick(object sender, EventArgs e)
        {

            toolStripMenuItem8.Checked = false;
            toolStripMenuItem8.PerformClick();

            timerAposAbrir.Stop(); // Para garantir que roda só uma vez

        }
        void AjustaNomesBut()
        {
            Amplislaoq.Name = "Mede Amplitude e frequencia do período marcado";
            MinimoEvento.Name = "Considerar duração mínima dos eventos";
            EventoUmClick.Name = "Evento de um clique";
            AnaliseAutomatica.Name = "Análise Automática";
            Video.Name = "Vídeo";
            Profile.Name = "Dados do Paciente";
            PainelPrinters.Name = "PainelPrinters";
            ImprimeLaudo.Name = "Laudo do Paciente";
            ImprimeSele.Name = "Páginas selecionadas para impressão";
            CopiaTela.Name = "Copia tela em arquivo";
            ImprimeTela.Name = "Imprimir tela";
            ImprimePagina.Name = "Selecionar Página para impressão posterior";
            OcultarPrinter.Name = "Ocultar";
            ImprimeTudo.Name = "Imprimir exame";
            Avanca.Name = "Avança o tempo mostrado na tela";
            AndaUmaPag.Name = "Avança 1 segundo";
            Pausa.Name = "Pára o Avanço/Retrocesso";
            VoltaUmaPag.Name = "Retrocede 1 segundo";
            OcultaTempo.Name = "Oculta";
            Retrocede.Name = "Retrocede o tempo mostrado na tela";
            TempoTimerAndar.Name = "TempoTimerAndar";
            TresProxima.Name = "";
            DuasProxima.Name = "";
            MarcaNoGraf.Name = "";
            MarcaDAguia.Name = "";
            QuatroProxima.Name = "";
            UmaProxima.Name = "";
            Atual.Name = "";
            UmaAnterior.Name = "";
            DuasAnterior.Name = "";
            TresAnterior.Name = "";
            OcultaPanelLoc.Name = "Oculta";
            QuatroAnterior.Name = "";
            MarcarR.Name = "REM";
            Marcar3.Name = "N3";
            Marcar2.Name = "N2";
            Marcar1.Name = "N1";
            Marcar0.Name = "Acordado";
            Proximo3.Name = "Move para a época seguinte com estágio 3";
            Anterior3.Name = "Move para a época anterior com estágio 3";
            ProximoDif.Name = "Move para o estágio seguinte diferente do atual";
            AnteriorDif.Name = "Move para o estágio anterior diferente da atual";
            ProximoR.Name = "Move para a época seguinte com estágio REM";
            AnteriorR.Name = "Move para a época anterior com estágio REM";
            Proximo2.Name = "Move para a época seguinte com estágio 2";
            Anterior2.Name = "Move para a época anterior com estágio 2";
            Proximo1.Name = "Move para a época seguinte com estágio 1";
            Anterior1.Name = "Move para a época anterior com estágio 1";
            Proximo0.Name = "Move para a época seguinte com estágio 0";
            OcultaPA.Name = "Oculta";
            Anterior0.Name = "Move para a época anterior com estágio 0";
            Cpap.Name = "Move para o início do CPAP";
            ProximoDes.Name = "Move para a dessaturação seguinte";
            AnteriorDes.Name = "Move para a dessaturação anterior";
            BaNotche.Name = "Move para o Boa Noite";
            BaDia.Name = "Move para o Bom Dia";
            ProximoComentario.Name = "Move para o comentário seguinte";
            AnteriorComentario.Name = "Move para o comentário anterior";
            ProximoRonco.Name = "Move para o evento de ronco seguinte";
            AnteriorRonco.Name = "Move para o evento de ronco anterior";
            ProximoPerna.Name = "Move para o evento de PLM seguinte";
            AnteriorPerna.Name = "Move para o evento de PLM anterior";
            ProximoCardio.Name = "Move para o evento de cardíaco seguinte";
            AnteriorCardio.Name = "Move para o evento de cardíaco anterior";
            ProximoAcordar.Name = "Move para o evento de despertar seguinte";
            AnteriorAcordar.Name = "Move para o evento de despertar anterior";
            ProximoPulmao.Name = "Move para apneia seguinte";
            button20.Name = "Ocultar";
            AnteriorPulmao.Name = "Move para apneia anterior";
            N.Name = "NREM";
            T.Name = "Transicional";
            ProximoT.Name = "Move para a época seguinte com estágio T";
            AnteriorT.Name = "Move para a época anterior com estágio T";
            ProximoN.Name = "Move para a época seguinte com estágio N";
            AnteriorN.Name = "Move para a época anterior com estágio N";

        }

        void F_Idioma()
        {
            string filename = @"C:\Temp\lngPortugues.txt";

            if (File.Exists(filename))
            {
                GlobVar.g_Traducoes = File.ReadLines(filename).FirstOrDefault() ?? "";
            }
            else
            {
                GlobVar.g_Traducoes = "";
            }
        }

        void ArrumarTbl_Estagios()
        {
            if (AdInf.Equals("A"))
            {
                // Busca a linha com Estagio == 4 (se houver)
                var row = GlobVar.tbl_Estagios.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("Estagio") == 4);

                // Se encontrou, remove do DataTable
                if (row != null)
                    GlobVar.tbl_Estagios.Rows.Remove(row);
            }
            else if (AdInf.Equals("I"))
            {
                // Se NÃO existe Estagio == 4, cria uma nova linha
                if (!GlobVar.tbl_Estagios.AsEnumerable().Any(r => r.Field<int>("Estagio") == 4))
                {
                    DataRow newRow = GlobVar.tbl_Estagios.NewRow();
                    newRow["Estagio"] = 4;
                    newRow["Legenda"] = "NREM";
                    newRow["Cor"] = 4227072;
                    newRow["Ordem"] = 7;
                    newRow["OrdemA"] = 6;
                    newRow["OrdemC"] = 7;
                    newRow["OrdemB"] = 4;

                    GlobVar.tbl_Estagios.Rows.Add(newRow);
                }
            }
        }
        public void atualizaJanelaResumo()
        {
            try
            {
                // Limpa os itens existentes para evitar duplicações
                JanelaSeparada.DropDownItems.Clear();
                // Percorre todas as linhas da tabela tbl_JanelaResumo
                foreach (DataRow row in GlobVar.tbl_JanelaResumo.Rows)
                {
                    // Cria um novo ToolStripMenuItem
                    var menuItem = new ToolStripMenuItem
                    {
                        Name = $"menuItem_{row["DescrJanela"]}", // Define o nome com base no DescrJanela
                        Text = row["DescrJanela"].ToString(),    // Define o texto do item
                        Tag = Convert.ToInt32(row["CodJanela"])  // Armazena a linha como Tag para referência futura
                    };

                    JanelaSeparada.DropDownItems.Add(menuItem);

                    menuItem.Click += JanelaSeparada_Click;
                }

                JanelaPrincipal.DropDownItems.Clear();
                // Percorre todas as linhas da tabela tbl_JanelaResumo
                foreach (DataRow row in GlobVar.tbl_JanelaResumo.Rows)
                {
                    // Cria um novo ToolStripMenuItem
                    var menuItem = new ToolStripMenuItem
                    {
                        Name = $"menuItem_{row["DescrJanela"]}", // Define o nome com base no DescrJanela
                        Text = row["DescrJanela"].ToString(),    // Define o texto do item
                        Tag = Convert.ToInt32(row["CodJanela"])  // Armazena a linha como Tag para referência futura
                    };

                    // Adiciona o ToolStripMenuItem aos menus
                    JanelaPrincipal.DropDownItems.Add(menuItem);
                }
            }
            catch (Exception ex)
            {
                // Exibe uma mensagem de erro caso algo dê errado
                MessageBox.Show($"Erro ao atualizar o menu: {ex.Message}", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
            }
        }
        public static HipnogramaForm Janela;

        public void JanelaSeparada_Click(object sender, EventArgs e)
        {
            // Verifica se o sender é um ToolStripMenuItem
            if (sender is ToolStripMenuItem menuItem)
            {
                // Obtém o valor do Tag do ToolStripMenuItem
                if (menuItem.Tag is int a)
                {
                    try
                    {
                        Janela = new HipnogramaForm(a);
                        Janela.Show(this);
                    }
                    catch
                    {
                        MessageBox.Show(
                            "O Exame está com estágio diferente do padrão para o tipo de exame, por favor ajuste.",
                            "Erro de Estágio",
                            (MessageBoxButton)MessageBoxButton.OK,
                            (MessageBoxImage)MessageBoxIcon.Error
                        );
                    }
                }
                else
                {                }
            }
        }
        FormLaudo laudinho;
        private void ImprimeLaudo_Click(object sender, EventArgs e)
        {
            laudinho = new FormLaudo();
            laudinho.Show(this);
        }

        public void chamarTelinhaVid()
        {
            // Verifica se telinha e tbl_ArqVideo não são nulos
            if (iCelera.telinha != null && GlobVar.tbl_ArqVideo != null)
            {
                // Verifica se o DataTable possui ao menos uma linha
                if (GlobVar.tbl_ArqVideo.Rows.Count > 0)
                {
                    // Verifica o valor de TickIni
                    if (Convert.ToInt32(GlobVar.tbl_ArqVideo.Rows[0]["TickIni"]) != 0)
                    {
                        iCelera.telinha.Show();
                        iCelera.telinha.videoCarregado();
                        iCelera.telinha.videoPlayer.Ctlcontrols.pause();
                        videoIni = true;
                    }
                    else if (Convert.ToInt32(GlobVar.tbl_ArqVideo.Rows[0]["TickIni"]) == 0)
                    {
                        iCelera.telinha.Close();
                        videoIni = true;
                    }
                }
                else
                {
                    // Caso não existam linhas no DataTable
                    iCelera.telinha.Close();
                    videoIni = false; // Ou mantenha o comportamento desejado
                }
            }
        }
        public void Video_Click(Object sender, EventArgs e)
        {
            if (iCelera.telinha != null && GlobVar.tbl_ArqVideo != null)
            {
                iCelera.telinha.Show();
                iCelera.telinha.videoPlayer.Ctlcontrols.pause();
                //iCelera.telinha.videoCarregado();
            }
        }
        public void falsoClick()
        {
            keyChecker = new KeyChecker();

            // Ação para a tecla Left
            keyChecker.LeftKeyPressed += () =>
            {
                tecla = "Seta esquerda.";
                // Adicione aqui a lógica para simular o clique no botão A
                var enterKeyEvent = new KeyEventArgs(Keys.A); // '\r' representa o Enter

                //var fakeScroll = new MouseEventArgs(MouseButtons.None, 0, 0, 0, -120);
                //OpenglControl1_MouseWheel(openglControl1, fakeScroll);

                TelaPlotagem_KeyDown(openglControl1, enterKeyEvent);
                TelaClearAndReload();

            };

            // Ação para a tecla Right
            keyChecker.RightKeyPressed += () =>
            {
                tecla = "Seta direita.";
                // Adicione aqui a lógica para simular o clique no botão D
                var enterKeyEvent = new KeyEventArgs(Keys.D); // '\r' representa o Enter

                //var fakeScroll = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 120);
                //OpenglControl1_MouseWheel(openglControl1, fakeScroll);

                TelaPlotagem_KeyDown(openglControl1, enterKeyEvent);
                TelaClearAndReload();  
            };
            // Ação para a tecla Left
            keyChecker.UpKeyPressed += () =>
            {
                tecla = "Seta esquerda.";
                // Adicione aqui a lógica para simular o clique no botão A
                var enterKeyEvent = new KeyEventArgs(Keys.W); // '\r' representa o Enter

                TelaPlotagem_KeyDown(openglControl1, enterKeyEvent);
                TelaClearAndReload();

            };

            // Ação para a tecla Right
            keyChecker.DownKeyPressed += () =>
            {
                tecla = "Seta direita.";
                // Adicione aqui a lógica para simular o clique no botão D
                var enterKeyEvent = new KeyEventArgs(Keys.S); // '\r' representa o Enter

                TelaPlotagem_KeyDown(openglControl1, enterKeyEvent);
                TelaClearAndReload();
            };

        }
        public void abreUltimaPaginaFechada()
        {
            int pagina = GlobVar.ultimaPag;
            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();

            int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]);

            if (pagina < 0) { pagina = 0; }
            if (pagina > (maximoPossivel / 30)) { pagina = maximoPossivel / 30; }

            string telaPagText = "000";
            if (pagina >= 100)
            {
                telaPagText = $"{pagina}";
            }
            else if (pagina >= 10)
            {
                telaPagText = $"0{pagina}";
            }
            else
            {
                if (pagina < 0) { pagina = 0; }
                telaPagText = $"00{pagina}";
            }
            ptsEmTela.Text = $"{telaPagText}";

            int newloc = pagina * 30;

            camera.X = newloc * GlobVar.namos;

            GlobVar.indice = newloc * GlobVar.namos;
            GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);

            GlobVar.indiceNumero = newloc * GlobVar.namosNumerico;
            GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);

            GlobVar.ponteiroVideo = GlobVar.indice + (GlobVar.namos * (GlobVar.segundos / 2));

            foiencontradoumUltimo = false;
            foiencontradoumUltimo = false;

            int alturaTela = (int)openglControl1.Height;
            //gl.Translate(camera.X, 0, 1);
            TelaClearAndReload();
            UpdateInicioTela();

        }
        private void AnaliseAutomatica_Click(object sender, EventArgs e)
        {
            AnaliseAuto autoAnalise = new AnaliseAuto();
            autoAnalise.Owner = this;
            autoAnalise.TopMost = true;
            autoAnalise.Show();
            TelaClearAndReload();
            UpdateInicioTela();

        }

        private void Tela_Plotagem_FormClosed(object sender, FormClosingEventArgs e)
        {
            GlobVar.ultimaPag = Convert.ToInt32(ptsEmTela.Text);

            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                OleDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Adicione uma condição WHERE para atualizar uma linha específica
                    string sql = "UPDATE tbl_DadosExame SET Ultima_Pagina = @ultimaPag WHERE CodPaciente = @CodPaciente";

                    using (OleDbCommand command = new OleDbCommand(sql, connection, transaction))
                    {
                        command.Parameters.Add("@ultimaPag", OleDbType.Integer).Value = GlobVar.ultimaPag + 1;
                        command.Parameters.Add("@CodPaciente", OleDbType.Integer).Value = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CodPaciente"]);

                        int rowsAffected = command.ExecuteNonQuery();
                        if(rowsAffected >0){
                            transaction.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            keyChecker.Stop();

            GlobVar.eventosUpdate.Clear();
            GlobVar.eventos.Clear();
            GlobVar.tbl_CadCanal.Clear();
            GlobVar.tbl_TipoCanal.Clear();
            GlobVar.tbl_EventoTipoCanal.Clear();

            GlobVar.tbl_Montagem.Clear();
            GlobVar.tbl_MontCanal.Clear();
            GlobVar.tbl_MontGrav.Clear();
            GlobVar.tbl_TipoExame.Clear();
            GlobVar.tbl_MontagemSelecionada.Clear();
            GlobVar.tbl_CadTipoCanal.Clear();
            GlobVar.tbl_CadEvento.Clear();
            GlobVar.tbl_Comentarios.Clear();
            GlobVar.tbl_DadosExame.Clear();
            GlobVar.tbl_Paginas.Clear();
            GlobVar.tbl_ResumoExame.Clear();
            GlobVar.tbl_SelImpressao.Clear();
            GlobVar.tbl_SeqEvento.Clear();
            GlobVar.tbl_ArqVideo.Clear();
            GlobVar.tbl_Estagios.Clear();

            GlobVar.eventosUpdate.Reset();
            GlobVar.eventos.Reset();
            GlobVar.tbl_CadCanal.Reset();
            GlobVar.tbl_TipoCanal.Reset();
            GlobVar.tbl_EventoTipoCanal.Reset();

            GlobVar.tbl_Montagem.Reset();
            GlobVar.tbl_MontCanal.Reset();
            GlobVar.tbl_MontGrav.Reset();
            GlobVar.tbl_TipoExame.Reset();
            GlobVar.tbl_MontagemSelecionada.Reset();
            GlobVar.tbl_CadTipoCanal.Reset();
            GlobVar.tbl_CadEvento.Reset();
            GlobVar.tbl_Comentarios.Reset();
            GlobVar.tbl_DadosExame.Reset();
            GlobVar.tbl_Paginas.Reset();
            GlobVar.tbl_ResumoExame.Reset();
            GlobVar.tbl_SelImpressao.Reset();
            GlobVar.tbl_SeqEvento.Reset();
            GlobVar.tbl_ArqVideo.Reset();
            GlobVar.tbl_Estagios.Reset();

            GlobVar.eventosUpdate.Dispose();
            GlobVar.eventos.Dispose();

            GlobVar.tbl_CadCanal.Dispose();
            GlobVar.tbl_TipoCanal.Dispose();
            GlobVar.tbl_EventoTipoCanal.Dispose();
            GlobVar.tbl_Montagem.Dispose();
            GlobVar.tbl_MontCanal.Dispose();
            GlobVar.tbl_MontGrav.Dispose();
            GlobVar.tbl_TipoExame.Dispose();
            GlobVar.tbl_MontagemSelecionada.Dispose();
            GlobVar.tbl_CadTipoCanal.Dispose();
            GlobVar.tbl_CadEvento.Dispose();
            GlobVar.tbl_Comentarios.Dispose();
            GlobVar.tbl_DadosExame.Dispose();
            GlobVar.tbl_Paginas.Dispose();
            GlobVar.tbl_ResumoExame.Dispose();
            GlobVar.tbl_SelImpressao.Dispose();
            GlobVar.tbl_SeqEvento.Dispose();
            GlobVar.tbl_ArqVideo.Dispose();
            GlobVar.tbl_Estagios.Dispose();

            GlobVar.tbl_CadCanal.Clear();
            GlobVar.tbl_MontCanal.Clear();
            GlobVar.tbl_Montagem.Clear();
            GlobVar.tbl_TipoExame.Clear();
            GlobVar.tbl_CadTipoCanal.Clear();
            GlobVar.tbl_CadEvento.Clear();
            GlobVar.tbl_EventoTipoCanal.Clear();
            GlobVar.tbl_TipoCanal.Clear();

            GlobVar.tbl_HipnoGrupos.Clear();
            GlobVar.tbl_HipnoSubGrupos.Clear();
            GlobVar.tbl_JanelaResumoItens.Clear();
            GlobVar.tbl_JanelaResumo.Clear();

            GlobVar.tbl_CadCanal.Dispose();
            GlobVar.tbl_MontCanal.Dispose();
            GlobVar.tbl_Montagem.Dispose();
            GlobVar.tbl_TipoExame.Dispose();
            GlobVar.tbl_CadTipoCanal.Dispose();
            GlobVar.tbl_CadEvento.Dispose();
            GlobVar.tbl_EventoTipoCanal.Dispose();
            GlobVar.tbl_TipoCanal.Dispose();

            GlobVar.tbl_HipnoGrupos.Dispose();
            GlobVar.tbl_HipnoSubGrupos.Dispose();
            GlobVar.tbl_JanelaResumoItens.Dispose();
            GlobVar.tbl_JanelaResumo.Dispose();


            iCelera ic = new iCelera();
            ic.Show();

            iCelera.telinha.Hide();
            this.Dispose();
            this.Close();
            GlobVar.ConnectionBDdat.Close();

        }
        //Metodo para inicializar os rectangle para fazer a realoc deles quando maximizado a tela
        private void rectangleLoad()
        {
            pnGr = new Rectangle(pnGrand.Location, pnGrand.Size);
            pnesta = new Rectangle(pnEstagio.Location, pnEstagio.Size);
            pnimpre = new Rectangle(pnImpressao.Location, pnImpressao.Size);
            pnPeri = new Rectangle(pnPeriodos.Location, pnPeriodos.Size);
            hScrol = new Rectangle(hScrollBar1.Location, hScrollBar1.Size);

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
            /*
            GlobVar.indice = GlobVar.namos * GlobVar.segundos * GlobVar.ultimaPag;
            GlobVar.maximaVect = GlobVar.indice * GlobVar.segundos;
            GlobVar.indiceNumero = GlobVar.segundos * GlobVar.ultimaPag * GlobVar.namosNumerico;
            GlobVar.maximaNumero = GlobVar.indiceNumero * GlobVar.segundos;
            camera.X = GlobVar.indice;
            UpdateInicioTela();
            */
            //Verificar depois o que e feito para abrir na ultima pagina aberta
            //ptsEmTela vai ser usado para mostrar a pagina que esta, e tambem usado para mudar a pagina
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            int pagina = GlobVar.ultimaPag;
            string telaPagText = "000";
            if (pagina >= 10)
            {
                pagina++;
                telaPagText = $"0{pagina}";
            }
            else if (pagina >= 100)
            {
                pagina++;
                telaPagText = $"{pagina}";
            }
            else
            {
                pagina++;
                telaPagText = $"00{pagina}";
            }
            ptsEmTela.Text = $"{telaPagText}";




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
                            { "NenhumLowGl", false },
                            { "hertz70Gl", false },
                            { "hertz50Gl", false },
                            { "hertz40Gl", false },
                            { "hertz35Gl", false },
                            { "hertz30Gl", false },
                            { "hertz25Gl", false },
                            { "hertz20Gl", false },
                            { "hertz15Gl", false },
                            { "hertz10Gl", false },
                            { "hertz5Gl", false },
                            { "OutroLowGl", false }
                        };

                        panelHighFilterStates[pn] = new Dictionary<string, bool>
                        {
                            { "NenhumHighGl", false },
                            { "outroHighGl", false },
                            { "hertz10HGl", false },
                            { "Hertz7Gl", false },
                            { "hertz5HGl", false },
                            { "hertz3Gl", false },
                            { "hertz1Gl", false },
                            { "hertz07Gl", false },
                            { "hertz05Gl", false },
                            { "hertz03Gl", false },
                            { "hertz01Gl", false }
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

            // Supondo que GlobVar.Amplitude seja um array de strings
            // Adiciona os itens de GlobVar.Amplitude ao DropDownItems
            Amplitude.DropDownItems.AddRange(Array.ConvertAll(GlobVar.Amplitude, item =>
            {
                // Formata o valor float com duas casas decimais
                string formattedItem = item.ToString();

                // Cria o ToolStripMenuItem
                ToolStripMenuItem menuItem = new ToolStripMenuItem
                {
                    Name = formattedItem,
                    Text = formattedItem,
                    Size = new System.Drawing.Size(148, 26) // Ajuste o tamanho conforme necessário
                };
                menuItem.Click += AmpliMenus_Click;
                // Ação de clique para o menuItem
                menuItem.Click += (sender, e) =>
                {

                };

                return menuItem;
            }));

            UpdateFilterStates();
        }
        public void UpdateFilterStates()
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
                if (lowHertz.Value == 70) panelLowFilterStates[panel]["hertz70Gl"] = true;
                else if (lowHertz.Value == 50) panelLowFilterStates[panel]["hertz50Gl"] = true;
                else if (lowHertz.Value == 40) panelLowFilterStates[panel]["hertz40Gl"] = true;
                else if (lowHertz.Value == 35) panelLowFilterStates[panel]["hertz35Gl"] = true;
                else if (lowHertz.Value == 30) panelLowFilterStates[panel]["hertz30Gl"] = true;
                else if (lowHertz.Value == 25) panelLowFilterStates[panel]["hertz25Gl"] = true;
                else if (lowHertz.Value == 20) panelLowFilterStates[panel]["hertz20Gl"] = true;
                else if (lowHertz.Value == 15) panelLowFilterStates[panel]["hertz15Gl"] = true;
                else if (lowHertz.Value == 10) panelLowFilterStates[panel]["hertz10Gl"] = true;
                else if (lowHertz.Value == 5) panelLowFilterStates[panel]["hertz5Gl"] = true;
                else panelLowFilterStates[panel]["OutroLowGl"] = true;
            }
            else
            {
                panelLowFilterStates[panel]["NenhumLowGl"] = true;
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
                if (highHertz.Value == 10) panelHighFilterStates[panel]["hertz10HGl"] = true;
                else if (highHertz.Value == 7) panelHighFilterStates[panel]["Hertz7Gl"] = true;
                else if (highHertz.Value == 5) panelHighFilterStates[panel]["hertz5HGl"] = true;
                else if (highHertz.Value == 3) panelHighFilterStates[panel]["hertz3Gl"] = true;
                else if (highHertz.Value == 1) panelHighFilterStates[panel]["hertz1Gl"] = true;
                else if (highHertz.Value == 0.7) panelHighFilterStates[panel]["hertz07Gl"] = true;
                else if (highHertz.Value == 0.5) panelHighFilterStates[panel]["hertz05Gl"] = true;
                else if (highHertz.Value == 0.3) panelHighFilterStates[panel]["hertz03Gl"] = true;
                else panelHighFilterStates[panel]["outroHighGl"] = true;
            }
            else
            {
                panelHighFilterStates[panel]["NenhumHighGl"] = true;
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

        private Panel movingPanel = null;
        private Panel tempLabel = null; // Usando Panel para a movimentação
        private const int borderWidth = 6; // Largura da borda sensível para redimensionamento
        private Rectangle tempRec;
        private int initialPanelY;
        private System.Drawing.Point mouseDownLocation;
        private bool isMoving = false;
        public bool isOnBottomBorder = false;
        public bool isOnTopBorder = false;
        private bool mouseIsDown = false;
        private bool isResizing = false;
        private int originalHeight;
        private int originalTop;
        private int originalBut;

        private void painelExames_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            if (isMoving)
            {
                using (Pen pen = new Pen(Color.Black, 2))
                {
                    e.Graphics.DrawRectangle(pen, tempRec);
                }
            }
        }
        private OverlayForm overlayForm;
        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Control clickedControl = sender as Control;

                // Verifica se o controle clicado é um Label
                if (clickedControl is Label)
                {
                    // Obtém o Panel pai do Label
                    movingPanel = clickedControl.Parent as Panel;
                }
                else
                {
                    // Caso contrário, assume que o clique foi diretamente em um Panel
                    movingPanel = clickedControl as Panel;
                }

                if ((isOnBottomBorder || isOnTopBorder) && e.Button == MouseButtons.Left)
                {
                    // Inicia o redimensionamento
                    mouseIsDown = true;
                    isResizing = true;
                    originalHeight = movingPanel.Height;
                    originalTop = movingPanel.Top;
                    originalBut = movingPanel.Bottom;

                    overlayForm = new OverlayForm();
                    overlayForm.Bounds = painelExames.RectangleToScreen(painelExames.ClientRectangle);
                    overlayForm.TempRect = new Rectangle(movingPanel.Left, originalTop, movingPanel.Width, originalHeight);
                    mouseDownLocation = e.Location;

                    overlayForm.Show();
                } else {

                    mouseDownLocation = e.Location;
                    initialPanelY = movingPanel.Top;
                    // Cria e configura o formulário de overlay
                    overlayForm = new OverlayForm();
                    overlayForm.Bounds = painelExames.RectangleToScreen(painelExames.ClientRectangle);
                    overlayForm.TempRect = new Rectangle(movingPanel.Left, movingPanel.Top, movingPanel.Width, movingPanel.Height);
                    overlayForm.Show();

                    isMoving = true;
                }
            }
        }
        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            Control clickedControl = sender as Control;

            // Verifica se o controle clicado é um Label
            if (clickedControl is Label)
            {
                // Obtém o Panel pai do Label
                movingPanel = clickedControl.Parent as Panel;
            }
            else
            {
                // Caso contrário, assume que o clique foi diretamente em um Panel
                movingPanel = clickedControl as Panel;
            }


            if (!mouseIsDown)
            {
                // Verifica se o mouse está na borda inferior ou superior do painel ao clicar
                isOnBottomBorder = Math.Abs(e.Y - movingPanel.Height) <= borderWidth;
                isOnTopBorder = Math.Abs(e.Y) <= borderWidth;

                if (isOnBottomBorder || isOnTopBorder)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
            if (isResizing && overlayForm != null)
            {
                int deltaY = e.Y - mouseDownLocation.Y;

                if (isOnTopBorder)
                {
                    int newTop = originalTop + deltaY;
                    int newHeight = originalHeight - deltaY;

                    // Garantir que o novo topo e altura estejam dentro dos limites do painelExames
                    newTop = Math.Max(0, Math.Min(newTop, painelExames.Height - newHeight));
                    newHeight = Math.Max(0, Math.Min(newHeight, painelExames.Height - newTop));

                    // Impedir que o painel fique menor que a altura mínima
                    if (newHeight < 20)
                    {
                        // Definir a altura mínima e corrigir o topo para evitar movimento indesejado
                        newHeight = 20;
                        newTop = originalTop + (originalHeight - newHeight); // Ajusta o Top para que o painel não se mova mais para cima
                    }
                    else if (newHeight > painelExames.Height * 0.35)
                    {
                        // Limitar a altura máxima do painel
                        newHeight = (int)(painelExames.Height * 0.35);
                        newTop = originalTop + (originalHeight - newHeight); // Ajusta o Top corretamente para o novo tamanho
                    }

                    // Atualizar a posição e o tamanho do overlay
                    overlayForm.TempRect = new Rectangle(overlayForm.TempRect.X, newTop, overlayForm.TempRect.Width, newHeight);
                }
                else
                {
                    int newHeight = originalHeight + deltaY;

                    newHeight = Math.Max(0, Math.Min(newHeight, painelExames.Height - originalTop));
                    if (newHeight < 20)
                    {
                        newHeight = 20;
                    }
                    else if (newHeight > painelExames.Height * 0.35)
                    {
                        newHeight = (int)(painelExames.Height * 0.35);
                    }
                    overlayForm.TempRect = new Rectangle(overlayForm.TempRect.X, overlayForm.TempRect.Y, overlayForm.TempRect.Width, newHeight);
                }

                overlayForm.Invalidate(); // Redesenha o retângulo no overlay
            }

            if (isMoving && overlayForm != null)
            {
                int deltaY = e.Y - mouseDownLocation.Y;
                int newTop = initialPanelY + deltaY;

                // Garantir que o retângulo temporário não saia do topo ou da parte inferior do painelExames
                newTop = Math.Max(0, Math.Min(newTop, painelExames.Height - overlayForm.TempRect.Height));

                overlayForm.TempRect = new Rectangle(overlayForm.TempRect.X, newTop, overlayForm.TempRect.Width, overlayForm.TempRect.Height);
                overlayForm.Invalidate(); // Redesenha o retângulo no overlay
            }
        }
        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isResizing && overlayForm != null)
            {
                // Finaliza o redimensionamento
                mouseIsDown = false;
                isResizing = false;
                movingPanel.Cursor = Cursors.Default;

                // Ajusta a altura e a posição do painel
                if (isOnTopBorder)
                {
                    movingPanel.Top = overlayForm.TempRect.Top;
                    movingPanel.Height = overlayForm.TempRect.Height;
                    if (movingPanel.Height < 20)
                    {
                        movingPanel.Height = 20;
                    }
                }
                else if (isOnBottomBorder)
                {
                    movingPanel.Height = overlayForm.TempRect.Height;
                    if (movingPanel.Height < 20)
                    {
                        movingPanel.Height = 20;
                    }
                }
                AdjustPanelsAfterResize(movingPanel);

                // Fecha o overlay
                overlayForm.Close();
                overlayForm.Dispose();
                overlayForm = null;
                // Atualiza a altura no DataTable após o redimensionamento
                UpdatePanelHeightInDataTable();
                AjustarFonteDosLabels();
                AjustarBotoesMinusEPlus();
                RepositionPanels();

                TelaClearAndReload();
            }

            if (isMoving && overlayForm != null)
            {
                int newTop = overlayForm.TempRect.Top;

                // Reposiciona o painel
                movingPanel.Top = newTop;

                // Fecha o overlay
                overlayForm.Close();
                overlayForm.Dispose();
                overlayForm = null;

                // Reposicionar todos os painéis para garantir a ordem correta
                RepositionPanels();
                // Atualizar a linha do DataTable com base na nova posição do painel
                UpdateDataTableRow(movingPanel);
                ReorderGrafSelectedCodSelectedAndScale();

                isMoving = false;
                movingPanel = null;
                TelaClearAndReload();
            }
        }

        private int margemBorda = 2; 
        public static bool redimensionando = false;
        private int larguraMinima = 100;
        private int larguraMaxima = 1000;
        private void PainelExames_MouseMove(object sender, MouseEventArgs e)
        {
            // Verifica se o mouse está perto da borda direita
            if (e.X >= painelExames.Width - margemBorda && e.X <= painelExames.Width + margemBorda)
            {
                this.Cursor = Cursors.SizeWE;

                if (redimensionando && e.Button == MouseButtons.Left)
                {
                    // Calcula a nova largura
                    int novaLargura = painelExames.Left + e.X;
                    novaLargura = Math.Max(larguraMinima, Math.Min(novaLargura, larguraMaxima));

                    painelExames.Width = novaLargura;
                }
            }
            else
            {
                if (!redimensionando)
                    this.Cursor = Cursors.Default;
            }
        }

        private void PainelExames_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.X >= painelExames.Width - margemBorda)
            {
                redimensionando = true;
            }
        }

        private void PainelExames_MouseUp(object sender, MouseEventArgs e)
        {
            redimensionando = false;
            this.Cursor = Cursors.Default;
        }

        private void AdjustPanelsAfterResize(Panel resizedPanel)
        {
            int totalHeight = painelExames.ClientSize.Height; // Altura total do contêiner pai
            int minHeightPerPanel = 20; // Altura mínima permitida para cada painel
            int newHeight = resizedPanel.Height; // Nova altura desejada do painel redimensionado

            // Garantir que o painel redimensionado não seja menor que o mínimo permitido
            if (newHeight < minHeightPerPanel)
            {
                newHeight = minHeightPerPanel;
            }

            // Definir a nova altura do painel redimensionado
            resizedPanel.Height = newHeight;
            int remainingHeight = totalHeight - newHeight; // Altura restante após redimensionar o painel

            // Filtrar os painéis visíveis (Location.X != -500) e excluir o painel redimensionado
            List<Panel> otherPanels = painelExames.Controls.OfType<Panel>()
                                            .Where(p => p.Location.X != -500 && p != resizedPanel)
                                            .ToList();

            // Calcular a altura combinada atual dos outros painéis
            int totalCurrentHeightOthers = otherPanels.Sum(p => p.Height);

            // Ajustar proporcionalmente a altura dos outros painéis se houver excesso de altura
            if (totalCurrentHeightOthers > remainingHeight)
            {
                int excessHeight = totalCurrentHeightOthers - remainingHeight; // Altura em excesso
                int reductionPerPanel = excessHeight / otherPanels.Count; // Redução média por painel
                int remainingReduction = excessHeight % otherPanels.Count; // Resto para ajuste fino

                foreach (Panel panel in otherPanels)
                {
                    // Ajustar altura de cada painel, garantindo o mínimo permitido
                    int adjustedHeight = panel.Height - reductionPerPanel;

                    if (remainingReduction > 0)
                    {
                        adjustedHeight--; // Distribuir o ajuste adicional
                        remainingReduction--;
                    }

                    // Garantir que a altura do painel não seja inferior ao mínimo
                    panel.Height = Math.Max(adjustedHeight, minHeightPerPanel);
                }
            }

            // Garantir que todos os painéis visíveis respeitem o tamanho mínimo
            foreach (Panel panel in otherPanels)
            {
                if (panel.Height < minHeightPerPanel)
                {
                    panel.Height = minHeightPerPanel; // Ajustar para o mínimo permitido
                }
            }

            // Recalcular a altura total dos painéis visíveis e verificar a diferença
            int currentTotalHeight = otherPanels.Sum(p => p.Height) + resizedPanel.Height;
            int difference = totalHeight - currentTotalHeight;

            // Redistribuir a diferença respeitando o mínimo permitido
            if (difference != 0)
            {
                int adjustmentPerPanel = difference / (otherPanels.Count + 1); // Incluindo o painel redimensionado

                foreach (Panel panel in painelExames.Controls.OfType<Panel>().Where(p => p.Location.X != -500))
                {
                    int adjustedHeight = panel.Height + adjustmentPerPanel;

                    // Garantir que o ajuste não faça o painel ficar abaixo do mínimo
                    if (adjustedHeight < minHeightPerPanel)
                    {
                        adjustedHeight = minHeightPerPanel;
                    }

                    panel.Height = adjustedHeight;
                }

                // Ajuste final caso ainda haja diferença devido ao arredondamento
                int finalAdjustment = totalHeight - painelExames.Controls.OfType<Panel>().Where(p => p.Location.X != -500).Sum(p => p.Height);
                if (finalAdjustment != 0)
                {
                    foreach (var panel in painelExames.Controls.OfType<Panel>().Where(p => p.Location.X != -500))
                    {
                        int allowedIncrease = finalAdjustment > 0 ? 1 : -1;

                        // Garantir que o ajuste final não ultrapasse o mínimo permitido
                        if (panel.Height + allowedIncrease >= minHeightPerPanel)
                        {
                            panel.Height += allowedIncrease;
                            finalAdjustment -= allowedIncrease;
                        }

                        if (finalAdjustment == 0)
                        {
                            break;
                        }
                    }
                }
            }

            // Atualizar o array `GlobVar.desenhoLoc` com a nova posição dos painéis visíveis
            int index = 0;
            foreach (Panel pn in painelExames.Controls.OfType<Panel>().Where(p => p.Location.X != -500))
            {
                int topPn = pn.Top;
                int meioPn = pn.Height / 2;
                GlobVar.desenhoLoc[index] = topPn + meioPn;
                index++;
            }

            // Zerar valores restantes no array `GlobVar.desenhoLoc`
            for (int i = index; i < GlobVar.desenhoLoc.Length; i++)
            {
                GlobVar.desenhoLoc[i] = 0;
            }

            // Reposicionar todos os painéis visíveis para garantir o alinhamento correto
            int currentY = 0;
            foreach (Panel panel in painelExames.Controls.OfType<Panel>().Where(p => p.Location.X != -500))
            {
                panel.Top = currentY;
                currentY += panel.Height;
            }
        }
        public void hideBottom_Click(object sender, EventArgs e)
        {
            // Verifica se o sender é um controle
            if (sender is Control controle)
            {
                // Obtém o painel pai do controle clicado
                Control painelPai = controle.Parent;

                // Verifica se o pai é um painel
                if (painelPai is Panel painel)
                {
                    // Define o painel como invisível
                    painel.Visible = false;
                    ReorganizarPainelComando();
                }
            }
        } // ------- Botao Hide
        public void ReorganizarPainelComando()
        {
            // Dimensões e margens
            int margemHorizontal = 5; // Espaçamento entre controles na horizontal
            int margemVertical = 5;   // Espaçamento entre controles na vertical

            // Largura máxima do painelComando para reorganização
            int larguraDisponivel = painelComando.Width;

            // Posição inicial
            int posicaoXAtual = margemHorizontal;
            int posicaoYAtual = margemVertical;

            // Obtenha todos os controles visíveis no painelComando
            var controlesVisiveis = painelComando.Controls.Cast<Control>()
                .Where(c => c.Visible) // Apenas controles visíveis
                .ToList();

            // Obtenha os painéis que podem ser reorganizados
            var paineisReorganizaveis = controlesVisiveis
                .OfType<Panel>() // Apenas `Panel`
                .Where(p => p.Name != "PainelPerfil") // Exclui o PainelPerfil
                .ToList();

            // Obtenha os limites fixos (TextBox, ComboBox e PainelPerfil visíveis)
            foreach (var controle in controlesVisiveis)
            {
                if (controle is TextBox || controle is ComboBox || controle.Name == "PainelPerfil")
                {
                    // Ajusta a posição inicial X para garantir que os painéis fiquem depois desses controles
                    int limiteDireito = controle.Bounds.Right + margemHorizontal;
                    if (limiteDireito > posicaoXAtual)
                        posicaoXAtual = limiteDireito;
                }
            }

            // Reorganizar os painéis visíveis
            foreach (var painel in paineisReorganizaveis)
            {
                // Verifica se o painel ultrapassa a largura disponível
                if (posicaoXAtual + painel.Width > larguraDisponivel)
                {
                    // Muda para a próxima "linha"
                    posicaoXAtual = margemHorizontal; // Reseta X
                    posicaoYAtual += painel.Height + margemVertical; // Avança Y
                }

                // Define a nova posição do painel
                painel.Location = new Point(posicaoXAtual, posicaoYAtual);

                // Atualiza a posição X para o próximo painel
                posicaoXAtual += painel.Width + margemHorizontal;
            }
            // Reorganiza a altura do Painel (painelComando) com base nos componentes que estão visíveis
            if (controlesVisiveis.Any())
            {
                // Obtém o limite inferior mais distante dos controles visíveis
                int alturaNecessaria = controlesVisiveis.Max(c => c.Bounds.Bottom) + margemVertical;

                // Ajusta a altura do painelComando
                painelComando.Height = alturaNecessaria;
            }
            else
            {
                // Define uma altura mínima se nenhum controle estiver visível
                painelComando.Height = margemVertical;
            }

            openglControl1.Top = painelComando.Bottom + 1;
            painelExames.Top = painelComando.Bottom + 1;

            int heigt = Math.Abs((painelComando.Bottom + 1) - (painelTelaGl.Top - 1));
            openglControl1.Height = heigt;
            painelExames.Height = heigt;
            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;

            GlobVar.sizePainelExams.X = painelExames.Width;
            GlobVar.sizePainelExams.Y = painelExames.Height;

            int normalSize = painelExames.Height / GlobVar.tbl_MontagemSelecionada.Rows.Count;

            int lastTop = 0;

            foreach (Panel pn in painelExames.Controls)
            {
                pn.Top = lastTop;
                pn.Height = normalSize;
                lastTop += normalSize;
            }

            int indiot = 0;
            foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
            {
                int topPn = pn.Top;
                int auuuu = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                int meioPn = pn.Height;
                if(indiot < GlobVar.desenhoLoc.Length){
                    GlobVar.desenhoLoc[indiot] = topPn + meioPn;
                }
                indiot++;
            }
            UpdatePanelHeightInDataTable();
            AjustarFonteDosLabels();
            UpdateInicioTela();
            TelaClearAndReload();

        }
        private void ImpressaoStripMenu_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem8.Checked)
            {
                toolStripMenuItem8.Checked = false;
                PainelPrinters.Visible = false;
                PainelPrinters.Hide();
                ReorganizarPainelComando();
            }
            else
            {
                toolStripMenuItem8.Checked = true;
                PainelPrinters.Show();
                PainelPrinters.Visible = true;
                ReorganizarPainelComando();

            }
        }
        private void DeslocStripMenu_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem2.Checked)
            {
                toolStripMenuItem2.Checked = false;
                PainelAvRet.Visible = false;
                PainelAvRet.Hide();
                ReorganizarPainelComando();
            }
            else
            {
                toolStripMenuItem2.Checked = true;
                PainelAvRet.Show();
                PainelAvRet.Visible = true;
                ReorganizarPainelComando();

            }
        }
        private void EstagEpocasStripMenu_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem4.Checked)
            {
                toolStripMenuItem4.Checked = false;
                PainelLoc.Visible = false;
                PainelLoc.Hide();
                ReorganizarPainelComando();
            }
            else
            {
                toolStripMenuItem4.Checked = true;
                PainelLoc.Show();
                PainelLoc.Visible = true;
                ReorganizarPainelComando();

            }
        }
        private void AtribuEstagioStripMenu_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem5.Checked)
            {
                toolStripMenuItem5.Checked = false;
                PainelMarca.Visible = false;
                PainelMarca.Hide();
                ReorganizarPainelComando();
            }
            else
            {
                toolStripMenuItem5.Checked = true;
                PainelMarca.Show();
                PainelMarca.Visible = true;
                ReorganizarPainelComando();

            }
        }
        private void PainelMarcaAntProxStripMenu_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem6.Checked)
            {
                toolStripMenuItem6.Checked = false;
                PainelMarcaAntProx.Visible = false;
                PainelMarcaAntProx.Hide();
                ReorganizarPainelComando();
            }
            else
            {
                toolStripMenuItem6.Checked = true;
                PainelMarcaAntProx.Show();
                PainelMarcaAntProx.Visible = true;
                ReorganizarPainelComando();

            }
        }
        private void PainelDescEventoStripMenu_Click(object sender, EventArgs e)
        {

            if (toolStripMenuItem7.Checked)
            {
                toolStripMenuItem7.Checked = false;
                PanelEventos.Visible = false;
                PanelEventos.Hide();
                ReorganizarPainelComando();
            }
            else
            {
                toolStripMenuItem7.Checked = true;
                PanelEventos.Show();
                PanelEventos.Visible = true;
                ReorganizarPainelComando();

            }
        }
        public static void UpdatePanelHeightInDataTable()
        {
            float totalPercentage = 0;
            DataRow lastRow = null;

            // Calcula a altura de cada painel como uma porcentagem, mas apenas para os painéis visíveis
            foreach (Panel pn in painelExames.Controls)
            {
                // Ignorar painéis que estão fora da tela (posição X = -500)
                if (pn.Location.X == -500)
                {
                    continue; // Pula para o próximo painel
                }

                var codCanal = pn.Tag.ToString();

                foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    if (row["CodCanal1"].ToString().Equals(codCanal))
                    {
                        // Calcula a altura do painel como uma porcentagem do painelExames
                        float alturaPercent = (float)pn.Height / painelExames.Height * 100;

                        // Limita a duas casas decimais
                        alturaPercent = (float)Math.Round(alturaPercent, 2);

                        // Atribui o valor formatado à coluna "Altura"
                        row["Altura"] = alturaPercent;

                        totalPercentage += alturaPercent;
                        lastRow = row; // Armazena a última linha encontrada
                        break;
                    }
                }
            }

            // Se o totalPercentage não somar 100% e houver uma última linha válida, ajuste a última linha para compensar a diferença
            if (totalPercentage < 100 && lastRow != null)
            {
                float remainingPercentage = 100 - totalPercentage;
                lastRow["Altura"] = (float)lastRow["Altura"] + remainingPercentage;
            }
        }
        public static void AjustarFonteDosLabels() // ------------ Ajuste Fontes
        {


            // Definindo os limites de tamanho da fonte
            int minFontSize = 6;
            int maxFontSize = 12;

            // Iterar sobre cada Panel no painelExames
            foreach (Panel pn in painelExames.Controls.OfType<Panel>())
            {
                if(Convert.ToInt32(pn.Tag) == -1)
                {
                    continue;
                }
                var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                    .Where(row => row.Field<int>("CodCanal") == Convert.ToInt32(pn.Tag)).CopyToDataTable();
                int TipoCanal = Convert.ToInt32(CodTipoCanal.Rows[0]["CodTipo"]);
                if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                    || TipoCanal == 15 || TipoCanal == 30 || TipoCanal == 12 || TipoCanal == 38)
                {
                    // Calcula um tamanho de fonte proporcional à altura do Panel
                    int novaFonteTamanho = Math.Max(minFontSize, Math.Min(maxFontSize, pn.Height / 3)); // Dividido por 3 como fator de ajuste
                    int novaFonteScala = Math.Max(4, Math.Min(6, pn.Height / 4));
                    int TopLoc = pn.Height / 2 - (novaFonteTamanho);
                    // Iterar sobre cada Label dentro do Panel
                    foreach (Label lb in pn.Controls.OfType<Label>())
                    {
                        if(lb.Tag == null)
                        {
                            continue;
                        }
                        if (lb.Tag.Equals("min"))
                        {
                            Point minLoc = new Point(0, 0);

                            minLoc.X = pn.Width - 30;
                            minLoc.Y = pn.Height - 15;

                            lb.Location = new Point(minLoc.X, minLoc.Y);
                        }
                        else if (lb.Tag.Equals("max"))
                        {
                            Point maxLoc = new Point(0, 0);

                            maxLoc.X = pn.Width - 30;
                            maxLoc.Y = 1;

                            lb.Location = new Point(maxLoc.X, maxLoc.Y);
                        }
                        else if (lb.Tag == pn.Tag)
                        {//Para diferenciar o Panel de Titulo do Label de scala
                         // Ajustar o tamanho da fonte do Label
                            lb.Top = TopLoc;
                            lb.Font = new Font(lb.Font.FontFamily, novaFonteTamanho, lb.Font.Style);
                        }
                        else if (lb.Tag.Equals("setas"))
                        {
                            // Definindo os parâmetros para o ajuste das setas
                            int alturaTotal = pn.Height; // Altura total do painel
                            int qtdSetas = 4; // Número total de setas (Cima, Direita, Esquerda, Baixo)
                            int alturaLabel = alturaTotal / (qtdSetas + 1); // Altura disponível para cada label de seta com espaçamento
                            int espacoEntreSetas = (alturaTotal - (qtdSetas * alturaLabel)) / (qtdSetas + 1); // Espaçamento entre as setas

                            // Definindo o tamanho da fonte com base na altura disponível
                            int tamanhoFonte = Math.Max(4, Math.Min(alturaLabel / 3, 14));

                            // Ajuste das posições baseado no texto do Label
                            switch (lb.Text)
                            {
                                case "ã": // Seta para cima
                                    lb.Location = new System.Drawing.Point(pn.Width - 35, espacoEntreSetas);
                                    break;
                                case "á": // Seta para direita
                                    lb.Location = new System.Drawing.Point(pn.Width - 35, espacoEntreSetas + alturaLabel + espacoEntreSetas);
                                    break;
                                case "â": // Seta para esquerda
                                    lb.Location = new System.Drawing.Point(pn.Width - 35, espacoEntreSetas + 2 * (alturaLabel + espacoEntreSetas));
                                    break;
                                case "ä": // Seta para baixo
                                    lb.Location = new System.Drawing.Point(pn.Width - 35, espacoEntreSetas + 3 * (alturaLabel + espacoEntreSetas));
                                    break;
                            }

                            // Ajuste da fonte para as setas com o tamanho calculado
                            lb.Font = new Font("Wingdings 3", tamanhoFonte, lb.Font.Style);
                        }
                    }

                }
                else
                {
                    // Calcula um tamanho de fonte proporcional à altura do Panel
                    int novaFonteTamanho = Math.Max(minFontSize, Math.Min(maxFontSize, pn.Height / 3)); // Dividido por 3 como fator de ajuste
                    int novaFonteScala = Math.Max(3, Math.Min(8, pn.Height / 4));
                    int TopLoc = pn.Height / 2 - (novaFonteTamanho);
                    // Iterar sobre cada Label dentro do Panel
                    foreach (Label lb in pn.Controls.OfType<Label>())
                    {
                        if (lb.Tag == null)
                        {
                            continue;
                        }

                        if (lb.Tag == pn.Tag)
                        {//Para diferenciar o Panel de Titulo do Label de scala
                         // Ajustar o tamanho da fonte do Label
                            lb.Top = TopLoc;
                            lb.Font = new Font(lb.Font.FontFamily, novaFonteTamanho, lb.Font.Style);
                        }
                        else
                        {
                            lb.Top = pn.Height / 2 - novaFonteScala;
                            lb.Font = new Font(lb.Font.FontFamily, novaFonteScala, lb.Font.Style);
                        }
                    }
                }
            }
        }
        private void MesmaAlturaCanais_Click(object sender, EventArgs e)
        {
            // Conta apenas os painéis de canais (ignorando o da marca d'água)
            var paineisCanais = painelExames.Controls
                .OfType<Panel>()
                .OrderBy(p => p.Top)
                .ToList();
            int qtd = MarcaDAguaNaTelaAtiva ? paineisCanais.Count : GlobVar.tbl_MontagemSelecionada.Rows.Count;

            int normalSize = painelExames.Height / qtd;

            int lastTop = 0;

            foreach (Panel pn in painelExames.Controls)
            {
                pn.Top = lastTop;
                pn.Height = normalSize;
                lastTop += normalSize;
            }

            int indiot = 0;
            foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
            {
                int topPn = pn.Top;
                int auuuu = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                int meioPn = pn.Height;
                if(indiot < GlobVar.desenhoLoc.Length){
                    GlobVar.desenhoLoc[indiot] = topPn + meioPn;
                }
                indiot++;
            }
            UpdatePanelHeightInDataTable();
            AjustarFonteDosLabels();

            TelaClearAndReload();
            UpdateInicioTela();

        }
        public static void AjustarBotoesMinusEPlus()
        {
            foreach (Panel pn in painelExames.Controls)
            {

                int newHeight = Math.Clamp(pn.Height / 2, 10, 20);
                foreach (System.Windows.Forms.Button bt in pn.Controls.OfType<System.Windows.Forms.Button>())
                {
                    bt.Hide();
                    bt.Height = newHeight;
                    if (bt.Tag.Equals("+"))
                    {
                        bt.Top = (pn.Height / 2) - (newHeight);
                    } else if (bt.Tag.Equals("-"))
                    {
                        bt.Top = (pn.Height / 2);
                    }
                }
            }
        }
        public static int tagCodCanal;
        // Evento de mouse enter para mostrar o ButtonForm
        private void panel_MouseEnter(object sender, EventArgs e)
        {
            timer1.Start();
            Panel panel;
            Control clickedControl = sender as Control;

            if (clickedControl is Label lb)
            {
                panel = lb.Parent as Panel;
            } else
            {
                panel = sender as Panel;
            }

            if (panel != null && buttonForm != null)
            {
                panelOn = panel;
                tagCodCanal = (int)panel.Tag;
                var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodCanal") == tagCodCanal).CopyToDataTable();
                int TipoCanal = Convert.ToInt32(CodTipoCanal.Rows[0]["CodTipo"]);
                if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                        || TipoCanal == 15 || TipoCanal == 30 || TipoCanal == 12 || TipoCanal == 38)
                {
                    buttonForm.HideOverlay();
                }
                else
                {
                    // Define a posição do ButtonForm em relação ao painel
                    Point location = panel.PointToScreen(Point.Empty); // Posição do painel na tela
                    buttonForm.ShowOverlay(new Point(location.X + panel.Width - 30, location.Y + (panel.Height / 2) - 25), new Size(25, 50));

                }
            }
        }
        // Evento de mouse leave para ocultar o ButtonForm
        private void panel_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Panel) {
                this.Cursor = Cursors.Default;
                buttonForm.HideOverlay();
            }
            else
            {
                buttonForm.HideOverlay();
            }
        }
        // Evento de clique no painel para mostrar o ButtonForm
        private void Panel_Click(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel != null && buttonForm != null)
            {
                // Exibe o ButtonForm no clique, na posição desejada
                Point location = panel.PointToScreen(Point.Empty);
                buttonForm.ShowOverlay(new Point(location.X + panel.Width - 30, location.Y - 10), new Size(25, 50));
            }
        }
        // Declaração do ButtonForm na classe principal
        private ButtonForm buttonForm;
        // Método para inicializar o ButtonForm
        private void InicializarButtonForm()
        {
            // Instancia o ButtonForm e configura os botões necessários
            buttonForm = new ButtonForm();
            buttonForm.Size = new Size(25, 50); // Ajuste o tamanho conforme necessário

            // Adiciona botões ou outros controles ao ButtonForm
            Button btnPlus = new Button
            {
                Text = "+",
                Size = new Size(25, 25),
                Location = new Point(0, 0),
                Tag = "+"
            };

            Button btnMinus = new Button
            {
                Text = "-",
                Size = new Size(25, 25),
                Location = new Point(0, 25),
                Tag = "-"
            };

            btnPlus.Tag = tagCodCanal;
            btnMinus.Tag = tagCodCanal;

            // Adiciona eventos aos botões se necessário
            btnPlus.Click += plusLb1_Click;
            btnMinus.Click += minusLb1_Click;

            // Adiciona os botões ao form
            buttonForm.Controls.Add(btnPlus);
            buttonForm.Controls.Add(btnMinus);
        }
        private void RepositionPanels()
        {

            // Definir o espaçamento entre os painéis visíveis
            const int spacing = 0; // Ajuste conforme necessário

            // Filtrar os painéis que estão visíveis (Location.X != -500)
            var visiblePanels = painelExames.Controls
                                            .OfType<Panel>()
                                            .Where(p => p.Location.X != -500)
                                            .OrderBy(p => p.Top)
                                            .ToList();

            int y = 0; // Coordenada inicial Y para o reposicionamento
            foreach (Panel panel in visiblePanels)
            {
                // Atualizar a posição Y do painel
                panel.Top = y;

                // Se necessário, ajuste o tamanho do painel aqui
                // Por exemplo, ajustar a altura do painel:
                // panel.Height = Math.Max(panel.Height, 50); // Exemplo de ajuste de altura mínima

                // Atualizar a posição Y para o próximo painel, considerando o espaçamento
                y = panel.Bottom + spacing;
            }
            List<int> agor = new List<int>();
            // Atualizar o array `desenhoLoc` com a nova posição dos painéis visíveis
            int index = 0;
            foreach (Panel pn in visiblePanels)
            {
                // Calcular a posição do centro do painel e armazenar em `GlobVar.desenhoLoc`
                int meioPn = pn.Top + (pn.Height / 2);
                agor.Add(meioPn);

                index++;
            }
            GlobVar.desenhoLoc = new float[agor.Count];
            index = 0;
            for(int i = 0; i < GlobVar.desenhoLoc.Length; i++)
            {
                GlobVar.desenhoLoc[i] = agor[i];
            }
            // Caso tenha menos painéis visíveis do que o tamanho original de `GlobVar.desenhoLoc`,
            // zera os valores restantes
            for (int i = index; i < GlobVar.desenhoLoc.Length; i++)
            {
                GlobVar.desenhoLoc[i] = 0;
            }
        }
        private void UpdateDataTableRow(Panel panel)   
        {
            var codCanal = panel.Tag.ToString();
            DataRow rowToMove = null;

            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                if (row["CodCanal1"].ToString().Equals(codCanal))
                {
                    rowToMove = row;
                    break;
                }
            }

            if (rowToMove != null)
            {
                DataRow newRow = GlobVar.tbl_MontagemSelecionada.NewRow();
                newRow.ItemArray = rowToMove.ItemArray.Clone() as object[];

                double auxIndex = painelExames.Height / GlobVar.tbl_MontagemSelecionada.Rows.Count;
                int newRowIndex = (int)Math.Max(0, Math.Min(GlobVar.tbl_MontagemSelecionada.Rows.Count, panel.Top / (double)auxIndex));

                GlobVar.tbl_MontagemSelecionada.Rows.Remove(rowToMove);

                if (newRowIndex >= GlobVar.tbl_MontagemSelecionada.Rows.Count)
                {
                    GlobVar.tbl_MontagemSelecionada.Rows.Add(newRow);
                }
                else
                {
                    GlobVar.tbl_MontagemSelecionada.Rows.InsertAt(newRow, newRowIndex);
                }
            }
        }
        private void ReorderGrafSelectedCodSelectedAndScale()
        {
            // Criar cópias temporárias para reorganizar os vetores
            int[] tempGrafSelected = new int[GlobVar.grafSelected.Length];
            int[] tempCodSelected = new int[GlobVar.codSelected.Length];
            float[] tempScale = new float[GlobVar.scale.Length];
            //int[] tempTxPorCanal = new int[GlobVar.txPorCanal.Length];

            // Reordenar conforme a nova ordem no DataTable
            for (int newIndex = 0; newIndex < GlobVar.tbl_MontagemSelecionada.Rows.Count; newIndex++)
            {
                // Obter o CodCanal1 da nova posição
                int codCanal = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[newIndex]["CodCanal1"]);
                // Encontrar a posição original do codCanal no codSelected
                int originalIndex = Array.IndexOf(GlobVar.codSelected, codCanal);

                // Reorganizar os vetores
                tempGrafSelected[newIndex] = GlobVar.grafSelected[originalIndex];
                tempCodSelected[newIndex] = codCanal;

                // Recalcular o valor para o vetor scale
                float scala = (float)(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[newIndex]["AmplitudeMin"]) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(newIndex)));
                tempScale[newIndex] = scala;
                //tempTxPorCanal[newIndex] = GlobVar.txPorCanal[originalIndex];
            }

            // Copiar os valores reorganizados de volta para os vetores originais
            //Array.Copy(tempTxPorCanal, GlobVar.txPorCanal, tempTxPorCanal.Length);
            Array.Copy(tempGrafSelected, GlobVar.grafSelected, tempGrafSelected.Length);
            Array.Copy(tempCodSelected, GlobVar.codSelected, tempCodSelected.Length);
            Array.Copy(tempScale, GlobVar.scale, tempScale.Length);
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
                canais.RealocPanel(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                canais.quantidadeGraf(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                canais.RealocButton();
                canais.PainelLb_Resize();
                canais.reloc();


                gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(GlobVar.tbl_MontagemSelecionada.Rows.Count, alturaTela);
                plotagem.Traco(GlobVar.tbl_MontagemSelecionada.Rows.Count, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, GlobVar.tbl_MontagemSelecionada.Rows.Count);

                AjustarFonteDosLabels();
                RepositionPanels();
                ReorderGrafSelectedCodSelectedAndScale();
                TelaClearAndReload();

                var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();
                int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]);

                hScrollBar1.Maximum = maximoPossivel;
                hScrollBar1.LargeChange = GlobVar.segundos;
                hScrollBar1.Refresh();

                if (GlobVar.ultimaPag != 0)
                {
                    abreUltimaPaginaFechada();
                }
                UpdateInicioTela();
                click = true;
            }

        }
        private void DownPainel_Resiz(object sender, EventArgs e)
        {
            resize_DownPainel(pnGrand, pnGr);
            resize_DownPainel(pnEstagio, pnesta);
            resize_DownPainel(pnImpressao, pnimpre);
            resize_DownPainel(pnPeriodos, pnPeri);
            resize_DownPainel(hScrollBar1, hScrol);
            adjustFont();
            ReorganizarPainelComando();
        }
        private void resize_DownPainel(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Size = new Size(newWidth, newHeight);
            c.Location = new Point(newX, newY);
        }
        private void adjustFont() 
        {
            // Definindo os limites de tamanho da fonte
            int minFontSize = 6;
            int maxFontSize = 9;

            foreach (Panel pn in painelTelaGl.Controls.OfType<Panel>())
            {
                // Calcula um tamanho de fonte proporcional à altura do Panel
                int novaFonteTamanho = Math.Max(minFontSize, Math.Min(maxFontSize, pn.Height / 2)); // Dividido por 3 como fator de ajuste
                int novaFonteScala = Math.Max(4, Math.Min(6, pn.Height / 4));
                int TopLoc = pn.Height / 2 - (novaFonteTamanho);
                foreach (Label lb in pn.Controls.OfType<Label>())
                {
                    lb.Top = TopLoc;
                    lb.Font = new Font(lb.Font.FontFamily, novaFonteTamanho, lb.Font.Style);
                }
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
            //painelComando.Size = new Size(this.Size.Width, this.Size.Height / 5);
            //painelComando.Location = new System.Drawing.Point(3, 31);

            resize_Control(painelExames, exExam);

            resize_Control(painelTelaGl, telaGl);
            resize_Control(openglControl1, OGLS);

            GlobVar.sizeOpenGl.X = openglControl1.Width;
            GlobVar.sizeOpenGl.Y = openglControl1.Height;

            GlobVar.sizePainelExams.X = painelExames.Width;
            GlobVar.sizePainelExams.Y = painelExames.Height;
            hScrollBar1.Width = painelExames.Width + openglControl1.Width;
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

            //painelComando.Size = new Size(this.Size.Width, this.Size.Height / 5);
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
        string concluido = "";
        public static bool conc = true;
        public static Task _backgroundTask;
        private CancellationTokenSource _cancellationTokenSource;
        private async void MontagemBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try{
            if (!isInitialized || MontagemBox.SelectedIndex == -1)
                return;

            // Cancela a tarefa anterior sem aguardar a conclusão
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

                using (CarregandoAltMontagem telaLoad = new CarregandoAltMontagem())
                {
                    if (MarcaDAguaNaTelaAtiva) {
                    MarcaDAguaNaTela_Click(Tela_Plotagem.MarcaNoGraf, EventArgs.Empty);
                    }
                    // Processo principal
                    cronometro4.Reset();
                    concluido = "";
                    cronometro4.Start();
                    telaLoad.label1.Text = "Alterando Montagem";
                    telaLoad.realoctxt();
                    telaLoad.Show();
                    await Task.Delay(10, token);
                    telaLoad.AtualizarProgresso(10);

                    int CodMont = Convert.ToInt32(GlobVar.tbl_Montagem.Rows[MontagemBox.Items.IndexOf(MontagemBox.Text)]["CodMontagem"]);
                    LeituraBanco.AlteraMontagem(CodMont);
                    telaLoad.AtualizarProgresso(25);

                    LeituraEmMatrizTeste.montagemSelecionadaAlterada();
                    LeituraEmMatrizTeste.referencias();
                    await Task.Delay(2, token);
                    telaLoad.AtualizarProgresso(50);

                    canais = new Canais(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                    canais.RealocPanel(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                    canais.quantidadeGraf(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                    canais.RealocButton();
                    canais.PainelLb_Resize();
                    canais.reloc();
                    await Task.Delay(2, token);
                    telaLoad.AtualizarProgresso(75);

                    UpdatePanelHeightInDataTable();
                    AjustarFonteDosLabels();
                    AjustarBotoesMinusEPlus();
                    await Task.Delay(2, token);
                    telaLoad.AtualizarProgresso(90);

                    UpdateFilterStates();
                    UpdateInicioTela();
                    TelaClearAndReload();
                    await Task.Delay(2, token);
                    telaLoad.AtualizarProgresso(100);
                    cronometro4.Stop();

                    // Inicia uma nova tarefa em segundo plano, interrompida imediatamente se for cancelada
                    _backgroundTask = Task.Run(() =>
                    {
                        try
                        {
                            // Verifica o token constantemente para cancelar rapidamente
                            if (token.IsCancellationRequested)
                                token.ThrowIfCancellationRequested();

                            LeituraEmMatrizTeste.montagemSelecionadaAlteradaTudo(token);

                            if (token.IsCancellationRequested)
                                 token.ThrowIfCancellationRequested();

                            concluido = "Concluido";
                            TelaClearAndReload();
                        }
                        catch (OperationCanceledException)
                        {
                            Console.WriteLine("A tarefa em segundo plano foi cancelada.");
                        }
                    }, token);
                }
            }
            catch { }
        }
        private void qtdGraficos_TextChanged(object sender, EventArgs e)
        {
            string texto = qtdGraficos.Text;
            int numero;

            if (int.TryParse(texto, out numero))
            {
                qtdGrafics = GlobVar.tbl_MontagemSelecionada.Rows.Count;
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
                canais.RealocPanel(qtdGrafics);
                canais.quantidadeGraf(qtdGrafics);
                canais.RealocButton();
                canais.PainelLb_Resize();
                canais.reloc();


                gl = openglControl1.OpenGL;
                plotagem = new Plotagem(gl);
                openglControl1.DoRender();
                plotagem.Margem(qtdGrafics, alturaTela);
                plotagem.Traco(qtdGrafics, alturaTela);
                plotagem.DesenhaGrafico(alturaTela, qtdGrafics);

                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);

                UpdateInicioTela();
                click = true;
            }
        }
        public static bool isDrawing = false;
        public static bool isDrawingRectangle = false;
        // Variável para rastrear o painel anterior
        private Panel previousPanel = null;
        //Metodo que faz a plotagem, e a replotagem quando precisa
        public static void TelaClearAndReload()
        {
            try
            {
                if (openglControl1.InvokeRequired)
                {
                    openglControl1.Invoke(new Action(TelaClearAndReload));
                    return;
                }

                openglControl1.DoRender();

                // Realize as operações gráficas no thread principal
                plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotComentatios.DesenhaComentario(gl);
                plotNumerico.PlotNumerico(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotEventos.DrawTexts(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                plotNumerico.PlotSetas(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
            }
            catch { }
        }
        private void openglControl1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
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
        private async void OpenglControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta != 0)
                {
                    if (e.Delta > 0)
                    {
                        if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1)) {



                            camera.X += GlobVar.saltoTelas * GlobVar.SPEED;

                            GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                            GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            //UpdateInicioTela();
                            GlobVar.ponteiroVideo += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                        }
                    }
                    else
                    {

                        if (GlobVar.indice > 0)
                        {
                            if (!conc)
                            {
                                int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                                if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                {
                                    LeituraEmMatrizTeste.Pause();
                                    await LeituraEmMatrizTeste.CarregamentoMontagemRapido(3, newLoc);
                                }
                            }

                            camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;

                            GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            if (GlobVar.indiceNumero < 0)
                            {
                                GlobVar.indiceNumero = 0;
                                GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                            }

                            GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            if (GlobVar.indice < 0)
                            {
                                GlobVar.indice = 0;
                                GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                                camera.X = 0;
                            }
                            GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            //UpdateInicioTela();
                            GlobVar.ponteiroVideo -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            LeituraEmMatrizTeste.Resume();
                        }
                    }
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;
                    if (hScrollBar1.InvokeRequired)
                    {
                        hScrollBar1.Invoke(new Action(() =>
                        {
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                        }));
                    }
                    else
                    {
                        hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                    }

                    int alturaTela = (int)openglControl1.Height;
                    TelaClearAndReload();
                    UpdateInicioTela();
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
        public static bool isThereAComment = false;
        public static bool isThereAXSartComment = false;
        public static bool isThereAXEndComment = false;
        public static bool isThereAYStartComment = false;
        public static bool isThereAYEndComment = false;
        public static bool isThereX0Y0Comment = false;
        public static bool isThereX0Y1Comment = false;
        public static bool isThereX1Y0Comment = false;
        public static bool isThereX1Y1Comment = false;
        public static bool isThereVideoPonteiro = false;
        private bool isTelaClearAndReloadExecuted;
        ToolTip tooltipEvento = new ToolTip();
        Point mousePositionLocal;

        Point posInicialMouse;
        int larguraInicialPainel;
        int larguraInicialOpenGL;
        int posicaoInicialOpenGL;
        // Mostra o tooltip na posição do mouse (após o Timer disparar)
        private void TooltipTimer_Tick(object sender, EventArgs e)
        {
            if (isDrawingRectangle)
            {
                double inicio = Plotagem.startX / GlobVar.namos;
                double fim = Plotagem.endX / GlobVar.namos;

                double size = Math.Abs(fim - inicio);

                tooltipEvento.Show($"{GlobVar.txtLastEvent}: {size:F2}Seg", openglControl1, mousePositionLocal.X + 10, mousePositionLocal.Y + 10);
            }
        }
        private void OpenGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (click)
            {
                if (e.Button == MouseButtons.Left)
                {
                    // Armazena o cursor original e marca que o mouse está pressionado
                    originalCursor = this.Cursor;
                    isMouseDown = true;

                    if (!isAnEvent && !isAnStartEvent && !isAnEndEvent && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro && !bordSize)
                    {
                        timer3.Stop();
                        timer2.Start();
                        isDrawing = true;
                        isDrawingRectangle = true;
                        mousePositionLocal = e.Location;
                        tooltipTimer.Start();
                        ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.startX, out Plotagem.startY);
                        if (!crtlAtivo) {
                            GlobVar.endX = (int)Plotagem.startX;
                            GlobVar.endY = (int)Plotagem.startY;
                            GlobVar.startX = (int)Plotagem.startX;
                            GlobVar.startY = (int)Plotagem.startY;
                            int startY = (int)Plotagem.startY;
                            plotEventos.LastEvent(GlobVar.desenhoLoc, startY);
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
                            initialMousePosition.Y = e.X;
                            GlobVar.startX = GlobVar.iniEventoMove;
                            openglControl1.DoRender();
                            //TelaClearAndReload();
                        }

                    }
                    else if (isThereAComment)
                    {
                        float aux;
                        float auy;
                        timer2.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereAXSartComment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereAXEndComment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereAYStartComment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereAYEndComment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }

                    else if (isThereX0Y0Comment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereX0Y1Comment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereX1Y0Comment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

                    }
                    else if (isThereX1Y1Comment)
                    {
                        float aux;
                        float auy;
                        timerComment.Start();
                        isDrawing = true;
                        lastMousePosition = e.Location;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                        initialMousePosition.X = (int)aux;
                        initialMousePosition.Y = (int)auy;

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
                        else {
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
                    else if (isThereVideoPonteiro)
                    {
                        isDrawing = true;
                    }
                    else if (bordSize)
                    {
                        //openglControl1.Visible = false;
                        redimensionando = true;
                        posInicialMouse = e.Location;
                        larguraInicialPainel = painelExames.Width;
                        larguraInicialOpenGL = openglControl1.Width;
                        posicaoInicialOpenGL = openglControl1.Location.X;
                        GlobVar.ponteiroRedimension = GlobVar.indice;
                        GlobVar.XRedimension = openglControl1.Location.X;
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
        Point locMuse;
        Point dimMouse;
        Point endEvent;
        bool bordSize = false;
        private void OpenGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (e != null)
                {
                    lbteste.Text = GlobVar.CodCanal.ToString();

                    this.musezin.X = e.X;
                    this.musezin.Y = e.Y;

                    ConvertToOpenGLCoordinates(musezin.X, musezin.Y, out Plotagem.endX, out Plotagem.startY);
                    GlobVar.DimXY.X = (int)Plotagem.endX;
                    GlobVar.DimXY.Y = (int)Plotagem.startY;
                    int startY = (int)Plotagem.startY;
                    float endX = Plotagem.endX;
                    plotEventos.LastEvent(GlobVar.desenhoLoc, startY);
                    mousePositionLocal = e.Location;
                    if (!isDrawing && !bordSize)
                    {
                        this.isAnEvent = plotEventos.IsThereAnEvent((int)endX, GlobVar.desenhoLoc, startY);
                        this.isAnStartEvent = plotEventos.IsInAnEventStart((int)endX, GlobVar.desenhoLoc, startY);
                        this.isAnEndEvent = plotEventos.IsInAnEventEnd((int)endX, GlobVar.desenhoLoc, startY);
                        if (this.isAnEvent)
                        {
                            isA_BN_CPAP_BD = false;
                            isThereAComment = false;
                        }
                    }
                    if (!isDrawing && !bordSize)
                    {
                        if (!this.isAnEvent)
                        {
                            this.isA_BN_CPAP_BD = plotEventos.EUmBoaNoite_Cpap_BomDia((int)endX);

                            isThereAComment = plotComentatios.IsThereAComment(e.X, e.Y);
                            isThereAXSartComment = plotComentatios.IsThereAXstartComment(e.X, e.Y);
                            isThereAXEndComment = plotComentatios.IsThereAXEndComment(e.X, e.Y);
                            isThereAYStartComment = plotComentatios.IsThereAYstartComment(e.X, e.Y);
                            isThereAYEndComment = plotComentatios.IsThereAYEndComment(e.X, e.Y);

                            isThereX0Y0Comment = plotComentatios.IsThereX0Y0Comment(e.X, e.Y);
                            isThereX0Y1Comment = plotComentatios.IsThereX0Y1Comment(e.X, e.Y);
                            isThereX1Y0Comment = plotComentatios.IsThereX1Y0Comment(e.X, e.Y);
                            isThereX1Y1Comment = plotComentatios.IsThereX1Y1Comment(e.X, e.Y);

                            isThereVideoPonteiro = FormVideo.ponteiroCoord(e.X, e.Y);
                        }
                    }
                    GlobVar.drawBordenInAnEvent = this.isAnEvent;

                    if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        if (!isMouseDown)
                        {
                            if (e.X >= 0 - 5 && e.X <= 0 + 5)
                            {
                                this.Cursor = Cursors.SizeWE;
                                bordSize = true;
                            }
                            else
                            {
                                bordSize = false;
                                this.Cursor = Cursors.Default;
                                Stringao.Text = "";

                                if (!isTelaClearAndReloadExecuted)
                                {
                                    TelaClearAndReload();
                                    UpdateInicioTela();
                                    iCelera.telinha.videoPlayer.Ctlcontrols.pause();
                                    isTelaClearAndReloadExecuted = true;
                                }
                            }
                        }
                        toolTip1.RemoveAll();
                        if (isDrawing)
                        {
                            timerClick.Start();
                            isTelaClearAndReloadExecuted = false;
                            if (crtlAtivo)
                            {
                                if (e.X >= openglControl1.Size.Width)
                                {

                                    camera.X += GlobVar.namos;

                                    GlobVar.indiceNumero += 8 * 1; //(int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED; // 8 * 1;
                                    GlobVar.maximaNumero += 8 * 1;

                                    GlobVar.maximaVect += GlobVar.namos;
                                    GlobVar.indice += GlobVar.namos;

                                    GlobVar.inicioTela += GlobVar.namos / GlobVar.namos;
                                    GlobVar.finalTela += GlobVar.namos / GlobVar.namos;
                                    GlobVar.ponteiroVideo += GlobVar.namos;

                                    //TelaClearAndReload();
                                    gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
                                    UpdateInicioTela();
                                    foiencontradoumUltimo = false;
                                    foiencontradoumUltimo = false;

                                }

                            }
                            else
                            {
                                if (GlobVar.endX >= GlobVar.startX)
                                {
                                    if (e.X <= musezin.X)
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

                                    GlobVar.indiceNumero += 8 * 1; //(int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED; // 8 * 1;
                                    GlobVar.maximaNumero += 8 * 1;

                                    GlobVar.maximaVect += GlobVar.namos;
                                    GlobVar.indice += GlobVar.namos;

                                    GlobVar.inicioTela += GlobVar.namos / GlobVar.namos;
                                    GlobVar.finalTela += GlobVar.namos / GlobVar.namos;
                                    GlobVar.ponteiroVideo += GlobVar.namos;

                                    //TelaClearAndReload();
                                    gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
                                    //UpdateInicioTela();
                                    foiencontradoumUltimo = false;
                                    foiencontradoumUltimo = false;

                                }
                            }
                            //openglControl1.Invalidate();
                            //plotEventos.DrawingAnEvent(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                        }
                        if (redimensionando && bordSize)
                        {
                            //TelaClearAndReload();
                            gl.ClearColor(1, 1, 1, 1);
                            // Posição absoluta do mouse em relação ao formulário
                            int mouseXGlobal = this.PointToClient(Control.MousePosition).X;

                            // Cálculo correto do novo width do painelExames
                            GlobVar.novaLarguraPainel = mouseXGlobal - painelExames.Left;

                            // Define limites mínimos/máximos do painelExames
                            GlobVar.larguraMinimaPainel = 50;
                            int larguraMaximaPainel = this.ClientSize.Width / 3;

                            GlobVar.novaLarguraPainel = Math.Max(GlobVar.larguraMinimaPainel, Math.Min(GlobVar.novaLarguraPainel, larguraMaximaPainel));

                            // Candidato à nova posição e largura do openglControl1
                            int novaPosXOpenGL = GlobVar.novaLarguraPainel + 9;
                            GlobVar.novaLarguraOpenGL = this.ClientSize.Width - novaPosXOpenGL - 30;

                            // Define limites mínimos/máximos do openglControl1
                            GlobVar.larguraMinimaOpenGL = this.ClientSize.Width / 3;
                            int larguraMaximaOpenGL = this.ClientSize.Width - GlobVar.larguraMinimaPainel;

                            GlobVar.novaLarguraOpenGL = Math.Max(GlobVar.larguraMinimaOpenGL, Math.Min(GlobVar.novaLarguraOpenGL, larguraMaximaOpenGL));
                                                        
                            GlobVar.XRedimension = novaPosXOpenGL;

                            float outX = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);

                            float deltaX = outX - initialMousePosition.X;

                            deltaX = GlobVar.ponteiroRedimension - outX;

                            if (e.X < lastMousePosition.X)
                            {
                                GlobVar.ponteiroRedimension -= ((int)Math.Abs(deltaX));
                            }
                            else
                            {
                                GlobVar.ponteiroRedimension += ((int)Math.Abs(deltaX));
                            }


                            initialMousePosition.X = (int)outX;

                            lastMousePosition = e.Location;

                            openglControl1.Refresh();
                            TelaClearAndReload();

                        }
                    }
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeAll;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                            float initialMouseX = initialMousePosition.X;
                            float initialMouseY = initialMousePosition.Y;

                            if (e.X != lastMousePosition.X && e.Y != lastMousePosition.Y)
                            {
                                float deltaX = outX - initialMouseX;
                                float deltaY = outY - initialMouseY;
                                float YiPlus = e.Y - lastMousePosition.Y;
                                // Atualizar as coordenadas do quadrado
                                GlobVar.XiYi.X += (int)deltaX;
                                GlobVar.XiYi.Y += (int)deltaY;
                                GlobVar.XfYf.X += (int)deltaX;
                                GlobVar.Yi += (int)YiPlus;

                                // Atualizar a posição inicial do mouse para a nova posição
                                initialMousePosition.X = (int)outX;
                                initialMousePosition.Y = (int)outY;
                            }
                            // Atualizar a última posição do mouse
                            lastMousePosition.X = e.X;
                            lastMousePosition.Y = e.Y;

                        }
                    }

                    /* x Inixio */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeWE;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";
                        }
                        else
                        {
                            float outX = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);
                            float initialMouseX = initialMousePosition.X;

                            float deltaX = outX - initialMouseX;
                            if (e.X != lastMousePosition.X)
                            {
                                int AuXsize = GlobVar.XiYi.X + GlobVar.XSize;

                                GlobVar.XiYi.X += (int)deltaX;
                                GlobVar.XSize = Math.Abs(AuXsize - GlobVar.XiYi.X);
                                if (GlobVar.XSize < 250)
                                {
                                    GlobVar.XSize = 250;
                                }
                                initialMousePosition.X = (int)outX;

                            }
                            lastMousePosition.X = e.X;

                        }
                    }
                    /* x Fim */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeWE;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);
                            float initialMouseX = initialMousePosition.X;

                            float deltaX = outX - initialMouseX;
                            if (e.X != lastMousePosition.X)
                            {
                                GlobVar.XSize += (int)deltaX;
                                if (GlobVar.XSize < 250)
                                {
                                    GlobVar.XSize = 250;
                                }

                                initialMousePosition.X = (int)outX;

                            }
                            lastMousePosition.X = e.X;

                        }

                    }
                    /* y Inixio */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeNS;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                            float initialMouseY = initialMousePosition.Y;

                            if (e.Y != lastMousePosition.Y)
                            {

                                float deltaY = outY - initialMouseY;
                                float ddeltaY = e.Y - lastMousePosition.Y;

                                int AuYsize = GlobVar.Yi + GlobVar.YSize;

                                GlobVar.Yi += (int)ddeltaY;
                                GlobVar.XiYi.Y += (int)deltaY;
                                GlobVar.YSize = AuYsize - GlobVar.Yi;
                                if (GlobVar.YSize < 30)
                                {
                                    GlobVar.YSize = 30;
                                }
                                initialMousePosition.Y = (int)outY;

                            }
                            lastMousePosition.Y = e.Y;
                        }

                    }
                    /* y Fim */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeNS;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                            float initialMouseY = initialMousePosition.Y;

                            if (e.Y != lastMousePosition.Y)
                            {
                                float deltaY = outY - initialMouseY;

                                GlobVar.YSize -= (int)deltaY;
                                if (GlobVar.YSize < 30)
                                {
                                    GlobVar.YSize = 30;
                                }

                                initialMousePosition.Y = (int)outY;

                            }
                            lastMousePosition.Y = e.Y;

                        }

                    }

                    /* X0 - Y0 */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeNWSE;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;

                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                            float initialMouseX = initialMousePosition.X;
                            float initialMouseY = initialMousePosition.Y;

                            float deltaX = outX - initialMouseX;
                            if (e.X != lastMousePosition.X)
                            {
                                float deltaY = outY - initialMouseY;
                                float ddeltaY = e.Y - lastMousePosition.Y;

                                int AuXsize = GlobVar.XiYi.X + GlobVar.XSize;
                                int AuYsize = GlobVar.Yi + GlobVar.YSize;

                                GlobVar.Yi += (int)ddeltaY;
                                GlobVar.XiYi.Y += (int)deltaY;
                                GlobVar.YSize = AuYsize - GlobVar.Yi;
                                GlobVar.XiYi.X += (int)deltaX;
                                GlobVar.XSize = Math.Abs(AuXsize - GlobVar.XiYi.X);
                                if (GlobVar.XSize < 250)
                                {
                                    GlobVar.XSize = 250;
                                }
                                if (GlobVar.YSize < 30)
                                {
                                    GlobVar.YSize = 30;
                                }

                                initialMousePosition.X = (int)outX;
                                initialMousePosition.Y = (int)outY;

                            }
                            lastMousePosition.X = e.X;
                            lastMousePosition.Y = e.Y;

                        }

                    }
                    /* X0 - Y1 */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeNESW;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);
                            float initialMouseX = initialMousePosition.X;


                            float initialMouseY = initialMousePosition.Y;

                            if (e.Y != lastMousePosition.Y && e.X != lastMousePosition.X)
                            {
                                float deltaY = outY - initialMouseY;
                                float deltaX = outX - initialMouseX;
                                int AuXsize = GlobVar.XiYi.X + GlobVar.XSize;

                                GlobVar.YSize -= (int)deltaY;
                                initialMousePosition.Y = (int)outY;


                                GlobVar.XiYi.X += (int)deltaX;
                                GlobVar.XSize = Math.Abs(AuXsize - GlobVar.XiYi.X);
                                if (GlobVar.XSize < 250)
                                {
                                    GlobVar.XSize = 250;
                                }
                                if (GlobVar.YSize < 30)
                                {
                                    GlobVar.YSize = 30;
                                }

                                initialMousePosition.X = (int)outX;

                            }
                            lastMousePosition.X = e.X;
                            lastMousePosition.Y = e.Y;

                        }

                    }
                    /* X1 - Y0 */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeNESW;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                            float initialMouseY = initialMousePosition.Y;
                            float initialMouseX = initialMousePosition.X;

                            float deltaX = outX - initialMouseX;
                            if (e.Y != lastMousePosition.Y)
                            {

                                float deltaY = outY - initialMouseY;
                                float ddeltaY = e.Y - lastMousePosition.Y;

                                int AuYsize = GlobVar.Yi + GlobVar.YSize;

                                GlobVar.Yi += (int)ddeltaY;
                                GlobVar.XiYi.Y += (int)deltaY;
                                GlobVar.YSize = AuYsize - GlobVar.Yi;
                                GlobVar.XSize += (int)deltaX;
                                if (GlobVar.XSize < 250)
                                {
                                    GlobVar.XSize = 250;
                                }
                                if (GlobVar.YSize < 30)
                                {
                                    GlobVar.YSize = 30;
                                }

                                initialMousePosition.Y = (int)outY;
                                initialMousePosition.X = (int)outX;

                            }
                            lastMousePosition.Y = e.Y;
                            lastMousePosition.X = e.X;
                        }
                    }
                    /* X1 - Y1 */
                    else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeNWSE;
                            openglControl1.DoRender();
                            plotComentatios.DrawCommentBorder(gl);
                            Stringao.Text = "";

                        }
                        else
                        {
                            float outX = 0;
                            float outY = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);
                            float initialMouseX = initialMousePosition.X;


                            float initialMouseY = initialMousePosition.Y;

                            if (e.Y != lastMousePosition.Y && e.X != lastMousePosition.X)
                            {
                                float deltaY = outY - initialMouseY;
                                float deltaX = outX - initialMouseX;

                                GlobVar.XSize += (int)deltaX;
                                initialMousePosition.X = (int)outX;
                                if (GlobVar.XSize < 250)
                                {
                                    GlobVar.XSize = 250;
                                }
                                if (GlobVar.YSize < 30)
                                {
                                    GlobVar.YSize = 30;
                                }

                                GlobVar.YSize -= (int)deltaY;
                                initialMousePosition.Y = (int)outY;

                            }
                            lastMousePosition.X = e.X;
                            lastMousePosition.Y = e.Y;

                        }

                    }

                    else if (isAnEvent && (!this.isAnStartEvent || !this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        timerClick.Start();

                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeAll;
                            openglControl1.DoRender();
                            plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                            toolTip1.SetToolTip(openglControl1, GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%");
                            Stringao.Text = GlobVar.AtualEvento + " - " + $"{GlobVar.DuracaoEvent:F2}" + " Seg";
                        }
                        else
                        {
                            Stringao.Text = "";

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
                    else if ((!isAnEvent && this.isAnStartEvent) || (!isAnEvent && this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeWE;
                            openglControl1.DoRender();
                            plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                            toolTip1.SetToolTip(openglControl1, GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%");
                            Stringao.Text = "";

                        }


                        if (isDrawing)
                        {
                            toolTip1.RemoveAll();
                            timerClick.Start();

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
                                    if (e.X >= openglControl1.Size.Width)
                                    {
                                        //lastMousePosition = e.Location;
                                        camera.X += GlobVar.namos;

                                        GlobVar.indiceNumero += 8 * 1; //(int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED; // 8 * 1;
                                        GlobVar.maximaNumero += 8 * 1;

                                        GlobVar.maximaVect += GlobVar.namos;
                                        GlobVar.indice += GlobVar.namos;

                                        GlobVar.inicioTela += GlobVar.namos / GlobVar.namos;
                                        GlobVar.finalTela += GlobVar.namos / GlobVar.namos;
                                        gl.Translate(-Tela_Plotagem.camera.X, 0, 1);
                                        UpdateInicioTela();
                                        foiencontradoumUltimo = false;
                                        foiencontradoumUltimo = false;
                                        GlobVar.ponteiroVideo += GlobVar.namos;

                                        ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);
                                        lastMousePosition = e.Location;
                                    }

                                    deltaX = (int)Math.Abs(outX - GlobVar.durEventoMove);
                                    locMuse.X = e.X;
                                    dimMouse.X = (int)outX;
                                    if (e.X < lastMousePosition.X)
                                    {
                                        endEvent.Y = GlobVar.durEventoMove;
                                        GlobVar.durEventoMove -= ((int)Math.Abs(deltaX));
                                        Math.Abs(GlobVar.durEventoMove);
                                        endEvent.X = GlobVar.durEventoMove;

                                    }
                                    else if (e.X >= lastMousePosition.X)
                                    {

                                        endEvent.Y = GlobVar.durEventoMove;

                                        GlobVar.durEventoMove += ((int)Math.Abs(deltaX));

                                        endEvent.X = GlobVar.durEventoMove;
                                    }

                                    GlobVar.startX = GlobVar.iniEventoMove;
                                    //openglControl1.Invalidate();
                                    //plotEventos.DrawBordenInAnEvent(isDrawing, gl, GlobVar.desenhoLoc);

                                }

                                initialMousePosition.X = (int)outX;

                                lastMousePosition = e.Location;
                                //TelaClearAndReload();


                                openglControl1.Refresh();
                                //plotEventos.DrawBordenInAnEvent(isDrawing, gl, GlobVar.desenhoLoc);

                            }
                        }
                    }

                    else if ((!isAnEvent && !this.isAnStartEvent) || (!isAnEvent && this.isAnEndEvent) && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && isThereVideoPonteiro)
                    {
                        isTelaClearAndReloadExecuted = false;

                        if (!isMouseDown)
                        {
                            this.Cursor = Cursors.SizeWE;
                            openglControl1.DoRender();
                            Stringao.Text = "";

                        }
                        if (isDrawing)
                        {
                            toolTip1.RemoveAll();
                            timerClick.Start();

                            float outX = 0;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);

                            float deltaX = outX - initialMousePosition.X;

                            if (e.X != lastMousePosition.X)
                            {
                                if (isThereVideoPonteiro)
                                {
                                    deltaX = GlobVar.ponteiroVideo - outX;

                                    if (e.X < lastMousePosition.X)
                                    {
                                        GlobVar.ponteiroVideo -= ((int)Math.Abs(deltaX));
                                    }
                                    else
                                    {
                                        GlobVar.ponteiroVideo += ((int)Math.Abs(deltaX));
                                    }

                                }

                                initialMousePosition.X = (int)outX;

                                lastMousePosition = e.Location;


                                openglControl1.Refresh();
                                TelaClearAndReload();
                                if (videoIni)
                                {
                                    iCelera.telinha.attLocVideo();
                                    iCelera.telinha.videoPlayer.Ctlcontrols.pause();
                                }
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
        private void ConfigurarTooltips(Control container)
        {
            foreach (Control controle in container.Controls)
            {
                if (controle is Button botao)
                {
                    // Adiciona os eventos para exibir e esconder o tooltip
                    botao.MouseEnter += (s, e) => MostrarTooltip(botao);
                    botao.MouseLeave += (s, e) => EsconderTooltip();
                }
                else if (controle is Panel painel)
                {
                    // Configura os botões nos subpainéis
                    ConfigurarTooltips(painel);
                }
            }
        }
        private void MostrarTooltip(Button botao)
        {
            // Configura o conteúdo e a posição do tooltip
            toolTip2.Show(botao.Name, botao, 0, botao.Height); // Dura 2 segundos
        }
        private void EsconderTooltip()
        {
            toolTip2.Hide(painelComando); // Esconde o tooltip do painel principal
        }
        int lastStartX;
        public static Point LocationMouseClickComentario;
        public static Point DimensionRightClick;
        private void OpenGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                plotanu = false;

                if (e.Button == MouseButtons.Left)
                {
                    if (!isAnEvent && !isAnStartEvent && !isAnEndEvent && !isThereAComment && (!isThereAXSartComment && !isThereAXEndComment && !isThereAYStartComment && !isThereAYEndComment) && (!isThereX0Y0Comment && !isThereX0Y1Comment && !isThereX1Y0Comment && !isThereX1Y1Comment) && !isThereVideoPonteiro && !redimensionando)
                    {
                        isDrawing = false;
                        isDrawingRectangle = false;
                        tooltipEvento.Hide(openglControl1);
                        tooltipTimer.Stop();
                        if (!crtlAtivo) {
                            //UpdateLoc();
                            isDrawingRectangle = false;
                            isDrawing = false;

                            ConvertToOpenGLCoordinates(e.X, e.Y, out Plotagem.endX, out Plotagem.endY);
                            GlobVar.endX = (int)Plotagem.endX;

                            if (GlobVar.startX == GlobVar.endX)
                            {

                                if (EventoUmClicked)
                                {
                                    var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                                    .Where(row => row.Field<int>("CodEvento") == GlobVar.lastEvent).CopyToDataTable();

                                    if (GlobVar.startX != null && GlobVar.lastEvent != null && (rowInfoEvento.Rows[0]["DuracaoEvento"] != DBNull.Value) || Convert.ToInt32(rowInfoEvento.Rows[0]["DuracaoEvento"]) != 0)
                                    {
                                        int minimoPossivel = Convert.ToInt32(rowInfoEvento.Rows[0]["DuracaoEvento"]);
                                        int tamanhoEmTela = minimoPossivel * GlobVar.namos;

                                        GlobVar.endX = (int)GlobVar.startX + tamanhoEmTela;

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
                                    else
                                    {
                                        GlobVar.startX = null;
                                    }
                                }
                                else
                                {
                                    GlobVar.startX = null;
                                }
                                isDrawing = false;
                            }

                            else if (GlobVar.startX != null && GlobVar.startX != GlobVar.endX)
                            {
                                int LimiteAux = Math.Abs(GlobVar.endX - (int)GlobVar.startX);
                                if (GlobVar.lastEvent != null)
                                {
                                    var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == GlobVar.lastEvent).CopyToDataTable();
                                    int minimoPossivel = (rowInfoEvento.Rows[0]["DuracaoEvento"] == DBNull.Value) ? 0 : Convert.ToInt32(rowInfoEvento.Rows[0]["DuracaoEvento"]);
                                    int LimiteEmMinAux = LimiteAux / GlobVar.namos;
                                    if(MinEventClicked && LimiteEmMinAux <= minimoPossivel)
                                    {

                                    }
                                    else
                                    {
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
                                }
                            }
                            timerClick.Stop();

                            openglControl1.DoRender();
                            plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);

                        }

                        timer3.Start();
                    }
                    else if (isAnEvent)
                    {
                        if (crtlAtivo)
                        {
                            isDrawing = false;
                            TelaClearAndReload();
                            UpdateInicioTela();
                        }
                        else
                        {
                            float outX = 0;
                            isDrawing = false;
                            ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out Plotagem.startY);


                            GlobVar.endX = GlobVar.durEventoMove;

                            if (GlobVar.iniEventoMove != GlobVar.startX)
                            {
                                plotEventos.UpdateEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                            }
                            lastMousePosition = e.Location;
                            TelaClearAndReload();
                            UpdateInicioTela();
                            timerClick.Stop();
                        }
                    }
                    else if (isThereAComment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();

                        timerClick.Stop();
                    }
                    else if (isThereAXSartComment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();

                        timerComment.Stop();
                    }
                    else if (isThereAXEndComment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();

                        timerComment.Stop();
                    }
                    else if (isThereAYStartComment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();

                        timerComment.Stop();
                    }
                    else if (isThereAYEndComment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();
                        timerComment.Stop();
                    }

                    else if (isThereX0Y0Comment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();
                        timerComment.Stop();
                    }
                    else if (isThereX0Y1Comment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();
                        timerComment.Stop();
                    }
                    else if (isThereX1Y0Comment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();
                        timerComment.Stop();
                    }
                    else if (isThereX1Y1Comment)
                    {
                        isDrawing = false;

                        plotComentatios.UpdateComment(e.X, e.Y);
                        lastMousePosition = e.Location;
                        TelaClearAndReload();
                        UpdateInicioTela();
                        timerComment.Stop();
                    }

                    else if (this.isAnStartEvent || this.isAnEndEvent)
                    {

                        if (crtlAtivo)
                        {
                            isDrawing = false;
                            TelaClearAndReload();
                            UpdateInicioTela();

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
                            UpdateInicioTela();

                        }
                    }
                    else if (isThereVideoPonteiro)
                    {
                        isDrawing = false;
                        TelaClearAndReload();
                        UpdateInicioTela();

                    }
                    else if (redimensionando)
                    {
                        //openglControl1.Visible = true;

                        redimensionando = false;
                        bordSize = false;
                        this.Cursor = Cursors.Default;
                        // Só aplica se ainda respeita todos os limites
                        if (GlobVar.novaLarguraOpenGL >= GlobVar.larguraMinimaOpenGL && GlobVar.novaLarguraPainel >= GlobVar.larguraMinimaPainel)
                        {
                            //TelaClearAndReload();

                            painelExames.Width = GlobVar.novaLarguraPainel;

                            openglControl1.Location = new Point(GlobVar.XRedimension, openglControl1.Location.Y);
                            openglControl1.Width = GlobVar.novaLarguraOpenGL;

                            AjustaPanelsX();

                            GlobVar.sizeOpenGl.X = openglControl1.Width;
                            GlobVar.sizeOpenGl.Y = openglControl1.Height;

                            //TelaClearAndReload();
                        }

                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    isDrawing = false;
                    float aux;
                    float auy;
                    ConvertToOpenGLCoordinates(e.X, e.Y, out aux, out auy);
                    DimensionRightClick.X = (int)aux;
                    DimensionRightClick.Y = (int)auy;
                    LocationMouseClickComentario = e.Location;
                }
                clickCount = 0;
                timer2.Stop();

                // Restaurar o cursor original e marca que o mouse foi liberado
                this.Cursor = originalCursor;
                isMouseDown = false;
                TelaClearAndReload();
                UpdateInicioTela();
            }
            catch
            {
            }
        }
        private void OpenglControl1_MouseHover(object sender, EventArgs e)
        {
            try
            {
                if (this.Cursor == Cursors.SizeAll)
                {
                    //toolTip1.SetToolTip(openglControl1, GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%");
                    //toolTip1.Show(GlobVar.Event + "\nINICIO : 10min e 32 segundos\nDuração : 14 segundos\nValor dessaturação : 87%", openglControl1, lastMousePosition.X, lastMousePosition.Y);
                }
            }
            catch { }
        }
        private void UpdateLoc(string canal = null)
        {
            try {
                if (timer2.Enabled) {
                    MouseLoc.Text = $"{stopwatch.Elapsed.TotalSeconds} seconds";
                }
            }
            catch { }
        }
        public static void ConvertToOpenGLCoordinates(int mouseX, int mouseY, out float openGLX, out float openGLY)
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

                for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
                {
                    int ampli = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]);
                    int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) - 1;
                    int newAmpli = Convert.ToInt32(GlobVar.Amplitude[indexAmpli]);
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
                    int ampli = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[i]["AmplitudeMin"]);
                    int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) + 1;
                    int newAmpli = Convert.ToInt32(GlobVar.Amplitude[indexAmpli]);
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
                //UpdateSelected(sender);

                //timer1.Start();

                int ampli = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"]);
                int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) - 1;
                int newAmpli = Convert.ToInt32(GlobVar.Amplitude[indexAmpli]);
                GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"] = newAmpli;
                float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(index));

                GlobVar.scale[index] = scala;

                UpdateInicioTela();

                TelaClearAndReload();

            }
            catch
            {

            }
        }
        private void minusLb1_Click(object sender, EventArgs e)
        {
            try
            {
                //UpdateSelected(sender);
                //timer1.Start();
                int alturaTela = (int)openglControl1.Height;

                int ampli = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"]);
                int indexAmpli = GlobVar.Amplitude.IndexOf(ampli) + 1;
                int newAmpli = Convert.ToInt32(GlobVar.Amplitude[indexAmpli]);
                GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"] = newAmpli;
                float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(index));

                GlobVar.scale[index] = scala;

                UpdateInicioTela();


                TelaClearAndReload();

            }
            catch
            {

            }
        }
        public static void AjustaPanelsX()
        {
            foreach(Control ct in painelExames.Controls)
            {
                if(ct is Panel pn)
                {
                    pn.Width = painelExames.Width;
                    foreach(Control crt in pn.Controls)
                    {
                        if(crt is Label lg)
                        {
                            if(lg.Tag != null)
                            {
                                if (lg.Tag.Equals("scala"))
                                {
                                    // Medir o tamanho do texto com a fonte atual
                                    Size tamanhoTexto = TextRenderer.MeasureText(lg.Text, lg.Font);

                                    int tamanho = Math.Abs((pn.Width - tamanhoTexto.Width) - 4);
                                    lg.Location = new Point(tamanho, lg.Location.Y);
                                }
                            }
                        }
                    }
                }
            }
        }
        private bool isScrollingRight = true;
        private bool crtlAtivo = false;
        private bool telaMovi = false;
        public string tecla = "";
        //Comecando a mexer nos KeyDown para alterar os eventos, usar o KeyUp para "replotar a tela"
        private async void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        //private void TelaPlotagem_KeyDown(object sender, KeyEventArgs e)
        //private void TelaPlotagem_KeyDown()
        {
            try
            {
                //tecla = e.KeyCode.ToString();
                if (isAnEvent || isAnEndEvent || isAnStartEvent)
                {
                    if (e.KeyValue == 46)
                    {
                        plotEventos.DeleteEvent(GlobVar.iniEventoMove, GlobVar.durEventoMove, GlobVar.CodCanalEvent, GlobVar.desenhoLoc, GlobVar.startY, GlobVar.seqEvento, GlobVar.CodEvento);
                    }
                    else {
                        var rowteclaRapida = GlobVar.tbl_CadEvento.AsEnumerable()
                                                                    .Where(row => row.Field<int>("TeclaRapida") == e.KeyValue);
                        if (rowteclaRapida.Any())
                        {
                            var tableTeclaRapida = rowteclaRapida.CopyToDataTable();
                            for (int i = 0; i < tableTeclaRapida.Rows.Count; i++)
                            {
                                if (!GlobVar.EventHasChange)
                                {
                                    int NewCodEvento = Convert.ToInt32(tableTeclaRapida.Rows[i]["CodEvento"]);
                                    plotEventos.ChangeEventType(GlobVar.CodCanalEvent, GlobVar.seqEvento, NewCodEvento);
                                }
                            }
                        }
                    }
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;

                    int alturaTela = (int)openglControl1.Height;
                    //gl.Translate(camera.X, 0, 1);
                    TelaClearAndReload();
                    UpdateInicioTela();

                }
                else
                {
                    if (e.KeyValue == 162 || e.KeyValue == 163 || e.KeyValue == 131072 || e.KeyValue == 17)
                    {
                        crtlAtivo = true;
                    }
                    switch (e.KeyData)
                    {
                        case Keys.Escape:
                            this.Close();
                            GlobVar.ConnectionBDdat.Close();

                            break;

                        case Keys.W:
                            if (GlobVar.indice > 0)
                            {
                                if (!conc)
                                {
                                    int newLoc = GlobVar.indice - (int)GlobVar.namos;
                                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                    {
                                        LeituraEmMatrizTeste.Pause();
                                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(3, newLoc);
                                    }
                                }
                                camera.X -= GlobVar.namos * GlobVar.SPEED;
                                GlobVar.indiceNumero -= (int)GlobVar.namosNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero -= (int)GlobVar.namosNumerico * (int)GlobVar.SPEED;
                                if (GlobVar.indiceNumero < 0)
                                {
                                    GlobVar.indiceNumero = 0;
                                    GlobVar.maximaNumero = GlobVar.namosNumerico;
                                }

                                GlobVar.maximaVect -= (int)GlobVar.namos * (int)GlobVar.SPEED;
                                GlobVar.indice -= (int)GlobVar.namos * (int)GlobVar.SPEED;

                                if (GlobVar.indice < 0)
                                {
                                    GlobVar.indice = 0;
                                    GlobVar.maximaVect = (int)GlobVar.namos;
                                    camera.X = 0;
                                }

                                GlobVar.inicioTela -= ((int)GlobVar.namos * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela -= ((int)GlobVar.namos * (int)GlobVar.SPEED) / GlobVar.namos;
                                if (GlobVar.inicioTela < 0)
                                {
                                    GlobVar.inicioTela = 0;
                                    GlobVar.finalTela = (int)GlobVar.namos / (int)GlobVar.namos;
                                }
                                GlobVar.ponteiroVideo -= (int)GlobVar.namos * (int)GlobVar.SPEED;

                                if (!conc) LeituraEmMatrizTeste.Resume();

                            }
                            break;
                        case Keys.S:
                            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                            {
                                if (!conc)
                                {
                                    int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                    {
                                        LeituraEmMatrizTeste.Pause();
                                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(4, newLoc);
                                    }
                                }

                                camera.X += GlobVar.namos * GlobVar.SPEED;
                                GlobVar.indiceNumero += (int)GlobVar.namosNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero += (int)GlobVar.namosNumerico * (int)GlobVar.SPEED;

                                GlobVar.maximaVect += (int)GlobVar.namos * (int)GlobVar.SPEED;
                                GlobVar.indice += (int)GlobVar.namos * (int)GlobVar.SPEED;

                                GlobVar.inicioTela += ((int)GlobVar.namos * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela += ((int)GlobVar.namos * (int)GlobVar.SPEED) / GlobVar.namos;
                                LeituraEmMatrizTeste.Resume();
                                GlobVar.ponteiroVideo += (int)GlobVar.namos * (int)GlobVar.SPEED;

                            }

                            break;
                        case Keys.D:
                            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                            {
                                if (!conc)
                                {
                                    int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                    {
                                        LeituraEmMatrizTeste.Pause();
                                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(4, newLoc);
                                    }
                                }

                                camera.X += GlobVar.saltoTelas * GlobVar.SPEED;
                                GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                LeituraEmMatrizTeste.Resume();
                                GlobVar.ponteiroVideo += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                if (hScrollBar1.InvokeRequired)
                                {
                                    hScrollBar1.Invoke(new Action(() =>
                                    {
                                        hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                                    }));
                                }
                                else
                                {
                                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                                }

                            }
                            break;
                        case Keys.PageDown:
                            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                            {
                                if (!conc)
                                {
                                    int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                    {
                                        LeituraEmMatrizTeste.Pause();
                                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(4, newLoc);
                                    }
                                }

                                camera.X += GlobVar.saltoTelas * GlobVar.SPEED;

                                GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                //UpdateInicioTela();
                                //TelaClearAndReload();
                                LeituraEmMatrizTeste.Resume();
                                GlobVar.ponteiroVideo += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            }

                            break;
                        case Keys.A:
                            if (GlobVar.indice > 0)
                            {
                                if (!conc)
                                {
                                    int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                    {
                                        LeituraEmMatrizTeste.Pause();
                                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(3, newLoc);
                                    }
                                }
                                camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;
                                GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                if (GlobVar.indiceNumero < 0)
                                {
                                    GlobVar.indiceNumero = 0;
                                    GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                                }

                                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                if (GlobVar.indice < 0)
                                {
                                    GlobVar.indice = 0;
                                    GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                                    camera.X = 0;
                                }

                                GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                if (GlobVar.inicioTela < 0)
                                {
                                    GlobVar.inicioTela = 0;
                                    GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
                                }
                                if (!conc) LeituraEmMatrizTeste.Resume();
                                GlobVar.ponteiroVideo -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                if (hScrollBar1.InvokeRequired)
                                {
                                    hScrollBar1.Invoke(new Action(() =>
                                    {
                                        hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                                    }));
                                }
                                else
                                {
                                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                                }

                            }
                            break;
                        case Keys.PageUp:
                            if (GlobVar.indice > 0)
                            {
                                if (!conc)
                                {
                                    int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                                    {
                                        LeituraEmMatrizTeste.Pause();
                                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(3, newLoc);
                                    }
                                }

                                camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;

                                GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                                if (GlobVar.indiceNumero < 0)
                                {
                                    GlobVar.indiceNumero = 0;
                                    GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                                }

                                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                                if (GlobVar.indice < 0)
                                {
                                    GlobVar.indice = 0;
                                    GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                                    camera.X = 0;
                                }

                                GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                                if (GlobVar.inicioTela < 0)
                                {
                                    GlobVar.inicioTela = 0;
                                    GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
                                }
                                //UpdateInicioTela();
                                //TelaClearAndReload();
                                if (!conc) LeituraEmMatrizTeste.Resume();
                                GlobVar.ponteiroVideo -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            }
                            break;

                        case Keys.NumPad0:
                            Marcar0.PerformClick();
                            break;
                        case Keys.D0:
                            Marcar0.PerformClick();
                            break;
                        case Keys.NumPad5:
                            MarcarR.PerformClick();
                            break;
                        case Keys.D5:
                            MarcarR.PerformClick();
                            break;
                        case Keys.R:
                            MarcarR.PerformClick();
                            break;

                        case Keys.NumPad1:
                            if (AdInf.Equals("A") || AdInf.Equals("I")) Marcar1.PerformClick();
                            break;
                        case Keys.D1:
                            if (AdInf.Equals("A") || AdInf.Equals("I")) Marcar1.PerformClick();
                            break;
                        case Keys.NumPad2:
                            if (AdInf.Equals("A") || AdInf.Equals("I")) Marcar2.PerformClick();
                            break;
                        case Keys.D2:
                            if (AdInf.Equals("A") || AdInf.Equals("I")) Marcar2.PerformClick();
                            break;
                        case Keys.NumPad3:
                            if (AdInf.Equals("A") || AdInf.Equals("I")) Marcar3.PerformClick();
                            break;
                        case Keys.D3:
                            if (AdInf.Equals("A") || AdInf.Equals("I")) Marcar3.PerformClick();
                            break;


                        case Keys.N:
                            if (AdInf.Equals("B") || AdInf.Equals("C") || AdInf.Equals("I")) N.PerformClick();
                            break;
                        case Keys.T:
                            if (AdInf.Equals("B") || AdInf.Equals("C") || AdInf.Equals("I")) T.PerformClick();
                            break;

                    }
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;

                    int alturaTela = (int)openglControl1.Height;
                    TelaClearAndReload();
                    UpdateInicioTela();

                }
                switch (e.KeyData)
                {
                    case Keys.End:
                        if (conc)
                        {
                            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();
                            int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]);
                            maximoPossivel = maximoPossivel / GlobVar.segundos - 1;

                            ptsEmTela.Text = $"{maximoPossivel}";
                            ptsEmTela.Focus();

                            // Simula o pressionamento da tecla Enter
                            var enterKeyEvent = new KeyEventArgs(Keys.Enter); // '\r' representa o Enter

                            PtsEmTela_KeyDown(ptsEmTela, enterKeyEvent);

                            // Remove o foco do TextBox e coloca no controle openglControl1
                            openglControl1.Focus();
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                        }
                        else
                        {
                            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();
                            int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]);
                            LeituraEmMatrizTeste.Pause();
                            await LeituraEmMatrizTeste.CarregamentoMontagemRapido(1,0);

                            maximoPossivel = maximoPossivel / GlobVar.segundos - 1;

                            ptsEmTela.Text = $"{maximoPossivel}";
                            ptsEmTela.Focus();

                            // Simula o pressionamento da tecla Enter
                            var enterKeyEvent = new KeyEventArgs(Keys.Enter); // '\r' representa o Enter

                            PtsEmTela_KeyDown(ptsEmTela, enterKeyEvent);

                            // Remove o foco do TextBox e coloca no controle openglControl1
                            openglControl1.Focus();
                            
                            LeituraEmMatrizTeste.Resume();
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                        }
                        break;

                    case Keys.Home:
                        if (conc)
                        {
                            int inicio = 0;

                            ptsEmTela.Text = $"{inicio}";
                            ptsEmTela.Focus();

                            // Simula o pressionamento da tecla Enter
                            var enterKeyEvent = new KeyEventArgs(Keys.Enter); // '\r' representa o Enter
                            PtsEmTela_KeyDown(ptsEmTela, enterKeyEvent);

                            // Remove o foco do TextBox e coloca no controle openglControl1
                            openglControl1.Focus();
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                        }
                        else
                        {
                            int inicio = 0;
                            //pausar a task aqui
                            LeituraEmMatrizTeste.Pause();
                            await LeituraEmMatrizTeste.CarregamentoMontagemRapido(0,0);

                            ptsEmTela.Text = $"{inicio}";
                            ptsEmTela.Focus();

                            // Simula o pressionamento da tecla Enter
                            var enterKeyEvent = new KeyEventArgs(Keys.Enter); // '\r' representa o Enter
                            PtsEmTela_KeyDown(ptsEmTela, enterKeyEvent);

                            // Remove o foco do TextBox e coloca no controle openglControl1
                            openglControl1.Focus();
                            //retomar a task aqui
                            LeituraEmMatrizTeste.Resume();
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;


                        }
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void TelaPlotagem_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e != null)
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
        public static bool isScroll = false;
        bool notClick = false;
        int lastScroll = 0;
        private async void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                isScroll = true;
                // Verifique se hScrollBar1.Value está atualizado
                hScrollBar1.Value = (int)e.NewValue;

                bool isRight = e.NewValue > lastScroll;
                bool isLeft = e.NewValue < lastScroll;
                int newLoc =  512 * e.NewValue;
                int newLocNum = 8 * e.NewValue;
                if (!conc)
                {
                    if(newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                    {
                        LeituraEmMatrizTeste.Pause();
                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(2, newLoc);
                        LeituraEmMatrizTeste.Resume();
                    }
                }
                camera.X = newLoc;
                GlobVar.indiceNumero = newLocNum;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico); ;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.indice = newLoc;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos); ;

                // Verifique se GlobVar.namos não é zero
                if (GlobVar.namos != 0)
                {
                    GlobVar.inicioTela = newLoc / GlobVar.namos;
                    GlobVar.finalTela = newLoc / GlobVar.namos;
                }
                if (GlobVar.indice < 0)
                {
                    GlobVar.indice = 0;
                    GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                    camera.X = 0;
                }
                if (GlobVar.indiceNumero < 0)
                {
                    GlobVar.indiceNumero = 0;
                    GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                }
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;

                foiencontradoumUltimo = false;

                int alturaTela = (int)openglControl1.Height;
                lastScroll = (int)e.NewValue;

                TelaClearAndReload();
                UpdateInicioTela();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }
        private void tempoEmTela_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tmpTela = tempoEmTela.Text;
            Match match = Regex.Match(tmpTela, @"\d+");
            if (match.Success)
            {
                GlobVar.segundos = int.Parse(match.Value);
            }


            GlobVar.tmpEmTela = (GlobVar.namos * GlobVar.segundos);
            GlobVar.saltoTelas = GlobVar.tmpEmTela;

            GlobVar.inicioTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
            GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos + (int)GlobVar.inicioTela;

            GlobVar.tmpEmTelaNumerico = (GlobVar.namosNumerico * GlobVar.segundos);
            GlobVar.finalTelaNumerico = (int)GlobVar.tmpEmTelaNumerico / (int)GlobVar.namosNumerico;
            GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;

            GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
            GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
            float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);
            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

            hScrollBar1.LargeChange = GlobVar.segundos;

            UpdateInicioTela();
            TelaClearAndReload();
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
        public static bool MarcaDAguaAtiva = false;
        private void MarcaDAguia_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.BackColor == Color.Lime)
                {
                    btn.BackColor = Color.FromArgb(50, 147, 50);  // Cor para indicar seleção

                    // Alterar a cor do texto, se necessário
                    btn.ForeColor = Color.White;

                    // Alterar o estilo da borda, se necessário
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;
                    MarcaDAguaAtiva = true;
                    UpdateInicioTela();
                    TelaClearAndReload();

                }
                else
                {
                    // Restaurar a cor original do botão
                    btn.BackColor = Color.Lime;  // Cor original
                    btn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = Color.Black;
                    MarcaDAguaAtiva = false;
                    UpdateInicioTela();
                    TelaClearAndReload();

                }

            }

        }
        public static bool MarcaDAguaNaTelaAtiva = false;
        private const string NomePainelMarcaDagua = "painelMarcaDagua";
        public static int AlturaMarcaDagua = 30; // altura desejada do painel transparente

        private void MarcaDAguaNaTela_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            if (MarcaDAguaNaTelaAtiva)
            {
                // DESATIVANDO
                MarcaDAguaNaTelaAtiva = false;

                // Estilo do botão
                btn.BackColor = Color.Lime;
                btn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = Color.Black;

                // Remover painel de marca d'água
                var painel = painelExames.Controls.OfType<Panel>().FirstOrDefault(p => p.Name == NomePainelMarcaDagua);
                if (painel != null)
                {
                    painelExames.Controls.Remove(painel);
                }

                // Conta apenas os painéis de canais (ignorando o da marca d'água)
                var paineisCanais = painelExames.Controls
                    .OfType<Panel>()
                    .OrderBy(p => p.Top)
                    .ToList();
                int qtd = MarcaDAguaNaTelaAtiva ? paineisCanais.Count : GlobVar.tbl_MontagemSelecionada.Rows.Count;

                int normalSize = painelExames.Height / qtd;

                int lastTop = 0;

                foreach (Panel pn in painelExames.Controls)
                {
                    pn.Top = lastTop;
                    pn.Height = normalSize;
                    lastTop += normalSize;
                }

                int indiot = 0;
                foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                {
                    int topPn = pn.Top;
                    int auuuu = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                    int meioPn = pn.Height;
                    if (indiot < GlobVar.desenhoLoc.Length)
                    {
                        GlobVar.desenhoLoc[indiot] = topPn + meioPn;
                    }
                    indiot++;
                }
                UpdatePanelHeightInDataTable();
                AjustarFonteDosLabels();

                UpdatePanelHeightInDataTable();
                AjustarFonteDosLabels();
                AjustarBotoesMinusEPlus();
                RepositionPanels();
            }
            else
            {
                // ATIVANDO
                MarcaDAguaNaTelaAtiva = true;

                btn.BackColor = Color.FromArgb(50, 147, 50);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderSize = 2;
                btn.FlatAppearance.BorderColor = Color.DarkGreen;

                // Criar painel transparente no topo
                Panel marca = new Panel
                {
                    Name = NomePainelMarcaDagua,
                    Height = AlturaMarcaDagua,
                    Width = painelExames.Width,
                    Location = new Point(0, 0),
                    Tag = -1,
                    BackColor = Color.Transparent // ou Color.FromArgb(0, 0, 0, 0)                    
                };

                // Painel transparente precisa ser o primeiro na lista de Controls (z-order)
                painelExames.Controls.Add(marca);
                painelExames.Controls.SetChildIndex(marca, 0);

                marca.Visible = false;
                // Conta apenas os painéis de canais (ignorando o da marca d'água)
                var paineisCanais = painelExames.Controls
                    .OfType<Panel>()
                    .Where(p => p.Location.X != -500)
                    .OrderBy(p => p.Top)
                    .ToList();
                int qtd = MarcaDAguaNaTelaAtiva ? paineisCanais.Count : GlobVar.tbl_MontagemSelecionada.Rows.Count;

                int normalSize = painelExames.Height / qtd;

                int lastTop = 0;
                
                foreach (Panel pn in painelExames.Controls)
                {
                    if(pn.Location.X < 0)
                    {
                        continue;
                    }
                    pn.Top = lastTop;
                    pn.Height = normalSize;
                    lastTop += normalSize;
                }

                int indiot = 0;
                foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                {
                    if (pn.Location.X < 0)
                    {
                        continue;
                    }
                    int topPn = pn.Top;
                    int auuuu = Math.Abs(pn.Top - Tela_Plotagem.painelExames.Height);

                    int meioPn = pn.Height;
                    if (indiot < GlobVar.desenhoLoc.Length)
                    {
                        GlobVar.desenhoLoc[indiot] = topPn + meioPn;
                    }
                    indiot++;
                }
                UpdatePanelHeightInDataTable();
                AjustarFonteDosLabels();
                UpdatePanelHeightInDataTable();
                AjustarFonteDosLabels();
                AjustarBotoesMinusEPlus();
                ReorganizaPaineis(marca);
            }

            UpdateInicioTela();
            TelaClearAndReload();
        }
        private void ReorganizaPaineis(Panel marca)
        {
            int offsetY = MarcaDAguaNaTelaAtiva ? AlturaMarcaDagua : 0;

            List<Panel> listaPainels = new List<Panel>();
            listaPainels.Add(marca);
            foreach (Control ctrl in painelExames.Controls)
            {
                if (ctrl is Panel p)
                {
                    if(p.Location.X != -500 || !p.Visible)
                    {
                        if (p.Visible)
                        {
                            listaPainels.Add(p);
                        }
                    }
                }
            }


            const int spacing = 0;
            int y = 0; // Coordenada inicial Y para o reposicionamento
            foreach (Panel panel in listaPainels)
            {
                // Atualizar a posição Y do painel
                panel.Top = y;

                // Atualizar a posição Y para o próximo painel, considerando o espaçamento
                y = panel.Bottom + spacing;
            }

            List<int> agor = new List<int>();
            // Atualizar o array `desenhoLoc` com a nova posição dos painéis visíveis
            int index = 0;
            foreach (Panel pn in listaPainels)
            {
                // Calcular a posição do centro do painel e armazenar em `GlobVar.desenhoLoc`
                int meioPn = pn.Top + (pn.Height / 2);
                agor.Add(meioPn);

                index++;
            }
            GlobVar.desenhoLoc = new float[agor.Count];
            index = 0;
            for (int i = 0; i < GlobVar.desenhoLoc.Length; i++)
            {
                GlobVar.desenhoLoc[i] = agor[i];
            }
            // Caso tenha menos painéis visíveis do que o tamanho original de `GlobVar.desenhoLoc`,
            // zera os valores restantes
            for (int i = index; i < GlobVar.desenhoLoc.Length; i++)
            {
                GlobVar.desenhoLoc[i] = 0;
            }



        }

        public void CalcularQtdImpressao()
        {
            // Verifica se o DataTable existe e possui linhas
            if (GlobVar.tbl_SelImpressao == null || GlobVar.tbl_SelImpressao.Rows.Count == 0)
            {
                GlobVar.qtdImpressao = 0;
                return;
            }

            // Usa LINQ para agrupar os valores da coluna "CodImpressao" e contar as linhas correspondentes
            var agrupado = GlobVar.tbl_SelImpressao.AsEnumerable()
                .GroupBy(row => row.Field<string>("CodImpressao"));

            // Soma todas as quantidades de agrupamento
            GlobVar.qtdImpressao = agrupado.Count();
        }
        public void UpdateInicioTela()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateInicioTela));
                return;
            }

            string nome = GlobVar.tbl_DadosExame.Rows[0]["Nome"].ToString();
            string sexo = $"({GlobVar.tbl_DadosExame.Rows[0]["Sexo"].ToString()})";
            string idade = $"{GlobVar.tbl_DadosExame.Rows[0]["IdadeAno"]} anos";
            string altura = $"{GlobVar.tbl_DadosExame.Rows[0]["Altura"].ToString()}m";
            string realizacao = GlobVar.tbl_DadosExame.Rows[0]["DataRealizacao"].ToString().Substring(0, 10);
            string arquivo = $"{Path.GetFileNameWithoutExtension(GlobVar.textFile)}";

            this.Text = $"iCelera - {nome} {sexo} {idade} - {altura} - Realizacao: {realizacao} - Arquivo: {arquivo}.DAT";

            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            int inicio = (GlobVar.indice / GlobVar.namos);
            TimeSpan tempo = TimeSpan.FromSeconds(inicio);
            string horasI = tempo.Hours.ToString().PadLeft(2, '0');
            string minutosI = tempo.Minutes.ToString().PadLeft(2, '0');
            string segundosI = tempo.Seconds.ToString().PadLeft(2, '0');

            // Atualização de controles com segurança de thread
            fimTela.Text = $"{horasI}:{minutosI}:{segundosI}";

            int segI = Convert.ToInt32(segundosI);

            if (GlobVar.segundos == 30 && (segI == 30 || segI == 0))
            {
                //PainelMarca.Enabled = true;

                foreach (Control cr in PainelMarca.Controls)
                {
                    if (cr is Button btn)
                    {
                        if (Convert.ToInt32(btn.Tag) != -1)
                        {
                            btn.Enabled = true;
                            btn.BackColor = Color.MediumSlateBlue;
                            btn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                            btn.FlatAppearance.BorderSize = 1;
                            btn.FlatAppearance.BorderColor = Color.Black;
                        }
                    }
                }
            }
            else
            {
                //PainelMarca.Enabled = false;

                foreach (Control cr in PainelMarca.Controls)
                {
                    if (cr is Button btn)
                    {
                        if (Convert.ToInt32(btn.Tag) != -1)
                        {
                            btn.Enabled = false;
                            btn.BackColor = Color.Gray;
                            btn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                            btn.FlatAppearance.BorderSize = 1;
                            btn.FlatAppearance.BorderColor = Color.Black;
                        }
                    }
                }
            }

            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(r => r.Field<int>("NumPag") == paginaCoerente);
            string horario = row["Horario"].ToString();
            inicioTela.Text = $"{horario.Substring(11)}";

            int pagina = paginaCoerente / 30;
            string telaPagText = pagina.ToString("D3");
            ptsEmTela.Text = $"{telaPagText}";

            int indexLabel = 0;
            for (int i = 1; i <= GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                FieldInfo labelInfo = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                if (labelInfo != null)
                {
                    Label label = (Label)labelInfo.GetValue(this);
                    if (label != null)
                    {
                        label.Text = $"{GlobVar.tbl_MontagemSelecionada.Rows[indexLabel]["AmplitudeMin"]}μV";
                        indexLabel++;
                    }
                }
            }

            int estagioAtual = Convert.ToInt32(row["Estagio"]);
            if (estagioAtual == 5) { lbEstagio.Text = "Estágio: R"; } else if (estagioAtual == 4) { lbEstagio.Text = "Estágio: N"; } else if (estagioAtual == 6) { lbEstagio.Text = "Estágio: T"; } else { lbEstagio.Text = "Estágio: " + estagioAtual.ToString(); }
            
            Atual.BackgroundImage = GetEstagioImage(estagioAtual);
            Atual.BackgroundImageLayout = ImageLayout.Stretch;
            if(GlobVar.tbl_SelImpressao != null)
            {
                var dt = GlobVar.tbl_SelImpressao.AsEnumerable()
                .GroupBy(r => r.Field<int>("CodImpressao"))
                .OrderBy(g => g.Key);
                int qtd = dt.Count();

                lbImpressao.Text = "Impressão: " + qtd.ToString();
            }
            else
            {
                lbImpressao.Text = "Impressão: 0";
            }

            if (!isScroll)
            {
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                isScroll = true;
            }
            atualizaButAntProx();
            //CalcularQtdImpressao();
            if (videoIni)
            {
                iCelera.telinha.attLocVideo();
                iCelera.telinha.videoPlayer.Ctlcontrols.pause();
            }
            if(Janela != null)
            {
                Janela.Desenha();
            }
            openglControl1.Focus();



        }

        public static void AjustaBotoesParaTipoExadeAdInf()
        {
            int locX = 1;
            int locY = 1;
            int Tamanho = 47;
            bool ContemNome(Button bt, string termo)
            {
                return bt.Name.IndexOf(termo, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            // Função robusta para extrair Tag como int (protege contra cast exceptions)
            int ObterTag(Button bt)
            {
                if (bt.Tag is int i)
                    return i;
                if (int.TryParse(bt.Tag?.ToString(), out int r))
                    return r;
                return -999;
            }

            if (AdInf.Equals("A"))
            {
                List<int> AdultoButEstagio = new List<int> { 0, 1, 2, 3, 5 };
                int TamanhoPanel = 6 * 47 + 2 + 6;
                PainelMarca.Size = new Size(TamanhoPanel, PainelMarca.Size.Height);

                foreach (Button bt in PainelMarca.Controls)
                {
                    if(Convert.ToInt32(bt.Tag) == 0)
                    {
                        bt.Location = new Point(1, 1);
                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 1)
                    {
                        bt.Location = new Point(48, 1);
                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 2)
                    {
                        bt.Location = new Point(95, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 3)
                    {
                        bt.Location = new Point(142, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 5)
                    {
                        bt.Location = new Point(189, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == -1)
                    {
                        bt.Location = new Point(242, 1);

                        bt.Visible = true;
                    }
                    else
                    {
                        bt.Visible = false;
                    }
                }
                TamanhoPanel = 13 * Tamanho + (locX * 2) + 6;
                PainelMarcaAntProx.Size = new Size(TamanhoPanel, PainelMarcaAntProx.Size.Height);
                // Lista de regras: cada regra tem predicate (condição) e ação associada
                var regras = new List<(Predicate<Button> condicao, Action<Button> acao)>
                {
                    // Tag==0
                    (bt => ObterTag(bt) == 0 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(1, 1);      bt.Visible = true; }),
                    (bt => ObterTag(bt) == 0 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(48, 1);     bt.Visible = true; }),

                    // Tag==1
                    (bt => ObterTag(bt) == 1 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(95, 1);     bt.Visible = true; }),
                    (bt => ObterTag(bt) == 1 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(142, 1);    bt.Visible = true; }),

                    // Tag==2
                    (bt => ObterTag(bt) == 2 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(189, 1);    bt.Visible = true; }),
                    (bt => ObterTag(bt) == 2 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(236, 1);    bt.Visible = true; }),

                    // Tag==3
                    (bt => ObterTag(bt) == 3 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(283, 1);    bt.Visible = true; }),
                    (bt => ObterTag(bt) == 3 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(330, 1);    bt.Visible = true; }),

                    // Tag==5 (supondo R significa Tag 5)
                    (bt => ObterTag(bt) == 5 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(377, 1);    bt.Visible = true; }),
                    (bt => ObterTag(bt) == 5 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(424, 1);    bt.Visible = true; }),

                    // Diferenças
                    (bt => ContemNome(bt, "anterior diferente"), bt => { bt.Location = new Point(471, 1);    bt.Visible = true; }),
                    (bt => ContemNome(bt, "seguinte diferente"),  bt => { bt.Location = new Point(518, 1);    bt.Visible = true; }),

                    // Tag==-1
                    (bt => ObterTag(bt) == -1, bt => { bt.Location = new Point(571, 1); bt.Visible = true; }),
                };

                foreach (Button bt in PainelMarcaAntProx.Controls.OfType<Button>())
                {
                    // Procura a primeira regra que bate
                    var regra = regras.FirstOrDefault(map => map.condicao(bt));

                    if (regra.acao != null)
                    {
                        // Se casou alguma regra, aplica a ação
                        regra.acao(bt);
                    }
                    else
                    {
                        // Caso nenhum match, esconda
                        bt.Visible = false;
                    }
                }
            }
            else if (AdInf.Equals("I"))
            {
                List<int> AdultoButEstagio = new List<int> { 0, 1, 2, 3, 5 };
                int TamanhoPanel = 7 * 47 + 2 + 6;
                PainelMarca.Size = new Size(TamanhoPanel, PainelMarca.Size.Height);

                foreach (Button bt in PainelMarca.Controls)
                {
                    if (Convert.ToInt32(bt.Tag) == 0)
                    {
                        bt.Location = new Point(1, 1);
                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 1)
                    {
                        bt.Location = new Point(48, 1);
                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 2)
                    {
                        bt.Location = new Point(95, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 3)
                    {
                        bt.Location = new Point(142, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 5)
                    {
                        bt.Location = new Point(189, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 4)
                    {
                        bt.Location = new Point(236, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == -1)
                    {
                        bt.Location = new Point(289, 1);

                        bt.Visible = true;
                    }
                    else
                    {
                        bt.Visible = false;
                    }
                }
                TamanhoPanel = 13 * Tamanho + (locX * 2) + 6;
                PainelMarcaAntProx.Size = new Size(TamanhoPanel, PainelMarcaAntProx.Size.Height);
                // Lista de regras: cada regra tem predicate (condição) e ação associada
                var regras = new List<(Predicate<Button> condicao, Action<Button> acao)>
                {
                    // Tag==0
                    (bt => ObterTag(bt) == 0 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(1, 1);      bt.Visible = true; }),
                    (bt => ObterTag(bt) == 0 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(48, 1);     bt.Visible = true; }),

                    // Tag==1
                    (bt => ObterTag(bt) == 1 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(95, 1);     bt.Visible = true; }),
                    (bt => ObterTag(bt) == 1 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(142, 1);    bt.Visible = true; }),

                    // Tag==2
                    (bt => ObterTag(bt) == 2 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(189, 1);    bt.Visible = true; }),
                    (bt => ObterTag(bt) == 2 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(236, 1);    bt.Visible = true; }),

                    // Tag==3
                    (bt => ObterTag(bt) == 3 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(283, 1);    bt.Visible = true; }),
                    (bt => ObterTag(bt) == 3 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(330, 1);    bt.Visible = true; }),

                    // Tag==5 (supondo R significa Tag 5)
                    (bt => ObterTag(bt) == 5 && ContemNome(bt, "anterior"), bt => { bt.Location = new Point(377, 1);    bt.Visible = true; }),
                    (bt => ObterTag(bt) == 5 && ContemNome(bt, "seguinte"),  bt => { bt.Location = new Point(424, 1);    bt.Visible = true; }),
                    // Tag 4
                    (bt => ObterTag(bt) == 4 && ContemNome(bt, "anterior"), bt =>{ bt.Location = new Point(471, 1); bt.Visible = true; }),
                    (bt => ObterTag(bt) == 4 && ContemNome(bt, "seguinte"), bt => { bt.Location = new Point(518, 1); bt.Visible = true; }),

                    // Diferenças
                    (bt => ContemNome(bt, "anterior diferente"), bt => { bt.Location = new Point(565, 1);    bt.Visible = true; }),
                    (bt => ContemNome(bt, "seguinte diferente"),  bt => { bt.Location = new Point(612, 1);    bt.Visible = true; }),

                    // Tag==-1
                    (bt => ObterTag(bt) == -1, bt => { bt.Location = new Point(665, 1); bt.Visible = true; }),
                };

                foreach (Button bt in PainelMarcaAntProx.Controls.OfType<Button>())
                {
                    // Procura a primeira regra que bate
                    var regra = regras.FirstOrDefault(map => map.condicao(bt));

                    if (regra.acao != null)
                    {
                        // Se casou alguma regra, aplica a ação
                        regra.acao(bt);
                    }
                    else
                    {
                        // Caso nenhum match, esconda
                        bt.Visible = false;
                    }
                }

            }
            else
            {
                List<int> CriancaoButEstagio = new List<int> { 0, 5, 4, 6 };
                int TamanhoPanel = 5 * 47 + 2 + 6;
                PainelMarca.Size = new Size(TamanhoPanel, PainelMarca.Size.Height);

                foreach (Button bt in PainelMarca.Controls)
                {
                    if (Convert.ToInt32(bt.Tag) == 0)
                    {
                        bt.Location = new Point(1, 1);
                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 5)
                    {
                        bt.Location = new Point(48, 1);
                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 4)
                    {
                        bt.Location = new Point(95, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == 6)
                    {
                        bt.Location = new Point(142, 1);

                        bt.Visible = true;
                    }
                    else if (Convert.ToInt32(bt.Tag) == -1)
                    {
                        bt.Location = new Point(195, 1);

                        bt.Visible = true;
                    }
                    else
                    {
                        bt.Visible = false;
                    }
                }
                TamanhoPanel = 11 * 47 + 2 + 6;
                PainelMarcaAntProx.Size = new Size(TamanhoPanel, PainelMarcaAntProx.Size.Height);
                // Função utilitária para busca case-insensitive

                // Mapeamento declarativo: predicate/button → location
                var mapeamento = new List<(Predicate<Button> predicate, Point location)>
                {
                    // Tag 0
                    (bt => ObterTag(bt) == 0 && ContemNome(bt, "anterior"), new Point(1, 1)),
                    (bt => ObterTag(bt) == 0 && ContemNome(bt, "seguinte"), new Point(48, 1)),

                    // Tag 5
                    (bt => ObterTag(bt) == 5 && ContemNome(bt, "anterior"), new Point(95, 1)),
                    (bt => ObterTag(bt) == 5 && ContemNome(bt, "seguinte"), new Point(142, 1)),

                    // Tag 4
                    (bt => ObterTag(bt) == 4 && ContemNome(bt, "anterior"), new Point(189, 1)),
                    (bt => ObterTag(bt) == 4 && ContemNome(bt, "seguinte"), new Point(236, 1)),

                    // Tag 6
                    (bt => ObterTag(bt) == 6 && ContemNome(bt, "anterior"), new Point(283, 1)),
                    (bt => ObterTag(bt) == 6 && ContemNome(bt, "seguinte"), new Point(330, 1)),

                    // Diferenças específicas (prioridade maior!)
                    (bt => ContemNome(bt, "anterior diferente"), new Point(377, 1)),
                    (bt => ContemNome(bt, "seguinte diferente"), new Point(424, 1)),

                    // Tag -1
                    (bt => ObterTag(bt) == -1, new Point(477, 1))
                };

                // Agora, seu loop fica enxuto, seguro e expansível:
                foreach (Button bt in PainelMarcaAntProx.Controls.OfType<Button>())
                {
                    bool found = false;
                    foreach (var map in mapeamento)
                    {
                        if (map.predicate(bt))
                        {
                            bt.Location = map.location;
                            bt.Visible = true;
                            found = true;
                            break; // Não continua em outros mapas!
                        }
                    }
                    if (!found)
                        bt.Visible = false;
                }
            }
        }
        public static async void OnVideoStateChanged(bool isPlaying)
        {
            if (isPlaying)
            {
                if(GlobVar.ponteiroVideo > GlobVar.indice + (GlobVar.segundos * GlobVar.namos))
                {
                    if (!conc)
                    {
                        int newLoc = GlobVar.indice - (int)GlobVar.saltoTelas;
                        if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                        {
                            LeituraEmMatrizTeste.Pause();
                            await LeituraEmMatrizTeste.CarregamentoMontagemRapido(4, newLoc);
                        }
                    }

                    camera.X += GlobVar.saltoTelas * GlobVar.SPEED;
                    GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                    GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;

                    GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                    GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                    GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                    LeituraEmMatrizTeste.Resume();
                    videoIni = false;
                    Tela_Plotagem.Instance?.UpdateInicioTela();
                    videoIni = true;
                }
                // Realiza ações quando o vídeo está tocando
                TelaClearAndReload();
            }
            else
            {
                // Realiza ações quando o vídeo está pausadoF
                // Por exemplo, parar atualizações
            }
        }
        public static Image GetEstagioImage(int estagioAtual)
        {
            string path = estagioAtual switch
            {
                0 => GlobVar.diretorioEstagioAtual0,
                1 => GlobVar.diretorioEstagioAtual1,
                2 => GlobVar.diretorioEstagioAtual2,
                3 => GlobVar.diretorioEstagioAtual3,
                5 => GlobVar.diretorioEstagioAtualR,
                4 => GlobVar.diretorioEstagioAtualN,
                6 => GlobVar.diretorioEstagioAtualT,
                _ => null
            };

            return path != null ? Image.FromFile(path) : null;
        }

        bool foiencontradoum = false;
        int lastfounded;
        int ultimoTipoCanalProcurado;
        int pagAndou;
        private void ProximoEvento_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int TipoProximoEvento = (int)btn.Tag;
            int pagAtual = GlobVar.indice;
            int[] codProximosEventos;
            if (TipoProximoEvento != ultimoTipoCanalProcurado) { foiencontradoum = false; }

            var codigoTipoProximo = GlobVar.tbl_EventoTipoCanal.AsEnumerable().Where(row => row.Field<int>("CodTipoCanal") == TipoProximoEvento).CopyToDataTable();
            codProximosEventos = new int[codigoTipoProximo.Rows.Count];

            for (int i = 0; i < codProximosEventos.Length; i++)
            {
                codProximosEventos[i] = Convert.ToInt32(codigoTipoProximo.Rows[i]["CodEvento"]);
            }
            // Encontra a próxima linha no DataTable GlobVar.eventosUpdate onde a coluna "Inicio" é maior que pagAtual
            var proximaLinha = GlobVar.eventosUpdate.AsEnumerable()
                .Where(row => row.Field<int>("Inicio") > pagAtual)  // Primeiro filtro: "Inicio" > pagAtual
                .Where(row => codProximosEventos.Contains(row.Field<int>("CodEvento")))  // Segundo filtro: "CodEvento" dentro do vetor codProximosEventos
                .OrderBy(row => row.Field<int>("Inicio"))  // Ordena pelas linhas com menor valor de "Inicio" após o filtro
                .FirstOrDefault();  // Pega a primeira linha correspondente
            if (proximaLinha != null)
            { //Feito para caso ja tenha sido encontrado um evento
                if (foiencontradoum)
                {
                    proximaLinha = GlobVar.eventosUpdate.AsEnumerable()
                                    .Where(row => row.Field<int>("Inicio") > lastfounded)  // Primeiro filtro: "Inicio" > pagAtual
                                    .Where(row => codProximosEventos.Contains(row.Field<int>("CodEvento")))  // Segundo filtro: "CodEvento" dentro do vetor codProximosEventos
                                    .OrderBy(row => row.Field<int>("Inicio"))  // Ordena pelas linhas com menor valor de "Inicio" após o filtro
                                    .FirstOrDefault();  // Pega a primeira linha correspondente
                }
            }
            if (proximaLinha != null) {

                pagAndou = pagAtual;
                ultimoTipoCanalProcurado = TipoProximoEvento;
                foiencontradoum = true;
                int proxPag = (Convert.ToInt32(proximaLinha[4]) / 512) / GlobVar.segundos;
                lastfounded = Convert.ToInt32(proximaLinha[4]);
                if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                {

                    int NovaLoc = GlobVar.namos * proxPag * GlobVar.segundos;
                    int NovaLocNumerico = GlobVar.numeroAmos * proxPag * GlobVar.segundos;

                    camera.X = NovaLoc;

                    camera.X = NovaLoc;
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.indice = NovaLoc;
                    GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);

                    GlobVar.indiceNumero = NovaLocNumerico;
                    GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                    foiencontradoumUltimo = false;
                    UpdateInicioTela();
                    TelaClearAndReload();
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                }
            }
            else
            {
                foiencontradoum = false;
            }

            int h = 1;
        }
        bool foiencontradoumUltimo = false;
        int lastfoundedUltimo;
        int ultimoTipoCanalProcuradoUltimo;
        private void UltimoEvento_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int TipoProximoEvento = (int)btn.Tag;
            int pagAtual = GlobVar.indice;
            int[] codProximosEventos;
            if (TipoProximoEvento != ultimoTipoCanalProcuradoUltimo) { foiencontradoumUltimo = false; }

            var codigoTipoProximo = GlobVar.tbl_EventoTipoCanal.AsEnumerable().Where(row => row.Field<int>("CodTipoCanal") == TipoProximoEvento).CopyToDataTable();
            codProximosEventos = new int[codigoTipoProximo.Rows.Count];

            for (int i = 0; i < codProximosEventos.Length; i++)
            {
                codProximosEventos[i] = Convert.ToInt32(codigoTipoProximo.Rows[i]["CodEvento"]);
            }
            // Encontra a próxima linha no DataTable GlobVar.eventosUpdate onde a coluna "Inicio" é maior que pagAtual
            var proximaLinha = GlobVar.eventosUpdate.AsEnumerable()
                        .Where(row => row.Field<int>("Inicio") < pagAtual)  // Filtro: "Inicio" < pagAtual
                        .Where(row => codProximosEventos.Contains(row.Field<int>("CodEvento")))  // Filtro: "CodEvento" dentro do vetor codUltimosEventos
                        .OrderByDescending(row => row.Field<int>("Inicio"))  // Ordena pelas linhas com maior valor de "Inicio"
                        .FirstOrDefault();  // Pega a primeira linha correspondente
            if (proximaLinha != null)
            { //Feito para caso ja tenha sido encontrado um evento
                if (foiencontradoumUltimo)
                {
                    proximaLinha = GlobVar.eventosUpdate.AsEnumerable()
                            .Where(row => row.Field<int>("Inicio") < lastfoundedUltimo)  // Filtro: "Inicio" < lastfoundedUltimo
                            .Where(row => codProximosEventos.Contains(row.Field<int>("CodEvento")))  // Filtro: "CodEvento" dentro do vetor codUltimosEventos
                            .OrderByDescending(row => row.Field<int>("Inicio"))  // Ordena pelas linhas com maior valor de "Inicio"
                            .FirstOrDefault();  // Pega a primeira linha correspondente
                }
            }
            if (proximaLinha != null)
            {

                pagAndou = pagAtual;
                ultimoTipoCanalProcuradoUltimo = TipoProximoEvento;
                foiencontradoumUltimo = true;
                int proxPag = (Convert.ToInt32(proximaLinha[4]) / 512) / GlobVar.segundos;
                lastfoundedUltimo = Convert.ToInt32(proximaLinha[4]);
                if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                {

                    int NovaLoc = GlobVar.namos * proxPag * GlobVar.segundos;
                    int NovaLocNumerico = GlobVar.numeroAmos * proxPag * GlobVar.segundos;

                    camera.X = NovaLoc;

                    camera.X = NovaLoc;
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.indice = NovaLoc;
                    GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                    GlobVar.indiceNumero = NovaLocNumerico;
                    GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                    foiencontradoum = false;
                    UpdateInicioTela();
                    TelaClearAndReload();
                }
            }
            else
            {
                foiencontradoumUltimo = false;
            }

            int h = 1;
        }
        private void IndoBomDiaBoaNoiteCPAP(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int TipoProximoEvento = (int)btn.Tag;

            // Encontra a próxima linha no DataTable GlobVar.eventosUpdate onde a coluna "Inicio" é maior que pagAtual
            var proximaLinha = GlobVar.eventosUpdate.AsEnumerable() // Filtro: "Inicio" < pagAtual
                        .Where(row => row.Field<int>("CodEvento") == TipoProximoEvento)  // Filtro: "CodEvento" dentro do vetor codUltimosEventos
                        .FirstOrDefault();  // Pega a primeira linha correspondente
            if (proximaLinha != null)
            {

                int proxPag = ((Convert.ToInt32(proximaLinha[4]) / 512) / GlobVar.segundos);
                if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                {

                    int NovaLoc = GlobVar.namos * proxPag * GlobVar.segundos;
                    int NovaLocNumerico = GlobVar.numeroAmos * proxPag * GlobVar.segundos;

                    camera.X = NovaLoc;

                    camera.X = NovaLoc;
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.indice = NovaLoc;
                    GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;

                    GlobVar.indiceNumero = NovaLocNumerico;
                    GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                    UpdateInicioTela();
                    TelaClearAndReload();
                }
            }
        }
        bool foiencontradoumComent = false;
        int lastfoundedComment;
        private void ProximoComentario_Click(object sender, EventArgs e)
        {
            int pagAtual = ((GlobVar.indice / 512) / 30) + 30;

            if (pagAndou != pagAtual) { foiencontradoumComent = false; }

            var proximaLinha = GlobVar.tbl_Comentarios.AsEnumerable()
                    .Where(row => row.Field<int>("NumPag") > pagAtual)  // Primeiro filtro: "Inicio" > pagAtual
                    .FirstOrDefault();  // Pega a primeira linha correspondente
            if (proximaLinha != null && foiencontradoumComent)
            {
                proximaLinha = GlobVar.tbl_Comentarios.AsEnumerable()
                    .Where(row => row.Field<int>("NumPag") > lastfoundedComment)  // Primeiro filtro: "Inicio" > lastfoundedComment
                    .FirstOrDefault();  // Pega a primeira linha correspondente
            }
            if (proximaLinha != null)
            {
                pagAndou = pagAtual;
                int proxPag = Convert.ToInt32(proximaLinha[3]) / 30;
                lastfoundedComment = Convert.ToInt32(proximaLinha[3]);
                foiencontradoumComent = true;

                if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                {

                    int NovaLoc = GlobVar.namos * proxPag * GlobVar.segundos;
                    int NovaLocNumerico = GlobVar.numeroAmos * proxPag * GlobVar.segundos;

                    camera.X = NovaLoc;

                    camera.X = NovaLoc;
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.indice = NovaLoc;
                    GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                    GlobVar.indiceNumero = NovaLocNumerico;
                    GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;

                    UpdateInicioTela();
                    TelaClearAndReload();
                }
            }
        }
        bool foiencontradoumComentLast = false;
        int lastfoundedCommentLast;
        private void AnteriorComentario_Click(object sender, EventArgs e)
        {
            int pagAtual = (GlobVar.indice / 512);

            if (pagAndou != pagAtual) { foiencontradoumComentLast = false; }

            var proximaLinha = GlobVar.tbl_Comentarios.AsEnumerable()
                    .Where(row => row.Field<int>("NumPag") < pagAtual)  // Primeiro filtro: "Inicio" > pagAtual
                    .FirstOrDefault();  // Pega a primeira linha correspondente
            if (proximaLinha != null && foiencontradoumComentLast)
            {
                proximaLinha = GlobVar.tbl_Comentarios.AsEnumerable()
                    .Where(row => row.Field<int>("NumPag") < lastfoundedCommentLast)  // Primeiro filtro: "Inicio" > lastfoundedComment
                    .OrderByDescending(row => row.Field<int>("Seq"))  // Ordena pelas linhas com maior valor de "Inicio"
                    .FirstOrDefault();  // Pega a primeira linha correspondente
            }
            if (proximaLinha != null)
            {
                pagAndou = pagAtual;
                int proxPag = Convert.ToInt32(proximaLinha[3]) / 30;
                lastfoundedCommentLast = Convert.ToInt32(proximaLinha[3]);
                foiencontradoumComentLast = true;

                if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                {

                    int NovaLoc = GlobVar.namos * proxPag * GlobVar.segundos;
                    int NovaLocNumerico = GlobVar.numeroAmos * proxPag * GlobVar.segundos;

                    camera.X = NovaLoc;

                    camera.X = NovaLoc;
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.indice = NovaLoc;
                    GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                    GlobVar.indiceNumero = NovaLocNumerico;
                    GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;

                    UpdateInicioTela();
                    TelaClearAndReload();
                }
            }
            else
            {
                foiencontradoumComentLast = false;
            }
        }
        private void MenorSat_Click(object sender, EventArgs e)
        {
            DataTable DtDesprezar = new DataTable();
            if (GlobVar.eventos.AsEnumerable().Any(row => row.Field<int>("CodEvento") == 100)){
                DtDesprezar = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 100).CopyToDataTable();

            }
            var numsDesprezar = new HashSet<int>(DtDesprezar.AsEnumerable()
                    .Select(row => row.Field<int>("NumPag")));

            int linhaSaturacao = GlobVar.codSelected.IndexOf(66); // A linha que você quer verificar
            int menorValor = 100; // Inicializa com o maior valor possível
            int posicaoMenorValor = -1; // Armazena a posição do menor valor
            int numPagMenorValro = -1;
            foreach(DataRow rw in GlobVar.tbl_Paginas.Rows)
            {
                int valorAtual = Convert.ToInt32(rw["SatBasal"]);
                int numPagAtual = Convert.ToInt32(rw["NumPag"]);

                if (!numsDesprezar.Contains(numPagAtual))
                {
                    if (valorAtual >= 20 && valorAtual < menorValor)
                    {
                        menorValor = valorAtual;
                        numPagMenorValro = numPagAtual;
                    }
                }
            }
            /*
            for (int i = 0; i < GlobVar.matrizCanal.GetLength(1); i += GlobVar.namosNumerico)
            {
                int valorAtual = GlobVar.matrizCanal[linhaSaturacao, i];
                int numPagAtual = i / GlobVar.namosNumerico;

                // Verifica se o valor numPagAtual está no HashSet numsDesprezar
                if (!numsDesprezar.Contains(numPagAtual))
                {
                    // Ignorar valores menores que 20
                    if (valorAtual >= 20 && valorAtual < menorValor)
                    {
                        menorValor = valorAtual;
                        posicaoMenorValor = i;
                    }
                }
            }
            */
            int proxPag = numPagMenorValro / GlobVar.segundos;// (posicaoMenorValor / GlobVar.numeroAmos) / 30;


            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {
                int NovaLoc = GlobVar.namos * proxPag * GlobVar.segundos;
                int NovaLocNumerico = GlobVar.numeroAmos * proxPag * GlobVar.segundos;

                camera.X = NovaLoc;

                camera.X = NovaLoc;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.indice = NovaLoc;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.indiceNumero = NovaLocNumerico;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                foiencontradoum = false;
                foiencontradoumUltimo = false;

                UpdateInicioTela();
                TelaClearAndReload();
            }

            // Se encontrou um valor mínimo válido
            if (numPagMenorValro != -1)
            {
                AutoCloseMessageBox.Show(
                    $"Menor valor encontrado: {menorValor}",
                    "Valor Encontrado",
                    3000 // 3 segundos (3000 ms)
                );
            }
            else
            {
                MessageBox.Show("Nenhum valor válido encontrado.", "Aviso", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Warning);
            }

        }
        // Método auxiliar para exibir um input box para o usuário
        public static string estagioatutxt = "";
        public static string estagioatutxt1 = "";
        public static string estagioatutxt2 = "";
        public static string estagioatutxt3 = "";
        public static string estagioatutxt4 = "";
        public static string estagioatutxt5 = "";
        public static string estagioatutxt6 = "";
        public static string estagioatutxt7 = "";
        public static string[] estagios = new string[8]
        {
                estagioatutxt,
                estagioatutxt1,
                estagioatutxt2,
                estagioatutxt3,
                estagioatutxt4,
                estagioatutxt5,
                estagioatutxt6,
                estagioatutxt7
        };
        private void Profile_Click(object sender, EventArgs e)
        {
            
            int codPaciente = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CodPaciente"]);

            ProfileForm profileForm = new ProfileForm(codPaciente);
            profileForm.ShowDialog();

            AdInf = GlobVar.tbl_DadosExame.Rows[0]["AdInf"].ToString();
            ArrumarTbl_Estagios();
            AjustaBotoesParaTipoExadeAdInf();
            ReorganizarPainelComando();

            UpdateInicioTela();
        }
        private void CopiaTela_Click(object sender, EventArgs e)
        {
            // Caminho base para salvar o arquivo
            string caminhoPasta = @"C:\Users\dev_i\source\repos\Dat";

            // Extrair o nome base do arquivo a partir de GlobVar.textFile
            string nomeBaseArquivo = $"{Path.GetFileNameWithoutExtension(GlobVar.textFile)}";

            // Chamar o método para capturar a tela e salvar como BMP
            SalvarPrintTela(caminhoPasta, nomeBaseArquivo);

        }
        private void SalvarPrintTela(string caminhoPasta, string nomeBaseArquivo)
        {
            try
            {
                // Verificar se o diretório existe, caso contrário, criá-lo
                if (!Directory.Exists(caminhoPasta))
                {
                    Directory.CreateDirectory(caminhoPasta);
                }

                // Iniciar o sufixo com "_00" e incrementar caso o arquivo já exista
                int contador = 0;
                string sufixo = "_tela00";
                string caminhoCompleto;

                // Loop para encontrar um nome de arquivo disponível
                do
                {
                    sufixo = "_" + "tela" + contador.ToString("D2"); // Formata o contador como "00", "01", etc.
                    caminhoCompleto = Path.Combine(caminhoPasta, nomeBaseArquivo + sufixo + ".bmp");
                    contador++;
                }
                while (File.Exists(caminhoCompleto)); // Continua incrementando até encontrar um nome disponível

                // Criar um bitmap com o tamanho do formulário atual
                Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

                // Desenhar o conteúdo do formulário no bitmap
                this.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                // Salvar o bitmap como arquivo BMP no local especificado
                bmp.Save(caminhoCompleto, System.Drawing.Imaging.ImageFormat.Bmp);

                // Liberar os recursos do bitmap
                bmp.Dispose();

                // Exibir uma mensagem de confirmação
                MessageBox.Show("Print da tela salvo com sucesso em " + caminhoCompleto, "Sucesso", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Exibir uma mensagem de erro caso ocorra algum problema
                MessageBox.Show("Erro ao salvar o print da tela: " + ex.Message, "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
            }
        }
        private void ImprimeTela_Click(object sender, EventArgs e)
        {
            // Abrir o diálogo de salvamento
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF File (*.pdf)|*.pdf|Bitmap Image (*.bmp)|*.bmp";
                saveFileDialog.Title = "Salvar como";
                saveFileDialog.FileName = "Arquivo";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Capturar a tela em um Bitmap
                    Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                    this.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    // Verificar a extensão do arquivo selecionado
                    string fileExtension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (fileExtension == ".pdf")
                    {
                        // Salvar como PDF
                        SalvarComoPDF(saveFileDialog.FileName, bmp);
                    }
                    else if (fileExtension == ".bmp")
                    {
                        // Salvar como BMP
                        bmp.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }

                    // Liberar os recursos do bitmap
                    bmp.Dispose();

                    MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                }
            }
        }
        public string getTitulo()
        {
            string text = "";
            var ini = new IniFile(@"C:\Temp\Config.ini");

            string retLen = ini.Read("LAUDO", "CABECALHO_PAGINA_TRACADO", "__NOT_FOUND__");

            if (retLen == "__NOT_FOUND__" || string.IsNullOrWhiteSpace(retLen))
            {
                // A chave não existe
                string nome = GlobVar.tbl_DadosExame.Rows[0]["Nome"].ToString();
                string sexo = $"({GlobVar.tbl_DadosExame.Rows[0]["Sexo"].ToString()})";
                string idade = $"{GlobVar.tbl_DadosExame.Rows[0]["IdadeAno"]} anos";
                string altura = $"{GlobVar.tbl_DadosExame.Rows[0]["Altura"].ToString()}m";
                string realizacao = GlobVar.tbl_DadosExame.Rows[0]["DataRealizacao"].ToString().Substring(0, 10);
                string arquivo = $"{Path.GetFileNameWithoutExtension(GlobVar.textFile)}";

                text = $"{nome} {sexo} {idade} - {altura} - Realizacao: {realizacao} - Arquivo: {arquivo}.DAT";
            }
            else
            {
                string texto = retLen;
                int pos1 = texto.IndexOf("&(");
                while (pos1 >= 0)
                {
                    int pos2 = texto.IndexOf(")&", pos1 + 2);
                    if (pos2 > pos1)
                    {
                        string campo = texto.Substring(pos1 + 2, pos2 - pos1 - 2).ToUpper();
                        string substituto = "";

                        DataRow dados = GlobVar.tbl_DadosExame.Rows[0];

                        switch (campo)
                        {
                            case "NOME":
                                substituto = dados["Nome"].ToString();
                                break;
                            case "DATA":
                                substituto = Convert.ToDateTime(dados["DataRealizacao"]).ToString("dd/MM/yyyy");
                                break;
                            case "ARQUIVO":
                                substituto = Path.GetFileNameWithoutExtension(GlobVar.textFile);
                                break;
                            case "SEXO":
                                substituto = dados["Sexo"].ToString();
                                break;
                            case "IDADE":
                                substituto = $"{dados["IdadeAno"]} anos";
                                break;
                            case "IDADE_AM":
                                substituto = dados["IdadeAM"].ToString();
                                break;
                            case "IDADE_AMD":
                                substituto = dados["IdadeAMD"].ToString();
                                break;
                            case "PESO":
                                substituto = $"{Convert.ToDouble(dados["Peso"]):0.0} Kg";
                                break;
                            case "ALTURA":
                                substituto = $"{Convert.ToDouble(dados["Altura"]):0.00} m";
                                break;
                            case "IMC":
                                double peso = Convert.ToDouble(dados["Peso"]);
                                double altura = Convert.ToDouble(dados["Altura"]);
                                if (altura > 0)
                                    substituto = $"{peso / (altura * altura):0.00}";
                                break;
                            case "EMAIL":
                                substituto = dados["EMail"].ToString();
                                break;
                            case "MEDICO_SOLIC":
                                substituto = dados["MedicoSolicitante"].ToString();
                                break;
                        }

                        // Substitui o campo
                        texto = texto.Substring(0, pos1) + substituto + texto.Substring(pos2 + 2);
                        pos1 = texto.IndexOf("&(", pos1 + substituto.Length); // continua após a substituição
                    }
                    else
                    {
                        break; // não encontrou fechamento
                    }
                }

                text = texto;
            }
            return text;
        }
        public static bool ImprimeLogo = false;
        private void SalvarComoPDF(string caminhoArquivo, Bitmap imagem)
        {
            var ini = new IniFile(@"C:\Temp\Config.ini");
            // Conversão de mm para pontos
            double CmParaPontos(double cm) => cm * 28.3465;
            double mmParaPontos(double mm) => mm * 2.83465;

            string logo = "";
            if (!ImprimeLogo)
            {
                string impLogo = ini.Read("LAUDO", "IMP_LOGO_ICELERA", "NÃO");
                ImprimeLogo = impLogo.Trim().ToUpper() == "SIM";

                string pathCliente = Path.Combine("C:\\Temp\\Icones", "LogoCliente.bmp");
                string pathPadrao = Path.Combine("C:\\Temp\\Icones", "iCelera.bmp");

                if (File.Exists(pathCliente))
                {
                    logo = pathCliente;
                }
                else if (File.Exists(pathPadrao))
                {
                    logo = pathPadrao;
                }
                else
                {
                    System.Windows.Forms.DialogResult resultado = System.Windows.Forms.MessageBox.Show(
                        "Arquivo de logo não encontrado.\nDeseja continuar sem o logo?",
                        "Logo não encontrado",
                        System.Windows.Forms.MessageBoxButtons.OKCancel,
                        System.Windows.Forms.MessageBoxIcon.Warning
                    );

                    if (resultado == DialogResult.Cancel)
                    {
                        return; // cancela o processo
                    }

                    ImprimeLogo = false; // continua sem logo
                }
            }            // Criar um documento PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Relatório";

            TempoTimerAndar.Visible = false;
            // Criar uma página no documento com orientação Paisagem
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Títulos com dados do exame
            string tituloPrincipal = getTitulo();
            string medicoSolicitante = GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"] != DBNull.Value || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"] != null || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"].Equals("") ?
                                       GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"].ToString() : "Informe o nome do médico";
            string modeloEquipamento = GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"] != DBNull.Value || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"] != null || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"].Equals("") ?
                                       GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"].ToString() : "Informe o nome da clínica";
            Icon icon = this.Icon; // ou qualquer outra instância de Icon

            double xLoc = mmParaPontos(int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_ESQUERDA")));
            double yLoc = mmParaPontos(int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_SUPERIOR")));
            double espacoEntreLinhas = mmParaPontos(2); // 2mm de espaçamento extra

            void DesenhaTextoAjustado(string texto, XFont fonte)
            {
                // Desenha o texto
                gfx.DrawString(texto, fonte, XBrushes.Black, new XPoint(xLoc, yLoc));

                // Atualiza yLoc com base na altura da fonte + espaçamento extra
                XSize tamanhoTexto = gfx.MeasureString(texto, fonte);
                yLoc += tamanhoTexto.Height + espacoEntreLinhas;
            }

            // Uso:
            DesenhaTextoAjustado(tituloPrincipal, new XFont("Arial", 14));
            DesenhaTextoAjustado(medicoSolicitante, new XFont("Arial", 10));
            DesenhaTextoAjustado(modeloEquipamento, new XFont("Arial", 10));

            // Converter a imagem Bitmap para XImage
            using (MemoryStream stream = new MemoryStream())
            {
                imagem.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                XImage xImage = XImage.FromStream(stream);

                // Ajustar o tamanho da imagem para caber na página, mantendo a proporção
                // Ajustar o tamanho da imagem para caber na página, mantendo a proporção
                double scaleFactor = Math.Min((page.Width - 20) / xImage.PixelWidth, (page.Height - 60) / xImage.PixelHeight);
                double width = xImage.PixelWidth * scaleFactor;
                double height = page.Height - 90 - 30;//  xImage.PixelHeight * scaleFactor;

                // Desenhar a imagem na página do PDF centralizada
                double posX = (page.Width - width) / 2;
                double posY = 95; // margem superior de 140 unidades
                                   // Desenhar a imagem combinada no PDF
                gfx.DrawImage(xImage, posX, posY, width, height);

                gfx.DrawRectangle(new XPen(XColors.Black, 1), posX, posY, width, height - 49);
            }
            if (ImprimeLogo)
            {
                using (Bitmap bmp = new Bitmap(logo))
                using (MemoryStream ms = new MemoryStream())
                {

                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // ou .Bmp
                    ms.Seek(0, SeekOrigin.Begin);

                    XImage log = XImage.FromStream(ms);

                    // Converte centímetros para pontos
                    double width = CmParaPontos(2.0);    // 2 cm de largura
                    double height = CmParaPontos(1.6);   // 1,6 cm de altura

                    // Lê os valores do .ini em milímetros
                    int margemDireitaMM = int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_DIREITA"));
                    int margemSuperiorMM = int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_SUPERIOR"));

                    // Define posição convertendo cm para pontos também
                    double posX = page.Width - mmParaPontos(20) - mmParaPontos(margemDireitaMM); // 2cm = 20mm
                    double posY = mmParaPontos(margemSuperiorMM);

                    // Desenha a imagem
                    gfx.DrawImage(log, posX, posY, width, height);
                }
            }

            TempoTimerAndar.Visible = true;

            // Salvar o documento PDF
            document.Save(caminhoArquivo);
            document.Close();
        }
        private void btnImprimeTudo_Click(object sender, EventArgs e)
        {
            // Abrir o diálogo de salvamento
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF File (*.pdf)|*.pdf|Bitmap Image (*.bmp)|*.bmp";
                saveFileDialog.Title = "Salvar como";
                saveFileDialog.FileName = "RelatorioCompleto";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Capturar a área específica (painelExames e tela OpenGL)
                    Bitmap bmp = new Bitmap(painelExames.Width, painelExames.Height);
                    painelExames.DrawToBitmap(bmp, new Rectangle(0, 0, painelExames.Width, painelExames.Height));
                    // Caso precise capturar outra área (por exemplo, tela OpenGL), faça uma operação similar

                    // Verificar a extensão do arquivo selecionado
                    string fileExtension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (fileExtension == ".pdf")
                    {
                        // Salvar como PDF
                        ImprimeTudo_Click(saveFileDialog.FileName);
                    }
                    else if (fileExtension == ".bmp")
                    {
                        // Salvar como BMP
                        bmp.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }

                    // Liberar os recursos do bitmap
                    bmp.Dispose();

                    MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                }
                ImprimeLogo = false; // continua sem logo
            }
        }
        private void ImprimeTudo_Click(string caminhoArquivo)
        {
            var ini = new IniFile(@"C:\Temp\Config.ini");
            // Conversão de mm para pontos
            double CmParaPontos(double cm) => cm * 28.3465;
            double mmParaPontos(double mm) => mm * 2.83465;

            string logo = "";
            if (!ImprimeLogo)
            {
                string impLogo = ini.Read("LAUDO", "IMP_LOGO_ICELERA", "NÃO");
                ImprimeLogo = impLogo.Trim().ToUpper() == "SIM";

                string pathCliente = Path.Combine("C:\\Temp\\Icones", "LogoCliente.bmp");
                string pathPadrao = Path.Combine("C:\\Temp\\Icones", "iCelera.bmp");

                if (File.Exists(pathCliente))
                {
                    ImprimeLogo = true;
                    logo = pathCliente;
                }
                else if (File.Exists(pathPadrao))
                {
                    logo = pathPadrao;
                    ImprimeLogo = true;
                }
                else
                {
                    System.Windows.Forms.DialogResult resultado = System.Windows.Forms.MessageBox.Show(
                        "Arquivo de logo não encontrado.\nDeseja continuar sem o logo?",
                        "Logo não encontrado",
                        System.Windows.Forms.MessageBoxButtons.OKCancel,
                        System.Windows.Forms.MessageBoxIcon.Warning
                    );

                    if (resultado == DialogResult.Cancel)
                    {
                        return; // cancela o processo
                    }

                    ImprimeLogo = false; // continua sem logo
                }
            }            // Criar um documento PDF
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Relatório Completo";

            // Criar uma página no documento com orientação Paisagem
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Títulos com dados do exame
            string tituloPrincipal = getTitulo();
            string medicoSolicitante = GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"] != DBNull.Value || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"] != null || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"].Equals("") ?
                                       GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"].ToString() : "Informe o nome do médico";
            string modeloEquipamento = GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"] != DBNull.Value || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"] != null || GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"].Equals("") ?
                                       GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"].ToString() : "Informe o nome da clínica";


            // Descrição com as informações fornecidas
            string descricao = $"Epoca: {ptsEmTela.Text} - Horario: {inicioTela.Text} - Estagio: {estagioatutxt} ({GlobVar.segundos} seg)";

            double xLoc = mmParaPontos(int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_ESQUERDA")));
            double yLoc = mmParaPontos(int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_SUPERIOR")));
            double espacoEntreLinhas = mmParaPontos(2); // 2mm de espaçamento extra

            void DesenhaTextoAjustado(string texto, XFont fonte)
            {
                // Desenha o texto
                gfx.DrawString(texto, fonte, XBrushes.Black, new XPoint(xLoc, yLoc));

                // Atualiza yLoc com base na altura da fonte + espaçamento extra
                XSize tamanhoTexto = gfx.MeasureString(texto, fonte);
                yLoc += tamanhoTexto.Height + espacoEntreLinhas;
            }

            // Uso:
            DesenhaTextoAjustado(tituloPrincipal, new XFont("Arial", 14));
            DesenhaTextoAjustado(medicoSolicitante, new XFont("Arial", 10));
            DesenhaTextoAjustado(modeloEquipamento, new XFont("Arial", 10));


            if (MostarAmplitudes.Checked)
            {
                MostarAmplitudes.Checked = false;
                MostarAmplitudes_Click(MostarAmplitudes, EventArgs.Empty);
            }

            // Capturar a imagem do painelExames
            Bitmap bitmapPainel = new Bitmap(painelExames.Width, (int)(painelExames.Height * 1.12f));
            painelExames.DrawToBitmap(bitmapPainel, new Rectangle(0, 0, painelExames.Width, (int)(painelExames.Height * 1.12f)));

            // Capturar a imagem da tela OpenGL
            Bitmap bitmapOpenGL = new Bitmap(openglControl1.Width, openglControl1.Height);
            openglControl1.DrawToBitmap(bitmapOpenGL, new Rectangle(0, 0, openglControl1.Width, openglControl1.Height));

            // Combinar as duas imagens lado a lado
            int totalWidth = bitmapPainel.Width + bitmapOpenGL.Width;
            int maxHeight = Math.Max(bitmapPainel.Height, bitmapOpenGL.Height);
            Bitmap bitmapCombinado = new Bitmap(totalWidth, maxHeight);
            using (Graphics g = Graphics.FromImage(bitmapCombinado))
            {
                g.DrawImage(bitmapPainel, 0, 0);
                g.DrawImage(bitmapOpenGL, bitmapPainel.Width + 2, 0);
            }

            // Converter a imagem combinada para XImage
            using (MemoryStream stream = new MemoryStream())
            {
                bitmapCombinado.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                XImage xImage = XImage.FromStream(stream);

                // Ajustar o tamanho da imagem para caber na página, mantendo a proporção
                double scaleFactor = Math.Min((page.Width - 20) / xImage.PixelWidth, (page.Height - 60) / xImage.PixelHeight);
                double width = xImage.PixelWidth * scaleFactor;
                double height = page.Height - 90 - 30;//  xImage.PixelHeight * scaleFactor;

                // Desenhar a imagem combinada no PDF
                gfx.DrawImage(xImage, (page.Width - width) / 2, 90, width, height); // A posição Y ajustada para ficar abaixo do texto

                // Desenhar a descrição acima da imagem no PDF
                // Atualiza yLoc com base na altura da fonte + espaçamento extra
                XSize tamanhoTexto = gfx.MeasureString(descricao, new XFont("Arial", 8));
                gfx.DrawString(descricao, new XFont("Arial", 8), XBrushes.Black, new XPoint(xLoc, (90 - tamanhoTexto.Height - 1)));

                // Desenhar uma borda ao redor da imagem
                XPen pen = new XPen(XColors.Black, 1); // cor e espessura da borda
                gfx.DrawRectangle(pen, (page.Width - width) / 2, 90, width, height - 49);
            }
            Icon icon = this.Icon; // ou qualquer outra instância de Icon

            if (ImprimeLogo)
            {
                using (Bitmap bmp = new Bitmap(logo))
                using (MemoryStream ms = new MemoryStream())
                {

                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // ou .Bmp
                    ms.Seek(0, SeekOrigin.Begin);

                    XImage log = XImage.FromStream(ms);

                    // Converte centímetros para pontos
                    double width = CmParaPontos(2.0);    // 2 cm de largura
                    double height = CmParaPontos(1.6);   // 1,6 cm de altura

                    // Lê os valores do .ini em milímetros
                    int margemDireitaMM = int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_DIREITA"));
                    int margemSuperiorMM = int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_SUPERIOR"));

                    // Define posição convertendo cm para pontos também
                    double posX = page.Width - mmParaPontos(20) - mmParaPontos(margemDireitaMM); // 2cm = 20mm
                    double posY = mmParaPontos(margemSuperiorMM);

                    // Desenha a imagem
                    gfx.DrawImage(log, posX, posY, width, height);

                }
            }
            // Salvar o documento PDF
            document.Save(caminhoArquivo);
            document.Close();
        }
        private void ImprimeSele_Click(object sender, EventArgs e)
        {
            PagSelecionadasImpressao pg = new PagSelecionadasImpressao(this);

            pg.ShowDialog();
        }
        private void ImprimePagina_Click(object sender, EventArgs e)
        {
            // Clonar a estrutura de tbl_SellImpressao para manter a mesma ordem de colunas
            DataTable telaSelect = GlobVar.tbl_SelImpressao.Clone();
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();

            int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]) / 30;

            var lastNum = GlobVar.tbl_SeqEvento.AsEnumerable().Last();
            int CodImpressao = lastNum == null ? 1 : Convert.ToInt32(lastNum["ProxPagImp"]) + 1;

            int PagInicial = paginaCoerente;
            int QtdSeg = GlobVar.segundos;
            // ajustar dps caso seja EEG
            int AmplGeral = -1;
            int PassaBaixaGeral = -1;
            int PassaAltaGeral = -1;
            int NotchGeral = -1;
            bool DadosPagina = false;
            bool Pontilhado1seg = Tela_Plotagem.Linha1Seg.Checked;
            int MaiorQtdAmostras = GlobVar.namos;
            int DadosPagina_MostrarACada = 0;
            int DadosPagina_OQueMostrar = 0;
            string Pag_TotPag = $"{(paginaCoerente / 30) + 1}/{maximoPossivel}";
            int LimpaSinal = 7;
            int Pos_Video = -1;
            int? Arq_Video = null;
            bool PontilhadoAmplitude = Tela_Plotagem.LinhaZeroCanais.Checked;
            bool MostrarAmpl = MostarAmplitudes.Checked;
            bool Pont200MiliSeg = Tela_Plotagem.Pontilhado200Mili.Checked;
            int Ref = 1;

            // Preencher o telaSelect com os dados correspondentes da tbl_MontagemSelecionada
            foreach (DataRow linhaMontagem in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                DataRow novaLinha = telaSelect.NewRow();

                // Copiar os valores das colunas que existem em ambas as tabelas
                foreach (DataColumn coluna in GlobVar.tbl_MontagemSelecionada.Columns)
                {
                    if (telaSelect.Columns.Contains(coluna.ColumnName))
                    {
                        novaLinha[coluna.ColumnName] = linhaMontagem[coluna.ColumnName] == DBNull.Value ? -1 : linhaMontagem[coluna.ColumnName];
                    }
                }

                // Adicionar a nova linha ao DataTable
                telaSelect.Rows.Add(novaLinha);
            }
            // Preencher o telaSelect com os dados correspondentes da tbl_MontagemSelecionada
            for (int i = 0; i < telaSelect.Rows.Count; i++)
            {
                DataRow linhaMontagem = telaSelect.Rows[i];

                // Atribuir os valores às respectivas colunas
                linhaMontagem["CodImpressao"] = CodImpressao;
                linhaMontagem["PagInicial"] = PagInicial;
                linhaMontagem["QtdSeg"] = QtdSeg;
                linhaMontagem["AmplGeral"] = AmplGeral;
                linhaMontagem["PassaBaixaGeral"] = PassaBaixaGeral;
                linhaMontagem["PassaAltaGeral"] = PassaAltaGeral;
                linhaMontagem["NotchGeral"] = NotchGeral;
                linhaMontagem["DadosPagina"] = DadosPagina;
                linhaMontagem["Pontilhado1seg"] = Pontilhado1seg;
                linhaMontagem["MaiorQtdAmostra"] = MaiorQtdAmostras;
                linhaMontagem["DadosPagina_MostrarACada"] = DadosPagina_MostrarACada;
                linhaMontagem["DadosPagina_OQueMostrar"] = DadosPagina_OQueMostrar;
                linhaMontagem["Pag_TotPag"] = Pag_TotPag;
                linhaMontagem["LimpaSinal"] = LimpaSinal;
                linhaMontagem["Pos_Video"] = Pos_Video;
                linhaMontagem["Arq_Video"] = DBNull.Value;
                linhaMontagem["PontilhadoAmplitude"] = PontilhadoAmplitude;
                linhaMontagem["MostrarAmpl"] = MostrarAmpl;
                linhaMontagem["Pont200MiliSeg"] = Pont200MiliSeg;
                linhaMontagem["Ref"] = Ref;
                linhaMontagem["NomeMontagem"] = GlobVar.tbl_MontGrav.Rows[0]["NomeMontagem"].ToString();

                // Adicionar a nova linha ao DataTable
            }

            // Adicionar as linhas do telaSelect ao GlobVar.tbl_SelImpressao
            foreach (DataRow row in telaSelect.Rows)
            {
                GlobVar.tbl_SelImpressao.ImportRow(row);
            }
            AlteraBD.AdicionarLinhasNoBancoDeDados(telaSelect, CodImpressao);

            GlobVar.tbl_SeqEvento.Rows[0]["ProxPagImp"] = CodImpressao;

            int a = 1;
            // Agora o telaSelect possui a estrutura de tbl_SellImpressao e os dados de tbl_MontagemSelecionada

            CustomMessageBox.ShowTimed("Pagina selecionada para impressão posterior", 2);


        }
        public static void retornaOsValoresDosOutrosEstagios()
        {
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var rowIndex = GlobVar.tbl_Paginas.AsEnumerable().ToList().FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);
            var indexProximo = rowIndex;
            for (int i = 1; i <= 8; i++)
            {
                if (indexProximo < GlobVar.tbl_Paginas.Rows.Count) // Verifica se o índice é válido
                {
                    var rowProxima = GlobVar.tbl_Paginas.Rows[indexProximo];
                    int estagioProximo = Convert.ToInt32(rowProxima["Estagio"]);
                    string est = "";
                    if(estagioProximo == 5)
                    {
                        est = "R";
                    }
                    else if(estagioProximo == 4) { est = "N"; }
                    else if(estagioProximo == 6) { est = "T"; }
                    else
                    {
                        est = $"{estagioProximo}";
                    }
                    switch (i)
                    {
                        case 1:
                            estagioatutxt = $"{est}";
                            break;
                        case 2:
                            estagioatutxt1 = $"{est}";
                            break;
                        case 3:
                            estagioatutxt2 = $"{est}";
                            break;
                        case 4:
                            estagioatutxt3 = $"{est}";
                            break;
                        case 5:
                            estagioatutxt4 = $"{est}";
                            break;
                        case 6:
                            estagioatutxt5 = $"{est}";
                            break;
                        case 7:
                            estagioatutxt6 = $"{est}";
                            break;
                        case 8:
                            estagioatutxt7 = $"{est}";
                            break;
                    }
                    estagios = new string[]
                    {
                            estagioatutxt,
                            estagioatutxt1,
                            estagioatutxt2,
                            estagioatutxt3,
                            estagioatutxt4,
                            estagioatutxt5,
                            estagioatutxt6,
                            estagioatutxt7
                    };

                }
                else
                {
                    switch (i)
                    {
                        case 1:
                            estagioatutxt = "";
                            break;
                        case 2:
                            estagioatutxt1 = "";
                            break;
                        case 3:
                            estagioatutxt2 = "";
                            break;
                        case 4:
                            estagioatutxt3 = "";
                            break;
                        case 5:
                            estagioatutxt4 = "";
                            break;
                        case 6:
                            estagioatutxt5 = "";
                            break;
                        case 7:
                            estagioatutxt6 = "";
                            break;
                        case 8:
                            estagioatutxt7 = "";
                            break;
                    }
                }
                indexProximo = rowIndex + 30 * i;
            }
        }
        public static void atualizaButAntProx()
        {
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var rowIndex = GlobVar.tbl_Paginas.AsEnumerable().ToList().FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);

            // Função auxiliar para definir a imagem correta com base no valor do "Estagio"
            string GetImageByEstagio(int? estagio)
            {
                switch (estagio)
                {
                    case 0: GlobVar.estagioAtual = estagio.ToString(); return GlobVar.diretorioEstagioAnteriorProximo0; 
                    case 1: GlobVar.estagioAtual = estagio.ToString(); return GlobVar.diretorioEstagioAnteriorProximo1; 
                    case 2: GlobVar.estagioAtual = estagio.ToString(); return GlobVar.diretorioEstagioAnteriorProximo2; 
                    case 3: GlobVar.estagioAtual = estagio.ToString(); return GlobVar.diretorioEstagioAnteriorProximo3; 
                    case 5: GlobVar.estagioAtual = "R"; return GlobVar.diretorioEstagioAnteriorProximoR;
                    case 4: GlobVar.estagioAtual = "N"; return GlobVar.diretorioEstagioAnteriorProximoN;
                    case 6: GlobVar.estagioAtual = "T"; return GlobVar.diretorioEstagioAnteriorProximoT;
                    default: return GlobVar.diretorioEstagioAnteriorProximoNada; // Caso o valor seja nulo ou não mapeado, usar a imagem "Nada"
                }
            }

            // Atualizar imagens dos botões "Anteriores"
            for (int i = 1; i <= 4; i++)
            {
                var indexAnterior = rowIndex - 30 * i;
                if (indexAnterior >= 0) // Verifica se o índice é válido
                {
                    var rowAnterior = GlobVar.tbl_Paginas.Rows[indexAnterior];
                    int estagioAnterior = Convert.ToInt32(rowAnterior["Estagio"]);
                    string imagePathAnterior = GetImageByEstagio(estagioAnterior);

                    // Definir imagem nos botões anteriores
                    switch (i)
                    {
                        case 1: UmaAnterior.BackgroundImage = Image.FromFile(imagePathAnterior); break;
                        case 2: DuasAnterior.BackgroundImage = Image.FromFile(imagePathAnterior); break;
                        case 3: TresAnterior.BackgroundImage = Image.FromFile(imagePathAnterior); break;
                        case 4: QuatroAnterior.BackgroundImage = Image.FromFile(imagePathAnterior); break;
                    }
                }
                else
                {
                    // Se o índice for inválido (fora do range), usa a imagem "Nada"
                    switch (i)
                    {
                        case 1: UmaAnterior.BackgroundImage = null; break;
                        case 2: DuasAnterior.BackgroundImage = null; break;
                        case 3: TresAnterior.BackgroundImage = null; break;
                        case 4: QuatroAnterior.BackgroundImage = null; break;
                    }
                }
            }

            // Atualizar imagens dos botões "Próximos"
            for (int i = 1; i <= 4; i++)
            {
                var indexProximo = rowIndex + 30 * i;
                if (indexProximo < GlobVar.tbl_Paginas.Rows.Count) // Verifica se o índice é válido
                {
                    var rowProxima = GlobVar.tbl_Paginas.Rows[indexProximo];
                    int estagioProximo = Convert.ToInt32(rowProxima["Estagio"]);
                    string imagePathProxima = GetImageByEstagio(estagioProximo);

                    // Definir imagem nos botões próximos
                    switch (i)
                    {
                        case 1: UmaProxima.BackgroundImage = Image.FromFile(imagePathProxima); break;
                        case 2: DuasProxima.BackgroundImage = Image.FromFile(imagePathProxima); break;
                        case 3: TresProxima.BackgroundImage = Image.FromFile(imagePathProxima); break;
                        case 4: QuatroProxima.BackgroundImage = Image.FromFile(imagePathProxima); break;
                    }
                }
                else
                {
                    // Se o índice for inválido (fora do range), usa a imagem "Nada"
                    switch (i)
                    {
                        case 1: UmaProxima.BackgroundImage = null; break;
                        case 2: DuasProxima.BackgroundImage = null; break;
                        case 3: TresProxima.BackgroundImage = null; break;
                        case 4: QuatroProxima.BackgroundImage = null; break;
                    }
                }
            }
        }
        private void Marcar_Click(object sender, EventArgs e)
        {
            if (GlobVar.maximaVect > GlobVar.matrizCanal.GetLength(1))
                return;

            Button botao = sender as Button;
            if (botao == null)
                return;

            int newEstagio = Convert.ToInt32(botao.Tag);
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            int rowIndex = FindRowIndex(paginaCoerente);

            if (rowIndex >= 0)
            {
                int totalRows = GlobVar.tbl_Paginas.Rows.Count;
                int limite = Math.Min(rowIndex + 30, totalRows);

                List<Tuple<int, int>> updates = PrepareUpdates(rowIndex, limite, newEstagio);

                ExecuteDatabaseUpdate(updates);
            }

            UpdateGlobVarStates();
            UpdateInterface();
        }
        /// <summary>
        /// Encontra o índice da linha no DataTable com base no número da página coerente.
        /// </summary>
        private int FindRowIndex(int paginaCoerente)
        {
            return GlobVar.tbl_Paginas.AsEnumerable()
                .ToList()
                .FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);
        }

        /// <summary>
        /// Prepara as atualizações a serem feitas no DataTable e no banco de dados.
        /// </summary>
        private List<Tuple<int, int>> PrepareUpdates(int start, int limit, int newEstagio)
        {
            List<Tuple<int, int>> updates = new List<Tuple<int, int>>();

            for (int i = start; i < limit; i++)
            {
                GlobVar.tbl_Paginas.Rows[i]["Estagio"] = newEstagio;
                int numPag = GlobVar.tbl_Paginas.Rows[i].Field<int>("NumPag");
                updates.Add(new Tuple<int, int>(numPag, newEstagio));
            }

            return updates;
        }

        /// <summary>
        /// Executa as atualizações no banco de dados.
        /// </summary>
        private void ExecuteDatabaseUpdate(List<Tuple<int, int>> updates)
        {
            BD.AlteraBD.AlteraEstagioDaEpoca(updates);
        }

        /// <summary>
        /// Atualiza os estados das variáveis globais.
        /// </summary>
        private void UpdateGlobVarStates()
        {
            GlobVar.maximaVect += (int)GlobVar.saltoTelas;
            GlobVar.indice += (int)GlobVar.saltoTelas;
            GlobVar.ponteiroVideo = GlobVar.indice + Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);
            GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico;
            GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico;
            GlobVar.inicioTela += ((int)GlobVar.saltoTelas / GlobVar.namos);
            GlobVar.finalTela += ((int)GlobVar.saltoTelas / GlobVar.namos);
        }

        /// <summary>
        /// Atualiza a interface do usuário.
        /// </summary>
        private void UpdateInterface()
        {
            camera.X += GlobVar.saltoTelas;
            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
            UpdateInicioTela();
            TelaClearAndReload();
        }
        private void Proximo_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int proximoEstagio = (int)btn.Tag;

            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(row => row.Field<int>("NumPag") == paginaCoerente);
            var rowIndex = GlobVar.tbl_Paginas.AsEnumerable().ToList().FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);
            int estagioAtual = Convert.ToInt32(row["Estagio"]);
            int startNovoEstagio = estagioAtual;

            int indexDifEstagio = 0;

            for (int i = rowIndex; i < GlobVar.tbl_Paginas.Rows.Count; i += 30)
            {
                var rowsfuturas = GlobVar.tbl_Paginas.Rows[i];

                if (Convert.ToInt32(rowsfuturas["Estagio"]) != estagioAtual)
                {
                    indexDifEstagio = GlobVar.tbl_Paginas.Rows.IndexOf(rowsfuturas);
                    break;
                }
            }

            for (int i = indexDifEstagio; i < GlobVar.tbl_Paginas.Rows.Count; i += 30)
            {
                var rowsfuturas = GlobVar.tbl_Paginas.Rows[i];

                if (Convert.ToInt32(rowsfuturas["Estagio"]) == proximoEstagio)
                {
                    startNovoEstagio = Convert.ToInt32(rowsfuturas["NumPag"]);
                    break;
                }
            }
            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {

                int NovaLoc = GlobVar.namos * startNovoEstagio;
                int NovaLocNumerico = GlobVar.numeroAmos * startNovoEstagio;

                camera.X = NovaLoc;

                camera.X = NovaLoc;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.indice = NovaLoc;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.indiceNumero = NovaLocNumerico;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

                UpdateInicioTela();
                TelaClearAndReload();
            }

        }
        private void ProximoDif_Click(object sender, EventArgs e)
        {

            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(row => row.Field<int>("NumPag") == paginaCoerente);
            var rowIndex = GlobVar.tbl_Paginas.AsEnumerable().ToList().FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);
            int estagioAtual = Convert.ToInt32(row["Estagio"]);
            int startNovoEstagio = estagioAtual;

            for (int i = rowIndex; i < GlobVar.tbl_Paginas.Rows.Count; i += 30)
            {
                var rowsfuturas = GlobVar.tbl_Paginas.Rows[i];

                if (Convert.ToInt32(rowsfuturas["Estagio"]) != estagioAtual)
                {
                    startNovoEstagio = Convert.ToInt32(rowsfuturas["NumPag"]);
                    break;
                }
            }
            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {

                int NovaLoc = GlobVar.namos * startNovoEstagio;
                int NovaLocNumerico = GlobVar.numeroAmos * startNovoEstagio;

                camera.X = NovaLoc;

                camera.X = NovaLoc;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.indice = NovaLoc;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.indiceNumero = NovaLocNumerico;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

                UpdateInicioTela();
                TelaClearAndReload();
            }
        }
        private void Anterior_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int proximoEstagio = (int)btn.Tag;

            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(row => row.Field<int>("NumPag") == paginaCoerente);
            var rowIndex = GlobVar.tbl_Paginas.AsEnumerable().ToList().FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);
            int estagioAtual = Convert.ToInt32(row["Estagio"]);
            int startNovoEstagio = estagioAtual;

            int indexDifEstagio = 0;

            // Procurar pelo estágio diferente no sentido inverso
            for (int i = rowIndex; i >= 0; i -= 30)
            {
                var rowsfuturas = GlobVar.tbl_Paginas.Rows[i];

                if (Convert.ToInt32(rowsfuturas["Estagio"]) != estagioAtual)
                {
                    indexDifEstagio = GlobVar.tbl_Paginas.Rows.IndexOf(rowsfuturas);
                    break;
                }
            }

            // Procurar pelo estágio anterior no sentido inverso
            for (int i = indexDifEstagio; i >= 0; i -= 30)
            {
                var rowsfuturas = GlobVar.tbl_Paginas.Rows[i];

                if (Convert.ToInt32(rowsfuturas["Estagio"]) == proximoEstagio)
                {
                    startNovoEstagio = Convert.ToInt32(rowsfuturas["NumPag"]);
                    break;
                }
            }
            if (startNovoEstagio == 0 || startNovoEstagio == 1) return;
            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {
                int NovaLoc = GlobVar.namos * startNovoEstagio;
                int NovaLocNumerico = GlobVar.numeroAmos * startNovoEstagio;

                camera.X = NovaLoc;

                camera.X = NovaLoc;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.indice = NovaLoc;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.indiceNumero = NovaLocNumerico;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

                UpdateInicioTela();
                TelaClearAndReload();
            }
        }
        private void AnteriorDif_Click(object sender, EventArgs e)
        {
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(row => row.Field<int>("NumPag") == paginaCoerente);
            var rowIndex = GlobVar.tbl_Paginas.AsEnumerable().ToList().FindIndex(row => row.Field<int>("NumPag") == paginaCoerente);
            int estagioAtual = Convert.ToInt32(row["Estagio"]);
            int startNovoEstagio = estagioAtual;

            // Procurar pelo estágio diferente no sentido inverso
            for (int i = rowIndex; i >= 0; i -= 30)
            {
                var rowsfuturas = GlobVar.tbl_Paginas.Rows[i];

                if (Convert.ToInt32(rowsfuturas["Estagio"]) != estagioAtual)
                {
                    startNovoEstagio = Convert.ToInt32(rowsfuturas["NumPag"]);
                    break;
                }
            }

            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {
                int NovaLoc = GlobVar.namos * startNovoEstagio;
                int NovaLocNumerico = GlobVar.numeroAmos * startNovoEstagio;

                camera.X = NovaLoc;

                camera.X = NovaLoc;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.indice = NovaLoc;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.indiceNumero = NovaLocNumerico;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

                UpdateInicioTela();
                TelaClearAndReload();
            }
        }
        private void UmaProxima_Click(object sender, EventArgs e)
        {
            Button botao = sender as Button;
            int escolha = Convert.ToInt32(botao.Tag);

            int inicio = (GlobVar.indice / GlobVar.namos);
            TimeSpan tempo = TimeSpan.FromSeconds(inicio);
            string segundosI = tempo.Seconds.ToString().PadLeft(2, '0');

            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {
                camera.X += GlobVar.saltoTelas * escolha;

                GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)escolha;
                GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)escolha;

                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)escolha;
                GlobVar.indice += (int)GlobVar.saltoTelas * (int)escolha;

                GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)escolha) / GlobVar.namos;
                GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)escolha) / GlobVar.namos;
                UpdateInicioTela();
                TelaClearAndReload();
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;
            }
            if (Convert.ToInt32(segundosI) != 30 && Convert.ToInt32(segundosI) != 0)
            {
                if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                {

                    int vezesAndar = (Convert.ToInt32(segundosI) > 30) ? Math.Abs(30 - (Convert.ToInt32(segundosI) - 30)) : 30 - Convert.ToInt32(segundosI);

                    int AndarUmSegundo = GlobVar.namos * vezesAndar;
                    int AndaUmSegundoNumerico = GlobVar.numeroAmos * vezesAndar;

                    camera.X += AndarUmSegundo * escolha;

                    GlobVar.indiceNumero += (int)AndaUmSegundoNumerico;
                    GlobVar.maximaNumero += (int)AndaUmSegundoNumerico;
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.maximaVect += (int)AndarUmSegundo;
                    GlobVar.indice += (int)AndarUmSegundo;
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                    GlobVar.inicioTela += ((int)AndarUmSegundo) / GlobVar.namos;
                    GlobVar.finalTela += ((int)AndarUmSegundo) / GlobVar.namos;
                    UpdateInicioTela();
                    TelaClearAndReload();
                    foiencontradoumUltimo = false;
                    foiencontradoumUltimo = false;

                }
            }
        }
        private void UmaAnterior_Click(object sender, EventArgs e)
        {
            Button botao = sender as Button;
            int escolha = Convert.ToInt32(botao.Tag);

            int inicio = (GlobVar.indice / GlobVar.namos);
            TimeSpan tempo = TimeSpan.FromSeconds(inicio);
            string segundosI = tempo.Seconds.ToString().PadLeft(2, '0');

            if (GlobVar.indice > 0)
            {
                camera.X -= GlobVar.saltoTelas * escolha;

                GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)escolha;
                GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)escolha;
                if (GlobVar.indiceNumero < 0)
                {
                    GlobVar.indiceNumero = 0;
                    GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                }
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)escolha;
                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)escolha;

                if (GlobVar.indice < 0)
                {
                    GlobVar.indice = 0;
                    GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                    camera.X = 0;
                }
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)escolha) / GlobVar.namos;
                GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)escolha) / GlobVar.namos;
                if (GlobVar.inicioTela < 0)
                {
                    GlobVar.inicioTela = 0;
                    GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
                }
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

                TelaClearAndReload();
                UpdateInicioTela();
            }
            if (Convert.ToInt32(segundosI) != 30 && Convert.ToInt32(segundosI) != 0)
            {
                if (GlobVar.indice > 0)
                {
                    int vezesAndar = (Convert.ToInt32(segundosI) > 30) ? Math.Abs((Convert.ToInt32(segundosI) - 30)) : Convert.ToInt32(segundosI);

                    int VoltaUmSegundo = GlobVar.namos * vezesAndar;
                    int VoltaUmSegundoNumerico = GlobVar.numeroAmos * vezesAndar;

                    camera.X -= VoltaUmSegundo;

                    GlobVar.indiceNumero -= (int)VoltaUmSegundoNumerico ;
                    GlobVar.maximaNumero -= (int)VoltaUmSegundoNumerico;
                    if (GlobVar.indiceNumero < 0)
                    {
                        GlobVar.indiceNumero = 0;
                        GlobVar.maximaNumero = VoltaUmSegundoNumerico;
                    }
                    float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    GlobVar.maximaVect -= (int)VoltaUmSegundo;
                    GlobVar.indice -= (int)VoltaUmSegundo;

                    if (GlobVar.indice < 0)
                    {
                        GlobVar.indice = 0;
                        GlobVar.maximaVect = (int)VoltaUmSegundo;
                        camera.X = 0;
                    }
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont; 
                    hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                    UpdateInicioTela();
                    TelaClearAndReload();
                }

            }

        }
        // Quando o ComboBox abre, destacar o item selecionado
        private void TempoTimerAndar_DropDown(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null && comboBox.SelectedIndex >= 0)
            {
                // Destaca o item que está atualmente selecionado
                comboBox.SelectedItem = comboBox.Items[comboBox.SelectedIndex];
            }
        }
        private void TempoTimerAndar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica qual item foi selecionado no ComboBox
            switch (TempoTimerAndar.SelectedIndex)
            {
                case 0: // "0.00x"
                    timerAndarRetroceder = 0;  // Timer parado ou sem efeito
                    break;
                case 1: // "0.10x"
                    timerAndarRetroceder = 100;  // 100 ms
                    break;
                case 2: // "0.25x"
                    timerAndarRetroceder = 250;  // 250 ms
                    break;
                case 3: // "0.50x"
                    timerAndarRetroceder = 500;  // 500 ms
                    break;
                case 4: // "1.00x"
                    timerAndarRetroceder = 1000;  // 1000 ms (1 segundo)
                    break;
                case 5: // "2.00x"
                    timerAndarRetroceder = 2000;  // 2000 ms (2 segundos)
                    break;
                default:
                    timerAndarRetroceder = 0;  // Caso nenhum seja selecionado, por padrão, 0 ms
                    break;
            }
        }
        private void TimerRetrocede_Tick(object sender, EventArgs e)
        {
            if (GlobVar.indice > 0)
            {
                camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;

                GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                if (GlobVar.indiceNumero < 0)
                {
                    GlobVar.indiceNumero = 0;
                    GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                }
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                if (GlobVar.indice < 0)
                {
                    GlobVar.indice = 0;
                    GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                    camera.X = 0;
                }
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                if (GlobVar.inicioTela < 0)
                {
                    GlobVar.inicioTela = 0;
                    GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
                }
                UpdateInicioTela();
                TelaClearAndReload();
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

            }
        }
        private void TimerVoltaUmaPag_Tick(object sender, EventArgs e)
        {
            if (GlobVar.indice > 0)
            {
                int VoltaUmSegundo = GlobVar.namos * 1;
                int VoltaUmSegundoNumerico = GlobVar.numeroAmos * 1;

                camera.X -= VoltaUmSegundo * GlobVar.SPEED;

                GlobVar.indiceNumero -= (int)VoltaUmSegundoNumerico * (int)GlobVar.SPEED;
                GlobVar.maximaNumero -= (int)VoltaUmSegundoNumerico * (int)GlobVar.SPEED;
                if (GlobVar.indiceNumero < 0)
                {
                    GlobVar.indiceNumero = 0;
                    GlobVar.maximaNumero = VoltaUmSegundoNumerico;
                }
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.maximaVect -= (int)VoltaUmSegundo * (int)GlobVar.SPEED;
                GlobVar.indice -= (int)VoltaUmSegundo * (int)GlobVar.SPEED;

                if (GlobVar.indice < 0)
                {
                    GlobVar.indice = 0;
                    GlobVar.maximaVect = (int)VoltaUmSegundo;
                    camera.X = 0;
                }
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.inicioTela -= ((int)VoltaUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                GlobVar.finalTela -= ((int)VoltaUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                if (GlobVar.inicioTela < 0)
                {
                    GlobVar.inicioTela = 0;
                    GlobVar.finalTela = (int)VoltaUmSegundo / (int)GlobVar.namos;
                }
                UpdateInicioTela();
                TelaClearAndReload();
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;
            }

        }
        private void TimerAndaUmaPag_Tick(object sender, EventArgs e)
        {
            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {
                int AndarUmSegundo = GlobVar.namos * 1;
                int AndaUmSegundoNumerico = GlobVar.numeroAmos * 1;
                camera.X += AndarUmSegundo * GlobVar.SPEED;

                GlobVar.indiceNumero += AndaUmSegundoNumerico * (int)GlobVar.SPEED;
                GlobVar.maximaNumero += AndaUmSegundoNumerico * (int)GlobVar.SPEED;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.maximaVect += AndarUmSegundo * (int)GlobVar.SPEED;
                GlobVar.indice += AndarUmSegundo * (int)GlobVar.SPEED;
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.inicioTela += (AndarUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                GlobVar.finalTela += (AndarUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                UpdateInicioTela();
                TelaClearAndReload();
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;
            }
        }
        private void TimerAvanca_Tick(object sender, EventArgs e)
        {
            if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
            {
                camera.X += GlobVar.saltoTelas * GlobVar.SPEED;

                GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                UpdateInicioTela();
                TelaClearAndReload();
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

            }
        }
        private void DesmarcarBotaoEvents(Button btn)
        {
            // Restaurar a cor original do botão
            btn.BackColor = Color.LightCyan;  // Cor original
            btn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.Black;

            if ((int)btn.Tag == 1)
            {
                EventoUmClicked = false;
            }
            else
            {
                MinEventClicked = false;
            }
        }
        Button botaoSelecionadoEventss = null;
        bool EventoUmClicked = false;
        private void EventoUmClick_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // Desmarcar o botão anterior
            if (botaoSelecionadoEventss != null && botaoSelecionadoEventss != btn)
            {
                //DesmarcarBotaoEvents(botaoSelecionadoEventss);
            }

            botaoSelecionadoEventss = btn;

            if (btn != null)
            {
                if (btn.BackColor == Color.LightCyan)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(133, 219, 219);  // Cor para indicar seleção
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;
                    EventoUmClicked = true;
                }
                else
                {
                    DesmarcarBotaoEvents(botaoSelecionadoEventss);  // Desmarcar o botão anterior
                    EventoUmClicked = false;
                }
            }
        }
        bool MinEventClicked = false;
        private void MinimoEvento_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // Desmarcar o botão anterior
            if (botaoSelecionadoEventss != null && botaoSelecionadoEventss != btn)
            {
                //DesmarcarBotaoEvents(botaoSelecionadoEventss);
            }

            botaoSelecionadoEventss = btn;

            if (btn != null)
            {
                if (btn.BackColor == Color.LightCyan)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(133, 219, 219);  // Cor para indicar seleção
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;
                    MinEventClicked = true;
                }
                else
                {
                    DesmarcarBotaoEvents(botaoSelecionadoEventss);  // Desmarcar o botão anterior
                    MinEventClicked = false;
                }
            }
        }
        Button botaoSelecionado;
        int timerAndarRetroceder;
        private void Avanca_Click(object sender, EventArgs e)
        {
            if (botaoSelecionado != null)
            {
                DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
            }
            botaoSelecionado = sender as Button;

            timerRetrocede.Stop();
            timerVoltaUmaPag.Stop();
            timerAndaUmaPag.Stop();

            // Ao clicar, altera a cor de fundo para indicar que está selecionado
            Button btn = sender as Button;
            if (btn != null)
            {
                if(btn.BackColor == Color.OrangeRed)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(227, 68, 0);  // Cor para indicar seleção

                    // Alterar a cor do texto, se necessário
                    btn.ForeColor = Color.White;

                    // Alterar o estilo da borda, se necessário
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;
                    if(timerAndarRetroceder == 0)
                    {
                        if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                        {
                            camera.X += GlobVar.saltoTelas * GlobVar.SPEED;

                            GlobVar.indiceNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero += (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                            GlobVar.maximaVect += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.indice += (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                            GlobVar.inicioTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela += ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            UpdateInicioTela();
                            TelaClearAndReload();
                            foiencontradoumUltimo = false;
                            foiencontradoumUltimo = false;
                        }
                    }
                    else
                    {
                        timerAvanca.Interval = timerAndarRetroceder;
                        timerAvanca.Start();
                    }
                }
                else
                {
                    DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
                    timerAvanca.Stop();
                }
            }
        }
        private void Retrocede_Click(object sender, EventArgs e)
        {
            if (botaoSelecionado != null)
            {
                DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
            }
            botaoSelecionado = sender as Button;

            timerAvanca.Stop();
            timerVoltaUmaPag.Stop();
            timerAndaUmaPag.Stop();

            // Ao clicar, altera a cor de fundo para indicar que está selecionado
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.BackColor == Color.OrangeRed)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(227, 68, 0);  // Cor para indicar seleção

                    // Alterar a cor do texto, se necessário
                    btn.ForeColor = Color.White;

                    // Alterar o estilo da borda, se necessário
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;

                    if (timerAndarRetroceder == 0)
                    {
                        if (GlobVar.indice > 0)
                        {
                            camera.X -= GlobVar.saltoTelas * GlobVar.SPEED;

                            GlobVar.indiceNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero -= (int)GlobVar.tmpEmTelaNumerico * (int)GlobVar.SPEED;
                            if (GlobVar.indiceNumero < 0)
                            {
                                GlobVar.indiceNumero = 0;
                                GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;
                            }
                            float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                            GlobVar.maximaVect -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;
                            GlobVar.indice -= (int)GlobVar.saltoTelas * (int)GlobVar.SPEED;

                            if (GlobVar.indice < 0)
                            {
                                GlobVar.indice = 0;
                                GlobVar.maximaVect = (int)GlobVar.saltoTelas;
                                camera.X = 0;
                            }
                            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                            GlobVar.inicioTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela -= ((int)GlobVar.saltoTelas * (int)GlobVar.SPEED) / GlobVar.namos;
                            if (GlobVar.inicioTela < 0)
                            {
                                GlobVar.inicioTela = 0;
                                GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
                            }
                            UpdateInicioTela();
                            TelaClearAndReload();
                            foiencontradoumUltimo = false;
                            foiencontradoumUltimo = false;
                        }
                    }
                    else
                    {
                        timerRetrocede.Interval = timerAndarRetroceder;
                        timerRetrocede.Start();
                    }
                }
                else
                {
                    DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
                    timerRetrocede.Stop();
                }
            }
        }
        private void VoltaUmaPag_Click(object sender, EventArgs e)
        {
            if (botaoSelecionado != null)
            {
                DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
            }
            botaoSelecionado = sender as Button;

            timerAvanca.Stop();
            timerRetrocede.Stop();
            timerAndaUmaPag.Stop();

            // Ao clicar, altera a cor de fundo para indicar que está selecionado
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.BackColor == Color.OrangeRed)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(227, 68, 0);  // Cor para indicar seleção

                    // Alterar a cor do texto, se necessário
                    btn.ForeColor = Color.White;

                    // Alterar o estilo da borda, se necessário
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;
                    if (timerAndarRetroceder == 0)
                    {
                        if (GlobVar.indice > 0)
                        {
                            int VoltaUmSegundo = GlobVar.namos * 1;
                            int VoltaUmSegundoNumerico = GlobVar.numeroAmos * 1;

                            camera.X -= VoltaUmSegundo * GlobVar.SPEED;

                            GlobVar.indiceNumero -= (int)VoltaUmSegundoNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero -= (int)VoltaUmSegundoNumerico * (int)GlobVar.SPEED;
                            if (GlobVar.indiceNumero < 0)
                            {
                                GlobVar.indiceNumero = 0;
                                GlobVar.maximaNumero = VoltaUmSegundoNumerico;
                            }
                            float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                            GlobVar.maximaVect -= (int)VoltaUmSegundo * (int)GlobVar.SPEED;
                            GlobVar.indice -= (int)VoltaUmSegundo * (int)GlobVar.SPEED;

                            if (GlobVar.indice < 0)
                            {
                                GlobVar.indice = 0;
                                GlobVar.maximaVect = (int)VoltaUmSegundo;
                                camera.X = 0;
                            }
                            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                            GlobVar.inicioTela -= ((int)VoltaUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela -= ((int)VoltaUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                            if (GlobVar.inicioTela < 0)
                            {
                                GlobVar.inicioTela = 0;
                                GlobVar.finalTela = (int)VoltaUmSegundo / (int)GlobVar.namos;
                            }
                            UpdateInicioTela();
                            TelaClearAndReload();
                            foiencontradoumUltimo = false;
                            foiencontradoumUltimo = false;
                        }
                    }
                    else
                    {
                        timerVoltaUmaPag.Interval = timerAndarRetroceder;
                        timerVoltaUmaPag.Start();
                    }
                }
                else
                {
                    DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
                    timerVoltaUmaPag.Stop();

                }
            }
        }
        private void AndaUmaPag_Click(object sender, EventArgs e)
        {
            if (botaoSelecionado != null)
            {
                DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
            }
            botaoSelecionado = sender as Button;

            timerAvanca.Stop();
            timerRetrocede.Stop();
            timerVoltaUmaPag.Stop();

            // Ao clicar, altera a cor de fundo para indicar que está selecionado
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.BackColor == Color.OrangeRed)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(227, 68, 0);  // Cor para indicar seleção

                    // Alterar a cor do texto, se necessário
                    btn.ForeColor = Color.White;

                    // Alterar o estilo da borda, se necessário
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;
                    if (timerAndarRetroceder == 0)
                    {
                        if (GlobVar.maximaVect <= GlobVar.matrizCanal.GetLength(1))
                        {
                            int AndarUmSegundo = GlobVar.namos * 1;
                            int AndaUmSegundoNumerico = GlobVar.numeroAmos * 1;
                            camera.X += AndarUmSegundo * GlobVar.SPEED;

                            GlobVar.indiceNumero += AndaUmSegundoNumerico * (int)GlobVar.SPEED;
                            GlobVar.maximaNumero += AndaUmSegundoNumerico * (int)GlobVar.SPEED;
                            float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                            GlobVar.maximaVect += AndarUmSegundo * (int)GlobVar.SPEED;
                            GlobVar.indice += AndarUmSegundo * (int)GlobVar.SPEED;
                            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                            GlobVar.inicioTela += (AndarUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                            GlobVar.finalTela += (AndarUmSegundo * (int)GlobVar.SPEED) / GlobVar.namos;
                            UpdateInicioTela();
                            TelaClearAndReload();
                            foiencontradoumUltimo = false;
                            foiencontradoumUltimo = false;

                        }
                    }
                    else
                    {
                        timerAndaUmaPag.Interval = timerAndarRetroceder;
                        timerAndaUmaPag.Start();
                    }
                }
                else
                {
                    DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
                    timerAndaUmaPag.Stop();

                }
            }
        }
        private void Pausa_Click(object sender, EventArgs e)
        {
            if (botaoSelecionado != null)
            {
                DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
            }
            botaoSelecionado = sender as Button;

            // Ao clicar, altera a cor de fundo para indicar que está selecionado
            Button btn = sender as Button;
            if (btn != null)
            {
                if (btn.BackColor == Color.OrangeRed)
                {
                    // Alterar a cor de fundo para mostrar o estado "selecionado"
                    btn.BackColor = Color.FromArgb(227, 68, 0);  // Cor para indicar seleção

                    // Alterar a cor do texto, se necessário
                    btn.ForeColor = Color.White;

                    // Alterar o estilo da borda, se necessário
                    btn.FlatAppearance.BorderSize = 2;
                    btn.FlatAppearance.BorderColor = Color.DarkGreen;

                    timerAvanca.Stop();
                    timerRetrocede.Stop();
                    timerVoltaUmaPag.Stop();
                    timerAndaUmaPag.Stop();

                }
                else
                {
                    DesmarcarBotao(botaoSelecionado);  // Desmarcar o botão anterior
                }
            }
        }
        // Caso queira "desmarcar" o botão ao clicar fora dele ou em outro botão
        private void DesmarcarBotao(Button btn)
        {
            // Restaurar a cor original do botão
            btn.BackColor = Color.OrangeRed;  // Cor original
            btn.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.Black;
        }
        // Evento para garantir que apenas números sejam inseridos
        private void PtsEmTela_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Obtém a última linha da tabela
            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();

            // Se houver uma última linha, define o valor máximo possível baseado no campo "NumPag"
            int maximoPossivel = lastRow != null ? Convert.ToInt32(lastRow["NumPag"]) : 0;

            // Converte esse valor para o número de dígitos permitidos
            int maxLength = maximoPossivel.ToString().Length;

            // Verifica se a tecla pressionada não é um número ou backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // Cancela a entrada se não for um número ou backspace
                e.Handled = true;
            }

            // Verifica se o número de caracteres excedeu o limite definido por maxLength
            if (ptsEmTela.Text.Length >= maxLength && !char.IsControl(e.KeyChar))
            {
                // Cancela a entrada se o limite de caracteres for atingido
                e.Handled = true;
            }
        }
        public async void PtsEmTela_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int pagina = Convert.ToInt32(ptsEmTela.Text);
                var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();

                int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]);

                if (pagina < 0) { pagina = 0; }
                if (pagina > (maximoPossivel / 30)) { pagina = maximoPossivel / 30; }

                string telaPagText = "000";
                if (pagina >= 100)
                {
                    telaPagText = $"{pagina}";
                }
                else if (pagina >= 10)
                {
                    telaPagText = $"0{pagina}";
                }
                else
                {
                    if(pagina < 0) { pagina = 0; }
                    telaPagText = $"00{pagina}";
                }
                ptsEmTela.Text = $"{telaPagText}";

                int newloc = pagina * 30;
                if (!conc)
                {
                    int newLoc = newloc * GlobVar.namos;
                    if (newLoc > GlobVar.areaCarregadaAltMont || newLoc < GlobVar.indice)
                    {
                        LeituraEmMatrizTeste.Pause();
                        await LeituraEmMatrizTeste.CarregamentoMontagemRapido(4, newLoc);
                    }
                }
                float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                camera.X = newloc * GlobVar.namos;

                GlobVar.indice = newloc * GlobVar.namos;
                GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
                hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

                GlobVar.indiceNumero = newloc * GlobVar.namosNumerico;
                GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);
                foiencontradoumUltimo = false;
                foiencontradoumUltimo = false;

                int alturaTela = (int)openglControl1.Height;
                //gl.Translate(camera.X, 0, 1);
                TelaClearAndReload();
                hScrollBar1.Refresh();
                UpdateInicioTela();
                LeituraEmMatrizTeste.Resume();
            }
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
                canais.RealocPanel(qtdGrafic);
                canais.quantidadeGraf(qtdGrafic);
                canais.RealocButton();
                canais.PainelLb_Resize();
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

                //UpdateInicioTela();
                click = true;
            }

        }
        private void ApnEvent_Click(object sender, EventArgs e) //------------------------------- apn eventos
        {
            if (GlobVar.eventosUpdate != null)
            {
                int[] valoresProcurados = { 1, 2, 3};
                string nomeArquivo = $"{Path.GetFileNameWithoutExtension(GlobVar.textFile)}_ApneiaEventos.txt";
                string pastaDownloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string caminhoArquivo = Path.Combine(pastaDownloads, nomeArquivo);

                bool possuiValores = GlobVar.eventosUpdate.AsEnumerable()
                    .Any(row => valoresProcurados.Contains(row.Field<int>("CodEvento")));

                if (possuiValores)
                {
                    var listaEventosResp = new List<string>();

                    var tableEventoRespiratorio = GlobVar.eventosUpdate.AsEnumerable()
                        .Where(row => valoresProcurados.Contains(row.Field<int>("CodEvento")))
                        .CopyToDataTable();
                    tableEventoRespiratorio = tableEventoRespiratorio.AsEnumerable().OrderBy(row => row.Field<int>("CodEvento")).OrderBy(row => row.Field<int>("CodCanal1")).CopyToDataTable();
                    foreach (DataRow row in tableEventoRespiratorio.Rows)
                    {
                        int inicio = Convert.ToInt32(row["Inicio"]);
                        int fim = Convert.ToInt32(row["Duracao"]);
                        string aevent = "";
                        int taxaCanal = GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt32(row["CodCanal1"]))];

                        aevent = plotEventos.ValoresEvento(inicio, fim, taxaCanal, Convert.ToInt32(row["CodCanal1"]));
                        listaEventosResp.Add(aevent);
                    }

                    // Salvar a lista em um arquivo de texto
                    try
                    {
                        File.WriteAllLines(caminhoArquivo, listaEventosResp);
                        MessageBox.Show($"Arquivo salvo com sucesso em: {caminhoArquivo}", "Sucesso", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    }
                }
            }

        }
        private void playSelect_Click(object sender, EventArgs e) //-------------------------------Hipno Eventos
        {
            if (GlobVar.eventosUpdate != null)
            {
                int[] valoresProcurados = { 4, 5, 6};
                string nomeArquivo = $"{Path.GetFileNameWithoutExtension(GlobVar.textFile)}_HipopneiaEventos.txt";
                string pastaDownloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                string caminhoArquivo = Path.Combine(pastaDownloads, nomeArquivo);

                bool possuiValores = GlobVar.eventosUpdate.AsEnumerable()
                    .Any(row => valoresProcurados.Contains(row.Field<int>("CodEvento")));

                if (possuiValores)
                {
                    var listaEventosResp = new List<string>();

                    var tableEventoRespiratorio = GlobVar.eventosUpdate.AsEnumerable()
                        .Where(row => valoresProcurados.Contains(row.Field<int>("CodEvento")))
                        .CopyToDataTable();
                    tableEventoRespiratorio = tableEventoRespiratorio.AsEnumerable().OrderBy(row => row.Field<int>("CodEvento")).OrderBy(row => row.Field<int>("CodCanal1")).CopyToDataTable();
                    foreach (DataRow row in tableEventoRespiratorio.Rows)
                    {
                        int inicio = Convert.ToInt32(row["Inicio"]);
                        int fim = Convert.ToInt32(row["Duracao"]);
                        string aevent = "";
                        int taxaCanal = GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(Convert.ToInt32(row["CodCanal1"]))];

                        aevent = plotEventos.ValoresEvento(inicio, fim, taxaCanal, Convert.ToInt32(row["CodCanal1"]));
                        listaEventosResp.Add(aevent);
                    }

                    // Salvar a lista em um arquivo de texto
                    try
                    {
                        File.WriteAllLines(caminhoArquivo, listaEventosResp);
                        MessageBox.Show($"Arquivo salvo com sucesso em: {caminhoArquivo}", "Sucesso", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar o arquivo: {ex.Message}", "Erro", (MessageBoxButton)MessageBoxButtons.OK, (MessageBoxImage)MessageBoxIcon.Error);
                    }
                }
            }

        }
        private Panel clickedPanel;
        private Panel panelOn;
        // Estrutura para armazenar o painel e sua posição original
        private List<(Panel panel, int originalIndex)> hiddenPanels = new List<(Panel panel, int originalIndex)>();
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
        private void AdjustPanelsAfterResizeWithHiddenPanels(Panel resizedPanel)
        {
            int totalHeight = painelExames.ClientSize.Height; // Altura total do contêiner pai
            int minHeightPerPanel = 20; // Altura mínima permitida para cada painel
            int newHeight = resizedPanel.Height; // Nova altura desejada do painel redimensionado

            // Garantir que o painel redimensionado não seja menor que o mínimo permitido
            if (newHeight < minHeightPerPanel)
            {
                newHeight = minHeightPerPanel;
            }

            // Definir a nova altura do painel redimensionado
            resizedPanel.Height = newHeight;

            // Filtrar os painéis visíveis e que não têm Location.X == -500
            List<Panel> visiblePanels = painelExames.Controls.OfType<Panel>()
                                             .Where(p => p.Visible && p.Location.X != -500)
                                             .ToList();

            int remainingHeight = totalHeight - visiblePanels.Sum(p => p == resizedPanel ? newHeight : p.Height); // Altura restante após o redimensionamento

            // Calcular a altura combinada atual dos outros painéis visíveis, excluindo o painel redimensionado
            List<Panel> otherPanels = visiblePanels
                                       .Where(p => p != resizedPanel)
                                       .ToList();

            int totalCurrentHeightOthers = otherPanels.Sum(p => p.Height);

            // Ajustar proporcionalmente a altura dos outros painéis se houver excesso de altura
            if (totalCurrentHeightOthers > remainingHeight)
            {
                int excessHeight = totalCurrentHeightOthers - remainingHeight; // Altura em excesso
                int reductionPerPanel = excessHeight / otherPanels.Count; // Redução média por painel
                int remainingReduction = excessHeight % otherPanels.Count; // Resto para ajuste fino

                foreach (Panel panel in otherPanels)
                {
                    // Ajustar altura de cada painel, garantindo o mínimo permitido
                    int adjustedHeight = panel.Height - reductionPerPanel;

                    if (remainingReduction > 0)
                    {
                        adjustedHeight--; // Distribuir o ajuste adicional
                        remainingReduction--;
                    }

                    // Garantir que a altura do painel não seja inferior ao mínimo
                    panel.Height = Math.Max(adjustedHeight, minHeightPerPanel);
                }
            }

            // Garantir que todos os painéis respeitem o tamanho mínimo
            foreach (Panel panel in visiblePanels)
            {
                if (panel.Height < minHeightPerPanel)
                {
                    panel.Height = minHeightPerPanel; // Ajustar para o mínimo permitido
                }
            }

            // Recalcular a altura total e verificar a diferença
            int currentTotalHeight = visiblePanels.Sum(p => p.Height);
            int difference = totalHeight - currentTotalHeight;

            // Redistribuir a diferença respeitando o mínimo permitido
            if (difference != 0)
            {
                int adjustmentPerPanel = difference / visiblePanels.Count;

                foreach (Panel panel in visiblePanels)
                {
                    int adjustedHeight = panel.Height + adjustmentPerPanel;

                    // Garantir que o ajuste não faça o painel ficar abaixo do mínimo
                    if (adjustedHeight < minHeightPerPanel)
                    {
                        adjustedHeight = minHeightPerPanel;
                    }

                    panel.Height = adjustedHeight;
                }

                // Ajuste final caso ainda haja diferença devido ao arredondamento
                int finalAdjustment = totalHeight - visiblePanels.Sum(p => p.Height);
                if (finalAdjustment != 0)
                {
                    foreach (var panel in visiblePanels)
                    {
                        int allowedIncrease = finalAdjustment > 0 ? 1 : -1;

                        // Garantir que o ajuste final não ultrapasse o mínimo permitido
                        if (panel.Height + allowedIncrease >= minHeightPerPanel)
                        {
                            panel.Height += allowedIncrease;
                            finalAdjustment -= allowedIncrease;
                        }

                        if (finalAdjustment == 0)
                        {
                            break;
                        }
                    }
                }
            }

            // Atualizar a posição dos painéis visíveis
            int currentY = 0;
            foreach (Panel panel in visiblePanels)
            {
                panel.Top = currentY;
                currentY += panel.Height;
            }

            // Ajustar o tamanho do array GlobVar.desenhoLoc para corresponder ao número de painéis visíveis
            int visiblePanelsCount = painelExames.Controls.OfType<Panel>().Count(p => p.Visible);
            if (GlobVar.desenhoLoc == null || GlobVar.desenhoLoc.Length != visiblePanelsCount)
            {
                GlobVar.desenhoLoc = new float[visiblePanelsCount];
            }

            int index = 0;
            foreach (Panel panel in painelExames.Controls.OfType<Panel>())
            {
                if (panel.Visible)
                {
                    GlobVar.desenhoLoc[index] = panel.Top + (panel.Height / 2);
                    index++;
                }
            }
        }
        private void OcultarCanal_Click(object sender, EventArgs e)
        {
            if (panelOn != null)
            {
                // Salva o índice original antes de ocultar
                int originalIndex = painelExames.Controls.GetChildIndex(panelOn);
                hiddenPanels.Add((panelOn, originalIndex));

                // Remove o painel do controle e o oculta
                painelExames.Controls.Remove(panelOn);
                panelOn.Visible = false;
                // Reajusta os painéis restantes
                AdjustPanelsAfterResizeWithHiddenPanels(panelOn);
                AjustarFonteDosLabels();
                RepositionPanels();
                TelaClearAndReload();
            }
        }
        private void AdjustPanelsAfterShowAll()
        {
            int totalHeight = painelExames.ClientSize.Height; // Altura total disponível do contêiner
            int minHeightPerPanel = 20; // Altura mínima permitida para cada painel

            // Filtrar todos os painéis visíveis após reexibir, ignorando aqueles com Location.X = -500
            List<Panel> visiblePanels = painelExames.Controls.OfType<Panel>()
                                             .Where(p => p.Visible && p.Location.X != -500)
                                             .ToList();

            // Calcular a altura combinada atual dos painéis visíveis
            int totalCurrentHeight = visiblePanels.Sum(p => p.Height);

            // Verificar se é necessário ajustar as alturas para se ajustar ao contêiner
            if (totalCurrentHeight < totalHeight)
            {
                // Redistribuir a diferença de altura igualmente entre os painéis
                int extraHeight = totalHeight - totalCurrentHeight;
                int adjustmentPerPanel = extraHeight / visiblePanels.Count;
                int remainingAdjustment = extraHeight % visiblePanels.Count;

                foreach (Panel panel in visiblePanels)
                {
                    // Ajusta a altura de cada painel
                    panel.Height += adjustmentPerPanel;

                    // Ajusta o painel adicional se houver resto
                    if (remainingAdjustment > 0)
                    {
                        panel.Height++;
                        remainingAdjustment--;
                    }
                }
            }
            else if (totalCurrentHeight > totalHeight)
            {
                // Caso o total das alturas dos painéis seja maior que a altura disponível
                int excessHeight = totalCurrentHeight - totalHeight;
                int reductionPerPanel = excessHeight / visiblePanels.Count;
                int remainingReduction = excessHeight % visiblePanels.Count;

                foreach (Panel panel in visiblePanels)
                {
                    // Ajustar altura de cada painel, garantindo o mínimo permitido
                    int adjustedHeight = panel.Height - reductionPerPanel;

                    if (remainingReduction > 0)
                    {
                        adjustedHeight--; // Distribuir o ajuste adicional
                        remainingReduction--;
                    }

                    // Garantir que a altura do painel não seja inferior ao mínimo
                    panel.Height = Math.Max(adjustedHeight, minHeightPerPanel);
                }
            }

            // Garantir que todos os painéis respeitem o tamanho mínimo
            foreach (Panel panel in visiblePanels)
            {
                if (panel.Height < minHeightPerPanel)
                {
                    panel.Height = minHeightPerPanel; // Ajustar para o mínimo permitido
                }
            }

            // Recalcular a altura total e verificar a diferença
            int currentTotalHeight = visiblePanels.Sum(p => p.Height);
            int difference = totalHeight - currentTotalHeight;

            // Redistribuir a diferença, se houver
            if (difference != 0)
            {
                int adjustmentPerPanel = difference / visiblePanels.Count;

                foreach (Panel panel in visiblePanels)
                {
                    int adjustedHeight = panel.Height + adjustmentPerPanel;

                    // Garantir que o ajuste não faça o painel ficar abaixo do mínimo
                    panel.Height = Math.Max(adjustedHeight, minHeightPerPanel);
                }

                // Ajuste final caso ainda haja diferença devido ao arredondamento
                int finalAdjustment = totalHeight - visiblePanels.Sum(p => p.Height);
                if (finalAdjustment != 0)
                {
                    foreach (var panel in visiblePanels)
                    {
                        int allowedIncrease = finalAdjustment > 0 ? 1 : -1;

                        // Garantir que o ajuste final não ultrapasse o mínimo permitido
                        if (panel.Height + allowedIncrease >= minHeightPerPanel)
                        {
                            panel.Height += allowedIncrease;
                            finalAdjustment -= allowedIncrease;
                        }

                        if (finalAdjustment == 0)
                        {
                            break;
                        }
                    }
                }
            }

            // Atualizar a posição dos painéis visíveis
            int currentY = 0;
            foreach (Panel panel in visiblePanels)
            {
                panel.Top = currentY;
                currentY += panel.Height;
            }

            // Atualiza os valores de localização para GlobVar.desenhoLoc
            int index = 0;
            foreach (Panel panel in painelExames.Controls.OfType<Panel>())
            {
                if (panel.Visible && panel.Location.X != -500)
                {
                    GlobVar.desenhoLoc[index] = panel.Top + (panel.Height / 2);
                    index++;
                }
            }
        }
        private void MostrarTodosCanais_Click(object sender, EventArgs e)
        {
            // Ordena os painéis ocultos pela posição original
            var sortedHiddenPanels = hiddenPanels.OrderBy(p => p.originalIndex).ToList();

            foreach (var (panel, originalIndex) in sortedHiddenPanels)
            {
                // Reinsere o painel na posição original
                painelExames.Controls.Add(panel);
                painelExames.Controls.SetChildIndex(panel, originalIndex);
                panel.Visible = true;
            }

            // Limpa a lista de ocultos após re-adicionar
            hiddenPanels.Clear();
            AdjustPanelsAfterShowAll();
            // Reajusta os painéis visíveis
            RepositionPanels();
            AjustarFonteDosLabels();
            TelaClearAndReload();
        }
        private void Descricao_Click(object sender, EventArgs e)
        {
            try
            {
                PlotagemOpenGL.FormesMenuPanels.InformaçõesDoCanal Info = new PlotagemOpenGL.FormesMenuPanels.InformaçõesDoCanal();
                buttonForm.HideOverlay();

                // Exibe o formulário e espera o fechamento
                DialogResult result = Info.ShowDialog();

                // Verifica se o formulário foi fechado
                if (result == DialogResult.OK)
                {
                    UpdateFilterStates();
                }
            }
            catch { }

        }
        private void Amplitude_DropDownOpening(object sender, EventArgs e)
        {
            // Captura o valor da variável ampli
            float ampli = Convert.ToSingle(GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"]);

            // Itera pelos itens no DropDown
            foreach (ToolStripItem item in Amplitude.DropDownItems)
            {
                // Verifica se o item é um ToolStripMenuItem
                if (item is ToolStripMenuItem menuItem)
                {
                    // Converte o texto do item para float e compara com ampli
                    if (float.TryParse(menuItem.Text, out float itemValue) && itemValue == ampli)
                    {
                        // Marca o item se corresponder ao valor de ampli
                        menuItem.Checked = true;
                    }
                    else
                    {
                        // Desmarca os itens que não correspondem
                        menuItem.Checked = false;
                    }
                }
            }
        }
        private void AltoScala_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);
            if (AltoScala.Checked)
            {
                rowNumerico["AutoEscala"] = true;
            }
            else
            {
                rowNumerico["AutoEscala"] = false;
            }
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();
            TelaClearAndReload();
        }
        private void InverteSinal_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);
            if (InverteSinal.Checked)
            {
                rowNumerico["InverteSinal"] = true;
            }
            else
            {
                rowNumerico["InverteSinal"] = false;
            }
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();
            TelaClearAndReload();
        }
        private void MostrarFaixaDeAmpli_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);
            if (MostrarFaixaDeAmpli.Checked)
            {
                rowNumerico["EliminaFreqInf"] = 1;
            }
            else
            {
                rowNumerico["EliminaFreqInf"] = DBNull.Value;
            }
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();
            TelaClearAndReload();

        }
        private void AlterarRef_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

            // Supondo que rowNumerico seja uma DataRow atual
            // Obter o valor atual de CodCanal2
            int codCanalAtual = Convert.ToInt32(rowNumerico["CodCanal2"]);

            // Encontrar o índice do valor atual em canaisReferencia
            int indexAtual = Array.IndexOf(GlobVar.canaisReferencia, codCanalAtual);

            // Se encontrar o valor, mover para o próximo índice
            int proximoIndex;
            if (indexAtual != -1)
            {
                // Ir para o próximo valor, e se for o último, volta para o primeiro
                proximoIndex = (indexAtual + 1) % GlobVar.canaisReferencia.Length;
            }
            else
            {
                // Se o valor atual não estiver no vetor, use o primeiro
                proximoIndex = 0;
            }

            // Atualizar CodCanal2 com o novo valor
            rowNumerico["CodCanal2"] = GlobVar.canaisReferencia[proximoIndex];


            // Atualizar matrizCanal com o novo valor de CodCanal2
            int codCanal1 = Convert.ToInt32(rowNumerico["CodCanal1"]);
            int novoCodCanal2 = Convert.ToInt32(rowNumerico["CodCanal2"]);

            // Chamar o método para atualizar a matriz com o novo valor de CodCanal2
            GlobVar.matrizCanal.SetRow<short>(
                GlobVar.codSelected.IndexOf(codCanal1),
                LeituraEmMatrizTeste.SetReferencia(codCanal1, novoCodCanal2));

            var cadCanal = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == Tela_Plotagem.tagCodCanal).CopyToDataTable();
            var cadCanal2 = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == Convert.ToInt32(rowNumerico["CodCanal2"])).CopyToDataTable();
            string newLegenda = $"{cadCanal.Rows[0]["NomeCanal"]} - {cadCanal2.Rows[0]["NomeCanal"]}";
            rowNumerico["Legenda"] = newLegenda;

            GlobVar.tbl_MontagemSelecionada.AcceptChanges();
            if (rowNumerico["PassaBaixa"] != DBNull.Value || rowNumerico["PassaAlta"] != DBNull.Value || rowNumerico["Notch"] != DBNull.Value)
            {
                float hertzSelectHigh = 0;
                float hertzSelect = 0;
                bool isAnyFilterSelectedHigh;

                if (rowNumerico["PassaAlta"] != DBNull.Value)
                {
                    hertzSelectHigh = (float)rowNumerico["PassaAlta"];
                } //Verifica se tem algum filtro aplicado de HighPass

                if (hertzSelectHigh == 0)
                {
                    int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                    if (rowNumerico["PassaBaixa"] == DBNull.Value)
                    {

                    }
                    else
                    {
                        hertzSelect = Convert.ToInt32(rowNumerico["PassaBaixa"]);
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])),
                        LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                    }
                }
                else
                {
                    int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                    if (rowNumerico["PassaBaixa"] == DBNull.Value)
                    {
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])),
                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])))), (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                    }
                    else
                    {
                        hertzSelect = Convert.ToInt32(rowNumerico["PassaBaixa"]);
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])),
                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])))),
                        (float)hertzSelect,
                        (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                    }

                }
                if (rowNumerico["Notch"] != DBNull.Value)
                {
                    float NotchHertz = Convert.ToInt32(rowNumerico["Notch"]);
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(rowNumerico["CodCanal1"])))),
                        (float)NotchHertz,
                        10,
                        GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }

            }

            foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
            {
                if ((int)pn.Tag == tagCodCanal)
                {
                    foreach (Label lb in pn.Controls.OfType<Label>())
                    {
                        if (lb.Tag is int intTag)
                        {
                            if (intTag == tagCodCanal)
                            {
                                lb.Text = newLegenda;
                            }
                        }
                        else if (lb.Tag is string strTag)
                        {
                            if (int.TryParse(strTag, out int stringTagAsInt) && stringTagAsInt == tagCodCanal)
                            {
                                lb.Text = newLegenda;
                            }
                        }
                    }
                }
            }
            TelaClearAndReload();
        }// Analisar dps
        private void CanalCor_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                        .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

            Color c = Color.Black;
            buttonForm.HideOverlay();
            ColorPickerDialog minhasCores = new ColorPickerDialog();
            if(minhasCores.ShowDialog() == DialogResult.OK)
            {
                c = minhasCores.Color;
                int cor = c.R | (c.G << 8) | (c.B << 16);
                rowNumerico["Cor"] = cor;

                GlobVar.tbl_MontagemSelecionada.AcceptChanges();
                TelaClearAndReload();
            }
        }
        private void Legenda_Click(object sender, EventArgs e)
        {

            try
            {
                LegendaInput legInput = new LegendaInput();
                buttonForm.HideOverlay();
                legInput.ShowDialog();
            }
            catch { }
        }
        private void LimiteSuperior_Click(object sender, EventArgs e)
        {
            try
            {
                LimiteSuperior lmSup = new LimiteSuperior();
                buttonForm.HideOverlay();
                lmSup.ShowDialog();
            }
            catch { }
        }
        private void LimiteInferior_Click(object sender, EventArgs e)
        {
            try
            {
                LimiteInferior lmInf = new LimiteInferior();
                buttonForm.HideOverlay();
                lmInf.ShowDialog();
            }
            catch { }
        }
        private void AmpliMenus_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = sender as ToolStripMenuItem;
            int newAmpli = Convert.ToInt32(clickedItem.Text);

            GlobVar.tbl_MontagemSelecionada.Rows[index]["AmplitudeMin"] = newAmpli;
            float scala = (float)(newAmpli) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(index));

            GlobVar.scale[index] = scala;

            foreach (Panel pn in painelExames.Controls)
            {
                if ((int)pn.Tag == tagCodCanal)
                {
                    foreach (Label lb in pn.Controls.OfType<Label>())
                    {
                        if (lb.Tag.Equals("scala"))
                        {
                            lb.Text = $"{newAmpli} μV";
                        }
                    }
                }
            }


            TelaClearAndReload();
        }
        private void ApenasNumero_Click(object sender, EventArgs e)
        {
            // Inverte o estado do checkbox
            //ApenasNumero.Checked = !ApenasNumero.Checked;

            // Encontra a linha correspondente no DataTable com base no CodCanal1
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

            // Verifica se a linha foi encontrada
            if (rowNumerico != null)
            {
                // Atualiza o campo "InverteSinal" com base no estado atual do checkbox
                rowNumerico["InverteSinal"] = ApenasNumero.Checked;
                rowNumerico["EliminaFreqInf"] = DBNull.Value;
            }
            foreach(Panel pn in painelExames.Controls)
            {
                if((int)pn.Tag == tagCodCanal)
                {
                    if (!ApenasNumero.Checked || rowNumerico["EliminaFreqInf"] != DBNull.Value)
                    {
                        foreach(Label lb in pn.Controls.OfType<Label>())
                        {
                            if(lb.Tag != null){
                                if (lb.Tag.Equals("min"))
                                {
                                    lb.Show();


                                    Point minLoc = new Point(0, 0);

                                    minLoc.X = pn.Width - 30;
                                    minLoc.Y = pn.Height - 15;

                                    lb.Location = new System.Drawing.Point(minLoc.X, minLoc.Y);
                                }
                                else if (lb.Tag.Equals("max"))
                                {
                                    lb.Show();
                                    Point maxLoc = new Point(0, 0);

                                    maxLoc.X = pn.Width - 30;
                                    maxLoc.Y = 1;

                                    lb.Location = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if(lb.Tag != null){
                                if (lb.Tag.Equals("min"))
                                {
                                    lb.Hide();
                                }
                                else if (lb.Tag.Equals("max"))
                                {
                                    lb.Hide();
                                }
                            }
                        }

                    }
                }
            }
            // Recarrega a tela após a atualização
            TelaClearAndReload();
        }
        private void MostrarSetas_Click(object sender, EventArgs e)
        {
            // Inverte o estado do checkbox
            //ApenasNumero.Checked = !ApenasNumero.Checked;

            // Encontra a linha correspondente no DataTable com base no CodCanal1
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

            // Verifica se a linha foi encontrada
            if (rowNumerico != null)
            {
                // Atualiza o campo "InverteSinal" com base no estado atual do checkbox
                rowNumerico["InverteSinal"] = MostrarSetas.Checked;
                rowNumerico["EliminaFreqInf"] = DBNull.Value;
            }
            if (MostrarSetas.Checked)
            {
                foreach(Panel pn in painelExames.Controls)
                {
                    if((int)pn.Tag == tagCodCanal)
                    {
                        foreach(Label lb in pn.Controls.OfType<Label>())
                        {
                            if (lb.Tag.Equals("setas"))
                            {
                                lb.Hide();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Panel pn in painelExames.Controls)
                {
                    if ((int)pn.Tag == tagCodCanal)
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if (lb.Tag.Equals("setas"))
                            {
                                lb.Show();
                            }
                        }
                    }
                }
            }

            // Recarrega a tela após a atualização
            TelaClearAndReload();

        }
        private void Configurar_Click(object sender, EventArgs e)
        {
            // Encontra a linha correspondente no DataTable com base no CodCanal1
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

            // Verifica se a linha foi encontrada
            if (Configurar.Checked)
            {
                if(rowNumerico != null)
                {
                    // Atualiza o campo "InverteSinal" com base no estado atual do checkbox
                    rowNumerico["EliminaFreqInf"] = 1;
                    rowNumerico["InverteSinal"] = false;
                }
            }
            else
            {
                // Verifica se a linha foi encontrada
                if (rowNumerico != null)
                {
                    // Atualiza o campo "EliminaFreqInf" com base no estado atual do checkbox
                    rowNumerico["EliminaFreqInf"] = DBNull.Value;
                    rowNumerico["InverteSinal"] = true;
                }

            }

            // Recarrega a tela após a atualização
            TelaClearAndReload();

        }
        private void GraficoENumero_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

            if (GraficoENumero.Checked)
            {
                // Verifica se a linha foi encontrada
                if (rowNumerico != null)
                {
                    // Atualiza o campo "EliminaFreqInf" com base no estado atual do checkbox
                    rowNumerico["EliminaFreqInf"] = 1;
                    rowNumerico["InverteSinal"] = false;

                }

            }
            else
            {
                // Verifica se a linha foi encontrada
                if (rowNumerico != null)
                {
                    // Atualiza o campo "EliminaFreqInf" com base no estado atual do checkbox
                    rowNumerico["EliminaFreqInf"] = DBNull.Value;
                    rowNumerico["InverteSinal"] = true;
                    foreach (Panel pn in painelExames.Controls)
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if (lb.Tag != null) 
                            { 
                                if (lb.Tag.Equals("min"))
                                {
                                    lb.Hide();
                                }
                                else if (lb.Tag.Equals("max"))
                                {
                                    lb.Hide();
                                }
                            }
                        }
                    }
                }

            }

            foreach (Panel pn in painelExames.Controls)
            {
                if ((int)pn.Tag == tagCodCanal)
                {
                    
                    if (!ApenasNumero.Checked || rowNumerico["EliminaFreqInf"] != DBNull.Value)
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if(lb.Tag != null){
                                if (lb.Tag.Equals("min"))
                                {
                                    lb.Show();


                                    Point minLoc = new Point(0, 0);

                                    minLoc.X = pn.Width - 30;
                                    minLoc.Y = pn.Height - 15;

                                    lb.Location = new System.Drawing.Point(minLoc.X, minLoc.Y);
                                }
                                else if (lb.Tag.Equals("max"))
                                {
                                    lb.Show();
                                    Point maxLoc = new Point(0, 0);

                                    maxLoc.X = pn.Width - 30;
                                    maxLoc.Y = 1;

                                    lb.Location = new System.Drawing.Point(maxLoc.X, maxLoc.Y);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if(lb.Tag != null){
                                if (lb.Tag.Equals("min"))
                                {
                                    lb.Hide();
                                }
                                else if (lb.Tag.Equals("max"))
                                {
                                    lb.Hide();
                                }
                            }
                        }

                    }
                }
            }

            TelaClearAndReload();

        }
        private void HorizontalOuVertical_Click(object sender, EventArgs e)
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);
            if (rowNumerico != null)
            {
                if ((bool)rowNumerico["AutoEscala"])
                {
                    rowNumerico["AutoEscala"] = false;
                }
                else
                {
                    rowNumerico["AutoEscala"] = true;
                }
            }
            TelaClearAndReload();

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
                        contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] {BomDiaExclui});
                    }
                    else if (GlobVar.nomeEvento.Equals("Boa noite"))
                    {
                        contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] {BoaNoiteExclui });
                    }
                    else if (GlobVar.nomeEvento.Equals("Início CPAP"))
                    {
                        contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] {InicioCPAPExclui });
                    }

                }
                else if ((isThereAComment || isThereAXSartComment || isThereAXEndComment || isThereAYStartComment || isThereAYEndComment || isThereX0Y0Comment || isThereX0Y1Comment || isThereX1Y0Comment || isThereX1Y1Comment))
                {
                    contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] { EditarComentario, toolStripSeparator1, ExcluirComentario });
                }
                else
                {
                    //Else seria quando ele abre fora de um evento
                    contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] { BomDia, BoaNoite, InicioCPAP, toolStripSeparator1, CorDeFundo, InserirCom, Linha1Seg, MesmaAlturaCanais, ReeshowAllCanal, LinhaZeroCanais, MostarAmplitudes, Epoca30Seg, Regua, Pontilhado200Mili});
                }
            }
            catch { }

        }
        private void ToolBarMenu_Opening(object sender, EventArgs e)
        {
            try
            {
                toolStripMenuItem2.Checked = PainelAvRet.Visible;
                toolStripMenuItem4.Checked = PainelLoc.Visible;
                toolStripMenuItem5.Checked = PainelMarca.Visible;
                toolStripMenuItem6.Checked = PainelMarcaAntProx.Visible;
                toolStripMenuItem7.Checked = PanelEventos.Visible;
                toolStripMenuItem8.Checked = PainelPrinters.Visible;
                                                                                                                                                                                                                                                                                                                                                       
            }
            catch
            {

            }
        }
        public static void alterarRefText(DataRow rowNumerico)
        {
            // Obtém o valor atual de CodCanal2
            int codCanalAtual = Convert.ToInt32(rowNumerico["CodCanal2"]);

            // Encontra o índice do valor atual no vetor de referências
            int indexAtual = Array.IndexOf(GlobVar.canaisReferencia, codCanalAtual);

            // Se o valor atual estiver no vetor, pegamos o próximo (alternado)
            int proximoIndex;

            // Se o valor atual estiver no vetor, alterna para o próximo valor
            if (indexAtual != -1)
            {
                // Se o valor atual for o último, volta para o primeiro. Caso contrário, vai para o próximo
                proximoIndex = (indexAtual + 1) % GlobVar.canaisReferencia.Length;
            }
            else
            {
                // Se o valor atual não estiver no vetor, começa com o primeiro valor do vetor
                proximoIndex = 0;
            }

            // Obtém o próximo valor de referência
            int proximoValorReferencia = GlobVar.canaisReferencia[proximoIndex];

            // Procura o nome do canal correspondente ao próximo valor de referência
            var cadCanal2 = GlobVar.tbl_CadCanal.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("CodCanal") == proximoValorReferencia);

            // Atualiza o texto do botão AlterarRef com o nome do próximo canal de referência
            if (cadCanal2 != null)
            {
                AlterarRef.Text = $"Alterar Ref. para: {cadCanal2["NomeCanal"]}";
            }
        }
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            timer1.Start();

            ContextMenuStrip menu = sender as ContextMenuStrip;

            if (menu != null)
            {
                Control sourceControl = menu.SourceControl;

                if(sourceControl is Label lb) 
                {
                    sourceControl = lb.Parent as Panel;
                }
                if (sourceControl is Panel panel)
                {

                    var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                            .Where(row => row.Field<int>("CodCanal") == tagCodCanal).CopyToDataTable();
                    int TipoCanal = Convert.ToInt32(CodTipoCanal.Rows[0]["CodTipo"]);

                    contextMenuStrip1.Items.Clear();

                    if(TipoCanal == 12)
                    {
                        contextMenuStrip1.Items.AddRange(new ToolStripItem[] {Descricao, CanalCor, Legenda, Configurar, MostrarSetas, OcultarCanal });

                        var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                            .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

                        // Verifica se a linha foi encontrada
                        if (rowNumerico != null)
                        {
                            if (rowNumerico["EliminaFreqInf"] != DBNull.Value)
                            {
                                Configurar.Checked = true;
                            }
                            else
                            {
                                Configurar.Checked = false;
                            }

                            // Atualiza o campo "InverteSinal" com base no estado atual do checkbox
                            if ((bool)rowNumerico["InverteSinal"])
                            {
                                MostrarSetas.Checked = true;
                            }
                            else
                            {
                                MostrarSetas.Checked = false;
                            }
                        }

                    }
                    else if(TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                        || TipoCanal == 15 || TipoCanal == 30 || TipoCanal == 38)
                    {
                        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { Descricao, CanalCor, Legenda, HorizontalOuVertical, GraficoENumero, ApenasNumero, LimiteSuperior, LimiteInferior, OcultarCanal });
                        var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                            .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);

                        // Verifica se a linha foi encontrada
                        if (rowNumerico != null)
                        {                    
                            if(rowNumerico["EliminaFreqInf"] != DBNull.Value)
                            {
                                GraficoENumero.Checked = true;
                            }
                            else
                            {
                                GraficoENumero.Checked = false;
                            }

                            // Atualiza o campo "InverteSinal" com base no estado atual do checkbox
                            if ((bool)rowNumerico["InverteSinal"])
                            {
                                ApenasNumero.Checked = true;
                            }
                            else
                            {
                                ApenasNumero.Checked = false;
                            }

                            if ((bool)rowNumerico["AutoEscala"])
                            {
                                HorizontalOuVertical.Text = "Alterar para Horizontal";
                            }
                            else
                            {
                                HorizontalOuVertical.Text = "Alterar para Vertical";
                            }
                        }

                    }
                    else if(TipoCanal == 1)
                    {
                        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { Descricao, CanalCor, Legenda, InverteSinal, AltoScala, Amplitude, Filtos, OcultarCanal, AlterarRef, MostrarFaixaDeAmpli});
                        Filtos.DropDownItems.AddRange(new ToolStripItem[] { LowPassFilterGl, HighPassFilterGl, NotchPassFilter });
                        var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                            .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);
                        if ((bool)rowNumerico["AutoEscala"])
                        {
                            AltoScala.Checked = true;
                        }
                        else
                        {
                            AltoScala.Checked = false;
                        }
                        if (rowNumerico["EliminaFreqInf"] != DBNull.Value)
                        {
                            MostrarFaixaDeAmpli.Checked = true;
                        }
                        else
                        {
                            MostrarFaixaDeAmpli.Checked = false;
                        }
                        if ((bool)rowNumerico["InverteSinal"])
                        {
                            InverteSinal.Checked = true;
                        }
                        else
                        {
                            InverteSinal.Checked = false;
                        }

                        alterarRefText(rowNumerico);

                        clickedPanel = panel;
                        // UpdateMenuItems(panel, menu.Items, panelLowFilterStates[panel]);
                        //UpdateMenuItems(panel, menu.Items, panelHighFilterStates[panel]);
                        //UpdateMenuItems(panel, menu.Items, panelNotchFilterStates[panel]);

                    }
                    else
                    {
                        contextMenuStrip1.Items.AddRange(new ToolStripItem[] { Descricao, CanalCor, Legenda, InverteSinal, AltoScala, Amplitude, Filtos, OcultarCanal});
                        Filtos.DropDownItems.AddRange(new ToolStripItem[] { LowPassFilterGl, HighPassFilterGl, NotchPassFilter });

                        var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                            .FirstOrDefault(row => row.Field<int>("CodCanal1") == tagCodCanal);
                        if ((bool)rowNumerico["AutoEscala"])
                        {
                            AltoScala.Checked = true;
                        }
                        else
                        {
                            AltoScala.Checked = false;
                        }
                        if ((bool)rowNumerico["InverteSinal"])
                        {
                            InverteSinal.Checked = true;
                        }
                        else
                        {
                            InverteSinal.Checked = false;
                        }
                        clickedPanel = panel;
                        UpdateMenuItems(panel, menu.Items, panelLowFilterStates[panel]);
                        UpdateMenuItems(panel, menu.Items, panelHighFilterStates[panel]);
                        UpdateMenuItems(panel, menu.Items, panelNotchFilterStates[panel]);


                    }




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
        private void FiltrosDropDownOpening(object sender, EventArgs e)
        {
            // Identificar o item que disparou o evento
            if (sender is ToolStripMenuItem clickedItem && clickedPanel is Panel panel)
            {
                // Atualizar os itens do menu com base no dicionário correspondente ao painel
                UpdateMenuItemsFilters(panel, clickedItem.DropDownItems, panelLowFilterStates[panel]);
            }
        }
        private void CorDeFundo_Click(object sender, EventArgs e)
        {
            try
            {
                PlotagemOpenGL.auxi.FormColorRGB.Colors insert = new PlotagemOpenGL.auxi.FormColorRGB.Colors();
                insert.ShowDialog();
            }
            catch { }

        }
        private void Epoca30Seg_Click(object sender, EventArgs e)
        {
            tempoEmTela.SelectedIndex = 5;
            GlobVar.segundos = 30;

            GlobVar.tmpEmTela = GlobVar.namos * GlobVar.segundos;
            GlobVar.saltoTelas = GlobVar.tmpEmTela;

            GlobVar.inicioTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos;
            //if (GlobVar.segundos >= 120)
            //{
            //    GlobVar.maximaVect *= 10;
            //}
            GlobVar.finalTela = (int)GlobVar.saltoTelas / (int)GlobVar.namos + (int)GlobVar.inicioTela;

            GlobVar.tmpEmTelaNumerico = GlobVar.namosNumerico * GlobVar.segundos;
            GlobVar.finalTelaNumerico = (int)GlobVar.tmpEmTelaNumerico / (int)GlobVar.namosNumerico;
            GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;


            int PagAux = DimensionRightClick.X / GlobVar.namos;
            int inicioPag = PagAux - 15;
            int finalPag = PagAux + 15;
            if (inicioPag < 0)
            {
                inicioPag = 0;
                finalPag = 30;
            }
            float calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

            GlobVar.indice = inicioPag * GlobVar.namos;
            GlobVar.maximaVect = finalPag * GlobVar.namos;
            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
            hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

            GlobVar.indiceNumero = inicioPag * GlobVar.namosNumerico;
            GlobVar.maximaNumero = finalPag * GlobVar.namosNumerico;
            camera.X = GlobVar.indice;
            UpdateInicioTela();
            foiencontradoumUltimo = false;
            foiencontradoumUltimo = false;

            TelaClearAndReload();

        }
        private void Regua_Click(object sender, EventArgs e)
        {
            TelaClearAndReload();
        }
        private void MostarAmplitudes_Click(object sender, EventArgs e)
        {
            if (!MostarAmplitudes.Checked)
            {
                for (int i = 1; i <= 23; i++) //Relaloca os Label's os butoes dentro do panel
                {
                    FieldInfo field = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                    if (field != null)
                    { //(Panel)field.GetValue(this);
                        Label label = (Label)field.GetValue(this);
                        if (label != null)
                        {
                            label.Hide();
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= 23; i++) //Relaloca os Label's os butoes dentro do panel
                {
                    FieldInfo field = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                    if (field != null)
                    { //(Panel)field.GetValue(this);
                        Label label = (Label)field.GetValue(this);
                        if (label != null)
                        {
                            label.Show();
                        }
                    }
                }
            }
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
                    int newCodevento = Convert.ToInt32(TableAux.Rows[0]["CodEvento"]);

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
        private void InserirCom_Click(object sender, EventArgs e)
        {
            try
            {
                InserirComentario insert = new InserirComentario();
                insert.Show();
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
        private void DeletCommentClick(object sender, EventArgs e)
        {
            try
            {
                plotComentatios.DeleteComment(GlobVar.CommentSeq);
                TelaClearAndReload();
            }
            catch { }
        }
        public static bool EditComentClick = false;
        private void EditarComentarioClick(object sender, EventArgs args)
        {
            try
            {
                EditComentClick = true;
                InserirComentario insert = new InserirComentario();                
                insert.ShowDialog();

            }
            catch { }
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
                        string outro = "OutroLowGl";
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
                        string outro = "OutroHighGl";
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
        private void UpdateMenuItemsFilters(Panel panel, ToolStripItemCollection items, Dictionary<string, bool> filterStates)
        {
            foreach (ToolStripItem item in items)
            {
                if (item is ToolStripMenuItem menuItem && filterStates.ContainsKey(menuItem.Name))
                {
                    menuItem.Checked = filterStates[menuItem.Name];
                }
            }
        }
        // ---------------------- Parte aonde mostra quais filtros estao aplicados no canal, com base em qual panel ele abre
        private void toolTripItemDropDown_OpeningLow(object sender, EventArgs e)
        {
            // Identificar o item que disparou o evento
            if (sender is ToolStripMenuItem clickedItem && clickedPanel is Panel panel)
            {
                // Atualizar os itens do menu com base no dicionário correspondente ao painel
                UpdateMenuItemsFilters(panel, clickedItem.DropDownItems, panelLowFilterStates[panel]);
            }
        }
        private void toolTripItemDropDown_OpeningHigh(object sender, EventArgs e)
        {
            // Identificar o item que disparou o evento
            if (sender is ToolStripMenuItem clickedItem && clickedPanel is Panel panel)
            {
                // Atualizar os itens do menu com base no dicionário correspondente ao painel
                UpdateMenuItemsFilters(panel, clickedItem.DropDownItems, panelHighFilterStates[panel]);
            }
        }
        private void toolTripItemDropDown_OpeningNotch(object sender, EventArgs e)
        {
            // Identificar o item que disparou o evento
            if (sender is ToolStripMenuItem clickedItem && clickedPanel is Panel panel)
            {
                // Atualizar os itens do menu com base no dicionário correspondente ao painel
                UpdateMenuItemsFilters(panel, clickedItem.DropDownItems, panelNotchFilterStates[panel]);
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
        private void MenuItem_Click(object sender, EventArgs e)  //Aplicacao de filtro ------------------------------------------------------------------------------------------------------- 
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

                            if (value.Equals("NenhumHighGl"))
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
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                    LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                        float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                        LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                            //para pegar a informacao de um valor que esteja sendo aplicado no filtro que seja em Outro, pega do banco de dados, para aplicar o filtro corretamente
                            else if (value.Equals("outroHighGl"))
                            {
                                Int32 hertzSelectH = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"]);
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = DBNull.Value;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                        float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    hertzSelectH = Convert.ToInt32(value.Substring(5, 1));
                                }
                                else if (value.Substring(5, 1).IsEqual("0"))
                                {
                                    hertzSelectH = Convert.ToInt32(value.Substring(5, 2));
                                    hertzSelectH = hertzSelectH / 10;
                                }
                                else
                                {
                                    hertzSelectH = Convert.ToInt32(value.Substring(5, 2));
                                }
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = DBNull.Value;

                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                        float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectH, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }

                            }
                        }
                        if (GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] != DBNull.Value)
                        {
                            float NotchHertz = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"]);
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))),
                                NotchHertz,
                                10,
                                GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                        }
                    TelaClearAndReload();
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

                            if (value.Equals("NenhumLowGl"))
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
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                        LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                            hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(2, 1));
                                            hertzSelect /= 10;
                                        }
                                        else
                                        {
                                            hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                        }
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                            else if (value.Equals("OutroLowGl"))
                            {
                                Int32 hertzSelectL = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"]);
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = DBNull.Value;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                            hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(2, 1));
                                            hertzSelect /= 10;
                                        }
                                        else
                                        {
                                            hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                        }
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }

                            }
                            else
                            {
                                float hertzSelectL = Convert.ToInt32(value.Substring(5, 2));
                                if (clickedItem.CheckOnClick == true)
                                {
                                    int selec = GlobVar.grafSelected.IndexOf(index);
                                    filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                                    if (clickedItem.Text.Equals("Nenhum"))
                                    {
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = DBNull.Value;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                        LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                            hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(2, 1));
                                            hertzSelect /= 10;
                                        }
                                        else
                                        {
                                            hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                        }
                                        GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"] = hertzSelect;
                                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                            LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectL, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

                                        openglControl1.DoRender();
                                        plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                    }
                                }
                            }
                        }
                        if (GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] != DBNull.Value)
                        {
                        float NotchHertz = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"]);
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))),
                                NotchHertz,
                                10,
                                GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                            openglControl1.DoRender();
                            plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);

                        }
                    TelaClearAndReload();

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
                            if (value.Equals("NenhumLowGl"))
                            {
                                hertzSelectLow = 0;

                            }
                            else if (value.Equals("OutroLowGl"))
                            {
                                hertzSelectLow = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaBaixa"]);

                            }
                            else
                            {
                                hertzSelectLow = Convert.ToInt32(value.Substring(5, 2));

                            }

                        } //Verifica se tem algum filtro aplicado de LowPass
                        if (isAnyFilterSelectedHigh)
                        {
                            string value = GetSelectedFilterItem(panel, filterStatesHigh);
                            if (value.Equals("NenhumHighGl"))
                            {
                                hertzSelectHigh = 0;

                            }
                            else if (value.Equals("outroHighGl"))
                            {
                                hertzSelectHigh = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["PassaAlta"]);
                            }
                            else
                            {
                                if (value.Substring(6, 1).IsEqual("H"))
                                {
                                    hertzSelectHigh = Convert.ToInt32(value.Substring(5, 1));
                                }
                                else if (value.Substring(5, 1).IsEqual("0"))
                                {
                                    hertzSelectHigh = Convert.ToInt32(value.Substring(5, 2));
                                    hertzSelectHigh = hertzSelectHigh / 10;
                                }
                                else
                                {
                                    hertzSelectHigh = Convert.ToInt32(value.Substring(5, 2));
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
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectLow, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));


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
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));


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
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelectLow, (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));

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
                                                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                                LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
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
                                    float hertzSelect = Convert.ToInt32(clickedItem.Text.Substring(0, 3));
                                    GlobVar.tbl_MontagemSelecionada.Rows[index]["Notch"] = hertzSelect;
                                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])),
                                    LeituraEmMatrizTeste.ShortToFloat(Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[index]["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[index])])));
                                    openglControl1.DoRender();
                                    plotagem.DesenhaGrafico((int)openglControl1.Height, qtdGrafics);
                                }
                            }

                        }
                    TelaClearAndReload();
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
        public static int index = 0;
        private void timerClick_Tick(object sender, EventArgs e)
        {
            if (isDrawing)
            {
                if(Cursor.Position.X >= (openglControl1.Width + openglControl1.Location.X))
                {
                    Cursor.Position = new Point(openglControl1.Width + openglControl1.Location.X - 1, Cursor.Position.Y);
                    Cursor.Position = new Point(openglControl1.Width + openglControl1.Location.X, Cursor.Position.Y);
                }
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            openglControl1.Update();

            TelaClearAndReload();

            if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && !isThereAComment)
            {
                if (isDrawing)
                {
                    plotEventos.DrawingAnEvent(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);
                }
            }
            else if (!isAnEvent && (!this.isAnStartEvent && !this.isAnEndEvent) && isThereAComment)
            {
                if (isDrawing)
                {
                    plotComentatios.DrawCommentBorder(gl);
                }
            }
            else if (isAnEvent && (!this.isAnStartEvent || !this.isAnEndEvent) && !isThereAComment)
            {
                if (isDrawing)
                {
                    plotEventos.DrawBordenInAnEvent(GlobVar.drawBordenInAnEvent, gl, GlobVar.desenhoLoc);
                }
            }
            else if ((!isAnEvent && this.isAnStartEvent) || (!isAnEvent && this.isAnEndEvent) && !isThereAComment)
            {
                if (isDrawing)
                {
                    plotEventos.DrawBordenInAnEvent(isDrawing, gl, GlobVar.desenhoLoc);
                }
            }
            //plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);

        }
        private void timerComment_Tick(object sender, EventArgs e)
        {
            openglControl1.Update();

            TelaClearAndReload();
            UpdateInicioTela();

            if ((isThereAXSartComment || isThereAXEndComment || isThereAYStartComment || isThereAYEndComment || isThereX0Y0Comment || isThereX0Y1Comment || isThereX1Y0Comment || isThereX1Y1Comment))
            {
                if (isDrawing)
                {
                    plotComentatios.DrawCommentBorder(gl);
                }
            }
            //plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, gl, GlobVar.desenhoLoc);

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            int? rowIndex = null;
            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                if (GlobVar.tbl_MontagemSelecionada.Rows[i].Field<int>("CodCanal1") == tagCodCanal)
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
            GlobVar.CodCanal = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[YAdjusted]["CodCanal1"]);

            /*Stringao.Text = $"Timer1: {cronometro1.Elapsed.ToString()} | Timer3: {cronometro3.Elapsed.ToString()} | TimerGeral: {cronometro4.Elapsed.ToString()} " +
                $"| TimerBand: {cronometroBand.Elapsed.ToString()} | TimerBaixa: {cronometroBaixa.Elapsed.ToString()} | TimerAlta: {cronometroAlta.Elapsed.ToString()} | TimerNotch: {cronometroNotch.Elapsed.ToString()} " +
                $"|  concluido: {concluido} | Tecla: {tecla} | Bd: {isA_BN_CPAP_BD}| Contador: {clickCount} | XiYi: {GlobVar.XiYi} " +
                $"| XfYf: {GlobVar.XfYf} | X0Y0: {isThereX0Y0Comment}  | X0Y1: {isThereX0Y1Comment} | X1Y0: {isThereX1Y0Comment} | X1Y1: {isThereX1Y1Comment}| EUmComentario: {isThereAComment}";*/
        }
    }
}


