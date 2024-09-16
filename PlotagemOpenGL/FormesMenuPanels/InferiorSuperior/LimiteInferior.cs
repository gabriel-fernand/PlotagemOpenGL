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

namespace PlotagemOpenGL.FormesMenuPanels.InferiorSuperior
{
    public partial class LimiteInferior : Form
    {
        public static int tagCodCanal = Tela_Plotagem.tagCodCanal;
        public static DataRow dt;

        public LimiteInferior()
        {
            InitializeComponent();
            load();
        }
        public static void load()
        {
            dt = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == Tela_Plotagem.tagCodCanal);
            if(dt["LimiteInferior"] != DBNull.Value)
            {
                textBoxLimiteInferior.Text = dt["LimiteInferior"].ToString();
            }
        }

        private void textBoxLimiteInferior_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir apenas números e controle de backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            // Lógica para quando o usuário confirmar
            string limiteInferior = textBoxLimiteInferior.Text;
            dt["LimiteInferior"] = limiteInferior;
            foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
            {
                if ((int)pn.Tag == tagCodCanal)
                {
                    foreach (Label lb in pn.Controls.OfType<Label>())
                    {
                        if (lb.Tag.Equals("min"))
                        {
                            lb.Text = $"{limiteInferior}";
                        }
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();
            Tela_Plotagem.TelaClearAndReload();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Lógica para quando o usuário cancelar
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
