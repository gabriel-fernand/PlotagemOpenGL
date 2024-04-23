using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL
{
    public class GlobVar
    {
        public static int[] canalA;
    }
    public class Leitura
    {
        public static void LerArquivo()
        {
            string filePath = @"C:\Users\dev_i\source\repos\PlotagemOpenGL\PlotagemOpenGL\Txt's\Serie.txt";

            try
            {
                string[] file = File.ReadAllLines(filePath);
                string[] values;

                foreach (String files in file)
                {
                    values = files.Split(',');

                    GlobVar.canalA = new int[values.Length];
                    for(int i = 0; i < values.Length; i++)
                    {
                        GlobVar.canalA[i] = Convert.ToInt32(values[i]);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao ler o arquivo: {ex.Message}");

            }
        }
    }
    
    public partial class Tela_Plotagem : Form
    {
        public Tela_Plotagem()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.MaximizeBox = false;
        }

        private void Tela_Plotagem_Load(object sender, EventArgs e)
        {

        }

    }
}
