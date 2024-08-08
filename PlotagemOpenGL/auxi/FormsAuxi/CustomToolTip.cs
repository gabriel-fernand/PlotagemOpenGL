using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormsAuxi
{
    internal class CustomToolTip : ToolTip
    {
        public CustomToolTip()
        {
            this.OwnerDraw = true;
            this.Popup += new PopupEventHandler(this.OnPopup);
            this.Draw += new DrawToolTipEventHandler(this.OnDraw);
        }

        private void OnPopup(object sender, PopupEventArgs e)
        {
            // Defina o tamanho da tooltip de acordo com o conteúdo
            e.ToolTipSize = new Size(250, 75); // Ajuste o tamanho conforme necessário
        }

        private void OnDraw(object sender, DrawToolTipEventArgs e)
        {
            Graphics g = e.Graphics;

            // Retângulo superior
            Rectangle upperRect = new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, (int)(e.Bounds.Height * 0.25f));
            // Retângulo inferior
            Rectangle lowerRect = new Rectangle(e.Bounds.X, e.Bounds.Y + (int)(e.Bounds.Height * 0.25f), e.Bounds.Width, e.Bounds.Height);

            // Desenhe o fundo do retângulo superior
            using (LinearGradientBrush upperBrush = new LinearGradientBrush(e.Bounds, Color.MintCream, Color.LightBlue, 45f))
            {
                g.FillRectangle(upperBrush, upperRect);
            }

            // Desenhe o fundo do retângulo inferior
            using (LinearGradientBrush lowerBrush = new LinearGradientBrush(e.Bounds, Color.MintCream, Color.MintCream, 45f))
            {
                g.FillRectangle(lowerBrush, lowerRect);
            }

            // Configuração das fontes e brushes
            Font titleFont = new Font("Arial", 10, FontStyle.Bold);
            Font textFont = new Font("Arial", 9, FontStyle.Regular);
            Brush textBrush = Brushes.Black;

            // Texto no retângulo superior
            string upperText = GlobVar.Event + "\n";
            string[] upperLines = upperText.Split(new[] { '\n' }, StringSplitOptions.None);

            float y = upperRect.Y + 2;
            foreach (string line in upperLines)
            {
                g.DrawString(line, titleFont, textBrush, new PointF(upperRect.X + 10, y));
                y += 20; // Ajuste o espaçamento entre as linhas conforme necessário
            }

            // Texto no retângulo inferior
            string lowerText = $"Duração: {GlobVar.DuracaoEvent:F2}s\nMenor Saturação: {GlobVar.satuMinCanal}\n";
            string[] lowerLines = lowerText.Split(new[] { '\n' }, StringSplitOptions.None);

            y = lowerRect.Y + 10;
            foreach (string line in lowerLines)
            {
                g.DrawString(line, textFont, textBrush, new PointF(lowerRect.X + 10, y));
                y += 20; // Ajuste o espaçamento entre as linhas conforme necessário
            }
        }
    }
}
