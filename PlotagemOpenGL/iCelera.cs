using PlotagemOpenGL.auxi;
using PlotagemOpenGL.FormesMenuPanels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL
{
    public partial class iCelera : Form
    {
        Tela_Plotagem exame;

        public static FormVideo telinha;

        public iCelera()
        {
            InitializeComponent();
            LeitorDiretorio.LeituraDiretorio();
            LeituraBanco.BancoConifg();
            AtualizarLabelsComArquivos();
            groupBox1.Paint += GroupBox1_Paint;
            groupBox2.Paint += GroupBox1_Paint;
            ConfigurarMouseEventosParaLabels();    
            exame = new Tela_Plotagem();
            telinha = new FormVideo();
            telinha.Owner = this;
            telinha.TopMost = true;
            this.FormClosing += ICelera_FormClosing;

            // Bloqueia a maximização do formulário
            this.MaximizeBox = false; // Remove o botão de maximizar
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Define um estilo fixo
        }

        private void ICelera_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Fechar formulários secundários
            telinha?.Close();

            // Forçar saída, se necessário
            Application.Exit();
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }
        private async void SelecionarExame_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    // Configurações do diálogo
                    openFileDialog.Filter = "Arquivos DAT (*.dat)|*.dat"; // Filtra para arquivos .dat
                    openFileDialog.Title = "Selecione um arquivo .dat";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Diretório inicial

                    // Exibe o diálogo de seleção
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string nomeArquivo = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                        // Obtém o caminho completo do arquivo selecionado
                        string arquivoSelecionado = openFileDialog.FileName;
                        // Exibe o diretório do arquivo
                        string diretorioArquivo = Path.GetDirectoryName(arquivoSelecionado);
                        string diretorioDatMdb = diretorioArquivo +"\\"+ nomeArquivo + ".mdb";

                        //MessageBox.Show($"Arquivo selecionado: {arquivoSelecionado}\nDiretório: {diretorioArquivo}\nmdb diretorio: {diretorioDatMdb}");

                        // Retorna ou utiliza o diretório conforme necessário
                        await Task.Run(() => ProcessarArquivo(arquivoSelecionado));
                        if (!string.IsNullOrEmpty(arquivoSelecionado))
                        {
                            // Exibe os diretórios selecionados
                            //MessageBox.Show($"Diretório selecionado: {diretorioDat} {diretorioMdb}", "Informação");

                            // Caminho do arquivo
                            string filePath = @"C:\Temp\Diretorios.txt";

                            if (File.Exists(filePath))
                            {
                                // Lê o conteúdo do arquivo
                                string fileContent = File.ReadAllText(filePath);

                                // Divide o conteúdo em diretórios
                                string[] diretorios = fileContent.Split(',');

                                // Substitui os dois primeiros diretórios, se existirem
                                if (diretorios.Length >= 2)
                                {
                                    diretorios[0] = arquivoSelecionado;
                                    diretorios[1] = diretorioDatMdb;

                                    // Junta os diretórios novamente com vírgulas
                                    string updatedContent = string.Join(",", diretorios);

                                    // Escreve o conteúdo atualizado de volta no arquivo
                                    File.WriteAllText(filePath, updatedContent);


                                    await exame.InitializeAsync(); // Aguarde a inicialização assíncrona
                                    exame.Show();
                                    this.Hide();

                                    //MessageBox.Show("Os diretórios foram atualizados com sucesso!", "Sucesso");
                                }
                                else
                                {
                                    MessageBox.Show("O arquivo não contém diretórios suficientes para atualizar.", "Erro");
                                }
                            }
                            else
                            {
                                MessageBox.Show("O arquivo Diretorios.txt não foi encontrado.", "Erro");
                            }
                        }
                        else
                        {
                            MessageBox.Show("A tag do Label está vazia ou não foi definida.", "Aviso");
                        }

                    }
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Exemplo de método para processar o arquivo selecionado
        private void ProcessarArquivo(string caminhoArquivo)
        {
            // Lógica para processar o arquivo .dat
            // Por exemplo, ler o conteúdo, validar o arquivo, etc.
        }
        private async void examClick(object sender, EventArgs e)
        {
            if (sender is Label label) // Verifica se o remetente do evento é um Label
            {
                string diretorioDat = label.Tag?.ToString() + ".Dat"; // Adiciona extensão .Dat
                string diretorioMdb = label.Tag?.ToString() + ".mdb"; // Adiciona extensão .mdb

                if (!string.IsNullOrEmpty(diretorioDat))
                {
                    // Exibe os diretórios selecionados
                    //MessageBox.Show($"Diretório selecionado: {diretorioDat} {diretorioMdb}", "Informação");

                    // Caminho do arquivo
                    string filePath = @"C:\Temp\Diretorios.txt";

                    if (File.Exists(filePath))
                    {
                        // Lê o conteúdo do arquivo
                        string fileContent = File.ReadAllText(filePath);

                        // Divide o conteúdo em diretórios
                        string[] diretorios = fileContent.Split(',');

                        // Substitui os dois primeiros diretórios, se existirem
                        if (diretorios.Length >= 2)
                        {
                            diretorios[0] = diretorioDat;
                            diretorios[1] = diretorioMdb;

                            // Junta os diretórios novamente com vírgulas
                            string updatedContent = string.Join(",", diretorios);

                            // Escreve o conteúdo atualizado de volta no arquivo
                            File.WriteAllText(filePath, updatedContent);


                            await exame.InitializeAsync(); // Aguarde a inicialização assíncrona
                            exame.Show();
                            this.Hide();

                            //MessageBox.Show("Os diretórios foram atualizados com sucesso!", "Sucesso");
                        }
                        else
                        {
                            MessageBox.Show("O arquivo não contém diretórios suficientes para atualizar.", "Erro");
                        }
                    }
                    else
                    {
                        MessageBox.Show("O arquivo Diretorios.txt não foi encontrado.", "Erro");
                    }
                }
                else
                {
                    MessageBox.Show("A tag do Label está vazia ou não foi definida.", "Aviso");
                }
            }
        }
        private void GroupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox groupBox = sender as GroupBox;
            if (groupBox == null) return;

            Graphics g = e.Graphics;

            // Offset para a sombra (mude para ajustar)
            int offsetX = 2;
            int offsetY = 2;
            Color shadowColor = Color.Gray;

            foreach (Control control in groupBox.Controls)
            {
                if (control is Label label)
                {
                    // Determinar posição e tamanho do texto
                    var labelPosition = label.Location;
                    var textSize = TextRenderer.MeasureText(label.Text, label.Font);

                    // Desenhar sombra
                    TextRenderer.DrawText(
                        g,
                        label.Text,
                        label.Font,
                        new Point(labelPosition.X + offsetX, labelPosition.Y + offsetY),
                        shadowColor
                    );

                    // Desenhar texto original
                    TextRenderer.DrawText(
                        g,
                        label.Text,
                        label.Font,
                        labelPosition,
                        label.ForeColor
                    );
                }
            }
        }


        private void AtualizarLabelsComArquivos()
        {
            // Caminho do diretório
            string directoryPath = @"C:\Temp\Dat";

            // Verifica se o diretório existe
            if (Directory.Exists(directoryPath))
            {
                // Obtém os arquivos .DAT no diretório e os ordena pela data de modificação mais recente
                var arquivos = new DirectoryInfo(directoryPath)
                    .GetFiles("*.DAT")
                    .OrderByDescending(file => file.LastWriteTime) // Ordena pela data de modificação
                    .ToList();

                // Número total de labels
                int totalLabels = 5;

                // Atribui os nomes dos arquivos às Labels
                for (int i = 0; i < totalLabels; i++)
                {
                    Label label = groupBox1.Controls.Find("label" + (i + 1), true).FirstOrDefault() as Label;
                    if (label != null)
                    {
                        // Se houver um arquivo correspondente, atribui o nome do arquivo
                        if (i < arquivos.Count)
                        {
                            label.Visible = true;
                            label.Text = Path.GetFileNameWithoutExtension(arquivos[i].Name);
                            string v = $@"C:\Temp\Dat\{label.Text}";
                            label.Tag = v;
                            label.Click += examClick;
                        }
                        else
                        {
                            // Caso contrário, limpa o texto da Label
                            label.Text = "";
                            label.Visible = false;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("O diretório não existe: " + directoryPath);
            }
        }

        private void ConfigurarMouseEventosParaLabels()
        {
            foreach (Control control in groupBox1.Controls)
            {
                if (control is Label label)
                {
                    // Adiciona os eventos MouseEnter e MouseLeave
                    label.MouseEnter += Label_MouseEnter;
                    label.MouseLeave += Label_MouseLeave;
                }
            }
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                if(label.Tag != null)
                {
                label.Cursor = Cursors.Hand; // Altera o cursor para a mão
                label.ForeColor = Color.Blue; // Destaca a cor do texto
                //label.Font = new Font(label.Font, FontStyle.Bold); // Torna o texto em negrito
                }
            }
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                label.Cursor = Cursors.Default; // Restaura o cursor padrão
                label.ForeColor = SystemColors.ControlText; // Restaura a cor padrão
                label.Font = new Font(label.Font, FontStyle.Regular); // Restaura o texto sem negrito
            }
        }
    }
}
