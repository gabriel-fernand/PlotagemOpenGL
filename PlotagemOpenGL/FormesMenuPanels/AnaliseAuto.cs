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
        Apneia ap;
        int codCanal;

        public AnaliseAuto()
        {
            InitializeComponent();
            ap = new Apneia();
            this.FormClosing += AnaliseAuto_FormClosing;
        }

        private void AnaliseAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ap != null)
            {
                ap.Close();
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

                ap.Show();

            }
            else
            {
                AmpliApneia.Text = ">>";
                ap.Hide();
            }
        }
    }
}
