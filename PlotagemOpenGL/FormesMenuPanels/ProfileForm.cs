using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class ProfileForm : Form
    {
        private bool editMode = false;
        private DataRow dadosExame;

        public ProfileForm(int codPaciente)
        {
            InitializeComponent();

            // Configurando o ComboBox do Sexo com as opções
            cmbSexo.Items.AddRange(new string[] { "Feminino", "Masculino", "Outro" });

            // Busca o DataRow com os dados do paciente baseado no CodPaciente
            dadosExame = GlobVar.tbl_DadosExame.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("CodPaciente") == codPaciente);

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
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            // Alterna o modo de edição
            editMode = !editMode;

            // Habilita ou desabilita os TextBoxes para edição
            txtNome.Enabled = editMode;
            txtAltura.Enabled = editMode;
            txtPeso.Enabled = editMode;
            dtpDataNascimento.Enabled = editMode;
            cmbSexo.Enabled = editMode;
            dtpDataExame.Enabled = editMode;
            txtEmail.Enabled = editMode;
            txtCpf.Enabled = editMode;
            txtMedicoSolicitante.Enabled = editMode;
            txtRg.Enabled = editMode;
            txtObservacao.Enabled = editMode;

            btnAlterar.Text = editMode ? "Salvar" : "Alterar";
            btnOk.Text = editMode ? "Cancelar" : "Ok";


            if (!editMode)
            {
                // Salvamos as alterações no DataTable
                dadosExame["Nome"] = string.IsNullOrWhiteSpace(txtNome.Text) ? DBNull.Value : txtNome.Text;
                dadosExame["Altura"] = int.TryParse(txtAltura.Text, out int altura) ? (object)altura : DBNull.Value;
                dadosExame["Peso"] = int.TryParse(txtPeso.Text, out int peso) ? (object)peso : DBNull.Value;
                dadosExame["DataNascimento"] = dtpDataNascimento.Value;
                dadosExame["Sexo"] = ConvertSexoToDb(cmbSexo.SelectedItem?.ToString());
                dadosExame["DataRealizacao"] = dtpDataExame.Value;
                dadosExame["Email"] = string.IsNullOrWhiteSpace(txtEmail.Text) ? DBNull.Value : txtEmail.Text;
                dadosExame["Cpf"] = string.IsNullOrWhiteSpace(txtCpf.Text) ? DBNull.Value : txtCpf.Text;
                dadosExame["MedicoSolicitante"] = string.IsNullOrWhiteSpace(txtMedicoSolicitante.Text) ? DBNull.Value : txtMedicoSolicitante.Text;
                dadosExame["Rg"] = string.IsNullOrWhiteSpace(txtRg.Text) ? DBNull.Value : txtRg.Text;
                dadosExame["Observacao"] = string.IsNullOrWhiteSpace(txtObservacao.Text) ? DBNull.Value : txtObservacao.Text;
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
                return ((int)floatValue).ToString();
            }

            // Se o valor for um int, converta para string
            if (value is int intValue)
            {
                return intValue.ToString();
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
    }
}
