using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormsAuxi
{
    public class OverlayForm : Form
    {
        public Rectangle TempRect { get; set; }

        public OverlayForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.LightGray;
            this.Opacity = 0.3;
            this.ShowInTaskbar = false;
            this.TopMost = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (TempRect != Rectangle.Empty)
            {
                using (Pen pen = new Pen(Color.Black, 3))
                {
                    e.Graphics.DrawRectangle(pen, TempRect);
                }
            }
        }
    }
}
