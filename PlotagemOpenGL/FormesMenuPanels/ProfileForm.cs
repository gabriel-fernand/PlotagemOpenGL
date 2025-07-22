using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
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

        private BloqueavelRadioButton Adulto;
        private BloqueavelRadioButton Infantil;
        private BloqueavelRadioButton Bebe;

        public ProfileForm(int codPaciente)
        {
            InitializeComponent();
            Adulto = new BloqueavelRadioButton() { Text = "Adulto", Location = new Point(15, 26), Tag = "A" };
            Infantil = new BloqueavelRadioButton() { Text = "Infantil", Location = new Point(15, 55), Tag = "I" };
            Bebe = new BloqueavelRadioButton() { Text = "Bebê", Location = new Point(15, 84), Tag = "B" };
            groupBox1.Controls.AddRange(new Control[] { Adulto, Infantil, Bebe });

            Adulto.CheckedChanged += RadioGroup_CheckedChanged;
            Infantil.CheckedChanged += RadioGroup_CheckedChanged;
            Bebe.CheckedChanged += RadioGroup_CheckedChanged;

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
                txtArquivo.Text = GlobVar.textFile.Substring(12, 8);

                string ADInf = dadosExame["AdInf"].ToString();
                if (!string.IsNullOrEmpty(ADInf))
                {
                    switch (ADInf)
                    {
                        case "A":
                            Adulto.Checked = true;
                            break;
                        case "I":
                            Infantil.Checked = true;
                            break;
                        case "B":
                            Bebe.Checked = true;
                            break;
                        case "C":
                            Infantil.Checked = true;
                            break;
                    }
                }

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

                bloqueio = true;
                SetBloqueioRadioButtonsNoGroupBox(true); // Deslbloqueia todos do groupBox1
                BloquearRadioButtons(true);

            }
        }
        private bool bloqueio = true;

        private void SetBloqueioRadioButtonsNoGroupBox(bool bloqueio)
        {
            foreach (Control ctrl in groupBox1.Controls)
            {
                if (ctrl is BloqueavelRadioButton rb)
                    rb.Bloqueado = bloqueio;
            }
        }
        private Panel overlayPanel = null;



        private void BloquearRadioButtons(bool bloquear)
        {
            if (bloquear)
            {
                if (overlayPanel == null)
                {
                    overlayPanel = new Panel
                    {
                        BackColor = Color.Transparent,
                        Location = groupBox1.Location,
                        Size = groupBox1.ClientSize,
                        Cursor = Cursors.No // Opcional: mostra cursor de bloqueado
                    };
                    overlayPanel.BringToFront();
                    groupBox1.Controls.Add(overlayPanel);
                    overlayPanel.BringToFront();
                }
            }
            else
            {
                if (overlayPanel != null)
                {
                    groupBox1.Controls.Remove(overlayPanel);
                    overlayPanel.Dispose();
                    overlayPanel = null;
                }
            }
        }
        private void RadioGroup_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (bloqueio)
            {
                var rb = (RadioButton)sender;
                rb.Checked = dadosExame["AdInf"].ToString() == rb.Tag.ToString();
            }
            */
        }
        // Associe para os três
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
                bloqueio = false;

                SetBloqueioRadioButtonsNoGroupBox(false); // Bloqueia todos do groupBox1
                BloquearRadioButtons(false);
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
                bloqueio = true;
                SetBloqueioRadioButtonsNoGroupBox(true); // Deslbloqueia todos do groupBox1
                BloquearRadioButtons(true);
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
                dadosExame["IdadeAno"] = CalculaIdade();
                dadosExame["Sexo"] = ConvertSexoToDb(cmbSexo.SelectedItem?.ToString());
                dadosExame["DataRealizacao"] = dtpDataExame.Value;
                dadosExame["Email"] = string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : txtEmail.Text;
                dadosExame["Cpf"] = string.IsNullOrWhiteSpace(txtCpf.Text) ? DBNull.Value : txtCpf.Text;
                dadosExame["MedicoSolicitante"] = string.IsNullOrWhiteSpace(txtMedicoSolicitante.Text) ? DBNull.Value : txtMedicoSolicitante.Text;
                dadosExame["Rg"] = string.IsNullOrWhiteSpace(txtRg.Text) ? DBNull.Value : txtRg.Text;
                dadosExame["Observacao"] = string.IsNullOrWhiteSpace(txtObservacao.Text) ? DBNull.Value : txtObservacao.Text;
                dadosExame["AdInf"] = Adulto.Checked ? "A" : Infantil.Checked ? "C" : "B";
                //AtualizarDadosExame(codPaci);

                string sql = @"UPDATE tbl_DadosExame SET
        Nome = ?, Altura = ?, Peso = ?, DataNascimento = ?, IdadeAno = ?, Sexo = ?, DataRealizacao = ?,
        Email = ?, Cpf = ?, MedicoSolicitante = ?, Rg = ?, Observacao = ?, AdInf = ?
        WHERE CodPaciente = ?"; // Ajuste conforme o nome da PK!

                using (var cmd = new OleDbCommand(sql, GlobVar.ConnectionBDdat))
                {
                    cmd.Parameters.AddWithValue("?", dadosExame["Nome"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Altura"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Peso"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["DataNascimento"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["IdadeAno"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Sexo"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["DataRealizacao"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Email"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Cpf"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["MedicoSolicitante"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Rg"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["Observacao"]);
                    cmd.Parameters.AddWithValue("?", dadosExame["AdInf"]);
                    cmd.Parameters.AddWithValue("?", codPaci); // ou outro PK

                    cmd.ExecuteNonQuery();
                }
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

        private int CalculaIdade()
        {
            // Obtém o ano de nascimento a partir do DateTimePicker
            int anoNascimento = dtpDataNascimento.Value.Year;

            // Obtém o ano atual
            int anoAtual = DateTime.Now.Year;

            // Calcula a idade inicial
            int idade = anoAtual - anoNascimento;

            // Ajusta a idade se o aniversário ainda não foi comemorado este ano
            if (dtpDataNascimento.Value.Date > DateTime.Now.Date.AddYears(-idade))
            {
                idade--;
            }

            return idade;
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

                    using (OleDbTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            int idade = CalculaIdade();
                            string sql = @"
                                UPDATE tbl_DadosExame 
                                SET 
                                DataRealizacao = @DataRealizacao, 
                                Nome = @Nome, 
                                Altura = @Altura, 
                                Peso = @Peso, 
                                DataNascimento = @DataNascimento, 
                                IdadeAno = @IdadeAno, 
                                Sexo = @Sexo, 
                                Email = @Email, 
                                Cpf = @Cpf, 
                                MedicoSolicitante = @MedicoSolicitante, 
                                Rg = @Rg, 
                                Observacao = @Observacao 
                                WHERE 
                                CodPaciente = @CodPaciente";

                            using (OleDbCommand command = new OleDbCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@DataRealizacao", dtpDataExame.Value);
                                command.Parameters.AddWithValue("@Nome", txtNome.Text);
                                command.Parameters.AddWithValue("@Altura", double.TryParse(txtAltura.Text, out double altura) ? (object)altura : DBNull.Value);
                                command.Parameters.AddWithValue("@Peso", int.TryParse(txtPeso.Text, out int peso) ? (object)peso : DBNull.Value);
                                command.Parameters.AddWithValue("@DataNascimento", dtpDataNascimento.Value);
                                command.Parameters.AddWithValue("@IdadeAno", idade);
                                command.Parameters.AddWithValue("@Sexo", ConvertSexoToDb(cmbSexo.SelectedItem?.ToString()));
                                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                                command.Parameters.AddWithValue("@Cpf", txtCpf.Text);
                                command.Parameters.AddWithValue("@MedicoSolicitante", txtMedicoSolicitante.Text);
                                command.Parameters.AddWithValue("@Rg", txtRg.Text);
                                command.Parameters.AddWithValue("@Observacao", txtObservacao.Text);
                                command.Parameters.AddWithValue("@CodPaciente", codPaciente);

                                command.ExecuteNonQuery();
                                transaction.Commit();
                                //MessageBox.Show("Dados atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show($"Erro ao atualizar os dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}


public class BloqueavelRadioButton : RadioButton
{
    public bool Bloqueado { get; set; } = false;

    protected override void WndProc(ref Message m)
    {
        // 0x201 = WM_LBUTTONDOWN
        // 0x203 = WM_LBUTTONDBLCLK
        if (Bloqueado && (m.Msg == 0x201 || m.Msg == 0x203))
            return;
        base.WndProc(ref m);
    }


}