using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
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
            // Verificar se há uma linha selecionada
            if (dataGridViewPaginas.SelectedRows.Count > 0)
            {
                // Obter o valor do "CodImpressao" da linha selecionada
                int codImpressao = Convert.ToInt32(dataGridViewPaginas.SelectedRows[0].Cells["Código"].Value);

                // Remover a linha do DataGridView
                dataGridViewPaginas.Rows.RemoveAt(dataGridViewPaginas.SelectedRows[0].Index);

                // Remover as linhas correspondentes do DataTable
                DataRow[] rowsToDelete = GlobVar.tbl_SelImpressao.Select($"CodImpressao = {codImpressao}");
                foreach (DataRow row in rowsToDelete)
                {
                    GlobVar.tbl_SelImpressao.Rows.Remove(row);
                }

                // Excluir do banco de dados
                ExcluirDoBancoDeDados(codImpressao);
            }
            else
            {
                MessageBox.Show("Por favor, selecione uma linha para excluir.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            MessageBox.Show("Exclusão realizada com sucesso.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Nenhuma linha encontrada para exclusão.", "Excluir", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir do banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
