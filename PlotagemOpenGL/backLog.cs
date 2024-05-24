using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace PlotagemOpenGL
{
    public partial class backLog : Form
    {
        private Size formOriginalSize;
        private Rectangle gridView;
        private Rectangle bt1;
        private Rectangle bt2;
        private Rectangle bt3;
        public backLog()
        {
            InitializeComponent();
            formOriginalSize = this.Size;
            gridView = new Rectangle(dataGridView1.Location, dataGridView1.Size);
            bt1 = new Rectangle(button1.Location, button1.Size);
            bt2 = new Rectangle(button2.Location, button2.Size);
            bt3 = new Rectangle(button3.Location, button3.Size);
            this.Resize += Tela_Plotagem_Resiz;

        }
        private void resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Location = new Point(newX, newY);
            c.Size = new Size(newWidth, newHeight);
        }
        private void Tela_Plotagem_Resiz(object sender, EventArgs e)
        {
            resize_Control(dataGridView1, gridView);
            resize_Control(button1, bt1 );
            resize_Control(button2, bt2 );
            resize_Control(button3, bt3 );
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

            // Exibir os dados agrupados no DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            //to get columns
            foreach (DataColumn col in GlobVar.codEventos.Columns)
            {
                var c = new DataGridViewTextBoxColumn() { HeaderText = col.ColumnName }; //Let say that the default column template of DataGridView is DataGridViewTextBoxColumn
                dataGridView1.Columns.Add(c);
            }

            //to get rows
            foreach (DataRow row in GlobVar.codEventos.Rows)
            {
                dataGridView1.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8]);
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

            // Ordena do menor para o maior
            var filteredRows1 = GlobVar.eventos.AsEnumerable()
                                              .OrderBy(row => row.Field<int>("NumPag"));

            var filteredRows = filteredRows1.AsEnumerable()
                                              .OrderBy(row => row.Field<int>("Seq"));

            var groupedRows = filteredRows.GroupBy(row => row.Field<int>("Seq"))
                                           .SelectMany(group => new[]
                                           {
                                               group.First(),
                                               group.Last()
                                           })
                                           .Distinct();

            // Adicionar as linhas filtradas ao DataGridView
            foreach (var row in groupedRows)
            {
                dataGridView1.Rows.Add(row.ItemArray);
            }
        }
    }
}
