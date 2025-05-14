using Newtonsoft.Json.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.BD;
using SharpGL;
using SharpGL.WPF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class PagSelecionadasImpressao : Form
    {
        private static Tela_Plotagem _formPai; 
        public PagSelecionadasImpressao(Tela_Plotagem formPai)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Define o estilo da borda como fixo
            this.MaximizeBox = false; // Desativa o botão de maximizar
            _formPai = formPai;
            CarregarDadosPaginacao();
            CarregarArquivosDiretorio(@"C:\Temp\Dat\", $"{Path.GetFileNameWithoutExtension(GlobVar.textFile)}_tela");
        }

        private void CarregarDadosPaginacao()
        {
            // Fazer uma cópia do DataTable original para não alterar o original
            DataTable dt = GlobVar.tbl_SelImpressao.Copy();

            // Agrupar os dados pela coluna "CodImpressao" e ordenar por "CodImpressao"
            var groupedRows = dt.AsEnumerable()
                .GroupBy(r => r.Field<int>("CodImpressao"))
                .OrderBy(g => g.Key);

            // Criar um novo DataTable para armazenar os dados ajustados
            DataTable dtAgrupado = new DataTable();
            dtAgrupado.Columns.Add("CodImpressao", typeof(int));
            dtAgrupado.Columns.Add("NomeMontagem", typeof(string));
            dtAgrupado.Columns.Add("PagInicial", typeof(int));
            dtAgrupado.Columns.Add("Epoca", typeof(int));
            dtAgrupado.Columns.Add("QtdSeg", typeof(int));
            dtAgrupado.Columns.Add("Arq_Video", typeof(string));
            dtAgrupado.Columns.Add("Ref", typeof(string)); // Coluna extra para Ref

            foreach (var group in groupedRows)
            {
                // Pegar a primeira ocorrência do grupo
                var row = group.First();

                DataRow newRow = dtAgrupado.NewRow();
                newRow["CodImpressao"] = row.Field<int>("CodImpressao"); // Ajustar para sequência contínua
                newRow["NomeMontagem"] = row.Field<string>("NomeMontagem");
                newRow["PagInicial"] = row.Field<int>("PagInicial");
                newRow["Epoca"] = row.Field<int>("PagInicial") / 30; // Valor de "Epoca" como inteiro
                newRow["QtdSeg"] = row.Field<int>("QtdSeg");
                newRow["Arq_Video"] = row.IsNull("Arq_Video") ? "." : row.Field<string>("Arq_Video");
                newRow["Ref"] = ""; // Inicialmente vazio, conforme solicitado

                // Adicionar a nova linha ao DataTable agrupado
                dtAgrupado.Rows.Add(newRow);
            }

            // Limpar o DataGridView antes de adicionar os novos dados
            dataGridViewPaginas.Rows.Clear();

            // Preencher o DataGridView com os dados ajustados
            foreach (DataRow row in dtAgrupado.Rows)
            {
                int codigo = row.Field<int>("CodImpressao");
                string montagem = row.Field<string>("NomeMontagem");
                int pagInicial = row.Field<int>("PagInicial");
                int epoca = row.Field<int>("Epoca");
                int duracaoSeg = row.Field<int>("QtdSeg");
                string video = row.Field<string>("Arq_Video");

                // Adicionar os dados ao DataGridView
                dataGridViewPaginas.Rows.Add(codigo, montagem, pagInicial, epoca, duracaoSeg, video);
            }
        }

        private void CarregarArquivosDiretorio(string diretorio, string nomeInicial)
        {
            if (Directory.Exists(diretorio))
            {
                var arquivos = Directory.GetFiles(diretorio, $"{nomeInicial}*");
                listBoxArquivos.Items.Clear();
                foreach (var arquivo in arquivos)
                {
                    listBoxArquivos.Items.Add(Path.GetFileName(arquivo));
                }
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void updtadeInfos()
        {
            int paginaCoerente = GlobVar.indice / GlobVar.namos;
            int inicio = (GlobVar.indice / GlobVar.namos);
            TimeSpan tempo = TimeSpan.FromSeconds(inicio);
            string horasI = tempo.Hours.ToString().PadLeft(2, '0');
            string minutosI = tempo.Minutes.ToString().PadLeft(2, '0');
            string segundosI = tempo.Seconds.ToString().PadLeft(2, '0');

            // Atualização de controles com segurança de thread
            Tela_Plotagem.fimTela.Text = $"{horasI}:{minutosI}:{segundosI}";

            Tela_Plotagem.PainelMarca.Enabled = GlobVar.segundos == 30 &&
                                  (Convert.ToInt16(segundosI) == 30 || Convert.ToInt16(segundosI) == 0);

            var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(r => r.Field<int>("NumPag") == paginaCoerente);
            string horario = row["Horario"].ToString();
            Tela_Plotagem.inicioTela.Text = $"{horario.Substring(11)}";

            int pagina = paginaCoerente / 30;
            string telaPagText = pagina.ToString("D3");
            Tela_Plotagem.ptsEmTela.Text = $"{telaPagText}";

            int indexLabel = 0;
            for (int i = 1; i <= GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                FieldInfo labelInfo = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                if (labelInfo != null)
                {
                    System.Windows.Forms.Label label = (System.Windows.Forms.Label)labelInfo.GetValue(this);
                    if (label != null)
                    {
                        label.Text = $"{GlobVar.tbl_MontagemSelecionada.Rows[indexLabel]["AmplitudeMin"]}μV";
                        indexLabel++;
                    }
                }
            }
            int estagioAtual = Convert.ToInt16(row["Estagio"]);
            if (estagioAtual == 5) { Tela_Plotagem.lbEstagio.Text = "Estágio: R"; } else { Tela_Plotagem.lbEstagio.Text = "Estágio: " + estagioAtual.ToString(); }

            Tela_Plotagem.Atual.BackgroundImage = Tela_Plotagem.GetEstagioImage(estagioAtual);
            Tela_Plotagem.Atual.BackgroundImageLayout = ImageLayout.Stretch;
            if (GlobVar.tbl_SelImpressao != null)
            {
                var dt = GlobVar.tbl_SelImpressao.AsEnumerable()
                .GroupBy(r => r.Field<int>("CodImpressao"))
                .OrderBy(g => g.Key);
                int qtd = dt.Count();

                Tela_Plotagem.lbImpressao.Text = "Impressão: " + qtd.ToString();
            }
            else
            {
                Tela_Plotagem.lbImpressao.Text = "Impressão: 0";
            }

            if (!Tela_Plotagem.isScroll)
            {
                Tela_Plotagem.hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                Tela_Plotagem.isScroll = true;
            }
            Tela_Plotagem.atualizaButAntProx();
            //CalcularQtdImpressao();
            if (Tela_Plotagem.videoIni)
            {
                iCelera.telinha.attLocVideo();
                iCelera.telinha.videoPlayer.Ctlcontrols.pause();
            }
            if (Tela_Plotagem.Janela != null)
            {
                Tela_Plotagem.Janela.Desenha();
            }
            Tela_Plotagem.openglControl1.Focus();

        }
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            if (dataGridViewPaginas.SelectedRows.Count > 0)
            {
                int newloc = 0;
                float calcPont = 0;
                DataGridViewRow selectedRow = dataGridViewPaginas.SelectedRows[0];
                int codImpressao = Convert.ToInt32(selectedRow.Cells["dataGridViewTextBoxColumn1"].Value);

                if (codImpressao > 0)
                {
                    // sua lógica aqui

                    DataRow rowCod = GlobVar.tbl_SelImpressao.AsEnumerable().Where(rw => rw.Field<int>("CodImpressao") == codImpressao).FirstOrDefault();
                    int cod = Convert.ToInt32(rowCod["CodMontagem"]);

                    int PagAtual = GlobVar.indice / GlobVar.namos;
                    int PagImpressao = Convert.ToInt32(rowCod["PagInicial"]);
                    if (PagAtual != PagImpressao)
                    {
                        newloc = PagImpressao;
                        calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                        Tela_Plotagem.camera.X = newloc * GlobVar.namos;

                        GlobVar.indice = newloc * GlobVar.namos;
                        GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                        GlobVar.ponteiroVideo = GlobVar.indice + calcPont;

                        GlobVar.indiceNumero = newloc * GlobVar.namosNumerico;
                        GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);

                    }

                    int CodAtual = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);
                    bool codDiferente = false;
                    if (cod != CodAtual)
                    {
                        codDiferente = true;
                        LeituraBanco.AlteraMontagem(cod);

                        LeituraEmMatrizTeste.montagemSelecionadaAlterada();
                        LeituraEmMatrizTeste.referencias();

                        Tela_Plotagem.canais = new Canais(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                        Tela_Plotagem.canais.RealocPanel(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                        Tela_Plotagem.canais.quantidadeGraf(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                        Tela_Plotagem.canais.RealocButton();
                        Tela_Plotagem.canais.PainelLb_Resize();
                        Tela_Plotagem.canais.reloc();

                        Tela_Plotagem.UpdatePanelHeightInDataTable();
                        Tela_Plotagem.AjustarFonteDosLabels();
                        Tela_Plotagem.AjustarBotoesMinusEPlus();
                        // Inicia uma nova tarefa em segundo plano, interrompida imediatamente se for cancelada
                        Tela_Plotagem._backgroundTask = Task.Run(() =>
                        {
                            try
                            {
                                LeituraEmMatrizTeste.montagemSelecionadaAlteradaTudo();
                            }
                            catch (OperationCanceledException)
                            {
                                Console.WriteLine("A tarefa em segundo plano foi cancelada.");
                            }
                        });

                        int normalSize = Tela_Plotagem.painelExames.Height / GlobVar.tbl_MontagemSelecionada.Rows.Count;

                        int lastTop = 0;

                        foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
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
                        Tela_Plotagem.UpdatePanelHeightInDataTable();
                        Tela_Plotagem.AjustarFonteDosLabels();

                    }



                    Tela_Plotagem.hScrollBar1.Value = GlobVar.indice / GlobVar.namos;
                    updtadeInfos();
                    Tela_Plotagem.TelaClearAndReload();

                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Verificar se há uma ou mais linhas selecionadas
            if (dataGridViewPaginas.SelectedRows.Count > 0)
            {
                // Lista para armazenar os códigos a serem excluídos
                var codigosParaExcluir = new List<int>();

                // Iterar sobre todas as linhas selecionadas
                foreach (DataGridViewRow selectedRow in dataGridViewPaginas.SelectedRows)
                {
                    // Obter o valor do "CodImpressao" da linha selecionada
                    int codImpressao = Convert.ToInt32(selectedRow.Cells["Código"].Value);
                    if (codImpressao > 0)
                    {
                        // Adicionar o código à lista de exclusão
                        codigosParaExcluir.Add(codImpressao);
                    }
                }

                // Remover as linhas do DataGridView
                foreach (DataGridViewRow selectedRow in dataGridViewPaginas.SelectedRows)
                {
                    if (!selectedRow.IsNewRow)
                    {
                        dataGridViewPaginas.Rows.Remove(selectedRow);
                    }
                }

                // Remover as linhas correspondentes do DataTable e do banco de dados
                foreach (int codImpressao in codigosParaExcluir)
                {
                    // Remover as linhas do DataTable
                    DataRow[] rowsToDelete = GlobVar.tbl_SelImpressao.Select($"CodImpressao = {codImpressao}");
                    foreach (DataRow row in rowsToDelete)
                    {
                        GlobVar.tbl_SelImpressao.Rows.Remove(row);
                    }

                    // Excluir do banco de dados
                    ExcluirDoBancoDeDados(codImpressao);
                }

                //MessageBox.Show("Exclusão realizada com sucesso.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma ou mais linhas para excluir.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExcluirDoBancoDeDados(int codImpressao)
        {
            string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};Persist Security Info=False";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Tbl_SelImpressao WHERE CodImpressao = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CodImpressao", codImpressao);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            //MessageBox.Show("Exclusão realizada com sucesso.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            //MessageBox.Show("Nenhuma linha encontrada para exclusão.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show($"Erro ao excluir do banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSelecionarTudo_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPaginas.SelectAll();
                for (int i = 0; i < listBoxArquivos.Items.Count; i++)
                {
                    listBoxArquivos.SetSelected(i, true);
                }
            }
            catch { }
        }

        public bool TabelasSaoIguaisPorColunasDeReferencia(DataTable referencia, DataTable comparada)
        {
            if (referencia.Rows.Count != comparada.Rows.Count)
                return false;

            for (int i = 0; i < referencia.Rows.Count; i++)
            {
                DataRow rowRef = referencia.Rows[i];
                DataRow rowCmp = comparada.Rows[i];

                foreach (DataColumn colRef in referencia.Columns)
                {
                    string columnName = colRef.ColumnName;

                    if (!comparada.Columns.Contains(columnName))
                        continue;

                    object valorRef = rowRef[columnName];
                    object valorCmp = rowCmp[columnName];

                    bool refEhNullOuDbNull = valorRef == null || valorRef == DBNull.Value;
                    bool cmpEhNullOuDbNull = valorCmp == null || valorCmp == DBNull.Value;

                    // Regra especial: DBNull ou null são considerados equivalentes a -1
                    if ((refEhNullOuDbNull && valorCmp?.ToString() == "-1") ||
                        (cmpEhNullOuDbNull && valorRef?.ToString() == "-1"))
                    {
                        continue;
                    }

                    // Comparação normal convertendo para string e ignorando espaços
                    string strRef = Convert.ToString(valorRef)?.Trim();
                    string strCmp = Convert.ToString(valorCmp)?.Trim();

                    if (!string.Equals(strRef, strCmp, StringComparison.InvariantCulture))
                    {
                        return false; // encontrou diferença
                    }
                }
            }

            return true;
        }

        private async void btnbtnSelecTodoExame_Click(object sender, EventArgs e)
        {
            using (CarregandoAltMontagem telaLoad = new CarregandoAltMontagem())
            {
                telaLoad.Show();
                telaLoad.label1.Text = "Salvando Paginas";

                var lastNum = GlobVar.tbl_SeqEvento.AsEnumerable().Last();
                int CodImpressao = lastNum == null ? 1 : Convert.ToInt32(lastNum["ProxPagImp"]);

                //variável para salvar todas as paginas. 
                var lastRow = GlobVar.tbl_Paginas.AsEnumerable().LastOrDefault();

                int maximoPossivel = Convert.ToInt32(lastRow["NumPag"]) / 30;

                int valLoad1 = (int)Math.Round(maximoPossivel / 10 * 0.01f);
                int valLoad = 0;

                int paginaCoerente = 0;
                while (paginaCoerente < maximoPossivel)
                {
                    // Clonar a estrutura de tbl_SellImpressao para manter a mesma ordem de colunas
                    DataTable telaSelect = GlobVar.tbl_SelImpressao.Clone();

                    CodImpressao++;
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
                    bool MostrarAmpl = Tela_Plotagem.MostarAmplitudes.Checked;
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
                    await Task.Delay(100);

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
                    await Task.Delay(100);

                    GlobVar.tbl_SeqEvento.Rows[0]["ProxPagImp"] = CodImpressao;
                    if (paginaCoerente % 10 == 0)
                    {
                        telaLoad.AtualizarProgresso(valLoad);
                        valLoad += (int)valLoad1;
                    }

                    paginaCoerente++;
                    // Agora o telaSelect possui a estrutura de tbl_SellImpressao e os dados de tbl_MontagemSelecionada
                }
                telaLoad.AtualizarProgresso(100);
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
        private void AdicionarImagemAoPDF(PdfDocument document, Bitmap imagem, IniFile ini, string epoca = "", string horario = "", int estagio = 0)
        {
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
                    ImprimeLogo = true;
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

            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            double xLoc = mmParaPontos(int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_ESQUERDA")));
            double yLoc = mmParaPontos(int.Parse(ini.Read("IMPRESSORA PADRAO", "MARGEM_SUPERIOR")));
            double espacoEntreLinhas = mmParaPontos(2);

            string tituloPrincipal = getTitulo();
            string medicoSolicitante = GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica1"]?.ToString() ?? "Informe o nome do médico";
            string modeloEquipamento = GlobVar.tbl_DadosClinica.Rows[0]["NomeClinica2"]?.ToString() ?? "Informe o nome da clínica";
            if(!epoca.Equals(""))
            {
                string descricao = $"Epoca: {epoca} - Horario: {horario} - Estagio: {estagio} ({GlobVar.segundos} seg)";
                // Desenhar a descrição acima da imagem no PDF
                // Atualiza yLoc com base na altura da fonte + espaçamento extra
                XSize tamanhoTexto = gfx.MeasureString(descricao, new XFont("Arial", 8));
                gfx.DrawString(descricao, new XFont("Arial", 8), XBrushes.Black, new XPoint(xLoc, (95 - tamanhoTexto.Height - 1)));

            }



            void DesenhaTextoAjustado(string texto, XFont fonte)
            {
                gfx.DrawString(texto, fonte, XBrushes.Black, new XPoint(xLoc, yLoc));
                XSize tamanhoTexto = gfx.MeasureString(texto, fonte);
                yLoc += tamanhoTexto.Height + espacoEntreLinhas;
            }

            DesenhaTextoAjustado(tituloPrincipal, new XFont("Arial", 14));
            DesenhaTextoAjustado(medicoSolicitante, new XFont("Arial", 10));
            DesenhaTextoAjustado(modeloEquipamento, new XFont("Arial", 10));

            using (MemoryStream stream = new MemoryStream())
            {
                imagem.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                XImage xImage = XImage.FromStream(stream);

                double scaleFactor = Math.Min((page.Width - 20) / xImage.PixelWidth, (page.Height - 60) / xImage.PixelHeight);
                double width = xImage.PixelWidth * scaleFactor;
                double height = page.Height - 90 - 30;
                double posX = (page.Width - width) / 2;
                double posY = 95;

                gfx.DrawImage(xImage, posX, posY, width, height);
                double recHeight = epoca.Equals("") ?  height : height - 49;
                gfx.DrawRectangle(new XPen(XColors.Black, 1), posX, posY, width, recHeight);
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
                ImprimeLogo = false;
            }

        }

        Panel novo = new Panel();

        public void creatAuxPanel()
        {
            // Painel original
            Panel original = Tela_Plotagem.painelExames;

            // Novo painel (clone)
            novo = new Panel();
            CopiarPropriedadesBasicas(original, novo);

            // Clonar os controles filhos recursivamente
            ClonarControles(original, novo);

            // Adiciona ao mesmo container (pai do original)
            original.Parent.Controls.Add(novo);

            // Posiciona acima do original
            novo.Visible = true;
            novo.BringToFront();
        }

        private void CopiarPropriedadesBasicas(Control origem, Control destino)
        {
            destino.Location = origem.Location;
            destino.Size = origem.Size;
            destino.Anchor = origem.Anchor;
            destino.Dock = origem.Dock;
            destino.BackColor = origem.BackColor;
            destino.ForeColor = origem.ForeColor;
            destino.Font = origem.Font;
            destino.Margin = origem.Margin;
            destino.Padding = origem.Padding;
            destino.Name = origem.Name + "_clone"; // evitar conflito de nomes
            destino.Text = origem.Text;
            if (destino is Panel painelDestino && origem is Panel painelOrigem)
            {
                painelDestino.BorderStyle = painelOrigem.BorderStyle;
            }
        }

        private void ClonarControles(Control origem, Control destino)
        {
            foreach (Control ctrl in origem.Controls)
            {
                Control novoCtrl = (Control)Activator.CreateInstance(ctrl.GetType());

                CopiarPropriedadesBasicas(ctrl, novoCtrl);

                // Se tiver filhos, clona também
                if (ctrl.HasChildren)
                {
                    ClonarControles(ctrl, novoCtrl);
                }

                destino.Controls.Add(novoCtrl);
            }
        }
        public static Bitmap CaptureOpenGLControl()
        {
            var gl = Tela_Plotagem.openglControl1.OpenGL;
            int width = Tela_Plotagem.openglControl1.Width;
            int height = Tela_Plotagem.openglControl1.Height;

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

        private async void btnImprimir_Click(object sender, EventArgs e)
        {
            var ini = new IniFile(@"C:\Temp\Config.ini");
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Relatório de Exames";
            bool montalterada = false;
            int newloc =0;
            float calcPont =0;

            int pagInicial = GlobVar.indice / GlobVar.namos;
            int MontInicial = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);

            creatAuxPanel();
            Tela_Plotagem.painelExames.Visible = false;


            List<int> CodigosSelect = new List<int>();
            foreach (DataGridViewRow selectedRow in dataGridViewPaginas.SelectedRows)
            {
                // Obter o valor do "CodImpressao" da linha selecionada
                int codImpressao = Convert.ToInt32(selectedRow.Cells["dataGridViewTextBoxColumn1"].Value);
                if (codImpressao > 0)
                {
                    // Adicionar o código à lista de Impressao
                    CodigosSelect.Add(codImpressao);
                }
            }
            foreach (int codigo in CodigosSelect)
            {
                DataRow rowCod = GlobVar.tbl_SelImpressao.AsEnumerable().Where(rw => rw.Field<int>("CodImpressao") == codigo).FirstOrDefault();
                int cod = Convert.ToInt32(rowCod["CodMontagem"]);

                int PagAtual = GlobVar.indice / GlobVar.namos;
                int PagImpressao = Convert.ToInt32(rowCod["PagInicial"]);

                if (PagAtual != PagImpressao)
                {
                    newloc = PagImpressao;
                    calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

                    Tela_Plotagem.camera.X = newloc * GlobVar.namos;
                    
                    GlobVar.indice = newloc * GlobVar.namos;
                    GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
                    GlobVar.ponteiroVideo = GlobVar.indice + calcPont;

                    GlobVar.indiceNumero = newloc * GlobVar.namosNumerico;
                    GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);

                    // Realize as operações gráficas no thread principal
                    Tela_Plotagem.plotagem.DesenhaGrafico((int)Tela_Plotagem.openglControl1.Height, Tela_Plotagem.qtdGrafics);

                    plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotComentatios.DesenhaComentario(Tela_Plotagem.gl);
                    plotNumerico.PlotNumerico(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotEventos.DrawTexts(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotNumerico.PlotSetas(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                }

                int CodAtual = Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[0]["CodMontagem"]);
                bool codDiferente = false;
                if(cod != CodAtual)
                {
                    montalterada = true;
                    codDiferente = true;
                    LeituraBanco.AlteraMontagem(CodAtual);

                    LeituraEmMatrizTeste.montagemSelecionadaAlterada();
                    LeituraEmMatrizTeste.referencias();

                    Tela_Plotagem.canais = new Canais(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                    Tela_Plotagem.canais.RealocPanel(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                    Tela_Plotagem.canais.quantidadeGraf(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                    Tela_Plotagem.canais.RealocButton();
                    Tela_Plotagem.canais.PainelLb_Resize();
                    Tela_Plotagem.canais.reloc();

                    Tela_Plotagem.UpdatePanelHeightInDataTable();
                    Tela_Plotagem.AjustarFonteDosLabels();
                    Tela_Plotagem.AjustarBotoesMinusEPlus();

                    int normalSize = Tela_Plotagem.painelExames.Height / GlobVar.tbl_MontagemSelecionada.Rows.Count;

                    int lastTop = 0;

                    foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
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
                    Tela_Plotagem.UpdatePanelHeightInDataTable();
                    Tela_Plotagem.AjustarFonteDosLabels();

                    // Realize as operações gráficas no thread principal
                    Tela_Plotagem.plotagem.DesenhaGrafico((int)Tela_Plotagem.openglControl1.Height, Tela_Plotagem.qtdGrafics);

                    plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotComentatios.DesenhaComentario(Tela_Plotagem.gl);
                    plotNumerico.PlotNumerico(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotEventos.DrawTexts(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
                    plotNumerico.PlotSetas(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);


                }

                DataTable montImprimirRapida = GlobVar.tbl_SelImpressao.AsEnumerable().Where(rw => rw.Field<int>("CodImpressao") == codigo).CopyToDataTable();
                bool MesmaMontagem = TabelasSaoIguaisPorColunasDeReferencia(GlobVar.tbl_MontagemSelecionada, montImprimirRapida);

                if(!MesmaMontagem)
                {
                    montalterada = true;

                }
                // Capturar a imagem do painelExames
                Bitmap bitmapPainel = new Bitmap(Tela_Plotagem.painelExames.Width, (int)(Tela_Plotagem.painelExames.Height * 1.12f));
                Tela_Plotagem.painelExames.DrawToBitmap(bitmapPainel, new Rectangle(0, 0, Tela_Plotagem.painelExames.Width, (int)(Tela_Plotagem.painelExames.Height * 1.12f)));

                Bitmap bitmapOpenGL = CaptureOpenGLControl();
                // Combinar as duas imagens lado a lado
                int totalWidth = bitmapPainel.Width + bitmapOpenGL.Width;
                int maxHeight = Math.Max(bitmapPainel.Height, bitmapOpenGL.Height);
                Bitmap bitmapCombinado = new Bitmap(totalWidth, maxHeight);
                using (Graphics g = Graphics.FromImage(bitmapCombinado))
                {
                    g.DrawImage(bitmapPainel, 0, 0);
                    g.DrawImage(bitmapOpenGL, bitmapPainel.Width + 2, 0);
                }
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
                string epoca = $"{telaPagText}";
                var row = GlobVar.tbl_Paginas.AsEnumerable().FirstOrDefault(r => r.Field<int>("NumPag") == paginaCoerente);
                string horario = row["Horario"].ToString();
                string hr = $"{horario.Substring(11)}";
                int estagio = Convert.ToInt32(row["Estagio"]);

                AdicionarImagemAoPDF(document, bitmapCombinado, ini, epoca, hr, estagio);

            }//fim

            CopiarPropriedadesBasicas(novo, Tela_Plotagem.painelExames);
            novo.Visible = false; //ocultando o panel auxiliar
            Tela_Plotagem.painelExames.Visible = true;
            newloc = pagInicial;
            calcPont = Math.Abs(GlobVar.ponteiroVideo - GlobVar.indice);

            Tela_Plotagem.camera.X = newloc * GlobVar.namos;

            GlobVar.indice = newloc * GlobVar.namos;
            GlobVar.maximaVect = GlobVar.indice + (GlobVar.segundos * GlobVar.namos);
            GlobVar.ponteiroVideo = GlobVar.indice + calcPont;
            Tela_Plotagem.hScrollBar1.Value = GlobVar.indice / GlobVar.namos;

            GlobVar.indiceNumero = newloc * GlobVar.namosNumerico;
            GlobVar.maximaNumero = GlobVar.indiceNumero + (GlobVar.segundos * GlobVar.namosNumerico);

            if (montalterada)
            {
                LeituraBanco.AlteraMontagem(MontInicial);

                LeituraEmMatrizTeste.montagemSelecionadaAlterada();
                LeituraEmMatrizTeste.referencias();

                Tela_Plotagem.canais = new Canais(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                Tela_Plotagem.canais.RealocPanel(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                Tela_Plotagem.canais.quantidadeGraf(GlobVar.tbl_MontagemSelecionada.Rows.Count);
                Tela_Plotagem.canais.RealocButton();
                Tela_Plotagem.canais.PainelLb_Resize();
                Tela_Plotagem.canais.reloc();

                Tela_Plotagem.UpdatePanelHeightInDataTable();
                Tela_Plotagem.AjustarFonteDosLabels();
                Tela_Plotagem.AjustarBotoesMinusEPlus();
                // Inicia uma nova tarefa em segundo plano, interrompida imediatamente se for cancelada
                Tela_Plotagem._backgroundTask = Task.Run(() =>
                {
                    try
                    {
                        LeituraEmMatrizTeste.montagemSelecionadaAlteradaTudo();
                    }
                    catch (OperationCanceledException)
                    {
                        Console.WriteLine("A tarefa em segundo plano foi cancelada.");
                    }
                });

            }
            // Realize as operações gráficas no thread principal
            Tela_Plotagem.plotagem.DesenhaGrafico((int)Tela_Plotagem.openglControl1.Height, Tela_Plotagem.qtdGrafics);

            plotEventos.DesenhaEventos(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
            plotGrafico.DesenhaGrafico(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
            plotComentatios.DesenhaComentario(Tela_Plotagem.gl);
            plotNumerico.PlotNumerico(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
            plotEventos.DrawTexts(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);
            plotNumerico.PlotSetas(GlobVar.tbl_MontagemSelecionada.Rows.Count, Tela_Plotagem.gl, GlobVar.desenhoLoc);


            foreach (var sele in listBoxArquivos.SelectedItems)
            {
                string arquivo = sele.ToString();
                string caminhoImagem = Path.Combine(Path.GetDirectoryName(GlobVar.bDataFile), arquivo);

                if (File.Exists(caminhoImagem))
                {
                    using (Bitmap bmp = new Bitmap(caminhoImagem))
                    {
                        AdicionarImagemAoPDF(document, bmp, ini);
                    }
                }
            }

            // Salvar o PDF completo
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
                saveFileDialog.Title = "Salvar relatório como";
                saveFileDialog.FileName = "RelatorioCompleto.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string nomePDF = saveFileDialog.FileName;
                    document.Save(nomePDF);
                    document.Close();
                    MessageBox.Show("PDF gerado com sucesso!", "Concluído", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    // O usuário cancelou o salvamento
                    document.Close();
                }
            }
        }
    }
}
