using Accord.Math;
using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PlotagemOpenGL
{
    public partial class montagem : Form
    {
        public montagem()
        {
            InitializeComponent();
            InitializeCheckedListBox();
        }
        private void InitializeCheckedListBox()
        {
            // Preenche o CheckedListBox com os valores de nomeCanais
            for (int i = 0; i < GlobVar.tbl_MontagemSelecionada.Rows.Count; i++)
            {
                checkedListBox1.Items.Add(GlobVar.tbl_MontagemSelecionada.Rows[i]["Legenda"]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Tela_Plotagem.elementoX();
        }


        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Atualiza grafSelected com os índices dos itens selecionados
            var selectedIndices = checkedListBox1.CheckedIndices;
            List<int> selectedIndicesList = new List<int>();
            foreach (int index in selectedIndices)
            {
                selectedIndicesList.Add(index);
            }
            Tela_Plotagem.aux = selectedIndicesList.Count;

            GlobVar.grafSelected.Clear();
            GlobVar.codSelected.Clear();

            // Inicializa grafSelected com o tamanho de nomeCanais
            for (int i = 0; i < selectedIndicesList.Count; i++)
            {
                GlobVar.grafSelected[i] = selectedIndicesList[i];
                GlobVar.codSelected[i] = GlobVar.codCanal[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[i]]["CodCanal1"]))];
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

