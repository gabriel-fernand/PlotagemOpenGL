using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class CarregandoAltMontagem : Form
    {
        public CarregandoAltMontagem()
        {
            InitializeComponent();
        }

        public void realoctxt()
        {
            // Calcula a metade da largura do formulário
            int meioSize = this.Size.Width / 2;

            // Calcula a metade da largura do label
            int metadeLabel = label1.Width / 2;

            // Define a nova localização para centralizar o label
            label1.Location = new Point(meioSize - metadeLabel, label1.Location.Y);
        }
        // Método para atualizar o progresso
        public void AtualizarProgresso(int valor)
        {
            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new Action(() => AtualizarProgresso(valor)));
            }
            else
            {
                progressBar1.Value = valor;

                // Fecha o formulário quando atingir 100%
                if (progressBar1.Value >= 100)
                {
                    this.Close();
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
