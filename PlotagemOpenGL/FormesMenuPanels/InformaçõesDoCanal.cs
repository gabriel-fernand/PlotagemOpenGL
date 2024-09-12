using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using PlotagemOpenGL.auxi;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class InformaçõesDoCanal : Form
    {
        public static int tagCodCanal = Tela_Plotagem.tagCodCanal;
        
        public InformaçõesDoCanal()
        {
            InitializeComponent();
            LoadComponentes();
        }

        public static void LoadComponentes()
        {
            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                .Where(row => row.Field<int>("CodCanal1") == Tela_Plotagem.tagCodCanal).CopyToDataTable();

            if (rowNumerico != null)
            {
                var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                            .Where(row => row.Field<int>("CodCanal") == Tela_Plotagem.tagCodCanal).CopyToDataTable();
                int TipoCanal = Convert.ToInt16(CodTipoCanal.Rows[0]["CodTipo"]);
                //If feito para mudar com base no tipo de canal quando de Posicao, Numero e Grafico -- faz abrir o que responde aoq no menu de inverter canal e 
                // mudar de grafico para numero ou seta
                if (TipoCanal == 12)
                {
                    Posicao.Show();
                    Grafico.Hide();
                    NumerosPosi.Hide();
                }
                else if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 
                    || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                                || TipoCanal == 15 || TipoCanal == 30)
                {
                    Posicao.Hide();
                    Grafico.Hide();
                    NumerosPosi.Show();
                }
                else
                {
                    Posicao.Hide();
                    Grafico.Show();
                    NumerosPosi.Hide();
                }
                //Fazendo o preencimento das informacoes do Canal
                var cadCanal = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == Tela_Plotagem.tagCodCanal).CopyToDataTable();
                textNome.Text = $"{cadCanal.Rows[0]["NomeCanal"].ToString()}";

                var cadTipoCanal = GlobVar.tbl_CadTipoCanal.AsEnumerable().Where(row => row.Field<int>("CodTipo") == TipoCanal).CopyToDataTable();
                textTipoDeCanal.Text = $"{cadTipoCanal.Rows[0]["DescrTipo"].ToString()}";

                textAmostragem.Text = $"{rowNumerico.Rows[0]["QtdAmostras"].ToString()}";
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            // Remove o foco do TextBox
            this.ActiveControl = null; // Remove o foco do TextBox
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Impede que o TextBox receba o foco quando clicado
            this.ActiveControl = null;
        }

    }
}
