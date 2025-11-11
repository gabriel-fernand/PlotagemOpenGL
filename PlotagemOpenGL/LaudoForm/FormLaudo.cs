using Accord.Math;
using Accord.Math.Geometry;
using Accord.Statistics;
using Cyotek.Windows.Forms;
using Google.Protobuf.WellKnownTypes;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.auxi.FormsAuxi;
using PlotagemOpenGL.BD;
using PlotagemOpenGL.Filtros;
using PlotagemOpenGL.Hipnograma;
using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow.Operations.Losses;

namespace PlotagemOpenGL.LaudoForm
{
    public partial class FormLaudo : Form
    {
        public static Rectangle recgl;
        private System.Drawing.Size formOriginalSize;
        public static int codGrupMouse;
        public static DataTable MontagemJanela;
        public static OpenGL gl;
        public static int codJanela;
        public static int codJanela_Click = -1;
        public static bool mouseClicked = false;
        public static int areaSelectpZero;
        public static int areaSelectTop;
        public static int marg;
        public static float Porcentagem;
        public static int NeEmarg;
        public static float NeWPorcentagem;
        public static int tempo_ronco = 0;
        public static int[] pontoZero;
        public static int[] pontoTop;

        public static float[] porc;

        public static int[,] eventosResp; //Evento cod 1
        public static int[] Despertar; // Evento    cod 3
        public static int[,] Cardio; // Evento     cod 4
        public static int[] plm; // Evento          cod 5
        public static int[] ronco; // Evento        cod 10
        public static int[,] Bruxismo; // Evento   cod 40

        public static int[] SA02; //                cod 6
        public static int[] FreqCard; //            cod 12
        public static int[] Microfone; //           cod 19

        public static int[] CPAP; //                cod 11
        public static int[] CPAPVaz; //             cod 18
        public static int[] CO2_Exal; //            cod 39

        public static int[] posicao; //           cod 7
        public static int[] estagio; //           cod 9
        public static int[] locyEstagio;

        public static bool Horario; //            cod 21
        public static DataTable horario;
        public static int pagAtual;
        public static bool openned = false;

        // Apneias
        public static EventoResumo ev_ap = new EventoResumo();
        public static EventoResumo ev_ap_obs = new EventoResumo();
        public static EventoResumo ev_ap_cen = new EventoResumo();
        public static EventoResumo ev_ap_mis = new EventoResumo();

        // Hipopneias
        public static EventoResumo ev_hipop = new EventoResumo();
        public static EventoResumo ev_hipop_obs = new EventoResumo();
        public static EventoResumo ev_hipop_cen = new EventoResumo();
        public static EventoResumo ev_hipop_mis = new EventoResumo();

        //Dessaturacao
        public static EventoResumo ev_dessat = new EventoResumo();

        //Despertar
        public static EventoResumo ev_desp = new EventoResumo();

        //Bruxismo
        public static EventoResumo ev_brux_fas = new EventoResumo();

        //Ronco
        public static EventoResumo ev_ronco = new EventoResumo();

        //Perna
        public static EventoResumo ev_plm = new EventoResumo();
        public static EventoResumo ev_mov_perna = new EventoResumo();

        // RERA
        public static EventoResumo ev_rera = new EventoResumo();
        public static System.Collections.Generic.Dictionary<int, CPAPRelat> cpapRelatDict = new System.Collections.Generic.Dictionary<int, CPAPRelat>();
        public static Dictionary<int, Dictionary<int, BPAPRelat>> g_BPAP_Relat = new Dictionary<int, Dictionary<int, BPAPRelat>>();

        public static NapResumo[] g_naps = new NapResumo[5];

        public static DataTable tbl_HipnoLaudo = new DataTable();

        private static TaskCompletionSource<bool> tcsTabIndexChanged;

        public static bool ExameTemVideo = false;

        public static double CPAP_Min = 0;
        public static double CPAP_Max = 0;

        public static int qtd_Desp_com_dessat;
        public static string texto2 = "";
        // Apneia e Hipopnéia com Dessaturação
        public static int qtd_ap_cen_com_dessat;
        public static int qtd_ap_obs_com_dessat;
        public static int qtd_ap_mis_com_dessat;
        public static int qtd_hipop_com_dessat;

        // Com microdespertar
        public static int qtd_ap_cen_com_mdesp;
        public static int qtd_ap_obs_com_mdesp;
        public static int qtd_ap_mis_com_mdesp;
        public static int qtd_hipop_com_mdesp;

        // Com Dessaturação e Microdespertar
        public static int qtd_ap_cen_com_dessat_e_mdesp;
        public static int qtd_ap_obs_com_dessat_e_mdesp;
        public static int qtd_ap_mis_com_dessat_e_mdesp;
        public static int qtd_hipop_com_dessat_e_mdesp;

        // RERA
        public static int qtd_RERA_com_dessat;
        public static int qtd_RERA_com_mdesp;
        public static int qtd_RERA_com_dessat_e_mdesp;

        // PLM
        public static int qtd_PLM_com_mdesp;

        public FormLaudo()
        {

            // Obtém as dimensões da tela principal
            int larguraTela = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int alturaTela = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            InitializeComponent();
            VerificaQtdPosicoes();


            foreach (DataRow roow in GlobVar.tbl_JanelaResumo.Rows)
            {
                HipnoMostrando.Items.Add(roow["DescrJanela"].ToString());
            }
            HipnoMostrando.SelectedIndex = 0;
            var drw = GlobVar.tbl_JanelaResumo.AsEnumerable()
                      .FirstOrDefault(row => row.Field<string>("DescrJanela").Equals(HipnoMostrando.Text));

            if (drw != null)
            {
                codJanela = Convert.ToInt32(drw["CodJanela"]);
            }
            openned = true;

            int calculo0 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 0) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 0).Count() : 0;
            int calculo1 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 1) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 1).Count() : 0;
            int calculo2 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 2) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 2).Count() : 0;
            int calculo3 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 3) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 3).Count() : 0;
            int calculo4 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 4) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 4).Count() : 0;
            int calculo5 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 5) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 5).Count() : 0;
            int calculo6 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 6) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 6).Count() : 0;
            int calculo7 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 7) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 7).Count() : 0;
            int calculo8 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 8) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 8).Count() : 0;
            int calculo9 = GlobVar.tbl_Paginas.AsEnumerable().Any(rw => rw.Field<int>("Estagio") == 9) ? GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("Estagio") == 9).Count() : 0;


            GlobVar.tbl_ResumoExame.Rows[0]["Est_0"] = calculo0;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_1"] = calculo1;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_2"] = calculo2;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_3"] = calculo3;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_4"] = calculo4;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_5"] = calculo5;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_6"] = calculo6;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_7"] = calculo7;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_8"] = calculo8;
            GlobVar.tbl_ResumoExame.Rows[0]["Est_9"] = calculo9;

            GlobVar.tbl_ResumoExame.Rows[0]["Pos_C"] = GlobVar.Pos_C;
            GlobVar.tbl_ResumoExame.Rows[0]["Pos_D"] = GlobVar.Pos_D;
            GlobVar.tbl_ResumoExame.Rows[0]["Pos_E"] = GlobVar.Pos_E;
            GlobVar.tbl_ResumoExame.Rows[0]["Pos_B"] = GlobVar.Pos_B;

            GlobVar.tbl_ResumoExame.Rows[0]["Lat_E1"] = GlobVar.tbl_ResumoExame.Rows[0]["Lat_E1"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_E1"];
            GlobVar.tbl_ResumoExame.Rows[0]["Lat_E2"] = GlobVar.tbl_ResumoExame.Rows[0]["Lat_E2"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_E2"];
            GlobVar.tbl_ResumoExame.Rows[0]["Lat_E3"] = GlobVar.tbl_ResumoExame.Rows[0]["Lat_E3"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_E3"];
            GlobVar.tbl_ResumoExame.Rows[0]["Lat_E4"] = GlobVar.tbl_ResumoExame.Rows[0]["Lat_E4"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_E4"];


            CalcularDadosFrequenciaCardiaca();
            AjustarDTJanela();
            PreparaOsArrays();
            gl = openglHipno.OpenGL;
            try {
            Desenha();
            }
            catch
            {
                MessageBox.Show(
                        "O Exame está com estágio diferente do padrão para o tipo de exame, por favor ajuste.",
                        "Erro de Estágio",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
            }
            formOriginalSize = this.Size;
            recgl = new Rectangle(openglHipno.Location, openglHipno.Size);
            this.Resize += resiz;
            buttonForm = new ButtonForm();
            pagAtual = GlobVar.indice / GlobVar.namos;
            var rw = GlobVar.tbl_JanelaResumo.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodJanela") == codJanela);

            this.Text = "Laudo e Relatório de Polissonografia";
            comentarios();
            CarregarArquivosNoComboBox();
            CalculaResumoMultiplaLatencia();
        }
        private void VerificaQtdPosicoes()
        {
            GlobVar.minPosi = new int[GlobVar.matrizCanal.GetLength(1)];

            int linhaSaturacao = GlobVar.codSelected.IndexOf(14);



            for (int i = 0; i < GlobVar.matrizCanal.GetLength(1) && i < GlobVar.minPosi.Length && i < GlobVar.matrizCanal.GetLength(1); i++)
            {
                GlobVar.minPosi[i] = Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]);
                if (Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) <= (21502 - 2110) && Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) >= (21502 + 2110)) // CIMA
                {
                    GlobVar.Pos_C++;
                }
                else if (Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) <= (-4070 - 2110) && Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) >= (-4070 + 2110)) // DIREITA
                {
                    GlobVar.Pos_D++;
                }
                else if (Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) <= (-16887 - 2110) && Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) >= (-16887 + 2110)) //BAIXO
                {
                    GlobVar.Pos_B++;
                }
                else if (Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) <= (-14031 - 2110) && Convert.ToInt32(GlobVar.matrizCanal[linhaSaturacao, i]) >= (-14031 + 2110)) // ESQUERDA
                {
                    GlobVar.Pos_E++;
                }
                else
                {
                    GlobVar.Pos_C++;

                }
                i += 7;
            }

        }
        private void CarregarArquivosNoComboBox()
        {
            string diretorio = Path.Combine(GlobVar.basePath, "Laudos");

            if (Directory.Exists(diretorio))
            {
                string[] arquivos = Directory.GetFiles(diretorio, "*.doc"); // Busca arquivos .doc

                comboBox1.Items.Clear(); // Limpa o ComboBox
                foreach (string arquivo in arquivos)
                {
                    comboBox1.Items.Add(Path.GetFileNameWithoutExtension(arquivo)); // Adiciona apenas o nome do arquivo
                }
            }
            else
            {
                MessageBox.Show("O diretório não existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string diretorio = Path.Combine(GlobVar.basePath, "Laudos");
            string nomeSelecionado = comboBox1.SelectedItem.ToString();


            string caminhoCompleto = Path.Combine(diretorio, nomeSelecionado + ".doc");

        }
        public static Panel pnl_Loading;
        public static Label lbl_Carregando;
        public static ProgressBar barraLoading;

        private void CriarPainelLoading()
        {
            // Painel de loading
            pnl_Loading = new Panel
            {
                Width = 300,
                Height = 100,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(245, 245, 255), // azul claro
                Visible = false
            };

            // Centralizar na tela
            pnl_Loading.Left = (this.ClientSize.Width - pnl_Loading.Width) / 2;
            pnl_Loading.Top = (this.ClientSize.Height - pnl_Loading.Height) / 2;

            // Label de carregamento
            lbl_Carregando = new Label
            {
                Text = "Loading Form   0 %",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = pnl_Loading.Width,
                Height = 30,
                Top = 10
            };

            // Barra de progresso com margem
            barraLoading = new ProgressBar
            {
                Style = ProgressBarStyle.Continuous,
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Width = pnl_Loading.Width - 40, // margem lateral de 20px
                Height = 18,
                Left = 20,
                Top = lbl_Carregando.Bottom + 15,
                ForeColor = Color.MediumPurple
            };

            // Adiciona ao painel
            pnl_Loading.Controls.Add(lbl_Carregando);
            pnl_Loading.Controls.Add(barraLoading);

            // Adiciona ao formulário
            Laudo.Controls.Add(pnl_Loading);
        }

        private void comentarios()
        {
            string coments = "";
            string observacao = "Observação:\n" + GlobVar.tbl_DadosExame.Rows[0]["Observacao"].ToString();
            string nextline = "\n\n";
            richTextBox1.Text += observacao;
            richTextBox1.Text += nextline;

            string horarioBn = "";
            var boanoite = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 18).FirstOrDefault();
            if (boanoite != null)
            {
                int numpag = Convert.ToInt32(boanoite["NumPag"]);
                var rwpg = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == numpag).FirstOrDefault();
                DateTime dataHora = rwpg.Field<DateTime>("Horario");
                string horario = dataHora.ToString("HH:mm:ss");
                horarioBn = horario + " <<Boa Noite>>";
                richTextBox1.Text += horarioBn;
                richTextBox1.Text += nextline;
            }
            foreach (DataRow rw in GlobVar.tbl_Comentarios.Rows)
            {
                int numpag = Convert.ToInt32(rw["NumPag"]);
                var rwpg = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == numpag).FirstOrDefault();
                DateTime dataHora = rwpg.Field<DateTime>("Horario");
                string horario = dataHora.ToString("HH:mm:ss");

                string coment = rw["Comentario"].ToString();

                richTextBox1.Text += horario + " - " + coment;
                richTextBox1.Text += nextline;
            }
            string horarioBd = "";
            var badia = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 19).FirstOrDefault();
            if (badia != null)
            {
                int numpag = Convert.ToInt32(badia["NumPag"]);
                var rwpg = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == numpag).FirstOrDefault();
                DateTime dataHora = rwpg.Field<DateTime>("Horario");
                string horario = dataHora.ToString("HH:mm:ss");
                horarioBd = horario + " <<Bom Dia>>";
                richTextBox1.Text += horarioBd;
                richTextBox1.Text += nextline;
            }

        }
        private void HipnoMostrando_TabIndexChanged(object sender, System.EventArgs e)
        {
            if (openned)
            {
                var drw = GlobVar.tbl_JanelaResumo.AsEnumerable()
                          .FirstOrDefault(row => row.Field<string>("DescrJanela").Equals(HipnoMostrando.Text));
                if (drw != null)
                {
                    codJanela = Convert.ToInt32(drw["CodJanela"]);
                }
                AjustarDTJanela();
                PreparaOsArrays();
                Desenha();
                tcsTabIndexChanged?.TrySetResult(true);
            }
        }

        private void resize_Control(Control c, Rectangle r)
        {
            // Define o novo tamanho e posição com base no tamanho atual do formulário
            c.Location = new Point(0, 0); // Mantém o controle ancorado no canto superior esquerdo
            c.Size = new Size(this.ClientSize.Width, this.ClientSize.Height); // Ajusta ao tamanho interno do formulário
        }
        public void resiz(object sender, EventArgs e)
        {
            // Redimensiona o controle openglHipno
            resize_Control(openglHipno, recgl);

            // Redesenha o conteúdo para ajustar ao novo tamanho
            Desenha();
        }
        public static void AjustarDTJanela()
        {
            if (MontagemJanela != null)
            {
                MontagemJanela.Dispose();
            }

            MontagemJanela = new DataTable();

            MontagemJanela = GlobVar.tbl_JanelaResumoItens.AsEnumerable().Where(row => row.Field<int>("CodJanela") == codJanela).OrderBy(row => row.Field<int>("Ordem")).CopyToDataTable();

            //MontagemJanela.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem"));
            reajustaPorc();
        }
        public static void reajustaPorc()
        {
            porc = new float[MontagemJanela.Rows.Count];
            pontoZero = new int[MontagemJanela.Rows.Count];
            pontoTop = new int[MontagemJanela.Rows.Count];

            int i = 0;
            float totalPorc = 0;

            // Preenche o array de porcentagens e calcula o total
            foreach (DataRow rw in MontagemJanela.Rows)
            {
                porc[i] = Convert.ToSingle(rw["Porc"]);
                totalPorc += porc[i];
                i++;
            }

            // Ajusta os valores para garantir que a soma seja 100%
            if (totalPorc != 100)
            {
                for (i = 0; i < porc.Length; i++)
                {
                    porc[i] = (porc[i] / totalPorc) * 100; // Regra de três para ajustar as porcentagens
                }
            }

            // Atualiza os valores no DataTable
            i = 0;
            foreach (DataRow rw in MontagemJanela.Rows)
            {
                rw["Porc"] = porc[i];
                i++;
            }

        }
        public static void PreparaOsArrays()
        {
            int tamanho = GlobVar.matrizCanal.GetLength(1) / GlobVar.namos;
            int inicio = 0;
            int codcanal;
            int codindex;
            int[] media;
            int rw = 0;
            int h;
            DataTable subGrupos = new DataTable();
            int tamanhoa = GlobVar.matrizCanal.GetLength(1) / GlobVar.namos;
            Porcentagem = (float)(tamanhoa * 1.05);
            int margem = Math.Abs((int)Porcentagem - tamanhoa);
            marg = margem;
            NeEmarg = marg;
            NeWPorcentagem = Porcentagem;
            pontoZero = new int[MontagemJanela.Rows.Count];
            pontoTop = new int[MontagemJanela.Rows.Count];

            foreach (DataRow row in MontagemJanela.Rows)
            {
                int codGrupo = Convert.ToInt32(row["CodGrupo"]);

                switch (codGrupo)
                {
                    // ------- Do tipo Evento --------
                    // Respiratorio
                    case 1:
                        subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                    .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                    .OrderByDescending(row => row.Field<int>("Evento"))
                                    .CopyToDataTable();


                        eventosResp = new int[tamanho, subGrupos.Rows.Count];

                        // Preenche a matriz com zeros
                        for (int i = 0; i < tamanho; i++)
                        {
                            for (int j = 0; j < subGrupos.Rows.Count; j++)
                            {
                                eventosResp[i, j] = 0;
                            }
                        }

                        rw = 0;
                        if (subGrupos != null && subGrupos.Rows.Count > 0)
                        {
                            foreach (DataRow subRow in subGrupos.Rows)
                            {
                                int codEvento = Convert.ToInt32(subRow["Evento"]);
                                DataTable EventosSub;

                                // Verifica se há resultados antes de chamar CopyToDataTable
                                var query = GlobVar.eventos.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == codEvento);

                                if (query.Any())
                                {
                                    EventosSub = query.CopyToDataTable();
                                }
                                else
                                {
                                    EventosSub = new DataTable(); // DataTable vazio
                                }

                                if (EventosSub.Rows.Count > 0)
                                {
                                    foreach (DataRow eventRow in EventosSub.Rows)
                                    {
                                        eventosResp[Convert.ToInt32(eventRow["NumPag"]), rw] = 1;
                                    }
                                }

                                rw++;
                            }
                        }

                        subGrupos.Dispose();
                        break;
                    // Despertar
                    case 3:
                        subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                    .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                    .CopyToDataTable();
                        subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                        Despertar = new int[tamanho];
                        Array.Clear(Despertar, 0, Despertar.Length);

                        if (subGrupos != null && subGrupos.Rows.Count > 0)
                        {
                            foreach (DataRow subRow in subGrupos.Rows)
                            {
                                int codEvento = Convert.ToInt32(subRow["Evento"]);
                                var query = GlobVar.eventos.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == codEvento);

                                DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                                if (EventosSub.Rows.Count > 0)
                                {
                                    foreach (DataRow eventRow in EventosSub.Rows)
                                    {
                                        int index = Convert.ToInt32(eventRow["NumPag"]);
                                        Despertar[index] = 1;
                                    }
                                }
                            }
                        }
                        subGrupos.Dispose();
                        break;
                    // Cardio
                    case 4:
                        subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                    .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                    .CopyToDataTable();
                        subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                        Cardio = new int[tamanho, subGrupos.Rows.Count];
                        for (int i = 0; i < tamanho; i++)
                        {
                            for (int j = 0; j < subGrupos.Rows.Count; j++)
                            {
                                Cardio[i, j] = 0;
                            }
                        }

                        rw = 0;
                        if (subGrupos != null && subGrupos.Rows.Count > 0)
                        {
                            foreach (DataRow subRow in subGrupos.Rows)
                            {
                                int codEvento = Convert.ToInt32(subRow["Evento"]);
                                var query = GlobVar.eventos.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == codEvento);

                                DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                                if (EventosSub.Rows.Count > 0)
                                {
                                    foreach (DataRow eventRow in EventosSub.Rows)
                                    {
                                        Cardio[Convert.ToInt32(eventRow["NumPag"]), rw] = 1;
                                    }
                                }
                                rw++;
                            }
                        }
                        subGrupos.Dispose();
                        break;
                    // PLM
                    case 5:
                        subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                    .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                    .CopyToDataTable();
                        subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                        plm = new int[tamanho];
                        Array.Clear(plm, 0, plm.Length);

                        if (subGrupos != null && subGrupos.Rows.Count > 0)
                        {
                            foreach (DataRow subRow in subGrupos.Rows)
                            {
                                int codEvento = Convert.ToInt32(subRow["Evento"]);
                                var query = GlobVar.eventos.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == codEvento);

                                DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                                if (EventosSub.Rows.Count > 0)
                                {
                                    foreach (DataRow eventRow in EventosSub.Rows)
                                    {
                                        plm[Convert.ToInt32(eventRow["NumPag"])] = 1;
                                    }
                                }
                            }
                        }
                        subGrupos.Dispose();
                        break;
                    // Ronco
                    case 10:
                        subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                    .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                    .CopyToDataTable();
                        subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                        ronco = new int[tamanho];
                        Array.Clear(ronco, 0, ronco.Length);

                        if (subGrupos != null && subGrupos.Rows.Count > 0)
                        {
                            foreach (DataRow subRow in subGrupos.Rows)
                            {
                                int codEvento = Convert.ToInt32(subRow["Evento"]);
                                var query = GlobVar.eventos.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == codEvento);

                                DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                                if (EventosSub.Rows.Count > 0)
                                {
                                    foreach (DataRow eventRow in EventosSub.Rows)
                                    {
                                        ronco[Convert.ToInt32(eventRow["NumPag"])] = 1;
                                    }
                                }
                            }
                        }
                        subGrupos.Dispose();
                        break;
                    // Bruxismo
                    case 40:
                        subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                    .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                    .CopyToDataTable();
                        subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                        Bruxismo = new int[tamanho, subGrupos.Rows.Count];
                        for (int i = 0; i < tamanho; i++)
                        {
                            for (int j = 0; j < subGrupos.Rows.Count; j++)
                            {
                                Bruxismo[i, j] = 0;
                            }
                        }

                        rw = 0;
                        if (subGrupos != null && subGrupos.Rows.Count > 0)
                        {
                            foreach (DataRow subRow in subGrupos.Rows)
                            {
                                int codEvento = Convert.ToInt32(subRow["Evento"]);
                                var query = GlobVar.eventos.AsEnumerable()
                                            .Where(row => row.Field<int>("CodEvento") == codEvento);

                                DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                                if (EventosSub.Rows.Count > 0)
                                {
                                    foreach (DataRow eventRow in EventosSub.Rows)
                                    {
                                        Bruxismo[Convert.ToInt32(eventRow["NumPag"]), rw] = 1;
                                    }
                                }
                                rw++;
                            }
                        }
                        subGrupos.Dispose();
                        break;

                    // ------- Sinais graafio -------
                    // SA02
                    case 6:
                        SA02 = new int[tamanho];
                        codcanal = 66;
                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        // Verifica se o índice existe
                        if (codindex >= 0 && codindex < GlobVar.grafSelected.Length)
                        {
                            // Loop para acumular valores
                            h = 0;
                            for (int g = 0; g < GlobVar.matrizCanal.GetLength(1); g += GlobVar.namosNumerico)
                            {
                                if (h < tamanho)
                                {
                                    // Acumula os valores correspondentes
                                    SA02[h] = (int)(int)GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
                                }
                                h++;
                            }
                        }
                        break;
                    // Freq Card
                    case 12:
                        FreqCard = new int[tamanho];
                        codcanal = 67;
                        codindex = GlobVar.codSelected.IndexOf(codcanal);

                        h = 0;
                        for (int g = 0; g < GlobVar.matrizCanal.GetLength(1);)
                        {
                            if (h < tamanho)
                            {
                                FreqCard[h] = (int)GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
                            }
                            h++;
                            g += GlobVar.namosNumerico;
                        }
                        break;
                    // Microfone
                    case 19:
                        Microfone = new int[tamanho];
                        codcanal = 5;
                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        media = new int[512];
                        // Aplica o filtro band-pass nos dados
                        //float[] linhaFiltrada = (GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex]));
                        float[] linhaFiltrada = BandPass.ApplyFilter((GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex])), 40f, 120f, 512);
                        //linhaFiltrada = PaissaBaixa.ApplyFilter(linhaFiltrada, 40f, 1);

                        double scala = GlobVar.scale[GlobVar.grafSelected[codindex]];
                        h = 0; // Índice para o array Microfone
                        for (int g = 0; g < GlobVar.matrizCanal.GetLength(1);)
                        {
                            for (int a = 0; a < media.Length && g < GlobVar.matrizCanal.GetLength(1); a++)
                            {
                                // Garante que não ultrapasse os limites da matriz
                                int valor = (int)(linhaFiltrada[g]);

                                // Trata o caso de int.MinValue
                                if (valor == int.MinValue)
                                {
                                    media[a] = int.MaxValue; // Substitui por int.MaxValue ou outro valor adequado
                                }
                                else
                                {
                                    media[a] = Math.Abs(valor);
                                }
                                g++;
                            }

                            // Verifica se existem valores válidos em 'media' antes de calcular a mediana
                            if (media.Length > 0)
                            {
                                Microfone[h] = Convert.ToInt32(media.Median());
                            }
                            h++;
                        }
                        //Microfone = LeituraEmMatrizTeste.FloatToInt(BandPass.ApplyFilter(LeituraEmMatrizTeste.IntToFloat(Microfone), 40f, 120f, 1));

                        break;

                    // CPAP e Familia
                    case 11:
                        CPAP = new int[tamanho];
                        codcanal = 65;
                        int canalIndex = 0;
                        int LimiteInferior = 0;
                        int LimiteSuperior = 0;

                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        if (codindex != -1)
                        {
                            canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                        }
                        else
                        {
                            var rwcp = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                        .Where(row => row.Field<int>("CodTipoCanal") == 15)
                                        .FirstOrDefault(); // Pega a primeira linha correspondente
                            if (rwcp == null)
                            {
                                break;
                            }


                            codcanal = Convert.ToInt32(rwcp["CodCanal1"]);

                            codindex = GlobVar.codSelected.IndexOf(codcanal);
                            canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                            LimiteInferior = Convert.ToInt32(rwcp["LimiteInferior"]);
                            LimiteSuperior = Convert.ToInt32(rwcp["LimiteSuperior"]);

                        }

                        int ponteiroI = GlobVar.ponteiroI[canalIndex];
                        int ponteiroF = GlobVar.ponteiroF[canalIndex];

                        int indexx = GlobVar.codCanal.IndexOf(codcanal);
                        int Taxa = GlobVar.txPorCanal[indexx];
                        int aoh = 0;
                        h = 0;
                        // Caso sem segundo canal
                        for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroI;
                            while (colunaComp < ponteiroF)
                            {
                                CPAP[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                colunaComp += Taxa;
                                h++;
                            }
                        }

                        if (codcanal != 65)
                        {
                            var dataToFilter = CPAP;
                            int lmAnaloInf = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Anal"]);
                            int lmAnaloSup = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Anal"]);

                            dataToFilter = DigiToAnalo(CPAP, LimiteInferior, LimiteSuperior, lmAnaloInf, lmAnaloSup);

                            Array.Copy(dataToFilter, 0, CPAP, 0, dataToFilter.Length);

                        }

                        break;

                    case 18:
                        CPAPVaz = new int[tamanho];
                        int canalIndexvz = 0;
                        int LimiteInferiorvz = 0;
                        int LimiteSuperiorvz = 0;

                        var rwvz = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                    .Where(row => row.Field<int>("CodTipoCanal") == 28)
                                    .FirstOrDefault(); // Pega a primeira linha correspondente
                        if (rwvz == null)
                        {
                            break;
                        }
                        codcanal = Convert.ToInt32(rwvz["CodCanal1"]);

                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                        LimiteInferiorvz = Convert.ToInt32(rwvz["LimiteInferior"]);
                        LimiteSuperiorvz = Convert.ToInt32(rwvz["LimiteSuperior"]);

                        ponteiroI = GlobVar.ponteiroI[canalIndex];
                        ponteiroF = GlobVar.ponteiroF[canalIndex];

                        indexx = GlobVar.codCanal.IndexOf(codcanal);
                        Taxa = GlobVar.txPorCanal[indexx];
                        aoh = 0;
                        h = 0;

                        for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroI;
                            while (colunaComp < ponteiroF)
                            {
                                CPAPVaz[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                colunaComp += Taxa;
                                h++;
                            }
                        }
                        var dataToFiltervz = CPAPVaz;
                        int lmAnaloInfvz = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Anal"]);
                        int lmAnaloSupvz = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Anal"]);

                        dataToFiltervz = DigiToAnalo(CPAPVaz, LimiteInferiorvz, LimiteSuperiorvz, lmAnaloInfvz, lmAnaloSupvz);

                        Array.Copy(dataToFiltervz, 0, CPAPVaz, 0, dataToFiltervz.Length);

                        break;

                    case 39:
                        CO2_Exal = new int[tamanho];
                        codcanal = 73;

                        int canalindexC = 0;
                        int LimiteInferiorC = 0;
                        int LimiteSuperiorC = 0;
                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        if (codindex != -1)
                        {
                            canalindexC = GlobVar.codCanal.IndexOf(codcanal);
                        }
                        else
                        {
                            var rwcp = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                        .Where(row => row.Field<int>("CodTipoCanal") == 38)
                                        .FirstOrDefault(); // Pega a primeira linha correspondente
                            if (rwcp == null)
                            {
                                break;
                            }
                            codcanal = Convert.ToInt32(rwcp["CodCanal1"]);

                            codindex = GlobVar.codSelected.IndexOf(codcanal);
                            canalindexC = GlobVar.codCanal.IndexOf(codcanal);
                            LimiteInferiorC = Convert.ToInt32(rwcp["LimiteInferior"]);
                            LimiteSuperiorC = Convert.ToInt32(rwcp["LimiteSuperior"]);

                        }

                        int ponteiroIC = GlobVar.ponteiroI[canalindexC];
                        int ponteiroFC = GlobVar.ponteiroF[canalindexC];

                        int indexxC = GlobVar.codCanal.IndexOf(codcanal);
                        int TaxaC = GlobVar.txPorCanal[indexxC];
                        int aoC = 0;
                        h = 0;

                        for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                        {
                            int colunaComp = ponteiroIC;
                            while (colunaComp < ponteiroFC)
                            {
                                CO2_Exal[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                                colunaComp += TaxaC;
                                h++;
                            }
                        }
                        //Parte para conversar de dig para analo se precisar
                        if (codcanal != 73)
                        {
                            var dataToFilter = CO2_Exal;
                            int lmAnaloInf = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteInf_Anal"]);
                            int lmAnaloSup = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteSup_Anal"]);

                            dataToFilter = DigiToAnalo(CO2_Exal, LimiteInferiorC, LimiteSuperiorC, lmAnaloInf, lmAnaloSup);

                            Array.Copy(dataToFilter, 0, CO2_Exal, 0, dataToFilter.Length);

                        }

                        break;
                    // ------- Posi / Estagio -------
                    // Posicao
                    case 7:
                        posicao = new int[tamanho];
                        codcanal = 14;
                        codindex = GlobVar.codSelected.IndexOf(codcanal);

                        h = 0;
                        for (int g = 0; g < GlobVar.matrizCanal.GetLength(1);)
                        {
                            if (h < tamanho)
                            {
                                posicao[h] += (int)(GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g] * -1);
                            }
                            h++;
                            g += GlobVar.namosNumerico;
                        }
                        for (int aq = 0; aq < posicao.Length; aq++)
                        {
                            if (posicao[aq] >= (GlobVar.PosCima - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosCima + GlobVar.PosIncremento)) // CIMA
                            {
                                posicao[aq] = 3;
                            }
                            else if (posicao[aq] >= (GlobVar.PosDireita - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosDireita + GlobVar.PosIncremento)) // DIREITA
                            {
                                posicao[aq] = 2;
                            }
                            else if (posicao[aq] >= (GlobVar.PosEsquerda - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosEsquerda + GlobVar.PosIncremento)) // ESQUERDA
                            {
                                posicao[aq] = 1;
                            }
                            else if (posicao[aq] >= (GlobVar.PosBaixo - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosBaixo + GlobVar.PosIncremento)) //BAIXO
                            {
                                posicao[aq] = 0;
                            }
                            else
                            {
                                posicao[aq] = 3;
                            }
                        }
                        break;
                    // Estagios
                    case 9:
                        estagio = new int[tamanho];

                        h = 0;
                        foreach (DataRow rowEstagio in GlobVar.tbl_Paginas.Rows)
                        {
                            if (h < tamanho)
                            {
                                estagio[h] = Convert.ToInt32(rowEstagio["Estagio"]);
                            }
                            h++;
                        }
                        break;

                    // Horario
                    case 21:
                        Horario = true;

                        // Chama o método para filtrar horários completos
                        DataTable horario = FiltrarHorariosCompletos(GlobVar.tbl_Paginas);

                        // Agora o DataTable 'horario' contém apenas as linhas com horários completos
                        break;
                }
            }
        }
        private bool desenhaMargLine = false;

        public void ConsBnBd_Clic(object sender, EventArgs e)
        {
            if (ConsBnBd.Checked)
            {
                var rowBn = GlobVar.eventos.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == 18);
                int pagBn = Convert.ToInt32(rowBn["NumPag"]);

                var rowBd = GlobVar.eventos.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == 19);
                int pagBd = Convert.ToInt32(rowBd["NumPag"]);

                int tamanhoa = Math.Abs(pagBn - pagBd);
                Porcentagem = (float)(tamanhoa * 1.05);
                int margem = Math.Abs((int)Porcentagem - tamanhoa);
                marg = margem;
                NeEmarg = marg;
                NeWPorcentagem = Porcentagem;
            }
            else
            {
                int tamanhoa = GlobVar.matrizCanal.GetLength(1) / GlobVar.namos;
                Porcentagem = (float)(tamanhoa * 1.05);
                int margem = Math.Abs((int)Porcentagem - tamanhoa);
                marg = margem;
                NeEmarg = marg;
                NeWPorcentagem = Porcentagem;
            }
            Desenha();
        }
        public void Desenha()
        {
            // Defina a cor de fundo com os valores RGB normalizados
            gl.ClearColor(1, 1, 1, 1); // A última variável é o alpha (opacidade), 1.0f para opaco

            // Continue com as configurações normais do OpenGL
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.Viewport(0, 0, (int)FormLaudo.openglHipno.Width, (int)FormLaudo.openglHipno.Height);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Ortho(0, Porcentagem, 0, FormLaudo.openglHipno.Height, -2, 2);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();
            gl.Translate(0, 0, 1);
            gl.PointSize(1.0f);
            gl.Color(0.5f, 0.5f, 0.5f);
            gl.Scale(1, 1, 1);

            // Desenhar a linha em marg se estiver sendo alterada
            if (desenhaMargLine)
            {
                gl.Color(0f, 0f, 0f); // Define a cor das linhas (preto)
                                      // Ativar o estilo de linha pontilhada
                gl.Enable(OpenGL.GL_LINE_STIPPLE);

                // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)

                // Iniciar o desenho da linha
                gl.Begin(OpenGL.GL_LINE_LOOP);
                gl.Vertex(NeEmarg, 0);
                gl.Vertex(NeEmarg, FormLaudo.openglHipno.Height);
                gl.End();
                gl.Flush();
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }
            else
            {
                //----------
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                gl.Begin(OpenGL.GL_LINE_STRIP);
                gl.Vertex(marg, 0);
                gl.Vertex(marg, FormLaudo.openglHipno.Height);
                gl.End();
                gl.Flush();
            }
            //----------
            if (GlobVar.indice / GlobVar.namos != pagAtual)
            {
                pagAtual = GlobVar.indice / GlobVar.namos;
            }

            //---------- ponteiro pag atual
            gl.Enable(OpenGL.GL_LINE_STIPPLE);

            // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
            gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)
            gl.Color(231f, 0f, 0f);

            // Iniciar o desenho da linha
            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.Vertex(marg + pagAtual, 0);
            gl.Vertex(marg + pagAtual, FormLaudo.openglHipno.Height);
            gl.End();
            gl.Flush();
            gl.Disable(OpenGL.GL_LINE_STIPPLE);

            gl.Color(0f, 0f, 0f);

            int espacox = FormLaudo.openglHipno.Height;
            int porcent = FormLaudo.openglHipno.Height;
            int topPorcent = FormLaudo.openglHipno.Height;

            int ant = MontagemJanela.Rows.Count;

            if (mouseClicked && !movendoGraf)
            {
                gl.Color(0.3f, 0.3f, 0.3f); // Define a cor das linhas (preto)
                                            // Ativar o estilo de linha pontilhada
                gl.Enable(OpenGL.GL_LINE_STIPPLE);

                // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)

                // Iniciar o desenho da linha
                gl.Begin(OpenGL.GL_LINE_LOOP);
                gl.Vertex(50, areaSelectpZero);
                gl.Vertex(Porcentagem - 10, areaSelectpZero);
                gl.Vertex(Porcentagem - 10, areaSelectTop);
                gl.Vertex(50, areaSelectTop);
                gl.End();
                gl.Flush();
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }
            if (movendoGraf)
            {
                gl.Color(0.3f, 0.3f, 0.3f); // Define a cor das linhas (preto)
                                            // Ativar o estilo de linha pontilhada
                gl.Enable(OpenGL.GL_LINE_STIPPLE);

                // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)

                // Iniciar o desenho da linha
                gl.Begin(OpenGL.GL_LINE_LOOP);
                gl.Vertex(50, areaSelectpZero);
                gl.Vertex(Porcentagem - 10, areaSelectpZero);
                gl.Vertex(Porcentagem - 10, areaSelectTop);
                gl.Vertex(50, areaSelectTop);
                gl.End();
                gl.Flush();
                gl.Disable(OpenGL.GL_LINE_STIPPLE);
            }

            for (int i = 0; i < MontagemJanela.Rows.Count; i++)
            {

                porcent -= (int)(espacox * (porc[i] / 100));
                pontoZero[i] = porcent;
                pontoTop[i] = topPorcent;

                if (!mouseClicked)
                {
                    gl.Color(0.5f, 0.5f, 0.5f); // Define a cor das linhas (preto)
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    gl.Vertex(0, porcent);
                    gl.Vertex(Porcentagem, porcent);
                    gl.End();
                    gl.Flush();
                }
                ant--;
                gl.Color(0.0f, 0.0f, 0.0f);

                legenda(porcent, topPorcent, Convert.ToInt32(MontagemJanela.Rows[i]["CodGrupo"]), marg, (int)Porcentagem);

                gl.End();
                gl.Flush();
                desenhaGarficos(porcent, topPorcent, Convert.ToInt32(MontagemJanela.Rows[i]["CodGrupo"]), marg, (int)Porcentagem);

                gl.End();
                gl.Flush();

                topPorcent -= (int)(espacox * (porc[i] / 100));
            }
        }
        public void desenhaGarficos(int pontoZero, int topPonto, int codGrupo, int xStart, int xEnd)
        {
            double quasi;
            DataTable dtSubGrupo = new DataTable();
            int tamanho;
            int qt;
            int locLeg;
            int tanhamorisco;
            int index;
            float[] Cor;
            int eusla;
            int cadEvent;
            int pagBn = 0;
            if (ConsBnBd.Checked)
            {
                var rowBn = GlobVar.eventos.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == 18);
                pagBn = Convert.ToInt32(rowBn["NumPag"]);
                // var rowBd = GlobVar.eventos.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == 19);
                // int pagBd = Convert.ToInt32(rowBd["NumPag"]);
                // xEnd = pagBd;
            }

            switch (codGrupo)
            {
                // ------- Do tipo Evento --------
                // Respiratorio
                case 1:
                    eventosRespStrip.Checked = true;
                    eventosRespStrip.Tag = 1;
                    // Calcula tamanho e divisões
                    tamanho = Math.Abs(topPonto - pontoZero);

                    dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                        .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                        .OrderByDescending(row => row.Field<int>("Evento")) // Ordena corretamente
                        .CopyToDataTable();

                    qt = dtSubGrupo.Rows.Count;
                    locLeg = tamanho / qt; // Espaçamento entre legendas
                    tanhamorisco = (int)(locLeg * 0.9f); // Altura do risco (90% do espaçamento)

                    // Começa no ponto inicial
                    int locrisc = pontoZero + locLeg / 2;
                    index = 0;
                    var rwr = MontagemJanela.AsEnumerable()
                                             .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rwr["CorGrafico"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rwr["CorGrafico"]) == 0)
                        {
                            eusla = 0;
                        }
                        else
                        {
                            eusla = 1;
                        }
                    }
                    else { eusla = 0; }
                    Cor = new float[3];

                    foreach (DataRow rw in dtSubGrupo.Rows)
                    {
                        cadEvent = Convert.ToInt32(rw["Evento"]);
                        var aoi = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == cadEvent);

                        // Desenha as riscas correspondentes
                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (eventosResp[j - xStart + pagBn, index] != 0) // Verifica se há evento
                            {
                                if (eusla != 0)
                                {
                                    Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(aoi["CorFundo"]));
                                    gl.Color(Cor[0], Cor[1], Cor[2]);
                                }
                                else { gl.Color(0, 0, 0); }

                                gl.Begin(OpenGL.GL_LINES); // Use GL_LINES para linhas simples
                                gl.Vertex(j, locrisc - tanhamorisco / 2); // Linha começa um pouco acima do texto
                                gl.Vertex(j, locrisc + tanhamorisco / 2); // Linha termina um pouco abaixo do texto
                                gl.End();
                                gl.Flush();
                                gl.Color(0, 0, 0); // Define a cor da linha

                            }
                        }
                        // Move para a próxima legenda
                        locrisc += locLeg;
                        index++;
                    }
                    break;
                // Despertar
                case 3:
                    DespertarStrip.Checked = true;
                    DespertarStrip.Tag = 3;
                    tamanho = Math.Abs(topPonto - pontoZero);

                    dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable().Where(row => row.Field<int>("CodGrupo") == codGrupo).CopyToDataTable();
                    dtSubGrupo.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));
                    qt = dtSubGrupo.Rows.Count;

                    locLeg = tamanho / qt;
                    tanhamorisco = (int)(locLeg * 0.8f);
                    var rwd = MontagemJanela.AsEnumerable()
                         .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rwd["CorGrafico"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rwd["CorGrafico"]) == 0)
                        {
                            eusla = 0;
                        }
                        else
                        {
                            eusla = 1;
                        }
                    }
                    else { eusla = 0; }
                    Cor = new float[3];
                    var aodes = dtSubGrupo.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodGrupo") == codGrupo);
                    cadEvent = Convert.ToInt32(aodes["Evento"]);
                    var aoid = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == cadEvent);

                    for (int i = 0; i < qt; i++)
                    {
                        index = pagBn;

                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (Despertar[index] != 0)
                            {
                                if (eusla != 0)
                                {
                                    Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(aoid["CorFundo"]));
                                    gl.Color(Cor[0], Cor[1], Cor[2]);
                                }
                                else { gl.Color(0, 0, 0); }

                                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                                gl.Vertex(j, pontoZero + tanhamorisco);
                                gl.Vertex(j, topPonto - tanhamorisco);
                                gl.End();
                                gl.Flush();
                                gl.Color(0, 0, 0); // Define a cor da linha

                            }
                            else
                            {

                            }
                            index++;
                        }
                    }
                    break;
                // Cardio
                case 4:
                    CardioStrip.Checked = true;
                    CardioStrip.Tag = 4;

                    // Calcula tamanho e divisões
                    tamanho = Math.Abs(topPonto - pontoZero);

                    dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                        .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                        .OrderByDescending(row => row.Field<int>("Evento")) // Ordena corretamente
                        .CopyToDataTable();

                    qt = dtSubGrupo.Rows.Count;
                    locLeg = tamanho / qt; // Espaçamento entre legendas
                    tanhamorisco = (int)(locLeg * 0.9f); // Altura do risco (90% do espaçamento)

                    // Começa no ponto inicial
                    int locc = pontoZero + locLeg / 2;
                    index = 0;
                    var rwc = MontagemJanela.AsEnumerable()
                                             .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rwc["CorGrafico"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rwc["CorGrafico"]) == 0)
                        {
                            eusla = 0;
                        }
                        else
                        {
                            eusla = 1;
                        }
                    }
                    else { eusla = 0; }
                    Cor = new float[3];

                    foreach (DataRow rw in dtSubGrupo.Rows)
                    {
                        cadEvent = Convert.ToInt32(rw["Evento"]);
                        var aoi = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == cadEvent);

                        // Desenha as riscas correspondentes
                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (Cardio[j - xStart + pagBn, index] != 0) // Verifica se há evento
                            {
                                if (eusla != 0)
                                {
                                    Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(aoi["CorFundo"]));
                                    gl.Color(Cor[0], Cor[1], Cor[2]);
                                }
                                else { gl.Color(0, 0, 0); }

                                gl.Begin(OpenGL.GL_LINES); // Use GL_LINES para linhas simples
                                gl.Vertex(j, locc - tanhamorisco / 2); // Linha começa um pouco acima do texto
                                gl.Vertex(j, locc + tanhamorisco / 2); // Linha termina um pouco abaixo do texto
                                gl.End();
                                gl.Flush();
                                gl.Color(0, 0, 0); // Define a cor da linha

                            }
                        }
                        // Move para a próxima legenda
                        locc += locLeg;
                        index++;
                    }



                    break;
                // PLM
                case 5:
                    MovimentodePernaStrip.Checked = true;
                    MovimentodePernaStrip.Tag = 5;
                    tamanho = Math.Abs(topPonto - pontoZero);

                    dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable().Where(row => row.Field<int>("CodGrupo") == codGrupo).CopyToDataTable();
                    dtSubGrupo.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));
                    qt = dtSubGrupo.Rows.Count;

                    locLeg = tamanho / qt;
                    tanhamorisco = (int)(locLeg * 0.8f);

                    var rwp = MontagemJanela.AsEnumerable()
                                         .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rwp["CorGrafico"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rwp["CorGrafico"]) == 0)
                        {
                            eusla = 0;
                        }
                        else
                        {
                            eusla = 1;
                        }
                    }
                    else { eusla = 0; }
                    Cor = new float[3];
                    var aoplm = dtSubGrupo.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodGrupo") == codGrupo);
                    cadEvent = Convert.ToInt32(aoplm["Evento"]);
                    var aoip = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == cadEvent);

                    for (int i = 0; i < qt; i++)
                    {
                        index = pagBn;

                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (plm[index] != 0)
                            {
                                if (eusla != 0)
                                {
                                    Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(aoip["CorFundo"]));
                                    gl.Color(Cor[0], Cor[1], Cor[2]);
                                }
                                else { gl.Color(0, 0, 0); }

                                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                                gl.Vertex(j, pontoZero + tanhamorisco);
                                gl.Vertex(j, topPonto - tanhamorisco);
                                gl.End();
                                gl.Flush();
                                gl.Color(0, 0, 0);
                            }
                            else
                            {

                            }
                            index++;
                        }
                    }

                    break;
                // Ronco
                case 10:
                    roncoStip.Checked = true;
                    roncoStip.Tag = 10;
                    tamanho = Math.Abs(topPonto - pontoZero);

                    dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable().Where(row => row.Field<int>("CodGrupo") == codGrupo).CopyToDataTable();
                    dtSubGrupo.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));
                    qt = dtSubGrupo.Rows.Count;

                    locLeg = tamanho / qt;
                    tanhamorisco = (int)(locLeg * 0.8f);
                    var rwronc = MontagemJanela.AsEnumerable()
                     .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rwronc["CorGrafico"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rwronc["CorGrafico"]) == 0)
                        {
                            eusla = 0;
                        }
                        else
                        {
                            eusla = 1;
                        }
                    }
                    else { eusla = 0; }
                    Cor = new float[3];
                    var aornc = dtSubGrupo.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodGrupo") == codGrupo);
                    cadEvent = Convert.ToInt32(aornc["Evento"]);
                    var aorr = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == cadEvent);


                    for (int i = 0; i < qt; i++)
                    {
                        index = pagBn;

                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (ronco[index] != 0)
                            {
                                if (eusla != 0)
                                {
                                    Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(aorr["CorFundo"]));
                                    gl.Color(Cor[0], Cor[1], Cor[2]);
                                }
                                else { gl.Color(0, 0, 0); }
                                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                                gl.Vertex(j, pontoZero + tanhamorisco);
                                gl.Vertex(j, topPonto - tanhamorisco);
                                gl.End();
                                gl.Flush();
                                gl.Color(0, 0, 0);
                            }
                            else
                            {

                            }
                            index++;
                        }
                    }

                    break;
                // Bruxismo
                case 40:
                    BruxismoStrip.Checked = true;
                    BruxismoStrip.Tag = 40;
                    // Calcula tamanho e divisões
                    tamanho = Math.Abs(topPonto - pontoZero);

                    dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                        .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                        .OrderByDescending(row => row.Field<int>("Evento")) // Ordena corretamente
                        .CopyToDataTable();

                    qt = dtSubGrupo.Rows.Count;
                    locLeg = tamanho / qt; // Espaçamento entre legendas
                    tanhamorisco = (int)(locLeg * 0.9f); // Altura do risco (90% do espaçamento)

                    // Começa no ponto inicial
                    int locr = pontoZero + locLeg / 2;
                    index = 0;
                    var rwb = MontagemJanela.AsEnumerable()
                                             .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rwb["CorGrafico"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(rwb["CorGrafico"]) == 0)
                        {
                            eusla = 0;
                        }
                        else
                        {
                            eusla = 1;
                        }
                    }
                    else { eusla = 0; }
                    Cor = new float[3];

                    foreach (DataRow rw in dtSubGrupo.Rows)
                    {
                        cadEvent = Convert.ToInt32(rw["Evento"]);
                        var aoi = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") == cadEvent);

                        // Desenha as riscas correspondentes
                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (Bruxismo[j - xStart + pagBn, index] != 0) // Verifica se há evento
                            {
                                if (eusla != 0)
                                {
                                    Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(aoi["CorFundo"]));
                                    gl.Color(Cor[0], Cor[1], Cor[2]);
                                }
                                else { gl.Color(0, 0, 0); }

                                gl.Begin(OpenGL.GL_LINES); // Use GL_LINES para linhas simples
                                gl.Vertex(j, locr - tanhamorisco / 2); // Linha começa um pouco acima do texto
                                gl.Vertex(j, locr + tanhamorisco / 2); // Linha termina um pouco abaixo do texto
                                gl.End();
                                gl.Flush();
                                gl.Color(0, 0, 0); // Define a cor da linha

                            }
                        }
                        // Move para a próxima legenda
                        locr += locLeg;
                        index++;
                    }


                    break;

                // ------- Sinais graafio -------
                // SA02
                case 6:
                    SA02Strip.Checked = true;
                    SA02Strip.Tag = 6;
                    var rows = MontagemJanela.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);
                    if (rows["CorGrafico"] != DBNull.Value)
                    {
                        Cor = new float[3];
                        Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rows["CorGrafico"]));
                        gl.Color(Cor[0], Cor[1], Cor[2]);
                    }
                    else
                    {
                        gl.Color(0, 0, 0);
                    }

                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int sasa = pagBn;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        if (NaomostrarQuedasZero.Checked && SA02[sasa] <= Convert.ToInt32(rows["LI"]))
                        {
                            sasa++;
                            i++;
                        }
                        else
                        {
                            quasi = NormalizarValor(SA02[sasa], Convert.ToInt32(rows["LI"]), Convert.ToInt32(rows["LS"]), pontoZero, topPonto);
                            gl.Vertex(i, quasi);
                            sasa++;
                        }
                    }
                    gl.End();
                    gl.Flush();
                    gl.Color(0, 0, 0);

                    break;
                // Freq Card
                case 12:
                    FreqCardStrip.Checked = true;
                    FreqCardStrip.Tag = 12;
                    var rowf = MontagemJanela.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rowf["CorGrafico"] != DBNull.Value)
                    {
                        Cor = new float[3];
                        Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowf["CorGrafico"]));
                        gl.Color(Cor[0], Cor[1], Cor[2]);
                    }
                    else
                    {
                        gl.Color(0, 0, 0);
                    }
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int feq = pagBn;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        if (NaomostrarQuedasZero.Checked && FreqCard[feq] <= Convert.ToInt32(rowf["LI"]))
                        {
                            feq++;
                            i++;
                        }
                        else
                        {
                            quasi = NormalizarValor(FreqCard[feq], Convert.ToInt32(rowf["LI"]), Convert.ToInt32(rowf["LS"]), pontoZero, topPonto);
                            gl.Vertex(i, quasi);
                            feq++;
                        }
                    }
                    gl.End();
                    gl.Flush();
                    gl.Color(0, 0, 0);

                    break;
                // Microfone
                case 19:
                    MicrofoneStrip.Checked = true;
                    MicrofoneStrip.Tag = 19;
                    int espaco = Math.Abs(topPonto - pontoZero);
                    int meioleg = espaco / 2;
                    meioleg = pontoZero;

                    int codcanal = 5;
                    int codindex = GlobVar.codSelected.IndexOf(codcanal);
                    if (codindex != -1)
                    {
                        double scala = GlobVar.scale[GlobVar.grafSelected[codindex]];
                        var rowm = MontagemJanela.AsEnumerable()
                                    .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                        if (rowm["CorGrafico"] != DBNull.Value)
                        {
                            Cor = new float[3];
                            Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowm["CorGrafico"]));
                            gl.Color(Cor[0], Cor[1], Cor[2]);
                        }
                        else
                        {
                            gl.Color(0, 0, 0);
                        }
                        gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                        int micmic = pagBn;
                        for (int i = xStart; i < xEnd; i++)
                        {
                            quasi = NormalizarValor(Microfone[micmic], Microfone.Min(), Microfone.Max(), pontoZero, topPonto);// scala;
                            gl.Vertex(i, quasi);// + meioleg);
                            micmic++;
                        }
                        gl.End();
                        gl.Flush();
                        gl.Color(0, 0, 0);
                    }
                    break;

                // --------- CPAP --------
                // CPAP
                case 11:
                    CPAPStrip.Checked = true;
                    CPAPStrip.Tag = 11;
                    var rowsf = MontagemJanela.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rowsf["CorGrafico"] != DBNull.Value)
                    {
                        Cor = new float[3];
                        Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowsf["CorGrafico"]));
                        gl.Color(Cor[0], Cor[1], Cor[2]);
                    }
                    else
                    {
                        gl.Color(0, 0, 0);
                    }
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int cpap = pagBn;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        quasi = NormalizarValor(CPAP[cpap], Convert.ToInt32(rowsf["LI"]), Convert.ToInt32(rowsf["LS"]), pontoZero, topPonto);
                        gl.Vertex(i, quasi);
                        cpap++;
                    }
                    gl.End();
                    gl.Flush();
                    gl.Color(0, 0, 0);

                    break;

                case 18:
                    cpapVazStrip.Checked = true;
                    cpapVazStrip.Tag = 18;
                    var rowvz = MontagemJanela.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rowvz["CorGrafico"] != DBNull.Value)
                    {
                        Cor = new float[3];
                        Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowvz["CorGrafico"]));
                        gl.Color(Cor[0], Cor[1], Cor[2]);
                    }
                    else
                    {
                        gl.Color(0, 0, 0);
                    }
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int cpapvz = pagBn;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        quasi = NormalizarValor(CPAPVaz[cpapvz], Convert.ToInt32(rowvz["LI"]), Convert.ToInt32(rowvz["LS"]), pontoZero, topPonto);
                        gl.Vertex(i, quasi);
                        cpapvz++;
                    }
                    gl.End();
                    gl.Flush();
                    gl.Color(0, 0, 0);


                    break;

                case 39:
                    CO2_ExalStrip.Checked = true;
                    CO2_ExalStrip.Tag = 12;
                    var rowc = MontagemJanela.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    if (rowc["CorGrafico"] != DBNull.Value)
                    {
                        Cor = new float[3];
                        Cor = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowc["CorGrafico"]));
                        gl.Color(Cor[0], Cor[1], Cor[2]);
                    }
                    else
                    {
                        gl.Color(0, 0, 0);
                    }
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int feqc = pagBn;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        if (NaomostrarQuedasZero.Checked && FreqCard[feqc] <= Convert.ToInt32(rowc["LI"]))
                        {
                            feqc++;
                            i++;
                        }
                        else
                        {
                            quasi = NormalizarValor(FreqCard[feqc], Convert.ToInt32(rowc["LI"]), Convert.ToInt32(rowc["LS"]), pontoZero, topPonto);
                            gl.Vertex(i, quasi);
                            feqc++;
                        }
                    }
                    gl.End();
                    gl.Flush();
                    gl.Color(0, 0, 0);


                    break;
                // ------- Posi / Estagio -------
                // Posicao
                case 7:
                    posicaoStip.Checked = true;
                    posicaoStip.Tag = 7;
                    qt = 4;

                    int espacosub = Math.Abs(pontoZero - topPonto);
                    int locz = espacosub / qt;

                    int meiolegz = locz / 2;
                    meiolegz += pontoZero;
                    int[] locY = new int[4];
                    for (int ao = 0; ao < qt; ao++)
                    {
                        locY[ao] = meiolegz;

                        meiolegz += locz;
                    }

                    int posiposi = pagBn;
                    for (int a = xStart; a < xEnd; a++, posiposi++)
                    {
                        gl.Color(0, 0, 0);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(a, locY[posicao[posiposi]]);

                        gl.End();
                        gl.Flush();
                    }
                    break;
                // Estagios
                case 9:
                    estagioStrip.Checked = true;
                    estagioStrip.Tag = 9;
                    var dte = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();
                    if (Tela_Plotagem.AdInf.Equals("A"))
                    {
                        dte = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();

                    }
                    else
                    {
                        dte = GlobVar.tbl_EstagiosInfatil.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();
                    }
                    qt = dte.Rows.Count;
                    var rwest = MontagemJanela.AsEnumerable()
                     .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    float[] color = new float[3];
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    int esta = pagBn;
                    int ultimoestagio = estagio[esta];
                    int lasty = -1;
                    for (int i = xStart; i < xEnd; i++, esta++)
                    {

                        int CodEstagio = estagio[esta];
                        DataRow row;
                        // Filtra a linha do DataTable
                        if (Tela_Plotagem.AdInf.Equals("A"))
                        {

                            row = GlobVar.tbl_Estagios.AsEnumerable()
                                        .FirstOrDefault(r => r.Field<int>("Estagio") == CodEstagio);
                        }
                        else
                        {
                            int ajust = CodEstagio == 0 ? 0 : CodEstagio == 5 ? 7 : CodEstagio == 4 ? 8 : 9;
                            row = GlobVar.tbl_EstagiosInfatil.AsEnumerable().FirstOrDefault(r => r.Field<short>("Estagio") == (short)ajust);
                        }

                        DataTable dt;
                        int ind;
                        if (Tela_Plotagem.AdInf.Equals("A"))
                        {
                            dt = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();
                            ind = dt.AsEnumerable()
                                    .Select((r, idx) => new { Row = r, Index = idx })
                                    .FirstOrDefault(x => x.Row.Field<int>("Estagio") == CodEstagio)?.Index ?? -1; gl.Color(0, 0, 0);

                        }
                        else
                        {
                            dt = GlobVar.tbl_EstagiosInfatil.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();
                            ind = dt.AsEnumerable()
                                     .Select((r, idx) => new { Row = r, Index = idx })
                                     .FirstOrDefault(x => x.Row.Field<short>("Estagio") == (short)CodEstagio)?.Index ?? -1; gl.Color(0, 0, 0);
                        }

                        if (ultimoestagio != CodEstagio)
                        {
                            gl.Color(0, 0, 0);
                            gl.Vertex(i, lasty);
                            gl.Vertex(i, locyEstagio[ind]);
                        }
                        else
                        {
                            if (Convert.ToInt32(rwest["CorGrafico"]) != 0)
                            {
                                // Obtém os componentes RGB com base no campo "Estagio"
                                color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(row["Cor"]));

                            }
                            else
                            {
                                // Obtém os componentes RGB com base no campo "Estagio"
                                color = [0f, 0f, 0f];

                            }
                            gl.Color(color[0], color[1], color[2]);
                            if (CodEstagio == 5)
                            {
                                gl.Vertex(i, locyEstagio[ind] - 2);
                                gl.Vertex(i, locyEstagio[ind] - 1);
                                gl.Vertex(i, locyEstagio[ind]);
                                gl.Vertex(i, locyEstagio[ind] + 1);
                                gl.Vertex(i, locyEstagio[ind] + 2);
                            }
                            else
                            {
                                gl.Vertex(i, locyEstagio[ind]);
                            }
                        }
                        lasty = locyEstagio[ind];
                        ultimoestagio = CodEstagio;
                        gl.Color(0, 0, 0);
                    }
                    gl.End();
                    gl.Flush();
                    break;
                case 21:
                    horarioStrip.Checked = true;
                    horarioStrip.Tag = 21;
                    break;
            }
        }
        public void legenda(int pontoZero, int topPonto, int codGrupo, int maxlegendx, int endX = 0)
        {
            DataTable dt = new DataTable();

            // 1. Verifica se a coluna existe:
            if (GlobVar.tbl_HipnoGrupos.Columns.Contains("CodGrupo"))
            {
                // 2. Faz o filtro:
                var filteredRows = GlobVar.tbl_HipnoGrupos.AsEnumerable()
                    .Where(row => row.Field<int>("CodGrupo") == codGrupo);
                // 3. Verifica se o resultado tem linhas:
                if (filteredRows.Any())
                {
                    dt = filteredRows.CopyToDataTable();
                    //dt = GlobVar.tbl_HipnoGrupos.AsEnumerable().Where(row => row.Field<int>("CodGrupo") == codGrupo).CopyToDataTable();
                    DataTable dtResumo = MontagemJanela.AsEnumerable().Where(row => row.Field<int>("CodGrupo") == codGrupo).CopyToDataTable();
                    DataTable dtSubGrupo = new DataTable();
                    bool linhahorario = LinhasHorarios.Checked;

                    int espaco = Math.Abs(topPonto - pontoZero);
                    if (codGrupo == 21)
                    {
                        int espacamento = Math.Abs(topPonto - pontoZero);
                        int meiohor = espacamento / 2;
                        meiohor += pontoZero;

                        int horarioAchado = 0;
                        int indexHoraio = 0;

                        for (int i = maxlegendx; i < endX; i++)
                        {
                            // Obtém o valor da coluna "Horario"
                            string horarioString = GlobVar.tbl_Paginas.Rows[indexHoraio]["Horario"].ToString();

                            // Tenta converter o valor para DateTime
                            if (DateTime.TryParse(horarioString, out DateTime horario))
                            {
                                // Verifica se o minuto e segundo são zero
                                if (horario.Minute == 0 && horario.Second == 0)
                                {
                                    // Formata o DateTime para string no formato HH:mm:ss
                                    string escreve = (horario.ToString("HH:mm"));
                                    // Adiciona a linha ao DataTable de resultado
                                    gl.Begin(OpenGL.GL_2D);
                                    int writeX = 0;
                                    int writeY = 0;
                                    int font = CalcularTamanhoFonteIdeal();
                                    System.Drawing.Font fonte = new System.Drawing.Font("Arial Narrow", font);
                                    SizeF tamanhostring = CalcularTamanhoString(escreve, fonte);
                                    ConvertToScreenCoordinates(i, 0, out writeX, out writeY);
                                    int alo = font - 2;
                                    writeX = (int)(writeX - (tamanhostring.Width / 4));
                                    writeY = meiohor;
                                    gl.DrawText(writeX, meiohor, 0.0f, 0.0f, 0.0f, "Arial Narrow", alo, "");
                                    gl.DrawText(writeX, meiohor, 0.0f, 0.0f, 0.0f, "Arial Narrow", font, escreve);

                                    gl.End();
                                    gl.Flush();

                                    if (linhahorario)
                                    {
                                        gl.Color(0.5f, 0.5f, 0.5f);
                                        // Ativar o estilo de linha pontilhada
                                        gl.Enable(OpenGL.GL_LINE_STIPPLE);

                                        // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                                        gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)

                                        // Iniciar o desenho da linha
                                        gl.Begin(OpenGL.GL_LINES);
                                        gl.Vertex(i, 0);
                                        gl.Vertex(i, openglHipno.Height);
                                        gl.End();
                                        gl.Flush();
                                        gl.Disable(OpenGL.GL_LINE_STIPPLE);

                                    }
                                }
                            }

                            indexHoraio++;
                        }
                    }
                    else if (codGrupo == 6 || codGrupo == 12 || codGrupo == 11 || codGrupo == 18 || codGrupo == 39)
                    {
                        string li = dtResumo.Rows[0]["LI"].ToString();
                        string ls = dtResumo.Rows[0]["LS"].ToString();

                        int dif = Math.Abs(Convert.ToInt32(dtResumo.Rows[0]["LI"]) - Convert.ToInt32(dtResumo.Rows[0]["LS"]));

                        int font = CalcularTamanhoFonteIdeal(12, 14);
                        int fontalo = font - 2;
                        System.Drawing.Font fonte = new System.Drawing.Font("Arial", font);

                        // Calculando tamanho do texto
                        SizeF tamanhoLi = CalcularTamanhoString(li, fonte);
                        SizeF tamanhoLs = CalcularTamanhoString(ls, fonte);

                        // Coordenadas do ponto final
                        int writeX = 0, writeY = 0;
                        ConvertToScreenCoordinates(maxlegendx, 0, out writeX, out writeY);

                        // Calculando o ponto inicial para escrita de trás para frente
                        float startXLi = writeX - (tamanhoLi.Width / 2);
                        float startXLs = writeX - (tamanhoLs.Width / 2);

                        int startYLs = (int)(topPonto - (tamanhoLs.Height / 2));
                        gl.Begin(OpenGL.GL_2D);
                        gl.DrawText((int)startXLi, pontoZero + 2, 0.0f, 0.0f, 0.0f, "Arial Narrow", fontalo, "");
                        gl.DrawText((int)startXLi, pontoZero + 2, 0.0f, 0.0f, 0.0f, "Arial Narrow", font, li);
                        gl.End();
                        gl.Flush();

                        gl.Color(0.7f, 0.7f, 0.7f);
                        // Ativar o estilo de linha pontilhada
                        gl.Enable(OpenGL.GL_LINE_STIPPLE);

                        // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                        gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)

                        // Iniciar o desenho da linha
                        gl.Begin(OpenGL.GL_LINES);
                        gl.Vertex(maxlegendx, pontoZero + (tamanhoLi.Height / 4));
                        gl.Vertex(endX, pontoZero + (tamanhoLi.Height / 4));
                        gl.End();
                        gl.Flush();
                        gl.Disable(OpenGL.GL_LINE_STIPPLE);

                        gl.Color(0, 0, 0);
                        gl.Begin(OpenGL.GL_2D);
                        gl.DrawText((int)startXLs, startYLs + 1, 0.0f, 0.0f, 0.0f, "Arial Narrow", fontalo, "");
                        gl.DrawText((int)startXLs, startYLs + 1, 0.0f, 0.0f, 0.0f, "Arial Narrow", font, ls);
                        gl.End();
                        gl.Flush();

                        gl.Color(0.7f, 0.7f, 0.7f);
                        // Ativar o estilo de linha pontilhada
                        gl.Enable(OpenGL.GL_LINE_STIPPLE);

                        // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                        gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)

                        // Iniciar o desenho da linha
                        gl.Begin(OpenGL.GL_LINES);
                        gl.Vertex(maxlegendx, startYLs + (tamanhoLs.Height / 4));
                        gl.Vertex(endX, startYLs + (tamanhoLs.Height / 4));
                        gl.End();
                        gl.Flush();
                        gl.Disable(OpenGL.GL_LINE_STIPPLE);

                        gl.Color(0, 0, 0);
                        int divsleg = Convert.ToInt32(dtResumo.Rows[0]["DivisoesLegendas"]);
                        if (divsleg != 0 && Convert.ToInt32(dtResumo.Rows[0]["DivisoesLegendas"]) < dif)
                        {
                            int alo = dif / divsleg;
                            int locdivs = (int)(pontoZero + (espaco / alo)); // - (tamanhoLs.Height / 2));
                            int espacodiv = espaco / alo;
                            int legdiv = Convert.ToInt32(dtResumo.Rows[0]["LI"]) + divsleg;
                            SizeF tamanhoDiv = CalcularTamanhoString(legdiv.ToString(), fonte);
                            float startXDiv = writeX - (tamanhoLi.Width / 2);

                            while (legdiv < Convert.ToInt32(dtResumo.Rows[0]["LS"]))
                            {
                                gl.Begin(OpenGL.GL_2D);
                                gl.DrawText((int)startXDiv, locdivs, 0.0f, 0.0f, 0.0f, "Arial Narrow", fontalo, "");
                                gl.DrawText((int)startXDiv, locdivs, 0.0f, 0.0f, 0.0f, "Arial Narrow", font, legdiv.ToString());
                                gl.End();
                                gl.Flush();
                                locdivs += espacodiv;
                                legdiv += divsleg;
                            }
                        }

                        int linhasint = Convert.ToInt32(dtResumo.Rows[0]["LinhasInternas"]);
                        if (codGrupo == 11 || codGrupo == 18)
                        {
                            if (linhasint != 0 && linhasint < dif)
                            {
                                int alo = dif / linhasint;
                                int locdivs = (int)(pontoZero + (espaco / alo)); // - (tamanhoLs.Height / 2));
                                int espacodiv = espaco / alo;

                                gl.Color(0.7f, 0.7f, 0.7f);
                                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                                // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                                gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)
                                                           // Iniciar o desenho da linha
                                gl.Begin(OpenGL.GL_LINES);
                                for (int i = 0; i < Convert.ToInt32(ls); i++)
                                {
                                    // Ativar o estilo de linha pontilhada
                                    gl.Vertex(maxlegendx, locdivs + (tamanhoLi.Height / 4));
                                    gl.Vertex(endX, locdivs + (tamanhoLi.Height / 4));
                                    locdivs += (int)(espacodiv);
                                }
                                gl.End();
                                gl.Flush();
                                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                                gl.Color(0, 0, 0);
                            }
                        }
                        else
                        {
                            if (linhasint != 0 && linhasint < dif)
                            {
                                int alo = dif / linhasint;
                                int locdivs = (int)(pontoZero + (espaco / alo)); // - (tamanhoLs.Height / 2));
                                int espacodiv = espaco / alo;

                                gl.Color(0.7f, 0.7f, 0.7f);
                                gl.Enable(OpenGL.GL_LINE_STIPPLE);
                                // Configurar o padrão de pontilhado (padrão de 16 bits e fator de repetição)
                                gl.LineStipple(1, 0x00FF); // Fator 1, padrão 0x00FF (pontos alternados)
                                                           // Iniciar o desenho da linha
                                gl.Begin(OpenGL.GL_LINES);

                                while (locdivs < topPonto)
                                {
                                    // Ativar o estilo de linha pontilhada
                                    gl.Vertex(maxlegendx, locdivs + (tamanhoLi.Height / 4));
                                    gl.Vertex(endX, locdivs + (tamanhoLi.Height / 4));
                                    locdivs += (int)(espacodiv);
                                }
                                gl.End();
                                gl.Flush();
                                gl.Disable(OpenGL.GL_LINE_STIPPLE);
                                gl.Color(0, 0, 0);
                            }
                        }
                    }

                    if (!dt.Rows[0]["Legenda"].Equals("") && !dt.Rows[0]["Legenda"].Equals("ESTAGIO") && !dt.Rows[0]["Legenda"].Equals("Posição") && codGrupo != 40)
                    {
                        string legMarcDAgua = dt.Rows[0]["LabelMenu"].ToString();
                        int meioleg = espaco / 2;
                        meioleg += pontoZero;
                        string leg = dt.Rows[0]["Legenda"].ToString();

                        gl.Begin(OpenGL.GL_2D);
                        int writeX = 0;
                        int writeY = 0;

                        writeX += 4;
                        writeY = meioleg;
                        gl.DrawText(writeX + 1, meioleg, 0.0f, 0.0f, 0.0f, "Arial Narrow", 13, "");
                        gl.DrawText(writeX + 1, meioleg, 0.0f, 0.0f, 0.0f, "Arial Narrow", 15, leg);

                        gl.End();
                        gl.Flush();

                        if (MarcaDAgua.Checked)
                        {
                            int locmarc = (int)(endX / 2);
                            int font = CalcularTamanhoFonteIdeal(13, 15);
                            int fontalo = font - 2;
                            System.Drawing.Font fonte = new System.Drawing.Font("Arial Narrow", font);

                            ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);
                            SizeF tamanhoDiv = CalcularTamanhoString(legMarcDAgua.ToString(), fonte);
                            writeX = (int)(writeX - (tamanhoDiv.Width / 4));

                            gl.Begin(OpenGL.GL_2D);

                            gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 13, "");
                            gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 15, legMarcDAgua);

                            gl.End();
                            gl.Flush();

                        }

                    }
                    else
                    {
                        if (codGrupo == 7)
                        {
                            string[] posicoes = new string[4];
                            posicoes = ["Brucos", "Esquerda", "Direita", "Supino"];

                            int qt = posicoes.Length;

                            int espacosub = Math.Abs(pontoZero - topPonto);
                            int locLeg = espacosub / qt;

                            int meioleg = locLeg / 2;
                            meioleg += pontoZero;
                            int aoi = 0;
                            for (int ao = 0; ao < qt; ao++)
                            {
                                string leg = posicoes[aoi];

                                gl.Begin(OpenGL.GL_2D);
                                int writeX = 0;
                                int writeY = 0;

                                writeX += 12;
                                writeY = meioleg;
                                gl.DrawText(writeX + 1, meioleg, 0.0f, 0.0f, 0.0f, "Arial Narrow", 12, "");
                                gl.DrawText(writeX + 1, meioleg, 0.0f, 0.0f, 0.0f, "Arial Narrow", 14, leg);

                                gl.End();
                                gl.Flush();

                                meioleg += locLeg;
                                aoi++;
                            }

                            string legMarcDAgua = dt.Rows[0]["LabelMenu"].ToString();
                            if (MarcaDAgua.Checked)
                            {
                                meioleg = espaco / 2;
                                meioleg += pontoZero;
                                int writeX = 0;
                                int writeY = 0;

                                int font = CalcularTamanhoFonteIdeal(13, 15);
                                int fontalo = font - 2;
                                System.Drawing.Font fonte = new System.Drawing.Font("Arial Narrow", font);

                                int locmarc = (int)(endX / 2);
                                ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);
                                SizeF tamanhoDiv = CalcularTamanhoString(legMarcDAgua.ToString(), fonte);
                                writeX = (int)(writeX - (tamanhoDiv.Width / 4));
                                gl.Begin(OpenGL.GL_2D);

                                gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 13, "");
                                gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 15, legMarcDAgua);

                                gl.End();
                                gl.Flush();
                            }

                        }
                        else if (codGrupo == 9)
                        {
                            var dte = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();
                            if (Tela_Plotagem.AdInf.Equals("A"))
                            {
                                dte = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();

                            }
                            else
                            {
                                dte = GlobVar.tbl_EstagiosInfatil.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();
                            }

                            int qt = dte.Rows.Count;
                            locyEstagio = new int[qt];
                            int font = CalcularTamanhoFonteIdeal(12, 14);
                            System.Drawing.Font fonte = new System.Drawing.Font("Arial", font);

                            int espacosub = Math.Abs(pontoZero - topPonto);
                            int locLeg = espacosub / qt;

                            int meioleg = locLeg / 2;
                            meioleg += pontoZero;
                            int aoi = 0;
                            for (int ao = 0; ao < qt; ao++, aoi++, meioleg += locLeg)
                            {
                                string leg = dte.Rows[aoi]["Legenda"].ToString();
                                locyEstagio[ao] = meioleg;


                                gl.Begin(OpenGL.GL_2D);

                                float[] color = new float[3];

                                color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(dte.Rows[ao]["Cor"]));
                                // Calculando o ponto inicial para escrita de trás para frente
                                SizeF tamanhoLi = CalcularTamanhoString(leg, fonte);
                                // Coordenadas do ponto final
                                int writeX = 0, writeY = 0;
                                ConvertToScreenCoordinates(maxlegendx, 0, out writeX, out writeY);

                                // Calculando o ponto inicial para escrita de trás para frente
                                float startXdiv = writeX - (tamanhoLi.Width / 2);
                                if (startXdiv < 0) { startXdiv = 0; }
                                writeY = meioleg;

                                gl.DrawText((int)startXdiv, meioleg, color[0], color[1], color[2], "Arial Narrow", 12, "");
                                gl.DrawText((int)startXdiv, meioleg, color[0], color[1], color[2], "Arial Narrow", 14, leg);

                                gl.Color(0, 0, 0);
                                gl.End();
                                gl.Flush();
                            }

                            string legMarcDAgua = dt.Rows[0]["LabelMenu"].ToString();
                            if (MarcaDAgua.Checked)
                            {
                                meioleg = espaco / 2;
                                meioleg += pontoZero;
                                int writeX = 0;
                                int writeY = 0;

                                font = CalcularTamanhoFonteIdeal(13, 15);
                                int fontalo = font - 2;
                                fonte = new System.Drawing.Font("Arial Narrow", font);

                                int locmarc = (int)(endX / 2);
                                ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);
                                SizeF tamanhoDiv = CalcularTamanhoString(legMarcDAgua.ToString(), fonte);
                                writeX = (int)(writeX - (tamanhoDiv.Width / 4));

                                gl.Begin(OpenGL.GL_2D);

                                gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 13, "");
                                gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 15, legMarcDAgua);

                                gl.End();
                                gl.Flush();
                            }
                        }
                        else
                        {
                            dtSubGrupo = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .OrderByDescending(row => row.Field<int>("Evento"))
                                .CopyToDataTable();

                            int qt = dtSubGrupo.Rows.Count;

                            int font = CalcularTamanhoFonteIdeal(12, 14);
                            System.Drawing.Font fonte = new System.Drawing.Font("Arial", font);

                            int espacosub = Math.Abs(pontoZero - topPonto);
                            int locLeg = espacosub / qt;

                            int meioleg = locLeg / 2;
                            meioleg += pontoZero;
                            foreach (DataRow rw in dtSubGrupo.Rows)
                            {
                                string leg = rw["DescrSubGrupo"].ToString();
                                SizeF tamanhoLi = CalcularTamanhoString(leg, fonte);
                                // Coordenadas do ponto final
                                int writeX = 0, writeY = 0;
                                ConvertToScreenCoordinates(maxlegendx, 0, out writeX, out writeY);

                                // Calculando o ponto inicial para escrita de trás para frente
                                float startXdiv = writeX - (tamanhoLi.Width / 2);
                                if (startXdiv < 0) { startXdiv = 0; }

                                gl.Begin(OpenGL.GL_2D);
                                writeY = meioleg;
                                gl.DrawText((int)startXdiv, meioleg, 0.0f, 0.0f, 0.0f, "Arial Narrow", 12, "");
                                gl.DrawText((int)startXdiv, meioleg, 0.0f, 0.0f, 0.0f, "Arial Narrow", 14, leg);
                                gl.End();
                                gl.Flush();
                                meioleg += locLeg;
                            }
                            string legMarcDAgua = dt.Rows[0]["LabelMenu"].ToString();
                            if (MarcaDAgua.Checked)
                            {
                                meioleg = espaco / 2;
                                meioleg += pontoZero;
                                int writeX = 0;
                                int writeY = 0;

                                font = CalcularTamanhoFonteIdeal(13, 15);
                                int fontalo = font - 2;
                                fonte = new System.Drawing.Font("Arial Narrow", font);

                                int locmarc = (int)(endX / 2);
                                ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);
                                SizeF tamanhoDiv = CalcularTamanhoString(legMarcDAgua.ToString(), fonte);
                                writeX = (int)(writeX - (tamanhoDiv.Width / 4));

                                gl.Begin(OpenGL.GL_2D);

                                gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 13, "");
                                gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 15, legMarcDAgua);

                                gl.End();
                                gl.Flush();
                            }
                        }
                    }
                }
            }
        }
        public static DataTable FiltrarHorariosCompletos(DataTable dataTable)
        {
            // Clona a estrutura do DataTable original
            DataTable resultado = dataTable.Clone();

            // Itera pelas linhas do DataTable original
            foreach (DataRow row in dataTable.Rows)
            {
                // Obtém o valor da coluna "Horario"
                string horarioString = row["Horario"].ToString();

                // Tenta converter o valor para DateTime
                if (DateTime.TryParse(horarioString, out DateTime horario))
                {
                    // Verifica se o minuto e segundo são zero
                    if (horario.Minute == 0 && horario.Second == 0)
                    {
                        // Adiciona a linha ao DataTable de resultado
                        resultado.ImportRow(row);
                    }
                }
            }

            return resultado;
        }
        public static Vector2 ConvertToScreenCoordinates(float openGLX, float openGLY, out int screenX, out int screenY)
        {
            var gl = FormLaudo.openglHipno.OpenGL;

            // Get the viewport and projection/modelview matrices
            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

            double[] modelview = new double[16];
            gl.GetDouble(OpenGL.GL_MODELVIEW_MATRIX, modelview);

            double[] projection = new double[16];
            gl.GetDouble(OpenGL.GL_PROJECTION_MATRIX, projection);

            // Arrays to store the window coordinates
            double[] winX = new double[1];
            double[] winY = new double[1];
            double[] winZ = new double[1];

            // Convert OpenGL coordinates to screen coordinates
            gl.Project(openGLX, openGLY, 0, modelview, projection, viewport, winX, winY, winZ);

            screenX = (int)winX[0];
            screenY = (int)(viewport[3] - winY[0]); // invert Y coordinate

            return new Vector2(screenX, screenY);
        }
        public static int CalcularTamanhoFonteIdeal(int tamanhoMinimo = 9, int tamanhoMaximo = 14)
        {
            // Obtém as dimensões do formulário
            int largura = openglHipno.Width;
            int altura = openglHipno.Height;

            // Calcula o tamanho base da fonte como uma média proporcional à área da tela
            int tamanhoCalculado = (int)Math.Sqrt((largura * altura) / 1000.0);

            // Garante que o tamanho esteja dentro dos limites mínimos e máximos
            tamanhoCalculado = Math.Max(tamanhoMinimo, Math.Min(tamanhoCalculado, tamanhoMaximo));

            return tamanhoCalculado;
        }

        public static double NormalizarValor(double valor, double minOriginal, double maxOriginal, double minY, double maxY)
        {
            if (maxOriginal == minOriginal) return 0;

            if (valor < minOriginal)
                return minY;
            if (valor > maxOriginal)
                return maxY;

            // Aplicando a fórmula de normalização
            return minY + (valor - minOriginal) * (maxY - minY) / (maxOriginal - minOriginal);
        }
        public static SizeF CalcularTamanhoString(string texto, System.Drawing.Font fonte)
        {
            using (Bitmap bitmap = new Bitmap(1, 1))
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
            {
                return g.MeasureString(texto, fonte);
            }
        }
        public void linhadehorario_Click(object sender, EventArgs e)
        {
            // Redesenha o conteúdo para ajustar ao novo tamanho
            Desenha();

        }

        float mouseLocX;
        float mouseLocY;
        static bool bordMargOn = false;
        static bool mouseDown = false;
        private Point lastMousePosition;
        public Point initialMousePosition;
        bool movendoGraf = false;
        bool onTop = false;
        bool onBut = false;
        bool changingSize = false;
        static string g_dir_laudos = "";
        static string nomeOrigem = "";
        static string nome_arq_temp = "";

        private void OpenGLHipno_MouseMove(object sender, MouseEventArgs e)
        {
            if (e == null) return;

            try
            {
                if (MontagemJanela == null || MontagemJanela.Rows.Count == 0) return;
                var dr = MontagemJanela.AsEnumerable().OrderBy(row => row.Field<int>("Ordem")).CopyToDataTable();

                ConvertToOpenGLCoordinates(e.X, e.Y, out mouseLocX, out mouseLocY);
                float outX = 0;
                float outY = 0;

                if (movendoGraf)
                {
                    ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                    float initialMouseY = initialMousePosition.Y;

                    if (e.Y != lastMousePosition.Y)
                    {
                        float deltaY = outY - initialMouseY;
                        float ddeltaY = e.Y - lastMousePosition.Y;

                        areaSelectpZero -= (int)ddeltaY;
                        areaSelectTop -= (int)ddeltaY;

                        initialMousePosition.Y = (int)outY;
                        openglHipno.Refresh();
                        Desenha();
                    }
                    lastMousePosition.Y = e.Y;
                }
                else if (changingSize)
                {
                    if (onTop)
                    {
                        outX = 0;
                        outY = 0;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                        float initialMouseY = initialMousePosition.Y;

                        if (e.Y != lastMousePosition.Y)
                        {
                            float ddeltaY = e.Y - lastMousePosition.Y;

                            areaSelectTop -= (int)ddeltaY;

                            initialMousePosition.Y = (int)outY;
                            openglHipno.Refresh();
                            Desenha();
                        }
                        lastMousePosition.Y = e.Y;
                    }
                    else if (onBut)
                    {
                        outX = 0;
                        outY = 0;
                        ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out outY);

                        float initialMouseY = initialMousePosition.Y;

                        if (e.Y != lastMousePosition.Y)
                        {
                            float ddeltaY = e.Y - lastMousePosition.Y;

                            areaSelectpZero -= (int)ddeltaY;

                            initialMousePosition.Y = (int)outY;
                            openglHipno.Refresh();
                            Desenha();
                        }
                        lastMousePosition.Y = e.Y;
                    }
                }
                else
                {
                    for (int i = 0; i < pontoZero.Length; i++)
                    {
                        if (mouseLocY > pontoZero[i] && mouseLocY < pontoTop[i])
                        {
                            codJanela = Convert.ToInt32(dr.Rows[i]["CodGrupo"]);

                            // Verifica se o cursor está na borda superior ou inferior
                            if (mouseClicked && mouseLocX < marg - 15 && ((mouseLocY >= areaSelectpZero - 3 && mouseLocY <= areaSelectpZero + 3) ||
                                                            (mouseLocY >= areaSelectTop - 3 && mouseLocY <= areaSelectTop + 3)))
                            {
                                bordMargOn = false;
                                this.Cursor = Cursors.SizeNS;
                            }
                            // Verifica se está na margem lateral
                            else if ((mouseLocX > marg - 15 && mouseLocX < marg + 15) && !mouseDown)
                            {
                                bordMargOn = true;
                                this.Cursor = Cursors.SizeWE;
                            }
                            // Se está segurando o mouse e movendo a margem
                            else if (mouseDown)
                            {
                                ConvertToOpenGLCoordinates(e.X, e.Y, out outX, out _);
                                float deltaX = outX - initialMousePosition.X;

                                if (e.X != lastMousePosition.X)
                                {
                                    NeEmarg += (int)deltaX;
                                    NeWPorcentagem += (int)deltaX;

                                    initialMousePosition.X = (int)outX;
                                    Desenha();
                                }
                                lastMousePosition.X = e.X;
                            }
                            else
                            {
                                bordMargOn = false;
                                this.Cursor = Cursors.Default;
                            }
                            break; // Sai do loop após encontrar a condição válida
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro em OpenGLHipno_MouseMove: " + ex.Message);
            }
        }
        private void OpenGLHipno_MouseDown(object sender, MouseEventArgs e)
        {
            if (e?.Button != MouseButtons.Left) return;

            try
            {
                if (MontagemJanela == null || MontagemJanela.Rows.Count == 0) return;
                var dr = MontagemJanela.AsEnumerable().CopyToDataTable();

                ConvertToOpenGLCoordinates(e.X, e.Y, out mouseLocX, out mouseLocY);
                if (mouseClicked && (mouseLocY > areaSelectpZero + 4 && mouseLocY < areaSelectTop - 4) && !bordMargOn && mouseLocX < marg)
                {
                    movendoGraf = true;
                }
                if (mouseClicked && !bordMargOn && ((mouseLocY >= areaSelectpZero - 3 && mouseLocY <= areaSelectpZero + 3) ||
                                                         (mouseLocY >= areaSelectTop - 3 && mouseLocY <= areaSelectTop + 3)) && mouseLocX < marg)
                {
                    changingSize = true;
                    if ((mouseLocY >= areaSelectpZero - 3 && mouseLocY <= areaSelectpZero + 3))
                    {
                        onBut = true;
                    }
                    else if ((mouseLocY >= areaSelectTop - 3 && mouseLocY <= areaSelectTop + 3))
                    {
                        onTop = true;
                    }
                }
                else if ((mouseClicked || !mouseClicked) && mouseLocX < marg)
                {
                    for (int i = 0; i < pontoZero.Length; i++)
                    {
                        if (bordMargOn)
                        {
                            ConvertToOpenGLCoordinates(e.X, e.Y, out float aux, out _);
                            initialMousePosition.X = (int)aux;
                            lastMousePosition = e.Location;
                            mouseDown = true;
                            desenhaMargLine = true;
                            openglHipno.Refresh();

                            Desenha();
                            break;
                        }
                        else if ((mouseLocY > pontoZero[i] && mouseLocY < pontoTop[i]) && mouseLocX < marg)
                        {
                            int codGrupoAtual = Convert.ToInt32(dr.Rows[i]["CodGrupo"]);

                            if (codGrupoAtual != codJanela_Click)
                            {
                                codJanela_Click = codGrupoAtual;
                                mouseClicked = true;
                                areaSelectpZero = pontoZero[i];
                                areaSelectTop = pontoTop[i];
                            }
                            else if (!movendoGraf && !changingSize)
                            {
                                mouseClicked = false;
                                codJanela_Click = -1;
                            }
                            openglHipno.Refresh();

                            Desenha();
                            break;
                        }
                    }
                }
                else if (mouseLocX > marg)
                {
                    /*
                    int newind = (int)((mouseLocX - marg) * GlobVar.namos);
                    int newmax = newind + 130000;
                    GlobVar.indice = newind;
                    GlobVar.maximaVect = newmax;
                    int newindNum = (int)((mouseLocX - marg) * GlobVar.numeroAmos);
                    int newmaxNum = newind + (GlobVar.segundos * GlobVar.namosNumerico);
                    GlobVar.indiceNumero = newindNum;
                    GlobVar.maximaNumero = newmaxNum;
                    */
                    if (this.Owner is Tela_Plotagem pai)
                    {
                        int inicio = (int)((mouseLocX - marg) / GlobVar.segundos);

                        Tela_Plotagem.ptsEmTela.Text = $"{inicio}";
                        Tela_Plotagem.ptsEmTela.Focus();
                        var enterKeyEvent = new KeyEventArgs(Keys.Enter); // '\r' representa o Enter
                        pai.PtsEmTela_KeyDown(Tela_Plotagem.ptsEmTela, enterKeyEvent);
                    }

                    movendoGraf = false;
                    mouseClicked = false;
                    codJanela_Click = -1;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro em OpenGLHipno_MouseDown: " + ex.Message);
            }
        }
        private void OpenGLHipno_MouseUp(object sender, MouseEventArgs e)
        {
            if (e?.Button != MouseButtons.Left) return;

            try
            {
                if (movendoGraf && !changingSize)
                {
                    movendoGraf = false;
                    mouseClicked = false;

                    // Encontrar a linha pelo CodGrupo
                    DataRow row = MontagemJanela.AsEnumerable().FirstOrDefault(r => r.Field<int>("CodGrupo") == codJanela_Click);

                    if (row != null)
                    {
                        int currentIndex = MontagemJanela.Rows.IndexOf(row);
                        int newIndex = -1;

                        // Verificar onde a linha deve ser inserida
                        for (int i = 0; i < MontagemJanela.Rows.Count; i++)
                        {
                            if (areaSelectpZero < pontoZero[i])
                            {
                                newIndex = i;
                            }
                        }
                        // Se o índice mudou, mover a linha
                        if (newIndex != currentIndex && newIndex != -1)
                        {
                            // Criar uma nova linha e copiar os dados antes de remover
                            DataRow newRow = MontagemJanela.NewRow();
                            newRow.ItemArray = row.ItemArray.Clone() as object[];

                            // Remover a linha original
                            MontagemJanela.Rows.Remove(row);

                            // Inserir na nova posição
                            MontagemJanela.Rows.InsertAt(newRow, newIndex);
                            codJanela_Click = -1;
                            reajustaPorc();
                        }
                    }

                }
                else if (changingSize && !movendoGraf)
                {
                    changingSize = false;
                    onBut = false;
                    onTop = false;
                    mouseClicked = false;

                    int newsize = Math.Abs(areaSelectpZero - areaSelectTop);
                    float newsizePorc = (newsize * 100f) / openglHipno.Height; // Regra de três
                    // Encontrar a linha pelo CodGrupo
                    DataRow row = MontagemJanela.AsEnumerable().FirstOrDefault(r => r.Field<int>("CodGrupo") == codJanela_Click);
                    row["Porc"] = newsizePorc;

                    MontagemJanela.AcceptChanges();
                    reajustaPorc();
                    codJanela_Click = -1;
                }
                mouseDown = false;
                lastMousePosition = e.Location;
                if (desenhaMargLine)
                {
                    desenhaMargLine = false;
                    marg = NeEmarg;
                    Porcentagem = NeWPorcentagem;
                }
                openglHipno.Refresh();
                Desenha();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro em OpenGLHipno_MouseUp: " + ex.Message);
            }
        }
        public static void ConvertToOpenGLCoordinates(int mouseX, int mouseY, out float openGLX, out float openGLY)
        {
            var gl = openglHipno.OpenGL;

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
        private void ContextMenuStripOpenGl_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                //Toda vez que o context e aberto, ele "da um clear nos itens que ele tem e altera com base no que ele vai fazer"
                contextMenuStripHipno.Items.Clear();

                if (codJanela == 6 || codJanela == 12 || codJanela == 11 || codJanela == 18)
                {
                    contextMenuStripHipno.Items.AddRange(new ToolStripItem[] { Imprimir,
                                                        separador, BruxismoStrip, CardioStrip, CO2_ExalStrip, CPAPStrip, cpapVazStrip, DespertarStrip, estagioStrip, FreqCardStrip, horarioStrip, MicrofoneStrip
                                                      , MovimentodePernaStrip, posicaoStip, eventosRespStrip, roncoStip, SA02Strip, separador1
                                                      , NaomostrarQuedasZero, ConsBnBd, LinhasHorarios, CorGraf, MarcaDAgua
                                                      , separador2, LimSup, LimInf, LinInt, DivLeg});
                }
                else
                {
                    contextMenuStripHipno.Items.AddRange(new ToolStripItem[] { Imprimir,
                                                        separador, BruxismoStrip, CardioStrip, CO2_ExalStrip, CPAPStrip, cpapVazStrip, DespertarStrip, estagioStrip, FreqCardStrip, horarioStrip, MicrofoneStrip
                                                      , MovimentodePernaStrip, posicaoStip, eventosRespStrip, roncoStip, SA02Strip, separador1,
                                                        NaomostrarQuedasZero, ConsBnBd, LinhasHorarios, CorGraf, MarcaDAgua});
                }

            }
            catch { }

        }
        private void lmSup_Click(object sender, EventArgs e)
        {
            // Verifica se a DataTable existe
            if (MontagemJanela == null || MontagemJanela.Rows.Count == 0)
            {
                MessageBox.Show("Tabela vazia ou não inicializada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Busca a linha onde CodGrupo é igual a codJanela
            DataRow[] foundRows = MontagemJanela.Select($"CodGrupo = {codJanela}");

            if (foundRows.Length > 0)
            {
                DataRow row = foundRows[0]; // Assume que há apenas uma linha correspondente

                // Pega o valor atual da coluna LS
                string valorAtual = row["LS"].ToString();

                // Abre um input para entrada do novo valor
                string novoValor = Microsoft.VisualBasic.Interaction.InputBox(
                    "Digite o novo Limite Superior:",
                    "Alterar Limite Superior",
                    valorAtual);

                // Verifica se o usuário digitou algo
                if (!string.IsNullOrEmpty(novoValor) && Convert.ToInt32(novoValor) <= 100 && Convert.ToInt32(novoValor) > Convert.ToInt32(row["LI"]))
                {
                    row["LS"] = novoValor; // Atualiza o valor na DataTable
                    Desenha();
                }
                else
                {
                    MessageBox.Show("Valor invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma linha encontrada para o CodGrupo especificado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void lmInf_Click(object sender, EventArgs e)
        {
            // Verifica se a DataTable existe
            if (MontagemJanela == null || MontagemJanela.Rows.Count == 0)
            {
                MessageBox.Show("Tabela vazia ou não inicializada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Busca a linha onde CodGrupo é igual a codJanela
            DataRow[] foundRows = MontagemJanela.Select($"CodGrupo = {codJanela}");

            if (foundRows.Length > 0)
            {
                DataRow row = foundRows[0]; // Assume que há apenas uma linha correspondente

                // Pega o valor atual da coluna LS
                string valorAtual = row["LI"].ToString();

                // Abre um input para entrada do novo valor
                string novoValor = Microsoft.VisualBasic.Interaction.InputBox(
                    "Digite o novo Limite Inferior:",
                    "Alterar Limite Inferior",
                    valorAtual);

                // Verifica se o usuário digitou algo
                if (!string.IsNullOrEmpty(novoValor) && Convert.ToInt32(novoValor) < Convert.ToInt32(row["LS"]))
                {
                    row["LI"] = novoValor; // Atualiza o valor na DataTable
                    Desenha();
                }
                else
                {
                    MessageBox.Show("Valor invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma linha encontrada para o CodGrupo especificado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LinhasInternas_Click(object sender, EventArgs e)
        {
            // Verifica se a DataTable existe
            if (MontagemJanela == null || MontagemJanela.Rows.Count == 0)
            {
                MessageBox.Show("Tabela vazia ou não inicializada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Busca a linha onde CodGrupo é igual a codJanela
            DataRow[] foundRows = MontagemJanela.Select($"CodGrupo = {codJanela}");

            if (foundRows.Length > 0)
            {
                DataRow row = foundRows[0]; // Assume que há apenas uma linha correspondente

                // Pega o valor atual da coluna LS
                string valorAtual = row["LinhasInternas"].ToString();
                string novoValor = "";
                if (codJanela == 11 || codJanela == 18)
                {
                    // Abre um input para entrada do novo valor
                    novoValor = Microsoft.VisualBasic.Interaction.InputBox(
                        "Informe o espacamento entre as linhas. Para nao",
                        "mostrar linhas internas digite '0'",
                        valorAtual);
                    // Verifica se o usuário digitou algo
                    if (!string.IsNullOrEmpty(novoValor) && (Convert.ToInt32(novoValor) > 1 || Convert.ToInt32(novoValor) < 0))
                    {
                        row["LinhasInternas"] = novoValor; // Atualiza o valor na DataTable
                        Desenha();
                    }
                    else
                    {
                        MessageBox.Show("Valor invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Abre um input para entrada do novo valor
                    novoValor = Microsoft.VisualBasic.Interaction.InputBox(
                        "Digite o novo valor de Linhas Internas:",
                        "Alterar Linhas Internas",
                        valorAtual);
                    // Verifica se o usuário digitou algo
                    if (!string.IsNullOrEmpty(novoValor) && Convert.ToInt32(novoValor) >= 0)
                    {
                        row["LinhasInternas"] = novoValor; // Atualiza o valor na DataTable
                        Desenha();
                    }
                    else
                    {
                        MessageBox.Show("Valor invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nenhuma linha encontrada para o CodGrupo especificado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DivLegenda_Click(object sender, EventArgs e)
        {
            // Verifica se a DataTable existe
            if (MontagemJanela == null || MontagemJanela.Rows.Count == 0)
            {
                MessageBox.Show("Tabela vazia ou não inicializada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Busca a linha onde CodGrupo é igual a codJanela
            DataRow[] foundRows = MontagemJanela.Select($"CodGrupo = {codJanela}");

            if (foundRows.Length > 0)
            {
                DataRow row = foundRows[0]; // Assume que há apenas uma linha correspondente

                // Pega o valor atual da coluna LS
                string valorAtual = row["DivisoesLegendas"].ToString();

                // Abre um input para entrada do novo valor
                string novoValor = Microsoft.VisualBasic.Interaction.InputBox(
                    "Digite o novo valor de Linhas Internas:",
                    "Alterar Linhas Internas",
                    valorAtual);

                // Verifica se o usuário digitou algo
                if (!string.IsNullOrEmpty(novoValor) && Convert.ToInt32(novoValor) >= 0)
                {
                    row["DivisoesLegendas"] = novoValor; // Atualiza o valor na DataTable
                    Desenha();
                }
                else
                {
                    MessageBox.Show("Valor invalido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma linha encontrada para o CodGrupo especificado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private ButtonForm buttonForm;
        private void CorSinal_Click(object sender, EventArgs e)
        {
            try
            {
                if (codJanela == 1 || codJanela == 3 || codJanela == 4 || codJanela == 5 || codJanela == 10 || codJanela == 40 || codJanela == 9)
                {
                    var rowNumerico = MontagemJanela.AsEnumerable()
                                    .FirstOrDefault(row => row.Field<int>("CodGrupo") == codJanela);

                    if (rowNumerico != null)
                    {
                        var titi = GlobVar.tbl_HipnoGrupos.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodGrupo") == codJanela);
                        string titulo = titi["DescrGrupo"].ToString();
                        int valorAtual;
                        if (rowNumerico["CorGrafico"] == DBNull.Value)
                        {
                            valorAtual = 0;
                        }
                        else
                        {
                            if (Convert.ToInt32(rowNumerico["CorGrafico"]) == 0)
                            {
                                valorAtual = 0;
                            }
                            else
                            {
                                valorAtual = 1;
                            }
                        }

                        using (MiniFormCor miniForm = new MiniFormCor(titulo, valorAtual))
                        {
                            if (miniForm.ShowDialog() == DialogResult.OK)
                            {
                                rowNumerico["CorGrafico"] = miniForm.Resultado;
                                MontagemJanela.AcceptChanges();
                                Desenha();
                            }
                        }
                    }
                }
                else
                {
                    var rowNumerico = MontagemJanela.AsEnumerable()
                                                .FirstOrDefault(row => row.Field<int>("CodGrupo") == codJanela);

                    System.Drawing.Color c = System.Drawing.Color.Black;
                    buttonForm.HideOverlay();
                    ColorPickerDialog minhasCores = new ColorPickerDialog();
                    if (minhasCores.ShowDialog() == DialogResult.OK)
                    {
                        c = minhasCores.Color;
                        int cor = c.R | (c.G << 8) | (c.B << 16);
                        rowNumerico["CorGrafico"] = cor;

                        MontagemJanela.AcceptChanges();
                        Desenha();
                    }
                }
            }
            catch { }
        }
        public bool VerificaDados(int codGrupo)
        {
            try
            {
                switch (codGrupo)
                {
                    // ------- Do tipo Evento --------
                    case 1:  // Respiratório
                        return eventosResp != null;

                    case 3:  // Despertar
                        return Despertar != null;

                    case 4:  // Cardio
                        CardioStrip.Checked = true;
                        CardioStrip.Tag = 4;
                        return false;

                    case 5:  // PLM
                        return plm != null;

                    case 10: // Ronco
                        return ronco != null;

                    case 40: // Bruxismo
                        BruxismoStrip.Checked = true;
                        BruxismoStrip.Tag = 40;
                        return false;

                    // ------- Sinais gráficos -------
                    case 6:  // SA02
                        return SA02 != null;

                    case 12: // Freq Card
                        return FreqCard != null;

                    case 19: // Microfone
                        return Microfone != null;

                    // ----------- Cpapi --------------,
                    case 11:
                        return CPAP != null;

                    case 18:
                        return CPAPVaz != null;
                    case 39:
                        return CO2_ExalStrip != null;
                    // ------- Posi / Estágio -------
                    case 7:  // Posição
                        return posicao != null;

                    case 9:  // Estágios
                        return estagio != null;

                    case 21: // Horário
                        horarioStrip.Checked = true;
                        horarioStrip.Tag = 21;
                        return horarioStrip.Checked;

                    default:
                        return false; // Retorno padrão para valores não mapeados
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro em VerificaDados: {ex.Message}");
                return false;
            }
        }
        public static void AdicionaOsDadosCasoNullo(int codGrupo)
        {
            int tamanho = GlobVar.matrizCanal.GetLength(1) / GlobVar.namos;
            int codcanal;
            int codindex;
            int[] media;
            int rw = 0;
            int h;
            DataTable subGrupos = new DataTable();

            switch (codGrupo)
            {
                // ------- Do tipo Evento --------
                // Respiratorio
                case 1:
                    subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .OrderByDescending(row => row.Field<int>("Evento"))
                                .CopyToDataTable();


                    eventosResp = new int[tamanho, subGrupos.Rows.Count];

                    // Preenche a matriz com zeros
                    for (int i = 0; i < tamanho; i++)
                    {
                        for (int j = 0; j < subGrupos.Rows.Count; j++)
                        {
                            eventosResp[i, j] = 0;
                        }
                    }

                    rw = 0;
                    if (subGrupos != null && subGrupos.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subGrupos.Rows)
                        {
                            int codEvento = Convert.ToInt32(subRow["Evento"]);
                            DataTable EventosSub;

                            // Verifica se há resultados antes de chamar CopyToDataTable
                            var query = GlobVar.eventos.AsEnumerable()
                                        .Where(row => row.Field<int>("CodEvento") == codEvento);

                            if (query.Any())
                            {
                                EventosSub = query.CopyToDataTable();
                            }
                            else
                            {
                                EventosSub = new DataTable(); // DataTable vazio
                            }

                            if (EventosSub.Rows.Count > 0)
                            {
                                foreach (DataRow eventRow in EventosSub.Rows)
                                {
                                    eventosResp[Convert.ToInt32(eventRow["NumPag"]), rw] = 1;
                                }
                            }

                            rw++;
                        }
                    }

                    subGrupos.Dispose();
                    break;
                // Despertar
                case 3:
                    subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .CopyToDataTable();
                    subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                    Despertar = new int[tamanho];
                    Array.Clear(Despertar, 0, Despertar.Length);

                    if (subGrupos != null && subGrupos.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subGrupos.Rows)
                        {
                            int codEvento = Convert.ToInt32(subRow["Evento"]);
                            var query = GlobVar.eventos.AsEnumerable()
                                        .Where(row => row.Field<int>("CodEvento") == codEvento);

                            DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                            if (EventosSub.Rows.Count > 0)
                            {
                                foreach (DataRow eventRow in EventosSub.Rows)
                                {
                                    int index = Convert.ToInt32(eventRow["NumPag"]);
                                    Despertar[index] = 1;
                                }
                            }
                        }
                    }
                    subGrupos.Dispose();
                    break;
                // Cardio
                case 4:
                    subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .CopyToDataTable();
                    subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                    Cardio = new int[tamanho, subGrupos.Rows.Count];
                    for (int i = 0; i < tamanho; i++)
                    {
                        for (int j = 0; j < subGrupos.Rows.Count; j++)
                        {
                            Cardio[i, j] = 0;
                        }
                    }

                    rw = 0;
                    if (subGrupos != null && subGrupos.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subGrupos.Rows)
                        {
                            int codEvento = Convert.ToInt32(subRow["Evento"]);
                            var query = GlobVar.eventos.AsEnumerable()
                                        .Where(row => row.Field<int>("CodEvento") == codEvento);

                            DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                            if (EventosSub.Rows.Count > 0)
                            {
                                foreach (DataRow eventRow in EventosSub.Rows)
                                {
                                    Cardio[Convert.ToInt32(eventRow["NumPag"]), rw] = 1;
                                }
                            }
                            rw++;
                        }
                    }
                    subGrupos.Dispose();
                    break;
                // PLM
                case 5:
                    subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .CopyToDataTable();
                    subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                    plm = new int[tamanho];
                    Array.Clear(plm, 0, plm.Length);

                    if (subGrupos != null && subGrupos.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subGrupos.Rows)
                        {
                            int codEvento = Convert.ToInt32(subRow["Evento"]);
                            var query = GlobVar.eventos.AsEnumerable()
                                        .Where(row => row.Field<int>("CodEvento") == codEvento);

                            DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                            if (EventosSub.Rows.Count > 0)
                            {
                                foreach (DataRow eventRow in EventosSub.Rows)
                                {
                                    plm[Convert.ToInt32(eventRow["NumPag"])] = 1;
                                }
                            }
                        }
                    }
                    subGrupos.Dispose();
                    break;
                // Ronco
                case 10:
                    subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .CopyToDataTable();
                    subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                    ronco = new int[tamanho];
                    Array.Clear(ronco, 0, ronco.Length);

                    if (subGrupos != null && subGrupos.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subGrupos.Rows)
                        {
                            int codEvento = Convert.ToInt32(subRow["Evento"]);
                            var query = GlobVar.eventos.AsEnumerable()
                                        .Where(row => row.Field<int>("CodEvento") == codEvento);

                            DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                            if (EventosSub.Rows.Count > 0)
                            {
                                foreach (DataRow eventRow in EventosSub.Rows)
                                {
                                    ronco[Convert.ToInt32(eventRow["NumPag"])] = 1;
                                }
                            }
                        }
                    }
                    subGrupos.Dispose();
                    break;
                // Bruxismo
                case 40:
                    subGrupos = GlobVar.tbl_HipnoSubGrupos.AsEnumerable()
                                .Where(row => row.Field<int>("CodGrupo") == codGrupo)
                                .CopyToDataTable();
                    subGrupos.AsEnumerable().OrderBy(row => row.Field<int>("CodSubGrupo"));

                    Bruxismo = new int[tamanho, subGrupos.Rows.Count];
                    for (int i = 0; i < tamanho; i++)
                    {
                        for (int j = 0; j < subGrupos.Rows.Count; j++)
                        {
                            Bruxismo[i, j] = 0;
                        }
                    }

                    rw = 0;
                    if (subGrupos != null && subGrupos.Rows.Count > 0)
                    {
                        foreach (DataRow subRow in subGrupos.Rows)
                        {
                            int codEvento = Convert.ToInt32(subRow["Evento"]);
                            var query = GlobVar.eventos.AsEnumerable()
                                        .Where(row => row.Field<int>("CodEvento") == codEvento);

                            DataTable EventosSub = query.Any() ? query.CopyToDataTable() : new DataTable();

                            if (EventosSub.Rows.Count > 0)
                            {
                                foreach (DataRow eventRow in EventosSub.Rows)
                                {
                                    Bruxismo[Convert.ToInt32(eventRow["NumPag"]), rw] = 1;
                                }
                            }
                            rw++;
                        }
                    }
                    subGrupos.Dispose();
                    break;

                // ------- Sinais graafio -------
                // SA02
                case 6:
                    SA02 = new int[tamanho];
                    codcanal = 66;
                    codindex = GlobVar.codSelected.IndexOf(codcanal);
                    // Verifica se o índice existe
                    if (codindex >= 0 && codindex < GlobVar.grafSelected.Length)
                    {
                        // Loop para acumular valores
                        h = 0;
                        for (int g = 0; g < GlobVar.matrizCanal.GetLength(1); g += GlobVar.namosNumerico)
                        {
                            if (h < tamanho)
                            {
                                // Acumula os valores correspondentes
                                SA02[h] = (int)GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
                            }
                            h++;
                        }
                    }
                    break;
                // Freq Card
                case 12:
                    FreqCard = new int[tamanho];
                    codcanal = 67;
                    codindex = GlobVar.codSelected.IndexOf(codcanal);

                    h = 0;
                    for (int g = 0; g < GlobVar.matrizCanal.GetLength(1);)
                    {
                        if (h < tamanho)
                        {
                            FreqCard[h] = (int)(int)GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
                        }
                        h++;
                        g += GlobVar.namosNumerico;
                    }
                    break;
                // Microfone
                case 19:
                    Microfone = new int[tamanho];
                    codcanal = 5;
                    codindex = GlobVar.codSelected.IndexOf(codcanal);
                    media = new int[GlobVar.namos];
                    // Aplica o filtro band-pass nos dados
                    //float[] linhaFiltrada = (GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex]));
                    float[] linhaFiltrada = BandPass.ApplyFilter((GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex])), 40f, 120f, 512);
                    //linhaFiltrada = PaissaBaixa.ApplyFilter(linhaFiltrada, 40f, 1);

                    double scala = GlobVar.scale[GlobVar.grafSelected[codindex]];
                    h = 0; // Índice para o array Microfone
                    for (int g = 0; g < GlobVar.matrizCanal.GetLength(1);)
                    {
                        for (int a = 0; a < media.Length && g < GlobVar.matrizCanal.GetLength(1); a++)
                        {
                            // Garante que não ultrapasse os limites da matriz
                            int valor = (int)(linhaFiltrada[g]);

                            // Trata o caso de int.MinValue
                            if (valor == int.MinValue)
                            {
                                media[a] = int.MaxValue; // Substitui por int.MaxValue ou outro valor adequado
                            }
                            else
                            {
                                media[a] = Math.Abs(valor);
                            }
                            g++;
                        }

                        // Verifica se existem valores válidos em 'media' antes de calcular a mediana
                        if (media.Length > 0)
                        {
                            Microfone[h] = Convert.ToInt32(media.Median());
                        }
                        h++;
                    }
                    //Microfone = LeituraEmMatrizTeste.FloatToInt(BandPass.ApplyFilter(LeituraEmMatrizTeste.IntToFloat(Microfone), 40f, 120f, 1));

                    break;

                // ---------- Cpap Fami ---------
                // CPAP
                case 11:
                    CPAP = new int[tamanho];
                    codcanal = 65;
                    int canalIndex = 0;
                    int LimiteInferior = 0;
                    int LimiteSuperior = 0;

                    codindex = GlobVar.codSelected.IndexOf(codcanal);
                    if (codindex != -1)
                    {
                        canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                    }
                    else
                    {
                        var rwcp = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                    .Where(row => row.Field<int>("CodTipoCanal") == 15)
                                    .FirstOrDefault(); // Pega a primeira linha correspondente
                        if (rwcp == null)
                        {
                            break;
                        }

                        codcanal = Convert.ToInt32(rwcp["CodCanal1"]);

                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                        LimiteInferior = Convert.ToInt32(rwcp["LimiteInferior"]);
                        LimiteSuperior = Convert.ToInt32(rwcp["LimiteSuperior"]);

                    }

                    int ponteiroI = GlobVar.ponteiroI[canalIndex];
                    int ponteiroF = GlobVar.ponteiroF[canalIndex];

                    int indexx = GlobVar.codCanal.IndexOf(codcanal);
                    int Taxa = GlobVar.txPorCanal[indexx];
                    int aoh = 0;
                    h = 0;
                    /*
                    // Loop para acumular valores
                    h = 0;
                    for (int g = 0; g < GlobVar.matrizCanal.GetLength(1); g += Taxa)
                    {
                        if (h < tamanho)
                        {
                            // Acumula os valores correspondentes
                            CPAP[h] = GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
                        }
                        h++;
                    }
                    */
                    // Caso sem segundo canal
                    for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        int colunaComp = ponteiroI;
                        while (colunaComp < ponteiroF)
                        {
                            CPAP[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            colunaComp += Taxa;
                            h++;
                        }
                    }

                    if (codcanal != 65)
                    {
                        var dataToFilter = CPAP;
                        int lmAnaloInf = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteInf_Anal"]);
                        int lmAnaloSup = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Pressao_LimiteSup_Anal"]);

                        dataToFilter = DigiToAnalo(CPAP, LimiteInferior, LimiteSuperior, lmAnaloInf, lmAnaloSup);

                        Array.Copy(dataToFilter, 0, CPAP, 0, dataToFilter.Length);

                    }

                    break;

                //cpap vazamento
                case 18:
                    CPAPVaz = new int[tamanho];
                    int canalIndexvz = 0;
                    int LimiteInferiorvz = 0;
                    int LimiteSuperiorvz = 0;

                    var rwvz = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                .Where(row => row.Field<int>("CodTipoCanal") == 28)
                                .FirstOrDefault(); // Pega a primeira linha correspondente
                    if (rwvz == null)
                    {
                        break;
                    }

                    codcanal = Convert.ToInt32(rwvz["CodCanal1"]);

                    codindex = GlobVar.codSelected.IndexOf(codcanal);
                    canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                    LimiteInferiorvz = Convert.ToInt32(rwvz["LimiteInferior"]);
                    LimiteSuperiorvz = Convert.ToInt32(rwvz["LimiteSuperior"]);

                    ponteiroI = GlobVar.ponteiroI[canalIndex];
                    ponteiroF = GlobVar.ponteiroF[canalIndex];

                    indexx = GlobVar.codCanal.IndexOf(codcanal);
                    Taxa = GlobVar.txPorCanal[indexx];
                    aoh = 0;
                    h = 0;

                    for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        int colunaComp = ponteiroI;
                        while (colunaComp < ponteiroF)
                        {
                            CPAPVaz[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            colunaComp += Taxa;
                            h++;
                        }
                    }
                    var dataToFiltervz = CPAPVaz;
                    int lmAnaloInfvz = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteInf_Anal"]);
                    int lmAnaloSupvz = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CPAP_Vazam_LimiteSup_Anal"]);

                    dataToFiltervz = DigiToAnalo(CPAPVaz, LimiteInferiorvz, LimiteSuperiorvz, lmAnaloInfvz, lmAnaloSupvz);

                    Array.Copy(dataToFiltervz, 0, CPAPVaz, 0, dataToFiltervz.Length);

                    break;

                case 39:
                    CO2_Exal = new int[tamanho];
                    codcanal = 73;

                    int canalindexC = 0;
                    int LimiteInferiorC = 0;
                    int LimiteSuperiorC = 0;
                    codindex = GlobVar.codSelected.IndexOf(codcanal);
                    if (codindex != -1)
                    {
                        canalindexC = GlobVar.codCanal.IndexOf(codcanal);
                    }
                    else
                    {
                        var rwcp = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                    .Where(row => row.Field<int>("CodTipoCanal") == 38)
                                    .FirstOrDefault(); // Pega a primeira linha correspondente
                        if (rwcp == null)
                        {
                            break;
                        }
                        codcanal = Convert.ToInt32(rwcp["CodCanal1"]);

                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        canalindexC = GlobVar.codCanal.IndexOf(codcanal);
                        LimiteInferiorC = Convert.ToInt32(rwcp["LimiteInferior"]);
                        LimiteSuperiorC = Convert.ToInt32(rwcp["LimiteSuperior"]);

                    }

                    int ponteiroIC = GlobVar.ponteiroI[canalindexC];
                    int ponteiroFC = GlobVar.ponteiroF[canalindexC];

                    int indexxC = GlobVar.codCanal.IndexOf(codcanal);
                    int TaxaC = GlobVar.txPorCanal[indexxC];
                    int aoC = 0;
                    h = 0;

                    for (int linha = 0; linha < GlobVar.matrizCompleta.GetLength(0); linha++)
                    {
                        int colunaComp = ponteiroIC;
                        while (colunaComp < ponteiroFC)
                        {
                            CO2_Exal[h] = (short)GlobVar.matrizCompleta[linha, colunaComp];
                            colunaComp += TaxaC;
                            h++;
                        }
                    }
                    //Parte para conversar de dig para analo se precisar
                    if (codcanal != 73)
                    {
                        var dataToFilter = CO2_Exal;
                        int lmAnaloInf = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteInf_Anal"]);
                        int lmAnaloSup = Convert.ToInt32(GlobVar.tbl_DadosExame.Rows[0]["CapnoEtCO2_LimiteSup_Anal"]);

                        dataToFilter = DigiToAnalo(CO2_Exal, LimiteInferiorC, LimiteSuperiorC, lmAnaloInf, lmAnaloSup);

                        Array.Copy(dataToFilter, 0, CO2_Exal, 0, dataToFilter.Length);

                    }

                    break;

                // ------- Posi / Estagio -------
                // Posicao
                case 7:
                    posicao = new int[tamanho];
                    codcanal = 14;
                    codindex = GlobVar.codSelected.IndexOf(codcanal);

                    h = 0;
                    for (int g = 0; g < GlobVar.matrizCanal.GetLength(1);)
                    {
                        if (h < tamanho)
                        {
                            posicao[h] += (int)(int)(GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g] * -1);
                        }
                        h++;
                        g += GlobVar.namosNumerico;
                    }
                    for (int aq = 0; aq < posicao.Length; aq++)
                    {
                        if (posicao[aq] >= (GlobVar.PosCima - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosCima + GlobVar.PosIncremento)) // CIMA
                        {
                            posicao[aq] = 3;
                        }
                        else if (posicao[aq] >= (GlobVar.PosDireita - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosDireita + GlobVar.PosIncremento)) // DIREITA
                        {
                            posicao[aq] = 2;
                        }
                        else if (posicao[aq] >= (GlobVar.PosEsquerda - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosEsquerda + GlobVar.PosIncremento)) // ESQUERDA
                        {
                            posicao[aq] = 1;
                        }
                        else if (posicao[aq] >= (GlobVar.PosBaixo - GlobVar.PosIncremento) && posicao[aq] <= (GlobVar.PosBaixo + GlobVar.PosIncremento)) //BAIXO
                        {
                            posicao[aq] = 0;
                        }
                        else
                        {
                            posicao[aq] = 3;
                        }
                    }
                    break;
                // Estagios
                case 9:
                    estagio = new int[tamanho];

                    h = 0;
                    foreach (DataRow rowEstagio in GlobVar.tbl_Paginas.Rows)
                    {
                        if (h < tamanho)
                        {
                            estagio[h] = Convert.ToInt32(rowEstagio["Estagio"]);
                        }
                        h++;
                    }
                    break;

                // Horario
                case 21:
                    Horario = true;

                    // Chama o método para filtrar horários completos
                    DataTable horario = FiltrarHorariosCompletos(GlobVar.tbl_Paginas);

                    // Agora o DataTable 'horario' contém apenas as linhas com horários completos
                    break;
            }

        }
        public void ClicaMostraGraf(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menuItem)
            {
                if (menuItem.Tag != null)
                {
                    if (!menuItem.Checked)
                    {
                        int tag = Convert.ToInt32(menuItem.Tag);
                        bool temograf = MontagemJanela.AsEnumerable().Any(row => row.Field<int>("CodGrupo") == tag);
                        if (temograf)
                        {
                            // Encontra a linha correspondente
                            DataRow rowParaRemover = MontagemJanela.AsEnumerable()
                                                                   .FirstOrDefault(row => row.Field<int>("CodGrupo") == tag);

                            if (rowParaRemover != null)
                            {
                                MontagemJanela.Rows.Remove(rowParaRemover); // Remove a linha
                                reajustaPorc();
                                Desenha();
                            }
                        }
                    }
                    else
                    {

                        int tag = Convert.ToInt32(menuItem.Tag);
                        if (VerificaDados(tag))
                        {
                            // Criar uma nova linha no DataTable
                            DataRow novaLinha = MontagemJanela.NewRow();

                            // Preencher os valores conforme solicitado
                            novaLinha["CodJanela"] = 2;
                            novaLinha["CodGrupo"] = tag;

                            int ultimaLinhaIndex = MontagemJanela.Rows.Count - 1;

                            if (ultimaLinhaIndex >= 0) // Garante que há pelo menos uma linha no DataTable
                            {
                                int codUltimaLinha = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["CodGrupo"]);

                                if (codUltimaLinha == 21)
                                {
                                    // Define a nova linha com a mesma ordem da última linha
                                    novaLinha["Ordem"] = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"]);

                                    // Atualiza a última linha, incrementando a ordem
                                    MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"] = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"]) + 1;

                                }
                                else
                                {
                                    // Se não for 21, apenas incrementa a ordem
                                    novaLinha["Ordem"] = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"]) + 1;
                                }
                            }
                            else
                            {
                                // Se for a primeira linha, define a ordem como 1
                                novaLinha["Ordem"] = 1;
                            }

                            novaLinha["Porc"] = 7.50;

                            int li = 0;
                            if (tag == 6) { li = 80; }
                            else if (tag == 12) { li = 40; }
                            else if (tag == 11) { li = 0; }
                            else if (tag == 18) { li = 0; }
                            else if (tag == 39) { li = 20; }
                            else { li = 0; }
                            novaLinha["LI"] = li;

                            int ls = 10;
                            if (tag == 6 || tag == 12) { ls = 100; }
                            else if (tag == 11) { ls = 20; }
                            else if (tag == 18) { ls = 120; }
                            else if (tag == 39) { ls = 60; }
                            else { ls = 0; }
                            novaLinha["LS"] = ls;

                            if (tag == 11 || tag == 18)
                            {
                                novaLinha["LinhasInternas"] = 1;
                                novaLinha["DivisoesLegendas"] = 2;
                            }
                            else { novaLinha["LinhasInternas"] = 0; novaLinha["DivisoesLegendas"] = 0; }

                            novaLinha["CorGrafico"] = 0;

                            // Adicionar a nova linha ao DataTable
                            MontagemJanela.Rows.Add(novaLinha);
                            // Reordena o DataTable manualmente
                            DataView dv = MontagemJanela.DefaultView;
                            dv.Sort = "Ordem ASC"; // Ordena pela coluna "Ordem"
                            MontagemJanela = dv.ToTable(); // Cria um novo DataTable ordenado

                            AdicionaOsDadosCasoNullo(tag);
                            reajustaPorc();
                            Desenha();
                        }
                        else
                        {
                            AdicionaOsDadosCasoNullo(tag);
                            // Criar uma nova linha no DataTable
                            DataRow novaLinha = MontagemJanela.NewRow();

                            // Preencher os valores conforme solicitado
                            novaLinha["CodJanela"] = 2;
                            novaLinha["CodGrupo"] = tag;

                            int ultimaLinhaIndex = MontagemJanela.Rows.Count - 1;

                            if (ultimaLinhaIndex >= 0) // Garante que há pelo menos uma linha no DataTable
                            {
                                int codUltimaLinha = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["CodGrupo"]);

                                if (codUltimaLinha == 21)
                                {
                                    // Define a nova linha com a mesma ordem da última linha
                                    novaLinha["Ordem"] = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"]);

                                    // Atualiza a última linha, incrementando a ordem
                                    MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"] = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"]) + 1;

                                }
                                else
                                {
                                    // Se não for 21, apenas incrementa a ordem
                                    novaLinha["Ordem"] = Convert.ToInt32(MontagemJanela.Rows[ultimaLinhaIndex]["Ordem"]) + 1;
                                }
                            }
                            else
                            {
                                // Se for a primeira linha, define a ordem como 1
                                novaLinha["Ordem"] = 1;
                            }

                            novaLinha["Porc"] = 7.50;

                            int li = 0;
                            if (tag == 6) { li = 80; }
                            else if (tag == 12) { li = 40; }
                            else if (tag == 11) { li = 0; }
                            else if (tag == 18) { li = 0; }
                            else if (tag == 39) { li = 20; }
                            else { li = 0; }
                            novaLinha["LI"] = li;

                            int ls = 10;
                            if (tag == 6 || tag == 12) { ls = 100; }
                            else if (tag == 11) { ls = 20; }
                            else if (tag == 18) { ls = 120; }
                            else if (tag == 39) { ls = 60; }
                            else { ls = 0; }
                            novaLinha["LS"] = ls;
                            if (tag == 11 || tag == 18)
                            {
                                novaLinha["LinhasInternas"] = 1;
                                novaLinha["DivisoesLegendas"] = 2;
                            }
                            else { novaLinha["LinhasInternas"] = 0; novaLinha["DivisoesLegendas"] = 0; }

                            novaLinha["CorGrafico"] = 0;


                            // Adicionar a nova linha ao DataTable
                            MontagemJanela.Rows.Add(novaLinha);
                            // Reordena o DataTable manualmente
                            DataView dv = MontagemJanela.DefaultView;
                            dv.Sort = "Ordem ASC"; // Ordena pela coluna "Ordem"
                            MontagemJanela = dv.ToTable(); // Cria um novo DataTable ordenado

                            reajustaPorc();
                            Desenha();
                        }
                    }
                }
            }
        }
        private void ImprimeTela_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF File (*.pdf)|*.pdf|Bitmap Image (*.bmp)|*.bmp";
                saveFileDialog.Title = "Salvar como";
                saveFileDialog.FileName = "Arquivo";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Focus(); // Garante que o formulário esteja visível antes da captura
                    System.Threading.Thread.Sleep(200); // Pequeno delay para evitar capturar a caixa de diálogo

                    // Captura apenas a área do formulário, sem a barra de título
                    Rectangle bounds = new Rectangle(this.Location, this.ClientSize);
                    Bitmap bmp = new Bitmap(bounds.Width, bounds.Height);

                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(this.PointToScreen(Point.Empty), Point.Empty, bounds.Size);
                    }

                    string fileExtension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                    if (fileExtension == ".pdf")
                    {
                        SalvarComoPDF(saveFileDialog.FileName, bmp);
                    }
                    else if (fileExtension == ".bmp")
                    {
                        bmp.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }

                    bmp.Dispose();
                    MessageBox.Show("Arquivo salvo com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void SalvarComoPDF(string caminhoArquivo, Bitmap imagem)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Relatório";

            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Coletando informações do exame com validação correta
            string nome = GlobVar.tbl_DadosExame.Rows[0]["Nome"]?.ToString() ?? "Paciente";
            string sexo = $"({GlobVar.tbl_DadosExame.Rows[0]["Sexo"]?.ToString() ?? "N/D"})";
            string idade = $"{GlobVar.tbl_DadosExame.Rows[0]["IdadeAno"] ?? "?"} anos";
            string altura = $"{GlobVar.tbl_DadosExame.Rows[0]["Altura"]?.ToString() ?? "?"}m";
            string realizacao = GlobVar.tbl_DadosExame.Rows[0]["DataRealizacao"]?.ToString().Substring(0, 10) ?? "Data não disponível";
            string arquivo = $"{(GlobVar.textFile?.Length >= 24 ? GlobVar.textFile.Substring(12, 12) : "Arquivo N/D")}";

            string tituloPrincipal = $"iCelera - {nome} {sexo} {idade} - {altura} - Realização: {realizacao} - Arquivo: {arquivo}";

            string medicoSolicitante = GlobVar.tbl_DadosExame.Rows[0]["MedicoSolicitante"] != DBNull.Value && GlobVar.tbl_DadosExame.Rows[0]["MedicoSolicitante"] != null ?
                                       GlobVar.tbl_DadosExame.Rows[0]["MedicoSolicitante"].ToString() : "Informe o nome do médico";
            string modeloEquipamento = GlobVar.tbl_DadosExame.Rows[0]["ModeloEquipamento"] != DBNull.Value && GlobVar.tbl_DadosExame.Rows[0]["ModeloEquipamento"] != null ?
                                       GlobVar.tbl_DadosExame.Rows[0]["ModeloEquipamento"].ToString() : "Informe o nome da clínica";

            // Desenhando textos no PDF
            gfx.DrawString(tituloPrincipal, new XFont("Arial", 12), XBrushes.Black, new XPoint(40, 20));
            gfx.DrawString(medicoSolicitante, new XFont("Arial", 10), XBrushes.Black, new XPoint(40, 35));
            gfx.DrawString(modeloEquipamento, new XFont("Arial", 10), XBrushes.Black, new XPoint(40, 45));

            // Converter a imagem para XImage e desenhar no PDF
            using (MemoryStream stream = new MemoryStream())
            {
                imagem.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                XImage xImage = XImage.FromStream(stream);

                double scaleFactor = Math.Min((page.Width - 80) / xImage.PixelWidth, (page.Height - 160) / xImage.PixelHeight);
                double width = xImage.PixelWidth * scaleFactor;
                double height = xImage.PixelHeight * scaleFactor;

                double posX = (page.Width - width) / 2;
                double posY = 100;

                gfx.DrawImage(xImage, posX, posY, width, height);
            }

            document.Save(caminhoArquivo);
            document.Close();
        }
        public static double ConverterYParaX(double y, double yMin, double yMax, double xMin, double xMax)
        {
            // Verifica se yMin e yMax são diferentes para evitar divisão por zero
            if (yMin == yMax)
            {
                throw new ArgumentException("Erro: yMin e yMax não podem ser iguais, pois isso resultaria em uma divisão por zero.");
            }

            // Regra de três para mapear o valor de Y para X
            double x = ((y - yMin) * (xMax - xMin) / (yMax - yMin)) + xMin;

            return x;
        }
        public static int[] DigiToAnalo(int[] toUp, int lmInf, int lmSup, int lmAnaloInf, int lmAnaloSup)
        {
            // Verifica se os limites analógicos são iguais para evitar divisão por zero
            if (lmAnaloInf == lmAnaloSup)
            {
                throw new ArgumentException("Erro: lmAnaloInf e lmAnaloSup não podem ser iguais, pois isso resultaria em uma divisão por zero.");
            }

            int[] aoba = new int[toUp.Length];

            for (int i = 0; i < aoba.Length; i++)
            {
                // Correção: Pegando o valor correto de toUp[i]
                toUp[i] *= -1;
                double y;//= ((toUp[i] - lmAnaloInf) * (lmSup - lmInf) / (double)(lmAnaloSup - lmAnaloInf)) + lmInf;

                y = lmInf + Math.Abs(toUp[i] - lmAnaloInf) / Math.Abs((lmAnaloSup - lmAnaloInf) / (lmSup - lmInf));

                aoba[i] = (int)y;// (int)Math.Round(y, 4);
            }

            return aoba;
        }
        public void CalculaCargaHipoxica()
        {
            string arq = Path.Combine(GlobVar.basePath, "Retorno/Dessat.txt");
            using (StreamWriter writer = new StreamWriter(arq, false, Encoding.Default))
            {
                // Leitura/Gravação no INI
                IniFile ini = new IniFile(Path.Combine(GlobVar.basePath, "Config.ini"));
                string cargaHipo = ini.Read("CARGAHIPO", "DIRETORIOS");
                if (string.IsNullOrWhiteSpace(cargaHipo))
                {
                    ini.Write("CARGAHIPO", "1", "DIRETORIOS");
                }
                //string sql = "SELECT * FROM tbl_Eventos WHERE CodEvento = 17 ORDER BY NumPag";
                var eventosQuery = GlobVar.eventos.AsEnumerable()
                    .Where(row => row.Field<int>("CodEvento") == 17)
                    .OrderBy(row => row.Field<int>("NumPag"));
                var eventosTestQuery = GlobVar.eventosUpdate.AsEnumerable()
                    .Where(row => row.Field<int>("CodEvento") == 17)
                    .OrderBy(row =>
                    {
                        // 1. Lê a coluna como string
                        string numPagStr = row.Field<string>("NumPag") ?? "";

                        // 2. Separa pelo delimitador "--"
                        var partes = numPagStr.Split(new[] { "--" }, StringSplitOptions.None);

                        // 3. Pega e limpa a primeira parte (antes do "--")
                        string primeiraParte = partes[0].Trim();

                        // 4. Tenta converter para int
                        if (int.TryParse(primeiraParte, out int numero))
                        { 
                            return numero;
                        }
                        else
                        {
                            // Define o que fazer se não for número válido. Aqui retorna 0.
                            return 0;
                        }
                    });

                DataTable eventos;
                DataTable eventosTest;
                if (eventosQuery.Any())
                {
                    eventos = eventosQuery.CopyToDataTable(); // ou new DataTable()
                    eventosTest = eventosTestQuery.CopyToDataTable();

                    // Pré-ordenar os eventos para garantir o agrupamento por seq (e ordem de páginas)
                    DataView view = new DataView(eventos);
                    view.Sort = "seq ASC, NumPag ASC";
                    DataTable ev = view.ToTable();

                    // Localizar o canal de SpO2 (código 66) e obter o ponteiro
                    int linhaSaturacao = GlobVar.codCanal.IndexOf(66);
                    if (linhaSaturacao < 0)
                        throw new InvalidOperationException("Canal SpO2 (código 66) não encontrado em GlobVar.codCanal.");

                    int ponteiro = GlobVar.ponteiroI[linhaSaturacao];

                    // Limites da matriz para proteção
                    int maxPag = GlobVar.matrizCompleta.GetLength(0) - 1;

                    // Função auxiliar para obter o valor de SpO2 de forma segura
                    int GetValorSao2(int numPag)
                    {
                        if (numPag < 0) numPag = 0;
                        if (numPag > maxPag) numPag = maxPag;
                        // Se sua matriz for double[,], ajuste o Convert conforme necessário
                        return Convert.ToInt32(GlobVar.matrizCompleta[numPag, ponteiro]);
                    }

                    double acum = 0.0;
                    double carga = 0.0;

                    if (ev.Rows.Count > 0)
                    {
                        int idx = 0;

                        while (idx < ev.Rows.Count)
                        {
                            int seqAtual = Convert.ToInt32(ev.Rows[idx]["seq"]);
                            int dur = 0;
                            int iniValor = 0;
                            int fim = 0;

                            // Percorre o grupo com o mesmo seq
                            while (idx < ev.Rows.Count && Convert.ToInt32(ev.Rows[idx]["seq"]) == seqAtual)
                            {
                                int numPag = Convert.ToInt32(ev.Rows[idx]["NumPag"]);

                                if (dur == 0)
                                {
                                    // Primeiro item do grupo: pega valor inicial na página anterior (NumPag - 1)
                                    iniValor = GetValorSao2(numPag - 1);
                                }
                                // Atualiza 'fim' SEMPRE com o valor da página atual (garante correto mesmo com grupo de 1 item)
                                fim = GetValorSao2(numPag);
                                dur++;
                                idx++;
                            }
                            // Calcula carga igual ao VB6 (divisão em double)
                            double calcCarga = 0.5 * (dur / 60.0) * Math.Abs(iniValor - fim);

                            // Escreve a linha no mesmo formato
                            writer.WriteLine($"{iniValor} - {fim} - {dur} ====== {calcCarga:0.0000}");

                            carga = calcCarga;
                            acum += carga;
                        }
                    }
                    //sql = "SELECT * FROM tbl_Paginas";
                    //DataTable paginas = obj_dbconfig.ExecutaSQL(cnn_dbExame, sql);
                    int pags = GlobVar.tbl_Paginas.Rows.Count;
                    int totalMinutos = pags / 60;

                    //sql = "SELECT * FROM tbl_DadosExame";
                    //DataTable dadosExame = obj_dbconfig.ExecutaSQLParaAlteracao(cnn_dbExame, sql);
                    if (GlobVar.tbl_DadosExame != null)
                    {
                        if (!GlobVar.tbl_DadosExame.Columns.Contains("CargaHipoxica"))
                        {
                            GlobVar.tbl_DadosExame.Columns.Add("CargaHipoxica", typeof(double)).DefaultValue = 0.0;
                        }

                        DataRow row = GlobVar.tbl_DadosExame.Rows[0];
                        row["CargaHipoxica"] = (acum / totalMinutos) * 60;
                        AlteraBD.SalvarAlteracoes(); // Método para alterar no Banco de Dados a tbl_DadosExame
                    }
                }
            }
        }

        static bool laudosplitnight = false;
        static bool laudosegmentos = false;

        public static int passagem = 0;
        static int ultimapassagem = 0;
        static int segmentos = 0;
        static List<object> lst_segmentos = new();
        static string g_variaveislaudo = "";
        static string g_arq_exame = "";

        public static Microsoft.Office.Interop.Word.Application wordApp;
        public static Microsoft.Office.Interop.Word.Document doc;

        public static Microsoft.Office.Interop.Excel.Application ObjExcel;
        public static Microsoft.Office.Interop.Excel.Workbook planExcel;
        private void CriarLaudo_Click(object sender, System.EventArgs e)
        {
            try
            {
                int pag_noite = Canais.Get_BoaNoite();
                int pag_dia = Canais.Get_BomDia();


                CalculaCargaHipoxica();

                laudosplitnight = TextoComboContem("SPLIT NIGHT");
                laudosegmentos = TextoComboContem("SEGMENTOS");

                if (GlobVar.eventos.AsEnumerable().Any(row => row.Field<int>("CodEvento") == 50) && (TextoComboContem("SPLIT NIGHT") || TextoComboContem("SEGMENTOS")))
                {
                    passagem = 3;
                    ultimapassagem = 3;
                }
                else
                {
                    passagem = 1;
                }

                lst_segmentos.Clear();
                if (TextoComboContem("SEGMENTOS"))
                {
                    segmentos = 0;
                    foreach (DataRow row in GlobVar.tbl_Comentarios.Rows)
                    {
                        string comentario = row["Comentario"]?.ToString();
                        if (!string.IsNullOrEmpty(comentario) && comentario.StartsWith("#"))
                        {
                            string txt = comentario.Substring(1); // Remove o primeiro '#'
                            int pos = txt.IndexOf("#");
                            if (pos > 0)
                            {
                                txt = txt.Substring(0, pos);
                                if (int.TryParse(txt, out int valorTexto))
                                {
                                    if (valorTexto > segmentos)
                                        segmentos = valorTexto;

                                    int numPag = Convert.ToInt32(row["NumPag"]);
                                    lst_segmentos.Add(numPag.ToString("D7")); // "0000000" formato
                                }
                            }
                        }
                    }

                    segmentos++; // segmentos = segmentos + 1;

                    if (segmentos > passagem)
                    {
                        passagem = segmentos;
                        ultimapassagem = segmentos;
                    }
                }

                string loc = Path.GetDirectoryName(GlobVar.bDataFile);
                string arq_exame = Path.GetFileNameWithoutExtension(GlobVar.bDataFile);


                g_arq_exame = Path.Combine(loc, arq_exame + " Laudo.DOC");

                if (File.Exists(g_arq_exame))
                {
                    if (ArquivoAberto(g_arq_exame))
                    {
                        MessageBox.Show(f_var("Var58073"), f_var("Var26063") + "!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (MessageBox.Show(f_var("Var58005"), f_var("Var26063") + "!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return;
                    }
                }

                g_variaveislaudo = "";
                arrumaTbl();

                if (!TextoComboContem("CALIBRACAO"))
                {
                    // Filtra os eventos com estagio = 0 na página, e CodEvento diferente dos listados
                    var eventosEstagio0 = from evento in GlobVar.Cons_Eventos.AsEnumerable()
                                          join pagina in GlobVar.tbl_Paginas.AsEnumerable()
                                            on evento.Field<int>("Pag_Ini") equals pagina.Field<int>("NumPag")
                                          where pagina.Field<int>("Estagio") == 0
                                             && !new[] { 18, 19, 100, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119 }
                                                 .Contains(evento.Field<int>("CodEvento"))
                                          group evento by evento.Field<int>("CodEvento") into g
                                          select new
                                          {
                                              CodEvento = g.Key,
                                              Cont = g.Count()
                                          };

                    if (eventosEstagio0.Any())
                    {
                        Fechar.Enabled = false;

                        string textoEventos = "";

                        foreach (var item in eventosEstagio0)
                        {
                            var descricao = GlobVar.tbl_CadEvento.AsEnumerable()
                                              .FirstOrDefault(c => c.Field<int>("CodEvento") == item.CodEvento)?
                                              .Field<string>("DescrEvento");

                            if (!string.IsNullOrEmpty(descricao))
                                textoEventos += $"{descricao} - {item.Cont}\r\n";
                        }

                        lbl_VerEventos.Tag = textoEventos;

                        CriarPainelMensagem();

                        pnl_Msg2.Left = (this.Width - pnl_Msg2.Width) / 2;
                        pnl_Msg2.Top = (this.Height - pnl_Msg2.Height) / 2;
                        pnl_Msg2.Visible = true;

                        while (pnl_Msg2.Visible)
                            System.Windows.Forms.Application.DoEvents();

                        int tag = (int)cmd_msg2.GetType().GetProperty("Tag").GetValue(cmd_msg2);

                        if (tag == 1)
                        {
                            // Deleta eventos conforme critérios
                            var eventosParaExcluir = from evento in GlobVar.Cons_Eventos.AsEnumerable()
                                                     join pagina in GlobVar.tbl_Paginas.AsEnumerable()
                                                       on evento.Field<int>("Pag_Ini") equals pagina.Field<int>("NumPag")
                                                     where pagina.Field<int>("estagio") == 0
                                                        && evento.Field<int>("CodEvento") != 18
                                                        && evento.Field<int>("CodEvento") != 19
                                                        && evento.Field<int>("CodEvento") != 110
                                                        && evento.Field<int>("CodEvento") != 119
                                                     select evento;

                            foreach (var row in eventosParaExcluir.ToList())
                            {
                                GlobVar.Cons_Eventos.Rows.Remove(row);
                            }
                        }

                        Fechar.Enabled = true;

                        if (tag == 2)
                            return;
                    }
                }

                CriarPainelLoading();


                pnl_Loading.Visible = true;
                Application.DoEvents(); // Permite atualizar UI

                // ... aqui você faz sua tarefa longa, pode ir atualizando o texto se quiser:
                lbl_Carregando.Text = "Carregando etapas...";

                barraLoading.Value = 10; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual


                //Cursor.Current = Cursors.WaitCursor;

                g_dir_laudos = Path.Combine(GlobVar.basePath, "Laudos/");
                string nomeOrigem = Path.Combine(g_dir_laudos, comboBox1.Text + ".doc");
                nome_arq_temp = "TMP" + DateTime.Now.ToString("HHmmss");
                string caminhoTemp = Path.Combine(g_dir_laudos, nome_arq_temp + ".doc");

                // Copia o arquivo original para o temporário
                File.Copy(nomeOrigem, caminhoTemp, overwrite: true);
                string g_textolaudo = "";

                // Inicializa o Word
                wordApp = new Microsoft.Office.Interop.Word.Application();
                wordApp.Visible = false; // <- ESSENCIAL para mostrar a janela do Word

                // Abre o documento
                doc = wordApp.Documents.Open(caminhoTemp);
                doc.Activate(); // Coloca o documento em foco (opcional se já visível)

                g_textolaudo = wordApp.Selection.Text;

                barraLoading.Value += 5; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                if (!TextoComboContem("CALIBRACAO"))
                {
                    if (segmentos > 0)
                    {
                        PreparaRelatorioMDB();
                    }
                }
                for (int i = 0; i < passagem; i++)
                {
                    if (segmentos > 0 && passagem > 0)
                    {

                    }
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    string origem = Path.Combine(g_dir_laudos, "Graficos para laudo.xls");
                    string destino = Path.Combine(g_dir_laudos, nome_arq_temp + ".xls");
                    File.Copy(origem, destino, overwrite: true); // sobrescreve se já existir

                    ObjExcel = new Microsoft.Office.Interop.Excel.Application();
                    ObjExcel.Visible = false; // <- ESSENCIAL para abrir visivelmente

                    planExcel = ObjExcel.Workbooks.Open(destino);

                    passagem = i + 1;

                    if (passagem == 1)
                    {
                        pag_dia = Canais.Get_BomDia();
                        pag_noite = Canais.Get_BoaNoite();
                    }
                    else if (laudosplitnight)
                    {
                        if (passagem == 1)
                        {
                            pag_noite = Canais.Get_BoaNoite();
                            pag_dia = Canais.Get_InicioCPAP();
                        }
                        else if (passagem == 2)
                        {
                            pag_noite = Canais.Get_InicioCPAP();
                            pag_dia = Canais.Get_BomDia();
                        }
                        else if (passagem == ultimapassagem)
                        {
                            pag_noite = Canais.Get_BoaNoite();
                            pag_dia = Canais.Get_BomDia();
                        }
                    }
                    else if (laudosegmentos)
                    {
                        if (passagem == ultimapassagem)
                        {
                            pag_noite = Canais.Get_BoaNoite();
                            pag_dia = Canais.Get_BomDia();
                        }
                        else
                        {
                            pag_noite = Convert.ToInt32(lst_segmentos[passagem - 1]);

                            if (passagem == passagem - 1)
                            {
                                pag_dia = Canais.Get_BomDia();
                            }
                            else
                            {
                                pag_dia = Convert.ToInt32(lst_segmentos[passagem]) - 1;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow row in GlobVar.tbl_Comentarios.Rows)
                        {
                            string comentario = row["Comentario"]?.ToString() ?? "";

                            if (!string.IsNullOrEmpty(comentario))
                            {
                                if (comentario.StartsWith(passagem.ToString()))
                                {
                                    pag_noite = Convert.ToInt32(row["NumPag"]);
                                }
                                else if (comentario.StartsWith((passagem + 1).ToString()))
                                {
                                    pag_dia = Convert.ToInt32(row["NumPag"]) - 1;
                                }
                            }
                        }
                    }

                    if (!TextoComboContem("CALIBRACAO"))
                    {
                        VerificaDessaturacao();
                        barraLoading.Value += 5; // ou += 10, etc.
                        Application.DoEvents();  // Atualiza visual

                        if (TextoComboContem("SPLIT-NIGHT") || TextoComboContem("SPLIT NIGHT"))
                        {
                            PreparaRelatorioMDB();

                            InicializaEvRespDOC();
                            barraLoading.Value += 5; // ou += 10, etc.
                            Application.DoEvents();  // Atualiza visual

                            //Latencia multipla
                            if (passagem == 1)
                            {
                                CalculaResumoMultiplaLatencia();
                            }
                            if (TextoComboContem("RESUMO_CPAP"))
                            {
                                S_CPAP_Dados(passagem, pag_noite, pag_dia);
                            }
                            else if (TextoComboContem("RESUMO_EPAP"))
                            {
                                S_BPAP_Dados(passagem, pag_noite, pag_dia);
                            }
                            barraLoading.Value += 5; // ou += 10, etc.
                            Application.DoEvents();  // Atualiza visual

                        }
                        barraLoading.Value += 5; // ou += 10, etc.
                        Application.DoEvents();  // Atualiza visual

                        if (TextoComboContem("&(FCX_"))
                        {

                        }
                        InicializaEvRespDOC();

                        F_PreencheLaudoDOC(Path.Combine(Path.Combine(GlobVar.basePath, "Exames/", comboBox1.Text + ".doc")));
                    }
                }

                if (TextoComboContem("CALIBRACAO"))
                {
                    s_monta_relatorio_Calibracao();
                }

                barraLoading.Value = 100; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                pnl_Loading.Visible = false;
                Application.DoEvents(); // Permite atualizar UI

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void s_monta_relatorio_Calibracao()
        {
            /*
            int coluna, linha, contador = 0;
            string conteudo;
            double percentualErro, fator, valorMedido = 0;

            var grid = frm_Principal.grd_Calibracao;
            int numRows = grid.RowCount;
            int numCols = grid.ColumnCount;

            for (coluna = 0; coluna <= 6; coluna++)
            {
                for (linha = 0; linha <= 48; linha++)
                {
                    if (linha < numRows && coluna < numCols)
                    {
                        conteudo = Convert.ToString(grid[coluna, linha].Value);
                    }
                    else
                    {
                        conteudo = " ";
                    }

                    if (string.IsNullOrWhiteSpace(conteudo)) conteudo = " ";

                    if (linha == 0 && coluna > 0 && !string.IsNullOrWhiteSpace(conteudo))
                        double.TryParse(conteudo, out valorMedido);

                    SubstituiVar($"&(C{coluna:00}L{linha:00})&", double.TryParse(conteudo, out double val) ? val.ToString("0.000") : " ");

                    if (coluna > 0 && linha > 0)
                    {
                        if (double.TryParse(conteudo, out double valorAtual) && valorMedido != 0)
                        {
                            percentualErro = (valorAtual - valorMedido) / valorMedido * 100;
                            //fator = (percentualErro + Convert.ToDouble(frm_Principal.txt_Calibracao_Fator.Text)) / 100 * valorAtual;

                            SubstituiVar($"&(C{coluna:00}L{linha:00}E)&", percentualErro.ToString("0.00"));
                            SubstituiVar($"&(C{coluna:00}L{linha:00}F)&", fator.ToString("0.00000"));
                        }
                        else
                        {
                            SubstituiVar($"&(C{coluna:00}L{linha:00}E)&", " ");
                            SubstituiVar($"&(C{coluna:00}L{linha:00}F)&", " ");
                        }
                    }

                    contador++;
                    if ((int)(contador / 3.43) % 10 == 0)
                        //pgb_LaudoDoc.Value = Math.Min(100, (int)(contador / 3.43));
                }
            }

            //SubstituiVar("&(CALIBRA_NS_EQUIP)&", frm_Principal.txt_Calibracao_NS.Text);
            SubstituiVar("&(DATA)&", DateTime.Now.ToString("dd/MMM/yyyy"));
            SubstituiVar("&(DATAANO)&", DateTime.Now.AddYears(1).ToString("dd/MMM/yyyy"));

            object unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
            object missing = System.Type.Missing;
            wordApp.Selection.HomeKey(ref unit, ref missing);

            //pgb_LaudoDoc.Value = 100;

            string caminhoFinal = g_arq_exame + " - Relatório de Calibração.doc";
            wordApp.ActiveDocument.SaveAs2(caminhoFinal);

            wordApp.Visible = true;
            wordApp.Activate();

            wordApp = null;

            try
            {
                File.Delete(Path.Combine(g_dir_laudos, nome_arq_temp + ".doc"));
            }
            catch { }

            planExcel.Close(false);
            planExcel = null;
            ObjExcel = null;

            try
            {
                File.Delete(Path.Combine(g_dir_laudos, nome_arq_temp + ".xls"));
            }
            catch { }
            */
        }

        public static void CalcularDadosFrequenciaCardiaca()
        {
            int pag_noite = Canais.Get_BoaNoite(); int pag_dia = Canais.Get_BomDia();
            int qtd_registros = 0;
            int qtd_media = 0;
            int FC_MEDIA = 0, FC_MAIOR = 0, FC_MENOR = 999;
            int FC_REM_MEDIA = 0, FC_REM_MAIOR = 0;
            int FC_NREM_MEDIA = 0, FC_NREM_MAIOR = 0;
            int FC_VIGILIA_MEDIA = 0, FC_VIGILIA_MAIOR = 0;

            int[] fc_med = new int[10];
            int[] fc_qtd = new int[10];
            int[] fc_min = Enumerable.Repeat(999, 10).ToArray();
            int[] fc_max = new int[10];

            var tbl = GlobVar.tbl_Paginas.AsEnumerable()
                .Where(r => r.Field<int>("NumPag") >= pag_noite && r.Field<int>("NumPag") <= pag_dia)
                .OrderBy(r => r.Field<int>("NumPag"))
                .ToList();

            qtd_registros = pag_dia - pag_noite + 1 - tbl.Count;

            var PagDesprezadas = new HashSet<int>();
            foreach (DataRow row in GlobVar.eventos.Select("CodEvento = 100 AND CodCanal1 = 66"))
            {
                PagDesprezadas.Add(Convert.ToInt32(row["NumPag"]));
            }


            for (int i = 0; i < GlobVar.codCanal.Length; i++)
            {
                if (Convert.ToInt32(GlobVar.tbl_CanaisAdquiridos.Rows[i]["CodTipoCanal"]) == 21)
                {
                    string Freq_Media = "";
                    int Freq_Segundos = 5; // tempo de janela, ajuste conforme necessário
                    int Freq_Desvio = 30;
                    int codCanal = Convert.ToInt32(GlobVar.tbl_CanaisAdquiridos.Rows[i]["CodCanal1"]);
                    int tblIndex = 0;
                    for (int j = pag_noite; j <= pag_dia - qtd_registros; j++)
                    {
                        if (!PagDesprezadas.Contains(j) && tblIndex < tbl.Count)
                        {
                            var row = tbl[tblIndex++];
                            int Freq_Estagio = row.IsNull("estagio") ? 0 : Convert.ToInt32(row["estagio"]);

                            int valor = Canais.F_Get1ValorDoCanalFC(codCanal, j);
                            if (valor > 0 && valor < 200)
                            {
                                if (valor > 90) { valor = valor; }

                                if (Freq_Media.Length < Freq_Segundos * 4)
                                {
                                    Freq_Media += valor.ToString("000") + "#";
                                }
                                else
                                {
                                    string[] valoresStr = Freq_Media.Split('#', StringSplitOptions.RemoveEmptyEntries);
                                    int Freq_valor = valoresStr.Take(Freq_Segundos).Sum(v => Convert.ToInt32(v));

                                    double media = Freq_valor / (double)Freq_Segundos;
                                    double margem = media * (Freq_Desvio / 100.0);

                                    if (valor < media + margem && valor > media - margem)
                                    {
                                        if (Freq_Estagio >= 0 && Freq_Estagio <= 5)
                                        {
                                            Freq_Media = string.Join("#", valoresStr.Skip(1)) + "#" + valor.ToString("000") + "#";
                                            valor = (int)Math.Round(media);

                                            FC_MEDIA += valor;
                                            qtd_media++;

                                            if (valor > FC_MAIOR) FC_MAIOR = valor;
                                            if (valor > 20 && valor < FC_MENOR) FC_MENOR = valor;

                                            fc_med[Freq_Estagio] += valor;
                                            fc_qtd[Freq_Estagio]++;
                                            if (valor > fc_max[Freq_Estagio]) fc_max[Freq_Estagio] = valor;
                                            if (valor > 20 && valor < fc_min[Freq_Estagio]) fc_min[Freq_Estagio] = valor;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    FC_MEDIA = qtd_media > 0 ? FC_MEDIA / qtd_media : 0;

                    FC_REM_MEDIA = fc_qtd[5] > 0 ? (int)Math.Round(fc_med[5] / (double)fc_qtd[5]) : 0;
                    FC_REM_MAIOR = fc_max[5];

                    int qtd_nrem = fc_qtd[1] + fc_qtd[2] + fc_qtd[3];
                    FC_NREM_MEDIA = qtd_nrem > 0 ? (int)Math.Round((fc_med[1] + fc_med[2] + fc_med[3]) / (double)qtd_nrem) : 0;
                    FC_NREM_MAIOR = Math.Max(fc_max[1], Math.Max(fc_max[2], fc_max[3]));

                    FC_VIGILIA_MEDIA = fc_qtd[0] > 0 ? (int)Math.Round(fc_med[0] / (double)fc_qtd[0]) : 0;
                    FC_VIGILIA_MAIOR = fc_max[0];

                    break; // só processa um canal FC
                }
            }

            GlobVar.g_dados_fc_separada = "";
            for (int i = 0; i < 10; i++)
            {
                int media = fc_qtd[i] > 0 ? (int)Math.Round(fc_med[i] / (double)fc_qtd[i]) : 0;
                GlobVar.g_dados_fc_separada += media.ToString("000");
                GlobVar.g_dados_fc_separada += fc_min[i].ToString("000");
                GlobVar.g_dados_fc_separada += fc_max[i].ToString("000");
            }
        }

        public static void SubstituiVar(string header, string data)
        {
            // Simula g_textolaudo como texto do documento (ou parte dele)
            string g_textolaudo = doc.Content.Text;

            // Simula g_variaveislaudo e arqexporta como variáveis globais/acumuladoras
            StringBuilder g_variaveislaudo = new StringBuilder();
            StringBuilder arqexporta = new StringBuilder();

            // Substitui quebras de linha por " - "
            while (data.Contains("\r\n"))
            {
                int index = data.IndexOf("\r\n");
                data = data.Substring(0, index) + " - " + data.Substring(index + 2);
            }

            // Lógica de ajuste do header
            if (passagem == 2 && segmentos == 0)
            {
                header = "&(SP_" + header.Substring(2);
            }
            else if (passagem > 1 && passagem < ultimapassagem)
            {
                // Exemplo mostra que não faz nada com passagem == 3
                if (passagem == 3)
                {
                    passagem = passagem; // provavelmente um placeholder ou breakpoint
                }
                header = "&(S" + passagem + "_" + header.Substring(2);
            }

            // Verifica se o header existe no texto do laudo
            if (g_textolaudo.Contains(header.Trim()))
            {
                Microsoft.Office.Interop.Word.Find findObject = wordApp.Selection.Find;
                findObject.ClearFormatting();
                findObject.Replacement.ClearFormatting();

                findObject.Text = header;
                findObject.Replacement.Text = data.Length > 255 ? data.Substring(0, 255) : data;

                findObject.Forward = true;
                findObject.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                findObject.Format = false;
                findObject.MatchCase = false;
                findObject.MatchWholeWord = false;
                findObject.MatchWildcards = false;
                findObject.MatchSoundsLike = false;
                findObject.MatchAllWordForms = false;

                // Acumula no arqexporta
                arqexporta.AppendLine($"{header}={data}");

                // Executa a substituição
                findObject.Execute(Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                // Acumula no g_variaveislaudo
                g_variaveislaudo.AppendLine($"{header}{data};");
            }

            // Se quiser usar as strings acumuladas depois:
            string resultadoVariaveis = g_variaveislaudo.ToString();
            string resultadoExporta = arqexporta.ToString();
        }
        private void arrumaTbl()
        {
            foreach (DataRow rw in GlobVar.tbl_Paginas.Rows)
            {
                if (rw["Estagio"] == DBNull.Value)
                {
                    rw["Estagio"] = 0;
                }
            }
            string connectionStringDatBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";
            OleDbConnection cnn_dbExame = new OleDbConnection(connectionStringDatBd);
            cnn_dbExame.Open();
            ExecutaSQLParaAlteracao(cnn_dbExame, "UPDATE tbl_Paginas set estagio = 0 WHERE tbl_Paginas.Estagio is Null");
            cnn_dbExame.Close();
        }
        private void InicializaEvRespDOC()
        {

            // Zera os eventos
            var eventos = new[] { ev_ap, ev_ap_obs, ev_ap_cen, ev_ap_mis, ev_hipop, ev_hipop_obs, ev_dessat, ev_desp, ev_rera, ev_brux_fas, ev_ronco, ev_plm, ev_mov_perna };
            foreach (var ev in eventos)
            {
                ev.indice = 0;
                ev.maior = 0;
                ev.media = 0;
                ev.durtotal = 0;
                ev.qtd = 0;
                ev.qtd_rem = 0;
                ev.qtd_nrem = 0;
                ev.qtd_pos_c = 0;
                ev.qtd_pos_x = 0;
            }
            string connectionStringDatBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";
            OleDbConnection cnn_dbExame = new OleDbConnection(connectionStringDatBd);
            cnn_dbExame.Open();

            // Consulta e atribuição direta por código de evento
            void Preenche(EventoResumo destino, int codEvento)
            {
                var rs = ExecutaSQL(cnn_dbExame, $"SELECT * FROM tbl_RelatResumoEventos WHERE CodEvento = {codEvento}");
                if (rs.Rows.Count > 0)
                {
                    var row = rs.Rows[0];
                    destino.qtd = Convert.ToInt32(row["Qtd"]);
                    destino.indice = Convert.ToDouble(row["QtdHora"]);
                    destino.maior = Convert.ToDouble(row["MaiorDuracao"]);
                    destino.media = Convert.ToDouble(row["DuracaoMedia"]);
                    destino.durtotal = Convert.ToDouble(row["DuracaoTotal"]);
                    destino.qtd_rem = Convert.ToInt32(row["QtdREM"]);
                    destino.qtd_nrem = Convert.ToInt32(row["QtdNREM"]);
                    destino.qtd_pos_c = Convert.ToInt32(row["QtdPosC"]);
                    destino.qtd_pos_x = Convert.ToInt32(row["QtdPosX"]);
                }
            }

            // Apneias
            Preenche(ev_ap_cen, 1);
            Preenche(ev_ap_obs, 2);
            Preenche(ev_ap_mis, 3);

            ev_ap.qtd = ev_ap_cen.qtd + ev_ap_obs.qtd + ev_ap_mis.qtd;
            ev_ap.indice = ev_ap_cen.indice + ev_ap_obs.indice + ev_ap_mis.indice;
            ev_ap.maior = Math.Max(ev_ap_cen.maior, Math.Max(ev_ap_obs.maior, ev_ap_mis.maior));
            ev_ap.media = ev_ap.qtd > 0
                ? ((ev_ap_cen.media * ev_ap_cen.qtd) + (ev_ap_obs.media * ev_ap_obs.qtd) + (ev_ap_mis.media * ev_ap_mis.qtd)) / ev_ap.qtd
                : 0;
            ev_ap.qtd_rem = ev_ap_cen.qtd_rem + ev_ap_obs.qtd_rem + ev_ap_mis.qtd_rem;
            ev_ap.qtd_nrem = ev_ap_cen.qtd_nrem + ev_ap_obs.qtd_nrem + ev_ap_mis.qtd_nrem;
            ev_ap.qtd_pos_c = ev_ap_cen.qtd_pos_c + ev_ap_obs.qtd_pos_c + ev_ap_mis.qtd_pos_c;
            ev_ap.qtd_pos_x = ev_ap_cen.qtd_pos_x + ev_ap_obs.qtd_pos_x + ev_ap_mis.qtd_pos_x;

            // Hipopneias
            Preenche(ev_hipop_obs, 5);
            // obs: ev_hipop_cen e ev_hipop_mis devem existir como no VB6, mesmo que não preenchidos aqui
            ev_hipop.qtd = ev_hipop_cen.qtd + ev_hipop_obs.qtd + ev_hipop_mis.qtd;
            ev_hipop.indice = ev_hipop_cen.indice + ev_hipop_obs.indice + ev_hipop_mis.indice;
            ev_hipop.maior = Math.Max(ev_hipop_cen.maior, Math.Max(ev_hipop_obs.maior, ev_hipop_mis.maior));
            ev_hipop.media = ev_hipop.qtd > 0
                ? ((ev_hipop_cen.media * ev_hipop_cen.qtd) + (ev_hipop_obs.media * ev_hipop_obs.qtd) + (ev_hipop_mis.media * ev_hipop_mis.qtd)) / ev_hipop.qtd
                : 0;
            ev_hipop.qtd_rem = ev_hipop_cen.qtd_rem + ev_hipop_obs.qtd_rem + ev_hipop_mis.qtd_rem;
            ev_hipop.qtd_nrem = ev_hipop_cen.qtd_nrem + ev_hipop_obs.qtd_nrem + ev_hipop_mis.qtd_nrem;
            ev_hipop.qtd_pos_c = ev_hipop_cen.qtd_pos_c + ev_hipop_obs.qtd_pos_c + ev_hipop_mis.qtd_pos_c;
            ev_hipop.qtd_pos_x = ev_hipop_cen.qtd_pos_x + ev_hipop_obs.qtd_pos_x + ev_hipop_mis.qtd_pos_x;

            //Despertar
            Preenche(ev_desp, 8);
            // RERA
            Preenche(ev_rera, 101);

            //Dessat - 17 
            Preenche(ev_dessat, 17);

            //Bruxismo - 9
            Preenche(ev_brux_fas, 9);

            //Ronco - 13
            Preenche(ev_ronco, 13);

            //Perna - plm - 12 - mov perna - 22
            Preenche(ev_plm, 12);
            Preenche(ev_mov_perna, 22);

        }
        public void CalculaResumoMultiplaLatencia()
        {
            int[] est = new int[10];
            int qtd_lat;
            int Lat_Sono_Qtd = 1;
            string Lat_Sono_Est = "1, 2, 3, 4, 5";

            for (int i = 0; i < 5; i++)
            {
                g_naps[i] = new NapResumo
                {
                    Inicio = TimeSpan.Zero,
                    fim = TimeSpan.Zero,
                    Lat_Est1 = -1,
                    Lat_Est2 = -1,
                    Lat_Est3 = -1,
                    Lat_Est4 = -1,
                    Lat_Est5_BoaNoite = -1,
                    Lat_Est5_SleepOnset = -1,
                    Lat_Sono = -1,
                    TempodeREM = 0,
                    TempoEst0 = 0,
                    TempoEst1 = 0,
                    TempoEst2 = 0,
                    TempoEst3 = 0,
                    TTR = 0,
                    TTS = 0,
                    HorarioREM = TimeSpan.Zero,
                    HorarioNREM = TimeSpan.Zero
                };
            }

            var tbl_Paginas = (from row in GlobVar.tbl_Paginas.AsEnumerable()
                               orderby row.Field<int>("NumPag")
                               select row).ToList();

            for (int pos = 0; pos < 5; pos++)
            {
                bool erro_pag = false;
                qtd_lat = 0;
                Array.Clear(est, 0, est.Length);

                int pag_dia = F_GetFimLatencia(pos + 1);
                int pag_noite = F_GetInicioLatencia(pos + 1);

                if (pag_dia == -1 || pag_noite == -1)
                {
                    erro_pag = true;
                }
                else
                {
                    var linha_fim = tbl_Paginas.FirstOrDefault(r => r.Field<int>("NumPag") == pag_dia);
                    if (linha_fim != null)
                        g_naps[pos].fim = linha_fim.Field<DateTime>("Horario").TimeOfDay;

                    var linha_ini = tbl_Paginas.FirstOrDefault(r => r.Field<int>("NumPag") == pag_noite);
                    if (linha_ini != null)
                        g_naps[pos].Inicio = linha_ini.Field<DateTime>("Horario").TimeOfDay;

                    int i = tbl_Paginas.FindIndex(r => r.Field<int>("NumPag") == pag_noite);
                    while (i < tbl_Paginas.Count && tbl_Paginas[i].Field<int>("NumPag") <= pag_dia)
                    {
                        var row = tbl_Paginas[i];
                        int estagio = row.Field<int?>("estagio") ?? -1;

                        if (estagio >= 0 && estagio <= 6)
                        {
                            if (estagio == 5 && est[5] == 0)
                                g_naps[pos].HorarioREM = row.Field<DateTime>("Horario").TimeOfDay;

                            if (estagio > 0 && estagio < 5 && g_naps[pos].HorarioNREM == TimeSpan.Zero)
                                g_naps[pos].HorarioNREM = row.Field<DateTime>("Horario").TimeOfDay;

                            est[estagio]++;
                        }

                        if (Lat_Sono_Est.Contains(estagio.ToString()))
                            qtd_lat++;

                        if (qtd_lat == Lat_Sono_Qtd && g_naps[pos].Lat_Sono == -1)
                            g_naps[pos].Lat_Sono = g_naps[pos].TTR - (qtd_lat - 1);

                        if (estagio == 1 && g_naps[pos].Lat_Est1 == -1)
                            g_naps[pos].Lat_Est1 = g_naps[pos].TTR * GlobVar.segundos;
                        if (estagio == 2 && g_naps[pos].Lat_Est2 == -1)
                            g_naps[pos].Lat_Est2 = g_naps[pos].TTR * GlobVar.segundos;
                        if (estagio == 3 && g_naps[pos].Lat_Est3 == -1)
                            g_naps[pos].Lat_Est3 = g_naps[pos].TTR * GlobVar.segundos;
                        if (estagio == 4 && g_naps[pos].Lat_Est4 == -1)
                            g_naps[pos].Lat_Est4 = g_naps[pos].TTR * GlobVar.segundos;
                        if (estagio == 5 && g_naps[pos].Lat_Est5_BoaNoite == -1)
                            g_naps[pos].Lat_Est5_BoaNoite = g_naps[pos].TTR * GlobVar.segundos;
                        if (estagio == 5 && g_naps[pos].Lat_Est5_SleepOnset == -1 && g_naps[pos].Lat_Sono > -1)
                            g_naps[pos].Lat_Est5_SleepOnset = (g_naps[pos].TTR - g_naps[pos].Lat_Sono) * GlobVar.segundos;

                        g_naps[pos].TTR++;
                        i += GlobVar.segundos;
                    }

                    g_naps[pos].TTS = 0;
                    for (int e = 1; e <= 5; e++)
                        g_naps[pos].TTS += est[e];

                    if (g_naps[pos].Lat_Sono > 0)
                        g_naps[pos].Lat_Sono *= GlobVar.segundos;

                    g_naps[pos].TTS *= GlobVar.segundos;
                    g_naps[pos].TTR *= GlobVar.segundos;
                    g_naps[pos].TempodeREM = est[5] * GlobVar.segundos;
                    g_naps[pos].TempoEst0 = est[0] * GlobVar.segundos;
                    g_naps[pos].TempoEst1 = est[1] * GlobVar.segundos;
                    g_naps[pos].TempoEst2 = est[2] * GlobVar.segundos;
                    g_naps[pos].TempoEst3 = est[3] * GlobVar.segundos;
                }
            }

            int pagDiaFinal = Canais.Get_BomDia();// F_GetBomDia();
            int pagNoiteFinal = Canais.Get_BoaNoite();// F_GetBoaNoite();
        }
        public int F_GetFimLatencia(int nap)
        {
            int cod_fim_periodo = 111 + (nap - 1) * 2;

            var eventos = GlobVar.eventos.AsEnumerable()
                .Where(r => r.Field<int>("CodEvento") == cod_fim_periodo)
                .OrderByDescending(r => r.Field<int>("NumPag"))
                .ToList();

            int pag = -1;
            if (eventos.Any())
                pag = eventos.First().Field<int>("NumPag");

            if (pag > GlobVar.tbl_Paginas.Rows.Count || pag == -1)
                pag = GlobVar.tbl_Paginas.Rows.Count - 1;
            return pag;
        }
        public int F_GetInicioLatencia(int nap)
        {
            int cod_ini_periodo = 110 + (nap - 1) * 2;

            var eventos = GlobVar.eventos.AsEnumerable()
                .Where(r => r.Field<int>("CodEvento") == cod_ini_periodo)
                .OrderBy(r => r.Field<int>("NumPag"))
                .ToList();

            int pag = -1;
            if (eventos.Any())
                pag = eventos.First().Field<int>("NumPag");
            if (pag == -1)
            {
                pag = 0;
            }
            return pag;
        }
        void S_CPAP_Dados(int passagem, int pag_noite, int pag_dia)
        {
            if (((passagem == 3 && passagem == 2) || passagem == 1) == false)
                return;

            for (int i = 0; i <= 30; i++)  // ajuste conforme o limite esperado
                cpapRelatDict[i] = new CPAPRelat();

            //int pos_can_SAO2 = frm_Principal.obj_DataSource.F_GetCanaldoTipo(20);
            if (GlobVar.tbl_CanaisAdquiridos.AsEnumerable().Any(row => row.Field<int>("CodTipoCanal") == 15))
                return;

            var tbl_Paginas = GlobVar.tbl_Paginas.AsEnumerable()
                .OrderBy(r => r.Field<double?>("Pressao_CPAP"))
                .ToList();

            if (!tbl_Paginas.Any() || tbl_Paginas.First().IsNull("Pressao_CPAP"))
            {
                MessageBox.Show(f_var("Var58074") + "\r\n" + f_var("Var58080"), f_var("Var26047"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CPAP_Min = tbl_Paginas.First().Field<double>("Pressao_CPAP");
            CPAP_Max = tbl_Paginas.Last().Field<double>("Pressao_CPAP");

            int pressaomenor = 100, pressaomaior = 0;

            foreach (var row in GlobVar.tbl_Paginas.AsEnumerable()
                     .Where(r => r.Field<int>("NumPag") >= pag_noite && r.Field<int>("NumPag") < pag_dia)
                     .OrderBy(r => r.Field<int>("NumPag")))
            {
                double pressao = row.Field<double>("Pressao_CPAP");
                int pag = row.Field<int>("NumPag");
                int valor = Canais.F_Get1ValorDoCanalSAO2(pag);

                int rounded = (int)Math.Round(pressao);
                if (valor > 40 && cpapRelatDict.ContainsKey(rounded))
                {
                    var rel = cpapRelatDict[rounded];
                    if (valor < rel.Sat_Min)
                        rel.Sat_Min = valor;

                    rel.Sat_Med += valor;
                    rel.qtd_pags++;

                    if (pressao < pressaomenor && pressao != 0) pressaomenor = (int)pressao;
                    if (pressao > pressaomaior) pressaomaior = (int)pressao;
                }
            }

            for (int i = pressaomenor; i <= pressaomaior; i++)
            {
                if (cpapRelatDict[i].qtd_pags > 0)
                    cpapRelatDict[i].Sat_Med = (int)Math.Round((double)cpapRelatDict[i].Sat_Med / cpapRelatDict[i].qtd_pags);
            }

            // Tempo por estágio
            var agrupadosTempo = GlobVar.tbl_Paginas.AsEnumerable()
                .Where(r => r.Field<int>("NumPag") > pag_noite && r.Field<int>("NumPag") < pag_dia)
                .GroupBy(r => new { Pressao = (int)Math.Round(r.Field<double>("Pressao_CPAP")), Estagio = r.Field<int>("Estagio") })
                .Select(g => new
                {
                    g.Key.Pressao,
                    g.Key.Estagio,
                    Tempo = g.Count()
                });

            foreach (var item in agrupadosTempo)
            {
                if (item.Pressao >= 0 && cpapRelatDict.ContainsKey(item.Pressao))
                {
                    var rel = cpapRelatDict[item.Pressao];
                    switch (item.Estagio)
                    {
                        case 0: rel.tempo_Vigilia += item.Tempo; break;
                        case 1 or 2 or 3 or 4: rel.tempo_NREM += item.Tempo; break;
                        case 5: rel.tempo_REM += item.Tempo; break;
                    }
                }
            }

            // Eventos
            var eventos = GlobVar.Cons_Eventos.AsEnumerable()
                .Join(GlobVar.tbl_Paginas.AsEnumerable(),
                      ev => ev.Field<int>("Pag_Ini"),
                      pg => pg.Field<int>("NumPag"),
                      (ev, pg) => new
                      {
                          CodEvento = ev.Field<int>("CodEvento"),
                          Pressao = (int)Math.Round(pg.Field<double>("Pressao_CPAP"))
                      })
                .GroupBy(x => new { x.Pressao, x.CodEvento })
                .Select(g => new
                {
                    g.Key.Pressao,
                    g.Key.CodEvento,
                    Qtde = g.Count()
                });

            foreach (var item in eventos)
            {
                if (item.Pressao >= 0 && cpapRelatDict.ContainsKey(item.Pressao))
                {
                    var rel = cpapRelatDict[item.Pressao];
                    switch (item.CodEvento)
                    {
                        case 1: rel.Qtd_AC += item.Qtde; break;
                        case 2: rel.Qtd_AO += item.Qtde; break;
                        case 3: rel.qtd_am += item.Qtde; break;
                        case 4 or 5 or 6: rel.Qtd_Hip += item.Qtde; break;
                        case 17: rel.Qtd_Dessat += item.Qtde; break;
                    }
                }
            }

            CalculaSaturacaoCPAP();
        }
        public void CalculaSaturacaoCPAP()
        {
            int i, j;
            int valor;
            string SAT_MEDIA = "";
            string Sat_Media_Calc;
            int despreza;
            int Sat_Segundos = 60;
            int Sat_Desvio = 10;
            int Sat_Valor;
            double acum = 0;
            int menor_sat, MAIOR_SAT = 0;
            int media_sat;
            int SaO2_100 = 511;
            int Sat_Basal_inicial = 100;
            float Sat_QuedaAbaixoDe = 4;
            int Sat_DuracaoMinima = 10;
            int Sat_Recalcular = 900;
            int Sat_DesprezarAbaixo = 40;
            int qtd;
            int menor_CPAP, maior_CPAP, pag_ini;

            // Recuperar parâmetros do exame
            var dadosExame = GlobVar.tbl_DadosExame.Rows[0];
            if (dadosExame["SaO2_100"] != DBNull.Value) SaO2_100 = Convert.ToInt32(dadosExame["SaO2_100"]);
            Sat_Basal_inicial = Convert.ToInt32(dadosExame["SatBasal"]);

            // Recuperar páginas a serem desprezadas
            var paginas_desprezadas = new HashSet<int>(
                GlobVar.eventos.AsEnumerable()
                    .Where(r => r.Field<int>("CodEvento") == 100)
                    .Select(r => r.Field<int>("NumPag"))
            );

            menor_sat = SaO2_100;
            MAIOR_SAT = 0;
            acum = 0;
            qtd = 0;

            // Parâmetros configuráveis
            var parametros = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];
            Sat_QuedaAbaixoDe = Convert.ToSingle(parametros["Sat_QuedaAbaixoDe"]);
            Sat_DuracaoMinima = Convert.ToInt32(parametros["Sat_DuracaoMinima"]);
            Sat_Recalcular = Convert.ToInt32(parametros["Sat_Recalcular"]);
            Sat_DesprezarAbaixo = Convert.ToInt32(parametros["Sat_DesprezarAbaixo"]);
            Sat_Segundos = Convert.ToInt32(parametros["Sat_Tempo_Medio"]);
            Sat_Desvio = Convert.ToInt32(parametros["Sat_Tolerancia_Desvio"]);

            despreza = SaO2_100 * Sat_DesprezarAbaixo / 100;

            // Faixa inicial para média basal
            int pag_dia = Canais.Get_BomDia();
            int pag_noite = Canais.Get_BoaNoite();
            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(r => Convert.ToDouble(r["Pressao_CPAP"]) > 0);
            pag_ini = Math.Max(0, row != null ? Convert.ToInt32(row["NumPag"]) - 30 : 0);

            for (i = pag_ini; i < pag_dia; i++)
            {
                if (!paginas_desprezadas.Contains(i))
                {
                    valor = Canais.F_Get1ValorDoCanalSAO2(i);
                    if (valor > despreza && valor <= 100)
                    {
                        if (SAT_MEDIA.Length < Sat_Segundos * 4)
                        {
                            SAT_MEDIA += valor.ToString("D3") + "#";
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            // Calcula média basal
            Sat_Media_Calc = SAT_MEDIA;
            Sat_Valor = 0;
            while (Sat_Media_Calc.Length > 1)
            {
                Sat_Valor += Convert.ToInt32(Sat_Media_Calc.Substring(0, 3));
                Sat_Media_Calc = Sat_Media_Calc.Length >= 4 ? Sat_Media_Calc.Substring(4) : "";
            }

            // Determinar menor e maior pressão CPAP
            menor_CPAP = GlobVar.tbl_Paginas.AsEnumerable().Min(r => Convert.ToInt32(r["Pressao_CPAP"]));
            maior_CPAP = GlobVar.tbl_Paginas.AsEnumerable().Max(r => Convert.ToInt32(r["Pressao_CPAP"]));

            for (j = menor_CPAP; j <= maior_CPAP; j++)
            {
                qtd = 0;
                acum = 0;
                menor_sat = 100;

                for (i = pag_noite; i < pag_dia; i++)
                {
                    var linha = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(r => Convert.ToInt32(r["NumPag"]) == i);
                    if (linha == null || Convert.ToInt32(linha["Pressao_CPAP"]) != j) continue;
                    if (paginas_desprezadas.Contains(i)) continue;

                    valor = Canais.F_Get1ValorDoCanalSAO2(i);
                    if (valor > despreza && valor <= 100)
                    {
                        SAT_MEDIA = SAT_MEDIA.Length >= 4 ? SAT_MEDIA.Substring(4) : "" + valor.ToString("D3") + "#";

                        Sat_Media_Calc = SAT_MEDIA;
                        Sat_Valor = 0;
                        while (Sat_Media_Calc.Length > 1)
                        {
                            Sat_Valor += Convert.ToInt32(Sat_Media_Calc.Substring(0, 3));
                            Sat_Media_Calc = Sat_Media_Calc.Length >= 4 ? Sat_Media_Calc.Substring(4) : "";
                        }

                        var mediaAtual = Sat_Valor / Sat_Segundos;
                        var tolerancia = mediaAtual * Sat_Desvio / 100.0;

                        if (valor >= mediaAtual - tolerancia && valor <= mediaAtual + tolerancia)
                        {
                            acum += valor;
                            qtd++;
                            if (valor > MAIOR_SAT) MAIOR_SAT = valor;
                            if (valor < menor_sat) menor_sat = valor;
                        }
                    }
                }

                if (menor_sat < 100)
                    cpapRelatDict[j].Sat_Min = menor_sat;

                if (qtd > 0)
                    cpapRelatDict[j].Sat_Med = (int)(acum / qtd);
            }
        }
        public void S_BPAP_Dados(int passagem, int pag_noite, int pag_dia)
        {
            if ((passagem == 3 && passagem == 2) || passagem == 1)
            {
                // Inicializa g_BPAP_Relat
                for (int i = 0; i <= 30; i++) // Limite arbitrário
                {
                    if (!g_BPAP_Relat.ContainsKey(i))
                        g_BPAP_Relat[i] = new Dictionary<int, BPAPRelat>();

                    for (int j = 1; j <= 30; j++) // EPAP de 1 a 30
                    {
                        g_BPAP_Relat[i][j] = new BPAPRelat();
                    }
                }

                // Se não adquiriu o canal tipo 15, sai
                if (GlobVar.tbl_CanaisAdquiridos.AsEnumerable().Any(row => row.Field<int>("CodTipoCanal") == 15)) return;

                //int pos_can_SAO2 = Canais.F_GetCanaldoTipo(20);

                var tbl_Paginas = GlobVar.tbl_Paginas.AsEnumerable().OrderBy(r => Convert.ToDouble(r["Pressao_CPAP"])).ToList();

                if (tbl_Paginas.Count == 0 || tbl_Paginas[0]["Pressao_CPAP"] == DBNull.Value)
                {
                    MessageBox.Show(f_var("Var58074") + "\n" + f_var("Var58080"), f_var("Var26047"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CPAP_Min = Convert.ToDouble(tbl_Paginas.First()["Pressao_CPAP"]);
                CPAP_Max = Convert.ToDouble(tbl_Paginas.Last()["Pressao_CPAP"]);

                var paginasPorNum = GlobVar.tbl_Paginas.AsEnumerable().OrderBy(r => Convert.ToInt32(r["NumPag"])).ToList();
                int pressaomenor = 100;
                int pressaomaior = 0;

                foreach (var row in paginasPorNum.Where(r => Convert.ToInt32(r["NumPag"]) >= pag_noite && Convert.ToInt32(r["NumPag"]) < pag_dia))
                {
                    int numPag = Convert.ToInt32(row["NumPag"]);
                    int valor = Canais.F_Get1ValorDoCanalSAO2(numPag);
                    int pressaoCPAP = (int)Math.Round(Convert.ToDouble(row["Pressao_CPAP"]));
                    int pressaoEPAP = Convert.ToInt32(row["Pressao_EPAP"]);

                    if (pressaoCPAP >= 0 && g_BPAP_Relat.ContainsKey(pressaoCPAP) && g_BPAP_Relat[pressaoCPAP].ContainsKey(pressaoEPAP))
                    {
                        if (valor > 40 && valor < g_BPAP_Relat[pressaoCPAP][pressaoEPAP].Sat_Min)
                        {
                            g_BPAP_Relat[pressaoCPAP][pressaoEPAP].Sat_Min = valor;
                            g_BPAP_Relat[pressaoCPAP][pressaoEPAP].Sat_Med += valor;
                            g_BPAP_Relat[pressaoCPAP][pressaoEPAP].qtd_pags++;

                            double p = Convert.ToDouble(row["Pressao_CPAP"]);
                            if (p < pressaomenor && p != 0) pressaomenor = (int)p;
                            if (p > pressaomaior) pressaomaior = (int)p;
                        }
                    }
                }

                // Calcula média de saturação
                for (int i = pressaomenor; i <= pressaomaior; i++)
                {
                    for (int j = 1; j <= 30; j++)
                    {
                        if (g_BPAP_Relat.ContainsKey(i) && g_BPAP_Relat[i].ContainsKey(j))
                        {
                            var relat = g_BPAP_Relat[i][j];
                            if (relat.qtd_pags > 0)
                                relat.Sat_Med = (int)Math.Round((double)relat.Sat_Med / relat.qtd_pags);
                        }
                    }
                }

                // Consulta CPAP por estágio
                var consultaEstagio = from r in GlobVar.tbl_Paginas.AsEnumerable()
                                      where Convert.ToInt32(r["NumPag"]) > pag_noite && Convert.ToInt32(r["NumPag"]) < pag_dia
                                      group r by new
                                      {
                                          CPAP = (int)Math.Round(Convert.ToDouble(r["Pressao_CPAP"])),
                                          EPAP = Convert.ToInt32(r["Pressao_EPAP"]),
                                          Estagio = Convert.ToInt32(r["Estagio"])
                                      } into g
                                      select new
                                      {
                                          g.Key.CPAP,
                                          g.Key.EPAP,
                                          g.Key.Estagio,
                                          Tempo = g.Count()
                                      };

                foreach (var row in consultaEstagio)
                {
                    if (g_BPAP_Relat.ContainsKey(row.CPAP) && g_BPAP_Relat[row.CPAP].ContainsKey(row.EPAP))
                    {
                        switch (row.Estagio)
                        {
                            case 0: g_BPAP_Relat[row.CPAP][row.EPAP].tempo_Vigilia += row.Tempo; break;
                            case 1 or 2 or 3 or 4: g_BPAP_Relat[row.CPAP][row.EPAP].tempo_NREM += row.Tempo; break;
                            case 5: g_BPAP_Relat[row.CPAP][row.EPAP].tempo_REM += row.Tempo; break;
                        }
                    }
                }

                // Consulta eventos
                var eventos = from e in GlobVar.eventos.AsEnumerable()
                              join p in GlobVar.tbl_Paginas.AsEnumerable()
                              on e["Pag_Ini"] equals p["NumPag"]
                              group e by new
                              {
                                  CPAP = (int)Math.Round(Convert.ToDouble(p["Pressao_CPAP"])),
                                  EPAP = Convert.ToInt32(p["Pressao_EPAP"]),
                                  CodEvento = Convert.ToInt32(e["CodEvento"])
                              } into g
                              select new
                              {
                                  g.Key.CPAP,
                                  g.Key.EPAP,
                                  g.Key.CodEvento,
                                  Qtde = g.Count()
                              };

                foreach (var row in eventos)
                {
                    if (g_BPAP_Relat.ContainsKey(row.CPAP) && g_BPAP_Relat[row.CPAP].ContainsKey(row.EPAP))
                    {
                        switch (row.CodEvento)
                        {
                            case 1: g_BPAP_Relat[row.CPAP][row.EPAP].Qtd_AC += row.Qtde; break;
                            case 2: g_BPAP_Relat[row.CPAP][row.EPAP].Qtd_AO += row.Qtde; break;
                            case 3: g_BPAP_Relat[row.CPAP][row.EPAP].qtd_am += row.Qtde; break;
                            case 4 or 5 or 6: g_BPAP_Relat[row.CPAP][row.EPAP].Qtd_Hip += row.Qtde; break;
                            case 17: g_BPAP_Relat[row.CPAP][row.EPAP].Qtd_Dessat += row.Qtde; break;
                        }
                    }
                }
            }
        }

        private static bool TextoComboContem(string termo)
        {
            return comboBox1.Text?.ToUpperInvariant().Trim()
                   .Contains(termo?.ToUpperInvariant().Trim()) ?? false;
        }
        public static bool ExisteArquivo(string path)
        {
            return File.Exists(path);
        }
        public static string f_var(string variavel)
        {
            string tag = $"#{variavel}#";
            int start = GlobVar.g_Traducoes.IndexOf(tag);
            if (start == -1) return ""; // não encontrou a variável

            // Move o ponteiro 10 caracteres à frente do início
            int innerStart = start + 10;
            if (innerStart >= GlobVar.g_Traducoes.Length) return "";

            string restante = GlobVar.g_Traducoes.Substring(innerStart);
            int end = restante.IndexOf('#');
            if (end == -1) return "";

            return restante.Substring(0, end);
        }
        public static bool ArquivoAberto(string path)
        {
            try
            {
                using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // Se conseguir abrir com exclusividade, não está aberto
                    return false;
                }
            }
            catch (IOException)
            {
                // Se der IOException, o arquivo provavelmente está sendo usado
                return true;
            }
        }
        private void Fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        Panel pnl_Msg2 = new Panel();
        Label lblMsg = new Label();
        RadioButton rdoProcessarMesmoAssim = new RadioButton();
        RadioButton rdoApagarEventos = new RadioButton();
        RadioButton rdoCancelar = new RadioButton();
        Button btnOkMsg2 = new Button();
        LinkLabel lnkVerEventos = new LinkLabel();
        Label lbl_VerEventos = new Label(); // Para armazenar os dados de texto
        object cmd_msg2 = new { Tag = 0 }; // Simulando cmd_msg2
        void CriarPainelMensagem()
        {
            pnl_Msg2.BorderStyle = BorderStyle.FixedSingle;
            pnl_Msg2.Size = new Size(380, 170); // Aumentado para acomodar melhor o conteúdo
            pnl_Msg2.Visible = false;
            pnl_Msg2.BackColor = System.Drawing.Color.Transparent;

            lblMsg.Text = "Existem eventos em estágio Zero que não serão mostrados no laudo.";
            lblMsg.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            lblMsg.Size = new Size(360, 40); // Largura ajustada
            lblMsg.Location = new Point(10, 10);

            rdoProcessarMesmoAssim.Text = "Processar";
            rdoProcessarMesmoAssim.Checked = true;
            rdoProcessarMesmoAssim.Location = new Point(20, 60);

            lnkVerEventos.Text = "(Ver eventos)";
            lnkVerEventos.AutoSize = true;
            lnkVerEventos.LinkColor = System.Drawing.Color.Blue;
            lnkVerEventos.Location = new Point(190, 60);
            lnkVerEventos.LinkClicked += (s, e) =>
            {
                string eventosTexto = lbl_VerEventos.Tag?.ToString();
                if (!string.IsNullOrEmpty(eventosTexto))
                {
                    var form = new Form
                    {
                        Text = "Eventos encontrados",
                        Size = new Size(250, 200),
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        StartPosition = FormStartPosition.CenterParent,
                        MinimizeBox = false,
                        MaximizeBox = false,
                        ShowInTaskbar = false
                    };

                    var lblTexto = new Label
                    {
                        Text = eventosTexto,
                        AutoSize = false,
                        TextAlign = ContentAlignment.TopLeft,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(10),
                        Font = new System.Drawing.Font("Segoe UI", 9),
                    };

                    var btnOk = new Button
                    {
                        Text = "OK",
                        DialogResult = DialogResult.OK,
                        Anchor = AnchorStyles.Bottom,
                        Width = 80,
                        Height = 30,
                        Left = (form.ClientSize.Width - 80) / 2,
                        Top = form.ClientSize.Height - 50
                    };

                    form.Controls.Add(lblTexto);
                    form.Controls.Add(btnOk);
                    form.AcceptButton = btnOk;

                    form.ShowDialog();
                }
            };

            rdoApagarEventos.Text = "Apagar";
            rdoApagarEventos.Location = new Point(20, 85);

            rdoCancelar.Text = "Cancelar";
            rdoCancelar.Location = new Point(20, 110);

            btnOkMsg2.Text = "OK";
            btnOkMsg2.Size = new Size(80, 30);
            btnOkMsg2.Location = new Point(pnl_Msg2.Width - 90, pnl_Msg2.Height - 40); // canto inferior direito
            btnOkMsg2.Click += (s, e) =>
            {
                if (rdoProcessarMesmoAssim.Checked)
                    cmd_msg2 = new { Tag = 0 };
                else if (rdoApagarEventos.Checked)
                    cmd_msg2 = new { Tag = 1 };
                else
                    cmd_msg2 = new { Tag = 2 };

                pnl_Msg2.Visible = false;
            };

            pnl_Msg2.Controls.Clear();
            pnl_Msg2.Controls.Add(lblMsg);
            pnl_Msg2.Controls.Add(rdoProcessarMesmoAssim);
            pnl_Msg2.Controls.Add(lnkVerEventos);
            pnl_Msg2.Controls.Add(rdoApagarEventos);
            pnl_Msg2.Controls.Add(rdoCancelar);
            pnl_Msg2.Controls.Add(btnOkMsg2);

            Laudo.Controls.Add(pnl_Msg2);
        }
        private static void VerificaDessaturacao()
        {
            string dim = "";
            DataTable tbl = new DataTable();
            string g_sao2 = "CINTA_ABDOM";
            string g_sao2_ser = "FC_SERIAL";
            string connectionStringDatBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";
            OleDbConnection cnn_dbExame = new OleDbConnection(connectionStringDatBd);
            cnn_dbExame.Open();

            int pagIni = Canais.Get_BoaNoite();

            int pagFim = Canais.Get_BomDia();

            foreach (DataRow rw in GlobVar.tbl_CanaisAdquiridos.Rows)
            {
                string SiglaTipoCanal = rw["SiglaTipoCanal"].ToString();
                if (SiglaTipoCanal.Equals(g_sao2) || SiglaTipoCanal.Equals(g_sao2_ser))
                {
                    if (SiglaTipoCanal.Equals(g_sao2))
                    {
                        ExecutaSQL(cnn_dbExame, "UPDATE tbl_DadosExame SET SaO2_100 = 511");
                    }
                    else if (SiglaTipoCanal.Equals(g_sao2_ser))
                    {
                        ExecutaSQL(cnn_dbExame, "UPDATE tbl_DadosExame SET SaO2_100 = 100");
                    }
                    else
                    {
                        ResumoDessaturacao(cnn_dbExame, pagIni, pagFim, 17, Convert.ToInt32(rw["Ordem"]), Convert.ToInt32(rw["CodCanal1"]), Convert.ToInt32(rw["CodCanal2"]));
                    }
                }
            }
            cnn_dbExame.Close();
        }
        private static void ResumoDessaturacao(OleDbConnection cnn_dbExame, int pagIni, int pagFim, int codEvento, int canGrav, int codCanal1, int codCanal2)
        {
            var tbl_Pagina = GlobVar.tbl_Paginas;
            var rsDadosExame = GlobVar.tbl_DadosExame;

            int SaO2_100 = 511;
            int Sat_Basal_inicial = 100;
            if (rsDadosExame.Rows.Count > 0)
            {
                var row = rsDadosExame.Rows[0];
                if (!Convert.IsDBNull(row["SaO2_100"]))
                    SaO2_100 = Convert.ToInt32(row["SaO2_100"]);
                Sat_Basal_inicial = Convert.ToInt32(row["SatBasal"]);
            }

            int menor_sat = SaO2_100, maior_sat = 0, valor, media_sat = 0;
            decimal acum = 0, qtd = 0;
            int abaixo90 = 0, abaixo80 = 0, abaixo70 = 0;
            int ref90 = (int)(SaO2_100 * 0.9);
            int ref80 = (int)(SaO2_100 * 0.8);
            int ref70 = (int)(SaO2_100 * 0.7);
            int SatSegundos = 60;
            int SatDesvio = 10;
            float SatQuedaAbaixoDe = 4;
            int SatDuracaoMinima = 10;
            int SatRecalcular = 900;
            int SatDesprezarAbaixo = 40;

            var rsParametros = GlobVar.tbl_ParametrosParaAnalisar;
            if (rsParametros.Rows.Count > 0)
            {
                var row = rsParametros.Rows[0];
                SatQuedaAbaixoDe = Convert.ToSingle(row["Sat_QuedaAbaixoDe"]);
                SatDuracaoMinima = Convert.ToInt32(row["Sat_DuracaoMinima"]);
                SatRecalcular = Convert.ToInt32(row["Sat_Recalcular"]);
                SatDesprezarAbaixo = Convert.ToInt32(row["Sat_DesprezarAbaixo"]);
                SatSegundos = Convert.ToInt32(row["Sat_Tempo_Medio"]);
                SatDesvio = Convert.ToInt32(row["Sat_Tolerancia_Desvio"]);
            }

            int despreza = (int)(SaO2_100 * SatDesprezarAbaixo / 100.0);
            int ref_queda = (int)(Sat_Basal_inicial * (100 - SatQuedaAbaixoDe) / 100.0);
            int menor_sat_dessat = SaO2_100;

            var PagDesprezadas = new HashSet<int>();
            foreach (DataRow row in GlobVar.eventos.Select("CodEvento = 100 AND CodCanal1 = 66"))
            {
                PagDesprezadas.Add(Convert.ToInt32(row["NumPag"]));
            }

            string satMedia = "";

            int indexPagina = 0;
            for (int pag = pagIni; pag <= pagFim && indexPagina < tbl_Pagina.Rows.Count; pag++, indexPagina++)
            {
                valor = Canais.F_Get1ValorDoCanalSAO2(pag);
                if (pag == pagIni) Sat_Basal_inicial = valor;

                if (valor > despreza && valor <= 100 && !PagDesprezadas.Contains(pag))
                {
                    var estagio = Convert.ToInt32(tbl_Pagina.Rows[indexPagina]["estagio"]);
                    if (estagio >= 0 && estagio <= 9)
                    {
                        if (satMedia.Length < SatSegundos * 4)
                        {
                            satMedia += valor.ToString("000") + "#";
                        }
                        else
                        {
                            satMedia = satMedia.Substring(4) + valor.ToString("000") + "#";
                            string calc = satMedia;
                            int satValor = 0, count = 0;
                            while (calc.Length >= 4)
                            {
                                satValor += int.Parse(calc.Substring(0, 3));
                                calc = calc.Substring(4);
                                count++;
                            }

                            float media = satValor / (float)count;
                            float desvio = media * SatDesvio / 100;

                            if (valor >= media - desvio && valor <= media + desvio)
                            {
                                satMedia = satMedia.Substring(4) + valor.ToString("000") + "#";
                                acum += valor;
                                qtd += 1;
                                maior_sat = Math.Max(maior_sat, valor);
                                menor_sat = Math.Min(menor_sat, valor);

                                if (valor < ref90)
                                {
                                    abaixo90 += GlobVar.txPorCanal[canGrav];
                                    if (valor < ref80)
                                    {
                                        abaixo80 += GlobVar.txPorCanal[canGrav];
                                        if (valor < ref70)
                                        {
                                            abaixo70 += GlobVar.txPorCanal[canGrav];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    media_sat++;
                }
            }

            if (qtd > 0)
                media_sat = (int)(acum / qtd);
            else
                media_sat = 0;

            //GlobVar.dbExame.S_GravaResumoDessat(menor_sat, maior_sat, media_sat,
            //abaixo90 / (double)GlobVar.txPorCanal[canGrav],
            //abaixo80 / (double)GlobVar.txPorCanal[canGrav],
            //abaixo70 / (double)GlobVar.txPorCanal[canGrav]);

            // Atualiza flag no resumo
            foreach (DataRow row in GlobVar.tbl_ResumoExame.Rows)
            {
                row["Recalcula_Dessat"] = false;
            }
            ExecutaSQLParaAlteracao(cnn_dbExame, "UPDATE tbl_ResumoExame SET Recalcula_Dessat = ");
        }
        //Prepara Relatiorio MDB ----- Concluido
        private static void PreparaRelatorioMDB()
        {
            int[] fc_med = new int[10];
            int[] fc_min = new int[10];
            int[] fc_max = new int[10];
            int[] fc_qtd = new int[10];
            string g_adulto = GetAdinfo();
            int freq_Segundos = GlobVar.tbl_ParametrosParaAnalisar.Rows[0]["FreqCardiaca_Tempo_Medio"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ParametrosParaAnalisar.Rows[0]["FreqCardiaca_Tempo_Medio"]);
            int freq_Desvio = GlobVar.tbl_ParametrosParaAnalisar.Rows[0]["FreqCardiaca_Tolerancia_Desvio"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ParametrosParaAnalisar.Rows[0]["FreqCardiaca_Tolerancia_Desvio"]);

            for (int i = 0; i < 10; i++)
            {
                fc_med[i] = 0;
                fc_min[i] = 100;
                fc_max[i] = 0;
                fc_qtd[i] = 0;
            }

            int FC_MAIOR = 0;
            int FC_MENOR = 100;
            string freq_Media = "";
            int freq_Valor = 0;

            // --- Abrir conexão com Relatorios.mdb ---
            string relatorioPath = Path.Combine("Relatorios.mdb");
            string connStringRelatorio = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={relatorioPath};Persist Security Info=False;";

            OleDbConnection cnn_dbRelatorio = new OleDbConnection(connStringRelatorio);

            string connectionStringDatBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";
            OleDbConnection cnn_dbExame = new OleDbConnection(connectionStringDatBd);
            string connectionStringConfigBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.configBD};Persist Security Info=False;";
            OleDbConnection cnn_Config = new OleDbConnection(connectionStringConfigBd);

            cnn_Config.Open();
            cnn_dbExame.Open();
            cnn_dbRelatorio.Open(); // ou pode colocar em um método F_OpenConnection

            // --- Obter Bom Dia e Boa Noite ---
            int pag_dia = Canais.Get_BomDia();
            int pag_noite = Canais.Get_BoaNoite();

            List<int> PagDesprezadas = new List<int>();
            string paginas_desprezadas = "#";
            var eventosFiltrados = GlobVar.eventos.AsEnumerable()
                .Where(rw => rw.Field<int>("CodEvento") == 100)
                .OrderBy(rw => rw.Field<int>("NumPag"))
                .ToList();

            if (eventosFiltrados.Any())
            {
                var tbl_Eventos = eventosFiltrados.CopyToDataTable();
                foreach (DataRow row in tbl_Eventos.Rows)
                {
                    PagDesprezadas.Add(Convert.ToInt32(row["NumPag"]));
                }
            }

            DataTable EventosCardiacos = GlobVar.tbl_EventoTipoCanal.AsEnumerable().Where(row => row.Field<int>("CodTipoCanal") == 2).CopyToDataTable();

            // Corrige o cálculo da diferença entre o total de páginas e o número de registros válidos
            var pag_Registros = GlobVar.tbl_Paginas.AsEnumerable()
                .Where(row => row.Field<int>("NumPag") >= pag_noite && row.Field<int>("NumPag") <= pag_dia)
                .OrderBy(row => row.Field<int>("NumPag"))
                .ToList();

            int qtd_Registros = (pag_dia - pag_noite + 1) - pag_Registros.Count;

            int FC_MEDIA = 0;
            int FC_REM_MEDIA = 0;
            int FC_REM_MAIOR = 0;
            int FC_NREM_MEDIA = 0;
            int FC_NREM_MAIOR = 0;
            int FC_VIGILIA_MEDIA = 0;
            int FC_VIGILIA_MAIOR = 0;

            int qtd_media = 0;
            string freqMediaStr = "";

            for (int i = 0; i < GlobVar.qtdCanais.Length; i++)
            {
                if (Convert.ToInt32(GlobVar.tbl_CanaisAdquiridos.Rows[i]["CodTipoCanal"]) == 21)
                {
                    int indexPag = 0;
                    int cod = Convert.ToInt32(GlobVar.tbl_CanaisAdquiridos.Rows[i]["CodCanal1"]);
                    for (int j = pag_noite; j <= pag_dia - qtd_Registros; j++)
                    {
                        if (!PagDesprezadas.Contains(j) && indexPag < pag_Registros.Count)
                        {
                            var pagina = pag_Registros[indexPag];
                            indexPag++;

                            int estagio = pagina["Estagio"] != DBNull.Value ? Convert.ToInt32(pagina["Estagio"]) : 0;
                            int valor = Canais.F_Get1ValorDoCanalFC(cod, j);

                            if (valor > 0 && valor < 200)
                            {
                                if (freqMediaStr.Length < freq_Segundos * 4)
                                {
                                    freqMediaStr += valor.ToString("000") + "#";
                                }
                                else
                                {
                                    string freqMediaCalc = freqMediaStr;
                                    int freq_valor = 0;

                                    while (freqMediaCalc.Length > 3)
                                    {
                                        freq_valor += Convert.ToInt32(freqMediaCalc.Substring(0, 3));
                                        freqMediaCalc = freqMediaCalc.Substring(4);
                                    }

                                    double media = freq_valor / (double)freq_Segundos;
                                    double margem = media * (freq_Desvio / 100.0);

                                    if (valor >= (media - margem) && valor <= (media + margem))
                                    {
                                        if (estagio >= 0 && estagio <= 5)
                                        {
                                            // Atualiza freqMediaStr mantendo últimos (Freq_Segundos - 1) valores
                                            freqMediaStr = freqMediaStr.Substring(4) + valor.ToString("000") + "#";

                                            valor = (int)Math.Round(media, 0);

                                            FC_MEDIA += valor;
                                            qtd_media++;

                                            if (valor > FC_MAIOR)
                                                FC_MAIOR = valor;

                                            if (valor > 20 && valor < FC_MENOR)
                                                FC_MENOR = valor;
                                        }
                                        // Atualiza por estágio
                                        fc_med[estagio] += valor;
                                        fc_qtd[estagio]++;
                                        if (valor > fc_max[estagio])
                                            fc_max[estagio] = valor;
                                        if (valor > 20 && valor < fc_min[estagio])
                                            fc_min[estagio] = valor;
                                    }
                                }
                            }
                        }
                    }

                    // Cálculo final após varredura das páginas
                    FC_MEDIA = qtd_media > 0 ? FC_MEDIA / qtd_media : 0;
                    FC_REM_MEDIA = fc_qtd[5] > 0 ? (int)Math.Round((double)fc_med[5] / fc_qtd[5], 0) : 0;
                    FC_REM_MAIOR = fc_max[5];

                    int qtdNREM = fc_qtd[1] + fc_qtd[2] + fc_qtd[3];
                    int somaNREM = fc_med[1] + fc_med[2] + fc_med[3];
                    FC_NREM_MEDIA = qtdNREM > 0 ? (int)Math.Round((double)somaNREM / qtdNREM) : 0;
                    FC_NREM_MAIOR = Math.Max(fc_max[1], Math.Max(fc_max[2], fc_max[3]));

                    FC_VIGILIA_MEDIA = fc_qtd[0] > 0 ? (int)Math.Round((double)fc_med[0] / fc_qtd[0]) : 0;
                    FC_VIGILIA_MAIOR = fc_max[0];

                    break; // Canal de FC já processado
                }
            }

            // Montagem do campo serializado
            string g_dados_fc_separada = "";
            for (int i = 0; i < 10; i++)
            {
                int media = fc_qtd[i] > 0 ? (int)Math.Round((double)fc_med[i] / fc_qtd[i]) : 0;
                g_dados_fc_separada += media.ToString("D3");     // média
                g_dados_fc_separada += fc_min[i].ToString("D3"); // mínima
                g_dados_fc_separada += fc_max[i].ToString("D3"); // máxima
            }

            // Atualização no banco
            string sql = $@"
                            UPDATE tbl_ResumoExame 
                            SET 
                                FC_Menor = {FC_MENOR},
                                FC_Maior = {FC_MAIOR},
                                FC_REM_MEDIA = {FC_REM_MEDIA},
                                FC_REM_MAIOR = {FC_REM_MAIOR},
                                FC_NREM_MEDIA = {FC_NREM_MEDIA},
                                FC_NREM_MAIOR = {FC_NREM_MAIOR},
                                FC_VIGILIA_MEDIA = {FC_VIGILIA_MEDIA},
                                FC_VIGILIA_MAIOR = {FC_VIGILIA_MAIOR},
                                FC_MEDIA = {Convert.ToInt32(FC_MEDIA)}
                        ";

            ExecutaSQLParaAlteracao(cnn_dbExame, sql);

            //Comentarios
            ExecutaSQLParaAlteracao(cnn_dbRelatorio, "DELETE * FROM tbl_Comentarios");
            if (GlobVar.tbl_Comentarios != null)
            {
                var listaComentarios = GlobVar.tbl_Comentarios.AsEnumerable()
                                        .Where(row => row.Field<int>("NumPag") >= pag_noite && row.Field<int>("NumPag") <= pag_dia)
                                        .OrderBy(row => row.Field<int>("Seq"));
                if (listaComentarios != null)
                {
                    foreach (DataRow row in listaComentarios)
                    {
                        int seq = Convert.ToInt32(row["seq"]);
                        //DataRow comentarioRow = GetComentarioBySeq(cnn_dbExame, seq);

                        if (row != null)
                        {
                            string comentario = row["Comentario"] == DBNull.Value ? "" : row["Comentario"].ToString();

                            // Remove aspas simples
                            string comentarioCorrigido = comentario.Replace("'", "");

                            // Limita a 50 caracteres
                            comentarioCorrigido = comentarioCorrigido.Length > 50 ? comentarioCorrigido.Substring(0, 50) : comentarioCorrigido;
                            int numPag = Convert.ToInt32(row["NumPag"]);

                            var rowpag = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == numPag).FirstOrDefault(); // ← adapta esse método conforme o seu uso

                            int epoca = (int)(numPag / 30) + 1;
                            string horario = DateTime.TryParse(rowpag["Horario"]?.ToString(), out DateTime dtHorario)
                                ? dtHorario.ToString("HH:mm:ss")
                                : "00:00:00";

                            string sqlInsert = $@"INSERT INTO tbl_Comentarios (Pagina, Epoca, Horario, DescrComent) 
                                            VALUES ({numPag}, {epoca}, '{horario}', '{comentarioCorrigido}')
                                        ";

                            ExecutaSQLParaAlteracao(cnn_dbRelatorio, sqlInsert);
                        }
                    }
                }
            }

            // Verifica se retornou resultado e define o TTS
            double TTS;
            if (GlobVar.tbl_ResumoExame != null && GlobVar.tbl_ResumoExame.Rows.Count > 0)
            {
                double valorTTS = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);
                TTS = valorTTS > 0 ? valorTTS : 1;
            }
            else
            {
                TTS = 1;
            }

            var deleteSql = "DELETE * FROM tbl_RelatResumoEventos";
            ExecutaSQLParaAlteracao(cnn_dbExame, deleteSql);

            sql = "";

            if (g_adulto.Equals("A"))
            {
                sql = $"SELECT CodEvento, COUNT(CodEvento) AS Qtd_Evento, SUM(Duracao) AS Dur_Total, MAX(Duracao) AS Maior_Dur " +
                      $"FROM Cons_EventosComEstag " +
                      $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                      $"AND (Estagio = 1 OR Estagio = 2 OR Estagio = 3 OR Estagio = 5) " +
                      $"GROUP BY CodEvento";
            }
            else if (g_adulto.Equals("I"))
            {
                sql = $"SELECT CodEvento, COUNT(CodEvento) AS Qtd_Evento, SUM(Duracao) AS Dur_Total, MAX(Duracao) AS Maior_Dur " +
                      $"FROM Cons_EventosComEstag " +
                      $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                      $"AND (Estagio >= 7 AND Estagio <= 9) " +
                      $"GROUP BY CodEvento";
            }
            else if (g_adulto.Equals("C"))
            {
                sql = $"SELECT CodEvento, COUNT(CodEvento) AS Qtd_Evento, SUM(Duracao) AS Dur_Total, MAX(Duracao) AS Maior_Dur " +
                      $"FROM Cons_EventosComEstag " +
                      $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                      $"AND (Estagio >= 1 AND Estagio <= 5) " +
                      $"GROUP BY CodEvento";
            }
            else if (g_adulto.Equals("B"))
            {
                sql = $"SELECT CodEvento, COUNT(CodEvento) AS Qtd_Evento, SUM(Duracao) AS Dur_Total, MAX(Duracao) AS Maior_Dur " +
                      $"FROM Cons_EventosComEstag " +
                      $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                      $"AND (Estagio >= 4 AND Estagio <= 6) " +
                      $"GROUP BY CodEvento";
            }

            DataTable tbl = ExecutaSQL(cnn_dbExame, sql);
            string descrEvento = "";
            string corFundo = "";
            string corTexto = "";// validar o codigo abaixo 
            if (tbl != null)
            {
                GlobVar.tbl_RelatResumo = ExecutaSQL(cnn_Config, "SELECT * FROM tbl_RelatResumo");
                foreach (DataRow tbl_RelatResumo in GlobVar.tbl_RelatResumo.Rows)
                {
                    int codGrupo = Convert.ToInt32(tbl_RelatResumo["CodGrupo"]);
                    string descrGrupo = tbl_RelatResumo["DescrGrupo"].ToString();

                    DataTable tbl_RelatResumoItem = ExecutaSQL(cnn_Config, "SELECT * FROM tbl_RelatResumoItem");
                    // Filtra usando LINQ
                    var itensFiltrados = GlobVar.tbl_RelatResumoItem.AsEnumerable()
                        .Where(row => row.Field<int>("CodGrupo") == codGrupo);

                    // Só tenta copiar se houver resultados
                    if (itensFiltrados.Any())
                    {
                        tbl_RelatResumoItem = itensFiltrados.CopyToDataTable();
                    }
                    if (tbl_RelatResumoItem != null && tbl_RelatResumoItem.AsEnumerable().Any(row => row.Field<int>("CodGrupo") == codGrupo))
                    {
                        foreach (DataRow row_RelatResumoItem in tbl_RelatResumoItem.Rows)
                        {
                            int codEvento = Convert.ToInt32(row_RelatResumoItem["CodEvento"]);
                            GetDadosEvento(codEvento, out descrEvento, out corFundo, out corTexto);
                            var filtrado = tbl.AsEnumerable()
                                              .Where(row => row.Field<int>("CodEvento") == codEvento);

                            if (tbl != null && tbl.AsEnumerable().Any(row => row.Field<int>("CodEvento") == codEvento))
                            {
                                int index = tbl.Rows.IndexOf(tbl.AsEnumerable().Where(row => row.Field<int>("CodEvento") == codEvento).FirstOrDefault());
                                DataRow rw = tbl.Rows[index];
                                if (!EventosCardiacos.AsEnumerable().Any(row => row.Field<int>("CodEvento") == codEvento))
                                {
                                    if (g_adulto.Equals("A"))
                                    {
                                        sql = $"SELECT FIRST(Posicao) AS Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                    $"AND (Estagio >= 1 AND Estagio <= 5) " +
                                                    $"AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    $"GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("I"))
                                    {
                                        sql = $"SELECT FIRST(Posicao) AS Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                    $"AND (Estagio >= 7 AND Estagio <= 9) " +
                                                    $"AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    $"GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("C"))
                                    {
                                        sql = $"SELECT FIRST(Posicao) AS Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                    $"AND (Estagio >= 1 AND Estagio <= 5) " +
                                                    $"AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    $"GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("B"))
                                    {
                                        sql = $"SELECT FIRST(Posicao) AS Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                    $"AND (Estagio >= 4 AND Estagio <= 6) " +
                                                    $"AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    $"GROUP BY CodEvento";
                                    }

                                    DataTable tbl3 = ExecutaSQL(cnn_dbExame, sql); // 'sql' já foi montado no trecho anterior

                                    int qtd_pos_c = 0;
                                    if (tbl3.Rows.Count > 0)
                                    {
                                        qtd_pos_c = Convert.ToInt32(tbl3.Rows[0]["Qtd_Evento"]);
                                    }
                                    // Agora monta o SQL para os eventos em NREM conforme g_adulto
                                    if (g_adulto.Equals("A"))
                                    {
                                        sql = $"SELECT Count(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                $"AND Estagio >= 1 AND Estagio <= 3 " +
                                                $"AND CodEvento = {codEvento} " +
                                                $"GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("I"))
                                    {
                                        sql = $"SELECT Count(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                $"AND Estagio >= 7 AND Estagio <= 9 " +
                                                $"AND CodEvento = {codEvento} " +
                                                $"GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("C"))
                                    {
                                        sql = $"SELECT Count(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                $"AND Estagio >= 1 AND Estagio <= 4 " +
                                                $"AND CodEvento = {codEvento} " +
                                                $"GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("B"))
                                    {
                                        sql = $"SELECT Count(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                $"WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} " +
                                                $"AND (Estagio = 4 OR Estagio <= 6) " +
                                                $"AND CodEvento = {codEvento} " +
                                                $"GROUP BY CodEvento";
                                    }

                                    // Executa a segunda consulta SQL (NREM)
                                    DataTable tbl4 = ExecutaSQL(cnn_dbExame, sql);

                                    int qtd_nrem = 0;
                                    if (tbl4.Rows.Count > 0)
                                    {
                                        qtd_nrem = Convert.ToInt32(tbl4.Rows[0]["Qtd_Evento"]);
                                    }
                                    // Consulta quantidade de eventos em REM (Estágio 5)
                                    sql = $@"
                                        SELECT COUNT(CodEvento) AS Qtd_Evento 
                                        FROM Cons_EventosComEstag 
                                        WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} 
                                            AND Estagio = 5 
                                            AND CodEvento = {codEvento}
                                        GROUP BY CodEvento
                                    ";
                                    tbl4 = ExecutaSQL(cnn_dbExame, sql);
                                    int qtd_rem = tbl4.Rows.Count > 0 ? Convert.ToInt32(tbl4.Rows[0]["Qtd_Evento"]) : 0;

                                    int qtdEvento = Convert.ToInt32(rw["Qtd_Evento"]);
                                    double durTotal = Convert.ToDouble(rw["Dur_Total"]);
                                    double maiorDur = Convert.ToDouble(rw["Maior_Dur"]);

                                    // Supondo que você tenha essas variáveis definidas:
                                    double duracaoMedia = (durTotal / GlobVar.namos) / qtdEvento;
                                    double maiorDurFormatada = maiorDur / GlobVar.namos;
                                    double qtdHora = qtdEvento / (TTS / 3600.0);
                                    double durTotalFormatada = durTotal / GlobVar.namos;
                                    int qtdPosX = qtdEvento - qtd_pos_c;

                                    // Formatar com ponto decimal
                                    string ReplaceComma(double valor) => valor.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

                                    // Monta SQL de INSERT
                                    string sqlInsert = $@"
                                            INSERT INTO tbl_RelatResumoEventos 
                                            (CodEvento, DescrGrupo, DescrEvento, Qtd, DuracaoMedia, MaiorDuracao, QtdHora, DuracaoTotal, QtdPosC, QtdPosX, QtdNREM, QtdREM)
                                            VALUES (
                                                {codEvento},
                                                '{descrGrupo}',
                                                '{descrEvento}',
                                                {qtdEvento},
                                                {ReplaceComma(duracaoMedia)},
                                                {ReplaceComma(maiorDurFormatada)},
                                                {ReplaceComma(qtdHora)},
                                                {ReplaceComma(durTotalFormatada)},
                                                {qtd_pos_c},
                                                {qtdPosX},
                                                {qtd_nrem},
                                                {qtd_rem})";
                                    using (OleDbCommand insertCommand = new OleDbCommand(sqlInsert, cnn_dbExame))
                                    {
                                        insertCommand.ExecuteNonQuery();
                                    }
                                }

                            }
                        }

                    }
                }
            }

            sql = "";

            if (g_adulto == "A")
            {
                sql = "SELECT Cons_EventosComEstag.CodEvento, COUNT(Cons_EventosComEstag.CodEvento) AS Qtd_Evento, " +
                      "SUM(Cons_EventosComEstag.Duracao) AS Dur_Total, MAX(Cons_EventosComEstag.Duracao) AS Maior_Dur " +
                      "FROM Cons_EventosComEstag " +
                      "WHERE (Estagio = 0 OR Estagio = 1 OR Estagio = 2 OR Estagio = 3 OR Estagio = 5) " +
                      "GROUP BY Cons_EventosComEstag.CodEvento";
            }
            else if (g_adulto == "I")
            {
                sql = "SELECT Cons_EventosComEstag.CodEvento, COUNT(Cons_EventosComEstag.CodEvento) AS Qtd_Evento, " +
                      "SUM(Cons_EventosComEstag.Duracao) AS Dur_Total, MAX(Cons_EventosComEstag.Duracao) AS Maior_Dur " +
                      "FROM Cons_EventosComEstag " +
                      "WHERE ((Estagio >= 7 AND Estagio <= 9) OR (Estagio = 0)) " +
                      "GROUP BY Cons_EventosComEstag.CodEvento";
            }
            else if (g_adulto == "C")
            {
                sql = "SELECT Cons_EventosComEstag.CodEvento, COUNT(Cons_EventosComEstag.CodEvento) AS Qtd_Evento, " +
                      "SUM(Cons_EventosComEstag.Duracao) AS Dur_Total, MAX(Cons_EventosComEstag.Duracao) AS Maior_Dur " +
                      "FROM Cons_EventosComEstag " +
                      "WHERE (Estagio <= 5) " +
                      "GROUP BY Cons_EventosComEstag.CodEvento";
            }
            else if (g_adulto == "B")
            {
                sql = "SELECT Cons_EventosComEstag.CodEvento, COUNT(Cons_EventosComEstag.CodEvento) AS Qtd_Evento, " +
                      "SUM(Cons_EventosComEstag.Duracao) AS Dur_Total, MAX(Cons_EventosComEstag.Duracao) AS Maior_Dur " +
                      "FROM Cons_EventosComEstag " +
                      "WHERE (Estagio <= 6) " +
                      "GROUP BY Cons_EventosComEstag.CodEvento";
            }
            tbl.Clear();
            tbl = ExecutaSQL(cnn_dbExame, sql);
            if (tbl != null)
            {
                GlobVar.tbl_RelatResumo = ExecutaSQL(cnn_Config, "SELECT * FROM tbl_RelatResumo");
                foreach (DataRow tbl_RelatResumo in GlobVar.tbl_RelatResumo.Rows)
                {
                    int codGrupo = Convert.ToInt32(tbl_RelatResumo["CodGrupo"]);
                    string descrGrupo = tbl_RelatResumo["DescrGrupo"].ToString();

                    DataTable tbl_RelatResumoItem = ExecutaSQL(cnn_Config, "SELECT * FROM tbl_RelatResumoItem");
                    // Filtra usando LINQ
                    var itensFiltrados = GlobVar.tbl_RelatResumoItem.AsEnumerable()
                        .Where(row => row.Field<int>("CodGrupo") == codGrupo);

                    // Só tenta copiar se houver resultados
                    if (itensFiltrados.Any())
                    {
                        tbl_RelatResumoItem = itensFiltrados.CopyToDataTable();
                    }
                    if (tbl_RelatResumoItem != null && itensFiltrados.Any())
                    {
                        foreach (DataRow row_RelatResumoItem in tbl_RelatResumoItem.Rows)
                        {
                            int codEvento = Convert.ToInt32(row_RelatResumoItem["CodEvento"]);
                            GetDadosEvento(codEvento, out descrEvento, out corFundo, out corTexto);
                            var filtrado = tbl.AsEnumerable()
                                              .Where(row => row.Field<int>("CodEvento") == codEvento);

                            if (filtrado.Any())
                            {
                                tbl = filtrado.CopyToDataTable();
                            }
                            if (tbl != null && filtrado.Any())
                            {
                                int index = tbl.Rows.IndexOf(tbl.AsEnumerable().Where(row => row.Field<int>("CodEvento") == codEvento).FirstOrDefault());
                                DataRow rw = tbl.Rows[index];

                                if (EventosCardiacos.AsEnumerable().Any(row => row.Field<int>("CodEvento") == codEvento))
                                {
                                    if (g_adulto.Equals("A"))
                                    {
                                        sql = $"SELECT First(Posicao) As Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE (Estagio <= 5) AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    "GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("I"))
                                    {
                                        sql = $"SELECT First(Posicao) As Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE ((Estagio >= 7 AND Estagio <= 9) OR Estagio = 0) AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    "GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("C"))
                                    {
                                        sql = $"SELECT First(Posicao) As Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE Estagio <= 5 AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    "GROUP BY CodEvento";
                                    }
                                    else if (g_adulto.Equals("B"))
                                    {
                                        sql = $"SELECT First(Posicao) As Posicao, COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag " +
                                                    $"WHERE Estagio <= 6 AND CodEvento = {codEvento} AND Posicao = 'C' " +
                                                    "GROUP BY CodEvento";
                                    }

                                    DataTable tbl3 = ExecutaSQL(cnn_dbExame, sql);
                                    int qtd_pos_c = 0;
                                    if (tbl3.Rows.Count == 0)
                                        qtd_pos_c = 0;
                                    else
                                        qtd_pos_c = Convert.ToInt32(tbl3.Rows[0]["Qtd_Evento"]);
                                    switch (g_adulto)
                                    {
                                        case "A":
                                            sql = $"SELECT COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag WHERE Estagio <= 4 AND CodEvento = {codEvento} GROUP BY CodEvento";
                                            break;

                                        case "I":
                                            sql = $"SELECT COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag WHERE (Estagio >= 7 AND Estagio <= 9) OR Estagio = 0 AND CodEvento = {codEvento} GROUP BY CodEvento";
                                            break;

                                        case "C":
                                            sql = $"SELECT COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag WHERE Estagio <= 4 AND CodEvento = {codEvento} GROUP BY CodEvento";
                                            break;

                                        case "B":
                                            sql = $"SELECT COUNT(CodEvento) AS Qtd_Evento FROM Cons_EventosComEstag WHERE (Estagio = 4 OR Estagio = 6 OR Estagio = 0) AND CodEvento = {codEvento} GROUP BY CodEvento";
                                            break;
                                    }
                                    DataTable tbl4 = ExecutaSQL(cnn_dbExame, sql);
                                    int qtd_nrem = 0;
                                    if (tbl4.Rows.Count == 0)
                                        qtd_nrem = 0;
                                    else
                                        qtd_nrem = Convert.ToInt32(tbl4.Rows[0]["Qtd_Evento"]);
                                    int qtd_rem = 0;

                                    // --- NREM já foi calculado anteriormente ---
                                    // Agora calcula o REM
                                    sql = $"SELECT COUNT(CodEvento) AS Qtd_Evento " +
                                            $"FROM Cons_EventosComEstag " +
                                            $"WHERE Estagio = 5 AND CodEvento = {codEvento} " +
                                            $"GROUP BY CodEvento";
                                    tbl4.Clear();
                                    tbl4 = ExecutaSQL(cnn_dbExame, sql);

                                    if (tbl4.Rows.Count == 0)
                                        qtd_rem = 0;
                                    else
                                        qtd_rem = Convert.ToInt32(tbl4.Rows[0]["Qtd_Evento"]);

                                    int qtdEvento = Convert.ToInt32(tbl.Rows[0]["Qtd_Evento"]);
                                    double durTotal = Convert.ToDouble(tbl.Rows[0]["Dur_Total"]);
                                    double maiorDur = Convert.ToDouble(tbl.Rows[0]["Maior_Dur"]);

                                    double duracaoMedia = (durTotal / GlobVar.namos) / qtdEvento;
                                    double maiorDuracao = maiorDur / GlobVar.namos;
                                    double qtdHora = qtdEvento / (TTS / 3600.0);
                                    double duracaoTotal = durTotal / GlobVar.namos;

                                    int qtdPosX = qtdEvento - qtd_pos_c;

                                    sql = $@"
                                        INSERT INTO tbl_RelatResumoEventos 
                                        (CodEvento, DescrGrupo, DescrEvento, Qtd, DuracaoMedia, MaiorDuracao, QtdHora, DuracaoTotal, QtdPosC, QtdPosX, QtdNREM, QtdREM)
                                        VALUES (
                                            {codEvento},
                                            '{GlobVar.tbl_RelatResumo.Rows[codGrupo - 1]["DescrGrupo"]}',
                                            '{descrEvento}',
                                            {qtdEvento},
                                            {duracaoMedia.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                                            {maiorDuracao.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                                            {qtdHora.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                                            {duracaoTotal.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                                            {qtd_pos_c},
                                            {qtdPosX},
                                            {qtd_nrem},
                                            {qtd_rem}
                                )";

                                    using (OleDbCommand insertCommand = new OleDbCommand(sql, cnn_dbExame))
                                    {
                                        insertCommand.ExecuteNonQuery();
                                    }

                                }
                            }

                        }

                    }
                }
            }

            cnn_dbExame.Close();
            cnn_dbRelatorio.Close();
            cnn_dbRelatorio.Close();
        }

        public static DataTable ExecutaSQL(OleDbConnection connection, string sql)
        {
            DataTable result = new DataTable();

            try
            {
                // 1. Extrair o nome da tabela da consulta SQL (simples).
                string tableName = ExtrairNomeTabela(sql);
                if (!string.IsNullOrEmpty(tableName))
                {
                    // 2. Verificar se a tabela existe
                    if (!TabelaExiste(connection, tableName))
                    {
                        // Se não existe, apenas retorna a tabela vazia
                        return result;
                    }
                }

                // 3. Executa normalmente caso a tabela exista
                using (OleDbCommand command = new OleDbCommand(sql, connection))
                {
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate o erro conforme necessário
                Console.WriteLine("Erro ao executar SQL: " + ex.Message + " Codigo usado: " + sql);
            }

            return result;
        }

        // Função auxiliar para verificar se a tabela existe
        private static bool TabelaExiste(OleDbConnection conn, string tableName)
        {
            // Buscar pelo catálogo de tabelas do banco
            DataTable tables = conn.GetSchema("Tables");
            foreach (DataRow row in tables.Rows)
            {
                if (row["TABLE_NAME"].ToString().Equals(tableName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        // Função auxiliar (opcional/idealmente melhorada) para extrair o nome da tabela SELECT simples
        private static string ExtrairNomeTabela(string sql)
        {
            // Simples e limitado: só funciona para "SELECT ... FROM TABELA ..."
            var tokens = sql.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length - 1; i++)
            {
                if (tokens[i] == "FROM")
                {
                    return tokens[i + 1];
                }
            }
            return null;
        }
        public static void ExecutaSQLParaAlteracao(OleDbConnection conexao, string sql)
        {
            using (OleDbCommand comando = new OleDbCommand(sql, conexao))
            {
                comando.ExecuteNonQuery();
            }
        }
        public static void GetDadosEvento(int codEvento, out string descrEvento, out string corFundo, out string corTexto)
        {
            descrEvento = string.Empty;
            corFundo = string.Empty;
            corTexto = string.Empty;

            if (GlobVar.tbl_CadEvento == null)
                return;

            DataRow row = GlobVar.tbl_CadEvento.AsEnumerable()
                .FirstOrDefault(r => Convert.ToInt32(r["CodEvento"]) == codEvento);

            if (row != null)
            {
                descrEvento = row["DescrEvento"]?.ToString() ?? string.Empty;
                corFundo = row["CorFundo"]?.ToString() ?? string.Empty;
                corTexto = row["CorTexto"]?.ToString() ?? string.Empty;
            }
        }
        public static string GetAdinfo()
        {
            string adInfo = "A";

            if (GlobVar.tbl_DadosExame.Rows.Count > 0)
            {
                var row = GlobVar.tbl_DadosExame.Rows[0];

                if (DateTime.TryParse(row["DataNascimento"].ToString(), out DateTime dataNascimento) &&
                    DateTime.TryParse(row["DataRealizacao"].ToString(), out DateTime dataRealizacao))
                {
                    // Calcula a diferença total de meses
                    int totalMeses = (dataRealizacao.Year - dataNascimento.Year) * 12 + (dataRealizacao.Month - dataNascimento.Month);

                    if (dataRealizacao.Day < dataNascimento.Day)
                    {
                        // Ajusta se ainda não completou o mês
                        totalMeses--;
                    }

                    // Classificação com base nos meses
                    if (totalMeses > 12)
                        adInfo = "A";
                    else if (totalMeses >= 2)
                        adInfo = "I";
                    else
                        adInfo = "B";
                }
            }

            return adInfo;
        }
        static void IncluiComentarios()
        {
            int linha = 1;

            foreach (DataRow row in GlobVar.tbl_Comentarios.Rows)
            {
                string comentario = row["Comentario"]?.ToString()?.Trim() ?? "";

                if (string.IsNullOrEmpty(comentario))
                    continue;

                // Verifica se o comentário começa com uma data no formato dd/MM/yy ou dd/MM/yyyy
                string possivelData = comentario.Length >= 8 ? comentario.Substring(0, 8) : "";
                bool temData = DateTime.TryParse(possivelData, out DateTime data);

                if (temData)
                {
                    ObjExcel.Cells[linha, 1] = data.ToString("dd/MM/yy");
                    ObjExcel.Cells[linha, 2] = comentario.Length > 11 ? comentario.Substring(11).Trim() : "";
                }
                else
                {
                    // Comentários longos são quebrados em blocos de até 250 caracteres
                    while (comentario.Length > 0)
                    {
                        string parte;
                        if (comentario.Length <= 250)
                        {
                            parte = comentario;
                            comentario = "";
                        }
                        else
                        {
                            int corte = comentario.LastIndexOf(' ', 250);
                            if (corte == -1) corte = 250;
                            parte = comentario.Substring(0, corte).Trim();
                            comentario = comentario.Substring(corte).Trim();
                        }

                        ObjExcel.Cells[linha, 2] = parte;
                        linha++;
                    }

                    continue;
                }

                linha++;
            }

            ColaResumoEventos("&(COMENTARIOS)&", 1, linha - 1);
        }
        static void ColaResumoEventos(string header, int linhaIni, int linhaFim)
        {
            // Seleciona o intervalo no Excel e copia
            string rangeStr = $"{linhaIni}:{linhaFim}";
            ObjExcel.Range[rangeStr].Select();
            ObjExcel.Selection.Copy();

            // Acha o marcador no Word
            var selection = wordApp.Selection;
            selection.Find.ClearFormatting();

            selection.Find.Text = header;
            selection.Find.Replacement.Text = "";
            selection.Find.Forward = true;
            selection.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
            selection.Find.Format = false;
            selection.Find.MatchCase = false;
            selection.Find.MatchWholeWord = false;
            selection.Find.MatchWildcards = false;
            selection.Find.MatchSoundsLike = false;
            selection.Find.MatchAllWordForms = false;

            if (selection.Find.Execute())
            {
                // Cola a tabela do Excel no Word
                selection.PasteExcelTable(false, false, false);
            }
        }

        public static void F_PreencheLaudoDOC(string texto)
        {
            string hora_boa_noite = "";
            string hora_bom_dia = "";
            DataTable rs = new DataTable();
            string texto_co2 = "";
            int NroArq = 0;
            int pag_noite = Canais.Get_BoaNoite();
            int pag_dia = Canais.Get_BomDia();

            string connectionStringDatBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False;";
            OleDbConnection cnn_dbExame = new OleDbConnection(connectionStringDatBd);
            cnn_dbExame.Open();
            string connectionStringConfigBd = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.configBD};Persist Security Info=False;";
            OleDbConnection cnn_dbConfig = new OleDbConnection(connectionStringDatBd);
            cnn_dbConfig.Open();

            tbl_HipnoLaudo = GlobVar.tbl_HipnoLaudo.AsEnumerable().CopyToDataTable();
            // Verifica se existe alguma linha com o "Laudo" igual ao texto do comboBox
            if (tbl_HipnoLaudo.AsEnumerable().Any(row => row.Field<string>("Laudo") == comboBox1.Text))
            {
                // Filtra a tabela apenas para as linhas correspondentes
                var linhasFiltradas = tbl_HipnoLaudo.AsEnumerable()
                                                    .Where(row => row.Field<string>("Laudo") == comboBox1.Text);

                tbl_HipnoLaudo = linhasFiltradas.CopyToDataTable();

                var hipnograma = tbl_HipnoLaudo.Rows[0]["Hipnogramas"]?.ToString();

                if (!string.IsNullOrEmpty(hipnograma))
                {
                    if (!hipnograma.Equals(HipnoMostrando.Text))
                    {
                        int index = HipnoMostrando.Items.IndexOf(hipnograma);
                        if (index >= 0)
                        {
                            HipnoMostrando.TabIndex = index;
                        }
                    }
                }
            }
            string Montagem = GlobVar.tbl_MontGrav.Rows[0]["NomeMontagem"].ToString();
            barraLoading.Value += 5; // ou += 10, etc.
            Application.DoEvents();  // Atualiza visual

            if (GlobVar.tbl_DadosExame != null && GlobVar.tbl_ResumoExame != null)
            {
                if (GlobVar.tbl_DadosExame.Rows.Count > 0)
                {
                    var tbl_DadosExame = GlobVar.tbl_DadosExame.Rows[0];
                    if (DateTime.TryParse(tbl_DadosExame["DataNascimento"].ToString(), out DateTime dataNascimento) &&
                        DateTime.TryParse(tbl_DadosExame["DataRealizacao"].ToString(), out DateTime dataRealizado))
                    {
                        if (dataNascimento < dataRealizado)
                        {
                            // Subtrai um ano da data de nascimento para calcular a idade
                            DateTime idade = dataNascimento.AddYears(-1);

                            // Calcula a diferença em anos
                            int idadeAno = dataRealizado.Year - idade.Year;
                            if (dataRealizado < idade.AddYears(idadeAno))
                            {
                                idadeAno--;
                            }

                            // Calcula a diferença em meses
                            int idadeMes = dataRealizado.Month - idade.Month;
                            if (idadeMes < 0)
                            {
                                idadeMes += 12;
                            }

                            // Calcula a diferença em dias
                            int idadeDia = dataRealizado.Day - idade.Day;
                            if (idadeDia < 0)
                            {
                                DateTime tempDate = dataRealizado.AddMonths(-1);
                                idadeDia += DateTime.DaysInMonth(tempDate.Year, tempDate.Month);
                            }

                            // Ajusta o ano, se necessário (caso a idade seja maior que 100 anos)
                            if (DateTime.Now.Year - idade.Year > 100)
                            {
                                idadeAno += 100;
                            }

                            // Converte DataNascimento para formato dd/MM/yyyy
                            string dataNascimentoFormatada = dataNascimento.ToString("dd/MM/yyyy");

                            // Manipulação de textos de Idade
                            string IdadeAno = idadeAno == 0 ? "" : idadeAno == 1 ? "1 " + f_var("Var56292") : idadeAno + " " + f_var("Var56143");
                            string IdadeMes = idadeMes == 0 ? "" : idadeMes == 1 ? "1 " + f_var("Var56201") : idadeMes + " " + f_var("Var56202");
                            string IdadeDia = idadeDia == 0 ? "" : idadeDia == 1 ? "1 " + f_var("Var56300") : idadeDia + " " + f_var("Var56301");

                            // IdadeAMD
                            string IdadeAMD = "";
                            if (IdadeAno != "" && IdadeMes != "" && IdadeDia != "")
                            {
                                IdadeAMD = IdadeAno + ", " + IdadeMes + " " + f_var("Var56160") + " " + IdadeDia;
                            }
                            else
                            {
                                if (IdadeAno != "" && IdadeMes != "")
                                {
                                    IdadeAMD = IdadeAno + " " + f_var("Var56160") + " " + IdadeMes;
                                }
                                else if (IdadeAno != "" && IdadeDia != "")
                                {
                                    IdadeAMD = IdadeAno + " " + f_var("Var56160") + " " + IdadeDia;
                                }
                                else if (IdadeMes != "" && IdadeDia != "")
                                {
                                    IdadeAMD = IdadeMes + " " + f_var("Var56160") + " " + IdadeDia;
                                }
                                else if (IdadeDia != "")
                                {
                                    IdadeAMD = IdadeDia;
                                }
                            }

                            // IdadeAM
                            string IdadeAM = "";
                            if (IdadeAno != "")
                            {
                                IdadeAM = IdadeMes != "" ? IdadeAno + " " + f_var("Var56160") + " " + IdadeMes : IdadeAno;
                            }

                            // Atribui Idade
                            string Idade = IdadeAno;

                        }
                        else
                        {
                            // Caso em que DataNascimento não é válida ou não é anterior a DataRealizacao
                            string Idade = "";
                            string IdadeAM = "";
                            string IdadeAMD = "";

                            // Se IdadeAno está presente na tabela
                            if (Convert.ToInt32(tbl_DadosExame["IdadeAno"]) > 0)
                            {
                                Idade = tbl_DadosExame["IdadeAno"].ToString() + " " + f_var("Var56143");

                                if (Convert.ToInt32(tbl_DadosExame["IdadeMes"]) > 0)
                                {
                                    if (Convert.ToInt32(tbl_DadosExame["IdadeMes"]) == 1)
                                    {
                                        Idade += " " + f_var("Var56160") + " 1 " + f_var("Var56201");
                                    }
                                    else
                                    {
                                        Idade += " " + f_var("Var56160") + " " + tbl_DadosExame["IdadeMes"] + " " + f_var("Var56202");
                                    }
                                }
                            }
                            else if (Convert.ToInt32(tbl_DadosExame["IdadeMes"]) > 0)
                            {
                                Idade = Convert.ToInt32(tbl_DadosExame["IdadeMes"]) == 1
                                    ? "1 " + f_var("Var56201")
                                    : tbl_DadosExame["IdadeMes"].ToString() + " " + f_var("Var56202");
                            }
                            else
                            {
                                Idade = "";
                            }

                            // Atribui IdadeAM e IdadeAMD
                            IdadeAM = Idade;
                            IdadeAMD = Idade;
                        }
                    }
                }

                string arq;
                int pos1 = GlobVar.bDataFile.LastIndexOf("\\");
                if (pos1 > 0)
                    arq = GlobVar.bDataFile.Substring(pos1 + 1);
                else
                    arq = GlobVar.bDataFile;

                // Carregando páginas para pegar horários
                DataTable tbl_Paginas = ExecutaSQL(cnn_dbExame, "SELECT * FROM tbl_Paginas ORDER BY NumPag");
                object valorData = GlobVar.tbl_DadosExame.Rows[0]["DataRealizacao"];

                DateTime dataRealizacao = new DateTime();

                if (valorData != null && valorData != DBNull.Value)
                {
                    if (DateTime.TryParse(valorData.ToString(), out dataRealizacao))
                    {
                        // Ok, dataRealizacao foi parseada com sucesso
                    }
                    else
                    {
                        // Aqui você pode decidir: ou joga uma exceção, ou define um valor padrão
                        dataRealizacao = DateTime.MinValue; // ou qualquer data padrão
                    }
                }
                DateTime horarioPrimeiraPagina = Convert.ToDateTime(tbl_Paginas.Rows[0]["horario"]);
                DateTime horarioUltimaPagina = Convert.ToDateTime(tbl_Paginas.Rows[^1]["horario"]);

                DateTime inicio_grav = dataRealizacao.Date.Add(horarioPrimeiraPagina.TimeOfDay);
                DateTime fim_grav = dataRealizacao.Date.Add(horarioUltimaPagina.TimeOfDay);
                if (fim_grav < inicio_grav)
                    fim_grav = fim_grav.AddDays(1);

                // Cálculo do tempo de ronco
                string sql = $"SELECT CodEvento, COUNT(CodEvento) AS Qtd_Evento, SUM(Duracao) AS Dur_Total, MAX(Duracao) AS Maior_Dur " +
                             $"FROM Cons_EventosComEstag WHERE estagio > 0 AND Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia} AND CodEvento = 13 " +
                             $"GROUP BY CodEvento";
                DataTable tbl = ExecutaSQL(cnn_dbExame, sql);
                tempo_ronco = tbl.Rows.Count == 0 ? 0 : Convert.ToInt32(tbl.Rows[0]["Dur_Total"]) / GlobVar.namos;

                // Apneia e Hipopnéia com Dessaturação
                 qtd_ap_cen_com_dessat = GetQtd("Cons_ApCen_Com_Dessat");
                 qtd_ap_obs_com_dessat = GetQtd("Cons_ApObs_Com_Dessat");
                 qtd_ap_mis_com_dessat = GetQtd("Cons_ApMis_Com_Dessat");
                 qtd_hipop_com_dessat = GetQtd("Cons_Hipop_Com_Dessat");

                // Com microdespertar
                 qtd_ap_cen_com_mdesp = GetQtd("Cons_ApCen_Com_MDesp");
                 qtd_ap_obs_com_mdesp = GetQtd("Cons_ApObs_Com_MDesp");
                 qtd_ap_mis_com_mdesp = GetQtd("Cons_ApMis_Com_MDesp");
                 qtd_hipop_com_mdesp = GetQtd("Cons_Hipop_Com_MDesp");

                // Com Dessaturação e Microdespertar
                 qtd_ap_cen_com_dessat_e_mdesp = GetQtdJoin("Cons_ApCen_Com_Dessat", "Cons_ApCen_Com_MDesp", "Cons_Eventos_ApCen");
                 qtd_ap_obs_com_dessat_e_mdesp = GetQtdJoin("Cons_ApObs_Com_Dessat", "Cons_ApObs_Com_MDesp", "Cons_Eventos_ApObs");
                 qtd_ap_mis_com_dessat_e_mdesp = GetQtdJoin("Cons_ApMis_Com_Dessat", "Cons_ApMis_Com_MDesp", "Cons_Eventos_ApMis");
                 qtd_hipop_com_dessat_e_mdesp = GetQtdJoin("Cons_Hipop_Com_Dessat", "Cons_Hipop_Com_MDesp", "Cons_Eventos_Hipop");

                // RERA
                 qtd_RERA_com_dessat = GetQtd("Cons_RERA_Com_Dessat");
                 qtd_RERA_com_mdesp = GetQtd("Cons_RERA_Com_MDesp");
                 qtd_RERA_com_dessat_e_mdesp = GetQtdJoin("Cons_RERA_Com_Dessat", "Cons_RERA_Com_MDesp", "Cons_Eventos_RERA");

                // PLM
                 qtd_PLM_com_mdesp = GetQtd("Cons_PLM_Com_MDesp");

                //Resumo de Eventos
                //Central
                SubstituiVar("&(QTD_APNEIA)&", ev_ap_cen.qtd.ToString("0"));
                SubstituiVar("&(DUR_APNEIA)&", TimeSpan.FromSeconds(ev_ap_cen.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_APNEIA)&", TimeSpan.FromSeconds(ev_ap_cen.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_APNEIA)&", TimeSpan.FromSeconds(ev_ap_cen.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(QTD_HR_APNEIA)&", TimeSpan.FromSeconds(ev_ap_cen.indice).ToString(@"hh\:mm\:ss"));

                //Obstrutiva
                SubstituiVar("&(QTD_APNEIA_OBS)&", ev_ap_obs.qtd.ToString("0"));
                SubstituiVar("&(DUR_APNEIA_OBS)&", TimeSpan.FromSeconds(ev_ap_obs.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_APNEIA_OBS)&", TimeSpan.FromSeconds(ev_ap_obs.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_APNEIA_OBS)&", TimeSpan.FromSeconds(ev_ap_obs.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(QTD_HR_APNEIA_OBS)&", TimeSpan.FromSeconds(ev_ap_obs.indice).ToString(@"hh\:mm\:ss"));

                //Hipo
                SubstituiVar("&(QTD_HIPOPNEIA)&", ev_hipop.qtd.ToString("0"));
                SubstituiVar("&(DUR_HIPOPNEIA)&", TimeSpan.FromSeconds(ev_hipop.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_HIPOPNEIA)&", TimeSpan.FromSeconds(ev_hipop.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_HIPOPNEIA)&", TimeSpan.FromSeconds(ev_hipop.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(QTD_HR_HIPOPNEIA)&", TimeSpan.FromSeconds(ev_hipop.indice).ToString(@"hh\:mm\:ss"));

                //Despertar
                SubstituiVar("&(QTD_DESPERTAR)&", ev_desp.qtd.ToString("0"));
                SubstituiVar("&(DUR_DESPERTAR)&", TimeSpan.FromSeconds(ev_desp.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_DESPERTAR)&", TimeSpan.FromSeconds(ev_desp.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_DESPERTAR)&", TimeSpan.FromSeconds(ev_desp.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(QTD_HR_DESPERTAR)&", TimeSpan.FromSeconds(ev_desp.indice).ToString(@"hh\:mm\:ss"));

                //Dessaturacao
                SubstituiVar("&(QTD_DESSAT)&", ev_dessat.qtd.ToString("0"));
                SubstituiVar("&(DUR_DESSAT)&", TimeSpan.FromSeconds(ev_dessat.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_DESSAT)&", TimeSpan.FromSeconds(ev_dessat.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_DESSAT)&", TimeSpan.FromSeconds(ev_dessat.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(IND_DESSAT)&", TimeSpan.FromSeconds(ev_dessat.indice).ToString(@"hh\:mm\:ss"));

                //Ronco
                SubstituiVar("&(QTD_RONCO)&", ev_ronco.qtd.ToString("0"));
                SubstituiVar("&(DUR_RONCO)&", TimeSpan.FromSeconds(ev_ronco.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_RONCO)&", TimeSpan.FromSeconds(ev_ronco.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_RONCO)&", TimeSpan.FromSeconds(ev_ronco.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(IND_RONCO)&", TimeSpan.FromSeconds(ev_ronco.indice).ToString(@"hh\:mm\:ss"));

                //Bruxismo
                SubstituiVar("&(QTD_PLM)&", ev_plm.qtd.ToString("0"));
                SubstituiVar("&(DUR_PLM)&", TimeSpan.FromSeconds(ev_plm.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_PLM)&", TimeSpan.FromSeconds(ev_plm.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_PLM)&", TimeSpan.FromSeconds(ev_plm.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(IND_PLM)&", TimeSpan.FromSeconds(ev_plm.indice).ToString(@"hh\:mm\:ss"));

                //Perna
                SubstituiVar("&(QTD_MOVPERNA)&", ev_mov_perna.qtd.ToString("0"));
                SubstituiVar("&(DUR_MOVPERNA)&", TimeSpan.FromSeconds(ev_mov_perna.durtotal).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MED_MOVPERNA)&", TimeSpan.FromSeconds(ev_mov_perna.media).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(MAIOR_MOVPERNA)&", TimeSpan.FromSeconds(ev_mov_perna.maior).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(IND_MOVPERNA)&", TimeSpan.FromSeconds(ev_mov_perna.indice).ToString(@"hh\:mm\:ss"));


                if (GlobVar.tbl_ArqVideo.Rows.Count != 0)
                { SubstituiVar("&(simVideo)&", "X"); SubstituiVar("&(naoVideo)&", ""); }
                else { SubstituiVar("&(simVideo)&", ""); SubstituiVar("&(naoVideo)&", "X"); }

                // Funções auxiliares
                int GetQtd(string table)
                {
                    string query = $"SELECT COUNT(CodEvento) AS Qtd_Evento FROM {table} WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia}";
                    DataTable result = ExecutaSQL(cnn_dbExame, query);
                    return Convert.ToInt32(result.Rows[0]["Qtd_Evento"]);
                }

                int GetQtdJoin(string table1, string table2, string joinKey)
                {
                    string query = $"SELECT COUNT({table1}.{joinKey}.Seq) AS Qtd_Evento FROM {table1} " +
                                   $"INNER JOIN {table2} ON {table1}.{joinKey}.Seq = {table2}.{joinKey}.Seq " +
                                   $"WHERE {table1}.Pag_Ini >= {pag_noite} AND {table1}.Pag_Ini <= {pag_dia} " +
                                   $"AND {table2}.Pag_Ini >= {pag_noite} AND {table2}.Pag_Ini <= {pag_dia}";
                    DataTable result = ExecutaSQL(cnn_dbExame, query);
                    return Convert.ToInt32(result.Rows[0]["Qtd_Evento"]);
                }

                SubstituiVar("&(QTD_PLM_DESP)&", qtd_PLM_com_mdesp.ToString("0"));
                double TTR = GlobVar.tbl_ResumoExame.Rows[0]["TTR"] != DBNull.Value ? (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) / 3600) : 0;

                SubstituiVar("&(IND_PLM_DESP)&", (qtd_PLM_com_mdesp / (TTR / 3600)).ToString("0.0"));

                // DESPERTAR COM DESSAT
                sql = $"SELECT COUNT(Cons_Desp_Com_Dessat.CodEvento) AS Qtd_Evento FROM Cons_Desp_Com_Dessat WHERE Cons_Desp_Com_Dessat.Pag_Ini >= {pag_noite} AND Cons_Desp_Com_Dessat.Pag_Ini <= {pag_dia}";
                tbl = ExecutaSQL(cnn_dbExame, sql);
                barraLoading.Value += 5; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                qtd_Desp_com_dessat = 0;
                if (tbl.Rows.Count > 0)
                {
                    qtd_Desp_com_dessat = Convert.ToInt32(tbl.Rows[0]["Qtd_Evento"]);
                }

                // Verifica passagens
                if ((passagem == 3 && passagem == ultimapassagem) || passagem == 1)
                {
                    if (TextoComboContem("RESUMO_CPAP"))
                    {
                        s_resumo_CPAP();
                    }
                    else if (TextoComboContem("RESUMO_EPAP"))
                    {
                        s_resumo_BPAP();
                    }
                    s_ResumoHipoVentilacao();
                }

                s_dados_PTT(cnn_dbExame);


                string direct = Path.GetDirectoryName(GlobVar.textFile);
                string co2File = Path.Combine(direct, Path.GetFileNameWithoutExtension(GlobVar.textFile) + ".CO2");
                barraLoading.Value += 5; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                if (File.Exists(co2File))
                {
                    texto_co2 = AnaliseCO2.AnaliseAutomatica();

                    SubstituiVar("&(CO2_MEDIA)&", int.Parse(texto_co2.Substring(0, 6)).ToString());
                    SubstituiVar("&(CO2_MAIOR)&", int.Parse(texto_co2.Substring(6, 6)).ToString());
                    SubstituiVar("&(CO2_MENOR)&", int.Parse(texto_co2.Substring(12, 6)).ToString());

                    string unidade = f_var("Var56252").Substring(0, 3);
                    string acimaLimite = FormataTempoMin(int.Parse(texto_co2.Substring(18, 6))) + " " + unidade + ".";
                    SubstituiVar("&(CO2_ACIMALIMITE)&", acimaLimite);

                    SubstituiVar("&(CO2_LIMITE)&", int.Parse(texto_co2.Substring(24, 6)).ToString());

                    string acimaLimite2 = FormataTempoMin(int.Parse(texto_co2.Substring(30, 6))) + " " + unidade + ".";
                    SubstituiVar("&(CO2_ACIMALIMITE2)&", acimaLimite2);

                    SubstituiVar("&(CO2_LIMITE2)&", int.Parse(texto_co2.Substring(36, 6)).ToString());
                    SubstituiVar("&(CO2_LIMITEPERC)&", texto_co2.Substring(42, 5));
                    SubstituiVar("&(CO2_LIMITEPERC2)&", texto_co2.Substring(47, 5));
                }

                if (passagem != ultimapassagem)
                {
                    //Comentarios
                    foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in planExcel.Sheets)
                    {
                        if (sheet.Name.Equals("Comentarios", StringComparison.OrdinalIgnoreCase))
                        {
                            sheet.Select(); // ou sheet.Activate()
                            break;
                        }
                    }
                    if (texto.EndsWith("COMENTARIOS.DOC", StringComparison.OrdinalIgnoreCase))
                    {
                        IncluiComentarios();
                    }

                    planExcel.RefreshAll();

                    foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in planExcel.Sheets)
                    {
                        if (sheet.Name.Equals("Sheet1", StringComparison.OrdinalIgnoreCase))
                        {
                            sheet.Select(); // ou sheet.Activate()
                            break;
                        }
                    }

                    //' RESUMO DE EVENTOS

                    ResumoEventos(pag_noite, pag_dia, cnn_dbExame, cnn_dbConfig);

                    //' NOVOS RESUMOS DE EVENTOS RESPIRATORIOS

                    S_PreparaEventosRespiratorios(pag_noite, pag_dia, cnn_dbExame, cnn_dbConfig);

                    //'rotina de calculo da saturação por estágio
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    s_Calcula_Saturacao_Estagio(pag_noite, pag_dia, cnn_dbExame, cnn_dbConfig);

                    // 'substitui variaveis do paciente e dados do exame

                    s_dados_exame_paciente(cnn_dbExame, cnn_dbConfig);

                    // 'determina Nadir da SaO2 associado com apnéia

                    s_Nadir_Apneia();

                    // 'busca Apnéias centrais por estagio

                    s_busca_apneias_por_estagio(cnn_dbExame);

                    // Posicao

                    s_dados_Posicao();

                    // '"EVENTOS X POSICAO"
                    S_PreparaEventosPosicao(cnn_dbExame);

                    //'ESTAGIOS
                    s_dados_Estagios(cnn_dbExame);

                    //'"RESUMO_DESP"
                    s_dados_Despertares_Ronco_PLM(cnn_dbExame);

                    if (GlobVar.tbl_Paginas != null)
                    {
                        DateTime ini = Convert.ToDateTime(GlobVar.tbl_Paginas.Rows[0]["Horario"]);
                        SubstituiVar("&(Ini_Sono)", ini.ToString("HH:mm:ss"));

                        var ultimaPagina = GlobVar.tbl_Paginas.Rows[GlobVar.tbl_Paginas.Rows.Count - 1];
                        DateTime fimSono = Convert.ToDateTime(ultimaPagina["Horario"]);
                        DateTime fimExame = Convert.ToDateTime(GlobVar.tbl_ResumoExame.Rows[0]["FIM_EXAME"]);

                        SubstituiVar("&(Fim_Sono)", fimSono.ToString("HH:mm:ss"));
                        TimeSpan despertoFim = fimExame - fimSono;
                        SubstituiVar("&(Desperto_Fim)", despertoFim.ToString(@"hh\:mm\:ss"));

                        //'BOA NOITE E BOM DIA
                        DataRow rowNoite = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == pag_noite).FirstOrDefault();
                        DateTime hrNoite = Convert.ToDateTime(rowNoite["Horario"]);
                        hora_boa_noite = hrNoite.ToString("HH:mm:ss");

                        DataRow rowDia = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == pag_dia).FirstOrDefault();
                        DateTime hrDia = Convert.ToDateTime(rowDia["Horario"]);
                        hora_bom_dia = hrDia.ToString("HH:mm:ss");
                    }
                    SubstituiVar("&(BOA_NOITE)&", hora_boa_noite);
                    SubstituiVar("&(BOM_DIA)&", hora_bom_dia);
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    //IND_APHIP
                    if (GlobVar.tbl_ResumoExame == null)
                    {
                        SubstituiVar("&(IND_APHIP)&", "0.0");
                    }
                    else
                    {
                        SubstituiVar("&(IND_APHIP)&", ((ev_ap.qtd + ev_hipop.qtd) / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) / 3600)).ToString("F1"));
                    }

                    // IND_AP
                    if (GlobVar.tbl_ResumoExame == null || Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                    {
                        SubstituiVar("&(IND_AP)&", "0.0");
                    }
                    else
                    {
                        SubstituiVar("&(IND_AP)&", ev_ap.indice.ToString("F1"));
                    }

                    // IND_HIP
                    if (GlobVar.tbl_ResumoExame == null || Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                    {
                        SubstituiVar("&(IND_HIP)&", "0.0");
                    }
                    else
                    {
                        SubstituiVar("&(IND_HIP)&", ev_hipop.indice.ToString("F1"));
                    }

                    // IND_APHIP_REM
                    if (GlobVar.tbl_ResumoExame == null || Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) == 0)
                    {
                        SubstituiVar("&(IND_APHIP_REM)&", "0.0");
                    }
                    else
                    {
                        double est5Horas = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600;
                        double indiceAPHIP_REM = (ev_ap.qtd_rem + ev_hipop.qtd_rem) / est5Horas;
                        SubstituiVar("&(IND_APHIP_REM)&", indiceAPHIP_REM.ToString("F1"));
                    }

                    // IND_AP_REM
                    if (GlobVar.tbl_ResumoExame == null || Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) == 0)
                    {
                        SubstituiVar("&(IND_AP_REM)&", "0.0");
                    }
                    else
                    {
                        double est5Horas = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600;
                        double indiceAP_REM = ev_ap.qtd_rem / est5Horas;
                        SubstituiVar("&(IND_AP_REM)&", indiceAP_REM.ToString("F1"));
                    }

                    // pgb_LaudoDoc.Value = 60; // Deixe isso onde você precisar controlar a barra de progresso.

                    // IND_HIP_REM
                    if (GlobVar.tbl_ResumoExame == null || Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) == 0)
                    {
                        SubstituiVar("&(IND_HIP_REM)&", "0.0");
                    }
                    else
                    {
                        double est5Horas = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600;
                        double indiceHIP_REM = ev_hipop.qtd_rem / est5Horas;
                        SubstituiVar("&(IND_HIP_REM)&", indiceHIP_REM.ToString("F1"));
                    }

                    //"RESUMO_RESP"
                    s_dados_Respiratorios(pag_noite, pag_dia, cnn_dbExame);
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    //'"IND_REM_HIPOPNEIA_OBS"
                    if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) == 0)
                    {
                        SubstituiVar("&(IND_REM_HIPOPNEIA_OBS)&", "0.0");

                    }
                    else
                    {
                        SubstituiVar("&(IND_REM_HIPOPNEIA_OBS)&", (ev_hipop_obs.qtd_rem / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600)).ToString("F1"));
                        double est5Segundos = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]);
                        double indice = est5Segundos == 0 ? 0.0 : ev_hipop_obs.qtd_rem / (est5Segundos / 3600);
                        texto = texto.Substring(0, pos1 - 1) + indice.ToString("0.0") + texto.Substring(0 + 2);

                    }

                    //'"SAT_BASAL"
                    s_dados_Saturacao(cnn_dbExame);

                    //' FREQ CARD
                    //'"FC_MAIOR"
                    SubstituiVar("&(FC_MAIOR)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_MAIOR"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_MAIOR"])).ToString("F0", new CultureInfo("pt-BR")));

                    SubstituiVar("&(FC_REM_MEDIA)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_REM_MEDIA"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_REM_MEDIA"])).ToString("F0", new CultureInfo("pt-BR")));
                    SubstituiVar("&(FC_REM_MAIOR)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_REM_MAIOR"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_REM_MAIOR"])).ToString("F0", new CultureInfo("pt-BR")));
                    SubstituiVar("&(FC_NREM_MEDIA)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_NREM_MEDIA"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_NREM_MEDIA"])).ToString("F0", new CultureInfo("pt-BR")));
                    SubstituiVar("&(FC_NREM_MAIOR)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_NREM_MAIOR"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_NREM_MAIOR"])).ToString("F0", new CultureInfo("pt-BR")));
                    SubstituiVar("&(FC_VIGILIA_MEDIA)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_VIGILIA_MEDIA"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_VIGILIA_MEDIA"])).ToString("F0", new CultureInfo("pt-BR")));
                    SubstituiVar("&(FC_VIGILIA_MAIOR)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_VIGILIA_MAIOR"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_VIGILIA_MAIOR"])).ToString("F0", new CultureInfo("pt-BR")));

                    //'"FC_MENOR"
                    SubstituiVar("&(FC_MENOR)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_MENOR"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_MENOR"])).ToString("F0", new CultureInfo("pt-BR")));

                    //'"FC_MEDIA"
                    SubstituiVar("&(FC_MEDIA)&", (GlobVar.tbl_ResumoExame.Rows[0]["FC_MEDIA"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_MEDIA"])).ToString("F0", new CultureInfo("pt-BR")));

                    SubstituiVar("&(CPAP_MIN)&", (CPAP_Min.ToString("F0", new CultureInfo("pt-BR"))));
                    SubstituiVar("&(CPAP_MAX)&", (CPAP_Max.ToString("F0", new CultureInfo("pt-BR"))));
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    s_dados_Multipla_Latencia();
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                }
                else
                {
                    //'substitui variaveis do paciente e dados do exame
                    s_dados_exame_paciente(cnn_dbExame, cnn_dbConfig);
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual


                    //'ESTAGIOS
                    s_dados_Estagios(cnn_dbExame);


                    //'"RESUMO_DESP"
                    s_dados_Despertares_Ronco_PLM(cnn_dbExame);
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual


                    //'"RESUMO_RESP"
                    s_dados_Respiratorios(pag_noite, pag_dia, cnn_dbExame);

                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    //'"EVENTOS X POSICAO"
                    S_PreparaEventosPosicao(cnn_dbExame);


                    //'"SAT_BASAL"
                    s_dados_Saturacao(cnn_dbExame);
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    //'SubstituiVar("&(IND_APHIP_INT)&", "0.0")
                    if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0){
                    SubstituiVar("&(IND_APHIP_INT)&", "0.0");

                    }//If tbl_ResumoExame!TTS = 0 Then
                    else
                    {
                    SubstituiVar("&(IND_APHIP_INT)&", TimeSpan.FromSeconds((ev_ap.qtd + ev_hipop.qtd) / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) / 3600)).ToString(@"hh\:mm\:ss"));
                    }
                    //'SubstituiVar("&(IND_AP_INT)&", "0.0")
                    SubstituiVar("&(IND_AP_INT)&", TimeSpan.FromSeconds(ev_ap.indice).ToString(@"hh\:mm\:ss"));
                    //'SubstituiVar("&(IND_HIP_INT)&", "0.0")
                    SubstituiVar("&(IND_HIP_INT)&", TimeSpan.FromSeconds(ev_hipop.indice).ToString(@"hh\:mm\:ss"));
                    //'SubstituiVar("&(IND_APHIP_REM_INT)&", "0.0")
                    if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) == 0)
                    {
                    SubstituiVar("&(IND_APHIP_REM_INT)&", "0.0");
                    SubstituiVar("&(IND_AP_REM_INT)&", "0.0");
                    SubstituiVar("&(IND_HIP_REM_INT)&", "0.0");

                    } //If tbl_ResumoExame!Est_5 = 0 Then
                    else
                    {
                    SubstituiVar("&(IND_APHIP_REM_INT)&", TimeSpan.FromSeconds((ev_ap.qtd_rem + ev_hipop.qtd_rem) / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600)).ToString(@"hh\:mm\:ss"));
                    SubstituiVar("&(IND_AP_REM_INT)&", TimeSpan.FromSeconds(ev_ap.qtd_rem / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600)).ToString(@"hh\:mm\:ss"));
                    SubstituiVar("&(IND_HIP_REM_INT)&", TimeSpan.FromSeconds(ev_hipop.qtd_rem / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600)).ToString(@"hh\:mm\:ss"));

                    }
                    barraLoading.Value += 5; // ou += 10, etc.
                    Application.DoEvents();  // Atualiza visual

                    //'SubstituiVar("&(IND_AP_REM_INT)&", "0.0");
                    //'SubstituiVar("&(IND_HIP_REM_INT)&", "0.0");
                    SubstituiVar("&(FC_MAIOR_INT)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_MAIOR"])).ToString(@"hh\:mm\:ss"));
                    SubstituiVar("&(FC_MENOR_INT)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_MENOR"])).ToString(@"hh\:mm\:ss"));
                    SubstituiVar("&(FC_MEDIA_INT)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["FC_MEDIA"])).ToString(@"hh\:mm\:ss"));

                }
                barraLoading.Value += 5; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                // Move o cursor para o início
                object unit = Microsoft.Office.Interop.Word.WdUnits.wdStory;
                object missing = System.Type.Missing;
                wordApp.Selection.HomeKey(ref unit, ref missing);

                string loc = Path.GetDirectoryName(GlobVar.bDataFile);
                string arq_exame = Path.GetFileNameWithoutExtension(GlobVar.bDataFile);
                string nomeArquivoFinal = "";

                // Verifica se é hora de salvar como definitivo
                if (passagem == 1 || passagem == ultimapassagem)
                {
                    if (texto.ToUpper().EndsWith("COMENTARIOS.DOC"))
                        nomeArquivoFinal = loc + "\\" + arq_exame + " Comentarios.doc";
                    else
                        nomeArquivoFinal = Path.Combine(loc + "\\" + arq_exame + " Laudo.doc");

                    // Salva como novo arquivo
                    doc.SaveAs2(nomeArquivoFinal);

                    // Torna o Word visível com o novo arquivo
                    //wordApp.Visible = true;
                    //oficial.Activate();
                }

                // Fecha e libera os objetos corretamente
                doc.Close(false);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
                doc = null;

                barraLoading.Value += 5; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                // Fecha Excel
                planExcel.Close(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(planExcel);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(ObjExcel);
                planExcel = null;
                ObjExcel = null;

                // Fecha o Word se quiser liberar totalmente
                // (cuidado: se tiver outros documentos abertos, ele fechará tudo)
                //wordApp.Quit(false);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
                //wordApp = null;
                barraLoading.Value += 5; // ou += 10, etc.
                Application.DoEvents();  // Atualiza visual

                // Remove arquivos temporários
                try
                {
                    File.Delete(Path.Combine(g_dir_laudos, nome_arq_temp + ".doc"));
                    File.Delete(Path.Combine(g_dir_laudos, nome_arq_temp + ".xls"));

                }
                catch (Exception ex)
                {
                    // Log ou ignore, como preferir
                }
                // Reabre o documento oficial e ativa o Word
                if (!string.IsNullOrEmpty(nomeArquivoFinal) && File.Exists(nomeArquivoFinal))
                {
                    var docOficial = wordApp.Documents.Open(nomeArquivoFinal);
                    wordApp.Visible = true;
                    docOficial.Activate();
                }

                // Carrega informações da clínica
                sql = "SELECT * FROM tbl_DadosClinica";
                rs = ExecutaSQL(cnn_dbConfig, sql);
                if (rs != null && rs.Rows.Count > 0)
                {
                    var idClinica = rs.Rows[0]["ID_Clinica"].ToString();
                    g_variaveislaudo = "&(ID_CLINICA)&" + idClinica + ";" + Environment.NewLine + g_variaveislaudo;
                }

            }
        }

        public static void s_dados_Multipla_Latencia()
        {
            for (int i = 1; i <= 5; i++)
            {
                int index = i - 1;
                string inicio = g_naps[index].Inicio == TimeSpan.Zero ? "-" : g_naps[index].Inicio.ToString(@"hh\:mm\:ss");
                string fim = g_naps[index].fim == TimeSpan.Zero ? "-" : g_naps[index].fim.ToString(@"hh\:mm\:ss");

                string latSono = g_naps[index].Lat_Sono == -1 ? "-" : g_naps[index].Lat_Sono.ToString();
                string latEst1 = g_naps[index].Lat_Est1 == -1 ? "-" : g_naps[index].Lat_Est1.ToString();
                string latEst2 = g_naps[index].Lat_Est2 == -1 ? "-" : g_naps[index].Lat_Est2.ToString();
                string latEst3 = g_naps[index].Lat_Est3 == -1 ? "-" : g_naps[index].Lat_Est3.ToString();
                string latEst4 = g_naps[index].Lat_Est4 == -1 ? "-" : g_naps[index].Lat_Est4.ToString();
                string latEst5BoaNoite = g_naps[index].Lat_Est5_BoaNoite == -1 ? "-" : g_naps[index].Lat_Est5_BoaNoite.ToString();
                string latEst5SleepOnset = g_naps[index].Lat_Est5_SleepOnset == -1 ? "-" : g_naps[index].Lat_Est5_SleepOnset.ToString();

                SubstituiVar($"&(INICIO_P{i})&", inicio);
                SubstituiVar($"&(FIM_P{i})&", fim);
                SubstituiVar($"&(LAT_SONO_P{i})&", latSono);
                SubstituiVar($"&(LAT_EST1_P{i})&", latEst1);
                SubstituiVar($"&(LAT_EST2_P{i})&", latEst2);
                SubstituiVar($"&(LAT_EST3_P{i})&", latEst3);
                SubstituiVar($"&(LAT_EST4_P{i})&", latEst4);
                SubstituiVar($"&(LAT_EST5_P{i})&", latEst5BoaNoite);
                SubstituiVar($"&(LAT_EST5_OS_P{i})&", latEst5SleepOnset);

                // Substituições com conversão para minutos (LAT_SONO_MIN_Px)
                string latSonoMin = g_naps[index].Lat_Sono == -1
                    ? TimeSpan.FromSeconds(g_naps[index].TTR).ToString(@"hh\:mm\:ss")
                    : TimeSpan.FromSeconds(g_naps[index].Lat_Sono).ToString(@"hh\:mm\:ss");
                SubstituiVar($"&(LAT_SONO_MIN_P{i})&", latSonoMin);

                // Substituições com conversão para minutos (LAT_EST1_MIN_Px)
                string latEst1Min = g_naps[index].Lat_Est1 == -1
                    ? "-"
                    : TimeSpan.FromSeconds(g_naps[index].Lat_Est1).ToString(@"hh\:mm\:ss");
                SubstituiVar($"&(LAT_EST1_MIN_P{i})&", latEst1Min);


            }

            int latMedSono = 0;
            int latMedSonoQtd = 0;

            for (int i = 1; i <= 5; i++)
            {
                int index = i - 1;
                int latEst1 = g_naps[index].Lat_Est1;
                if (latEst1 != -1)
                {
                    latMedSono += latEst1;
                    latMedSonoQtd++;
                }
            }

            if (latMedSonoQtd > 0)
            {
                SubstituiVar("&(LAT_MED_SONO)&", TimeSpan.FromSeconds((double)latMedSono / latMedSonoQtd).ToString(@"hh\:mm\:ss"));
            }
            else
            {
                SubstituiVar("&(LAT_MED_SONO)&", "-");
            }

            // Assume: SubstituiVar(string variavel, string valor)
            // Assume: FormataTempo(double tempo, bool comHoras)

            for (int estagio = 0; estagio <= 3; estagio++)
            {
                double soma = 0;
                for (int i = 1; i <= 5; i++)
                {
                    int index = i - 1;
                    switch (estagio)
                    {
                        case 0: soma += g_naps[index].TempoEst0; break;
                        case 1: soma += g_naps[index].TempoEst1; break;
                        case 2: soma += g_naps[index].TempoEst2; break;
                        case 3: soma += g_naps[index].TempoEst3; break;
                    }
                }
                SubstituiVar($"&(DUR_EST{estagio})&", TimeSpan.FromSeconds(soma).ToString(@"hh\:mm\:ss"));
            }

            // Estágio REM (equivale a EST5)
            double somaREM = 0;
            for (int i = 0; i < 5; i++)
            {
                somaREM += g_naps[i].TempodeREM;
            }
            SubstituiVar("&(DUR_EST5)&", TimeSpan.FromSeconds(somaREM).ToString(@"hh\:mm\:ss"));

            // Latências por estágio (EST2 a EST5) e páginas (P1 a P5)
            for (int est = 2; est <= 5; est++)
            {
                for (int pag = 1; pag <= 5; pag++)
                {
                    int index = pag - 1;
                    string variavel = $"&(LAT_EST{est}_MIN_P{pag})&";
                    double valor = est switch
                    {
                        2 => g_naps[index].Lat_Est2,
                        3 => g_naps[index].Lat_Est3,
                        4 => g_naps[index].Lat_Est4,
                        5 => g_naps[index].Lat_Est5_BoaNoite,
                        _ => -1
                    };

                    SubstituiVar(variavel, valor == -1 ? "-" : TimeSpan.FromSeconds(valor).ToString(@"hh\:mm\:ss"));
                }
            }
            string[] remVars = { "LAT_EST5_MIN_P1", "LAT_EST5_MIN_P2", "LAT_EST5_MIN_P3", "LAT_EST5_MIN_P4", "LAT_EST5_MIN_P5" };
            double somaBoaNoite = 0;

            foreach (string var in remVars)
            {
                int index = int.Parse(var[^1].ToString()) - 1;
                somaBoaNoite += g_naps[index].Lat_Est5_BoaNoite;
            }

            double mediaBoaNoite = somaBoaNoite / remVars.Length;
            SubstituiVar("&(LAT_REM_MIN)&", TimeSpan.FromSeconds(mediaBoaNoite).ToString(@"hh\:mm\:ss"));

            for (int i = 1; i <= 5; i++)
            {
                int index = i - 1;
                string varName = $"&(LAT_EST5_OS_MIN_P{i})&";
                double onset = g_naps[index].Lat_Est5_SleepOnset;

                string valor = (onset == -1)
                    ? "-"
                    : TimeSpan.FromSeconds(onset).ToString(@"hh\:mm\:ss");

                SubstituiVar(varName, valor);
            }

            for (int i = 1; i <= 5; i++)
            {
                int index = i - 1;

                // TTS_MIN_Px
                string varTTS = $"TTS_MIN_P{i}";
                if (g_naps[index].TTS == 0)
                    SubstituiVar($"&({varTTS})&", "-");
                else
                    SubstituiVar($"&({varTTS})&", TimeSpan.FromSeconds(g_naps[index].TTS).ToString(@"hh\:mm\:ss"));

                // TTR_MIN_Px
                string varTTR = $"TTR_MIN_P{i}";
                if (g_naps[index].TTR == 0)
                    SubstituiVar($"&({varTTR})&", "-");
                else
                    SubstituiVar($"&({varTTR})&", TimeSpan.FromSeconds(g_naps[index].TTR).ToString(@"hh\:mm\:ss"));

                // EFIC_SONO_Px
                string varEFIC = $"EFIC_SONO_P{i}";
                if (g_naps[index].TTR == 0)
                    SubstituiVar($"&({varEFIC})&", "0.0");
                else
                {
                    double efic = g_naps[index].TTS * 100.0 / g_naps[index].TTR;
                    SubstituiVar($"&({varEFIC})&", efic.ToString("0.0", CultureInfo.InvariantCulture));
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                int index = i - 1;

                SubstituiVar($"&(HorarioNREM_P{i})&", g_naps[index].HorarioNREM.ToString(@"hh\:mm\:ss"));
                SubstituiVar($"&(HorarioREM_P{i})&", g_naps[index].HorarioREM.ToString(@"hh\:mm\:ss"));

                string tempoEst0;
                if (g_naps[index].TempoEst0 == 0)
                    tempoEst0 = "-";
                else
                    tempoEst0 = TimeSpan.FromSeconds(g_naps[index].TempoEst0).ToString(@"hh\:mm\:ss");

                SubstituiVar($"&(TEMPO_EST0_MIN_P{i})&", tempoEst0);
            }

            string[] estagios = { "TEMPO_EST0_MIN", "TEMPO_EST1_MIN", "TEMPO_EST2_MIN", "TEMPO_EST3_MIN" };
            for (int est = 0; est <= 3; est++)
            {
                for (int p = 1; p <= 5; p++)
                {
                    string varName = $"{estagios[est]}_P{p}";
                    int index = p - 1;

                    double tempo = est switch
                    {
                        0 => g_naps[index].TempoEst0,
                        1 => g_naps[index].TempoEst1,
                        2 => g_naps[index].TempoEst2,
                        3 => g_naps[index].TempoEst3,
                        _ => 0
                    };

                    if (tempo == 0)
                        SubstituiVar($"&({varName})&", "-");
                    else
                        SubstituiVar($"&({varName})&", TimeSpan.FromSeconds(tempo).ToString(@"hh\:mm\:ss"));
                }
            }

            // TEMPO_REM_MIN_P1 a P5
            for (int p = 1; p <= 5; p++)
            {
                int index = p - 1;
                string varName = $"&(TEMPO_REM_MIN_P{p})&";
                if (g_naps[index].TempodeREM == 0)
                {
                    SubstituiVar(varName, "-");
                }
                else
                {
                    SubstituiVar(varName, TimeSpan.FromSeconds(g_naps[index].TempodeREM).ToString(@"hh\:mm\:ss"));
                }
            }

            // EFIC_SONO_ML
            double ttr_ml = 0;
            double tts_ml = 0;
            int qtd_ttr_ml = 0;
            for (int i = 0; i < 5; i++)
            {
                if (g_naps[i].TTR != 0)
                {
                    qtd_ttr_ml++;
                    ttr_ml += g_naps[i].TTR;
                    tts_ml += g_naps[i].TTS;
                }
            }
            if (ttr_ml == 0)
            {
                SubstituiVar("&(EFIC_SONO_ML)&", "0.0");
            }
            else
            {
                SubstituiVar("&(EFIC_SONO_ML)&", (tts_ml * 100 / ttr_ml).ToString("0.0") + " %");
            }

            // MEDIA_LAT5_ML
            double lat_ml = 0;
            for (int i = 0; i < 5; i++)
            {
                if (g_naps[i].Lat_Sono > -1)
                {
                    if (g_naps[i].Lat_Sono > 0)
                        lat_ml += g_naps[i].Lat_Sono;
                }
                else
                {
                    lat_ml += g_naps[i].TTR;
                }
            }
            SubstituiVar("&(MEDIA_LAT5_ML)&", TimeSpan.FromSeconds(lat_ml / 5).ToString(@"hh\:mm\:ss") + " mm:ss");
            SubstituiVar("&(MEDIA_LAT5_ML_)&", TimeSpan.FromSeconds(lat_ml / 5).ToString(@"hh\:mm\:ss"));

            // MEDIA_LAT4_ML
            lat_ml = 0;
            for (int i = 0; i < 4; i++)
            {
                if (g_naps[i].Lat_Sono > -1)
                {
                    if (g_naps[i].Lat_Sono > 0)
                        lat_ml += g_naps[i].Lat_Sono;
                }
                else
                {
                    lat_ml += g_naps[i].TTR;
                }
            }
            SubstituiVar("&(MEDIA_LAT4_ML)&", TimeSpan.FromSeconds(lat_ml / 4).ToString(@"hh\:mm\:ss") + " mm:ss");

            // MEDIA_LAT3_ML
            lat_ml = 0;
            for (int i = 0; i < 3; i++)
            {
                if (g_naps[i].Lat_Sono > -1)
                {
                    if (g_naps[i].Lat_Sono > 0)
                        lat_ml += g_naps[i].Lat_Sono;
                }
                else
                {
                    lat_ml += g_naps[i].TTR;
                }
            }
            SubstituiVar("&(MEDIA_LAT3_ML)&", TimeSpan.FromSeconds(lat_ml / 3).ToString(@"hh\:mm\:ss") + " mm:ss");

            // MEDIA_LAT2_ML
            lat_ml = 0;
            for (int i = 0; i < 2; i++)
            {
                if (g_naps[i].Lat_Sono > -1)
                {
                    if (g_naps[i].Lat_Sono > 0)
                        lat_ml += g_naps[i].Lat_Sono;
                }
                else
                {
                    lat_ml += g_naps[i].TTR;
                }
            }
            SubstituiVar("&(MEDIA_LAT2_ML)&", TimeSpan.FromSeconds(lat_ml / 2).ToString(@"hh\:mm\:ss") + " mm:ss");

            // QTD_REM_ML
            int qtd_rem_ml = 0;
            for (int i = 0; i < 5; i++)
            {
                if (g_naps[i].Lat_Est5_BoaNoite > -1)
                {
                    qtd_rem_ml++;
                }
            }
            SubstituiVar("&(QTD_REM_ML)&", qtd_rem_ml.ToString("0"));

            // MEDIA_LAT_REM_ML
            lat_ml = 0;
            int qtd_lat_ml = 0;
            for (int i = 0; i < 5; i++)
            {
                if (g_naps[i].Lat_Est5_BoaNoite > -1)
                {
                    lat_ml += g_naps[i].Lat_Est5_BoaNoite;
                    qtd_lat_ml++;
                }
            }
            if (qtd_lat_ml == 0)
            {
                SubstituiVar("&(MEDIA_LAT_REM_ML)&", "-");
            }
            else
            {
                SubstituiVar("&(MEDIA_LAT_REM_ML)&", TimeSpan.FromSeconds(lat_ml / qtd_lat_ml).ToString(@"hh\:mm\:ss") + " mm:ss");
            }

            // MEDIA_LAT_REM_OS_ML
            lat_ml = 0;
            qtd_lat_ml = 0;
            for (int i = 0; i < 5; i++)
            {
                if (g_naps[i].Lat_Est5_BoaNoite > -1)
                {
                    lat_ml += g_naps[i].Lat_Est5_SleepOnset;
                    qtd_lat_ml++;
                }
            }
            if (qtd_lat_ml == 0)
            {
                SubstituiVar("&(MEDIA_LAT_REM_OS_ML)&", "-");
            }
            else
            {
                SubstituiVar("&(MEDIA_LAT_REM_OS_ML)&", TimeSpan.FromSeconds(lat_ml / qtd_lat_ml).ToString(@"hh\:mm\:ss") + " mm:ss");
            }



        }
        public static void s_dados_Saturacao(OleDbConnection cnn_dbExame)
        {
            try
            {
                int qtd_rem = 0;
                int qtd_nrem = 0;
                int qtd = GetQtdEvento(cnn_dbExame, 17);

                if (passagem == ultimapassagem)
                {
                    if (Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["SaO2_100"]) == 0)
                    {
                        SubstituiVar("&(SAT_MEDIA_INT)&", "0");
                        SubstituiVar("&(MAIOR_SAT_INT)&", "0");
                        SubstituiVar("&(MENOR_SAT_INT)&", "0");
                    }
                    else
                    {
                        double sao2 = Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["SaO2_100"]);
                        SubstituiVar("&(SAT_MEDIA_INT)&", ((double)GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Media"] * 100 / sao2).ToString("0"));
                        SubstituiVar("&(MAIOR_SAT_INT)&", ((double)GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Maior"] * 100 / sao2).ToString("0"));
                        SubstituiVar("&(MENOR_SAT_INT)&", ((double)GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Menor"] * 100 / sao2).ToString("0"));
                    }

                    SubstituiVar("&(SAT90_MIN_INT)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"])));
                    SubstituiVar("&(SAT_BASAL_INT)&", Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["SatBasal"]).ToString("0"));

                    double ttr = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]);
                    if (ttr == 0)
                    {
                        SubstituiVar("&(SAT90_PORC_INT)&", "0.0");
                        SubstituiVar("&(SAT80_PORC_INT)&", "0.0");
                    }
                    else
                    {
                        SubstituiVar("&(SAT90_PORC_INT)&", (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"]) * 100 / ttr).ToString("0.0"));
                        SubstituiVar("&(SAT80_PORC_INT)&", (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"]) * 100 / ttr).ToString("0.0"));
                        SubstituiVar("&(SAT70_PORC_INT)&", (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"]) * 100 / ttr).ToString("0.0"));
                    }

                    SubstituiVar("&(SAT80_MIN_INT)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"])));

                    if (qtd == 0)
                        SubstituiVar("&(DESSAT_INT)&", f_var("Var58025"));
                    else if (qtd == 1)
                        SubstituiVar("&(DESSAT_INT)&", f_var("Var58039"));
                    else
                        SubstituiVar("&(DESSAT_INT)&", f_var("Var58032") + " " + qtd + " " + f_var("Var56158").ToLower());
                }
                else
                {
                    SubstituiVar("&(SAT_BASAL)&", Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["SatBasal"]).ToString("0"));

                    double sao2 = Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["SaO2_100"]);
                    double ttr = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]);
                    double tts = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);

                    SubstituiVar("&(SAT_MEDIA)&", sao2 == 0 ? "0" : ((GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Media"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Media"])) * 100 / sao2).ToString("0"));
                    SubstituiVar("&(MAIOR_SAT)&", sao2 == 0 ? "0" : ((GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Maior"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Maior"])) * 100 / sao2).ToString("0"));
                    SubstituiVar("&(MENOR_SAT)&", sao2 == 0 ? "0" : ((GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Menor"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Menor"])) * 100 / sao2).ToString("0"));
                    SubstituiVar("&(CARGA_HIPOXICA)&", $"{GlobVar.tbl_DadosExame.Rows[0]["CargaHipoxica"]:0.00}");

                    SubstituiVar("&(SAT90)&", ttr == 0 ? "0.0" :
                        TimeSpan.FromSeconds(Convert.ToInt32( GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"]) ).ToString(@"hh\:mm\:ss") +
                        $" ({((GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"])) * 100 / ttr):0.0} %)");

                    SubstituiVar("&(SAT90_MIN)&", FormataTempoMin(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"]) ));
                    SubstituiVar("&(SAT90_PORC)&", ttr == 0 ? "0.0" : (GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo90"]) * 100 / ttr).ToString("0.0"));

                    SubstituiVar("&(SAT80)&", ttr == 0 ? "0.0" :
                        TimeSpan.FromSeconds( Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"]) ).ToString(@"hh\:mm\:ss") +
                        $" ({(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"]) * 100 / ttr):0.0} %)");

                    SubstituiVar("&(SAT80_MIN)&", FormataTempoMin(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"]) ));
                    SubstituiVar("&(SAT80_PORC)&", ttr == 0 ? "0.0" : (GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo80"] ) * 100 / ttr).ToString("0.0"));

                    SubstituiVar("&(SAT70)&", ttr == 0 ? "0.0" :
                        TimeSpan.FromSeconds(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"]) ).ToString(@"hh\:mm\:ss") +
                        $" ({(Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"]) * 100 / ttr):0.0} %)");

                    SubstituiVar("&(SAT70_MIN)&", FormataTempoMin(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"] == DBNull.Value ? 0 : Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"]) ));
                    SubstituiVar("&(SAT70_PORC)&", ttr == 0 ? "0.0" : (GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Dessat_Abaixo70"]) * 100 / ttr).ToString("0.0"));

                    DataTable dtqt = ExecutaSQL(cnn_dbExame, "SELECT * FROM Cons_EventosComEstag WHERE Estagio = 5");//GlobVar.eventos.AsEnumerable().Where(rw => rw.Field<int>("Estagio") == 5).CopyToDataTable();
                    var query = dtqt.AsEnumerable().Where(rw => rw.Field<int>("CodEvento") == 15);
                    if (query.Any())
                    {
                        var qtDesTem = query.CopyToDataTable();
                        qtd_rem = qtDesTem.Rows.Count;
                    }
                    else
                    {
                        qtd_rem = 0;
                    }
                    qtd_nrem = qtd - qtd_rem;

                    if (qtd == 0)
                        SubstituiVar("&(DESSAT)&", f_var("Var58025"));
                    else if (qtd == 1)
                        SubstituiVar("&(DESSAT)&", f_var("Var58039"));
                    else
                        SubstituiVar("&(DESSAT)&", f_var("Var58032") + " " + qtd + " " + f_var("Var56158").ToLower());

                    SubstituiVar("&(QTD_DESSAT)&", qtd.ToString());
                    SubstituiVar("&(QTD_DESSAT_REM)&", qtd_rem.ToString());
                    SubstituiVar("&(QTD_DESSAT_NREM)&", qtd_nrem.ToString());

                    SubstituiVar("&(IND_DESSAT)&", tts == 0 ? "0.0" : (qtd / (tts / 3600)).ToString("0.0"));

                    double est5 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]);
                    double est_nrem = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]) +
                                      Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]) +
                                      Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]);

                    double ind_rem = est5 == 0 ? 0 : qtd_rem / (est5 / 3600);
                    double ind_nrem = est_nrem == 0 ? 0 : qtd_nrem / (est_nrem / 3600);

                    SubstituiVar("&(IND_DESSAT_REM)&", ind_rem.ToString("0.0"));
                    SubstituiVar("&(IND_DESSAT_NREM)&", ind_nrem.ToString("0.0"));
                }
            }
            catch (Exception ex)
            {
                // Trate o erro de forma adequada, se necessário
            }
        }

        public static void s_dados_Respiratorios(int pag_noite, int pag_dia, OleDbConnection cnn_dbExame)
        {
            int maior_ev_num = 0;
            string maior_ev_string = "";
            int pos_c = 0, pos_b = 0, pos_d = 0, pos_e = 0;
            string sql = "";
            DataTable rs = new DataTable();

            sql = "SELECT * FROM tbl_PosEstagio";
            rs = ExecutaSQL(cnn_dbExame, sql);
            
            if(rs != null)
            {
                foreach(DataRow rw in rs.Rows)
                {
                    if (Convert.ToInt32(rw["Estagio"]) > 0)
                    {
                        pos_c += Convert.ToInt32(rw["Cima"]);
                        pos_b += Convert.ToInt32(rw["Baixo"]);
                        pos_d += Convert.ToInt32(rw["Direita"]);
                        pos_e += Convert.ToInt32(rw["Esquerda"]);
                    }
                }
            }

            texto2 = "";

            if (ev_ap.qtd + ev_hipop.qtd == 0)
            {
                texto2 = f_var("Var58026");
            }
            else if (ev_ap.qtd + ev_hipop.qtd == 1)
            {
                if (ev_ap_cen.qtd == 1)
                {
                    texto2 = f_var("Var58036");
                }
                else if (ev_ap_obs.qtd + ev_hipop.qtd == 1)
                {
                    texto2 = f_var("Var58038");
                }
                else
                {
                    texto2 = f_var("Var58037");
                }
            }
            else
            {
                texto2 = f_var("Var58032") + " " + (ev_ap.qtd + ev_hipop.qtd).ToString() + " " + f_var("Var58012").ToLower() + ", " + f_var("Var58011") + " ";

                if (ev_ap_cen.qtd > 1)
                {
                    texto2 += ev_ap_cen.qtd.ToString() + " " + f_var("Var58003") + ", ";
                }
                else
                {
                    texto2 += ev_ap_cen.qtd.ToString() + " " + f_var("Var58004") + ", ";
                }

                if (ev_ap_obs.qtd + ev_ap_cen.qtd + ev_hipop.qtd > 1)
                {
                    texto2 += (ev_ap_obs.qtd + ev_hipop.qtd).ToString() + " " + f_var("Var58031") + " e ";
                }
                else
                {
                    texto2 += (ev_ap_obs.qtd + ev_hipop.qtd).ToString() + " " + f_var("Var58030") + " e ";
                }

                if (ev_ap_mis.qtd > 1)
                {
                    texto2 += ev_ap_mis.qtd.ToString() + " " + f_var("Var58020");
                }
                else
                {
                    texto2 += ev_ap_mis.qtd.ToString() + " " + f_var("Var58019");
                }
            }

            string texto2RERA = "";

            int totalEventosRERA = ev_ap.qtd + ev_hipop.qtd + ev_rera.qtd;

            if (totalEventosRERA == 0)
            {
                texto2RERA = f_var("Var58026");
            }
            else if (totalEventosRERA == 1)
            {
                if (ev_ap_cen.qtd == 1)
                {
                    texto2RERA = f_var("Var58036");
                }
                else if (ev_ap_obs.qtd + ev_hipop.qtd + ev_rera.qtd == 1)
                {
                    texto2RERA = f_var("Var58038");
                }
                else
                {
                    texto2RERA = f_var("Var58037");
                }
            }
            else
            {
                texto2RERA = f_var("Var58032") + " " + totalEventosRERA.ToString() + " " + f_var("Var58012").ToLower() + ", " + f_var("Var58011") + " ";

                if (ev_ap_cen.qtd > 1)
                {
                    texto2RERA += ev_ap_cen.qtd.ToString() + " " + f_var("Var58003") + " e ";
                }
                else
                {
                    texto2RERA += ev_ap_cen.qtd.ToString() + " " + f_var("Var58004") + " e ";
                }

                int totalObsMisHipRera = ev_ap_obs.qtd + ev_ap_mis.qtd + ev_hipop.qtd + ev_rera.qtd;

                if (ev_ap_obs.qtd + ev_hipop.qtd + ev_rera.qtd > 1)
                {
                    texto2RERA += totalObsMisHipRera.ToString() + " " + f_var("Var58031") + ".";
                }
                else
                {
                    texto2RERA += totalObsMisHipRera.ToString() + " " + f_var("Var58030") + ".";
                }

                // Caso você deseje reativar o trecho comentado no VB6:
                /*
                if (ev_ap_mis.qtd > 1)
                {
                    texto2RERA += ev_ap_mis.qtd.ToString() + " " + f_var("Var58020");
                }
                else
                {
                    texto2RERA += ev_ap_mis.qtd.ToString() + " " + f_var("Var58019");
                }
                */
            }

            if (passagem == ultimapassagem)
            {
                SubstituiVar("&(RESUMO_RESP_INT)&", texto2RERA);
                SubstituiVar("&(RESUMO_RESP_INT_RERA)&", texto2RERA);
            }
            else
            {
                SubstituiVar("&(RESUMO_RESP)&", texto2RERA);
                SubstituiVar("&(RESUMO_RESP_RERA)&", texto2RERA);

                SubstituiVar("&(TEMPO_DORSAL)&", TimeSpan.FromSeconds(pos_c).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TEMPO_NDORSAL)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) - pos_c).ToString(@"hh\:mm\:ss"));

                // MAIORIA_RESP
                if (ev_ap_cen.qtd > (ev_ap_obs.qtd + ev_hipop.qtd) && ev_ap_cen.qtd > ev_ap_mis.qtd)
                {
                    SubstituiVar("&(MAIORIA_RESP)&", f_var("Var58004"));
                }
                else if ((ev_ap_obs.qtd + ev_hipop.qtd) > ev_ap_cen.qtd && (ev_ap_obs.qtd + ev_hipop.qtd) > ev_ap_mis.qtd)
                {
                    SubstituiVar("&(MAIORIA_RESP)&", "obstrutiva");
                }
                else
                {
                    SubstituiVar("&(MAIORIA_RESP)&", "mista");
                }

                // QUANT_RESP
                SubstituiVar("&(QUANT_RESP)&", (ev_ap.qtd + ev_hipop.qtd).ToString());

                // QUANT_CEN
                SubstituiVar("&(QUANT_CEN)&", ev_ap_cen.qtd.ToString());

                // QUANT_OBS
                SubstituiVar("&(QUANT_OBS)&", (ev_ap_obs.qtd + ev_hipop.qtd).ToString());

                // QUANT_OBS_RERA
                SubstituiVar("&(QUANT_OBS_RERA)&", (ev_ap_obs.qtd + ev_ap_mis.qtd + ev_hipop.qtd + ev_rera.qtd).ToString());

                // IND_OBS_RERA
                SubstituiVar("&(IND_OBS_RERA)&", (ev_ap_obs.indice + ev_ap_mis.indice + ev_hipop.indice + ev_rera.indice).ToString("0.0"));

                // QUANT_MIS
                SubstituiVar("&(QUANT_MIS)&", ev_ap_mis.qtd.ToString());

                // QUANT_OBS_MIS (obs: parece que houve repetição da tag QUANT_OBS no original)
                SubstituiVar("&(QUANT_OBS)&", (ev_ap_obs.qtd + ev_ap_mis.qtd + ev_hipop.qtd).ToString());

                // IND_OBS
                if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                {
                    SubstituiVar("&(IND_OBS)&", "0.0");
                }
                else
                {
                    SubstituiVar("&(IND_OBS)&", (ev_ap_obs.indice + ev_hipop.indice).ToString("0.0"));
                }

                // IND_OBS_MIS
                if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                {
                    SubstituiVar("&(IND_OBS_MIS)&", "0.0");
                }
                else
                {
                    SubstituiVar("&(IND_OBS_MIS)&", (ev_ap_obs.indice + ev_ap_mis.indice + ev_hipop.indice).ToString("0.0"));
                }
            }

            // APNEIA e HIPOP COM DESSAT
            SubstituiVar("&(QTD_APNEIA_DESSAT)&", (qtd_ap_cen_com_dessat + qtd_ap_obs_com_dessat + qtd_ap_mis_com_dessat).ToString("0"));
            SubstituiVar("&(QTD_APNEIA_CEN_DESSAT)&", qtd_ap_cen_com_dessat.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_OBS_DESSAT)&", qtd_ap_obs_com_dessat.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_MIS_DESSAT)&", qtd_ap_mis_com_dessat.ToString("0"));
            SubstituiVar("&(QTD_HIPOP_DESSAT)&", qtd_hipop_com_dessat.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_HIPOP_DESSAT)&", (qtd_hipop_com_dessat + qtd_ap_cen_com_dessat + qtd_ap_obs_com_dessat + qtd_ap_mis_com_dessat).ToString("0"));

            // APNEIA e HIPOP COM MDESP
            SubstituiVar("&(QTD_APNEIA_MDESP)&", (qtd_ap_cen_com_mdesp + qtd_ap_obs_com_mdesp + qtd_ap_mis_com_mdesp).ToString("0"));
            SubstituiVar("&(QTD_APNEIA_CEN_MDESP)&", qtd_ap_cen_com_mdesp.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_OBS_MDESP)&", qtd_ap_obs_com_mdesp.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_MIS_MDESP)&", qtd_ap_mis_com_mdesp.ToString("0"));
            SubstituiVar("&(QTD_HIPOP_MDESP)&", qtd_hipop_com_mdesp.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_HIPOP_MDESP)&", (qtd_hipop_com_mdesp + qtd_ap_cen_com_mdesp + qtd_ap_obs_com_mdesp + qtd_ap_mis_com_mdesp).ToString("0"));

            // APNEIA e HIPOP COM DESSAT e MDESP
            SubstituiVar("&(QTD_APNEIA_MDESP_DESSAT)&", (qtd_ap_cen_com_dessat_e_mdesp + qtd_ap_obs_com_dessat_e_mdesp + qtd_ap_mis_com_dessat_e_mdesp).ToString("0"));
            SubstituiVar("&(QTD_APNEIA_CEN_MDESP_DESSAT)&", qtd_ap_cen_com_dessat_e_mdesp.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_OBS_MDESP_DESSAT)&", qtd_ap_obs_com_dessat_e_mdesp.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_MIS_MDESP_DESSAT)&", qtd_ap_mis_com_dessat_e_mdesp.ToString("0"));
            SubstituiVar("&(QTD_HIPOP_MDESP_DESSAT)&", qtd_hipop_com_dessat_e_mdesp.ToString("0"));
            SubstituiVar("&(QTD_APNEIA_HIPOP_MDESP_DESSAT)&", (qtd_hipop_com_dessat_e_mdesp + qtd_ap_cen_com_dessat_e_mdesp + qtd_ap_obs_com_dessat_e_mdesp + qtd_ap_mis_com_dessat_e_mdesp).ToString("0"));

            // RERA
            SubstituiVar("&(QTD_RERA_DESSAT)&", qtd_RERA_com_dessat.ToString("0"));
            SubstituiVar("&(QTD_RERA_MDESP)&", qtd_RERA_com_mdesp.ToString("0"));
            SubstituiVar("&(QTD_RERA_MDESP_DESSAT)&", qtd_RERA_com_dessat_e_mdesp.ToString("0"));

            SubstituiVar("&(QTD_PLM_MDESP)&", qtd_PLM_com_mdesp.ToString("0"));

            // APNEIA
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_APNEIA)&", "0.0");
            else
                SubstituiVar("&(PORC_APNEIA)&", (ev_ap.qtd / (double)(ev_ap.qtd + ev_hipop.qtd)).ToString("0.0"));

            // DESPERTAR
            SubstituiVar("&(QTD_DESP_DESSAT)&", qtd_Desp_com_dessat.ToString("0"));

            // QTD_APNEIA
            SubstituiVar("&(QTD_APNEIA)&", ev_ap.qtd.ToString("0"));

            // IND_APNEIA
            SubstituiVar("&(IND_APNEIA)&", ev_ap.indice.ToString("0.0"));

            // MAIOR_APNEIA
            SubstituiVar("&(MAIOR_APNEIA)&", ev_ap.maior.ToString("0.0"));

            // MEDIA_APNEIA
            SubstituiVar("&(MEDIA_APNEIA)&", ev_ap.media.ToString("0.0"));

            // QTD_REM_APNEIA
            SubstituiVar("&(QTD_REM_APNEIA)&", ev_ap.qtd_rem.ToString("0"));

            // IND_REM_APNEIA
            if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"])== 0)
                SubstituiVar("&(IND_REM_APNEIA)&", "0.0");
            else
                SubstituiVar("&(IND_REM_APNEIA)&", (ev_ap.qtd_rem / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]) / 3600.0)).ToString("0.0"));

            // QTD_NREM_APNEIA
            SubstituiVar("&(QTD_NREM_APNEIA)&", ev_ap.qtd_nrem.ToString("0"));

            // IND_NREM_APNEIA
            double est_nrem = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]) + Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]) + Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]) + Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]);
            if (est_nrem == 0)
                SubstituiVar("&(IND_NREM_APNEIA)&", "0.0");
            else
                SubstituiVar("&(IND_NREM_APNEIA)&", (ev_ap.qtd_nrem / (est_nrem / 3600.0)).ToString("0.0"));

            // Posição - QTD_PC_APNEIA
            SubstituiVar("&(QTD_PC_APNEIA)&", ev_ap.qtd_pos_c.ToString("0"));

            // IND_PC_APNEIA
            if (ev_ap.qtd_pos_c == 0 || Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
            {
                SubstituiVar("&(IND_PC_APNEIA)&", "0.0");
                SubstituiVar("&(IND_PC_APNEIA_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PC_APNEIA)&", (ev_ap.qtd_pos_c / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) / 3600.0)).ToString("0.0"));
                SubstituiVar("&(IND_PC_APNEIA_TTS)&", (ev_ap.qtd_pos_c / (pos_c / 3600.0)).ToString("0.0"));
            }

            // PORC_PC_APNEIA
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PC_APNEIA)&", "0.0");
            else
                SubstituiVar("&(PORC_PC_APNEIA)&", ((ev_ap.qtd_pos_c / (double)(ev_ap.qtd + ev_hipop.qtd)) * 100).ToString("0.0"));

            // QTD_PNC_APNEIA
            SubstituiVar("&(QTD_PNC_APNEIA)&", ev_ap.qtd_pos_x.ToString("0"));

            // IND_PNC_APNEIA
            if (ev_ap.qtd_pos_x == 0 || Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
            {
                SubstituiVar("&(IND_PNC_APNEIA)&", "0.0");
                SubstituiVar("&(IND_PNC_APNEIA_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PNC_APNEIA)&", (ev_ap.qtd_pos_x / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) / 3600.0)).ToString("0.0"));
                SubstituiVar("&(IND_PNC_APNEIA_TTS)&", (ev_ap.qtd_pos_x / ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) - pos_c) / 3600.0)).ToString("0.0"));
            }

            // "PORC_PNC_APNEIA"
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PNC_APNEIA)&", "0.0");
            else
                SubstituiVar("&(PORC_PNC_APNEIA)&", Math.Round(ev_ap.qtd_pos_x * 100.0 / (ev_ap.qtd + ev_hipop.qtd), 1).ToString("0.0"));

            // "PORC_APNEIA_CEN"
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_APNEIA_CEN)&", "0.0");
            else
                SubstituiVar("&(PORC_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd * 100.0 / (ev_ap.qtd + ev_hipop.qtd), 1).ToString("0.0"));

            // "QTD_APNEIA_CEN"
            SubstituiVar("&(QTD_APNEIA_CEN)&", ev_ap_cen.qtd.ToString("0"));

            // "IND_APNEIA_CEN"
            SubstituiVar("&(IND_APNEIA_CEN)&", ev_ap_cen.indice.ToString("0.0"));

            // "MAIOR_APNEIA_CEN"
            SubstituiVar("&(MAIOR_APNEIA_CEN)&", ev_ap_cen.maior.ToString("0.0"));

            // "MEDIA_APNEIA_CEN"
            SubstituiVar("&(MEDIA_APNEIA_CEN)&", ev_ap_cen.media.ToString("0.0"));

            // "QTD_REM_APNEIA_CEN"
            SubstituiVar("&(QTD_REM_APNEIA_CEN)&", ev_ap_cen.qtd_rem.ToString("0"));

            double EstagioRem = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]);
            double Est1 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]);
            double Est2 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]);
            double Est3 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]);
            double Est4 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]);
            double TTS = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);

            // "IND_REM_APNEIA_CEN"
            if (EstagioRem == 0)
                SubstituiVar("&(IND_REM_APNEIA_CEN)&", "0.0");
            else
                SubstituiVar("&(IND_REM_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd_rem / (EstagioRem / 3600.0), 1).ToString("0.0"));

            // "QTD_NREM_APNEIA_CEN"
            SubstituiVar("&(QTD_NREM_APNEIA_CEN)&", ev_ap_cen.qtd_nrem.ToString("0"));

            // "IND_NREM_APNEIA_CEN"
            double tempoNrem = Est1 + Est2 + Est3 + Est4;
            if (tempoNrem == 0)
                SubstituiVar("&(IND_NREM_APNEIA_CEN)&", "0.0");
            else
                SubstituiVar("&(IND_NREM_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd_nrem / (tempoNrem / 3600.0), 1).ToString("0.0"));

            // "QTD_PC_APNEIA_CEN"
            SubstituiVar("&(QTD_PC_APNEIA_CEN)&", ev_ap_cen.qtd_pos_c.ToString("0"));

            // "IND_PC_APNEIA_CEN" e "IND_PC_APNEIA_CEN_TTS"
            if (ev_ap_cen.qtd_pos_c == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PC_APNEIA_CEN)&", "0.0");
                SubstituiVar("&(IND_PC_APNEIA_CEN_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PC_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd_pos_c / (TTS / 3600.0), 1).ToString("0.0"));
                SubstituiVar("&(IND_PC_APNEIA_CEN_TTS)&", Math.Round(ev_ap_cen.qtd_pos_c / (pos_c / 3600.0), 1).ToString("0.0"));
            }

            // "PORC_PC_APNEIA_CEN"
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PC_APNEIA_CEN)&", "0.0");
            else
                SubstituiVar("&(PORC_PC_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd_pos_c * 100.0 / (ev_ap.qtd + ev_hipop.qtd), 1).ToString("0.0"));

            // "QTD_PNC_APNEIA_CEN"
            SubstituiVar("&(QTD_PNC_APNEIA_CEN)&", ev_ap_cen.qtd_pos_x.ToString("0"));

            // "IND_PNC_APNEIA_CEN" e "IND_PNC_APNEIA_CEN_TTS"
            if (ev_ap_cen.qtd_pos_x == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PNC_APNEIA_CEN)&", "0.0");
                SubstituiVar("&(IND_PNC_APNEIA_CEN_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PNC_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd_pos_x / (TTS / 3600.0), 1).ToString("0.0"));
                SubstituiVar("&(IND_PNC_APNEIA_CEN_TTS)&", Math.Round(ev_ap_cen.qtd_pos_x / ((TTS - pos_c) / 3600.0), 1).ToString("0.0"));
            }

            // "PORC_PNC_APNEIA_CEN"
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PNC_APNEIA_CEN)&", "0.0");
            else
                SubstituiVar("&(PORC_PNC_APNEIA_CEN)&", Math.Round(ev_ap_cen.qtd_pos_x * 100.0 / (ev_ap.qtd + ev_hipop.qtd), 1).ToString("0.0"));

            double totalApHipop = ev_ap.qtd + ev_hipop.qtd;
            SubstituiVar("&(PORC_APNEIA_OBS)&", totalApHipop == 0 ? "0.0" : (ev_ap_obs.qtd / totalApHipop * 100).ToString("0.0"));

            SubstituiVar("&(QTD_APNEIA_OBS)&", ev_ap_obs.qtd.ToString("0"));
            SubstituiVar("&(IND_APNEIA_OBS)&", ev_ap_obs.indice.ToString("0.0"));
            SubstituiVar("&(MAIOR_APNEIA_OBS)&", ev_ap_obs.maior.ToString("0.0"));
            SubstituiVar("&(MEDIA_APNEIA_OBS)&", ev_ap_obs.media.ToString("0.0"));
            SubstituiVar("&(QTD_REM_APNEIA_OBS)&", ev_ap_obs.qtd_rem.ToString("0"));

            // IND_REM_APNEIA_OBS
            double est5 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]);
            SubstituiVar("&(IND_REM_APNEIA_OBS)&", est5 == 0 ? "0.0" : (ev_ap_obs.qtd_rem / (est5 / 3600)).ToString("0.0"));

            SubstituiVar("&(QTD_NREM_APNEIA_OBS)&", ev_ap_obs.qtd_nrem.ToString("0"));

            double est1_4 = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]) +
                            Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]) +
                            Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]) +
                            Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]);

            SubstituiVar("&(IND_NREM_APNEIA_OBS)&", est1_4 == 0 ? "0.0" : (ev_ap_obs.qtd_nrem / (est1_4 / 3600)).ToString("0.0"));

            // Posição - PC
            SubstituiVar("&(QTD_PC_APNEIA_OBS)&", ev_ap_obs.qtd_pos_c.ToString("0"));

            double tts = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);
            if (ev_ap_obs.qtd_pos_c == 0 || tts == 0)
            {
                SubstituiVar("&(IND_PC_APNEIA_OBS)&", "0.0");
                SubstituiVar("&(IND_PC_APNEIA_OBS_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PC_APNEIA_OBS)&", (ev_ap_obs.qtd_pos_c / (tts / 3600)).ToString("0.0"));
                SubstituiVar("&(IND_PC_APNEIA_OBS_TTS)&", (ev_ap_obs.qtd_pos_c / (pos_c / 3600)).ToString("0.0"));
            }

            SubstituiVar("&(PORC_PC_APNEIA_OBS)&", totalApHipop == 0 ? "0.0" : (ev_ap_obs.qtd_pos_c / totalApHipop * 100).ToString("0.0"));

            // Posição - PNC
            SubstituiVar("&(QTD_PNC_APNEIA_OBS)&", ev_ap_obs.qtd_pos_x.ToString("0"));

            if (ev_ap_obs.qtd_pos_x == 0 || tts == 0)
            {
                SubstituiVar("&(IND_PNC_APNEIA_OBS)&", "0.0");
                SubstituiVar("&(IND_PNC_APNEIA_OBS_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PNC_APNEIA_OBS)&", (ev_ap_obs.qtd_pos_x / (tts / 3600)).ToString("0.0"));
                SubstituiVar("&(IND_PNC_APNEIA_OBS_TTS)&", (ev_ap_obs.qtd_pos_x / ((tts - pos_c) / 3600)).ToString("0.0"));
            }

            SubstituiVar("&(PORC_PNC_APNEIA_OBS)&", totalApHipop == 0 ? "0.0" : (ev_ap_obs.qtd_pos_x / totalApHipop * 100).ToString("0.0"));

            // PORC_APNEIA_MIS
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_APNEIA_MIS)&", "0.0");
            else
                SubstituiVar("&(PORC_APNEIA_MIS)&", (ev_ap_mis.qtd / (ev_ap.qtd + ev_hipop.qtd)).ToString("0.0"));

            // QTD_APNEIA_MIS
            SubstituiVar("&(QTD_APNEIA_MIS)&", ev_ap_mis.qtd.ToString("0"));

            // IND_APNEIA_MIS
            SubstituiVar("&(IND_APNEIA_MIS)&", ev_ap_mis.indice.ToString("0.0"));

            // MAIOR_APNEIA_MIS
            SubstituiVar("&(MAIOR_APNEIA_MIS)&", ev_ap_mis.maior.ToString("0.0"));

            // MEDIA_APNEIA_MIS
            SubstituiVar("&(MEDIA_APNEIA_MIS)&", ev_ap_mis.media.ToString("0.0"));

            // QTD_REM_APNEIA_MIS
            SubstituiVar("&(QTD_REM_APNEIA_MIS)&", ev_ap_mis.qtd_rem.ToString("0"));

            // IND_REM_APNEIA_MIS
            if (est5 == 0)
                SubstituiVar("&(IND_REM_APNEIA_MIS)&", "0.0");
            else
                SubstituiVar("&(IND_REM_APNEIA_MIS)&", (ev_ap_mis.qtd_rem / (est5 / 3600.0)).ToString("0.0"));

            // QTD_NREM_APNEIA_MIS
            SubstituiVar("&(QTD_NREM_APNEIA_MIS)&", ev_ap_mis.qtd_nrem.ToString("0"));

            // IND_NREM_APNEIA_MIS
            if (tempoNrem == 0)
                SubstituiVar("&(IND_NREM_APNEIA_MIS)&", "0.0");
            else
                SubstituiVar("&(IND_NREM_APNEIA_MIS)&", (ev_ap_mis.qtd_nrem / (tempoNrem / 3600.0)).ToString("0.0"));

            // QTD_PC_APNEIA_MIS
            SubstituiVar("&(QTD_PC_APNEIA_MIS)&", ev_ap_mis.qtd_pos_c.ToString("0"));

            // IND_PC_APNEIA_MIS
            if (ev_ap_mis.qtd_pos_c == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PC_APNEIA_MIS)&", "0.0");
                SubstituiVar("&(IND_PC_APNEIA_MIS_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PC_APNEIA_MIS)&", (ev_ap_mis.qtd_pos_c / (TTS / 3600.0)).ToString("0.0"));
                SubstituiVar("&(IND_PC_APNEIA_MIS_TTS)&", (ev_ap_mis.qtd_pos_c / (pos_c / 3600.0)).ToString("0.0"));
            }

            // PORC_PC_APNEIA_MIS
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PC_APNEIA_MIS)&", "0.0");
            else
                SubstituiVar("&(PORC_PC_APNEIA_MIS)&", ((ev_ap_mis.qtd_pos_c / (ev_ap.qtd + ev_hipop.qtd)) * 100).ToString("0.0"));

            // QTD_PNC_APNEIA_MIS
            SubstituiVar("&(QTD_PNC_APNEIA_MIS)&", ev_ap_mis.qtd_pos_x.ToString("0"));

            // IND_PNC_APNEIA_MIS
            if (ev_ap_mis.qtd_pos_x == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PNC_APNEIA_MIS)&", "0.0");
                SubstituiVar("&(IND_PNC_APNEIA_MIS_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PNC_APNEIA_MIS)&", (ev_ap_mis.qtd_pos_x / (TTS / 3600.0)).ToString("0.0"));
                SubstituiVar("&(IND_PNC_APNEIA_MIS_TTS)&", (ev_ap_mis.qtd_pos_x / ((TTS - pos_c) / 3600.0)).ToString("0.0"));
            }

            // PORC_PNC_APNEIA_MIS
            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PNC_APNEIA_MIS)&", "0.0");
            else
                SubstituiVar("&(PORC_PNC_APNEIA_MIS)&", ((ev_ap_mis.qtd_pos_x / (ev_ap.qtd + ev_hipop.qtd)) * 100).ToString("0.0"));

            // RERA e IDR
            s_dados_RERA_IDR();

            // HIPOPNEIA

            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_HIPOPNEIA)&", "0.0");
            else
                SubstituiVar("&(PORC_HIPOPNEIA)&", ((ev_hipop.qtd * 100.0) / (ev_ap.qtd + ev_hipop.qtd)).ToString("0.0"));

            SubstituiVar("&(QTD_HIPOPNEIA)&", ev_hipop.qtd.ToString("0"));
            SubstituiVar("&(IND_HIPOPNEIA)&", ev_hipop.indice.ToString("0.0"));
            SubstituiVar("&(MAIOR_HIPOPNEIA)&", ev_hipop.maior.ToString("0.0"));
            SubstituiVar("&(MEDIA_HIPOPNEIA)&", ev_hipop.media.ToString("0.0"));

            SubstituiVar("&(QTD_REM_HIPOPNEIA)&", ev_hipop.qtd_rem.ToString("0"));

            if (est5 == 0)
                SubstituiVar("&(IND_REM_HIPOPNEIA)&", "0.0");
            else
                SubstituiVar("&(IND_REM_HIPOPNEIA)&", (ev_hipop.qtd_rem / (est5 / 3600.0)).ToString("0.0"));

            SubstituiVar("&(QTD_NREM_HIPOPNEIA)&", ev_hipop.qtd_nrem.ToString("0"));

            double estNrem = Est1 + Est2 + Est3 + Est4;
            if (estNrem == 0)
                SubstituiVar("&(IND_NREM_HIPOPNEIA)&", "0.0");
            else
                SubstituiVar("&(IND_NREM_HIPOPNEIA)&", (ev_hipop.qtd_nrem / (estNrem / 3600.0)).ToString("0.0"));

            // POSIÇÃO

            SubstituiVar("&(QTD_PC_HIPOPNEIA)&", ev_hipop.qtd_pos_c.ToString("0"));

            if (ev_hipop.qtd_pos_c == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PC_HIPOPNEIA)&", "0.0");
                SubstituiVar("&(IND_PC_HIPOPNEIA_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PC_HIPOPNEIA)&", (ev_hipop.qtd_pos_c / (TTS / 3600.0)).ToString("0.0"));
                if (pos_c > 0)
                    SubstituiVar("&(IND_PC_HIPOPNEIA_TTS)&", (ev_hipop.qtd_pos_c / (pos_c / 3600.0)).ToString("0.0"));
                else
                    SubstituiVar("&(IND_PC_HIPOPNEIA_TTS)&", "0.0");
            }

            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PC_HIPOPNEIA)&", "0.0");
            else
                SubstituiVar("&(PORC_PC_HIPOPNEIA)&", ((ev_hipop.qtd_pos_c * 100.0) / (ev_ap.qtd + ev_hipop.qtd)).ToString("0.0"));

            SubstituiVar("&(QTD_PNC_HIPOPNEIA)&", ev_hipop.qtd_pos_x.ToString("0"));

            if (ev_hipop.qtd_pos_x == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PNC_HIPOPNEIA)&", "0.0");
                SubstituiVar("&(IND_PNC_HIPOPNEIA_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PNC_HIPOPNEIA)&", (ev_hipop.qtd_pos_x / (TTS / 3600.0)).ToString("0.0"));
                SubstituiVar("&(IND_PNC_HIPOPNEIA_TTS)&", (ev_hipop.qtd_pos_x / ((TTS - pos_c) / 3600.0)).ToString("0.0"));
            }

            if (ev_ap.qtd + ev_hipop.qtd == 0)
                SubstituiVar("&(PORC_PNC_HIPOPNEIA)&", "0.0");
            else
                SubstituiVar("&(PORC_PNC_HIPOPNEIA)&", ((ev_hipop.qtd_pos_x * 100.0) / (ev_ap.qtd + ev_hipop.qtd)).ToString("0.0"));

            // HIPOPNEIA_OBS

            SubstituiVar("&(QTD_HIPOPNEIA_OBS)&", ev_hipop_obs.qtd.ToString("0"));
            SubstituiVar("&(IND_HIPOPNEIA_OBS)&", ev_hipop_obs.indice.ToString("0.0"));
            SubstituiVar("&(MAIOR_HIPOPNEIA_OBS)&", ev_hipop_obs.maior.ToString("0.0"));
            SubstituiVar("&(MEDIA_HIPOPNEIA_OBS)&", ev_hipop_obs.media.ToString("0.0"));
            SubstituiVar("&(QTD_REM_HIPOPNEIA_OBS)&", ev_hipop_obs.qtd_rem.ToString("0"));
            SubstituiVar("&(QTD_NREM_HIPOPNEIA_OBS)&", ev_hipop_obs.qtd_nrem.ToString("0"));

            if (estNrem == 0)
                SubstituiVar("&(IND_NREM_HIPOPNEIA_OBS)&", "0.0");
            else
                SubstituiVar("&(IND_NREM_HIPOPNEIA_OBS)&", (ev_hipop_obs.qtd_nrem / (estNrem / 3600.0)).ToString("0.0"));

            // APNEIA E HIPOPNEIA

            // QTD_APNEIA_HIPOP
            SubstituiVar("&(QTD_APNEIA_HIPOP)&", (ev_ap.qtd + ev_hipop.qtd).ToString("0"));

            // IND_APNEIA_HIPOP
            SubstituiVar("&(IND_APNEIA_HIPOP)&", (ev_ap.indice + ev_hipop.indice).ToString("0.0"));

            // IND_APNEIA_HIPOP sem apneia central
            SubstituiVar("&(IND_AP_M_OB_HIP)&", (ev_ap.indice + ev_hipop.indice - ev_ap_cen.indice).ToString("0.0"));

            // MAIOR_APNEIA_HIPOP e MAIOR_EV_RESP
            var maiorApneiaHipop = Math.Max(ev_ap.maior, ev_hipop.maior).ToString("0.0");
            SubstituiVar("&(MAIOR_APNEIA_HIPOP)&", maiorApneiaHipop);
            SubstituiVar("&(MAIOR_EV_RESP)&", maiorApneiaHipop);

            // MEDIA_APNEIA_HIPOP e MEDIA_EV_RESP
            int totalQtd = ev_ap.qtd + ev_hipop.qtd;
            if (totalQtd == 0)
            {
                SubstituiVar("&(MEDIA_APNEIA_HIPOP)&", "0.0");
                SubstituiVar("&(MEDIA_EV_RESP)&", "0.0");
            }
            else
            {
                double media = (ev_ap.media * ev_ap.qtd + ev_hipop.media * ev_hipop.qtd) / totalQtd;
                SubstituiVar("&(MEDIA_APNEIA_HIPOP)&", media.ToString("0.0"));
                SubstituiVar("&(MEDIA_EV_RESP)&", media.ToString("0.0"));
            }

            // QTD_REM_APNEIA_HIPOP
            SubstituiVar("&(QTD_REM_APNEIA_HIPOP)&", (ev_ap.qtd_rem + ev_hipop.qtd_rem).ToString("0"));

            // IND_REM_APNEIA_HIPOP
            if (est5 == 0)
            {
                SubstituiVar("&(IND_REM_APNEIA_HIPOP)&", "0.0");
            }
            else
            {
                double indRem = (ev_ap.qtd_rem + ev_hipop.qtd_rem) / (est5 / 3600.0);
                SubstituiVar("&(IND_REM_APNEIA_HIPOP)&", indRem.ToString("0.0"));
            }

            // QTD_NREM_APNEIA_HIPOP
            SubstituiVar("&(QTD_NREM_APNEIA_HIPOP)&", (ev_ap.qtd_nrem + ev_hipop.qtd_nrem).ToString("0"));

            // IND_NREM_APNEIA_HIPOP
            if (estNrem == 0)
            {
                SubstituiVar("&(IND_NREM_APNEIA_HIPOP)&", "0.0");
            }
            else
            {
                double indNrem = (ev_ap.qtd_nrem + ev_hipop.qtd_nrem) / (estNrem / 3600.0);
                SubstituiVar("&(IND_NREM_APNEIA_HIPOP)&", indNrem.ToString("0.0"));
            }

            // Posição - QTD_PC_APNEIA_HIPOP
            SubstituiVar("&(QTD_PC_APNEIA_HIPOP)&", (ev_ap.qtd_pos_c + ev_hipop.qtd_pos_c).ToString("0"));

            // IND_PC_APNEIA_HIPOP e IND_PC_APNEIA_HIPOP_TTS
            int qtdPosC = ev_ap.qtd_pos_c + ev_hipop.qtd_pos_c;
            if (qtdPosC == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PC_APNEIA_HIPOP)&", "0.0");
                SubstituiVar("&(IND_PC_APNEIA_HIPOP_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PC_APNEIA_HIPOP)&", (qtdPosC / (TTS / 3600.0)).ToString("0.0"));
                if (pos_c > 0)
                {
                    SubstituiVar("&(IND_PC_APNEIA_HIPOP_TTS)&", (qtdPosC / (pos_c / 3600.0)).ToString("0.0"));
                }
                else
                {
                    SubstituiVar("&(IND_PC_APNEIA_HIPOP_TTS)&", "0.0");
                }
            }

            // PORC_PC_APNEIA_HIPOP
            if (totalQtd == 0)
            {
                SubstituiVar("&(PORC_PC_APNEIA_HIPOP)&", "0.0");
            }
            else
            {
                SubstituiVar("&(PORC_PC_APNEIA_HIPOP)&", (qtdPosC * 100.0 / totalQtd).ToString("0.0"));
            }

            // QTD_PNC_APNEIA_HIPOP
            SubstituiVar("&(QTD_PNC_APNEIA_HIPOP)&", (ev_ap.qtd_pos_x + ev_hipop.qtd_pos_x).ToString("0"));

            // IND_PNC_APNEIA_HIPOP e IND_PNC_APNEIA_HIPOP_TTS
            int qtdPosX = ev_ap.qtd_pos_x + ev_hipop.qtd_pos_x;
            if (qtdPosX == 0 || TTS == 0)
            {
                SubstituiVar("&(IND_PNC_APNEIA_HIPOP)&", "0.0");
                SubstituiVar("&(IND_PNC_APNEIA_HIPOP_TTS)&", "0.0");
            }
            else
            {
                SubstituiVar("&(IND_PNC_APNEIA_HIPOP)&", (qtdPosX / (TTS / 3600.0)).ToString("0.0"));
                SubstituiVar("&(IND_PNC_APNEIA_HIPOP_TTS)&", (qtdPosX / ((TTS - pos_c) / 3600.0)).ToString("0.0"));
            }

            // PORC_PNC_APNEIA_HIPOP
            if (totalQtd == 0)
            {
                SubstituiVar("&(PORC_PNC_APNEIA_HIPOP)&", "0.0");
            }
            else
            {
                SubstituiVar("&(PORC_PNC_APNEIA_HIPOP)&", (qtdPosX * 100.0 / totalQtd).ToString("0.0"));
            }

            maior_ev_num = ev_ap_cen.qtd;
            maior_ev_string = f_var("Var58066");
            if (ev_ap_obs.qtd > maior_ev_num)
            {
                maior_ev_num = ev_ap_obs.qtd;
                maior_ev_string = f_var("Var58068");
            }
            if (ev_ap_mis.qtd > maior_ev_num)
            {
                maior_ev_num = ev_ap_mis.qtd;
                maior_ev_string = f_var("Var58067");
            }
            if (ev_hipop.qtd > maior_ev_num)
            {
                maior_ev_num = ev_hipop.qtd;
                maior_ev_string = f_var("Var56044");
            }
            if (ev_rera.qtd > maior_ev_num)
            {
                maior_ev_num = ev_rera.qtd;
                maior_ev_string = "RERA";
            }
            SubstituiVar("&(MAIOR_QTDE_EV_RESP)&", maior_ev_string);

        }
        public static void s_dados_RERA_IDR()
        {
            try
            {
                DataRow tblResumoExame = GlobVar.tbl_ResumoExame.Rows[0];
                // RERA
                SubstituiVar("&(QTD_RERA)&", ev_rera.qtd.ToString("0"));
                SubstituiVar("&(IND_RERA)&", ev_rera.indice.ToString("0.0"));
                SubstituiVar("&(MAIOR_RERA)&", ev_rera.maior.ToString("0.0"));
                SubstituiVar("&(MEDIA_RERA)&", ev_rera.media.ToString("0.0"));

                SubstituiVar("&(QTD_REM_RERA)&", ev_rera.qtd_rem.ToString("0"));

                double est5 = Convert.ToDouble(tblResumoExame["Est_5"]);
                SubstituiVar("&(IND_REM_RERA)&", est5 == 0 ? "0.0" :
                    (ev_rera.qtd_rem / (est5 / 3600)).ToString("0.0"));

                int qtdNrem = ev_rera.qtd_nrem;
                SubstituiVar("&(QTD_NREM_RERA)&", qtdNrem.ToString("0"));

                double totalNrem = Convert.ToDouble(tblResumoExame["Est_1"]) + Convert.ToDouble(tblResumoExame["Est_2"]) +
                                   Convert.ToDouble(tblResumoExame["Est_3"]) + Convert.ToDouble(tblResumoExame["Est_4"]);
                SubstituiVar("&(IND_NREM_RERA)&", totalNrem == 0 ? "0.0" :
                    (qtdNrem / (totalNrem / 3600)).ToString("0.0"));

                SubstituiVar("&(QTD_PC_RERA)&", ev_rera.qtd_pos_c.ToString("0"));

                double tts = Convert.ToDouble(tblResumoExame["TTS"]);
                if (ev_rera.qtd_pos_c == 0 || tts == 0)
                {
                    SubstituiVar("&(IND_PC_RERA)&", "0.0");
                    SubstituiVar("&(IND_PC_RERA_TTS)&", "0.0");
                }
                else
                {
                    SubstituiVar("&(IND_PC_RERA)&", (ev_rera.qtd_pos_c / (tts / 3600)).ToString("0.0"));
                    double pos_c = Convert.ToDouble(tblResumoExame["pos_c"]);
                    SubstituiVar("&(IND_PC_RERA_TTS)&", (ev_rera.qtd_pos_c / (pos_c / 3600)).ToString("0.0"));
                }

                SubstituiVar("&(PORC_PC_RERA)&", ev_rera.qtd == 0 ? "0.0" :
                    ((ev_rera.qtd_pos_c / (double)ev_rera.qtd) * 100).ToString("0.0"));

                SubstituiVar("&(QTD_PNC_RERA)&", ev_rera.qtd_pos_x.ToString("0"));

                if (ev_rera.qtd_pos_x == 0 || tts == 0)
                {
                    SubstituiVar("&(IND_PNC_RERA)&", "0.0");
                    SubstituiVar("&(IND_PNC_RERA_TTS)&", "0.0");
                }
                else
                {
                    double pos_c = Convert.ToDouble(tblResumoExame["pos_c"]);
                    SubstituiVar("&(IND_PNC_RERA)&", (ev_rera.qtd_pos_x / (tts / 3600)).ToString("0.0"));
                    SubstituiVar("&(IND_PNC_RERA_TTS)&", (ev_rera.qtd_pos_x / ((tts - pos_c) / 3600)).ToString("0.0"));
                }

                SubstituiVar("&(PORC_PNC_RERA)&", ev_rera.qtd == 0 ? "0.0" :
                    ((ev_rera.qtd_pos_x / (double)ev_rera.qtd) * 100).ToString("0.0"));

                // IDR
                int qtdIDR = ev_ap.qtd + ev_hipop.qtd + ev_rera.qtd;
                SubstituiVar("&(QTD_IDR)&", qtdIDR.ToString("0"));
                SubstituiVar("&(IND_IDR)&", (ev_rera.indice + ev_ap.indice + ev_hipop.indice).ToString("0.0"));

                double maiorIDR = new[] { ev_rera.maior, ev_ap.maior, ev_hipop.maior }.Max();
                SubstituiVar("&(MAIOR_IDR)&", maiorIDR.ToString("0.0"));

                if (qtdIDR == 0)
                    SubstituiVar("&(MEDIA_IDR)&", "0.0");
                else
                {
                    double mediaIDR = (ev_ap.media * ev_ap.qtd + ev_hipop.media * ev_hipop.qtd + ev_rera.media * ev_rera.qtd) / qtdIDR;
                    SubstituiVar("&(MEDIA_IDR)&", mediaIDR.ToString("0.0"));
                }

                SubstituiVar("&(QTD_REM_IDR)&", (ev_ap.qtd_rem + ev_hipop.qtd_rem + ev_rera.qtd_rem).ToString("0"));
                SubstituiVar("&(IND_REM_IDR)&", est5 == 0 ? "0.0" :
                    ((ev_ap.qtd_rem + ev_hipop.qtd_rem + ev_rera.qtd_rem) / (est5 / 3600)).ToString("0.0"));

                SubstituiVar("&(QTD_NREM_IDR)&", (ev_ap.qtd_nrem + ev_hipop.qtd_nrem + ev_rera.qtd_nrem).ToString("0"));
                SubstituiVar("&(IND_NREM_IDR)&", totalNrem == 0 ? "0.0" :
                    ((ev_ap.qtd_nrem + ev_hipop.qtd_nrem + ev_rera.qtd_nrem) / (totalNrem / 3600)).ToString("0.0"));

                SubstituiVar("&(QTD_IDR_MDESP)&", totalNrem == 0 ? "0" :
                    (qtd_hipop_com_mdesp + qtd_ap_cen_com_mdesp + qtd_ap_obs_com_mdesp + qtd_ap_mis_com_mdesp + qtd_RERA_com_mdesp).ToString("0"));

                SubstituiVar("&(QTD_IDR_DESSAT)&", totalNrem == 0 ? "0" :
                    (qtd_hipop_com_dessat + qtd_ap_cen_com_dessat + qtd_ap_obs_com_dessat + qtd_ap_mis_com_dessat + qtd_RERA_com_dessat).ToString("0"));

                SubstituiVar("&(QTD_IDR_MDESP_DESSAT)&", totalNrem == 0 ? "0" :
                    (qtd_hipop_com_dessat_e_mdesp + qtd_ap_cen_com_dessat_e_mdesp + qtd_ap_obs_com_dessat_e_mdesp + qtd_ap_mis_com_dessat_e_mdesp + qtd_RERA_com_dessat_e_mdesp).ToString("0"));

                int qtdPosC_IDR = ev_ap.qtd_pos_c + ev_hipop.qtd_pos_c + ev_rera.qtd_pos_c;
                SubstituiVar("&(QTD_PC_IDR)&", qtdPosC_IDR.ToString("0"));

                if (qtdPosC_IDR == 0 || tts == 0)
                {
                    SubstituiVar("&(IND_PC_IDR)&", "0.0");
                    SubstituiVar("&(IND_PC_IDR_TTS)&", "0.0");
                }
                else
                {
                    SubstituiVar("&(IND_PC_IDR)&", (qtdPosC_IDR / (tts / 3600)).ToString("0.0"));
                    double pos_c = Convert.ToDouble(tblResumoExame["pos_c"]);
                    if (pos_c > 0)
                        SubstituiVar("&(IND_PC_IDR_TTS)&", (qtdPosC_IDR / (pos_c / 3600)).ToString("0.0"));
                }
            }
            catch (Exception ex)
            {
                // Tratamento de erro opcional
                Console.WriteLine("Erro ao calcular RERA/IDR: " + ex.Message);
            }
        }
        public static void s_dados_Despertares_Ronco_PLM(OleDbConnection cnn_dbExame)
        {
            int qtd = GetQtdEvento(cnn_dbExame, 7);
            int qtd2 = GetQtdEvento(cnn_dbExame, 8);
            if (passagem == ultimapassagem)
            {
                double tts = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);

                if (tts == 0)
                {
                    SubstituiVar("&(QH_PLM_INT)&", "0.0");
                    SubstituiVar("&(QTD_MDESP_INT)&", "0.0");
                }
                else
                {
                    SubstituiVar("&(QH_PLM_INT)&", (GetQtdEvento(cnn_dbExame,12) / (tts / 3600)).ToString("0.0"));
                    SubstituiVar("&(QTD_MDESP_INT)&", GetQtdEvento(cnn_dbExame,8).ToString("0.0"));
                }
            }
            else
            {
                int total = qtd + qtd2;

                if (total == 0)
                {
                    texto2 = " " + f_var("Var56160") + " " + f_var("Var58024");
                }
                else if (total == 1)
                {
                    if (qtd == 1)
                        texto2 = f_var("Var56160") + " " + f_var("Var58035");
                    else
                        texto2 = f_var("Var56160") + " " + f_var("Var58034");
                }
                else
                {
                    if (qtd > 1)
                        texto2 = " " + f_var("Var58032") + " " + qtd.ToString() + " " + f_var("Var58008") + " ";
                    else
                        texto2 = " " + f_var("Var58033") + " " + qtd.ToString() + " " + f_var("Var35008") + " ";

                    if (qtd2 > 1)
                        texto2 += f_var("Var56160") + " " + qtd2.ToString() + " " + f_var("Var58009");
                    else
                        texto2 += f_var("Var56160") + " " + qtd2.ToString() + " " + f_var("Var58007");
                }

                SubstituiVar("&(RESUMO_DESP)&", texto2);
                SubstituiVar("&(TEMPO_RONCO)&", FormataTempoMin(tempo_ronco));

                double tts = Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);
                double qtd_desp = GlobVar.tbl_ResumoExame.Rows[0]["qtd_desp"] == DBNull.Value ? 0 : Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["qtd_desp"]);

                SubstituiVar("&(PORC_TEMPO_RONCO)&", tts == 0 ? "0.0" : (tempo_ronco * 100 / tts).ToString("0.0"));
                SubstituiVar("&(QTD_DESP)&", qtd_desp.ToString("0"));

                int qtd_mdesp = GetQtdEvento(cnn_dbExame,8);
                SubstituiVar("&(QTD_MDESP)&", qtd_mdesp.ToString());

                int qtd_idr_sem_mdesp = qtd_mdesp - qtd_hipop_com_mdesp - qtd_ap_cen_com_mdesp - qtd_ap_obs_com_mdesp - qtd_ap_mis_com_mdesp - qtd_RERA_com_mdesp;
                SubstituiVar("&(QTD_IDR_SEM_MDESP)&", qtd_idr_sem_mdesp.ToString());

                SubstituiVar("&(IND_MDESP)&", tts == 0 ? "0.0" : (qtd_mdesp / (tts / 3600)).ToString("0.0"));
                SubstituiVar("&(IND_DESP)&", tts == 0 ? "0.0" : (qtd_desp / (tts / 3600)).ToString("0.0"));

                int qtd_movperna = GetQtdEvento(cnn_dbExame,22);
                SubstituiVar("&(QTD_MOVPERNA)&", qtd_movperna.ToString());

                double qh_movperna = tts == 0 ? 0.0 : (qtd_movperna / (tts / 3600));
                SubstituiVar("&(QH_MOVPERNA)&", qh_movperna.ToString("0.0"));
                SubstituiVar("&(IND_MOVPERNA)&", qh_movperna.ToString("0.0"));

                int qtd_plm = GetQtdEvento(cnn_dbExame,12);
                SubstituiVar("&(QTD_PLM)&", qtd_plm.ToString());

                double qh_plm = tts == 0 ? 0.0 : (qtd_plm / (tts / 3600));
                SubstituiVar("&(QH_PLM)&", qh_plm.ToString("0.0"));
                SubstituiVar("&(IND_PLM)&", qh_plm.ToString("0.0"));

                int qtd_ronco = GetQtdEvento(cnn_dbExame,13);
                double qh_ronco = tts == 0 ? 0.0 : (qtd_ronco / (tts / 3600));
                SubstituiVar("&(QH_RONCO)&", qh_ronco.ToString("0.0"));
                SubstituiVar("&(QTD_RONCO)&", qtd_ronco.ToString());
            }

        }
        private static int GetQtdEvento(OleDbConnection cnn_dbExame, int codEvento)
        {
            int resultado = 0;

            // Supondo que pag_noite e pag_dia são inteiros já definidos em GlobVar
            int pagNoite = Canais.Get_BoaNoite();
            int pagDia = Canais.Get_BomDia();

            string sql = $@"
                SELECT CodEvento, 
                       COUNT(CodEvento) AS Qtd_Evento, 
                       SUM(Duracao) AS Dur_Total, 
                       MAX(Duracao) AS Maior_Dur 
                FROM Cons_EventosComEstag 
                WHERE Estagio > 0 
                  AND Pag_Ini >= {pagNoite} 
                  AND Pag_Ini <= {pagDia} 
                  AND CodEvento = {codEvento}
                GROUP BY CodEvento";

            DataTable tblResumoEventos = ExecutaSQL(cnn_dbExame, sql); // Retorna um DataTable

            if (tblResumoEventos.Rows.Count > 0)
            {
                resultado = Convert.ToInt32(tblResumoEventos.Rows[0]["Qtd_Evento"]);
            }

            return resultado;
        }

        public static void s_dados_Estagios(OleDbConnection cnn_dbExame)
        {
            if (passagem == ultimapassagem)
            {
                var rowResumo = GlobVar.tbl_ResumoExame.Rows[0];

                double tts = Convert.ToDouble(rowResumo["TTS"]);

                SubstituiVar("&(TTS_EST_34_INT)&", tts == 0
                    ? "0.0"
                    : ((Convert.ToDouble(rowResumo["Est_3"]) + Convert.ToDouble(rowResumo["Est_4"])) * 100 / tts).ToString("0.0"));

                SubstituiVar("&(TTS_EST_5_INT)&", tts == 0
                    ? "0.0"
                    : (Convert.ToDouble(rowResumo["Est_5"]) * 100 / tts).ToString("0.0"));

                SubstituiVar("&(TTS_EST_4_INT)&", tts == 0
                    ? "0.0"
                    : (Convert.ToDouble(rowResumo["Est_4"]) * 100 / tts).ToString("0.0"));

                SubstituiVar("&(TTS_EST_3_INT)&", tts == 0
                    ? "0.0"
                    : (Convert.ToDouble(rowResumo["Est_3"]) * 100 / tts).ToString("0.0"));

                SubstituiVar("&(TTS_EST_2_INT)&", tts == 0
                    ? "0.0"
                    : (Convert.ToDouble(rowResumo["Est_2"]) * 100 / tts).ToString("0.0"));

                SubstituiVar("&(TTS_EST_1_INT)&", tts == 0
                    ? "0.0"
                    : (Convert.ToDouble(rowResumo["Est_1"]) * 100 / tts).ToString("0.0"));

                // Tempo total em ESTÁGIO 0
                SubstituiVar("&(EST_0_MIN_INT)&", FormataTempoMin(Convert.ToInt32(rowResumo["Est_0"])));

                // TTS_EST_NREM
                SubstituiVar("&(TTS_EST_NREM)&", tts == 0
                    ? "0.0"
                    : ((Convert.ToDouble(rowResumo["Est_1"])
                      + Convert.ToDouble(rowResumo["Est_2"])
                      + Convert.ToDouble(rowResumo["Est_3"])
                      + Convert.ToDouble(rowResumo["Est_4"])) * 100 / tts).ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(EST_0_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_0"])));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0)
                    SubstituiVar("&(TTR_EST_0)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_0)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["EST_0"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                    SubstituiVar("&(TTS_EST_0)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_0)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["EST_0"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0"));

                // Estágio 1
                SubstituiVar("&(EST_1)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_1_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"])));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0)
                    SubstituiVar("&(TTR_EST_1)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_1)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                    SubstituiVar("&(TTS_EST_1)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_1)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E1"]) < 0)
                    SubstituiVar("&(LAT_EST_1_MIN)&", " - ");
                else
                    SubstituiVar("&(LAT_EST_1_MIN)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E1"]) * 60).ToString(@"hh\:mm\:ss"));

                // Estágio 2
                SubstituiVar("&(EST_2)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_2_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"])));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0)
                    SubstituiVar("&(TTR_EST_2)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_2)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                    SubstituiVar("&(TTS_EST_2)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_2)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E2"]) < 0)
                    SubstituiVar("&(LAT_EST_2_MIN)&", " - ");
                else
                    SubstituiVar("&(LAT_EST_2_MIN)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E2"]) * 60).ToString(@"hh\:mm\:ss"));

                // Estágio 3
                SubstituiVar("&(EST_3)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_3_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"])));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0)
                    SubstituiVar("&(TTR_EST_3)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_3)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                    SubstituiVar("&(TTS_EST_3)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_3)&", ((Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]) * 100) / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0"));

                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E3"]) < 0)
                    SubstituiVar("&(LAT_EST_3_MIN)&", " - ");
                else
                    SubstituiVar("&(LAT_EST_3_MIN)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E3"]) * 60).ToString(@"hh\:mm\:ss"));

                // Estágio 4
                if (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E4"]) < 0)
                    SubstituiVar("&(LAT_EST_4_MIN)&", " - ");
                else
                    SubstituiVar("&(LAT_EST_4_MIN)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E4"]) * 60).ToString(@"hh\:mm\:ss"));

                // "LAT_EST_34_MIN"
                int latE3 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E3"]);
                int latE4 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_E4"]);

                if (latE3 < 0 && latE4 < 0)
                    SubstituiVar("&(LAT_EST_34_MIN)&", " - ");
                else if (latE3 < 0)
                    SubstituiVar("&(LAT_EST_34_MIN)&", FormataTempoMin(latE4));
                else if (latE4 < 0)
                    SubstituiVar("&(LAT_EST_34_MIN)&", FormataTempoMin(latE3));
                else if (latE3 < latE4)
                    SubstituiVar("&(LAT_EST_34_MIN)&", FormataTempoMin(latE3));
                else
                    SubstituiVar("&(LAT_EST_34_MIN)&", FormataTempoMin(latE4));

                // "EST_4"
                int est4 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]);
                SubstituiVar("&(EST_4)&", TimeSpan.FromSeconds(est4).ToString(@"hh\:mm\:ss"));

                // "EST_4_MIN"
                SubstituiVar("&(EST_4_MIN)&", FormataTempoMin(est4));

                // "TTR_EST_4"
                int ttr = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]);
                if (ttr == 0)
                    SubstituiVar("&(TTR_EST_4)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_4)&", (est4 * 100 / ttr).ToString("0.0"));

                // "TTS_EST_4"
                int tts = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]);
                if (tts == 0)
                    SubstituiVar("&(TTS_EST_4)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_4)&", (est4 * 100 / tts).ToString("0.0"));

                // "LAT_EST_4_MIN"
                if (latE4 < 0)
                    SubstituiVar("&(LAT_EST_4_MIN)&", " - ");
                else
                    SubstituiVar("&(LAT_EST_4_MIN)&", TimeSpan.FromSeconds(latE4 * 60).ToString(@"hh\:mm\:ss"));

                // "EST_5"
                int est5 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_5"]);
                SubstituiVar("&(EST_5)&", TimeSpan.FromSeconds(est5).ToString(@"hh\:mm\:ss"));

                // "EST_5_MIN"
                SubstituiVar("&(EST_5_MIN)&", FormataTempoMin(est5));

                // "TTR_EST_5"
                if (ttr == 0)
                    SubstituiVar("&(TTR_EST_5)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_5)&", (est5 * 100 / ttr).ToString("0.0"));

                // "TTS_EST_5"
                if (tts == 0)
                    SubstituiVar("&(TTS_EST_5)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_5)&", (est5 * 100 / tts).ToString("0.0"));

                // "LAT_EST_5_MIN"
                DateTime latREM = DBNull.Value.Equals(GlobVar.tbl_ResumoExame.Rows[0]["Lat_SonoREM"])
                    ? DateTime.MinValue
                    : Convert.ToDateTime(GlobVar.tbl_ResumoExame.Rows[0]["Lat_SonoREM"]); if (latREM == DateTime.MinValue)
                    SubstituiVar("&(LAT_EST_5_MIN)&", " - ");
                else
                    SubstituiVar("&(LAT_EST_5_MIN)&", latREM.ToString("hh:mm:ss"));

                // "EST_6"
                int est6 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_6"]);
                SubstituiVar("&(EST_6)&", TimeSpan.FromSeconds(est6).ToString(@"hh\:mm\:ss"));

                // "EST_6_MIN"
                SubstituiVar("&(EST_6_MIN)&", FormataTempoMin(est6));

                // "TTR_EST_6"
                if (ttr == 0)
                    SubstituiVar("&(TTR_EST_6)&", "0.0");
                else
                    SubstituiVar("&(TTR_EST_6)&", (est6 * 100 / ttr).ToString("0.0"));

                // "EST_NREM"
                int est1 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_1"]);
                int est2 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_2"]);
                int est3 = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]);
                int estNREM = est1 + est2 + est3;
                SubstituiVar("&(EST_NREM)&", FormataTempoMin(estNREM));

                // "TTS_EST_NREM"
                int est4Total = estNREM + est4;
                if (tts == 0)
                    SubstituiVar("&(TTS_EST_NREM)&", "0.0");
                else
                    SubstituiVar("&(TTS_EST_NREM)&", (est4Total * 100 / tts).ToString("0.0"));

                //================================================

                // EST_7 (C)
                SubstituiVar("&(EST_7)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_7"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_7_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_7"])));
                SubstituiVar("&(TTR_EST_7)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_7"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0")));
                SubstituiVar("&(TTS_EST_7)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_7"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0")));

                // EST_8 (A)
                SubstituiVar("&(EST_8)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_8"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_8_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_8"])));
                SubstituiVar("&(TTR_EST_8)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_8"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0")));
                SubstituiVar("&(TTS_EST_8)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_8"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0")));

                // EST_9 (I)
                SubstituiVar("&(EST_9)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_9"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_9_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_9"])));
                SubstituiVar("&(TTR_EST_9)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_9"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0")));
                SubstituiVar("&(TTS_EST_9)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_9"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0")));

                // QTD_MT
                SubstituiVar("&(QTD_MT)&", (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_6"]) / 30).ToString("0")));

                // EST_3 e EST_4
                SubstituiVar("&(EST_3)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(EST_4)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"])).ToString(@"hh\:mm\:ss"));

                // TTR_EST_3 e TTR_EST_4
                SubstituiVar("&(TTR_EST_3)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]) * 100 / Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0")));
                SubstituiVar("&(TTR_EST_4)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0")));

                // TTS_EST_3 e TTS_EST_34
                if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0)
                {
                    SubstituiVar("&(TTS_EST_3)&", "0.0");
                    SubstituiVar("&(TTS_EST_34)&", "0.0");
                }
                else
                {
                    string valorTTS3 = (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0"));
                    SubstituiVar("&(TTS_EST_3)&", valorTTS3);
                    SubstituiVar("&(TTS_EST_34)&", valorTTS3);
                }

                // EST_34
                est3 =Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_3"]);
                est4 =Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]);
                SubstituiVar("&(EST_34)&", TimeSpan.FromSeconds(est3 + est4).ToString(@"hh\:mm\:ss"));

                // TTS_EST_4
                SubstituiVar("&(TTS_EST_4)&",Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0
                    ? "0.0"
                    : (Convert.ToDouble(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_4"]) * 100 /Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString("0.0")));

            }
        }
        public static void S_PreparaEventosPosicao(OleDbConnection cnn_dbExame)
        {
            string sql = "";
            DataTable rs = new DataTable();
            double CalculoC, CalculoB, CalculoD, CalculoE, CalculoT;

            double[] AC_C_Q = new double[9], AC_B_Q = new double[9], AC_D_Q = new double[9], AC_E_Q = new double[9], AC_C_T = new double[9], AC_B_T = new double[9], AC_D_T = new double[9], AC_E_T = new double[9];

            double[] AO_C_Q = new double[9], AO_B_Q = new double[9], AO_D_Q = new double[9], AO_E_Q = new double[9], AO_C_T = new double[9], AO_B_T = new double[9], AO_D_T = new double[9], AO_E_T = new double[9];

            double[] AM_C_Q = new double[9], AM_B_Q = new double[9], AM_D_Q = new double[9], AM_E_Q = new double[9], AM_C_T = new double[9], AM_B_T = new double[9], AM_D_T = new double[9], AM_E_T = new double[9];

            double[] HP_C_Q = new double[9], HP_B_Q = new double[9], HP_D_Q = new double[9], HP_E_Q = new double[9], HP_C_T = new double[9], HP_B_T = new double[9], HP_D_T = new double[9], HP_E_T = new double[9];

            double[] RE_C_Q = new double[9], RE_B_Q = new double[9], RE_D_Q = new double[9], RE_E_Q = new double[9], RE_C_T = new double[9], RE_B_T = new double[9], RE_D_T = new double[9], RE_E_T = new double[9];

            int pos_c = 0, pos_b = 0, pos_d = 0, pos_e = 0;

            int pos_c_r = 0, pos_b_r = 0, pos_d_r = 0, pos_e_r = 0;

            int pos_c_n = 0 , pos_b_n = 0, pos_d_n = 0, pos_e_n = 0;

            double calculo = 0;

            int pag_dia = Canais.Get_BomDia();
            int pag_noite = Canais.Get_BoaNoite();

            sql = "SELECT * FROM tbl_PosEstagio";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if(rs != null)
            {
                foreach(DataRow rw in rs.Rows)
                {
                    if (Convert.ToInt32(rw["Estagio"]) == 5)
                    {
                        pos_c_r = Convert.ToInt32(rw["Cima"]);
                        pos_b_r = Convert.ToInt32(rw["Baixo"]);
                        pos_d_r = Convert.ToInt32(rw["Direita"]);
                        pos_e_r = Convert.ToInt32(rw["Esquerda"]);
                    }
                    else
                    {
                        pos_c_n += Convert.ToInt32(rw["Cima"]);
                        pos_b_n += Convert.ToInt32(rw["Baixo"]);
                        pos_d_n += Convert.ToInt32(rw["Direita"]);
                        pos_e_n += Convert.ToInt32(rw["Esquerda"]);

                    }
                }
            }
            //'Apneia Central

            sql = "SELECT * FROM Cons_Eventos_ApCen WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ")";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if(rs != null)
            {
                foreach (DataRow rw in rs.Rows)
                {
                    if (rw["Posicao"].ToString().Equals("C"))
                    {
                        AC_C_Q[Convert.ToInt32(rw["Estagio"])] = AC_C_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AC_C_T[Convert.ToInt32(rw["Estagio"])] = AC_C_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("B"))
                    {
                        AC_B_Q[Convert.ToInt32(rw["Estagio"])] = AC_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AC_B_T[Convert.ToInt32(rw["Estagio"])] = AC_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);

                    }
                    else if (rw["Posicao"].ToString().Equals("D"))
                    {
                        AC_D_Q[Convert.ToInt32(rw["Estagio"])] = AC_D_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AC_D_T[Convert.ToInt32(rw["Estagio"])] = AC_D_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);

                    }
                    else if (rw["Posicao"].ToString().Equals("E"))
                    {
                        AC_E_Q[Convert.ToInt32(rw["Estagio"])] = AC_E_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AC_E_T[Convert.ToInt32(rw["Estagio"])] = AC_E_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);

                    }
                    else if (rw["Posicao"].ToString().Equals("."))
                    {
                        AC_B_Q[Convert.ToInt32(rw["Estagio"])] = AC_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AC_B_T[Convert.ToInt32(rw["Estagio"])] = AC_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);

                    }
                }
            }

            //'Apneia Obstrutiva
            sql = "SELECT * FROM Cons_Eventos_ApObs WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ")";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if(rs != null)
            {
                foreach(DataRow rw in rs.Rows)
                {
                    int estagio = Convert.ToInt32(rw["Estagio"]);
                    int duracao = Convert.ToInt32(rw["Duracao"]);

                    if (rw["Posicao"].ToString().Equals("C"))
                    {
                        AO_C_Q[estagio] = AO_C_Q[estagio] + 1;
                        AO_C_T[estagio] = AO_C_T[estagio] + duracao;
                    }
                    else if (rw["Posicao"].ToString().Equals("B"))
                    {
                        AO_B_Q[Convert.ToInt32(rw["Estagio"])] = AO_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AO_B_T[Convert.ToInt32(rw["Estagio"])] = AO_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("D"))
                    {
                        AO_D_Q[Convert.ToInt32(rw["Estagio"])] = AO_D_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AO_D_T[Convert.ToInt32(rw["Estagio"])] = AO_D_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("E"))
                    {
                        AO_E_Q[Convert.ToInt32(rw["Estagio"])] = AO_E_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AO_E_T[Convert.ToInt32(rw["Estagio"])] = AO_E_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("."))
                    {
                        AO_B_Q[Convert.ToInt32(rw["Estagio"])] = AO_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AO_B_T[Convert.ToInt32(rw["Estagio"])] = AO_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                }
            }

            //'Apneia Mista
            sql = "SELECT * FROM Cons_Eventos_ApMis WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ")";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if (rs != null)
            {
                foreach (DataRow rw in rs.Rows)
                {
                    if (rw["Posicao"].ToString().Equals("C"))
                    {
                        AM_C_Q[Convert.ToInt32(rw["Estagio"])] = AM_C_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AM_C_T[Convert.ToInt32(rw["Estagio"])] = AM_C_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("B"))
                    {
                        AM_B_Q[Convert.ToInt32(rw["Estagio"])] = AM_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AM_B_T[Convert.ToInt32(rw["Estagio"])] = AM_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("D"))
                    {
                        AM_D_Q[Convert.ToInt32(rw["Estagio"])] = AM_D_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AM_D_T[Convert.ToInt32(rw["Estagio"])] = AM_D_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("E"))
                    {
                        AM_E_Q[Convert.ToInt32(rw["Estagio"])] = AM_E_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AM_E_T[Convert.ToInt32(rw["Estagio"])] = AM_E_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("."))
                    {
                        AM_B_Q[Convert.ToInt32(rw["Estagio"])] = AM_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        AM_B_T[Convert.ToInt32(rw["Estagio"])] = AM_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                }
            }

            //''Hipopneia
            sql = "SELECT * FROM Cons_Eventos_Hipop WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ")";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if (rs != null)
            {
                foreach (DataRow rw in rs.Rows)
                {
                    if (rw["Posicao"].ToString().Equals("C"))
                    {
                        HP_C_Q[Convert.ToInt32(rw["Estagio"])] = HP_C_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        HP_C_T[Convert.ToInt32(rw["Estagio"])] = HP_C_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("B"))
                    {
                        HP_B_Q[Convert.ToInt32(rw["Estagio"])] = HP_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        HP_B_T[Convert.ToInt32(rw["Estagio"])] = HP_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("D"))
                    {
                        HP_D_Q[Convert.ToInt32(rw["Estagio"])] = HP_D_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        HP_D_T[Convert.ToInt32(rw["Estagio"])] = HP_D_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("E"))
                    {
                        HP_E_Q[Convert.ToInt32(rw["Estagio"])] = HP_E_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        HP_E_T[Convert.ToInt32(rw["Estagio"])] = HP_E_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("."))
                    {
                        HP_B_Q[Convert.ToInt32(rw["Estagio"])] = HP_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        HP_B_T[Convert.ToInt32(rw["Estagio"])] = HP_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                }
            }

            //''HERA
            sql = "SELECT * FROM Cons_Eventos_RERA WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ")";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if (rs != null)
            {
                foreach (DataRow rw in rs.Rows)
                {
                    if (rw["Posicao"].ToString().Equals("C"))
                    {
                        RE_C_Q[Convert.ToInt32(rw["Estagio"])] = RE_C_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        RE_C_T[Convert.ToInt32(rw["Estagio"])] = RE_C_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("B"))
                    {
                        RE_B_Q[Convert.ToInt32(rw["Estagio"])] = RE_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        RE_B_T[Convert.ToInt32(rw["Estagio"])] = RE_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("D"))
                    {
                        RE_D_Q[Convert.ToInt32(rw["Estagio"])] = RE_D_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        RE_D_T[Convert.ToInt32(rw["Estagio"])] = RE_D_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("E"))
                    {
                        RE_E_Q[Convert.ToInt32(rw["Estagio"])] = RE_E_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        RE_E_T[Convert.ToInt32(rw["Estagio"])] = RE_E_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                    else if (rw["Posicao"].ToString().Equals("."))
                    {
                        RE_B_Q[Convert.ToInt32(rw["Estagio"])] = RE_B_Q[Convert.ToInt32(rw["Estagio"])] + 1;
                        RE_B_T[Convert.ToInt32(rw["Estagio"])] = RE_B_T[Convert.ToInt32(rw["Estagio"])] + Convert.ToInt32(rw["Duracao"]);
                    }
                }
            }

            pos_c = pos_c_r + pos_c_n;
            pos_b = pos_b_r + pos_b_n;
            pos_d = pos_d_r + pos_d_n;
            pos_e = pos_e_r + pos_e_n;

            //'IND_AP_NREM

            CalculoC = AC_C_Q[1] + AC_C_Q[2] + AC_C_Q[3] + AM_C_Q[1] + AM_C_Q[2] + AM_C_Q[3] + AO_C_Q[1] + AO_C_Q[2] + AO_C_Q[3];
            if (pos_c_n > 0) calculo = CalculoC / (pos_c_n / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_NREM_C)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoB = AC_B_Q[1] + AC_B_Q[2] + AC_B_Q[3] + AM_B_Q[1] + AM_B_Q[2] + AM_B_Q[3] + AO_B_Q[1] + AO_B_Q[2] + AO_B_Q[3];
            if (pos_b_n > 0) calculo = CalculoB / (pos_b_n / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_NREM_B)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoD = AC_D_Q[1] + AC_D_Q[2] + AC_D_Q[3] + AM_D_Q[1] + AM_D_Q[2] + AM_D_Q[3] + AO_D_Q[1] + AO_D_Q[2] + AO_D_Q[3];
            if (pos_d_n > 0) calculo = CalculoD / (pos_d_n / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_NREM_D)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoE = AC_E_Q[1] + AC_E_Q[2] + AC_E_Q[3] + AM_E_Q[1] + AM_E_Q[2] + AM_E_Q[3] + AO_E_Q[1] + AO_E_Q[2] + AO_E_Q[3];
            if (pos_e_n > 0) calculo = CalculoE / (pos_e_n / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_NREM_E)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;
            var Stringcalculo = ((pos_c_n + pos_b_n + pos_d_n + pos_e_n) != 0) ? (CalculoT / ((pos_c_n + pos_b_n + pos_d_n + pos_e_n) / 3600.0)).ToString("F2", new CultureInfo("pt-BR")) : "0,00";
            SubstituiVar("&(IND_AP_NREM_T)&", Stringcalculo);

            //'IND_AP_REM
            CalculoC = AC_C_Q[5] + AM_C_Q[5] + AO_C_Q[5];
            if (pos_c_r > 0) calculo = CalculoC / (pos_c_r / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_REM_C)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoB = AC_B_Q[5] + AM_B_Q[5] + AO_B_Q[5];
            if (pos_b_r > 0) calculo = CalculoB / (pos_b_r / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_REM_B)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoD = AC_D_Q[5] + AM_D_Q[5] + AO_D_Q[5];
            if (pos_d_r > 0) calculo = CalculoD / (pos_d_r / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_REM_D)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoE = AC_E_Q[5] + AM_E_Q[5] + AO_E_Q[5];
            if (pos_e_r > 0) calculo = CalculoE / (pos_e_r / 3600); else calculo = 0;
            SubstituiVar("&(IND_AP_REM_C)&", calculo.ToString("F1", new CultureInfo("pt-BR")));

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;
            Stringcalculo = ((pos_c_r + pos_b_r + pos_d_r + pos_e_r) != 0) ? (CalculoT / ((pos_c_r + pos_b_r + pos_d_r + pos_e_r) / 3600.0)).ToString("F2", new CultureInfo("pt-BR")) : "0,00";
            SubstituiVar("&(IND_AP_REM_T)&", Stringcalculo);

            //'QTDE_APNEIA_CENTRAL
            CalculoC = 0;
            for(int i = 1; i <  5; i++)
            {
                CalculoC = CalculoC + AC_C_Q[i];
            }

            CalculoB = 0;
            for (int i = 1; i < 5; i++)
            {
                CalculoB = CalculoB + AC_B_Q[i];
            }

            CalculoD = 0;
            for (int i = 1; i < 5; i++)
            {
                CalculoD = CalculoD + AC_D_Q[1];
            }

            CalculoE = 0;
            for (int i = 1; i < 5; i++)
            {
                CalculoE = CalculoE + AC_E_Q[i];
            }

            SubstituiVar("&(AC_N_C)&", CalculoC.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_N_B)&", CalculoB.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_N_D)&", CalculoD.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_N_E)&", CalculoE.ToString("F0", new CultureInfo("pt-BR")));

            SubstituiVar("&(AC_R_C)&", AC_C_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_R_B)&", AC_B_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_R_D)&", AC_D_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_R_E)&", AC_E_Q[5].ToString("F0", new CultureInfo("pt-BR")));

            CalculoC = CalculoC + AC_C_Q[5];
            CalculoB = CalculoB + AC_B_Q[5];
            CalculoD = CalculoD + AC_D_Q[5];
            CalculoE = CalculoE + AC_E_Q[5];
            SubstituiVar("&(AC_T_C)&", CalculoC.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_T_B)&", CalculoB.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_T_D)&", CalculoD.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AC_T_E)&", CalculoE.ToString("F0", new CultureInfo("pt-BR")));

            //'QTDE_APNEIA_OBSTRUTIVA
            CalculoC = 0;
            for (int i = 1; i < 5; i++) 
            {
            CalculoC = CalculoC + AO_C_Q[i];
            }

            CalculoB = 0;
            for (int i = 1; i < 5; i++)
            {
            CalculoB = CalculoB + AO_B_Q[i];
            }

            CalculoD = 0;
            for (int i = 1; i < 5; i++)
            {
            CalculoD = CalculoD + AO_D_Q[i];
            }

            CalculoE = 0;
            for (int i = 1; i < 5; i++)
            {
            CalculoE = CalculoE + AO_E_Q[i];
            }

            SubstituiVar("&(AO_N_C)&", CalculoC.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_N_B)&", CalculoB.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_N_D)&", CalculoD.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_N_E)&", CalculoE.ToString("F0", new CultureInfo("pt-BR")));

            SubstituiVar("&(AO_R_C)&", AO_C_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_R_B)&", AO_B_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_R_D)&", AO_D_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_R_E)&", AO_E_Q[5].ToString("F0", new CultureInfo("pt-BR")));

            CalculoC = CalculoC + AO_C_Q[5];
            CalculoB = CalculoB + AO_B_Q[5];
            CalculoD = CalculoD + AO_D_Q[5];
            CalculoE = CalculoE + AO_E_Q[5];
            SubstituiVar("&(AO_T_C)&", CalculoC.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_T_B)&", CalculoB.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_T_D)&", CalculoD.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AO_T_E)&", CalculoE.ToString("F0", new CultureInfo("pt-BR")));

            // 'QTDE_APNEIA_MISTA

            CalculoC = 0;
            for (int i = 1; i < 5; i++) {CalculoC = CalculoC + AM_C_Q[i]; }
            
            CalculoB = 0;
            for (int i = 1; i < 5; i++) {CalculoB = CalculoB + AM_B_Q[i]; }
            
            CalculoD = 0;
            for (int i = 1; i < 5; i++) {CalculoD = CalculoD + AM_D_Q[i]; }
            
            CalculoE = 0;
            for (int i = 1; i < 5; i++) {CalculoE = CalculoE + AM_E_Q[i]; }                

            SubstituiVar("&(AM_N_C)&", CalculoC.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_N_B)&", CalculoB.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_N_D)&", CalculoD.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_N_E)&", CalculoE.ToString("F0", new CultureInfo("pt-BR")));
   
            SubstituiVar("&(AM_R_C)&", AM_C_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_R_B)&", AM_B_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_R_D)&", AM_D_Q[5].ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_R_E)&", AM_E_Q[5].ToString("F0", new CultureInfo("pt-BR")));

            CalculoC = CalculoC + AM_C_Q[5];
            CalculoB = CalculoB + AM_B_Q[5];
            CalculoD = CalculoD + AM_D_Q[5];
            CalculoE = CalculoE + AM_E_Q[5];
            SubstituiVar("&(AM_T_C)&", CalculoC.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_T_B)&", CalculoB.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_T_D)&", CalculoD.ToString("F0", new CultureInfo("pt-BR")));
            SubstituiVar("&(AM_T_E)&", CalculoE.ToString("F0", new CultureInfo("pt-BR")));

            //  'IND_AP_TOTAL

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;

            SubstituiVar("&(IND_AP_TOTAL_C)&", (pos_c > 0 ? (CalculoC / (pos_c / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_AP_TOTAL_B)&", (pos_b > 0 ? (CalculoB / (pos_b / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_AP_TOTAL_D)&", (pos_d > 0 ? (CalculoD / (pos_d / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_AP_TOTAL_E)&", (pos_e > 0 ? (CalculoE / (pos_e / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_AP_TOTAL_T)&", ((pos_c + pos_b + pos_d + pos_e) > 0 ? (CalculoT / ((pos_c + pos_b + pos_d + pos_e) / 3600)).ToString("0.0") : "0,00"));

            //'IND_HIP_NREM
            CalculoC = HP_C_Q[1] + HP_C_Q[2] + HP_C_Q[3];
            SubstituiVar("&(IND_HIP_NREM_C)&", (pos_c_n > 0 ? (CalculoC / (pos_c_n / 3600)).ToString("0.0") : "0,00"));

            CalculoB = HP_B_Q[1] + HP_B_Q[2] + HP_B_Q[3];
            SubstituiVar("&(IND_HIP_NREM_B)&", (pos_b_n > 0 ? (CalculoB / (pos_b_n / 3600)).ToString("0.0") : "0,00"));

            CalculoD = HP_D_Q[1] + HP_D_Q[2] + HP_D_Q[3];
            SubstituiVar("&(IND_HIP_NREM_D)&", (pos_d_n > 0 ? (CalculoD / (pos_d_n / 3600)).ToString("0.0") : "0,00"));

            CalculoE = HP_E_Q[1] + HP_E_Q[2] + HP_E_Q[3];
            SubstituiVar("&(IND_HIP_NREM_E)&", (pos_e_n > 0 ? (CalculoE / (pos_e_n / 3600)).ToString("0.0") : "0,00"));

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;
            SubstituiVar("&(IND_HIP_NREM_T)&", ((pos_c_n + pos_b_n + pos_d_n + pos_e_n) > 0 ? (CalculoT / ((pos_c_n + pos_b_n + pos_d_n + pos_e_n) / 3600)).ToString("0.0") : "0,00"));

            // 'IND_HIP_REM
            CalculoT = HP_C_Q[5] + HP_B_Q[5] + HP_D_Q[5] + HP_E_Q[5];
            SubstituiVar("&(IND_HIP_REM_C)&", (pos_c_r > 0 ? (HP_C_Q[5] / (pos_c_r / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_HIP_REM_B)&", (pos_b_r > 0 ? (HP_B_Q[5] / (pos_b_r / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_HIP_REM_D)&", (pos_d_r > 0 ? (HP_D_Q[5] / (pos_d_r / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_HIP_REM_E)&", (pos_e_r > 0 ? (HP_E_Q[5] / (pos_e_r / 3600)).ToString("0.0") : "0,00"));
            SubstituiVar("&(IND_HIP_REM_T)&", ((pos_c_r + pos_b_r + pos_d_r + pos_e_r) > 0 ? (CalculoT / ((pos_c_r + pos_b_r + pos_d_r + pos_e_r) / 3600)).ToString("0.0") : "0,00"));

            // 'IND_HIP_TOTAL
            CalculoC = HP_C_Q[1] + HP_C_Q[2] + HP_C_Q[3] + HP_C_Q[4];
            CalculoB = HP_B_Q[1] + HP_B_Q[2] + HP_B_Q[3] + HP_B_Q[4];
            CalculoD = HP_D_Q[1] + HP_D_Q[2] + HP_D_Q[3] + HP_D_Q[4];
            CalculoE = HP_E_Q[1] + HP_E_Q[2] + HP_E_Q[3] + HP_E_Q[4];

            SubstituiVar("&(HP_N_C)&", CalculoC.ToString("0"));
            SubstituiVar("&(HP_N_B)&", CalculoB.ToString("0"));
            SubstituiVar("&(HP_N_D)&", CalculoD.ToString("0"));
            SubstituiVar("&(HP_N_E)&", CalculoE.ToString("0"));

            SubstituiVar("&(HP_R_C)&", HP_C_Q[5].ToString("0"));
            SubstituiVar("&(HP_R_B)&", HP_B_Q[5].ToString("0"));
            SubstituiVar("&(HP_R_D)&", HP_D_Q[5].ToString("0"));
            SubstituiVar("&(HP_R_E)&", HP_E_Q[5].ToString("0"));

            CalculoC += HP_C_Q[5];
            CalculoB += HP_B_Q[5];
            CalculoD += HP_D_Q[5];
            CalculoE += HP_E_Q[5];

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;

            calculo = pos_c > 0 ? CalculoC / (pos_c / 3600.0) : 0;
            SubstituiVar("&(IND_HIP_TOTAL_C)&", calculo.ToString("0.0"));

            calculo = pos_b > 0 ? CalculoB / (pos_b / 3600.0) : 0;
            SubstituiVar("&(IND_HIP_TOTAL_B)&", calculo.ToString("0.0"));

            calculo = pos_d > 0 ? CalculoD / (pos_d / 3600.0) : 0;
            SubstituiVar("&(IND_HIP_TOTAL_D)&", calculo.ToString("0.0"));

            calculo = pos_e > 0 ? CalculoE / (pos_e / 3600.0) : 0;
            SubstituiVar("&(IND_HIP_TOTAL_E)&", calculo.ToString("0.0"));

            calculo = (pos_c + pos_b + pos_d + pos_e) > 0 ? CalculoT / ((pos_c + pos_b + pos_d + pos_e) / 3600.0) : 0;
            SubstituiVar("&(IND_HIP_TOTAL_T)&", calculo.ToString("0.0"));

            SubstituiVar("&(HP_T_C)&", CalculoC.ToString("0"));
            SubstituiVar("&(HP_T_B)&", CalculoB.ToString("0"));
            SubstituiVar("&(HP_T_D)&", CalculoD.ToString("0"));
            SubstituiVar("&(HP_T_E)&", CalculoE.ToString("0"));

            //'IND_RERA_TOTAL
            CalculoC = RE_C_Q[1] + RE_C_Q[2] + RE_C_Q[3] + RE_C_Q[4];
            CalculoB = RE_B_Q[1] + RE_B_Q[2] + RE_B_Q[3] + RE_B_Q[4];
            CalculoD = RE_D_Q[1] + RE_D_Q[2] + RE_D_Q[3] + RE_D_Q[4];
            CalculoE = RE_E_Q[1] + RE_E_Q[2] + RE_E_Q[3] + RE_E_Q[4];

            SubstituiVar("&(RE_N_C)&", CalculoC.ToString("0"));
            SubstituiVar("&(RE_N_B)&", CalculoB.ToString("0"));
            SubstituiVar("&(RE_N_D)&", CalculoD.ToString("0"));
            SubstituiVar("&(RE_N_E)&", CalculoE.ToString("0"));

            SubstituiVar("&(RE_R_C)&", RE_C_Q[5].ToString("0"));
            SubstituiVar("&(RE_R_B)&", RE_B_Q[5].ToString("0"));
            SubstituiVar("&(RE_R_D)&", RE_D_Q[5].ToString("0"));
            SubstituiVar("&(RE_R_E)&", RE_E_Q[5].ToString("0"));

            CalculoC += RE_C_Q[5];
            CalculoB += RE_B_Q[5];
            CalculoD += RE_D_Q[5];
            CalculoE += RE_E_Q[5];

            SubstituiVar("&(RE_T_C)&", CalculoC.ToString("0"));
            SubstituiVar("&(RE_T_B)&", CalculoB.ToString("0"));
            SubstituiVar("&(RE_T_D)&", CalculoD.ToString("0"));
            SubstituiVar("&(RE_T_E)&", CalculoE.ToString("0"));

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;

            calculo = pos_c > 0 ? CalculoC / (pos_c / 3600.0) : 0;
            SubstituiVar("&(IND_RERA_TOTAL_C)&", calculo.ToString("0.0"));

            calculo = pos_b > 0 ? CalculoB / (pos_b / 3600.0) : 0;
            SubstituiVar("&(IND_RERA_TOTAL_B)&", calculo.ToString("0.0"));

            calculo = pos_d > 0 ? CalculoD / (pos_d / 3600.0) : 0;
            SubstituiVar("&(IND_RERA_TOTAL_D)&", calculo.ToString("0.0"));

            calculo = pos_e > 0 ? CalculoE / (pos_e / 3600.0) : 0;
            SubstituiVar("&(IND_RERA_TOTAL_E)&", calculo.ToString("0.0"));

            calculo = (pos_c + pos_b + pos_d + pos_e) > 0
                ? CalculoT / ((pos_c + pos_b + pos_d + pos_e) / 3600.0)
                : 0;
            SubstituiVar("&(IND_RERA_TOTAL_T)&", calculo.ToString("0.0"));

            //IND_AP_HIP_NREM
            CalculoC = 0; CalculoB = 0; CalculoD = 0; CalculoE = 0; CalculoT = 0; calculo = 0;

            for (int i = 1; i <= 3; i++)
            {
                CalculoC += AC_C_Q[i] + AM_C_Q[i] + AO_C_Q[i] + HP_C_Q[i];
                CalculoB += AC_B_Q[i] + AM_B_Q[i] + AO_B_Q[i] + HP_B_Q[i];
                CalculoD += AC_D_Q[i] + AM_D_Q[i] + AO_D_Q[i] + HP_D_Q[i];
                CalculoE += AC_E_Q[i] + AM_E_Q[i] + AO_E_Q[i] + HP_E_Q[i];
            }

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;

            calculo = pos_c_n > 0 ? CalculoC / (pos_c_n / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_NREM_C)&", calculo.ToString("0.0"));

            calculo = pos_b_n > 0 ? CalculoB / (pos_b_n / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_NREM_B)&", calculo.ToString("0.0"));

            calculo = pos_d_n > 0 ? CalculoD / (pos_d_n / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_NREM_D)&", calculo.ToString("0.0"));

            calculo = pos_e_n > 0 ? CalculoE / (pos_e_n / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_NREM_E)&", calculo.ToString("0.0"));

            calculo = (pos_c_n + pos_b_n + pos_d_n + pos_e_n) > 0 ? CalculoT / ((pos_c_n + pos_b_n + pos_d_n + pos_e_n) / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_NREM_T)&", calculo.ToString("0.0"));

            // IND_AP_HIP_REM
            CalculoC = AC_C_Q[5] + AM_C_Q[5] + AO_C_Q[5] + HP_C_Q[5];
            calculo = pos_c_r > 0 ? CalculoC / (pos_c_r / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_REM_C)&", calculo.ToString("0.0"));

            CalculoB = AC_B_Q[5] + AM_B_Q[5] + AO_B_Q[5] + HP_B_Q[5];
            calculo = pos_b_r > 0 ? CalculoB / (pos_b_r / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_REM_B)&", calculo.ToString("0.0"));

            CalculoD = AC_D_Q[5] + AM_D_Q[5] + AO_D_Q[5] + HP_D_Q[5];
            calculo = pos_d_r > 0 ? CalculoD / (pos_d_r / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_REM_D)&", calculo.ToString("0.0"));

            CalculoE = AC_E_Q[5] + AM_E_Q[5] + AO_E_Q[5] + HP_E_Q[5];
            calculo = pos_e_r > 0 ? CalculoE / (pos_e_r / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_REM_E)&", calculo.ToString("0.0"));

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;
            calculo = (pos_c_r + pos_b_r + pos_d_r + pos_e_r) > 0 ? CalculoT / ((pos_c_r + pos_b_r + pos_d_r + pos_e_r) / 3600.0) : 0;
            SubstituiVar("&(IND_AP_HIP_REM_T)&", calculo.ToString("0.0"));

            //'IND_AP_HIP_TOTAL

            CalculoC = 0; CalculoB = 0; CalculoD = 0; CalculoE = 0; CalculoT = 0;
            calculo = 0;

            // Soma por posição para os cinco tipos de eventos (índices totais)
            for (int i = 1; i <= 5; i++)
            {
                CalculoC += AC_C_Q[i] + AO_C_Q[i] + AM_C_Q[i] + HP_C_Q[i];
                CalculoB += AC_B_Q[i] + AO_B_Q[i] + AM_B_Q[i] + HP_B_Q[i];
                CalculoD += AC_D_Q[i] + AO_D_Q[i] + AM_D_Q[i] + HP_D_Q[i];
                CalculoE += AC_E_Q[i] + AO_E_Q[i] + AM_E_Q[i] + HP_E_Q[i];
            }
            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;

            // Substituição dos índices por posição
            calculo = pos_c > 0 ? CalculoC / (pos_c / 3600.0) : 0.0;
            SubstituiVar("&(IND_AP_HIP_TOTAL_C)&", calculo.ToString("0.0"));

            calculo = pos_b > 0 ? CalculoB / (pos_b / 3600.0) : 0.0;
            SubstituiVar("&(IND_AP_HIP_TOTAL_B)&", calculo.ToString("0.0"));

            calculo = pos_d > 0 ? CalculoD / (pos_d / 3600.0) : 0.0;
            SubstituiVar("&(IND_AP_HIP_TOTAL_D)&", calculo.ToString("0.0"));

            calculo = pos_e > 0 ? CalculoE / (pos_e / 3600.0) : 0.0;
            SubstituiVar("&(IND_AP_HIP_TOTAL_E)&", calculo.ToString("0.0"));

            calculo = (pos_c + pos_b + pos_d + pos_e) > 0 ? CalculoT / ((pos_c + pos_b + pos_d + pos_e) / 3600.0) : 0.0;
            SubstituiVar("&(IND_AP_HIP_TOTAL_T)&", calculo.ToString("0.0"));

            // Substituição da quantidade total de eventos por posição
            SubstituiVar("&(AP_HP_T_C)&", CalculoC.ToString("0"));
            SubstituiVar("&(AP_HP_T_B)&", CalculoB.ToString("0"));
            SubstituiVar("&(AP_HP_T_D)&", CalculoD.ToString("0"));
            SubstituiVar("&(AP_HP_T_E)&", CalculoE.ToString("0"));

            //'IDR - não zerei as variaveis para aproveitar parte do calculo
            for (int i = 1; i <= 5; i++)
            {
                CalculoC += RE_C_Q[i];
                CalculoB += RE_B_Q[i];
                CalculoD += RE_D_Q[i];
                CalculoE += RE_E_Q[i];
            }

            CalculoT = CalculoC + CalculoB + CalculoD + CalculoE;

            // Cálculo do índice de ronco por posição (IDR)
            calculo = pos_c > 0 ? CalculoC / (pos_c / 3600.0) : 0.0;
            SubstituiVar("&(IDR_C)&", calculo.ToString("0.0"));

            calculo = pos_b > 0 ? CalculoB / (pos_b / 3600.0) : 0.0;
            SubstituiVar("&(IDR_B)&", calculo.ToString("0.0"));

            calculo = pos_d > 0 ? CalculoD / (pos_d / 3600.0) : 0.0;
            SubstituiVar("&(IDR_D)&", calculo.ToString("0.0"));

            calculo = pos_e > 0 ? CalculoE / (pos_e / 3600.0) : 0.0;
            SubstituiVar("&(IDR_E)&", calculo.ToString("0.0"));

            calculo = (pos_c + pos_b + pos_d + pos_e) > 0 ? CalculoT / ((pos_c + pos_b + pos_d + pos_e) / 3600.0) : 0.0;
            SubstituiVar("&(IDR_T)&", calculo.ToString("0.0"));

            sql = "SELECT * FROM tbl_PosEstagio";
            rs = ExecutaSQL(cnn_dbExame, sql);

            if (rs.Rows.Count > 0)
            {
                CalculoC = 0; CalculoB = 0; CalculoD = 0; CalculoE = 0;

                // NREM Tempo (estágios 1, 2, 3)
                foreach (DataRow row in rs.Rows)
                {
                    int estagio = Convert.ToInt32(row["estagio"]);
                    if (estagio == 1 || estagio == 2 || estagio == 3)
                    {
                        CalculoC += Convert.ToDouble(row["Cima"]);
                        CalculoB += Convert.ToDouble(row["Baixo"]);
                        CalculoD += Convert.ToDouble(row["Direita"]);
                        CalculoE += Convert.ToDouble(row["Esquerda"]);
                    }
                }

                SubstituiVar("&(NREM_C)&", (CalculoC / 60).ToString("0.0"));
                SubstituiVar("&(NREM_B)&", (CalculoB / 60).ToString("0.0"));
                SubstituiVar("&(NREM_D)&", (CalculoD / 60).ToString("0.0"));
                SubstituiVar("&(NREM_E)&", (CalculoE / 60).ToString("0.0"));
                SubstituiVar("&(NREM_T)&", ((CalculoC + CalculoB + CalculoD + CalculoE) / 60).ToString("0.0"));

                // REM Tempo
                bool encontrouREM = false;
                foreach (DataRow row in rs.Rows)
                {
                    if (Convert.ToInt32(row["estagio"]) == 5)
                    {
                        double cima = Convert.ToDouble(row["Cima"]);
                        double baixo = Convert.ToDouble(row["Baixo"]);
                        double direita = Convert.ToDouble(row["Direita"]);
                        double esquerda = Convert.ToDouble(row["Esquerda"]);
                        double totalREM = cima + baixo + direita + esquerda;

                        SubstituiVar("&(REM_C)&", (cima / 60).ToString("0.0"));
                        SubstituiVar("&(REM_B)&", (baixo / 60).ToString("0.0"));
                        SubstituiVar("&(REM_D)&", (direita / 60).ToString("0.0"));
                        SubstituiVar("&(REM_E)&", (esquerda / 60).ToString("0.0"));
                        SubstituiVar("&(REM_T)&", (totalREM / 60).ToString("0.0"));

                        CalculoC += cima;
                        CalculoB += baixo;
                        CalculoD += direita;
                        CalculoE += esquerda;

                        SubstituiVar("&(REM%_C)&", ((cima / (CalculoC + CalculoB + CalculoD + CalculoE)) * 100).ToString("0.0"));
                        SubstituiVar("&(REM%_B)&", ((baixo / (CalculoC + CalculoB + CalculoD + CalculoE)) * 100).ToString("0.0"));
                        SubstituiVar("&(REM%_D)&", ((direita / (CalculoC + CalculoB + CalculoD + CalculoE)) * 100).ToString("0.0"));
                        SubstituiVar("&(REM%_E)&", ((esquerda / (CalculoC + CalculoB + CalculoD + CalculoE)) * 100).ToString("0.0"));

                        encontrouREM = true;
                        break;
                    }
                }

                if (!encontrouREM)
                {
                    SubstituiVar("&(REM_C)&", "0.0");
                    SubstituiVar("&(REM_B)&", "0.0");
                    SubstituiVar("&(REM_D)&", "0.0");
                    SubstituiVar("&(REM_E)&", "0.0");
                    SubstituiVar("&(REM_T)&", "0.0");

                    SubstituiVar("&(REM%_C)&", "0.0");
                    SubstituiVar("&(REM%_B)&", "0.0");
                    SubstituiVar("&(REM%_D)&", "0.0");
                    SubstituiVar("&(REM%_E)&", "0.0");
                }

                // TTS Tempo
                double total_TTS = CalculoC + CalculoB + CalculoD + CalculoE;
                SubstituiVar("&(TTS_C)&", (CalculoC / 60).ToString("0.0"));
                SubstituiVar("&(TTS_B)&", (CalculoB / 60).ToString("0.0"));
                SubstituiVar("&(TTS_D)&", (CalculoD / 60).ToString("0.0"));
                SubstituiVar("&(TTS_E)&", (CalculoE / 60).ToString("0.0"));
                SubstituiVar("&(TTS_T)&", (total_TTS / 60).ToString("0.0"));

                SubstituiVar("&(TTS%_C)&", ((CalculoC / total_TTS) * 100).ToString("0.0"));
                SubstituiVar("&(TTS%_B)&", ((CalculoB / total_TTS) * 100).ToString("0.0"));
                SubstituiVar("&(TTS%_D)&", ((CalculoD / total_TTS) * 100).ToString("0.0"));
                SubstituiVar("&(TTS%_E)&", ((CalculoE / total_TTS) * 100).ToString("0.0"));

                // SOL Tempo
                bool encontrouSOL = false;
                foreach (DataRow row in rs.Rows)
                {
                    if (Convert.ToInt32(row["estagio"]) == 3)
                    {
                        SubstituiVar("&(SOL%_C)&", ((Convert.ToDouble(row["Cima"]) / total_TTS) * 100).ToString("0.0"));
                        SubstituiVar("&(SOL%_B)&", ((Convert.ToDouble(row["Baixo"]) / total_TTS) * 100).ToString("0.0"));
                        SubstituiVar("&(SOL%_D)&", ((Convert.ToDouble(row["Direita"]) / total_TTS) * 100).ToString("0.0"));
                        SubstituiVar("&(SOL%_E)&", ((Convert.ToDouble(row["Esquerda"]) / total_TTS) * 100).ToString("0.0"));
                        encontrouSOL = true;
                        break;
                    }
                }

                if (!encontrouSOL)
                {
                    SubstituiVar("&(SOL%_C)&", "0.0");
                    SubstituiVar("&(SOL%_B)&", "0.0");
                    SubstituiVar("&(SOL%_D)&", "0.0");
                    SubstituiVar("&(SOL%_E)&", "0.0");
                }
            }

            sql = "SELECT * FROM Cons_Eventos_Dessat";
            rs = ExecutaSQL(cnn_dbExame, sql);
            if(rs != null)
            {
                CalculoC = 0; CalculoB = 0; CalculoD = 0; CalculoE = 0;

                foreach(DataRow rw in rs.Rows)
                {
                    if (rw["Posicao"].ToString().Equals(".")) { CalculoC++; }
                    if (rw["Posicao"].ToString().Equals("C")) { CalculoC++; }
                    if (rw["Posicao"].ToString().Equals("B")) { CalculoB++; }
                    if (rw["Posicao"].ToString().Equals("D")) { CalculoD++; }
                    if (rw["Posicao"].ToString().Equals("E")) { CalculoE++; }

                }

                SubstituiVar("&(DESSAT_C)&", CalculoC.ToString("0"));
                SubstituiVar("&(DESSAT_B)&", CalculoB.ToString("0"));
                SubstituiVar("&(DESSAT_D)&", CalculoD.ToString("0"));
                SubstituiVar("&(DESSAT_E)&", CalculoE.ToString("0"));

            }
            else
            {
                SubstituiVar("&(DESSAT_C)&", "0,0");
                SubstituiVar("&(DESSAT_B)&", "0,0");
                SubstituiVar("&(DESSAT_D)&", "0,0");
                SubstituiVar("&(DESSAT_E)&", "0,0");
            }

        }

        public static void s_dados_Posicao()
        {
            try
            {
                DataRow rwResumoExame = GlobVar.tbl_ResumoExame.Rows[0];

                // POS_C
                int pos_c = Convert.ToInt32(rwResumoExame["pos_c"]);
                int tts = Convert.ToInt32(rwResumoExame["TTS"]);
                SubstituiVar("&(POS_C_MIN)&", FormataTempoMin(pos_c));
                SubstituiVar("&(POS_C_PORC)&", tts == 0 ? "0.0" : (pos_c * 100.0 / tts).ToString("0.0", CultureInfo.InvariantCulture));

                // POS_D
                int pos_d = Convert.ToInt32(rwResumoExame["pos_d"]);
                SubstituiVar("&(POS_D_MIN)&", FormataTempoMin(pos_d));
                SubstituiVar("&(POS_D_PORC)&", tts == 0 ? "0.0" : (pos_d * 100.0 / tts).ToString("0.0", CultureInfo.InvariantCulture));

                // POS_E
                int pos_e = Convert.ToInt32(rwResumoExame["pos_e"]);
                SubstituiVar("&(POS_E_MIN)&", FormataTempoMin(pos_e));
                SubstituiVar("&(POS_E_PORC)&", tts == 0 ? "0.0" : (pos_e * 100.0 / tts).ToString("0.0", CultureInfo.InvariantCulture));

                // POS_B
                int pos_b = Convert.ToInt32(rwResumoExame["pos_b"]);
                SubstituiVar("&(POS_B_MIN)&", FormataTempoMin(pos_b));
                SubstituiVar("&(POS_B_PORC)&", tts == 0 ? "0.0" : (pos_b * 100.0 / tts).ToString("0.0", CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                // Logar ou tratar erro, se necessário
                Console.WriteLine("Erro em s_dados_Posicao: " + ex.Message);
            }
        }

        public static void s_busca_apneias_por_estagio(OleDbConnection cnn_dbExame)
        {
            DataTable tbl = new DataTable();
            int[] apn_cen_est = new int[9];

            for(int i = 0; i < apn_cen_est.Length; i ++)
            {
                apn_cen_est[i] = 0;
            }

            tbl = ExecutaSQL(cnn_dbExame, "SELECT * FROM Cons_Eventos_ApCen");
            if(tbl != null)
            {
                foreach(DataRow rw in tbl.Rows)
                {
                    int estagio = Convert.ToInt32(rw["Estagio"]);
                    apn_cen_est[estagio]++;
                }
            }

            string adulto = GlobVar.tbl_DadosExame.Rows[0]["Adulto"].ToString();

            if (adulto.Equals("A"))
            {
                SubstituiVar("&(APN_CEN_EST_1)&", apn_cen_est[1] > 0 ? apn_cen_est[1].ToString() : "0");
                SubstituiVar("&(APN_CEN_EST_2)&", apn_cen_est[2] > 0 ? apn_cen_est[2].ToString() : "0");
                SubstituiVar("&(APN_CEN_EST_3)&", apn_cen_est[3] > 0 ? apn_cen_est[3].ToString() : "0");

            }
            else if (adulto.Equals("I"))
            {
                SubstituiVar("&(APN_CEN_EST_7)&", apn_cen_est[7] > 0 ? apn_cen_est[7].ToString() : "0");
                SubstituiVar("&(APN_CEN_EST_8)&", apn_cen_est[8] > 0 ? apn_cen_est[8].ToString() : "0");
                SubstituiVar("&(APN_CEN_EST_9)&", apn_cen_est[9] > 0 ? apn_cen_est[9].ToString() : "0");
            }
        }

        public static void s_Nadir_Apneia()
        {
            int Nadir_Apneia = 200;

            var linhasValidas = GlobVar.tbl_Paginas.AsEnumerable()
                .Where(rw => rw.Field<int>("Estagio") > 0)
                .OrderBy(rw => rw.Field<float>("SatBasal"));

            if (linhasValidas != null)
            {
                int satBasal = (int)linhasValidas.First().Field<float>("SatBasal");
                if (satBasal < Nadir_Apneia)
                    Nadir_Apneia = satBasal;
            }

            if (Nadir_Apneia == 200)
                Nadir_Apneia = 0;

            SubstituiVar("&(NADIR_APNEIA)&", Nadir_Apneia.ToString());
        }

        public static void s_dados_exame_paciente(OleDbConnection cnn_dbExame, OleDbConnection cnn_dbConfig)
        {
            try
            {
                if (passagem == ultimapassagem)
                {
                    SubstituiVar("&(INICIO_EXAME_CPAP_INT)&", GlobVar.tbl_ResumoExame.Rows[0]["Ini_Exame"].ToString());
                    SubstituiVar("&(FIM_EXAME_CPAP_INT)&", GlobVar.tbl_ResumoExame.Rows[0]["Fim_Exame"].ToString());
                    SubstituiVar("&(LAT_SONO_MIN_INT)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"])).ToString());
                    SubstituiVar("&(LAT_SONO_REM_MIN_INT)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Lat_SonoREM"])).ToString());

                    SubstituiVar("&(TTS_MIN_INT)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0 ? "0" : FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])));
                    SubstituiVar("&(EFIC_SONO_INT)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0 ? "0.0" :
                        (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) * 100.0 / Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0"));
                    return;
                }
                var rowFirst = GlobVar.tbl_Paginas.AsEnumerable()
                    .Where(rw => rw.Field<int>("NumPag") == 0)
                    .FirstOrDefault();

                var rowLast = GlobVar.tbl_Paginas.AsEnumerable()
                    .OrderByDescending(rw => rw.Field<int>("NumPag"))
                    .FirstOrDefault();

                DateTime? inicio_grav = rowFirst.Field<DateTime?>("Horario");
                DateTime? fim_grav = rowLast.Field<DateTime?>("Horario");

                // Dados gerais do exame
                SubstituiVar("&(NOME)&", GlobVar.tbl_DadosExame.Rows[0]["Nome"].ToString());
                SubstituiVar("&(DATA)&", Convert.ToDateTime(GlobVar.tbl_DadosExame.Rows[0]["DataRealizacao"]).ToString("dd/MM/yyyy"));
                SubstituiVar("&(DATA_INICIO_FULL)&", inicio_grav.Value.ToString("dddd dd/MM/yyyy HH:mm"));
                SubstituiVar("&(DATA_INICIO)&", inicio_grav.Value.ToString("dd/MM/yyyy"));
                SubstituiVar("&(HORA_INICIO)&", inicio_grav.Value.ToString("HH:mm"));
                SubstituiVar("&(DATA_TERMINO_FULL)&", fim_grav.Value.ToString("dddd dd/MM/yyyy HH:mm"));
                SubstituiVar("&(DATA_TERMINO)&", fim_grav.Value.ToString("dd/MM/yyyy"));
                SubstituiVar("&(HORA_TERMINO)&", fim_grav.Value.ToString("HH:mm"));

                TimeSpan duracao = fim_grav.Value - inicio_grav.Value;

                int ttaMin = (int)duracao.TotalMinutes;
                SubstituiVar("&(TTA_MIN)&", ttaMin.ToString());

                int ttaSegundos = (int)duracao.TotalSeconds;
                SubstituiVar("&(TTA_EXTENSO)&", FormataTempoExtenso(ttaSegundos));
                var rowDadosExame = GlobVar.tbl_DadosExame.Rows[0];
                var inicioObj = GlobVar.tbl_ResumoExame.Rows[0]["Ini_Exame"];
                DateTime inicio;
                SubstituiVar("&(TTE_EXTENSO)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TTE)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])));
                if (DateTime.TryParse(inicioObj.ToString(), out inicio))
                {
                    SubstituiVar("&(INICIO_EXAME)&", inicio.ToString("HH:mm:ss"));
                }
                SubstituiVar("&(ARQUIVO)&", Path.GetFileNameWithoutExtension(GlobVar.bDataFile));
                SubstituiVar("&(SEXO)&", GlobVar.tbl_DadosExame.Rows[0]["Sexo"].ToString());
                DateTime dataNascimento = Convert.ToDateTime(rowDadosExame["DataNascimento"]);
                DateTime dataRealizacao = Convert.ToDateTime(rowDadosExame["DataRealizacao"]);

                int anos = dataRealizacao.Year - dataNascimento.Year;
                int meses = dataRealizacao.Month - dataNascimento.Month;
                int dias = dataRealizacao.Day - dataNascimento.Day;

                if (dias < 0)
                {
                    meses--;
                    dias += DateTime.DaysInMonth(dataRealizacao.AddMonths(-1).Year, dataRealizacao.AddMonths(-1).Month);
                }
                if (meses < 0)
                {
                    anos--;
                    meses += 12;
                }

                // IDADE (apenas anos)
                SubstituiVar("&(IDADE)&", anos.ToString());

                // IDADE_AM (anos e meses)
                string idadeAM = $"{anos} ano{(anos == 1 ? "" : "s")}";
                if (meses > 0)
                    idadeAM += $" e {meses} mes{(meses == 1 ? "" : "es")}";
                SubstituiVar("&(IDADE_AM)&", idadeAM);

                // IDADE_AMD (anos, meses e dias)
                string idadeAMD = $"{anos} ano{(anos == 1 ? "" : "s")}";
                if (meses > 0)
                    idadeAMD += $", {meses} mes{(meses == 1 ? "" : "es")}";
                if (dias > 0)
                    idadeAMD += $", {dias} dia{(dias == 1 ? "" : "s")}";
                SubstituiVar("&(IDADE_AMD)&", idadeAMD);
                SubstituiVar("&(DATANASCIMENTO)&", rowDadosExame.Field<DateTime>("DataNascimento").ToString("dd:MM:yyyy"));
                SubstituiVar("&(PESO)&", $"{GlobVar.tbl_DadosExame.Rows[0]["Peso"]} Kg");
                SubstituiVar(" &(ALTURA)&", $"{GlobVar.tbl_DadosExame.Rows[0]["Altura"]:0.00} m");

                double imc = Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["Altura"]) > 0
                    ? Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["Peso"]) / Math.Pow(Convert.ToDouble(GlobVar.tbl_DadosExame.Rows[0]["Altura"]), 2) : 0.0;
                SubstituiVar(" &(IMC)&", imc.ToString("0.0"));

                SubstituiVar("&(EMAIL)&", GlobVar.tbl_DadosExame.Rows[0]["EMail"].ToString());
                SubstituiVar("&(MEDICO_SOLIC)&", GlobVar.tbl_DadosExame.Rows[0]["MedicoSolicitante"].ToString());
                var fimObj = GlobVar.tbl_ResumoExame.Rows[0]["FIM_EXAME"];
                DateTime fim;
                if (DateTime.TryParse(fimObj.ToString(), out fim))
                {
                    SubstituiVar("&(FIM_EXAME)&", fim.ToString("HH:mm:ss"));
                }
                SubstituiVar("&(LAT_SONO)&", GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"].ToString());

                // Teste
                string teste = string.Join("\r\n", Enumerable.Range(0, 11).Select(i => $"linha {i}"));
                SubstituiVar("&(TESTE)&", teste);

                var latSonoObj = GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"];
                if (TimeSpan.TryParse(latSonoObj.ToString(), out TimeSpan ts))
                {
                    // Já é hh:mm:ss
                    SubstituiVar("&(LAT_SONO_MIN)&", ts.ToString(@"hh\:mm\:ss"));
                }
                else
                {
                    // É número de segundos
                    int segundos = Convert.ToInt32(latSonoObj);
                    SubstituiVar("&(LAT_SONO_MIN)&", TimeSpan.FromSeconds(segundos).ToString(@"hh\:mm\:ss"));
                }

                var ttr = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]); // TTR em minutos

                // Lê Lat_Sono e tenta converter de forma segura
                var latObj = GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"];
                int lat_sono_minutos;

                if (TimeSpan.TryParse(latObj.ToString(), out TimeSpan latTimeSpan))
                {
                    // Se estiver no formato hh:mm:ss
                    lat_sono_minutos = (int)latTimeSpan.TotalMinutes;
                }
                else
                {
                    // Caso esteja em segundos (int), convertendo para minutos
                    lat_sono_minutos = Convert.ToInt32(latObj) / 60;
                }

                // Calcula PTS em segundos
                int pts = (ttr - lat_sono_minutos) * 60;

                // Substituições
                SubstituiVar("&(PTS)&", TimeSpan.FromSeconds(pts).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(PTS_MIN)&", FormataTempoMin(pts));

                SubstituiVar("&(LAT_SONO_REM)&", GlobVar.tbl_ResumoExame.Rows[0]["Lat_SonoREM"].ToString());
                latObj = GlobVar.tbl_ResumoExame.Rows[0]["Lat_SonoREM"] == DBNull.Value ? 0 : GlobVar.tbl_ResumoExame.Rows[0]["Lat_SonoREM"];
                int latREMMin = 0;

                if (TimeSpan.TryParse(latObj.ToString(), out TimeSpan latRemTimeSpan))
                {
                    latREMMin = (int)latRemTimeSpan.TotalMinutes;
                }
                else
                {
                    // fallback se vier em segundos diretamente (ex: 2490)
                    latREMMin = Convert.ToInt32(latObj) / 60;
                }

                SubstituiVar("&(LAT_SONO_REM_MIN)&", latREMMin.ToString());
                SubstituiVar("&(LAT_SONO_REM_MIN_DEC)&", latREMMin.ToString() + ",0");

                SubstituiVar("&(TTS)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0 ? "00:00" : TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TTS_MIN)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) == 0 ? "0" : FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTS"])));

                SubstituiVar("&(TTR)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TTR_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])));
                SubstituiVar("&(EFIC_SONO)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0 ? "0.0" :
                    (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) * 100.0 / Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])).ToString("0.0"));

                SubstituiVar("&(EST_0)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_0"])).ToString(@"hh\:mm\:ss"));
                int vais = pts - Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]);
                SubstituiVar("&(VAIS)&", TimeSpan.FromSeconds(vais).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(OBSERVACAO)&", GlobVar.tbl_DadosExame.Rows[0]["Observacao"].ToString());

                SubstituiVar("&(ADULTO)&", GlobVar.tbl_DadosExame.Rows[0]["Adulto"].ToString().Equals("A") ? "Adulto" : "Infantil");
                SubstituiVar("&(MONTAGEM)&", Tela_Plotagem.MontagemBox.Text);

                for (int i = 0; i <= 9; i++)
                {
                    string texto2 = GlobVar.g_dados_fc_separada.Substring(i * 9, 9);
                    SubstituiVar($"&(Est_{i}_Media)&", int.Parse(texto2.Substring(0, 3)).ToString());
                    SubstituiVar($"&(Est_{i}_Minima)&", int.Parse(texto2.Substring(3, 3)).ToString());
                    SubstituiVar($"&(Est_{i}_Maxima)&", int.Parse(texto2.Substring(6, 3)).ToString());
                }

                var tbl_MudaEstagio = ExecutaSQL(cnn_dbExame, "SELECT * FROM tbl_MudancaEstagio");
                SubstituiVar("&(MUD_EST)&", tbl_MudaEstagio.Rows.Count.ToString());

                SubstituiVar("&(TTS_7)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_7"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TTS_7_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_7"])));
                SubstituiVar("&(TTS_8)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_8"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TTS_8_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_8"])));
                SubstituiVar("&(TTS_9)&", TimeSpan.FromSeconds(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_9"])).ToString(@"hh\:mm\:ss"));
                SubstituiVar("&(TTS_9_MIN)&", FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["Est_9"])));

                SubstituiVar("&(TTS_MIN_INT)&", Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) == 0 ? "0" : FormataTempoMin(Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"])));

                if (Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) > 0)
                {
                    double mudEstHoraSono = tbl_MudaEstagio.Rows.Count / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTR"]) / 3600.0);
                    SubstituiVar("&(MUD_EST_HORA_SONO)&", mudEstHoraSono.ToString("0.0"));
                }

                int h = GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"] == DBNull.Value ? 0 : int.Parse(GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"].ToString().Substring(0, 2));
                int m = GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"] == DBNull.Value ? 0 : int.Parse(GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"].ToString().Substring(3, 2));
                int s = GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"] == DBNull.Value ? 0 : int.Parse(GlobVar.tbl_ResumoExame.Rows[0]["Lat_Sono"].ToString().Substring(6, 2));

                int latSegundos = h * 3600 + m * 60 + s;
                int waso = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_0"]) - latSegundos;
                SubstituiVar("&(WASO)&", (waso / 60).ToString());
            }
            catch (Exception ex)
            {
                // Log ou tratamento de erro
            }
        }

        public static void s_Calcula_Saturacao_Estagio(int pag_noite, int pag_dia, OleDbConnection cnn_dbExame, OleDbConnection cnn_dbConfig)
        {
            int SaO2_100 = 511;
            int Sat_Basal_inicial = 100;

            // Obtém dados do exame
            var rsDadosExame = GlobVar.tbl_DadosExame;
            if (rsDadosExame != null && rsDadosExame.Rows.Count > 0)
            {
                var row = rsDadosExame.Rows[0];
                if (!row.IsNull("SaO2_100"))
                    SaO2_100 = Convert.ToInt32(row["SaO2_100"]);
                if (!row.IsNull("SatBasal"))
                    Sat_Basal_inicial = Convert.ToInt32(row["SatBasal"]);
            }

            // Coleta páginas a desprezar com evento CodEvento = 100
            string paginas_desprezadas = "#";
            var eventosFiltrados = GlobVar.eventos.AsEnumerable()
                .Where(rw => rw.Field<int>("CodEvento") == 100)
                .OrderBy(rw => rw.Field<int>("NumPag"))
                .ToList();

            if (eventosFiltrados.Any())
            {
                var tbl_Eventos = eventosFiltrados.CopyToDataTable();
                foreach (DataRow row in tbl_Eventos.Rows)
                {
                    paginas_desprezadas += row["NumPag"].ToString() + "#";
                }
            }
            // Variáveis de controle
            double menor_sat = SaO2_100;
            double MAIOR_SAT = 0;
            double acum = 0;
            int qtd = 0;
            int abaixo90 = 0;
            int abaixo80 = 0;
            double ref_90 = SaO2_100 * 0.9;
            double ref_80 = SaO2_100 * 0.8;

            int Sat_Segundos = 60;
            int Sat_Desvio = 10;
            int Sat_QuedaAbaixoDe = 4;
            int Sat_DuracaoMinima = 10;
            int Sat_Recalcular = 900;
            int Sat_DesprezarAbaixo = 40;

            // Parâmetros de configuração
            var rsParametros = GlobVar.tbl_ParametrosParaAnalisar;
            if (rsParametros != null && rsParametros.Rows.Count > 0)
            {
                var p = rsParametros.Rows[0];
                Sat_QuedaAbaixoDe = p.IsNull("Sat_QuedaAbaixoDe") ? 4 : Convert.ToInt32(p["Sat_QuedaAbaixoDe"]);
                Sat_DuracaoMinima = p.IsNull("Sat_DuracaoMinima") ? 10 : Convert.ToInt32(p["Sat_DuracaoMinima"]);
                Sat_Recalcular = p.IsNull("Sat_Recalcular") ? 900 : Convert.ToInt32(p["Sat_Recalcular"]);
                Sat_DesprezarAbaixo = p.IsNull("Sat_DesprezarAbaixo") ? 40 : Convert.ToInt32(p["Sat_DesprezarAbaixo"]);
                Sat_Segundos = p.IsNull("Sat_Tempo_Medio") ? 60 : Convert.ToInt32(p["Sat_Tempo_Medio"]);
                Sat_Desvio = p.IsNull("Sat_Tolerancia_Desvio") ? 10 : Convert.ToInt32(p["Sat_Tolerancia_Desvio"]);
            }

            double despreza = SaO2_100 * Sat_DesprezarAbaixo / 100.0;
            double ref_queda = Sat_Basal_inicial * (100.0 - Sat_QuedaAbaixoDe) / 100.0;

            int Qtd_Dessat = 0;
            double menor_sat_dessat = SaO2_100;

            // Inicializa vetores de contagem por estágio
            int[] sat_menor_95 = new int[10];
            int[] sat_menor_90 = new int[10];
            int[] sat_menor_85 = new int[10];
            int[] sat_menor_80 = new int[10];
            int[] sat_menor_75 = new int[10];
            int[] sat_menor_70 = new int[10];
            double[] sat_media_vlr = new double[10];
            int[] sat_media_qte = new int[10];

            int sat_Confere = 0;
            int sat_80a84 = 0;
            int sat_85a89 = 0;
            int sat_90a95 = 0;
            int sat_outras = 0;

            // Paginação e posição do canal SaO2
            if ((pag_dia == 0)) pag_dia = Canais.Get_BomDia();
            if ((pag_noite == 0)) pag_noite = Canais.Get_BoaNoite();


            var tbl_Paginas = GlobVar.tbl_Paginas.AsEnumerable().OrderBy(row => row.Field<int>("NumPag")).CopyToDataTable();

            // Primeira página com estágio de sono (≠ 0)
            foreach (DataRow row in tbl_Paginas.Rows)
            {
                if (Convert.ToInt32(row["estagio"]) != 0)
                {
                    SubstituiVar("&(INICIO_SONO)&", row["horario"].ToString());
                    int pagNum = Convert.ToInt32(row["NumPag"]);
                    SubstituiVar("&(TEMPO_ACORDADO_ANTES_SONO)&", FormataTempoMin(pagNum - Convert.ToInt32(pag_noite)));
                    break;
                }
            }

            // Posiciona na página de início da noite
            int indiceInicio = 0;
            for (int i = 0; i < tbl_Paginas.Rows.Count; i++)
            {
                if (Convert.ToInt32(tbl_Paginas.Rows[i]["NumPag"]) == Convert.ToInt32(pag_noite))
                {
                    indiceInicio = i;
                    break;
                }
            }
            int Cont = 0;
            int abaixo70 = 0;
            abaixo80 = 0;
            abaixo90 = 0;
            int qtd_pag_vigilia = 0;
            double media_pag_vigilia = 0;
            int qtd_pag_nrem = 0;
            double media_pag_nrem = 0;
            int qtd_pag_rem = 0;
            double media_pag_rem = 0;

            string SAT_MEDIA = "";
            string Sat_Media_Calc = "";
            double Sat_Valor = 0;
            double valor = 0;

            for (int i = Convert.ToInt32(pag_noite); i < Convert.ToInt32(pag_dia); i++)
            {
                if (!paginas_desprezadas.Contains("#" + i.ToString() + "#"))
                {
                    valor = Canais.F_Get1ValorDoCanalSAO2(i);
                    if (valor > despreza && valor <= 100)
                    {
                        if (SAT_MEDIA.Length < Sat_Segundos * 4)
                        {
                            SAT_MEDIA += valor.ToString("000") + "#";
                        }
                        else
                        {
                            SAT_MEDIA = SAT_MEDIA.Substring(4) + valor.ToString("000") + "#";

                            Sat_Media_Calc = SAT_MEDIA;
                            Sat_Valor = 0;
                            while (Sat_Media_Calc.Length > 1)
                            {
                                Sat_Valor += Convert.ToInt32(Sat_Media_Calc.Substring(0, 3));
                                Sat_Media_Calc = Sat_Media_Calc.Substring(4);
                            }

                            double mediaAtual = Sat_Valor / Sat_Segundos;
                            double tolerancia = mediaAtual * Sat_Desvio / 100.0;

                            if (valor < mediaAtual + tolerancia && valor > mediaAtual - tolerancia)
                            {
                                SAT_MEDIA = SAT_MEDIA.Substring(4) + valor.ToString("000") + "#";
                                acum += valor;
                                qtd++;

                                if (valor > MAIOR_SAT) MAIOR_SAT = valor;
                                if (valor < menor_sat) menor_sat = valor;

                                if (valor < 90)
                                {
                                    abaixo90++;
                                    if (valor < 80)
                                    {
                                        abaixo80++;
                                        if (valor < 70)
                                            abaixo70++;
                                    }
                                }

                                // Obter estágio da página atual
                                var rowPag = tbl_Paginas.Rows.Cast<DataRow>().FirstOrDefault(r => Convert.ToInt32(r["NumPag"]) == i);
                                if (rowPag == null) continue;
                                int estagio = Convert.ToInt32(rowPag["estagio"]);

                                if (estagio == 0)
                                {
                                    qtd_pag_vigilia++;
                                    media_pag_vigilia += valor;
                                }
                                else if (estagio == 5)
                                {
                                    qtd_pag_rem++;
                                    media_pag_rem += valor;
                                }
                                else
                                {
                                    qtd_pag_nrem++;
                                    media_pag_nrem += valor;
                                }

                                // Faixas clássicas
                                if (valor < 70)
                                    sat_menor_70[estagio]++;
                                else if (valor < 75)
                                    sat_menor_75[estagio]++;
                                else if (valor < 80)
                                    sat_menor_80[estagio]++;
                                else if (valor < 85)
                                    sat_menor_85[estagio]++;
                                else if (valor < 90)
                                    sat_menor_90[estagio]++;
                                else if (valor < 95)
                                    sat_menor_95[estagio]++;
                                else
                                    sat_Confere++;

                                // Novas faixas
                                if (valor >= 80 && valor <= 84)
                                    sat_80a84++;
                                else if (valor >= 85 && valor <= 89)
                                    sat_85a89++;
                                else if (valor >= 90 && valor <= 95)
                                    sat_90a95++;
                                else
                                    sat_outras++;

                                // Acumuladores por estágio
                                sat_media_vlr[estagio] += valor;
                                sat_media_qte[estagio] += 1;

                            }
                        }
                    }
                }
            }

            int sat_tot95 = 0;
            int sat_tot90 = 0;
            int sat_tot85 = 0;
            int sat_tot80 = 0;
            int sat_tot75 = 0;
            int sat_tot70 = 0;

            if (media_pag_vigilia > 0 && qtd_pag_vigilia > 0)
                SubstituiVar("&(SAT_MEDIA_VIGILIA)&", (media_pag_vigilia / qtd_pag_vigilia).ToString("0.0"));
            else
                SubstituiVar("&(SAT_MEDIA_VIGILIA)&", "0.0");

            if (media_pag_rem > 0 && qtd_pag_rem > 0)
                SubstituiVar("&(SAT_MEDIA_REM)&", (media_pag_rem / qtd_pag_rem).ToString("0.0"));
            else
                SubstituiVar("&(SAT_MEDIA_REM)&", "0.0");

            if (media_pag_nrem > 0 && qtd_pag_nrem > 0)
                SubstituiVar("&(SAT_MEDIA_NREM)&", (media_pag_nrem / qtd_pag_nrem).ToString("0.0"));
            else
                SubstituiVar("&(SAT_MEDIA_NREM)&", "0.0");

            for (int i = 0; i <= 9; i++)
            {
                SubstituiVar($"&(S95_{i})&", (sat_menor_95[i] / 60.0).ToString("0.0"));
                SubstituiVar($"&(S90_{i})&", (sat_menor_90[i] / 60.0).ToString("0.0"));
                SubstituiVar($"&(S85_{i})&", (sat_menor_85[i] / 60.0).ToString("0.0"));
                SubstituiVar($"&(S80_{i})&", (sat_menor_80[i] / 60.0).ToString("0.0"));
                SubstituiVar($"&(S75_{i})&", (sat_menor_75[i] / 60.0).ToString("0.0"));
                SubstituiVar($"&(S70_{i})&", (sat_menor_70[i] / 60.0).ToString("0.0"));

                if (i == 5)
                {
                    sat_tot95 += sat_menor_95[i];
                    sat_tot90 += sat_menor_90[i];
                    sat_tot85 += sat_menor_85[i];
                    sat_tot80 += sat_menor_80[i];
                    sat_tot75 += sat_menor_75[i];
                    sat_tot70 += sat_menor_70[i];
                }
            }

            double sat_nrem95 = 0, sat_nrem90 = 0, sat_nrem85 = 0, sat_nrem80 = 0, sat_nrem75 = 0, sat_nrem70 = 0;

            for (int i = 0; i <= 3; i++)
            {
                sat_nrem95 += sat_menor_95[i];
                sat_nrem90 += sat_menor_90[i];
                sat_nrem85 += sat_menor_85[i];
                sat_nrem80 += sat_menor_80[i];
                sat_nrem75 += sat_menor_75[i];
                sat_nrem70 += sat_menor_70[i];
            }

            SubstituiVar("&(S95NR)&", (sat_nrem95 / 60.0).ToString("0.0"));
            SubstituiVar("&(S90NR)&", (sat_nrem90 / 60.0).ToString("0.0"));
            SubstituiVar("&(S85NR)&", (sat_nrem85 / 60.0).ToString("0.0"));
            SubstituiVar("&(S80NR)&", (sat_nrem80 / 60.0).ToString("0.0"));
            SubstituiVar("&(S75NR)&", (sat_nrem75 / 60.0).ToString("0.0"));
            SubstituiVar("&(S70NR)&", (sat_nrem70 / 60.0).ToString("0.0"));

            SubstituiVar("&(S95TOT)&", ((sat_nrem95 + sat_tot95) / 60.0).ToString("0.0"));
            SubstituiVar("&(S90TOT)&", ((sat_nrem90 + sat_tot90) / 60.0).ToString("0.0"));
            SubstituiVar("&(S85TOT)&", ((sat_nrem85 + sat_tot85) / 60.0).ToString("0.0"));
            SubstituiVar("&(S80TOT)&", ((sat_nrem80 + sat_tot80) / 60.0).ToString("0.0"));
            SubstituiVar("&(S75TOT)&", ((sat_nrem75 + sat_tot75) / 60.0).ToString("0.0"));
            SubstituiVar("&(S70TOT)&", ((sat_nrem70 + sat_tot70) / 60.0).ToString("0.0"));

            // Médias de saturação por estágio (0 a 9)
            for (int i = 0; i <= 9; i++)
            {
                string mediaStr = sat_media_qte[i] > 0
                    ? (sat_media_vlr[i] / sat_media_qte[i]).ToString("0.0")
                    : "0.0";
                SubstituiVar($"&(SAT_MED_{i})&", mediaStr);
            }

            // Faixas percentuais de saturação
            double totalPag = pag_dia - pag_noite;
            if (totalPag > 0)
            {
                SubstituiVar("&(SATX80_84)&", (sat_80a84 / totalPag * 100).ToString("0.0"));
                SubstituiVar("&(SATX85_89)&", (sat_85a89 / totalPag * 100).ToString("0.0"));
                SubstituiVar("&(SATX90_95)&", (sat_90a95 / totalPag * 100).ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(SATX80_84)&", "0.0");
                SubstituiVar("&(SATX85_89)&", "0.0");
                SubstituiVar("&(SATX90_95)&", "0.0");
            }

        }

        public static void S_PreparaEventosRespiratorios(int pag_noite, int pag_dia, OleDbConnection cnn_dbExame, OleDbConnection cnn_dbConfig)
        {
            int? Lat_N1 = null;
            int? Lat_N2 = null;
            int? Lat_N3 = null;
            int j = 0;
            var paginas = GlobVar.tbl_Paginas.AsEnumerable()
                .OrderBy(row => row.Field<int>("NumPag"))
                .ToList();

            for (int i = 0; i < paginas.Count; i++)
            {
                int estagio = paginas[i].Field<int>("estagio");
                int numPag = paginas[i].Field<int>("NumPag");

                if (estagio != 0)
                {
                    if (estagio == 1 && Lat_N1 == null)
                        Lat_N1 = numPag;

                    if (estagio == 2 && Lat_N2 == null)
                        Lat_N2 = numPag;

                    if (estagio == 3 && Lat_N3 == null)
                    {
                        j = i + 1000;
                        if (j >= paginas.Count) j = paginas.Count - 1;
                        Lat_N3 = paginas[j].Field<int>("NumPag");
                    }

                    if (Lat_N1.HasValue && Lat_N2.HasValue && Lat_N3.HasValue)
                        break;
                }
            }

            // Substitui variáveis
            SubstituiVar("&(LAT_SONO_N1)&", ((Lat_N1.GetValueOrDefault() - pag_noite) / 60.0).ToString("00"));
            SubstituiVar("&(LAT_SONO_N2)&", ((Lat_N2.GetValueOrDefault() - pag_noite) / 60.0).ToString("00"));
            SubstituiVar("&(LAT_SONO_N3)&", ((Lat_N3.GetValueOrDefault() - pag_noite) / 60.0).ToString("00"));

            // Se a variável não existe, encerra
            if (!ExisteVar("&(DUR_MAX_REM_APNEIA_CEN)&"))
                return;

            // HIPOPNEIA - REM
            string sql = "SELECT * FROM Cons_Eventos_Hipop WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and Estagio = 5";
            var eventosRemHipop = ExecutaSQL(cnn_dbExame, sql);

            j = eventosRemHipop.Rows.Count > 0 ? eventosRemHipop.Rows.Count : 1;
            double dur_max = 0;
            double dur_tot = 0;

            foreach (DataRow ev in eventosRemHipop.Rows)
            {
                double dur = Convert.ToDouble(ev["duracao"]);
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_rem_hip = dur_max / GlobVar.namos;
            double dur_med_rem_hip = dur_tot / j / GlobVar.namos;
            double dur_tot_rem_hip = dur_tot / GlobVar.namos;
            int qtd_rem_hip = eventosRemHipop.Rows.Count;

            SubstituiVar("&(DUR_MAX_REM_HIPOPNEIA)&", dur_max_rem_hip.ToString("0.0"));
            SubstituiVar("&(DUR_MED_REM_HIPOPNEIA)&", dur_med_rem_hip.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_REM_HIPOPNEIA)&", TimeSpan.FromSeconds(dur_tot_rem_hip).ToString(@"hh\:mm\:ss"));

            // HIPOPNEIA - NREM
            sql = "SELECT * FROM Cons_Eventos_Hipop WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and (Estagio > 0 and Estagio < 4)";
            var eventosNremHipop = ExecutaSQL(cnn_dbExame, sql);

            j = eventosNremHipop.Rows.Count > 0 ? eventosNremHipop.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosNremHipop.Rows)
            {
                double dur = Convert.ToDouble(ev["duracao"]);
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_nrem_hip = dur_max / GlobVar.namos;
            double dur_med_nrem_hip = dur_tot / j / GlobVar.namos;
            double dur_tot_nrem_hip = dur_tot / GlobVar.namos;
            int qtd_nrem_hip = eventosNremHipop.Rows.Count;

            SubstituiVar("&(DUR_MAX_NREM_HIPOPNEIA)&", dur_max_nrem_hip.ToString("0.0"));
            SubstituiVar("&(DUR_MED_NREM_HIPOPNEIA)&", dur_med_nrem_hip.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_NREM_HIPOPNEIA)&", TimeSpan.FromSeconds(dur_tot_nrem_hip).ToString(@"hh\:mm\:ss"));


            // RERA - REM
            sql = "SELECT * FROM Cons_Eventos_RERA WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and Estagio = 5";
            var eventosRemRera = ExecutaSQL(cnn_dbExame, sql);

            j = eventosRemRera.Rows.Count > 0 ? eventosRemRera.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosRemRera.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_rem_rera = dur_max / GlobVar.namos;
            double dur_med_rem_rera = dur_tot / j / GlobVar.namos;
            double dur_tot_rem_rera = dur_tot / GlobVar.namos;
            int QTD_REM_RERA = eventosRemRera.Rows.Count;

            SubstituiVar("&(DUR_MAX_REM_RERA)&", dur_max_rem_rera.ToString("0.0"));
            SubstituiVar("&(DUR_MED_REM_RERA)&", dur_med_rem_rera.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_REM_RERA)&", TimeSpan.FromSeconds(dur_tot_rem_rera).ToString(@"hh\:mm\:ss"));

            // RERA - NREM
            sql = "SELECT * FROM Cons_Eventos_RERA WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and (Estagio > 0 and Estagio < 4)";
            var eventosNremRera = ExecutaSQL(cnn_dbExame, sql);

            j = eventosNremRera.Rows.Count > 0 ? eventosNremRera.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosNremRera.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_nrem_rera = dur_max / GlobVar.namos;
            double dur_med_nrem_rera = dur_tot / j / GlobVar.namos;
            double dur_tot_nrem_rera = dur_tot / GlobVar.namos;
            int QTD_NREM_RERA = eventosNremRera.Rows.Count;

            SubstituiVar("&(DUR_MAX_NREM_RERA)&", dur_max_nrem_rera.ToString("0.0"));
            SubstituiVar("&(DUR_MED_NREM_RERA)&", dur_med_nrem_rera.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_NREM_RERA)&", TimeSpan.FromSeconds(dur_tot_nrem_rera).ToString(@"hh\:mm\:ss"));


            // APNEIA CENTRAL - REM

            sql = "SELECT * FROM Cons_Eventos_ApCen WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and Estagio = 5";
            var eventosRemApCen = ExecutaSQL(cnn_dbExame, sql);

            j = eventosRemApCen.Rows.Count > 0 ? eventosRemApCen.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosRemApCen.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_rem_ap_cen = dur_max / GlobVar.namos;
            double dur_med_rem_ap_cen = dur_tot / j / GlobVar.namos;
            double dur_tot_rem_ap_cen = dur_tot / GlobVar.namos;
            int qtd_rem_ap_cen = eventosRemApCen.Rows.Count;

            SubstituiVar("&(DUR_MAX_REM_APNEIA_CEN)&", dur_max_rem_ap_cen.ToString("0.0"));
            SubstituiVar("&(DUR_MED_REM_APNEIA_CEN)&", dur_med_rem_ap_cen.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_REM_APNEIA_CEN)&",  TimeSpan.FromSeconds(dur_tot_rem_ap_cen).ToString(@"hh\:mm\:ss"));

            // APNEIA CENTRAL - NREM
            sql = "SELECT * FROM Cons_Eventos_ApCen WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and (Estagio > 0 and Estagio < 4)";
            var eventosNremApCen = ExecutaSQL(cnn_dbExame, sql);

            j = eventosNremApCen.Rows.Count > 0 ? eventosNremApCen.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosNremApCen.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_nrem_ap_cen = dur_max / GlobVar.namos;
            double dur_med_nrem_ap_cen = dur_tot / j / GlobVar.namos;
            double dur_tot_nrem_ap_cen = dur_tot / GlobVar.namos;
            int qtd_nrem_ap_cen = eventosNremApCen.Rows.Count;

            SubstituiVar("&(DUR_MAX_NREM_APNEIA_CEN)&", dur_max_nrem_ap_cen.ToString("0.0"));
            SubstituiVar("&(DUR_MED_NREM_APNEIA_CEN)&", dur_med_nrem_ap_cen.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_NREM_APNEIA_CEN)&",  TimeSpan.FromSeconds(dur_tot_nrem_ap_cen).ToString(@"hh\:mm\:ss"));

            // APNEIA MISTA - REM
            sql = "SELECT * FROM Cons_Eventos_ApMis WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and Estagio = 5";
            var eventosRemApMis = ExecutaSQL(cnn_dbExame, sql);

            j = eventosRemApMis.Rows.Count > 0 ? eventosRemApMis.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosRemApMis.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_rem_ap_mis = dur_max / GlobVar.namos;
            double dur_med_rem_ap_mis = dur_tot / j / GlobVar.namos;
            double dur_tot_rem_ap_mis = dur_tot / GlobVar.namos;
            int qtd_rem_ap_mis = eventosRemApMis.Rows.Count;

            SubstituiVar("&(DUR_MAX_REM_APNEIA_MIS)&", dur_max_rem_ap_mis.ToString("0.0"));
            SubstituiVar("&(DUR_MED_REM_APNEIA_MIS)&", dur_med_rem_ap_mis.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_REM_APNEIA_MIS)&", TimeSpan.FromSeconds(dur_tot_rem_ap_mis).ToString(@"hh\:mm\:ss"));

            // APNEIA MISTA - NREM
            sql = "SELECT * FROM Cons_Eventos_ApMis WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and (Estagio > 0 and Estagio < 4)";
            var eventosNremApMis = ExecutaSQL(cnn_dbConfig, sql);

            j = eventosNremApMis.Rows.Count > 0 ? eventosNremApMis.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosNremApMis.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_nrem_ap_mis = dur_max / GlobVar.namos;
            double dur_med_nrem_ap_mis = dur_tot / j / GlobVar.namos;
            double dur_tot_nrem_ap_mis = dur_tot / GlobVar.namos;
            int qtd_nrem_ap_mis = eventosNremApMis.Rows.Count;

            SubstituiVar("&(DUR_MAX_NREM_APNEIA_MIS)&", dur_max_nrem_ap_mis.ToString("0.0"));
            SubstituiVar("&(DUR_MED_NREM_APNEIA_MIS)&", dur_med_nrem_ap_mis.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_NREM_APNEIA_MIS)&", TimeSpan.FromSeconds(dur_tot_nrem_ap_mis).ToString(@"hh\:mm\:ss"));


            // APNEIA OBSTRUTIVA - REM
            sql = "SELECT * FROM Cons_Eventos_ApObs WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and Estagio = 5";
            var eventosRemApObs = ExecutaSQL(cnn_dbExame, sql);

            j = eventosRemApObs.Rows.Count > 0 ? eventosRemApObs.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosRemApObs.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_rem_ap_obs = dur_max / GlobVar.namos;
            double dur_med_rem_ap_obs = dur_tot / j / GlobVar.namos;
            double dur_tot_rem_ap_obs = dur_tot / GlobVar.namos;
            int qtd_rem_ap_obs = eventosRemApObs.Rows.Count;

            SubstituiVar("&(DUR_MAX_REM_APNEIA_OBS)&", dur_max_rem_ap_obs.ToString("0.0"));
            SubstituiVar("&(DUR_MED_REM_APNEIA_OBS)&", dur_med_rem_ap_obs.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_REM_APNEIA_OBS)&", TimeSpan.FromSeconds(dur_tot_rem_ap_obs).ToString(@"hh\:mm\:ss"));

            // APNEIA OBSTRUTIVA - NREM
            sql = "SELECT * FROM Cons_Eventos_ApObs WHERE (Pag_Ini >= " + pag_noite + " AND Pag_Ini <= " + pag_dia + ") and (Estagio > 0 and Estagio < 4)";
            var eventosNremApObs = ExecutaSQL(cnn_dbExame,sql);

            j = eventosNremApObs.Rows.Count > 0 ? eventosNremApObs.Rows.Count : 1;
            dur_max = 0;
            dur_tot = 0;

            foreach (DataRow ev in eventosNremApObs.Rows)
            {
                double dur = ev.Field<double>("duracao");
                if (dur > dur_max) dur_max = dur;
                dur_tot += dur;
            }

            double dur_max_nrem_ap_obs = dur_max / GlobVar.namos;
            double dur_med_nrem_ap_obs = dur_tot / j / GlobVar.namos;
            double dur_tot_nrem_ap_obs = dur_tot / GlobVar.namos;
            int qtd_nrem_ap_obs = eventosNremApObs.Rows.Count;

            SubstituiVar("&(DUR_MAX_NREM_APNEIA_OBS)&", dur_max_nrem_ap_obs.ToString("0.0"));
            SubstituiVar("&(DUR_MED_NREM_APNEIA_OBS)&", dur_med_nrem_ap_obs.ToString("0.0"));
            SubstituiVar("&(DUR_TOT_NREM_APNEIA_OBS)&", TimeSpan.FromSeconds(dur_tot_nrem_ap_obs).ToString(@"hh\:mm\:ss"));

            //   'SUBSTITUICAO DAS SOMAS
            // REM APNEIA
            // DUR_MAX_REM_APNEIA
            dur_max = dur_max_rem_ap_cen;
            if (dur_max_rem_ap_mis > dur_max) dur_max = dur_max_rem_ap_mis;
            if (dur_max_rem_ap_obs > dur_max) dur_max = dur_max_rem_ap_obs;
            SubstituiVar("&(DUR_MAX_REM_APNEIA)&", dur_max.ToString("0.0"));

            // DUR_MED_REM_APNEIA
            int total_qtd_rem_apneia = qtd_rem_ap_cen + qtd_rem_ap_mis + qtd_rem_ap_obs;
            if (total_qtd_rem_apneia > 0)
            {
                double dur_med = (dur_tot_rem_ap_cen + dur_tot_rem_ap_mis + dur_tot_rem_ap_obs) / total_qtd_rem_apneia;
                SubstituiVar("&(DUR_MED_REM_APNEIA)&", dur_med.ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(DUR_MED_REM_APNEIA)&", "0.0");
            }

            // DUR_TOT_REM_APNEIA
            SubstituiVar("&(DUR_TOT_REM_APNEIA)&", TimeSpan.FromSeconds(dur_tot_rem_ap_cen + dur_tot_rem_ap_mis + dur_tot_rem_ap_obs).ToString(@"hh\:mm\:ss"));


            // NREM APNEIA
            // DUR_MAX_NREM_APNEIA
            dur_max = dur_max_nrem_ap_cen;
            if (dur_max_nrem_ap_mis > dur_max) dur_max = dur_max_nrem_ap_mis;
            if (dur_max_nrem_ap_obs > dur_max) dur_max = dur_max_nrem_ap_obs;
            SubstituiVar("&(DUR_MAX_NREM_APNEIA)&", dur_max.ToString("0.0"));

            // DUR_MED_NREM_APNEIA
            int total_qtd_nrem_apneia = qtd_nrem_ap_cen + qtd_nrem_ap_mis + qtd_nrem_ap_obs;
            if (total_qtd_nrem_apneia > 0)
            {
                double dur_med = (dur_tot_nrem_ap_cen + dur_tot_nrem_ap_mis + dur_tot_nrem_ap_obs) / total_qtd_nrem_apneia;
                SubstituiVar("&(DUR_MED_NREM_APNEIA)&", dur_med.ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(DUR_MED_NREM_APNEIA)&", "0.0");
            }

            // DUR_TOT_NREM_APNEIA
            SubstituiVar("&(DUR_TOT_NREM_APNEIA)&", TimeSpan.FromSeconds(dur_tot_nrem_ap_cen + dur_tot_nrem_ap_mis + dur_tot_nrem_ap_obs).ToString(@"hh\:mm\:ss"));


            // REM APNEIA_HIPOP
            // MAX_REM_APNEIA_HIPOP
            dur_max = dur_max_rem_ap_cen;
            if (dur_max_rem_ap_mis > dur_max) dur_max = dur_max_rem_ap_mis;
            if (dur_max_rem_ap_obs > dur_max) dur_max = dur_max_rem_ap_obs;
            if (dur_max_rem_hip > dur_max) dur_max = dur_max_rem_hip;
            SubstituiVar("&(MAX_REM_APNEIA_HIPOP)&", dur_max.ToString("0.0"));

            if (dur_max_nrem_rera > dur_max) dur_max = dur_max_nrem_rera;
            SubstituiVar("&(MAX_REM_AHR)&", dur_max.ToString("0.0"));

            // MED_REM_APNEIA_HIPOP
            int total_qtd_rem_ahr = qtd_rem_hip + qtd_rem_ap_cen + qtd_rem_ap_mis + qtd_rem_ap_obs;
            if (total_qtd_rem_ahr > 0)
            {
                double dur_med = (dur_tot_rem_hip + dur_tot_rem_ap_cen + dur_tot_rem_ap_mis + dur_tot_rem_ap_obs) / total_qtd_rem_ahr;
                SubstituiVar("&(MED_REM_APNEIA_HIPOP)&", dur_med.ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(MED_REM_APNEIA_HIPOP)&", "0.0");
            }

            // TOT_REM_APNEIA_HIPOP
            SubstituiVar("&(TOT_REM_APNEIA_HIPOP)&", TimeSpan.FromSeconds(dur_tot_rem_hip + dur_tot_rem_ap_cen + dur_tot_rem_ap_mis + dur_tot_rem_ap_obs).ToString(@"hh\:mm\:ss"));


            // NREM APNEIA_HIPOP
            // MAX_NREM_APNEIA_HIPOP
            dur_max = dur_max_nrem_ap_cen;
            if (dur_max_nrem_ap_mis > dur_max) dur_max = dur_max_nrem_ap_mis;
            if (dur_max_nrem_ap_obs > dur_max) dur_max = dur_max_nrem_ap_obs;
            if (dur_max_nrem_hip > dur_max) dur_max = dur_max_nrem_hip;
            SubstituiVar("&(MAX_NREM_APNEIA_HIPOP)&", dur_max.ToString("0.0"));

            if (dur_max_nrem_rera > dur_max) dur_max = dur_max_nrem_rera;
            SubstituiVar("&(MAX_NREM_AHR)&", dur_max.ToString("0.0"));

            // MED_NREM_APNEIA_HIPOP
            int total_qtd_nrem_ahr = qtd_nrem_hip + qtd_nrem_ap_cen + qtd_nrem_ap_mis + qtd_nrem_ap_obs;
            if (total_qtd_nrem_ahr > 0)
            {
                double dur_med = (dur_tot_nrem_hip + dur_tot_nrem_ap_cen + dur_tot_nrem_ap_mis + dur_tot_nrem_ap_obs) / total_qtd_nrem_ahr;
                SubstituiVar("&(MED_NREM_APNEIA_HIPOP)&", dur_med.ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(MED_NREM_APNEIA_HIPOP)&", "0.0");
            }

            // TOT_NREM_APNEIA_HIPOP
            SubstituiVar("&(TOT_NREM_APNEIA_HIPOP)&", TimeSpan.FromSeconds(dur_tot_nrem_hip + dur_tot_nrem_ap_cen + dur_tot_nrem_ap_mis + dur_tot_nrem_ap_obs).ToString(@"hh\:mm\:ss"));

            // REM APNEIA_HIPOP_RERA
            // MED_REM_APNEIA_HIPOP_RERA
            int total_qtd_rem_ahr_rera = QTD_REM_RERA + qtd_rem_hip + qtd_rem_ap_cen + qtd_rem_ap_mis + qtd_rem_ap_obs;
            if (total_qtd_rem_ahr_rera > 0)
            {
                double dur_med = (dur_tot_rem_rera + dur_tot_rem_hip + dur_tot_rem_ap_cen + dur_tot_rem_ap_mis + dur_tot_rem_ap_obs) / total_qtd_rem_ahr_rera;
                SubstituiVar("&(MED_REM_AHR)&", dur_med.ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(MED_REM_AHR)&", "0.0");
            }

            // TOT_REM_APNEIA_HIPOP_RERA
            SubstituiVar("&(TOT_REM_AHR)&", TimeSpan.FromSeconds(dur_tot_rem_rera + dur_tot_rem_hip + dur_tot_rem_ap_cen + dur_tot_rem_ap_mis + dur_tot_rem_ap_obs).ToString(@"hh\:mm\:ss"));

            // NREM APNEIA_HIPOP_RERA
            // MED_NREM_APNEIA_HIPOP_RERA
            int total_qtd_nrem_ahr_rera = QTD_NREM_RERA + qtd_nrem_hip + qtd_nrem_ap_cen + qtd_nrem_ap_mis + qtd_nrem_ap_obs;
            if (total_qtd_nrem_ahr_rera > 0)
            {
                double dur_med = (dur_tot_nrem_rera + dur_tot_nrem_hip + dur_tot_nrem_ap_cen + dur_tot_nrem_ap_mis + dur_tot_nrem_ap_obs) / total_qtd_nrem_ahr_rera;
                SubstituiVar("&(MED_NREM_AHR)&", dur_med.ToString("0.0"));
            }
            else
            {
                SubstituiVar("&(MED_NREM_AHR)&", "0.0");
            }

            // TOT_NREM_APNEIA_HIPOP_RERA
            SubstituiVar("&(TOT_NREM_AHR)&", TimeSpan.FromSeconds(dur_tot_nrem_rera + dur_tot_nrem_hip + dur_tot_nrem_ap_cen + dur_tot_nrem_ap_mis + dur_tot_nrem_ap_obs).ToString(@"hh\:mm\:ss"));

            // DUR_TOT_APNEIA_CEN
            SubstituiVar("&(DUR_TOT_APNEIA_CEN)&", TimeSpan.FromSeconds(dur_tot_rem_ap_cen + dur_tot_nrem_ap_cen).ToString(@"hh\:mm\:ss"));

            // DUR_TOT_APNEIA_OBS
            SubstituiVar("&(DUR_TOT_APNEIA_OBS)&", TimeSpan.FromSeconds(dur_tot_rem_ap_obs + dur_tot_nrem_ap_obs).ToString(@"hh\:mm\:ss"));  // Corrigido: código original repetia "ap_cen" por engano

            // DUR_TOT_APNEIA_MIS
            SubstituiVar("&(DUR_TOT_APNEIA_MIS)&", TimeSpan.FromSeconds(dur_tot_rem_ap_mis + dur_tot_nrem_ap_mis).ToString(@"hh\:mm\:ss"));

            // DUR_TOT_APNEIA
            dur_tot = dur_tot_rem_ap_cen + dur_tot_nrem_ap_cen + dur_tot_rem_ap_mis + dur_tot_nrem_ap_mis + dur_tot_rem_ap_obs + dur_tot_nrem_ap_obs;
            SubstituiVar("&(DUR_TOT_APNEIA)&", TimeSpan.FromSeconds(dur_tot).ToString(@"hh\:mm\:ss"));

            // DUR_TOT_HIPOPNEIA
            dur_tot += dur_tot_rem_hip + dur_tot_nrem_hip;
            SubstituiVar("&(DUR_TOT_HIPOPNEIA)&", TimeSpan.FromSeconds(dur_tot).ToString(@"hh\:mm\:ss"));

            // QTD_REM_AHR
            SubstituiVar("&(QTD_REM_AHR)&", (qtd_rem_hip + QTD_REM_RERA + qtd_rem_ap_cen + qtd_rem_ap_mis + qtd_rem_ap_obs).ToString());

            // QTD_NREM_AHR
            SubstituiVar("&(QTD_NREM_AHR)&", (qtd_nrem_hip + QTD_NREM_RERA + qtd_nrem_ap_cen + qtd_nrem_ap_mis + qtd_nrem_ap_obs).ToString());

            // Continuação do método vai aqui...
        }

        public static async void ResumoEventos(int pag_noite, int pag_dia, OleDbConnection cnn_dbExame, OleDbConnection cnn_dbConfig)
        {
            if (!ExisteVar("&(RESUMO_EVENTOS)&")) return;

            string eventosCardiacos = "#";
            string sql;
            DataTable tbl;
            DataTable tblResumoEventos2, tblGrupoEventos2;
            DataTable tblResumoEventos, tblGrupoEventos;
            // AQ
            /*
            try
            {
                if (ExisteVar("&(RESUMO_EVENTOS)&"))
                {
                        // Eventos cardíacos
                        sql = "SELECT * FROM tbl_EventoTipoCanal WHERE CodTipoCanal = 2";
                        tbl = GlobVar.tbl_EventoTipoCanal.AsEnumerable().Where(rw => rw.Field<int>("CodTipoCanal") == 2).CopyToDataTable();
                        foreach (DataRow row in tbl.Rows)
                        {
                            eventosCardiacos += ((int)row["CodEvento"]).ToString("000") + "#";
                        }

                        // Eventos cardíacos (estágio 0 permitido)
                        sql = $@"SELECT CodEvento, COUNT(CodEvento) AS qtd_evento,
                    SUM(Duracao) AS Dur_Total,
                    MAX(Duracao) AS Maior_Dur
             FROM Cons_EventosComEstag
             WHERE Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia}
             GROUP BY CodEvento";
                    tblResumoEventos2 = ExecutaSQL(cnn_dbExame, sql);

                    tblGrupoEventos2 = new DataTable();
                    tblGrupoEventos2.Columns.Add("CodGrupo", typeof(int));
                    tblGrupoEventos2.Columns.Add("DescrGrupo", typeof(string));
                    tblGrupoEventos2.Columns.Add("CodEvento", typeof(int));
                    tblGrupoEventos2.Columns.Add("DescrEvento", typeof(string));

                    // Executa a junção com LINQ
                    var query = from a in GlobVar.tbl_RelatResumo.AsEnumerable()
                                join b in GlobVar.tbl_RelatResumoItem.AsEnumerable()
                                    on a.Field<int>("CodGrupo") equals b.Field<int>("CodGrupo")
                                join c in GlobVar.tbl_CadEvento.AsEnumerable()
                                    on b.Field<int>("CodEvento") equals c.Field<int>("CodEvento")
                                orderby a.Field<int>("CodGrupo"), b.Field<int>("Ordem")
                                select new
                                {
                                    CodGrupo = a.Field<int>("CodGrupo"),
                                    DescrGrupo = a.Field<string>("DescrGrupo"),
                                    CodEvento = b.Field<int>("CodEvento"),
                                    DescrEvento = c.Field<string>("DescrEvento")
                                };

                    // Preenche a DataTable com o resultado
                    foreach (var item in query)
                    {
                        tblGrupoEventos2.Rows.Add(item.CodGrupo, item.DescrGrupo, item.CodEvento, item.DescrEvento);
                    }
                    // Eventos não cardíacos (estágio > 0)
                    sql = $@"SELECT CodEvento, COUNT(CodEvento) AS qtd_evento,
                    SUM(Duracao) AS Dur_Total,
                    MAX(Duracao) AS Maior_Dur
             FROM Cons_EventosComEstag
             WHERE estagio > 0 AND Pag_Ini >= {pag_noite} AND Pag_Ini <= {pag_dia}
             GROUP BY CodEvento";
                    tblResumoEventos = ExecutaSQL(cnn_dbExame, sql);

                    tblGrupoEventos = new DataTable();
                    tblGrupoEventos.Columns.Add("CodGrupo", typeof(int));
                    tblGrupoEventos.Columns.Add("DescrGrupo", typeof(string));
                    tblGrupoEventos.Columns.Add("CodEvento", typeof(int));
                    tblGrupoEventos.Columns.Add("DescrEvento", typeof(string));

                    // Executa a junção com LINQ
                    var query2 = from a in GlobVar.tbl_RelatResumo.AsEnumerable()
                                join b in GlobVar.tbl_RelatResumoItem.AsEnumerable()
                                    on a.Field<int>("CodGrupo") equals b.Field<int>("CodGrupo")
                                join c in GlobVar.tbl_CadEvento.AsEnumerable()
                                    on b.Field<int>("CodEvento") equals c.Field<int>("CodEvento")
                                orderby a.Field<int>("CodGrupo"), b.Field<int>("Ordem")
                                select new
                                {
                                    CodGrupo = a.Field<int>("CodGrupo"),
                                    DescrGrupo = a.Field<string>("DescrGrupo"),
                                    CodEvento = b.Field<int>("CodEvento"),
                                    DescrEvento = c.Field<string>("DescrEvento")
                                };

                    // Preenche a DataTable com o resultado
                    foreach (var item in query)
                    {
                        tblGrupoEventos.Rows.Add(item.CodGrupo, item.DescrGrupo, item.CodEvento, item.DescrEvento);
                    }
                    string grupo = "";
                    int linha = 40;
                    int i;

                    for (int index = 0; index < tblGrupoEventos2.Rows.Count; index++)
                    {
                        var rowGrupo = tblGrupoEventos.Rows[index];
                        var rowGrupo2 = tblGrupoEventos2.Rows[index];

                        string codEvento = rowGrupo2["CodEvento"].ToString();
                        DataRow[] eventoResumo = tblResumoEventos.Select($"CodEvento = {codEvento}");
                        DataRow[] eventoResumo2 = tblResumoEventos2.Select($"CodEvento = {codEvento}");

                        if (eventoResumo2.Length > 0)
                        {
                            if (grupo != rowGrupo["DescrGrupo"].ToString())
                            {
                                grupo = rowGrupo["DescrGrupo"].ToString();

                                for (i = linha; i <= 140; i++)
                                {
                                    string cell = ObjExcel.Cells[i, 1]?.Value?.ToString() ?? "";
                                    if (cell == "Título")
                                    {
                                        ObjExcel.Cells[i, 1].Value = grupo;
                                        linha = i + 1;
                                        break;
                                    }
                                    else if (cell == "FIMFIM")
                                    {
                                        break;
                                    }
                                }
                            }

                            if (rowGrupo["DescrGrupo"].ToString() != "Eventos Cardíacos")
                            {
                                DataRow ev = eventoResumo[0];
                                ObjExcel.Cells[linha, 1].Value = "     " + rowGrupo["DescrEvento"].ToString();
                                ObjExcel.Cells[linha, 2].Value = ev["qtd_evento"];
                                ObjExcel.Cells[linha, 3].Value = TimeSpan.FromSeconds(Convert.ToDouble(ev["Dur_Total"]) / GlobVar.namos).ToString(@"hh\:mm\:ss");
                                ObjExcel.Cells[linha, 4].Value = TimeSpan.FromSeconds(Convert.ToDouble(ev["Dur_Total"]) / GlobVar.namos / Convert.ToDouble(ev["qtd_evento"])).ToString(@"hh\:mm\:ss");
                                ObjExcel.Cells[linha, 5].Value = TimeSpan.FromSeconds(Convert.ToDouble(ev["Maior_Dur"]) / GlobVar.namos).ToString(@"hh\:mm\:ss");
                                ObjExcel.Cells[linha, 6].Value = (GlobVar.tbl_ResumoExame.Rows[0]["TTS"] is 0) ? "0.0" :
                                    (Convert.ToDouble(ev["qtd_evento"]) / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) / 3600)).ToString("0.0");
                            }
                            else
                            {
                                
                                DataRow ev = eventoResumo2[0];
                                ObjExcel.Cells[linha, 1].Value = "     " + rowGrupo2["DescrEvento"].ToString();
                                ObjExcel.Cells[linha, 2].Value = ev["qtd_evento"];
                                ObjExcel.Cells[linha, 3].Value = TimeSpan.FromSeconds(Convert.ToDouble(ev["Dur_Total"]) / GlobVar.namos).ToString(@"hh\:mm\:ss");
                                ObjExcel.Cells[linha, 4].Value = TimeSpan.FromSeconds(Convert.ToDouble(ev["Dur_Total"]) / GlobVar.namos / Convert.ToDouble(ev["qtd_evento"])).ToString(@"hh\:mm\:ss");
                                ObjExcel.Cells[linha, 5].Value = TimeSpan.FromSeconds(Convert.ToDouble(ev["Maior_Dur"]) / GlobVar.namos).ToString(@"hh\:mm\:ss");
                                ObjExcel.Cells[linha, 6].Value = (GlobVar.tbl_ResumoExame.Rows[0]["TTS"] is 0) ? "0.0" :
                                    (Convert.ToDouble(ev["qtd_evento"]) / (Convert.ToDouble(GlobVar.tbl_ResumoExame.Rows[0]["TTS"]) / 3600)).ToString("0.0");
                            }

                            linha++;
                        }
                    }                    // Remove linhas vazias e "Título"
                    while (linha < 150 && (ObjExcel.Cells[linha, 1]?.Value?.ToString() ?? "") != "FIMFIM")
                    {
                        string celula = ObjExcel.Cells[linha, 1]?.Value?.ToString() ?? "";
                        string celulaProxima = ObjExcel.Cells[linha + 1, 1]?.Value?.ToString() ?? "";

                        if (celula == "Título" || (string.IsNullOrEmpty(celula) && (string.IsNullOrEmpty(celulaProxima) || celulaProxima == "Título")))
                        {
                            ObjExcel.Rows[linha].Delete();
                        }
                        else
                        {
                            linha++;
                        }
                    }

                    if (passagem == 1)
                    {
                        Cola_Resumo_Eventos("&(RESUMO_EVENTOS)&", 38, linha - 1);
                    }
                    else
                    {
                        Cola_Resumo_Eventos("&(SP_RESUMO_EVENTOS)&", 38, linha - 1);
                    }
                    
                }


                // Gráfico de Estágios
                ObjExcel.Application.Cells[1, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_0"]);// tblResumoExame_EST_0;
                ObjExcel.Application.Cells[2, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_1"]);// tblResumoExame_Est_1;
                ObjExcel.Application.Cells[3, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_2"]);// tblResumoExame_Est_2;
                ObjExcel.Application.Cells[4, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_3"]);// tblResumoExame_Est_3;
                ObjExcel.Application.Cells[5, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_4"]);// tblResumoExame_Est_5;
                ObjExcel.Application.Cells[6, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_5"]);// tblResumoExame_Est_4;
                ObjExcel.Application.Cells[7, 2].Value = Convert.ToInt32(GlobVar.tbl_ResumoExame.Rows[0]["EST_6"]);// tblResumoExame_Est_6;

                ObjExcel.Application.Cells[20, 2].Value = ev_ap_cen.qtd;
                ObjExcel.Application.Cells[21, 2].Value = ev_ap_obs.qtd;
                ObjExcel.Application.Cells[22, 2].Value = ev_ap_mis.qtd;
                ObjExcel.Application.Cells[23, 2].Value = ev_hipop.qtd;
                ObjExcel.Application.Cells[24, 2].Value = ev_rera.qtd;

                // Remove linhas com 0
                for (int i = 24; i >= 20; i--)
                {
                    if (Convert.ToDouble(ObjExcel.Application.Cells[i, 2].Value) == 0)
                        ObjExcel.Application.Rows[i].Delete();
                }
                for (int i = 7; i >= 1; i--)
                {
                    if (Convert.ToDouble(ObjExcel.Application.Cells[i, 2].Value) == 0)
                        ObjExcel.Application.Rows[i].Delete();
                }

                // Inserção de gráficos
                if (passagem == 1)
                {
                    if (ExisteVar("&(GRAF_ESTAG)&"))
                        Cola_Grafico("&(GRAF_ESTAG)&", "Chart 1");

                    if (ExisteVar("&(GRAF_EV_RESP)&"))
                        Cola_Grafico("&(GRAF_EV_RESP)&", "Chart 2");
                }
                else if (passagem == 2)
                {
                    if (ExisteVar("&(SP_GRAF_ESTAG)&"))
                        Cola_Grafico("&(SP_GRAF_ESTAG)&", "Chart 1");

                    if (ExisteVar("&(SP_GRAF_EV_RESP)&"))
                        Cola_Grafico("&(SP_GRAF_EV_RESP)&", "Chart 2");
                }
                else if (passagem > 2)
                {
                    if (ExisteVar($"&(S{passagem}_GRAF_ESTAG)&"))
                        Cola_Grafico($"&(S{passagem}_GRAF_ESTAG)&", "Chart 1");

                    if (ExisteVar($"&(S{passagem}_GRAF_EV_RESP)&"))
                        Cola_Grafico($"&(S{passagem}_GRAF_EV_RESP)&", "Chart 2");
                }

                // Inserção de hipnogramas
                //stab_Doc.Tab = 1;
                //F_Timer(1);

                if (ExisteVar("&(HIPNOGRAMA)&"))
                    Cola_Hipnograma("&(HIPNOGRAMA)&");

                if (ExisteVar("&(HIPNOGRAMA0)&") || ExisteVar("&(HIPNOGRAMA1)&") || ExisteVar("&(HIPNOGRAMA2)&"))
                {
                    if (ExisteVar("&(HIPNOGRAMA0)&") && tbl_HipnoLaudo.Rows.Count >= 2)
                    {
                        if (tbl_HipnoLaudo.Rows[1]["Hipnogramas"] != DBNull.Value)
                        {
                            if (!(tbl_HipnoLaudo.Rows[1]["Hipnogramas"].Equals(HipnoMostrando.Text)))
                            {
                                int index = HipnoMostrando.Items.IndexOf(tbl_HipnoLaudo.Rows[1]["Hipnogramas"]);
                                if (index >= 0)
                                {
                                    HipnoMostrando.TabIndex = index;
                                    await tcsTabIndexChanged.Task;
                                }
                            }
                            Cola_Hipnograma("&(HIPNOGRAMA0)&");
                        }
                    }
                    if (ExisteVar("&(HIPNOGRAMA1)&") && tbl_HipnoLaudo.Rows.Count >= 3)
                    {
                        if (tbl_HipnoLaudo.Rows[2]["Hipnogramas"] != DBNull.Value)
                        {
                            if (!(tbl_HipnoLaudo.Rows[2]["Hipnogramas"].Equals(HipnoMostrando.Text)))
                            {
                                int index = HipnoMostrando.Items.IndexOf(tbl_HipnoLaudo.Rows[2]["Hipnogramas"]);
                                if (index >= 0)
                                {
                                    HipnoMostrando.TabIndex = index;
                                    await tcsTabIndexChanged.Task;
                                }
                            }
                            Cola_Hipnograma("&(HIPNOGRAMA1)&");
                        }
                    }
                    if (ExisteVar("&(HIPNOGRAMA2)&") && tbl_HipnoLaudo.Rows.Count >= 4)
                    {
                        if (tbl_HipnoLaudo.Rows[3]["Hipnogramas"] != DBNull.Value)
                        {
                            if (!(tbl_HipnoLaudo.Rows[3]["Hipnogramas"].Equals(HipnoMostrando.Text)))
                            {
                                int index = HipnoMostrando.Items.IndexOf(tbl_HipnoLaudo.Rows[3]["Hipnogramas"]);
                                if (index >= 0)
                                {
                                    HipnoMostrando.TabIndex = index;
                                    await tcsTabIndexChanged.Task;
                                }
                            }
                            Cola_Hipnograma("&(HIPNOGRAMA2)&");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar resumo de eventos: " + ex.Message);
            }*/
        }
        public static Bitmap CaptureOpenGLControl()
        {
            var gl = openglHipno.OpenGL;
            int width = openglHipno.Width;
            int height = openglHipno.Height;

            // Cria buffer para pixels com 4 bytes por pixel (BGRA)
            byte[] pixelData = new byte[width * height * 4];

            // Lê os pixels do OpenGL no formato BGRA
            gl.ReadPixels(0, 0, width, height, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE, pixelData);

            // Cria o bitmap no formato 32bpp ARGB
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

            int stride = bmpData.Stride;
            IntPtr ptr = bmpData.Scan0;

            // Copia linha por linha (invertendo Y)
            for (int y = 0; y < height; y++)
            {
                int srcIndex = (height - y - 1) * width * 4;
                IntPtr destPtr = IntPtr.Add(ptr, y * stride);
                Marshal.Copy(pixelData, srcIndex, destPtr, width * 4);
            }

            bitmap.UnlockBits(bmpData);
            return bitmap;
        }
        public static void Cola_Hipnograma(string header)
        {
            // Captura OpenGL em alta resolução
            Bitmap originalBmp = CaptureOpenGLControl();

            // Define o controle de referência para saber o tamanho alvo
            Control areaReferencia = FormLaudo.openglHipno;

            // Converte para pontos (1 ponto = 1/72 in, 96 DPI típico em tela)
            int targetWidth = areaReferencia.Width;
            int targetHeight = areaReferencia.Height;

            // Redimensiona a imagem com qualidade alta
            Bitmap resizedBmp = new Bitmap(targetWidth, targetHeight);
            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(resizedBmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalBmp, 0, 0, targetWidth, targetHeight);
            }

            // Coloca no clipboard
            Clipboard.Clear();
            Clipboard.SetImage(resizedBmp);

            // Cola no Word
            var selection = FormLaudo.wordApp.Selection;
            selection.Find.ClearFormatting();
            selection.Find.Text = header;
            selection.Find.Replacement.ClearFormatting();
            selection.Find.Replacement.Text = "";
            selection.Find.Forward = true;
            selection.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
            selection.Find.Format = false;
            selection.Find.MatchCase = false;
            selection.Find.MatchWholeWord = false;
            selection.Find.MatchWildcards = false;
            selection.Find.MatchSoundsLike = false;
            selection.Find.MatchAllWordForms = false;
            selection.Find.Execute();

            selection.Paste();

            // Redimensiona na interface do Word se necessário
            if (selection.InlineShapes.Count > 0)
            {
                var shape = selection.InlineShapes[selection.InlineShapes.Count];
                float widthInPoints = targetWidth * 72f / 96f;
                float heightInPoints = targetHeight * 72f / 96f;

                shape.Width = widthInPoints;
                shape.Height = heightInPoints;
            }

            // Liberação
            originalBmp.Dispose();
            resizedBmp.Dispose();
        }

        public static void Cola_Grafico(string header, string chartName)
        {
            // Ativa o gráfico no Excel
            Microsoft.Office.Interop.Excel.ChartObject chartObj = (Microsoft.Office.Interop.Excel.ChartObject)ObjExcel.ActiveSheet.ChartObjects(chartName);
            chartObj.Activate();
            ObjExcel.ActiveChart.ChartArea.Select();
            ObjExcel.ActiveChart.ChartArea.Copy();

            // Localiza o texto no Word
            wordApp.Selection.Find.ClearFormatting();

            Microsoft.Office.Interop.Word.Find find = wordApp.Selection.Find;
            find.Text = header;
            find.Replacement.Text = "";
            find.Forward = true;
            find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
            find.Format = false;
            find.MatchCase = false;
            find.MatchWholeWord = false;
            find.MatchWildcards = false;
            find.MatchSoundsLike = false;
            find.MatchAllWordForms = false;

            if (find.Execute())
            {
                // Cola o gráfico como imagem
                wordApp.Selection.PasteAndFormat(Microsoft.Office.Interop.Word.WdRecoveryType.wdChartPicture);
            }
        }
        private static bool ExisteVar(string header)
        {
            string g_textolaudo = doc.Content.Text;
            if (g_textolaudo.Contains(header))
            {
                var find = wordApp.Selection.Find;
                find.ClearFormatting();
                find.Replacement.ClearFormatting();
                find.Text = header;
                find.Replacement.Text = "";
                find.Forward = true;
                find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                find.Format = false;
                find.MatchCase = false;
                find.MatchWholeWord = false;
                find.MatchWildcards = false;
                find.MatchSoundsLike = false;
                find.MatchAllWordForms = false;

                return find.Execute();
            }

            return false;
        }
        private static void Cola_Resumo_Eventos(string header, int linhaIni, int linhaFim)
        {
            try
            {
                // Pega a planilha correta
                var sheet = (Microsoft.Office.Interop.Excel.Worksheet)planExcel.Sheets[1]; // ou pelo nome

                // Seleciona e copia o intervalo
                var range = sheet.Range[$"{linhaIni}:{linhaFim}"];
                range.Copy();

                // Faz o Word visível (essencial para depuração e colagem correta)
                wordApp.Visible = false;
                doc.Activate();

                var selection = wordApp.Selection;

                // Localiza o marcador no Word
                selection.Find.ClearFormatting();
                selection.Find.Text = header;
                selection.Find.Replacement.ClearFormatting();
                selection.Find.Replacement.Text = "";
                selection.Find.Forward = true;
                selection.Find.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                selection.Find.Format = false;
                selection.Find.MatchCase = false;
                selection.Find.MatchWholeWord = false;
                selection.Find.MatchWildcards = false;
                selection.Find.MatchSoundsLike = false;
                selection.Find.MatchAllWordForms = false;

                if (selection.Find.Execute())
                {
                    selection.PasteExcelTable(false, false, false);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show($"Marcador \"{header}\" não encontrado no Word.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erro ao colar resumo de eventos: " + ex.Message);
            }
        }

        public static string FormataTempoMin(int valor)
        {
            // Supondo que valor representa minutos, você pode ajustar:
            float minut = valor / 60;
            return  minut.ToString("F1") + " min";
        }

        private static void s_resumo_CPAP()
        {
            foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in planExcel.Sheets)
            {
                if (sheet.Name.Equals("CPAP_0", StringComparison.OrdinalIgnoreCase))
                {
                    sheet.Select(); // ou sheet.Activate()
                    break;
                }
            }

            if (ExisteVar("&(RESUMO_CPAP)&"))
            {
                int linha = 2;

                for (int i = 4; i < cpapRelatDict.Count; i++)
                {
                    var relatorio = cpapRelatDict[i];

                    if ((relatorio.tempo_NREM + relatorio.tempo_REM + relatorio.tempo_Vigilia) > 0)
                    {
                        ObjExcel.Cells[linha, 1].Value = i;
                        ObjExcel.Cells[linha, 2].Value = Convert.ToDouble(FormataTempoMin(relatorio.tempo_REM));
                        ObjExcel.Cells[linha, 3].Value = Convert.ToDouble(FormataTempoMin(relatorio.tempo_NREM));
                        ObjExcel.Cells[linha, 4].Value = Convert.ToDouble(FormataTempoMin(relatorio.tempo_Vigilia));
                        ObjExcel.Cells[linha, 5].Value = relatorio.Qtd_AC;
                        ObjExcel.Cells[linha, 6].Value = relatorio.Qtd_AO;
                        ObjExcel.Cells[linha, 7].Value = relatorio.qtd_am;
                        ObjExcel.Cells[linha, 8].Value = relatorio.Qtd_Hip;
                        ObjExcel.Cells[linha, 9].Value = relatorio.Qtd_Dessat;
                        ObjExcel.Cells[linha, 10].Value = relatorio.Sat_Min;
                        ObjExcel.Cells[linha, 11].Value = relatorio.Sat_Med;
                        ObjExcel.Cells[linha, 12].Value = relatorio.Apn_Cen;

                        linha++;
                    }
                }

                planExcel.RefreshAll();

                linha = 35;
                while (linha < 66 && ObjExcel.Cells[linha, 1].Value?.ToString() != "FIMFIM")
                {
                    var valorCelula1 = ObjExcel.Cells[linha, 1].Value?.ToString();
                    var valorCelula2 = ObjExcel.Cells[linha, 2].Value?.ToString();
                    var valorCelulaProx1 = ObjExcel.Cells[linha + 1, 1].Value?.ToString();

                    if (valorCelula2 == "0" || (string.IsNullOrEmpty(valorCelula1) && (string.IsNullOrEmpty(valorCelulaProx1) || valorCelulaProx1 == "Título")))
                    {
                        var linhaExcluir = ObjExcel.Rows[linha];
                        if (linhaExcluir != null)
                        {
                            linhaExcluir.Delete();
                        }
                    }
                    else
                    {
                        linha++;
                    }
                }

                // Colar resumo de eventos
                Cola_Resumo_Eventos("&(RESUMO_CPAP)&", 35, linha - 1);
            }
        }

        private static void s_resumo_BPAP()
        {
            foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in planExcel.Sheets)
            {
                if (sheet.Name.Equals("BPAP_0", StringComparison.OrdinalIgnoreCase))
                {
                    sheet.Select(); // ou sheet.Activate()
                    break;
                }
            }

            if (ExisteVar("&(RESUMO_EPAP)&"))
            {
                int linha = 2;

                for (int i = 4; i < g_BPAP_Relat.Count; i++) // g_BPAP_Relat em C# é um array 2D ou estrutura equivalente
                {
                    for (int j = 4; j < g_BPAP_Relat[i].Count; j++)
                    {
                        var relatorio = g_BPAP_Relat[i][j];

                        if ((relatorio.tempo_NREM + relatorio.tempo_REM + relatorio.tempo_Vigilia) > 2)
                        {
                            ObjExcel.Cells[linha, 1].Value = i;
                            ObjExcel.Cells[linha, 2].Value = j;
                            ObjExcel.Cells[linha, 3].Value = Convert.ToDouble(FormataTempoMin(relatorio.tempo_REM));
                            ObjExcel.Cells[linha, 4].Value = Convert.ToDouble(FormataTempoMin(relatorio.tempo_NREM));
                            ObjExcel.Cells[linha, 5].Value = Convert.ToDouble(FormataTempoMin(relatorio.tempo_Vigilia));
                            ObjExcel.Cells[linha, 6].Value = relatorio.Qtd_AC;
                            ObjExcel.Cells[linha, 7].Value = relatorio.Qtd_AO;
                            ObjExcel.Cells[linha, 8].Value = relatorio.qtd_am;
                            ObjExcel.Cells[linha, 9].Value = relatorio.Qtd_Hip;
                            ObjExcel.Cells[linha, 10].Value = relatorio.Qtd_Dessat;
                            ObjExcel.Cells[linha, 11].Value = relatorio.Sat_Min;
                            ObjExcel.Cells[linha, 12].Value = relatorio.Sat_Med;
                            ObjExcel.Cells[linha, 13].Value = relatorio.Apn_Cen;

                            linha++;
                        }
                    }
                }

                planExcel.RefreshAll();

                linha = 266;
                while (linha < 517 && ObjExcel.Cells[linha, 1].Value?.ToString() != "FIMFIM")
                {
                    var valorCelula1 = ObjExcel.Cells[linha, 1].Value?.ToString();
                    var valorCelula2 = ObjExcel.Cells[linha, 2].Value?.ToString();
                    var valorCelulaProx1 = ObjExcel.Cells[linha + 1, 1].Value?.ToString();

                    if (valorCelula2 == "0" || (string.IsNullOrEmpty(valorCelula1) && (string.IsNullOrEmpty(valorCelulaProx1) || valorCelulaProx1 == "Título")))
                    {
                        var linhaExcluir = ObjExcel.Rows[linha];
                        if (linhaExcluir != null)
                        {
                            linhaExcluir.Delete();
                        }
                    }
                    else
                    {
                        linha++;
                    }
                }

                Cola_Resumo_Eventos("&(RESUMO_bpap)&", 266, linha - 1);
            }
        }

        private static void s_ResumoHipoVentilacao()
        {

            if (ExisteVar("&(HIPOVENTILACAO)&"))
            {
                foreach (Microsoft.Office.Interop.Excel.Worksheet sheet in planExcel.Sheets)
                {
                    if (sheet.Name.Equals("Hipo", StringComparison.OrdinalIgnoreCase))
                    {
                        sheet.Select(); // ou sheet.Activate()
                        break;
                    }
                }


                /* Codigo original em VB6 que esta comentado, caso seja preciso utilizar ele futuramente
                 *       'For i = 1 To frm_Principal.grd_HipoVent.Rows - 1            
                          '   frm_Principal.grd_HipoVent.Row = i
                          '   frm_Principal.grd_HipoVent.col = 0
                          '   If frm_Principal.grd_HipoVent.Text = "" Then Exit For
                          '   ObjExcel.Application.Cells(linha, 1) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 1
                          '   ObjExcel.Application.Cells(linha, 2) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 2
                          '   ObjExcel.Application.Cells(linha, 3) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 3
                          '   ObjExcel.Application.Cells(linha, 4) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 4
                          '   ObjExcel.Application.Cells(linha, 5) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 5
                          '   ObjExcel.Application.Cells(linha, 6) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 6
                          '   ObjExcel.Application.Cells(linha, 7) = frm_Principal.grd_HipoVent.Text
                          '   frm_Principal.grd_HipoVent.col = 7
                          '   ObjExcel.Application.Cells(linha, 8) = frm_Principal.grd_HipoVent.Text
                          '   linha = linha + 1
                          'Next
                          'planExcel.RefreshAll
                */

                int linha = 1;

                while (linha < 91 && ObjExcel.Cells[linha, 1].Value?.ToString() != "FIMFIM")
                {
                    var valorCelula1 = ObjExcel.Cells[linha, 1].Value?.ToString();
                    var valorCelula2 = ObjExcel.Cells[linha, 2].Value?.ToString();
                    var valorCelulaProx1 = ObjExcel.Cells[linha + 1, 1].Value?.ToString();

                    if (valorCelula2 == "0" || (string.IsNullOrEmpty(valorCelula1) && (string.IsNullOrEmpty(valorCelulaProx1) || valorCelulaProx1 == "Título")))
                    {
                        var linhaExcluir = ObjExcel.Rows[linha];
                        if (linhaExcluir != null)
                        {
                            linhaExcluir.Delete();
                        }
                        // NÃO incrementa linha aqui porque as linhas sobem após deletar
                    }
                    else
                    {
                        linha++;
                    }
                }

                Cola_Resumo_Eventos("&(HIPOVENTILACAO)&", 1, linha - 2);

            }
        }

        private static void s_dados_PTT(OleDbConnection cnn_dbExame)
        {
            DataTable tbl_EventosPTT = new DataTable();
            double X = 0;

            string sql = "SELECT Estagio, Count(SumOfDuracao) AS qtd_PTT, Max(SumOfDuracao) AS Max_PTT, Sum(SumOfDuracao) AS Duracao_PTT From Cons_Eventos_PTT GROUP BY Estagio";

            tbl_EventosPTT = ExecutaSQL(cnn_dbExame, sql);
            if(tbl_EventosPTT != null)
            {
                for(int i = 0; i <= 9; i++)
                {
                    if(tbl_EventosPTT.AsEnumerable().Any(row => row.Field<int>("Estagio") == i))
                    {
                        DataRow rw_EventosPTT = tbl_EventosPTT.AsEnumerable().Where(row => row.Field<int>("Estagio") == i).FirstOrDefault();

                        SubstituiVar("&(QTD_PTT_" + i + ")&", rw_EventosPTT["qtd_PTT"].ToString());
                        SubstituiVar("&(MAIOR_PTT_" + i + ")&", FormataTempoMin(Convert.ToInt32(rw_EventosPTT["max_PTT"]) / 512));
                        SubstituiVar("&(DUR_PTT_" + i + ")&", FormataTempoMin(Convert.ToInt32(rw_EventosPTT["Duracao_PTT"]) / 512));
                    }
                }
            }
            else
            {
                for(int i = 0; i <= 9; i++)
                {
                    SubstituiVar("&(QTD_PTT_" + i + ")&", "0");
                    SubstituiVar("&(MAIOR_PTT_" + i + ")&", "-");
                    SubstituiVar("&(DUR_PTT_" + i + ")&", "-");
                }
            }

        }
        private static string FormataTempoExtenso(int totalSegundos)
        {
            int horas = totalSegundos / 3600;
            int minutos = (totalSegundos % 3600) / 60;
            int segundos = totalSegundos % 60;

            List<string> partes = new List<string>();

            if (horas > 0)
                partes.Add(horas + (horas == 1 ? " hora" : " horas"));

            if (minutos > 0)
                partes.Add(minutos + (minutos == 1 ? " minuto" : " minutos"));

            if (segundos > 0 && horas == 0) // opcional: só mostra segundos se for menos de 1h
                partes.Add(segundos + (segundos == 1 ? " segundo" : " segundos"));

            return string.Join(" e ", partes);
        }

        public static void F_AnaliseHipoventilacao()
        {
            int pag_ini_eve = 0;
            int pag_fim_eve = 0;
            string hora_ini_eve = "";
            string hora_fim_eve = "";
            int tempo_rem_eve = 0;
            int tempo_nRem_eve = 0;
            int menor_sat_rem = 0;
            int menor_sat_nRem = 0;
            int tempototal;
            int Sat_Estagio = 0;
            int pag_ini = Canais.Get_BoaNoite();
            int pag_fim = Canais.Get_BomDia();
            int Sat_DesprezarAbaixo = 40;
            bool novoevento = false;

            if (GlobVar.tbl_ParametrosParaAnalisar.Rows != null)
            {
                Sat_DesprezarAbaixo = Convert.ToInt32(GlobVar.tbl_ParametrosParaAnalisar.Rows[0]["Sat_DesprezarAbaixo"]);
            }

            int despreza = Sat_DesprezarAbaixo;

            List<int> PagDesprezadas = new List<int>();

            DataTable filt = GlobVar.eventos.AsEnumerable().Where(rw => rw.Field<int>("CodEvento") == 100).Where(rw => rw.Field<int>("CodCanal1") == 66).CopyToDataTable();

            foreach(DataRow rw in filt.Rows)
            {
                PagDesprezadas.Add(Convert.ToInt32(rw["NumPag"]));
            }
            filt = GlobVar.eventos.AsEnumerable().Where(rw => rw.Field<int>("CodEvento") == 8).CopyToDataTable();
            foreach (DataRow rw in filt.Rows)
            {
                PagDesprezadas.Add(Convert.ToInt32(rw["NumPag"]));
            }

            for(int i = pag_ini; i < pag_fim; i++)
            {
                var rowTbl_Pagina = GlobVar.tbl_Paginas.Rows[i];
                int valor = Canais.F_Get1ValorDoCanalSAO2(i);
                if(valor > despreza && valor <= 100)
                {
                    if (!PagDesprezadas.Contains(i))
                    {
                        Sat_Estagio = Convert.ToInt32(rowTbl_Pagina["Estagio"]);
                        if ((Sat_Estagio > 0 && Sat_Estagio <= 9) && valor <= 88)
                        {
                            if (!novoevento)
                            {
                                pag_ini_eve = i;
                                hora_ini_eve = rowTbl_Pagina["horario"].ToString();
                                tempo_rem_eve = 0;
                                tempo_nRem_eve = 0;
                                menor_sat_rem = 0;
                                menor_sat_nRem = 0;
                                novoevento = true;
                            }
                            if (Sat_Estagio == 5)
                            {
                                tempo_rem_eve += 1;
                                if (menor_sat_rem == 0 || valor < menor_sat_rem)
                                {
                                    menor_sat_rem = valor;
                                }
                            }
                            else
                            {
                                tempo_nRem_eve += 1;
                                if(menor_sat_nRem == 0 || valor < menor_sat_nRem)
                                {
                                    menor_sat_nRem = valor;
                                }
                            }
                            novoevento = true;
                        }
                        else
                        {
                            // ... DENTRO DO ELSE, dentro do for, substituindo o bloco de VB:
                            if (novoevento)
                            {
                                pag_fim_eve = i;
                                // MovePrevious em VB busca a linha anterior; então:
                                var prevRowTbl_Pagina = GlobVar.tbl_Paginas.Rows[i - 1];
                                hora_fim_eve = prevRowTbl_Pagina["horario"].ToString();
                                novoevento = false;

                                DateTime dtHoraIniEve = DateTime.Parse(hora_ini_eve);
                                DateTime dtHoraFimEve = DateTime.Parse(hora_fim_eve);

                                if (dtHoraFimEve < dtHoraIniEve)
                                {
                                    TimeSpan ateFimDia = DateTime.Parse("23:59:59") - dtHoraIniEve;
                                    TimeSpan desdeMeiaNoite = dtHoraFimEve - DateTime.Parse("00:00:00");
                                    tempototal = (int)ateFimDia.TotalSeconds + 1 + (int)desdeMeiaNoite.TotalSeconds;
                                }
                                else
                                {
                                    tempototal = (int)(dtHoraFimEve - dtHoraIniEve).TotalSeconds;
                                }

                                if (tempototal > 300)
                                {
                                    // Se for grid, exemplo usando DataTable como fonte:
                                    DataRow linha;
                                    // Supondo DataTable grd_HipoVent:
                                    linha = GlobVar.grd_HipoVent.NewRow();
                                    linha[0] = hora_ini_eve;
                                    linha[1] = hora_fim_eve;
                                    linha[2] = FormataTempo(tempototal, false);
                                    if (menor_sat_rem > 0)
                                        linha[3] = (menor_sat_nRem < menor_sat_rem) ? menor_sat_nRem : menor_sat_rem;
                                    else
                                        linha[3] = menor_sat_nRem;

                                    if (tempo_rem_eve > 0)
                                    {
                                        linha[4] = FormataTempo(tempo_rem_eve, false);
                                        linha[5] = menor_sat_rem;
                                    }
                                    if (tempo_nRem_eve > 0)
                                    {
                                        linha[6] = FormataTempo(tempo_nRem_eve - 1, false);
                                        linha[7] = menor_sat_nRem;
                                    }
                                    linha[8] = pag_ini_eve;
                                    linha[9] = pag_fim_eve;

                                    GlobVar.grd_HipoVent.Rows.Add(linha);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string FormataTempo(int segundos, bool mostraSinal)
        {
            // Evite valores negativos, a não ser que deseje sinal explícito
            bool negativo = segundos < 0;
            segundos = Math.Abs(segundos);

            TimeSpan tempo = TimeSpan.FromSeconds(segundos);

            string formatado = $"{(int)tempo.TotalHours:00}:{tempo.Minutes:00}:{tempo.Seconds:00}";
            if (mostraSinal && negativo)
                return "-" + formatado;
            else
                return formatado;
        }
    }
}

public class EventoResumo
{
    public int qtd;
    public double indice;
    public double maior;
    public double media;
    public double durtotal;
    public int qtd_rem;
    public int qtd_nrem;
    public int qtd_pos_c;
    public int qtd_pos_x;
}

public class NapResumo
{
    public TimeSpan Inicio { get; set; }
    public TimeSpan fim { get; set; }
    public int Lat_Est1 { get; set; }
    public int Lat_Est2 { get; set; }
    public int Lat_Est3 { get; set; }
    public int Lat_Est4 { get; set; }
    public int Lat_Est5_BoaNoite { get; set; }
    public int Lat_Est5_SleepOnset { get; set; }
    public int Lat_Sono { get; set; }
    public int TempodeREM { get; set; }
    public int TempoEst0 { get; set; }
    public int TempoEst1 { get; set; }
    public int TempoEst2 { get; set; }
    public int TempoEst3 { get; set; }
    public int TTR { get; set; }
    public int TTS { get; set; }
    public TimeSpan HorarioREM { get; set; }
    public TimeSpan HorarioNREM { get; set; }
}

public class CPAPRelat
{
    public int Qtd_AC = 0;
    public int qtd_am = 0;
    public int Qtd_AO = 0;
    public int Qtd_Dessat = 0;
    public int Qtd_Hip = 0;
    public int Sat_Min = 100;
    public int tempo_NREM = 0;
    public int tempo_REM = 0;
    public int tempo_Vigilia = 0;
    public int Sat_Med = 0;
    public int Apn_Cen = 0;
    public int qtd_pags = 0;
}

public class BPAPRelat
{
    public int Qtd_AC = 0;
    public int qtd_am = 0;
    public int Qtd_AO = 0;
    public int Qtd_Dessat = 0;
    public int Qtd_Hip = 0;
    public int Sat_Min = 100;
    public int tempo_NREM = 0;
    public int tempo_REM = 0;
    public int tempo_Vigilia = 0;
    public int Sat_Med = 0;
    public int qtd_pags = 0;
    public int Apn_Cen = 0;
}
