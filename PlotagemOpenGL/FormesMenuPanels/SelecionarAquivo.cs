using Microsoft.VisualBasic;
using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tensorflow;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class SelecionarAquivo : Form
    {
        string extensao = "";
        string path;

        public SelecionarAquivo()
        {
            InitializeComponent();
            TiposExames.TextChanged += comboBoxTiposExames_SelectedIndexChanged;

            Discos.Items.Clear();

            // Obtém todos os drives disponíveis
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                // Só mostra discos que estão prontos (com mídia, montados)
                if (drive.IsReady)
                {
                    // Exemplo: "C:\ (Fixed)" ou "D:\ (Removable)"
                    string displayName = $"{drive.Name}";
                    Discos.Items.Add(displayName);
                }
            }

            // Opcional: seleciona o primeiro disco, se houver
            if (Discos.Items.Count > 0)
                Discos.SelectedIndex = 0;

            // Supondo dataGridView1 já presente no seu form:
            Exames.Columns.Clear();
            Exames.Columns.Add("Arquivo", "Arquivo");
            Exames.Columns.Add("Data", "Data");
            Exames.Columns.Add("Tamanho", "Tamanho");
            Exames.Columns.Add("Paciente", "Paciente");

            // Deixe a seleção apenas "FullRowSelect", por boas práticas
            Exames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            PreencherTiposExames();

            CarregarDiretorioRaiz();
            Diretorios.BeforeExpand += treeView1_BeforeExpand;
            if (TiposExames.Items.Count > 0) TiposExames.SelectedIndex = 0;
            Diretorios.AfterSelect += Diretorios_AfterSelect;

            //path = Diretorios.SelectedNode.FullPath;
        }

        private void Diretorios_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                path = Path.GetFullPath(Diretorios.SelectedNode.FullPath);

                // Busca arquivos conforme a extensão
                string[] arquivos;
                if (extensao == ".") // Se for "Todos os Arquivos"
                {
                    arquivos = Directory.GetFiles(path); // Todos os arquivos
                }
                else
                {
                    arquivos = Directory.GetFiles(path, "*" + extensao); // Só arquivos da extensão
                }

                // Limpa linhas antigas
                Exames.Rows.Clear();

                foreach (string arquivo in arquivos)
                {
                    string[] aux = DadosDat(arquivo); // Retorna [Paciente, ..., Data, ...]
                    string[] aux2 = new string[4];
                    aux2[0] = Path.GetFileNameWithoutExtension(arquivo); // Arquivo
                    aux2[1] = aux[2]; // Data
                    var fileInfo = new FileInfo(arquivo);
                    aux2[2] = FormatFileSize(fileInfo.Length); // Tamanho
                    aux2[3] = aux[0]; // Paciente
                    Exames.Rows.Add(aux2);
                }

            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "Acesso negado ao diretório:\n" + path,
                    "Erro de Permissão",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return; // Sai do método para evitar erro
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Erro ao acessar o diretório:\n" + path + "\n\n" + ex.Message,
                    "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }
        }
        private void CarregarDiretorioRaiz()
        {
            string caminhoRaiz = $@"{Discos.Text}";
            TreeNode raiz = new TreeNode(caminhoRaiz)
            {
                Tag = caminhoRaiz
            };
            Diretorios.Nodes.Add(raiz);
            AdicionarSubDiretorios(raiz);
        }
        private void AdicionarSubDiretorios(TreeNode node)
        {
            string path = node.Tag.ToString();
            try
            {
                string[] diretorios = Directory.GetDirectories(path);
                foreach (string dir in diretorios)
                {
                    TreeNode novoNo = new TreeNode(Path.GetFileName(dir))
                    {
                        Tag = dir
                    };
                    node.Nodes.Add(novoNo);
                    // Adiciona um nó vazio apenas como placeholder para mostrar o símbolo de expandir [+]
                    novoNo.Nodes.Add("");
                }
            }
            catch { /* Tratamento de exceção */ }
        }
        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // Só carrega se ainda não carregou
            if (e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == "")
            {
                e.Node.Nodes.Clear();
                AdicionarSubDiretorios(e.Node);
            }
        }
        // Exemplos dos tipos de exames
        private void PreencherTiposExames()
        {
            TiposExames.Items.Clear();
            TiposExames.Items.Add("iCelera Tecnologia (*.dat)");
            TiposExames.Items.Add("EDF 1 (*.edf)");
            TiposExames.Items.Add("EDF 2 (*.rec)");
            TiposExames.Items.Add("Todos Arquivos (*.*)");
        }
        // Quando o usuário selecionar uma opção
        private void comboBoxTiposExames_SelectedIndexChanged(object sender, EventArgs e)
        {
            string textoSelecionado = TiposExames.SelectedItem.ToString();
            extensao = ExtrairExtensao(textoSelecionado);
        }
        // Função para extrair o texto entre parênteses
        private string ExtrairExtensao(string texto)
        {
            var match = Regex.Match(texto, @"\(([^)]*)\)");
            if (match.Success)
            {
                string entreParenteses = match.Groups[1].Value; // Ex: "*.dat" ou "*.*"
                                                                // Caso seja "*.*", retorna somente "."
                if (entreParenteses == "*.*")
                    return ".";
                // Caso normal: pega o ponto e o restante
                int posPonto = entreParenteses.IndexOf('.');
                if (posPonto >= 0)
                    return entreParenteses.Substring(posPonto); // Ex: ".dat"
                                                                // Fallback
                return entreParenteses;
            }
            return string.Empty;
        }
        public static string[] DadosDat(string diretorio)
        {
            string[] dados = new string[3];
            byte[] WATec = new byte[19];
            byte[] buffer0 = new byte[30];
            byte[] buffer1 = new byte[100];
            byte[] buffer2 = new byte[4];
            byte[] buffer3 = new byte[10];
            byte[] anda = new byte[19];

            using (FileStream fs = new FileStream(diretorio, FileMode.Open, FileAccess.Read))
            {
                fs.Read(WATec, 0, WATec.Length);

                string tipo = (Encoding.UTF8.GetString(WATec));
                // Remove nulos e espaços no fim para evitar pegar '\0'
                tipo = tipo.TrimEnd('\0', ' ', '\t', '\r', '\n');


                fs.Read(buffer0, 0, buffer0.Length);

                fs.Read(buffer1, 0, buffer1.Length);
                fs.Read(buffer2, 0, buffer2.Length);
                fs.Read(anda, 0, anda.Length);
                fs.Read(buffer3, 0, buffer3.Length);
                //50

                dados[0] = (Encoding.UTF8.GetString(buffer1));
                dados[1] = (Encoding.UTF8.GetString(buffer2));
                dados[2] = (Encoding.UTF8.GetString(buffer3));
            }

            return dados;
        }
        private void Fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static string FormatFileSize(long bytes)
        {
            const double KB = 1024;
            const double MB = KB * 1024;
            const double GB = MB * 1024;

            if (bytes < KB)
                return bytes + " B";
            else if (bytes < MB)
                return (bytes / KB).ToString("F0") + " KB";
            else if (bytes < GB)
                return (bytes / MB).ToString("F0") + " MB";
            else
                return (bytes / GB).ToString("F0") + " GB";
        }
        private void Abrir_Click(object sender, EventArgs e)
        {
            if (Exames.SelectedRows.Count > 0 && Exames.SelectedRows.Count < 2)
            {
                if (Exames.SelectedRows.Count == 1) // Só uma linha selecionada
                {

                    DataGridViewRow row = Exames.SelectedRows[0];

                    string Arquivo = $"{row.Cells["Arquivo"].Value?.ToString()}{extensao}";

                    string diretorio = Path.Combine(path, Arquivo);

                    GlobVar.FileName = diretorio;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void Exames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Garante que não é o header
            {
                DataGridViewRow row = Exames.SelectedRows[0];

                string Arquivo = $"{row.Cells["Arquivo"].Value?.ToString()}{extensao}";

                string diretorio = Path.Combine(path, Arquivo);

                GlobVar.FileName = diretorio;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Excluir_Click(object sender, EventArgs e)
        {
            if (Exames.SelectedRows.Count == 1) // Só uma linha selecionada
            {
                DataGridViewRow row = Exames.SelectedRows[0];
                string Arquivo = $"{row.Cells["Arquivo"].Value?.ToString()}{extensao}";
                string diretorio = Path.Combine(path, Arquivo);

                // Pergunta ao usuário antes de excluir
                DialogResult result = MessageBox.Show($"Deseja realmente excluir o arquivo {Arquivo} ?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (File.Exists(diretorio))
                        {
                            File.Delete(diretorio);
                        }
                        // Remove a linha do DataGridView
                        Exames.Rows.Remove(row);

                        MessageBox.Show("Arquivo excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao excluir arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione apenas uma linha para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Copiar_Click(object sender, EventArgs e)
        {
            if (Exames.SelectedRows.Count == 1) // Só uma linha selecionada
            {
                DataGridViewRow row = Exames.SelectedRows[0];
                string Arquivo = $"{row.Cells["Arquivo"].Value?.ToString()}{extensao}";
                string diretorio = Path.Combine(path, Arquivo);

                // Abrir dialog para salvar a cópia
                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.FileName = Arquivo; // Nome sugerido
                    saveDialog.Filter = "Todos os arquivos (*.*)|*.*"; // Pode adaptar o filtro conforme a extensão do seu arquivo
                    saveDialog.Title = "Salvar cópia como...";

                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            File.Copy(diretorio, saveDialog.FileName, true);
                            MessageBox.Show("Arquivo copiado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao copiar o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione exatamente uma linha para copiar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Renomear_Click(object sender, EventArgs e)
        {
            if (Exames.SelectedRows.Count == 1)
            {
                DataGridViewRow row = Exames.SelectedRows[0];
                string ArquivoAtual = $"{row.Cells["Arquivo"].Value?.ToString()}{extensao}";
                string diretorioAtual = Path.Combine(path, ArquivoAtual);

                // Solicita o novo nome para o usuário (sem extensão, pode adaptar!)
                string novoNome = Interaction.InputBox("Digite o novo nome do arquivo (sem extensão):", "Renomear Arquivo", row.Cells["Arquivo"].Value?.ToString());

                if (!string.IsNullOrWhiteSpace(novoNome))
                {
                    string novoArquivo = $"{novoNome}{extensao}";
                    string novoDiretorio = Path.Combine(path, novoArquivo);

                    try
                    {
                        // Renomeia o arquivo
                        File.Move(diretorioAtual, novoDiretorio);

                        // Atualiza o DataGridView (opcional, adapte conforme seu refresh)
                        row.Cells["Arquivo"].Value = novoNome;
                        MessageBox.Show("Arquivo renomeado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao renomear o arquivo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione exatamente uma linha para renomear.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static string InputBox(string title, string promptText, string value = "")
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancelar";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            return dialogResult == DialogResult.OK ? textBox.Text : null;
        }


    }
}
