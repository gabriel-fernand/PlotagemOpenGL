using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormColorRGB
{
    public partial class Colors : Form
    {
        private Color selectedColor;

        public Colors()
        {
            InitializeComponent();
            LoadColors();
        }

        private void LoadColors()
        {
            Color[] colors = new Color[]
{
                Color.White, Color.LightGray, Color.Gainsboro,
                Color.MistyRose, Color.LavenderBlush, Color.SeaShell,
                Color.OldLace, Color.Linen, Color.Lavender,
                Color.LavenderBlush, Color.Honeydew, Color.MintCream,
                Color.Azure, Color.AliceBlue, Color.LightCyan,
                Color.LightPink, Color.LightCoral, Color.Salmon,
                Color.LightSalmon, Color.PeachPuff, Color.PapayaWhip,
                Color.PaleGoldenrod, Color.Khaki, Color.LightGoldenrodYellow,
                Color.LightYellow, Color.LemonChiffon, Color.LightGoldenrodYellow,
                Color.LightGreen, Color.PaleGreen, Color.LightBlue,
                Color.SkyBlue, Color.PowderBlue, Color.PaleTurquoise,
                Color.LightSkyBlue, Color.LightSteelBlue, Color.LightSlateGray,
                Color.Thistle, Color.Plum, Color.Orchid,
                Color.Violet, Color.LightBlue, Color.LightSteelBlue,
                Color.MediumAquamarine, Color.Aquamarine, Color.Turquoise,
                // Adicione mais cores claras como necessário
            };

            foreach (Color color in colors)
            {
                Button btnColor = new Button();
                btnColor.BackColor = color;
                btnColor.Width = 40;
                btnColor.Height = 40;
                btnColor.Margin = new Padding(3);
                btnColor.Click += (s, e) =>
                {
                    selectedColor = color;
                    HighlightSelectedColor((Button)s);
                };
                colorPanel.Controls.Add(btnColor);
                btnColor.Click += colorButton_Click;
            }
        }

        private void HighlightSelectedColor(Button selectedButton)
        {
            foreach (Button btn in colorPanel.Controls.OfType<Button>())
            {
                btn.FlatAppearance.BorderSize = 0;
            }

            selectedButton.FlatAppearance.BorderSize = 2;
            selectedButton.FlatAppearance.BorderColor = Color.Black;
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            if (selectedColor != null)
            {
                GlobVar.FundoColor = new int[] { selectedColor.R, selectedColor.G, selectedColor.B, selectedColor.A };
            }
            Tela_Plotagem.TelaClearAndReload();
            this.Close();
        }
        private void colorButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                // Obtém a cor do botão clicado
                Color selectedColor = clickedButton.BackColor;

                // Atualiza a cor selecionada
                this.BackColor = selectedColor;
                colorPanel.BackColor = selectedColor;
                //lblSelectedColor.BackColor = selectedColor;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
