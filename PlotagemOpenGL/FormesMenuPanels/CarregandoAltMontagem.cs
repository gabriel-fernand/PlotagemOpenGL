using System;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class CarregandoAltMontagem : Form
    {
        public CarregandoAltMontagem()
        {
            InitializeComponent();
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
    }
}
