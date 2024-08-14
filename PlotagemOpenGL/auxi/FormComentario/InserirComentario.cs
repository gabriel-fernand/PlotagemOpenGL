using ADODB;
using PlotagemOpenGL.auxi.auxPlotagem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormComentario
{
    public partial class InserirComentario : Form
    {
        public InserirComentario()
        {
            InitializeComponent();
            popularlista();
        }

        private void popularlista()
        {
            string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";

            using var connectionConfigBd = new OdbcConnection(connectionStringConfigBd);

            connectionConfigBd.Open();

            string queryTbl_CadComentario = "SELECT * FROM tbl_CadComentario";

            using var commandTipoCanal = new OdbcCommand(queryTbl_CadComentario, connectionConfigBd);

            using var adapter = new OdbcDataAdapter(commandTipoCanal);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // Agora preenche a lista com os dados do DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Supondo que você deseja adicionar uma coluna específica ao ComboBox ou ListBox
                // por exemplo, a coluna "NomeComentario"
                string item = row["Comentario"].ToString();
                ListaComentarios.Items.Add(item);
            }
            connectionConfigBd.Close();

        }

        private void ListaComentarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaComentarios.SelectedItem != null)
            {
                // Atribui o texto selecionado na ListBox ao TextBox
                textBox1.Clear();
                textBox1.Text = ListaComentarios.SelectedItem.ToString();
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Confirmar_Click(object sender, EventArgs e)
        {
            Point XiYi = Tela_Plotagem.LocationMouseClickComentario;
            plotComentatios.AdicionarComentario(XiYi.X, XiYi.Y, textBox1.Text.ToString());
            Tela_Plotagem.TelaClearAndReload();
            this.Close();
        }
    }
}
