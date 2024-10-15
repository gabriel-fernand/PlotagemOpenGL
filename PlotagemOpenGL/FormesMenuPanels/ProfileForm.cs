using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class ProfileForm : Form
    {
        private bool editMode = false;
        private DataRow dadosExame;
        private DataRow resumoExame;
        int codPaci;

        public ProfileForm(int codPaciente)
        {
            InitializeComponent();
            codPaci = codPaciente;
            txtNome.MaxLength = 50; // Limite de 50 caracteres para o nome
            txtEmail.MaxLength = 80; // Limite para o email, ajuste conforme necessário
            txtCpf.MaxLength = 11; // Limite para CPF
            txtRg.MaxLength = 9; // Limite para RG
            txtObservacao.MaxLength = 255; // Limite para observações
            txtMedicoSolicitante.MaxLength = 50;
            // Configurando o ComboBox do Sexo com as opções
            cmbSexo.Items.AddRange(new string[] { "Feminino", "Masculino", "Outro" });

            // Busca o DataRow com os dados do paciente baseado no CodPaciente
            dadosExame = GlobVar.tbl_DadosExame.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("CodPaciente") == codPaciente);
            resumoExame = GlobVar.tbl_ResumoExame.AsEnumerable().First();

            if (dadosExame != null)
            {
                // Preenche os campos com os dados do paciente
                txtNome.Text = dadosExame.Field<string>("Nome") ?? string.Empty;
                txtAltura.Text = ConvertToIntString(dadosExame["Altura"]);
                txtPeso.Text = ConvertToIntString(dadosExame["Peso"]);
                dtpDataNascimento.Value = dadosExame.Field<DateTime?>("DataNascimento") ?? DateTime.Today;
                cmbSexo.SelectedItem = ConvertSexo(dadosExame.Field<string>("Sexo"));
                dtpDataExame.Value = dadosExame.Field<DateTime?>("DataRealizacao") ?? DateTime.Today;
                txtEmail.Text = dadosExame.Field<string>("Email") ?? string.Empty;
                txtCpf.Text = dadosExame.Field<string>("Cpf") ?? string.Empty;
                txtMedicoSolicitante.Text = dadosExame.Field<string>("MedicoSolicitante") ?? string.Empty;
                txtRg.Text = dadosExame.Field<string>("Rg") ?? string.Empty;
                txtObservacao.Text = dadosExame.Field<string>("Observacao") ?? string.Empty;
                txtArquivo.Text = GlobVar.textFile.Substring(32, 8);

                var pagSel = GlobVar.tbl_SelImpressao.AsEnumerable().OrderByDescending(row => row.Field<int>("CodImpressao")).First();

                // Obtém os horários de início e fim do exame
                DateTime iniExame = resumoExame.Field<DateTime>("Ini_Exame");
                DateTime fimExame = resumoExame.Field<DateTime>("Fim_Exame");

                // Se o exame terminou depois da meia-noite (fim é menor que o início), adicione um dia ao fim
                if (fimExame < iniExame)
                {
                    fimExame = fimExame.AddDays(1);
                }

                // Calcula a duração do exame
                TimeSpan duracao = fimExame - iniExame;

                // Atribui a duração ao campo txtDuracao
                txtDuracao.Text = duracao.ToString(@"hh\:mm\:ss");
                PgparaImpressao.Text = Convert.ToString(pagSel[0]);
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Alterna o modo de edição
            editMode = !editMode;
            // Habilita ou desabilita os TextBoxes para edição
            if(editMode)
            {
                txtNome.ReadOnly = false;
                txtAltura.ReadOnly = false;
                txtPeso.ReadOnly = false;
                txtEmail.ReadOnly = false;
                txtCpf.ReadOnly = false;
                txtMedicoSolicitante.ReadOnly = false;
                txtRg.ReadOnly = false;
                txtObservacao.ReadOnly = false;
            }
            else
            {
                txtNome.ReadOnly = true;
                txtAltura.ReadOnly = true;
                txtPeso.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtCpf.ReadOnly = true;
                txtMedicoSolicitante.ReadOnly = true;
                txtRg.ReadOnly = true;
                txtObservacao.ReadOnly = true;
            }

            dtpDataNascimento.Enabled = editMode;
            cmbSexo.Enabled = editMode;
            dtpDataExame.Enabled = editMode;

            btnAlterar.Text = editMode ? "Salvar" : "Alterar";
            btnOk.Text = editMode ? "Cancelar" : "Ok";


            if (!editMode)
            {
                // Salvamos as alterações no DataTable
                dadosExame["Nome"] = string.IsNullOrWhiteSpace(txtNome.Text) ? DBNull.Value : txtNome.Text;
                dadosExame["Altura"] = double.TryParse(txtAltura.Text, out double altura) ? (object)altura : DBNull.Value;
                dadosExame["Peso"] = int.TryParse(txtPeso.Text, out int peso) ? (object)peso : DBNull.Value;
                dadosExame["DataNascimento"] = dtpDataNascimento.Value;
                dadosExame["Sexo"] = ConvertSexoToDb(cmbSexo.SelectedItem?.ToString());
                dadosExame["DataRealizacao"] = dtpDataExame.Value;
                dadosExame["Email"] = string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : txtEmail.Text;
                dadosExame["Cpf"] = string.IsNullOrWhiteSpace(txtCpf.Text) ? DBNull.Value : txtCpf.Text;
                dadosExame["MedicoSolicitante"] = string.IsNullOrWhiteSpace(txtMedicoSolicitante.Text) ? DBNull.Value : txtMedicoSolicitante.Text;
                dadosExame["Rg"] = string.IsNullOrWhiteSpace(txtRg.Text) ? DBNull.Value : txtRg.Text;
                dadosExame["Observacao"] = string.IsNullOrWhiteSpace(txtObservacao.Text) ? DBNull.Value : txtObservacao.Text;

                AtualizarDadosExame(codPaci);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Método auxiliar para converter valores float/int para string (para exibição nos TextBoxes)
        private string ConvertToIntString(object value)
        {
            if (value == DBNull.Value) return string.Empty;

            // Se o valor for um float, converta para int
            if (value is float floatValue)
            {
                return (floatValue).ToString();
            }

            // Se o valor for um int, converta para string
            if (value is int intValue)
            {
                return intValue.ToString();
            }

            if(value is string strValue)
            {
                return strValue.ToString();
            }

            if(value is Single sglValue)
            {
                return sglValue.ToString();
            }
            if(value is double doubleValue)
            {
                return doubleValue.ToString();
            }
            // Caso o valor seja de outro tipo, retorne vazio
            return string.Empty;
        }

        // Método para converter o valor de sexo para exibição no ComboBox
        private string ConvertSexo(string dbValue)
        {
            return dbValue switch
            {
                "F" => "Feminino",
                "M" => "Masculino",
                "O" => "Outro",
                _ => string.Empty,
            };
        }

        // Método para converter o valor do ComboBox para salvar no DataTable
        private string ConvertSexoToDb(string displayValue)
        {
            return displayValue switch
            {
                "Feminino" => "F",
                "Masculino" => "M",
                "Outro" => "O",
                _ => DBNull.Value.ToString(),
            };
        }
        private void txtNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            ApenasCaracteres(e);
        }

        private void txtAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            ApenasNumeros(e);
        }

        private void ApenasCaracteres(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ApenasNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        // Método para validar o campo txtAltura (altura no formato 1.69 ou 1,69)
        private void txtAlturaa_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Permite dígitos, ponto e vírgula, mas impede mais de um ponto ou vírgula
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Impede mais de um ponto ou vírgula
            if ((e.KeyChar == ',') && (textBox.Text.Contains(',')))
            {
                e.Handled = true;
            }

            // Limite de 5 caracteres
            if (textBox.Text.Length >= 5 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // Método para validar o campo txtPeso (somente números e limite de 4 caracteres)
        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            // Permite apenas números e teclas de controle como Backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            // Limite de 4 caracteres
            if (textBox.Text.Length >= 3 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public void AtualizarDadosExame(int codPaciente)
        {
            try
            {
                string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.bDataFile};";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Inicia uma transação
                    OleDbTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // SQL para atualizar os campos necessários
                        string sql = "UPDATE tbl_DadosExame SET DataRealizacao = @DataRealizacao, Nome = @Nome, Altura = @Altura, Peso = @Peso, DataNascimento = @DataNascimento, Sexo = @Sexo, Email = @Email, Cpf = @Cpf, MedicoSolicitante = @MedicoSolicitante, Rg = @Rg, Observacao = @Observacao WHERE CodPaciente = @CodPaciente";

                        using (OleDbCommand command = new OleDbCommand(sql, connection, transaction))
                        {
                            // Verificando comprimento dos campos antes de salvar
                            string nome = txtNome.Text.Length > 50 ? txtNome.Text.Substring(0, 50) : txtNome.Text;
                            string email = txtEmail.Text.Length > 100 ? txtEmail.Text.Substring(0, 100) : txtEmail.Text;
                            string cpf = txtCpf.Text.Length > 11 ? txtCpf.Text.Substring(0, 11) : txtCpf.Text;
                            string rg = txtRg.Text.Length > 9 ? txtRg.Text.Substring(0, 9) : txtRg.Text;
                            string observacao = txtObservacao.Text.Length > 255 ? txtObservacao.Text.Substring(0, 255) : txtObservacao.Text;

                            // Formatando a data corretamente como DateTime para inserir no banco
                            DateTime dataNascimento = dtpDataNascimento.Value.Date;

                            // Adicionando parâmetros para a query
                            command.Parameters.Add("@Nome", OleDbType.VarChar).Value = nome;
                            command.Parameters.Add("@Altura", OleDbType.Double).Value = double.TryParse(txtAltura.Text, out double altura) ? altura : 0;
                            command.Parameters.Add("@Peso", OleDbType.Double).Value = double.TryParse(txtPeso.Text, out double peso) ? peso : 0;
                            command.Parameters.Add("@DataNascimento", OleDbType.Date).Value = string.IsNullOrWhiteSpace(dtpDataNascimento.Value.ToString("dd/MM/yyyy")) ? DBNull.Value : dtpDataNascimento.Value.ToString("dd/MM/yyyy"); // Salvando a data como DateTime
                            command.Parameters.Add("@Sexo", OleDbType.VarChar).Value = cmbSexo.SelectedItem.ToString();
                            command.Parameters.Add("@Email", OleDbType.VarChar).Value = email;
                            command.Parameters.Add("@Cpf", OleDbType.VarChar).Value = cpf;
                            command.Parameters.Add("@MedicoSolicitante", OleDbType.VarChar).Value = txtMedicoSolicitante.Text.Length > 50 ? txtMedicoSolicitante.Text.Substring(0, 50) : txtMedicoSolicitante.Text;
                            command.Parameters.Add("@Rg", OleDbType.VarChar).Value = rg;
                            command.Parameters.Add("@Observacao", OleDbType.VarChar).Value = observacao;
                            command.Parameters.Add("@CodPaciente", OleDbType.Integer).Value = codPaciente;
                            command.Parameters.Add("@DataRealizacao", OleDbType.Date).Value = string.IsNullOrWhiteSpace(dtpDataExame.Value.ToString("dd/MM/yyyy")) ? DBNull.Value : dtpDataExame.Value.ToString("dd/MM/yyyy");

                            // Executa a query de atualização
                            command.ExecuteNonQuery();
                        }

                        // Confirma a transação
                        transaction.Commit();
                        MessageBox.Show("Dados atualizados com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        // Reverte a transação em caso de erro
                        transaction.Rollback();
                        MessageBox.Show($"Erro ao atualizar os dados: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }

    }
}
