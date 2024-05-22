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
            checkedListBox1.Items.AddRange(GlobVar.nomeCanais);

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
            for (int index = 0; index < GlobVar.grafSelected.Length; index++)
            {
                GlobVar.grafSelected[index] = 0;
            }
            // Inicializa grafSelected com o tamanho de nomeCanais
            for (int i = 0; i < selectedIndicesList.Count; i++)
            {
                GlobVar.grafSelected[i] = selectedIndicesList[i];
            }

        }
    }
}
