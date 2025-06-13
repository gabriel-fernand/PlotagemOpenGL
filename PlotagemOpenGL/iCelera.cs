using PdfSharp.Quality;
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
            AtualizarLabelsComArquivosRecentes();
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
                        string diretorioDatMdb = diretorioArquivo + "\\" + nomeArquivo + ".mdb";

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
            if (sender is Label label && label.Tag is string basePath)
            {
                string diretorioDat = Path.ChangeExtension(basePath, ".dat");
                string diretorioMdb = Path.ChangeExtension(basePath, ".mdb");

                if (!File.Exists(diretorioDat))
                {
                    MessageBox.Show($"Arquivo .DAT não encontrado:\n{diretorioDat}", "Erro");
                    return;
                }

                if (!File.Exists(diretorioMdb))
                {
                    MessageBox.Show($"Arquivo .MDB não encontrado:\n{diretorioMdb}", "Erro");
                    return;
                }

                // Atualizar Diretorios.txt
                string filePath = @"C:\Temp\Diretorios.txt";
                string[] diretorios = new string[2];

                if (File.Exists(filePath))
                {
                    diretorios = File.ReadAllText(filePath).Split(',');
                    if (diretorios.Length < 2)
                        diretorios = new string[2];
                }

                diretorios[0] = diretorioDat;
                diretorios[1] = diretorioMdb;

                File.WriteAllText(filePath, string.Join(",", diretorios));

                // Atualizar ARQUIVOS RECENTES no config.ini
                AtualizarArquivosRecentes(diretorioDat);

                // Iniciar exame
                await exame.InitializeAsync();
                exame.Show();
                this.Hide();
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

        IniFile ini = new IniFile(@"C:\Temp\Config.ini");
        private void AtualizarLabelsComArquivosRecentes()
        {
            int totalLabels = 5;

            for (int i = 1; i <= totalLabels; i++)
            {
                string chave = $"ARQ_{i}";
                string caminho = ini.Read("ARQUIVOS RECENTES", chave, "config.ini");

                Label label = groupBox1.Controls.Find("label" + i, true).FirstOrDefault() as Label;
                if (label != null)
                {
                    if (!string.IsNullOrWhiteSpace(caminho) && File.Exists(caminho))
                    {
                        label.Visible = true;
                        label.Text = Path.GetFileNameWithoutExtension(caminho);
                        label.Tag = caminho;
                        label.Click -= examClick; // previne múltiplas associações
                        label.Click += examClick;
                    }
                    else
                    {
                        label.Visible = false;
                        label.Text = "";
                        label.Tag = null;
                    }
                }
            }
        }
        public void AtualizarArquivosRecentes(string novoArquivo)
        {
            string iniPath = "config.ini";
            List<string> arquivos = new List<string>();

            // Carrega os arquivos atuais (ARQ_1 a ARQ_5)
            for (int i = 1; i <= 5; i++)
            {
                string val = ini.Read("ARQUIVOS RECENTES", $"ARQ_{i}", iniPath);
                if (!string.IsNullOrWhiteSpace(val))
                    arquivos.Add(val);
            }

            // Remove se já existir
            arquivos.RemoveAll(a => string.Equals(a, novoArquivo, StringComparison.OrdinalIgnoreCase));

            // Insere no topo
            arquivos.Insert(0, novoArquivo);

            // Limita a 5 arquivos
            while (arquivos.Count > 5)
                arquivos.RemoveAt(5);

            // Salva de volta no INI
            for (int i = 0; i < arquivos.Count; i++)
            {
                ini.Write("ARQUIVOS RECENTES", $"ARQ_{i + 1}", arquivos[i]);
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
                if (label.Tag != null)
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

        private void groupBox10_Click(object sender, EventArgs e)
        {
            MontagemForm mtgForm = new MontagemForm();
            mtgForm.ShowDialog();
        }
    }
}
