using Accord.Math;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.WPF;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using static System.Windows.Forms.AxHost;
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
                float[] color = new float[3];
                float[] colorLinha = new float[3];
                var filteredRows = eventos.AsEnumerable()
                              .OrderBy(row => row.Field<int>("Seq"));
                Vector2 cordenadasTexto;

                // Agrupar por Seq e processar cada grupo
                var groupedRows = filteredRows.GroupBy(row => row.Field<int>("Seq"));
                int des = qtdGraf - 1;
                foreach (var group in groupedRows)
                {
                    var firstRow = group.First();
                    int codCanal1First = firstRow.Field<int>("CodCanal1");
                    int inicio = firstRow.Field<int>("Inicio");
                    int termino = firstRow.Field<int>("Duracao");
                    int codEvento = firstRow.Field<int>("CodEvento");
                    if (GlobVar.codSelected.Contains(codCanal1First))
                    {
                        var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodEvento") == codEvento).CopyToDataTable();
                        int rgbDex = Convert.ToInt32(rowInfoEvento.Rows[0]["CorFundo"]);
                        color = plotGrafico.ObterComponentesRGB(rgbDex);
                        colorLinha = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowInfoEvento.Rows[0]["CorTexto"]));

                        int YAdjusted = EncontrarValorMaisProximo(desenhoLoc, desenhoLoc[GlobVar.codSelected.IndexOf(codCanal1First)]);
                        string tipoCanal = rowInfoEvento.Rows[0]["DescrEvento"].ToString();


                        gl.Begin(OpenGL.GL_QUADS);
                        gl.PointSize(3.0f); // Define o tamanho dos pontos
                        gl.Color(color[0], color[1], color[2], 0.44f);
                        //gl.ColorMask(3, 6, 7, alpha);
                        gl.Vertex(inicio, GlobVar.StartY[YAdjusted] + 5, -1.5f);
                        gl.Vertex(termino, GlobVar.StartY[YAdjusted] + 5, -1.5f);
                        gl.Vertex(termino, GlobVar.EndY[YAdjusted] - 5, -1.5f);
                        gl.Vertex(inicio, GlobVar.EndY[YAdjusted] - 5, -1.5f);
                        gl.End();
                        gl.Flush();
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
        //Metodo para salvar um evento novo
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
            GlobVar.eventosUpdate.Rows.Add(seq, numPag, 101, GlobVar.codSelected[loc], inicio, termino);

            // Exportar DataTable para Excel
            string excelFilePath = @"C:\Teste\Teste";
            //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
            eventos.Dispose();
        }
        //Metodo criado para mover ou modificar um evento
        public static void UpdateEvent(int inicio, int termino, int codCanal, float[] desenhoLoc, float startY, int seq, int codEvento)
        {

            int loc = EncontrarValorMaisProximo(desenhoLoc, startY);

            DataView view = new DataView(GlobVar.eventosUpdate);
            view.RowFilter = $"Seq = {seq}";

            if (view.Count > 0)
            {
                // Se a linha for encontrada, remova-a usando o DataTable original
                DataRow rowToRemove = view[0].Row;
                GlobVar.eventosUpdate.Rows.Remove(rowToRemove);
            }
            GlobVar.eventosUpdate.Rows.Add(seq, GlobVar.NumPagEvent, codEvento, codCanal, inicio, termino);
            //GlobVar.eventosUpdate.Rows.Remove(row => row.Field<int>("Seq") == seq);
        }
        /*private static Vector2 ConvertToScreenCoordinates(float openGLX, float openGLY, out int screenX, out int screenY)
        {
            var gl = Tela_Plotagem.openglControl1.OpenGL;

            // Get the viewport and projection/modelview matrices
            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

            double[] modelview = new double[16];
            gl.GetDouble(OpenGL.GL_MODELVIEW_MATRIX, modelview);

            double[] projection = new double[16];
            gl.GetDouble(OpenGL.GL_PROJECTION_MATRIX, projection);

            // Convert OpenGL coordinates to screen coordinates
            double winX, winY, winZ;
            gl.Project(openGLX, openGLY, 0, modelview, projection, viewport, out winX, out winY, out winZ);

            screenX = (int)winX;
            screenY = (int)(viewport[3] - winY); // invert Y coordinate

            return new Vector2(screenX, screenY);
        }*/

        public static void DeleteEvent(int inicio, int termino, int codCanal, float[] desenhoLoc, float startY, int seq, int codEvento)
        {
            int loc = EncontrarValorMaisProximo(desenhoLoc, startY);

            DataView view = new DataView(GlobVar.eventosUpdate);
            view.RowFilter = $"Seq = {seq}";

            if (view.Count > 0)
            {
                // Se a linha for encontrada, remova-a usando o DataTable original
                DataRow rowToRemove = view[0].Row;
                GlobVar.eventosUpdate.Rows.Remove(rowToRemove);
            }

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
        public static void EventMovement(DataRow eventRow, int txPorCanal, int loc)
        {
            try
            {
                GlobVar.iniEventoMove = Convert.ToInt32(eventRow["Inicio"]);
                GlobVar.durEventoMove = Convert.ToInt32(eventRow["Duracao"]);
            }
            catch { }
        }
        public static void ProcessEvent(DataRow eventRow, int txPorCanal, int loc)
        {
            try
            {
                int ini = Convert.ToInt32(eventRow["Inicio"]);
                int dur = Convert.ToInt32(eventRow["Duracao"]);

                string formattedStartTime;
                string formattedDuration;

                //Faz o calculo para arrumar a questao de taxa de amostragem, caso seja diferente das de 512 - Pois no mdb estao todos os eventos transformados para 512.
                if (txPorCanal != 512)
                {
                    ini = ini / 512;
                    dur = dur / 512;

                    ini = ini * txPorCanal;
                    dur = dur * txPorCanal;
                }
                // Calcula o tempo de início e o tempo de fim em segundos

                double startSeconds = (double)ini / txPorCanal;
                double endSeconds = (double)dur / txPorCanal;
                double duriti = endSeconds - startSeconds;
                // Cria os TimeSpan correspondentes
                TimeSpan startTimeSpan = TimeSpan.FromSeconds(startSeconds);
                TimeSpan endTimeSpan = TimeSpan.FromSeconds(endSeconds);
                TimeSpan durationTimeSpan = TimeSpan.FromSeconds(duriti);// Calcula a duração
                //Para ter uma diferenca em caso de o evento comecar em minutos e nao em hora, pois fica melhor vizualmente
                if (startTimeSpan.Hours == 0)
                {
                    // Formata os TimeSpan para o formato desejado
                    formattedStartTime = $"{startTimeSpan.Minutes:D2}M:{startTimeSpan.Seconds:D2}S";
                    formattedDuration = $"{durationTimeSpan.Minutes:D2}M:{durationTimeSpan.Seconds:D2}S";
                }
                else
                {
                    formattedStartTime = $"{startTimeSpan.Hours:D2}H:{startTimeSpan.Minutes:D2}M:{startTimeSpan.Seconds:D2}S";
                    formattedDuration = $"{durationTimeSpan.Minutes:D2}M:{durationTimeSpan.Seconds:D2}S";
                }
                // Atribui os valores às variáveis globais
                GlobVar.InicioEvent = formattedStartTime;
                GlobVar.DuracaoEvent = formattedDuration;

                var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                        .Where(row => row.Field<int>("CodEvento") == Convert.ToInt16(eventRow["CodEvento"])).CopyToDataTable();
                GlobVar.Event = rowInfoEvento.Rows[0]["DescrEvento"].ToString();
            }
            catch { }
        }
        public static bool IsThereAnEvent(int mouseX, float[] desenhoLoc, float startY)
        {
            try
            {
                bool isThereAnEvent = false;


                int loc = EncontrarValorMaisProximo(desenhoLoc, startY);
                DataTable sequancias = new DataTable();

                // Verifica se o DataTable 'eventosUpdate' está vazio
                if (GlobVar.eventosUpdate == null || GlobVar.eventosUpdate.Rows.Count == 0)
                {
                    return false;
                }

                // Filtra as linhas correspondentes
                var filteredRows = GlobVar.eventosUpdate.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodCanal1") == GlobVar.codSelected[loc]);

                // Verifica se há linhas filtradas antes de tentar copiar para um DataTable
                if (!filteredRows.Any())
                {
                    return false;
                }

                sequancias = filteredRows.CopyToDataTable();

                // Verifica se o DataTable 'sequancias' está vazio
                if (sequancias == null || sequancias.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < sequancias.Rows.Count; i++)
                {
                    if (mouseX > Convert.ToInt64(sequancias.Rows[i]["Inicio"]) + 25 && mouseX < Convert.ToInt64(sequancias.Rows[i]["Duracao"]) - 25)
                    {
                        ProcessEvent(sequancias.Rows[i], GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[loc])], loc);
                        EventMovement(sequancias.Rows[i], GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[loc])], loc);
                        GlobVar.seqEvento = Convert.ToInt32(sequancias.Rows[i]["Seq"]);
                        GlobVar.CodEvento = Convert.ToInt32(sequancias.Rows[i]["CodEvento"]);
                        GlobVar.NumPagEvent = sequancias.Rows[i]["NumPag"].ToString();
                        GlobVar.CodCanalEvent = Convert.ToInt16(sequancias.Rows[i]["CodCanal1"]);
                        isThereAnEvent = true;
                        //break; // Sai do loop assim que encontrar um evento
                    }
                }

                // Limpa o DataTable 'sequancias' se necessário
                sequancias.Dispose();


                return isThereAnEvent;
            }
            catch { return false; }
        }

        public static bool IsInAnEventStart(int mouseX, float[] desenhoLoc, float startY)
        {
            try
            {
                bool isThereAnEvent = false;
                int loc = EncontrarValorMaisProximo(desenhoLoc, startY);
                DataTable sequancias = new DataTable();

                // Verifica se o DataTable 'eventosUpdate' está vazio
                if (GlobVar.eventosUpdate == null || GlobVar.eventosUpdate.Rows.Count == 0)
                {
                    return false;
                }

                // Filtra as linhas correspondentes
                var filteredRows = GlobVar.eventosUpdate.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodCanal1") == GlobVar.codSelected[loc]);

                // Verifica se há linhas filtradas antes de tentar copiar para um DataTable
                if (!filteredRows.Any())
                {
                    return false;
                }

                sequancias = filteredRows.CopyToDataTable();

                // Verifica se o DataTable 'sequancias' está vazio
                if (sequancias == null || sequancias.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < sequancias.Rows.Count; i++)
                {
                    if ((mouseX <= Convert.ToInt64(sequancias.Rows[i]["Inicio"]) + 25) && (mouseX >= Convert.ToInt64(sequancias.Rows[i]["Inicio"]) - 25))
                    {
                        ProcessEvent(sequancias.Rows[i], GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[loc])], loc);
                        EventMovement(sequancias.Rows[i], GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[loc])], loc);
                        GlobVar.seqEvento = Convert.ToInt32(sequancias.Rows[i]["Seq"]);
                        GlobVar.CodEvento = Convert.ToInt32(sequancias.Rows[i]["CodEvento"]);
                        GlobVar.NumPagEvent = sequancias.Rows[i]["NumPag"].ToString();
                        GlobVar.CodCanalEvent = Convert.ToInt16(sequancias.Rows[i]["CodCanal1"]);
                        isThereAnEvent = true;
                        //break; // Sai do loop assim que encontrar um evento
                    }
                }
                // Limpa o DataTable 'sequancias' se necessário
                sequancias.Dispose();


                return isThereAnEvent;
            }
            catch { return false; }
        }
        public static bool IsInAnEventEnd(int mouseX, float[] desenhoLoc, float startY)
        {
            try
            {
                bool isThereAnEvent = false;
                int loc = EncontrarValorMaisProximo(desenhoLoc, startY);
                DataTable sequancias = new DataTable();

                // Verifica se o DataTable 'eventosUpdate' está vazio
                if (GlobVar.eventosUpdate == null || GlobVar.eventosUpdate.Rows.Count == 0)
                {
                    return false;
                }

                // Filtra as linhas correspondentes
                var filteredRows = GlobVar.eventosUpdate.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodCanal1") == GlobVar.codSelected[loc]);

                // Verifica se há linhas filtradas antes de tentar copiar para um DataTable
                if (!filteredRows.Any())
                {
                    return false;
                }

                sequancias = filteredRows.CopyToDataTable();

                // Verifica se o DataTable 'sequancias' está vazio
                if (sequancias == null || sequancias.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < sequancias.Rows.Count; i++)
                {
                    if ((mouseX >= Convert.ToInt64(sequancias.Rows[i]["Duracao"]) - 25) && (mouseX <= Convert.ToInt64(sequancias.Rows[i]["Duracao"]) + 25))
                    {
                        ProcessEvent(sequancias.Rows[i], GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[loc])], loc);
                        EventMovement(sequancias.Rows[i], GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[loc])], loc);
                        GlobVar.seqEvento = Convert.ToInt32(sequancias.Rows[i]["Seq"]);
                        GlobVar.CodEvento = Convert.ToInt32(sequancias.Rows[i]["CodEvento"]);
                        GlobVar.NumPagEvent = sequancias.Rows[i]["NumPag"].ToString();
                        GlobVar.CodCanalEvent = Convert.ToInt16(sequancias.Rows[i]["CodCanal1"]);
                        isThereAnEvent = true;
                        //break; // Sai do loop assim que encontrar um evento
                    }
                }
                // Limpa o DataTable 'sequancias' se necessário
                sequancias.Dispose();

                return isThereAnEvent;
            }
            catch { return false; }
        }

        public static void DrawBordenInAnEvent(bool slaDpsEuPenso, OpenGL gl, float[] desenhoLoc)
        {
            if (slaDpsEuPenso)
            {
                int YAdjusted = EncontrarValorMaisProximo(desenhoLoc, desenhoLoc[GlobVar.codSelected.IndexOf(GlobVar.CodCanalEvent)]);

                gl.Begin(OpenGL.GL_LINE_LOOP);
                gl.PointSize(2.0f); // Define o tamanho dos pontos
                gl.Color(1, 0, 0, 0.44f);
                //gl.ColorMask(3, 6, 7, alpha);
                gl.Vertex(GlobVar.iniEventoMove - 25, GlobVar.StartY[YAdjusted] + 1, -1.9f);
                gl.Vertex(GlobVar.durEventoMove + 25, GlobVar.StartY[YAdjusted] + 1, -1.9f);
                gl.Vertex(GlobVar.durEventoMove + 25, GlobVar.EndY[YAdjusted] - 1, -1.9f);
                gl.Vertex(GlobVar.iniEventoMove - 25, GlobVar.EndY[YAdjusted] - 1, -1.9f);
                gl.End();
                gl.Flush();
            }
        }

        public static void DrawingAnEvent(int qtdGraf, OpenGL gl, float[] desenhoLoc) 
        {
            float[] color = new float[3];

            var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodEvento") == 101).CopyToDataTable();
            int rgbDex = Convert.ToInt32(rowInfoEvento.Rows[0]["CorFundo"]);
            color = plotGrafico.ObterComponentesRGB(rgbDex);
            int YAdjusted = Plotagem.EncontrarValorMaisProximo(desenhoLoc, GlobVar.startY);

            //gl.Color(0.0f, 0.0f, 0.0f);

            gl.Begin(OpenGL.GL_QUADS);
            gl.PointSize(3.0f); // Define o tamanho dos pontos
            gl.Color(color[0], color[1], color[2], 0.44f);
            //gl.Color(0, 0, 0);
            //gl.ColorMask(3, 6, 7, alpha);
            gl.Vertex(GlobVar.startX, GlobVar.StartY[YAdjusted] + 5, -1.5f);
            gl.Vertex(GlobVar.endX, GlobVar.StartY[YAdjusted] + 5, -1.5f);
            gl.Vertex(GlobVar.endX, GlobVar.EndY[YAdjusted] - 5, -1.5f);
            gl.Vertex(GlobVar.startX, GlobVar.EndY[YAdjusted] - 5, -1.5f);
            gl.End();  
            //startX = 0;

        }
    }
}

