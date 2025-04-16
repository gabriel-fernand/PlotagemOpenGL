using Accord.Math;
using Accord.Statistics;
using Cyotek.Windows.Forms;
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
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow.Operations.Losses;
using UnityEngine;
using Excel = Microsoft.Office.Interop.Excel;

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
        public FormLaudo()
        {
            // Obtém as dimensões da tela principal
            int larguraTela = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int alturaTela = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            InitializeComponent();

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

            AjustarDTJanela();
            PreparaOsArrays();
            gl = openglHipno.OpenGL;
            Desenha();
            formOriginalSize = this.Size;
            recgl = new Rectangle(openglHipno.Location, openglHipno.Size);
            this.Resize += resiz;
            buttonForm = new ButtonForm();
            pagAtual = GlobVar.indice / GlobVar.namos;
            var rw = GlobVar.tbl_JanelaResumo.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodJanela") == codJanela);

            this.Text = "Laudo e Relatório de Polissonografia";
            comentarios();
            CarregarArquivosNoComboBox();
        }
        private void CarregarArquivosNoComboBox()
        {
            string diretorio = @"C:\Temp\Laudos";

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
            string diretorio = @"C:\Temp\Laudos";
            string nomeSelecionado = comboBox1.SelectedItem.ToString();


            string caminhoCompleto = Path.Combine(diretorio, nomeSelecionado + ".doc");

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
                int codGrupo = Convert.ToInt16(row["CodGrupo"]);

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
                                    SA02[h] = GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
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
                                FreqCard[h] = GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
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
                        //float[] linhaFiltrada = LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex]));
                        float[] linhaFiltrada = BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex])), 40f, 120f, 512);
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
                                posicao[h] += (GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g] * -1);
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

                legenda(porcent, topPorcent, Convert.ToInt16(MontagemJanela.Rows[i]["CodGrupo"]), marg, (int)Porcentagem);

                gl.End();
                gl.Flush();
                desenhaGarficos(porcent, topPorcent, Convert.ToInt16(MontagemJanela.Rows[i]["CodGrupo"]), marg, (int)Porcentagem);

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
                            quasi = NormalizarValor(SA02[sasa], Convert.ToInt16(rows["LI"]), Convert.ToInt16(rows["LS"]), pontoZero, topPonto);
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
                            quasi = NormalizarValor(FreqCard[feq], Convert.ToInt16(rowf["LI"]), Convert.ToInt16(rowf["LS"]), pontoZero, topPonto);
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
                        quasi = NormalizarValor(CPAP[cpap], Convert.ToInt16(rowsf["LI"]), Convert.ToInt16(rowsf["LS"]), pontoZero, topPonto);
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
                        quasi = NormalizarValor(CPAPVaz[cpapvz], Convert.ToInt16(rowvz["LI"]), Convert.ToInt16(rowvz["LS"]), pontoZero, topPonto);
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
                            quasi = NormalizarValor(FreqCard[feqc], Convert.ToInt16(rowc["LI"]), Convert.ToInt16(rowc["LS"]), pontoZero, topPonto);
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
                        // Filtra a linha do DataTable
                        var row = GlobVar.tbl_Estagios.AsEnumerable()
                                    .FirstOrDefault(r => r.Field<int>("Estagio") == CodEstagio);

                        var dt = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();

                        // Procura o índice da linha correspondente ao CodEstagio no DataTable
                        int ind = dt.AsEnumerable()
                                     .Select((r, idx) => new { Row = r, Index = idx }) // Combina linha e índice
                                     .FirstOrDefault(x => x.Row.Field<int>("Estagio") == CodEstagio)?.Index ?? -1;
                        gl.Color(0, 0, 0);

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
            dt = GlobVar.tbl_HipnoGrupos.AsEnumerable().Where(row => row.Field<int>("CodGrupo") == codGrupo).CopyToDataTable();
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
                                SA02[h] = GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
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
                            FreqCard[h] = GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g];
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
                    //float[] linhaFiltrada = LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex]));
                    float[] linhaFiltrada = BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.grafSelected[codindex])), 40f, 120f, 512);
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
                            posicao[h] += (GlobVar.matrizCanal[GlobVar.grafSelected[codindex], g] * -1);
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
            string arq = @"C:\Temp\Retorno\Dessat.txt";
            using (StreamWriter writer = new StreamWriter(arq, false, Encoding.Default))
            {
                // Leitura/Gravação no INI
                IniFile ini = new IniFile(@"C:\Temp\Config.ini");
                string cargaHipo = ini.Read("CARGAHIPO", "DIRETORIOS");
                if (string.IsNullOrWhiteSpace(cargaHipo))
                {
                    ini.Write("CARGAHIPO", "1", "DIRETORIOS");
                }

                double carga = 0;
                double acum = 0;
                //string sql = "SELECT * FROM tbl_Eventos WHERE CodEvento = 17 ORDER BY NumPag";
                var eventosQuery = GlobVar.eventos.AsEnumerable()
                    .Where(row => row.Field<int>("CodEvento") == 17)
                    .OrderBy(row => row.Field<int>("NumPag"));

                DataTable eventos = eventosQuery.Any() ? eventosQuery.CopyToDataTable() : GlobVar.eventos.Clone(); // ou new DataTable()
                if (eventos.Rows.Count > 0)
                {
                    int idx = 0;
                    int dur = 0;
                    int iniValor = 0;
                    int fim = 0;

                    while (idx < eventos.Rows.Count)
                    {
                        int num = Convert.ToInt32(eventos.Rows[idx]["seq"]);

                        // Reset antes de processar o grupo
                        iniValor = 0;
                        dur = 0;

                        while (idx < eventos.Rows.Count && Convert.ToInt32(eventos.Rows[idx]["seq"]) == num)
                        {
                            int numPag = Convert.ToInt32(eventos.Rows[idx]["NumPag"]);
                            int linhaSaturacao = GlobVar.codSelected.IndexOf(66);
                            int ponteiro = numPag * 8;

                            if (iniValor == 0)
                            {
                                // Primeiro valor do grupo (pegando página anterior)
                                ponteiro = (numPag - 1) * 8;
                                iniValor = Convert.ToInt16(GlobVar.matrizCanal[linhaSaturacao, ponteiro]);
                            }
                            else
                            {
                                fim = Convert.ToInt16(GlobVar.matrizCanal[linhaSaturacao, ponteiro]);
                            }

                            dur++;
                            idx++;
                        }

                        if (iniValor != 0)
                        {
                            double calcCarga = 0.5 * (dur / 60.0) * Math.Abs(iniValor - fim);
                            string linha = $"{iniValor} - {fim} - {dur} ====== {calcCarga:0.0000}";
                            writer.WriteLine(linha);
                            carga = calcCarga;
                            acum += carga;
                        }
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

        bool laudosplitnight = false;
        bool laudosegmentos = false;

        int passagens = 0;
        int ultimapassagem = 0;
        int segmentos = 0;
        List<object> lst_segmentos = new();
        string g_variaveislaudo = "";

        private void CriarLaudo_Click(object sender, System.EventArgs e)
        {
            try
            {
                int pag_noite = 0;//Canais.Get_BoaNoite();
                int pag_dia = 0;//Canais.Get_BomDia();


                CalculaCargaHipoxica();

                laudosplitnight = TextoComboContem("SPLIT NIGHT");
                laudosegmentos = TextoComboContem("SEGMENTOS");

                if (GlobVar.eventos.AsEnumerable().Any(row => row.Field<int>("CodEvento") == 50) && (TextoComboContem("SPLIT NIGHT") || TextoComboContem("SEGMENTOS")))
                {
                    passagens = 3;
                    ultimapassagem = 3;
                }
                else
                {
                    passagens = 1;
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

                    if (segmentos > passagens)
                    {
                        passagens = segmentos;
                        ultimapassagem = segmentos;
                    }
                }

                string g_arq_exame = @"C:\Temp\Exames\" + comboBox1.Text + " Laudo.DOC";

                if (ExisteArquivo(g_arq_exame))
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

                //Cursor.Current = Cursors.WaitCursor;

                string g_dir_laudos = @"C:\Temp\Laudos\";
                string nomeOrigem = Path.Combine(g_dir_laudos, comboBox1.Text + ".doc");
                string nome_arq_temp = "TMP" + DateTime.Now.ToString("HHmmss");
                string caminhoTemp = Path.Combine(g_dir_laudos, nome_arq_temp + ".doc");

                // Copia o arquivo original para o temporário
                File.Copy(nomeOrigem, caminhoTemp, true);

                // Inicializa o Word
                Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
                Microsoft.Office.Interop.Word.Document doc = null;
                string g_textolaudo = "";

                doc = wordApp.Documents.Open(caminhoTemp);
                doc.Activate();

                g_textolaudo = wordApp.Selection.Text;

                if (!TextoComboContem("CALIBRACAO"))
                {
                    if (segmentos > 0)
                    {
                        PreparaRelatorioMDB();
                    }
                }
                for (int i = 1; i < passagens; i++)
                {
                    if (segmentos > 0 && passagens > 0)
                    {

                    }

                    string origem = Path.Combine(g_dir_laudos, "Graficos para laudo.xls");
                    string destino = Path.Combine(g_dir_laudos, nome_arq_temp + ".xls");
                    File.Copy(origem, destino, overwrite: true); // overwrite se quiser sobrescrever o destino


                    Excel.Application ObjExcel = new Excel.Application();

                    Excel.Workbook workbook = ObjExcel.Workbooks.Open(destino);

                    passagens = i;

                    if (passagens == 1)
                    {
                        pag_dia = Canais.Get_BomDia();
                        pag_noite = Canais.Get_BoaNoite();
                    }
                    else if (laudosplitnight)
                    {
                        if (passagens == 1)
                        {
                            pag_noite = Canais.Get_BoaNoite();
                            pag_dia = Canais.Get_InicioCPAP();
                        }
                        else if (passagens == 2)
                        {
                            pag_noite = Canais.Get_InicioCPAP();
                            pag_dia = Canais.Get_BomDia();
                        }
                        else if (passagens == ultimapassagem)
                        {
                            pag_noite = Canais.Get_BoaNoite();
                            pag_dia = Canais.Get_BomDia();
                        }
                    }
                    else if (laudosegmentos)
                    {
                        if (passagens == ultimapassagem)
                        {
                            pag_noite = Canais.Get_BoaNoite();
                            pag_dia = Canais.Get_BomDia();
                        }
                        else
                        {
                            pag_noite = Convert.ToInt32(lst_segmentos[passagens - 1]);

                            if (passagens == passagens - 1)
                            {
                                pag_dia = Canais.Get_BomDia();
                            }
                            else
                            {
                                pag_dia = Convert.ToInt32(lst_segmentos[passagens]) - 1;
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
                                if (comentario.StartsWith(passagens.ToString()))
                                {
                                    pag_noite = Convert.ToInt32(row["NumPag"]);
                                }
                                else if (comentario.StartsWith((passagens + 1).ToString()))
                                {
                                    pag_dia = Convert.ToInt32(row["NumPag"]) - 1;
                                }
                            }
                        }
                    }

                    if (!TextoComboContem("CALIBRACAO"))
                    {

                    }
                }

            }
            catch { }
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

        private bool TextoComboContem(string termo)
        {
            return comboBox1.Text.ToUpper().Contains(termo.ToUpper());
        }
        public static bool ExisteArquivo(string path)
        {
            return File.Exists(path);
        }
        private string f_var(string key)
        {
            return key switch
            {
                "Var58073" => "O arquivo está aberto, feche-o para continuar.",
                "Var58005" => "O laudo já existe. Deseja refazer o laudo?",
                "Var26063" => "Atenção",
                _ => ""
            };
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
            string relatorioPath = Path.Combine("C:/Temp/", "Relatorios.mdb");
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

            var eventosDesprezar = GlobVar.eventos.AsEnumerable()
                                                    .Where(row => row.Field<int>("CodEvento") == 100 && row.Field<int>("CodCanal1") == 67)
                                                    .OrderBy(row => row.Field<int>("NumPag"));
            List<int> PagDesprezadas = eventosDesprezar
                .Select(row => row.Field<int>("NumPag"))
                .ToList();

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

                                            valor = (int)Math.Round(media , 0);

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
            if(tbl != null)
            {
                GlobVar.tbl_RelatResumo = ExecutaSQL(cnn_Config, "SELECT * FROM tbl_RelatResumo");
                foreach(DataRow tbl_RelatResumo in GlobVar.tbl_RelatResumo.Rows)
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
                    if(tbl_RelatResumoItem != null && tbl_RelatResumoItem.AsEnumerable().Any(row => row.Field<int>("CodGrupo") == codGrupo))
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
                // Trate o erro conforme necessário (log, exceção customizada, etc.)
                Console.WriteLine("Erro ao executar SQL: " + ex.Message);
            }

            return result;
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
    }
}
