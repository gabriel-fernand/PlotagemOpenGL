using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiMontagemForm
{
    public partial class AssociarLaudo : Form
    {
        private const string DiretorioLaudos = @"C:\Temp\Laudos";

        public AssociarLaudo()
        {
            InitializeComponent();
            PreencherListBoxComLaudos();
            PreencherListBoxComHipno();
            listLaudosDisponiveis.SelectionMode = SelectionMode.One;
            listHipnos.SelectionMode = SelectionMode.One;
            listBox2.SelectionMode = SelectionMode.One;


        }

        private void PreencherListBoxComLaudos()
        {
            string caminho = @"C:\Temp\Laudos";
            if (!Directory.Exists(caminho))
            {
                return;
            }

            // Limpa o ListBox antes de adicionar os itens
            listLaudosDisponiveis.Items.Clear();

            string[] arquivos = Directory.GetFiles(caminho, "*.doc");
            foreach (string arquivo in arquivos)
            {
                listLaudosDisponiveis.Items.Add(Path.GetFileNameWithoutExtension(arquivo));
            }
        }
        private void PreencherListBoxComHipno()
        {
            listHipnos.Items.Clear();

            if (GlobVar.tbl_JanelaResumo != null && GlobVar.tbl_JanelaResumo.Rows.Count > 0)
            {
                // Usa LINQ para ordenar as linhas pela coluna DescrJanela
                var linhasOrdenadas = GlobVar.tbl_JanelaResumo.AsEnumerable()
                                        .OrderBy(rw => rw["DescrJanela"]);

                foreach (var rw in linhasOrdenadas)
                {
                    string descrHipno = rw["DescrJanela"].ToString();
                    listHipnos.Items.Add(descrHipno);
                }
            }
        }

        private void FecharBut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listLaudosDisponiveis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listLaudosDisponiveis.SelectedItems != null && listLaudosDisponiveis.SelectedItems.Count > 0)
            {
                foreach (var item in listLaudosDisponiveis.SelectedItems)
                {
                    string textoItem = item.ToString();

                    label5.Text = @$"'{textoItem}'";
                    LaudoSelecionado.Text = $"{textoItem}";
                }
            }
        }

        private void CopiarBut_Click(object sender, EventArgs e)
        {
            string caminho = @"C:\Temp\Laudos";
            string novoNome = NovoModeloDeLaudo.Text.Trim();

            // 1. Verificar se o campo do novo nome está preenchido
            if (string.IsNullOrEmpty(novoNome))
            {
                MessageBox.Show("Digite o nome para o novo modelo de laudo.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Verificar se algum laudo está selecionado
            if (listLaudosDisponiveis.SelectedItem == null)
            {
                MessageBox.Show("Selecione um laudo para copiar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Obter o nome do arquivo selecionado
            string nomeArquivoSelecionado = listLaudosDisponiveis.SelectedItem.ToString();
            string caminhoArquivoOrigem = Path.Combine(caminho, nomeArquivoSelecionado + ".doc");
            string caminhoArquivoDestino = Path.Combine(caminho, novoNome + ".doc");

            // 4. Verificar se já existe arquivo com o novo nome
            if (File.Exists(caminhoArquivoDestino))
            {
                MessageBox.Show("Já existe um laudo com esse nome.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 5. Copiar o arquivo
                File.Copy(caminhoArquivoOrigem, caminhoArquivoDestino);
                MessageBox.Show("Arquivo copiado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 6. Atualiza a lista
                PreencherListBoxComLaudos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
