using Accord.Math;
using Accord.Statistics;
using Cyotek.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.auxi.FormsAuxi;
using PlotagemOpenGL.Filtros;
using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UnityEngine;

namespace PlotagemOpenGL.Hipnograma
{
    public partial class HipnogramaForm : Form
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

        public HipnogramaForm(int cod)
        {
            // Obtém as dimensões da tela principal
            int larguraTela = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            int alturaTela = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            // Define o tamanho e a posição inicial do formulário
            this.StartPosition = FormStartPosition.Manual;
            this.Size = new Size((int)(larguraTela * 0.8), (int)(alturaTela * 0.8)); // 80% da largura e altura da tela
            this.Location = new Point((larguraTela - this.Width) / 2, (alturaTela - this.Height) / 2); // Centraliza o formulário na tela

            InitializeComponent();

            openglHipno.Location = new Point(0, 0); // Mantém o controle ancorado no canto superior esquerdo
            openglHipno.Size = new Size(this.ClientSize.Width, this.ClientSize.Height); // Ajusta ao tamanho interno do formulário

            codJanela = cod;
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
            string name = rw["DescrJanela"].ToString();

            this.Text = name;

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

                        int canalindexC02 = 0;
                        int LimiteInferiorC02 = 0;
                        int LimiteSuperiorC02 = 0;
                        codindex = GlobVar.codSelected.IndexOf(codcanal);
                        if (codindex != -1)
                        {
                            canalIndex = GlobVar.codCanal.IndexOf(codcanal);
                        }
                        else
                        {
                            var rwcp = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                        .Where(row => row.Field<int>("CodTipoCanal") == 31)
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

                        int ponteiroIC = GlobVar.ponteiroI[canalIndex];
                        int ponteiroFC = GlobVar.ponteiroF[canalIndex];

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
                        for(int aq = 0; aq < posicao.Length; aq++)
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
                        foreach(DataRow rowEstagio in GlobVar.tbl_Paginas.Rows)
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
            gl.Viewport(0, 0, (int)HipnogramaForm.openglHipno.Width, (int)HipnogramaForm.openglHipno.Height);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();

            gl.Ortho(0, Porcentagem, 0, HipnogramaForm.openglHipno.Height, -2, 2);

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
                gl.Vertex(NeEmarg, HipnogramaForm.openglHipno.Height);
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
                gl.Vertex(marg, HipnogramaForm.openglHipno.Height);
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
            gl.Vertex(marg + pagAtual, HipnogramaForm.openglHipno.Height);
            gl.End();
            gl.Flush();
            gl.Disable(OpenGL.GL_LINE_STIPPLE);

            gl.Color(0f, 0f, 0f); 

            int espacox = HipnogramaForm.openglHipno.Height;
            int porcent = HipnogramaForm.openglHipno.Height;
            int topPorcent = HipnogramaForm.openglHipno.Height;

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
                        }else
                        {
                            eusla = 1;
                        } 
                    }
                    else { eusla = 0; }
                    Cor = new float[3];
                    
                    foreach (DataRow rw in dtSubGrupo.Rows)
                    {
                        cadEvent = Convert.ToInt32(rw["Evento"]);
                        var aoi = GlobVar.tbl_CadEvento.AsEnumerable().FirstOrDefault(row => row.Field<int>("CodEvento") ==  cadEvent);

                        // Desenha as riscas correspondentes
                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (eventosResp[j - xStart + pagBn, index] != 0) // Verifica se há evento
                            {
                                if(eusla != 0)
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
                    if(codindex != -1){
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
                    for(int i = xStart; i < xEnd; i++, esta++)
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
                            if(CodEstagio == 5)
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
                        for(int i = 0; i < Convert.ToInt32(ls); i++)
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
                    for(int ao = 0; ao < qt; ao++)
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
                else if(codGrupo == 9) 
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
                        if(startXdiv < 0) { startXdiv = 0; }

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
            var gl = HipnogramaForm.openglHipno.OpenGL;

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
                else{
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

                if(codJanela == 6 || codJanela == 12 || codJanela == 11 || codJanela == 18)
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
                    if (!string.IsNullOrEmpty(novoValor) && ( Convert.ToInt32(novoValor) > 1 || Convert.ToInt32(novoValor) < 0))
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
                if(codJanela == 1 || codJanela == 3 || codJanela == 4 || codJanela == 5 || codJanela == 10 || codJanela == 40 || codJanela == 9) 
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
                else{
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
                    if(codindex != -1)
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

                    if(codcanal != 65)
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

                    int canalindexC02 = 0;
                    int LimiteInferiorC02 = 0;
                    int LimiteSuperiorC02 = 0;
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

                    int ponteiroIC = GlobVar.ponteiroI[canalIndex];
                    int ponteiroFC = GlobVar.ponteiroF[canalIndex];

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
                if(menuItem.Tag != null)
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

    }
}
