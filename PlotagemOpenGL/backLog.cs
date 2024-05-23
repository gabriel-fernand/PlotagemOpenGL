using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL
{
    public partial class backLog : Form
    {
        public backLog()
        {
            InitializeComponent();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Exibir os dados agrupados no DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            //to get columns
            foreach (DataColumn col in GlobVar.eventos.Columns)
            {
                var c = new DataGridViewTextBoxColumn() { HeaderText = col.ColumnName }; //Let say that the default column template of DataGridView is DataGridViewTextBoxColumn
                dataGridView1.Columns.Add(c);
            }

            //to get rows
            foreach (DataRow row in GlobVar.eventos.Rows)
            {
                dataGridView1.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Agrupar os dados pela coluna "CodEvento"
            var groupedData = from row in GlobVar.eventos.AsEnumerable()
                              group row by row.Field<int>("CodEvento") into grouped
                              select new
                              {
                                  CodEvento = grouped.Key,
                                  NumPag = grouped.Average(r => r.Field<int>("NumPag")),
                                  CodCanal = grouped.First().Field<int>("CodCanal1"), // Ou outra lógica de agregação
                                  CodCanal2 = grouped.First().Field<int>("CodCanal2"),
                                  Inicio = grouped.Min(r => r.Field<int>("Inicio")),
                                  Duracao = grouped.Sum(r => r.Field<int>("Duracao")),
                                  MenorSat = grouped.Min(r => r.Field<Single>("MenorSat")),
                                  Posicao = grouped.First().Field<string>("Posicao")
                              };

            // Criar um novo DataTable para armazenar os dados agrupados
            DataTable groupedTable = new DataTable();
            groupedTable.Columns.Add("CodEvento", typeof(int));
            groupedTable.Columns.Add("NumPag", typeof(double));
            groupedTable.Columns.Add("CodCanal1", typeof(int));
            groupedTable.Columns.Add("CodCanal2", typeof(int));
            groupedTable.Columns.Add("Inicio", typeof(int));
            groupedTable.Columns.Add("Duracao", typeof(int));
            groupedTable.Columns.Add("MenorSat", typeof(Single));
            groupedTable.Columns.Add("Posicao", typeof(string));

            // Adicionar os dados agrupados ao novo DataTable
            foreach (var item in groupedData)
            {
                groupedTable.Rows.Add(item.CodEvento, item.NumPag, item.CodCanal, item.CodCanal2, item.Inicio, item.Duracao, item.MenorSat, item.Posicao);
            }

            // Exibir os dados agrupados no DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Adicionar colunas ao DataGridView
            foreach (DataColumn col in groupedTable.Columns)
            {
                var c = new DataGridViewTextBoxColumn() { HeaderText = col.ColumnName };
                dataGridView1.Columns.Add(c);
            }

            // Adicionar linhas ao DataGridView
            foreach (DataRow row in groupedTable.Rows)
            {
                dataGridView1.Rows.Add(row.ItemArray);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Limpar o DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            // Adicionar colunas ao DataGridView
            foreach (DataColumn col in GlobVar.eventos.Columns)
            {
                var c = new DataGridViewTextBoxColumn() { HeaderText = col.ColumnName };
                dataGridView1.Columns.Add(c);
            }

            // Filtrar as linhas onde CodCanal1 é igual a 17
            var filteredRows = GlobVar.eventos.AsEnumerable()
                                              .Where(row => row.Field<int>("CodCanal1") == 17);

            // Adicionar as linhas filtradas ao DataGridView
            foreach (var row in filteredRows)
            {
                dataGridView1.Rows.Add(row.ItemArray);
            }
        }
    }
}
