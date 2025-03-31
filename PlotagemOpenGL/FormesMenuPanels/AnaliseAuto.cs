using PlotagemOpenGL.auxi;
using PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise;
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
    public partial class AnaliseAuto : Form
    {
        AnaliseAutomaticaApneia anApneiHipo;
        AnaliseAutomaticaDessaturacao anDessatu;
        AnaliseAutomaticaRonco anRonco;
        Apneia ap;
        Dessaturacao desa;
        Ronco ronc;
        PLM plm;
        int codCanal;

        public AnaliseAuto()
        {
            InitializeComponent();
            ap = new Apneia();
            desa = new Dessaturacao();
            ronc = new Ronco();
            plm = new PLM();
            this.FormClosing += AnaliseAuto_FormClosing;
            SatuBasal.TextChanged += SatuBasal_TextChanged;
        }

        private void AnaliseAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ap != null)
            {
                ap.Close();
                desa.Close();
                ronc.Close();
                plm.Close();
            }
        }

        public void Processar_Click(object sender, EventArgs e)
        {
            if (ApHipo.Checked)
            {
                int QualAnalisar = ap.Canula.Checked ? 13 : 8; // 13 Canula 8 Fluxo
                var row = GlobVar.tbl_MontagemSelecionada.AsEnumerable().Where(row => row.Field<int>("CodTipoCanal") == QualAnalisar).FirstOrDefault();
                codCanal = Convert.ToInt16(row["CodCanal1"]);
                anApneiHipo = new AnaliseAutomaticaApneia(80, 50, 10, 3, 30, 5, codCanal, DeleteApnHip.Checked);
            }
            if(Dessatu.Checked)
            {
                anDessatu = new AnaliseAutomaticaDessaturacao(DeleteDess.Checked, SatuBasal.Text);
            }
            if(Ronco.Checked)
            {
                int QualAnalisar = ronc.microfone.Checked ? 5 : 32;
                anRonco = new AnaliseAutomaticaRonco(QualAnalisar, DeleteRonco.Checked);
            }
        }

        private void AmpliApneia_Click(object sender, EventArgs e)
        {

            if (AmpliApneia.Text.Equals(">>"))
            {
                AmpliApneia.Text = "<<";
                // Obtém a posição do formulário principal
                int x = this.Location.X + this.Width - 7; // Posição à direita do formulário atual
                int y = this.Location.Y + 39;              // Alinhado na mesma altura do formulário atual

                // Define a posição do novo formulário
                ap.StartPosition = FormStartPosition.Manual;
                ap.Location = new Point(x, y);

                if (AmpliDessatu.Text.Equals("<<")) desa.Hide(); AmpliDessatu.Text = ">>";
                if (AmpliRonco.Text.Equals("<<")) ronc.Hide(); AmpliRonco.Text = ">>";
                if (AmpliMovPPLM.Text.Equals("<<")) plm.Hide(); AmpliMovPPLM.Text = ">>";

                ap.Show();

            }
            else
            {
                AmpliApneia.Text = ">>";
                ap.Hide();
            }
        }

        private void AmpliDessatu_Click(object sender, EventArgs e)
        {
            if (AmpliDessatu.Text.Equals(">>"))
            {
                AmpliDessatu.Text = "<<";
                // Obtém a posição do formulário principal
                int x = this.Location.X + this.Width - 7; // Posição à direita do formulário atual
                int y = this.Location.Y + 39;              // Alinhado na mesma altura do formulário atual

                // Define a posição do novo formulário
                desa.StartPosition = FormStartPosition.Manual;
                desa.Location = new Point(x, y);

                if (AmpliApneia.Text.Equals("<<")) ap.Hide(); AmpliApneia.Text = ">>";
                if (AmpliRonco.Text.Equals("<<")) ronc.Hide(); AmpliRonco.Text = ">>";
                if (AmpliMovPPLM.Text.Equals("<<")) plm.Hide(); AmpliMovPPLM.Text = ">>";

                desa.Show();

            }
            else
            {
                AmpliDessatu.Text = ">>";
                desa.Hide();
            }

        }
        private void SatuBasal_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(SatuBasal.Text, out int valor))
            {
                if (valor > 100)
                {
                    SatuBasal.Text = "100";
                }
            }
            else
            {
                SatuBasal.Text = "0"; // Caso o valor não seja um número válido
            }
        }

        private void AmpliRonco_Click(object sender, EventArgs e)
        {
            if (AmpliRonco.Text.Equals(">>"))
            {
                AmpliRonco.Text = "<<";
                // Obtém a posição do formulário principal
                int x = this.Location.X + this.Width - 7; // Posição à direita do formulário atual
                int y = this.Location.Y + 39;              // Alinhado na mesma altura do formulário atual

                // Define a posição do novo formulário
                ronc.StartPosition = FormStartPosition.Manual;
                ronc.Location = new Point(x, y);

                if (AmpliApneia.Text.Equals("<<")) ap.Hide(); AmpliApneia.Text = ">>";
                if (AmpliDessatu.Text.Equals("<<")) desa.Hide(); AmpliDessatu.Text = ">>";
                if (AmpliMovPPLM.Text.Equals("<<")) plm.Hide(); AmpliMovPPLM.Text = ">>";

                ronc.Show();

            }
            else
            {
                AmpliRonco.Text = ">>";
                ronc.Hide();
            }

        }

        private void AmpliMovPPLM_Click(object sender, EventArgs e)
        {
            if (AmpliMovPPLM.Text.Equals(">>"))
            {
                AmpliMovPPLM.Text = "<<";
                // Obtém a posição do formulário principal
                int x = this.Location.X + this.Width - 7; // Posição à direita do formulário atual
                int y = this.Location.Y + 39;              // Alinhado na mesma altura do formulário atual

                // Define a posição do novo formulário
                plm.StartPosition = FormStartPosition.Manual;
                plm.Location = new Point(x, y);

                if (AmpliApneia.Text.Equals("<<")) ap.Hide(); AmpliApneia.Text = ">>";
                if (AmpliDessatu.Text.Equals("<<")) desa.Hide(); AmpliDessatu.Text = ">>";
                if (AmpliRonco.Text.Equals("<<")) ronc.Hide(); AmpliRonco.Text = ">>";

                plm.Show();

            }
            else
            {
                AmpliMovPPLM.Text = ">>";
                plm.Hide();
            }


        }
    }
}
