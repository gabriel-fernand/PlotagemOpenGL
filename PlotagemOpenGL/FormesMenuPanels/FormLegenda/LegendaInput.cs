using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormLegenda
{
    public partial class LegendaInput : Form
    {
        public static int tagCodCanal = Tela_Plotagem.tagCodCanal;
        public static DataRow dt;

        public LegendaInput()
        {
            InitializeComponent();
            load();
        }

        public static void load()
        {
            dt = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == Tela_Plotagem.tagCodCanal);

            if(dt != null)
            {
                textBoxLegenda.Text = dt["Legenda"].ToString();
            }

        }

        public void buttonOK_Click(object sender, EventArgs e)
        {
            // Lógica para lidar com a confirmação do usuário
            string legenda = textBoxLegenda.Text;

            dt["Legenda"] = textBoxLegenda.Text;

            foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
            {
                if ((int)pn.Tag == tagCodCanal)
                {
                    foreach (Label lb in pn.Controls.OfType<Label>())
                    {

                        if (lb.Tag is int intTag)
                        {
                            if (intTag == tagCodCanal)
                            {
                                lb.Text = textBoxLegenda.Text;
                            }
                        }
                        else if (lb.Tag is string strTag)
                        {
                            if (int.TryParse(strTag, out int stringTagAsInt) && stringTagAsInt == tagCodCanal)
                            {
                                lb.Text = textBoxLegenda.Text;
                            }
                        }
                    }
                }
            }
            // Fazer algo com a legenda...
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void buttonCancel_Click(object sender, EventArgs e)
        {
            // Lógica para cancelar
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
