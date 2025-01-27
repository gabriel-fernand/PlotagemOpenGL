using Accord.Audio.Filters;
using Accord.Math;
using Accord.Math.Geometry;
using Accord.Statistics;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.Filtros;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnityEngine;
using static OpenTK.Graphics.OpenGL.GL;

namespace PlotagemOpenGL.Hipnograma
{
    public partial class HipnogramaForm : Form
    {
        public static Rectangle recgl;
        private Size formOriginalSize;
        public static int codGrupMouse;
        public static DataTable MontagemJanela;
        public static OpenGL gl;
        public static int codJanela;
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
            porc = new float[MontagemJanela.Rows.Count];
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
            int codcanal;
            int codindex;
            int[] media;
            int rw = 0;
            int h;
            DataTable subGrupos = new DataTable();

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
        public void Desenha()
        {
            int tamanho = GlobVar.matrizCanal.GetLength(1) / GlobVar.namos;
            float Porcentagem = (float)(tamanho * 1.05);
            int margem = Math.Abs((int)Porcentagem - tamanho);

            pontoZero = new int[MontagemJanela.Rows.Count];
            pontoTop = new int[MontagemJanela.Rows.Count];

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
            gl.Color(0.0f, 0.0f, 0.0f);
            gl.Scale(1, 1, 1);

            gl.Color(0.1f, 0.1f, 0.1f);

            gl.Color(0.5f, 0.5f, 0.5f); // Define a cor das linhas (preto)

            //----------
            gl.Begin(OpenGL.GL_LINE_STRIP);
            gl.Vertex(margem, 0);
            gl.Vertex(margem, HipnogramaForm.openglHipno.Height);
            gl.End();
            gl.Flush();
            //----------

            int espacox = HipnogramaForm.openglHipno.Height;
            int porcent = HipnogramaForm.openglHipno.Height;
            int topPorcent = HipnogramaForm.openglHipno.Height;

            int ant = MontagemJanela.Rows.Count;
            for (int i = 0; i < MontagemJanela.Rows.Count; i++)
            {
                porcent -= (int)(espacox * (porc[i] / 100));
                pontoZero[i] = porcent;
                pontoTop[i] = topPorcent;
                gl.Begin(OpenGL.GL_LINE_STRIP);
                gl.Vertex(0, porcent);
                gl.Vertex(Porcentagem, porcent);
                gl.End();
                gl.Flush();
                ant--;

                legenda(porcent, topPorcent, Convert.ToInt16(MontagemJanela.Rows[i]["CodGrupo"]), margem, (int)Porcentagem);

                gl.End();
                gl.Flush();
                desenhaGarficos(porcent, topPorcent, Convert.ToInt16(MontagemJanela.Rows[i]["CodGrupo"]), margem, (int)Porcentagem);

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

                    foreach (DataRow rw in dtSubGrupo.Rows)
                    {
                        // Desenha as riscas correspondentes
                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (eventosResp[j - xStart, index] != 0) // Verifica se há evento
                            {
                                gl.Color(0, 0, 0); // Define a cor da linha
                                gl.Begin(OpenGL.GL_LINES); // Use GL_LINES para linhas simples
                                gl.Vertex(j, locrisc - tanhamorisco / 2); // Linha começa um pouco acima do texto
                                gl.Vertex(j, locrisc + tanhamorisco / 2); // Linha termina um pouco abaixo do texto
                                gl.End();
                                gl.Flush();
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


                    for (int i = 0; i < qt; i++)
                    {
                        index = 0;

                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (Despertar[index] != 0)
                            {
                                gl.Color(0, 0, 0);
                                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                                gl.Vertex(j, pontoZero + tanhamorisco);
                                gl.Vertex(j, topPonto - tanhamorisco);
                                gl.End();
                                gl.Flush();

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


                    for (int i = 0; i < qt; i++)
                    {
                        index = 0;

                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (plm[index] != 0)
                            {
                                gl.Color(0, 0, 0);
                                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                                gl.Vertex(j, pontoZero + tanhamorisco);
                                gl.Vertex(j, topPonto - tanhamorisco);
                                gl.End();
                                gl.Flush();

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


                    for (int i = 0; i < qt; i++)
                    {
                        index = 0;

                        for (int j = xStart; j < xEnd; j++)
                        {
                            if (ronco[index] != 0)
                            {
                                gl.Color(0, 0, 0);
                                gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                                gl.Vertex(j, pontoZero + tanhamorisco);
                                gl.Vertex(j, topPonto - tanhamorisco);
                                gl.End();
                                gl.Flush();

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
                    break;

                // ------- Sinais graafio -------
                // SA02
                case 6:
                    SA02Strip.Checked = true;
                    SA02Strip.Tag = 6;
                    var rows = GlobVar.tbl_JanelaResumoItens.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    gl.Color( 0, 0, 0);
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int sasa = 0;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        quasi = NormalizarValor(SA02[sasa], Convert.ToInt16(rows["LI"]), Convert.ToInt16(rows["LS"]), pontoZero, topPonto);
                        gl.Vertex(i, quasi);
                        sasa++;
                    }
                    gl.End();
                    gl.Flush();
                    break;
                // Freq Card
                case 12:
                    FreqCardStrip.Checked = true;
                    FreqCardStrip.Tag = 12;
                    var rowf = GlobVar.tbl_JanelaResumoItens.AsEnumerable()
                                .FirstOrDefault(r => r.Field<int>("CodGrupo") == codGrupo);

                    gl.Color(0, 0, 0);
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int feq = 0;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        quasi = NormalizarValor(FreqCard[feq], Convert.ToInt16(rowf["LI"]), Convert.ToInt16(rowf["LS"]), pontoZero, topPonto);
                        gl.Vertex(i, quasi);
                        feq++;
                    }
                    gl.End();
                    gl.Flush();
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
                    double scala = GlobVar.scale[GlobVar.grafSelected[codindex]];

                    gl.Color(0, 0, 0);
                    gl.Begin(OpenGL.GL_LINE_STRIP); // Inicia o desenho da linha
                    int micmic = 0;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        quasi = NormalizarValor(Microfone[micmic], Microfone.Min(), Microfone.Max(), pontoZero, topPonto);// scala;
                        gl.Vertex(i, quasi);// + meioleg);
                        micmic++;
                    }
                    gl.End();
                    gl.Flush();

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

                    int posiposi = 0;
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
                    float[] color = new float[3];
                    gl.Begin(OpenGL.GL_LINE_STRIP);
                    int ultimoestagio = -1;

                    int esta = 0;
                    for(int i = xStart; i < xEnd; i++, esta++)
                    {

                        int CodEstagio = estagio[esta];
                        // Filtra a linha do DataTable
                        var row = GlobVar.tbl_Estagios.AsEnumerable()
                                    .FirstOrDefault(r => r.Field<int>("Estagio") == CodEstagio);
                            // Obtém os componentes RGB com base no campo "Estagio"
                        color = plotGrafico.ObterComponentesRGB(Convert.ToInt32(row["Cor"]));
                        var dt = GlobVar.tbl_Estagios.AsEnumerable().OrderByDescending(row => row.Field<int>("Ordem")).CopyToDataTable();

                        // Procura o índice da linha correspondente ao CodEstagio no DataTable
                        int ind = dt.AsEnumerable()
                                     .Select((r, idx) => new { Row = r, Index = idx }) // Combina linha e índice
                                     .FirstOrDefault(x => x.Row.Field<int>("Estagio") == CodEstagio)?.Index ?? -1;
                        if(ultimoestagio != CodEstagio)
                        {
                            gl.Color(0, 0, 0);
                        }
                        else
                        {
                            gl.Color(color[0], color[1], color[2]);
                        }
                        gl.Vertex(i, locyEstagio[ind]);
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
            if(codGrupo == 21)
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
                            writeX = (int)(writeX - (tamanhostring.Width/4));
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
            else if (codGrupo == 6 || codGrupo == 12)
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

                gl.Color(0.5f, 0.5f, 0.5f);
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

                gl.Color(0.5f, 0.5f, 0.5f);
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
                if (linhasint != 0 && linhasint < dif)
                {
                    int alo = dif / linhasint;
                    int locdivs = (int)(pontoZero + (espaco / alo)); // - (tamanhoLs.Height / 2));
                    int espacodiv = espaco / alo;

                    gl.Color(0.5f, 0.5f, 0.5f);
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

            if (!dt.Rows[0]["Legenda"].Equals("") && !dt.Rows[0]["Legenda"].Equals("ESTAGIO") && !dt.Rows[0]["Legenda"].Equals("Posição"))
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
                    ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);

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

                        int locmarc = (int)(endX / 2);
                        ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);

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

                        int locmarc = (int)(endX / 2);
                        ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);

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

                        int locmarc = (int)(endX / 2);
                        ConvertToScreenCoordinates(locmarc, 0, out writeX, out writeY);

                        gl.Begin(OpenGL.GL_2D);

                        gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 13, "");
                        gl.DrawText(writeX, meioleg, 0.5f, 0.5f, 0.5f, "Arial Narrow", 15, legMarcDAgua);

                        gl.End();
                        gl.Flush();
                    }
                }
            }
        }
        public static double Normalizar(int input, int pontoZero, int topPonto, double minOutput = 0, double maxOutput = 0.5f)
        {
            try
            {
                // Garante que pontoZero é o menor valor
                int min = Math.Min(pontoZero, topPonto);
                int max = Math.Max(pontoZero, topPonto);

                // Evita divisão por zero
                if (max == min)
                    return 0;

                // Normaliza o valor para o intervalo [0, 1]
                double normalizado = (double)(input - min) / (max - min);

                // Ajusta o valor para o intervalo desejado [minOutput, maxOutput]
                return normalizado * (maxOutput - minOutput) + minOutput;
            }
            catch { return 0; }
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

            if (valor < minOriginal) return minY;
            if (valor > maxOriginal) return maxY;

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
        private void OpenGLHipno_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if(e != null)
                {
                    var dr = MontagemJanela.AsEnumerable().OrderBy(row => row.Field<int>("Ordem")).CopyToDataTable();
                    ConvertToOpenGLCoordinates(e.X, e.Y, out mouseLocX, out mouseLocY);
                    for(int i = 0; i < pontoZero.Length; i++)
                    {
                        if (mouseLocY > pontoZero[i] && mouseLocY < pontoTop[i])
                        {
                            codJanela = Convert.ToInt32(dr.Rows[i]["CodGrupo"]);
                        }
                    }
                }
            }
            catch
            {

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
                //contextMenuStripHipno.Items.Clear();

            }
            catch { }

        }
    }
}
