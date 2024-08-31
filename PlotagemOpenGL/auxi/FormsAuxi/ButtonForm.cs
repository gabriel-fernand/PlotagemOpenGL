using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormsAuxi
{
    public class ButtonForm : Form
    {
        // Propriedade para armazenar o retângulo temporário a ser desenhado
        public Rectangle TempRect { get; set; }

        // Construtor do OverlayForm
        public ButtonForm()
        {
            // Configuração do Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.LightGray; // Ajuste para uma cor neutra
            this.Opacity = 0.8; // Define a opacidade para simular transparência
            this.ShowInTaskbar = false; // Não exibir na barra de tarefas
            this.TopMost = true; // Sempre no topo de outros formulários
            this.Size = new System.Drawing.Size(25, 50);
            // Tamanho e outros ajustes podem ser feitos conforme necessário
        }

        // Método de pintura para desenhar o retângulo dentro do Form
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Verifica se o retângulo não está vazio antes de desenhar
            if (TempRect != Rectangle.Empty)
            {
                // Desenha um retângulo com borda preta e largura de 3 pixels
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawRectangle(pen, TempRect);
                }
            }
        }

        // Método para definir a posição e o tamanho do ButtonForm com base na posição do mouse
        public void ShowOverlay(Point location, Size size)
        {
            this.Location = location;
            this.Size = size;
            this.Show();
        }

        // Método para ocultar o ButtonForm
        public void HideOverlay()
        {
            this.Hide();
        }
    }
}
