using PlotagemOpenGL.auxi;
using PlotagemOpenGL.BD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class PagSelecionadasImpressao : Form
    {
        public PagSelecionadasImpressao()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Define o estilo da borda como fixo
            this.MaximizeBox = false; // Desativa o botão de maximizar
            CarregarDadosPaginacao();
            CarregarArquivosDiretorio(@"C:\Users\dev_i\source\repos\Dat\", $"{GlobVar.textFile.Substring(32, 8)}_tela");
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

        private void btnVisualizar_Click(object sender, EventArgs e)
        {

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
                    if(codImpressao > 0)
                    {
                        // Adicionar o código à lista de exclusão
                        codigosParaExcluir.Add(codImpressao);
                    }
                }

                // Remover as linhas do DataGridView
                foreach (DataGridViewRow selectedRow in dataGridViewPaginas.SelectedRows)
                {
                    if(!selectedRow.IsNewRow)
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
            try {
                dataGridViewPaginas.SelectAll();
                listBoxArquivos.Select();
            } 
            catch { }
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
                        if(paginaCoerente % 10 ==  0)
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
    }
}
