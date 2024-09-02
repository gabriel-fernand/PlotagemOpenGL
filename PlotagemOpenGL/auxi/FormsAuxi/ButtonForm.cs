using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormsAuxi
{
    public class ButtonForm : Form
    {
        public Rectangle TempRect { get; set; }

        public ButtonForm()
        {
            // Configuração do Form
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.LightGray; // Ajuste para uma cor neutra
            this.Opacity = 0.8; // Define a opacidade para simular transparência
            this.ShowInTaskbar = false; // Não exibir na barra de tarefas
            this.TopMost = true; // Sempre no topo de outros formulários
            this.Size = new Size(25, 50);

            // Definindo as bordas arredondadas do formulário
            SetRoundedCorners(10); // Ajuste o raio para modificar a curva das bordas
        }

        // Método para definir bordas arredondadas
        private void SetRoundedCorners(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90); // Canto superior esquerdo
            path.AddLine(radius, 0, this.Width - radius, 0); // Borda superior
            path.AddArc(new Rectangle(this.Width - radius, 0, radius, radius), 270, 90); // Canto superior direito
            path.AddLine(this.Width, radius, this.Width, this.Height - radius); // Borda direita
            path.AddArc(new Rectangle(this.Width - radius, this.Height - radius, radius, radius), 0, 90); // Canto inferior direito
            path.AddLine(this.Width - radius, this.Height, radius, this.Height); // Borda inferior
            path.AddArc(new Rectangle(0, this.Height - radius, radius, radius), 90, 90); // Canto inferior esquerdo
            path.CloseFigure();

            // Aplicando o contorno arredondado ao formulário
            this.Region = new Region(path);
        }

        // Método de pintura para desenhar o retângulo dentro do Form
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Verifica se o retângulo não está vazio antes de desenhar
            if (TempRect != Rectangle.Empty)
            {
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
            SetRoundedCorners(10); // Reaplica o contorno para garantir que as bordas sejam arredondadas
            this.Show();
        }

        // Método para ocultar o ButtonForm
        public void HideOverlay()
        {
            this.Hide();
        }
    }
}
