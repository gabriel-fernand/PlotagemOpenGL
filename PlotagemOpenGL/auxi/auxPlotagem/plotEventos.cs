using Accord.Math;
using SharpGL;
using System;
using System.Data;
using System.IO;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;

namespace PlotagemOpenGL.auxi.auxPlotagem
{
    public static class plotEventos
    {
        public static void DesenhaEventos(int qtdGraf, OpenGL gl, float[] desenhoLoc) 
        {
            try
            {
                DataTable eventos = GlobVar.eventosUpdate;

                var filteredRows = eventos.AsEnumerable()
                              .OrderBy(row => row.Field<int>("Seq"));

                // Agrupar por Seq e processar cada grupo
                var groupedRows = filteredRows.GroupBy(row => row.Field<int>("Seq"));
                int des = qtdGraf - 1;
                foreach (var group in groupedRows)
                {
                    var firstRow = group.First();
                    int codCanal1First = firstRow.Field<int>("CodCanal1");
                    int inicio = firstRow.Field<int>("Inicio");
                    int termino = firstRow.Field<int>("Duracao");
                    if (GlobVar.codSelected.Contains(codCanal1First))
                    {
                        int YAdjusted = EncontrarValorMaisProximo(desenhoLoc, desenhoLoc[GlobVar.codSelected.IndexOf(codCanal1First)]);

                        gl.Begin(OpenGL.GL_QUADS);
                        gl.PointSize(3.0f); // Define o tamanho dos pontos
                        gl.Color(GlobVar.colors[GlobVar.codSelected.IndexOf(codCanal1First)].X, GlobVar.colors[GlobVar.codSelected.IndexOf(codCanal1First)].Y, GlobVar.colors[GlobVar.codSelected.IndexOf(codCanal1First)].Z, 0.001);
                        //gl.ColorMask(3, 6, 7, alpha);
                        gl.Vertex(inicio, Plotagem.StartY[YAdjusted] + 5, -1.9f);
                        gl.Vertex(termino, Plotagem.StartY[YAdjusted] + 5, -1.9f);
                        gl.Vertex(termino, Plotagem.EndY[YAdjusted] - 5, -1.9f);
                        gl.Vertex(inicio, Plotagem.EndY[YAdjusted] - 5, -1.9f);
                        gl.End();
                        des--;
                    }
                }
            }
            catch { }
        }
        public static int EncontrarValorMaisProximo(float[] valores, float y)
        {
            float valorMaisProximo = valores[0];
            float menorDiferenca = Math.Abs(valores[0] - y);

            foreach (float valor in valores)
            {
                float diferencaAtual = Math.Abs(valor - y);

                if (diferencaAtual < menorDiferenca)
                {
                    menorDiferenca = diferencaAtual;
                    valorMaisProximo = valor;
                }
            }
            int index = Array.IndexOf(valores, valorMaisProximo);

            // Invertendo o índice
            int indexInvertido = valores.Length - 1 - index;

            return indexInvertido;
        }
        public static void AdicionarEventoAoDataTable(int inicio, int termino, int YAdjusted, float[] desenhoLoc, float startY)
        {
            DataTable eventos = GlobVar.eventosUpdate;

            int loc = EncontrarValorMaisProximo(desenhoLoc, startY);

            // Adicionar colunas ao DataTable se não existirem
            if (eventos.Columns.Count == 0)
            {
                eventos.Columns.Add("Seq", typeof(int));
                eventos.Columns.Add("NumPag", typeof(string));
                eventos.Columns.Add("CodEvento", typeof(int));
                eventos.Columns.Add("CodCanal1", typeof(int));
                eventos.Columns.Add("Inicio", typeof(int));
                eventos.Columns.Add("Duracao", typeof(int));
            }

            // Calcular NumPag para início e término
            int numPagInicio = inicio / GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
            int numPagTermino = termino / GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
            string numPag = $"{numPagInicio} -- {numPagTermino}";
            // Obter o próximo valor de Seq
            int seq = eventos.Rows.Count > 0 ? eventos.AsEnumerable().Max(row => row.Field<int>("Seq")) + 1 : 1;

            // Adicionar dados ao DataTable
            GlobVar.eventosUpdate.Rows.Add(seq, numPag, 0, GlobVar.codSelected[loc], inicio, termino);

            // Exportar DataTable para Excel
            string excelFilePath = @"C:\Teste\Teste";
            //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
        }

        // Export DataTable into an excel file with field names in the header line
        // - Save excel file without ever making it visible if filepath is given
        // - Don't save excel file, just make it visible if no filepath is given
        public static void CreateCSVFile(DataTable dt, string strFilePath)
        {
            try
            {
                // Create the CSV file to which grid data will be exported.
                StreamWriter sw = new StreamWriter(strFilePath, false);
                // First we will write the headers.
                //DataTable dt = m_dsProducts.Tables[0];
                int iColCount = dt.Columns.Count;
                for (int i = 0; i < iColCount; i++)
                {
                    sw.Write(dt.Columns[i]);
                    if (i < iColCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);

                // Now write all the rows.

                foreach (DataRow dr in dt.Rows)
                {
                    for (int i = 0; i < iColCount; i++)
                    {
                        if (!Convert.IsDBNull(dr[i]))
                        {
                            sw.Write(dr[i].ToString());
                        }
                        if (i < iColCount - 1)
                        {
                            sw.Write(",");
                        }
                    }

                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
