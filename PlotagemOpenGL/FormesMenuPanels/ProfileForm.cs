using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Busca o DataRow com os dados do paciente
            dadosExame = GlobVar.tbl_DadosExame.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("CodPaciente") == codPaciente);

            if (dadosExame != null)
            {
                // Preenche os campos com os dados
                txtNome.Text = dadosExame.Field<string>("Nome") ?? string.Empty;
                txtAltura.Text = dadosExame.Field<string>("Altura") ?? string.Empty;
                txtPeso.Text = dadosExame.Field<string>("Peso") ?? string.Empty;
                txtDataNascimento.Text = dadosExame.Field<DateTime?>("DataNascimento")?.ToString("dd/MM/yyyy") ?? string.Empty;
                txtSexo.Text = dadosExame.Field<string>("Sexo") ?? string.Empty;
                txtDataExame.Text = dadosExame.Field<DateTime?>("DataRealizacao")?.ToString("dd/MM/yyyy") ?? string.Empty;
                txtDuracao.Text = dadosExame.Field<string>("Duracao") ?? string.Empty;
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

            // Habilita ou desabilita os TextBoxes
            txtNome.Enabled = editMode;
            txtAltura.Enabled = editMode;
            txtPeso.Enabled = editMode;
            txtDataNascimento.Enabled = editMode;
            txtSexo.Enabled = editMode;
            txtDataExame.Enabled = editMode;
            txtDuracao.Enabled = editMode;
            txtEmail.Enabled = editMode;
            txtCpf.Enabled = editMode;
            txtMedicoSolicitante.Enabled = editMode;
            txtRg.Enabled = editMode;
            txtObservacao.Enabled = editMode;

            btnAlterar.Text = editMode ? "Salvar" : "Alterar";

            if (!editMode)
            {
                // Se sairmos do modo de edição, salvamos as mudanças no DataTable
                dadosExame["Nome"] = txtNome.Text;
                dadosExame["Altura"] = txtAltura.Text;
                dadosExame["Peso"] = txtPeso.Text;
                dadosExame["DataNascimento"] = DateTime.Parse(txtDataNascimento.Text);
                dadosExame["Sexo"] = txtSexo.Text;
                dadosExame["DataRealizacao"] = DateTime.Parse(txtDataExame.Text);
                dadosExame["Duracao"] = txtDuracao.Text;
                dadosExame["Email"] = txtEmail.Text;
                dadosExame["Cpf"] = txtCpf.Text;
                dadosExame["MedicoSolicitante"] = txtMedicoSolicitante.Text;
                dadosExame["Rg"] = txtRg.Text;
                dadosExame["Observacao"] = txtObservacao.Text;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
