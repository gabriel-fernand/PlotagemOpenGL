using Accord.Math;
using SharpGL;
using SharpGL.SceneGraph;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Documents;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.AxHost;
using ClassesBDNano;
using ADODB;
using PlotagemOpenGL.BD;

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

                        //plotNumerico.PlotNumerico(qtdGraf, gl, desenhoLoc);

                    }
                    if (codEvento == 18 || codEvento == 19 || codEvento == 50)
                    {
                        var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                .Where(row => row.Field<int>("CodEvento") == codEvento).CopyToDataTable();
                        int rgbDex = Convert.ToInt32(rowInfoEvento.Rows[0]["CorFundo"]);
                        color = plotGrafico.ObterComponentesRGB(rgbDex);
                        colorLinha = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowInfoEvento.Rows[0]["CorTexto"]));

                        string tipoCanal = rowInfoEvento.Rows[0]["DescrEvento"].ToString();

                        gl.Begin(OpenGL.GL_QUADS);
                        gl.PointSize(3.0f); // Define o tamanho dos pontos
                        gl.Color(color[0], color[1], color[2], 0.44f);
                        //gl.ColorMask(3, 6, 7, alpha);
                        gl.Vertex(inicio, 0 + 5, -1.8f);
                        gl.Vertex(termino, 0 + 5, -1.8f);
                        gl.Vertex(termino, GlobVar.sizeOpenGl.Y - 5, -1.8f);
                        gl.Vertex(inicio, GlobVar.sizeOpenGl.Y - 5, -1.8f);
                        gl.End();
                        gl.Flush();
                        des--;

                        //DrawVerticalText(gl, (int)writeX, (int)writeY, $"{tipoCanal}", "Arial", 20);

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
            try{
                if (GlobVar.lastEvent != null){
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
                    int numPagInicio = inicio / 512;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    int numPagTermino = termino / 512;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    string numPag = $"{numPagInicio} -- {numPagTermino}";
                    // Obter o próximo valor de Seq
                    int seq = eventos.Rows.Count > 0 ? eventos.AsEnumerable().Max(row => row.Field<int>("Seq")) + 1 : 1;

                    // Adicionar dados ao DataTable
                    GlobVar.eventosUpdate.Rows.Add(seq, numPag, GlobVar.lastEvent, GlobVar.codSelected[loc], inicio, termino);
                    AlteraBD.GravaEvento(seq, numPagInicio, (int)GlobVar.lastEvent, GlobVar.CodCanal, -1, inicio, termino, GlobVar.namos, numPagTermino);
                    // Exportar DataTable para Excel
                    string excelFilePath = @"C:\Teste\Teste";
                    //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
                    eventos.Dispose();
                }
            }
            catch { }
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
            int Inicio = (int)inicio / 512;
            int numPagTermino = (int)termino / 512;
            AlteraBD.GravaEvento(seq, Inicio, (int)GlobVar.lastEvent, GlobVar.CodCanal, -1, inicio, termino, GlobVar.namos, numPagTermino);
            //GlobVar.eventosUpdate.Rows.Remove(row => row.Field<int>("Seq") == seq);
        }
        public static void ChangeEventType(int codCanal, int seq, int codEvento)
        {
            try{
                //Verifica se o evento pode ser do tipo
                var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodCanal") == codCanal).CopyToDataTable();
                int TipoCanal = Convert.ToInt16(CodTipoCanal.Rows[0]["CodTipo"]);

                var EventoTipo = GlobVar.tbl_EventoTipoCanal.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodTipoCanal") == TipoCanal).CopyToDataTable();
                var CanHaveAnEvent = EventoTipo.AsEnumerable()
                                                 .Where(row => row.Field<int>("CodEvento") == codEvento);
                if (CanHaveAnEvent.Any())
                {
                    DataRow[] rowsToUpdate = GlobVar.eventosUpdate.Select($"Seq = {seq}");

                    foreach (DataRow row in rowsToUpdate)
                    {
                        // Altera o valor da coluna 'CodEvento' para 'newCodEvento'
                        row["CodEvento"] = codEvento;

                        int codCanal1 = (int)row["CodCanal1"];
                        int inicio = (int)row["Inicio"];
                        int termino = (int)row["Duracao"];
                        int Inicio = (int)row["inicio"] / 521;
                        int numPagTermino = (int)termino / 512;

                        AlteraBD.GravaEvento(seq, Inicio, (int)GlobVar.lastEvent, GlobVar.CodCanal, -1, inicio, termino, GlobVar.namos, numPagTermino);
                    }

                    // Se necessário, aceite as alterações na tabela
                    GlobVar.eventosUpdate.AcceptChanges();
                    GlobVar.EventHasChange = true;
                }
                else
                {
                    return;
                }
            }
            catch { }

        }



        private static Vector2 ConvertToScreenCoordinates(float openGLX, float openGLY, out int screenX, out int screenY)
        {
            var gl = Tela_Plotagem.openglControl1.OpenGL;

            // Get the viewport and projection/modelview matrices
            int[] viewport = new int[4];
            gl.GetInteger(OpenGL.GL_VIEWPORT, viewport);

            double[] modelview = new double[16];
            gl.GetDouble(OpenGL.GL_MODELVIEW_MATRIX, modelview);

            double[] projection = new double[16];
            gl.GetDouble(OpenGL.GL_PROJECTION_MATRIX, projection);

            // Arrays to store the window coordinates
            double[] winX = new double[1];
            double[] winY = new double[1];
            double[] winZ = new double[1];

            // Convert OpenGL coordinates to screen coordinates
            gl.Project(openGLX, openGLY, 0, modelview, projection, viewport, winX, winY, winZ);

            screenX = (int)winX[0];
            screenY = (int)(viewport[3] - winY[0]); // invert Y coordinate

            return new Vector2(screenX, screenY);
        }

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
            AlteraBD.ExcluiEvento(seq);

            //GlobVar.obj_dbEventos = new cls_dbExame();
            //bool Real = cls_dbExame.OpenConnection(GlobVar.bDataFile,ref GlobVar.cnn_dbExame, 512);
            //GlobVar.isTheDBOpen = Real;
            //bool x = GlobVar.obj_dbEventos.ExcluiEvento(GlobVar.cnn_dbExame, seq);
            //GlobVar.isTheDBOpen = x;
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
        public static void EventMovement(DataRow eventRow, int txPorCanal, int loc = 0)
        {
            try
            {
                GlobVar.iniEventoMove = Convert.ToInt32(eventRow["Inicio"]);
                GlobVar.durEventoMove = Convert.ToInt32(eventRow["Duracao"]);
            }
            catch { }
        }
        public static void ProcessEvent(DataRow eventRow, int txPorCanal, int loc = 0)
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
                    string durAux = $"{durationTimeSpan.Seconds},{durationTimeSpan.Milliseconds}seg";
                    formattedStartTime = $"{startTimeSpan.Minutes:D2}M:{startTimeSpan.Seconds:D2}S";
                    formattedDuration = $"{durAux}";
                }
                else
                {
                    string durAux = $"{durationTimeSpan.Seconds},{durationTimeSpan.Milliseconds}seg";
                    formattedStartTime = $"{startTimeSpan.Hours:D2}H:{startTimeSpan.Minutes:D2}M:{startTimeSpan.Seconds:D2}S";
                    formattedDuration = $"{durAux}";
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

                GlobVar.CodCanal = Convert.ToInt16(GlobVar.codSelected[loc]);

                var codTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                .Where(row => row.Field<int>("CodCanal") == GlobVar.CodCanal).CopyToDataTable();
                GlobVar.CodTipoCanalEvent = Convert.ToInt16(codTipoCanal.Rows[0]["CodTipo"]);

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
                GlobVar.CodCanal = Convert.ToInt16(GlobVar.codSelected[loc]);

                var codTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                .Where(row => row.Field<int>("CodCanal") == GlobVar.CodCanal).CopyToDataTable();
                GlobVar.CodTipoCanalEvent = Convert.ToInt16(codTipoCanal.Rows[0]["CodTipo"]);

                // Limpa o DataTable 'sequancias' se necessário
                sequancias.Dispose();

                return isThereAnEvent;
            }
            catch { return false; }
        }
        public static bool EUmBoaNoite_Cpap_BomDia(int mouseX)
        {
            try
            {
                bool isThereAnEvent = false;

                //GlobVar.CodCanal = Convert.ToInt16(GlobVar.codSelected[loc]);


                DataTable sequancias = new DataTable();

                // Verifica se o DataTable 'eventosUpdate' está vazio
                if (GlobVar.eventosUpdate == null || GlobVar.eventosUpdate.Rows.Count == 0)
                {
                    return false;
                }

                // Filtra as linhas correspondentes
                var filteredRows = GlobVar.eventosUpdate.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodCanal1") == -1);

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
                    if (mouseX > Convert.ToInt64(sequancias.Rows[i]["Inicio"])&& mouseX < Convert.ToInt64(sequancias.Rows[i]["Duracao"]))
                    {
                        ProcessEvent(sequancias.Rows[i], 512);
                        EventMovement(sequancias.Rows[i], 512);
                        GlobVar.seqEvento = Convert.ToInt32(sequancias.Rows[i]["Seq"]);
                        GlobVar.CodEvento = Convert.ToInt32(sequancias.Rows[i]["CodEvento"]);
                        GlobVar.NumPagEvent = sequancias.Rows[i]["NumPag"].ToString();
                        GlobVar.CodCanalEvent = Convert.ToInt16(sequancias.Rows[i]["CodCanal1"]);

                        var rowAux = GlobVar.tbl_CadEvento.AsEnumerable().Where(row => row.Field<int>("CodEvento") == GlobVar.CodEvento).CopyToDataTable();
                        GlobVar.nomeEvento = rowAux.Rows[0]["DescrOriginal"].ToString();
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
                gl.Vertex(GlobVar.iniEventoMove - 25, GlobVar.StartY[YAdjusted] + 1, -1.0f);
                gl.Vertex(GlobVar.durEventoMove + 25, GlobVar.StartY[YAdjusted] + 1, -1.0f);
                gl.Vertex(GlobVar.durEventoMove + 25, GlobVar.EndY[YAdjusted] - 1, -1.0f);
                gl.Vertex(GlobVar.iniEventoMove - 25, GlobVar.EndY[YAdjusted] - 1, -1.0f);
                gl.End();
                gl.Flush();
            }
        }

        public static void DrawingAnEvent(int qtdGraf, OpenGL gl, float[] desenhoLoc) 
        {
            try
            {
                if (GlobVar.lastEvent != null) {
                    float[] color = new float[3];

                    var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                                                .Where(row => row.Field<int>("CodEvento") == GlobVar.lastEvent).CopyToDataTable();
                    int rgbDex = Convert.ToInt32(rowInfoEvento.Rows[0]["CorFundo"]);
                    color = plotGrafico.ObterComponentesRGB(rgbDex);
                    int YAdjusted = Plotagem.EncontrarValorMaisProximo(desenhoLoc, GlobVar.startY);

                    //gl.Color(0.0f, 0.0f, 0.0f);

                    gl.Begin(OpenGL.GL_QUADS);
                    gl.PointSize(3.0f); // Define o tamanho dos pontos
                    gl.Color(color[0], color[1], color[2], 0.44f);
                    //gl.Color(0, 0, 0);
                    //gl.ColorMask(3, 6, 7, alpha);
                    gl.Vertex((int)GlobVar.startX, GlobVar.StartY[YAdjusted] + 5, -1.5f);
                    gl.Vertex(GlobVar.endX, GlobVar.StartY[YAdjusted] + 5, -1.5f);
                    gl.Vertex(GlobVar.endX, GlobVar.EndY[YAdjusted] - 5, -1.5f);
                    gl.Vertex((int)GlobVar.startX, GlobVar.EndY[YAdjusted] - 5, -1.5f);
                    gl.End();
                    //startX = 0;
                }
            }
            catch { }
        }

        //metodo para fazer a escrita do Bom dia e dos tipos de eventos sobre eles
        public static void DrawTexts(int qtdGraf, OpenGL gl, float[] desenhoLoc)
        {
            try
            {
                DataTable eventos = GlobVar.eventosUpdate;
                float[] color = new float[3];
                float[] colorLinha = new float[3];
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

                        gl.Begin(OpenGL.GL_2D);
                        int writeX = 0;
                        int writeY = (int)GlobVar.EndY[YAdjusted] - 10;
                        ConvertToScreenCoordinates(inicio, 0, out writeX, out writeY);
                        writeX += 4;
                        writeY = (int)GlobVar.EndY[YAdjusted] - 15;
                        gl.DrawText(writeX, writeY, 0.0f, 0.0f, 0.0f, "Arial Narrow", 10, "");
                        gl.DrawText(writeX, writeY, 0.0f, 0.0f, 0.0f, "Arial Narrow", 10, tipoCanal);

                        gl.End();
                        gl.Flush();
                        des--;

                        //plotNumerico.PlotNumerico(qtdGraf, gl, desenhoLoc);

                    }
                    if (codEvento == 18 || codEvento == 19)
                    {
                        var rowInfoEvento = GlobVar.tbl_CadEvento.AsEnumerable()
                                .Where(row => row.Field<int>("CodEvento") == codEvento).CopyToDataTable();
                        int rgbDex = Convert.ToInt32(rowInfoEvento.Rows[0]["CorFundo"]);
                        color = plotGrafico.ObterComponentesRGB(rgbDex);
                        colorLinha = plotGrafico.ObterComponentesRGB(Convert.ToInt32(rowInfoEvento.Rows[0]["CorTexto"]));

                        string tipoCanal = rowInfoEvento.Rows[0]["DescrEvento"].ToString();

                        int writeX = 0;
                        int writeY;
                        int whereToDraw = ((termino - inicio) / 2) + inicio;
                        ConvertToScreenCoordinates(whereToDraw, 0, out writeX, out writeY);
                        writeY = (int)GlobVar.sizeOpenGl.Y - (int)(GlobVar.sizeOpenGl.Y / 3) - 30;



                        float verticalSpacing = 18; // Ajuste conforme necessário para o espaçamento vertical entre caracteres
                        float currentY = writeY;

                        //gl.DrawText((int)writeX, (int)writeY, 0, 0, 0, "Arial Narrow", 2, "");
                        //gl.DrawText((int)writeX, (int)writeY, 0, 0, 0, "Arial Narrow", 2, ".");
                        gl.Begin(OpenGL.GL_2D);
                        for (int i = 0; i < tipoCanal.Length; i++)
                        {
                            string a = Convert.ToString(tipoCanal[i]);
                            gl.DrawText((int)writeX, (int)currentY, 0, 0, 0, "Arial Narrow", 18, a);
                            gl.DrawText((int)writeX, (int)currentY, 0, 0, 0, "Arial Narrow", 18, a);
                            currentY -= verticalSpacing; // Move para a próxima linha verticalmente
                        }
                        gl.End();
                        gl.Flush();

                        //DrawVerticalText(gl, (int)writeX, (int)writeY, $"{tipoCanal}", "Arial", 20);

                    }
                }

            }
            catch (Exception ex)
            {
            }
        }

        public static void LastEvent(float[] desenhoLoc, float startY)
        {
            try{
                int loc = EncontrarValorMaisProximo(desenhoLoc, startY);
                DataTable sequancias = new DataTable();

                // Verifica se o DataTable 'eventosUpdate' está vazio
                if (GlobVar.eventosUpdate == null || GlobVar.eventosUpdate.Rows.Count == 0)
                {
                    return;
                }

                var filteredRows = GlobVar.eventosUpdate.AsEnumerable()
                                                    .Where(row => row.Field<int>("CodCanal1") == GlobVar.codSelected[loc]);

                // Verifica se há linhas filtradas antes de tentar copiar para um DataTable
                if (!filteredRows.Any())
                {
                    DataTable canHaveEventTable = new DataTable();

                    int codCanal = Convert.ToInt16(GlobVar.codSelected[loc]);

                    var codTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                    .Where(row => row.Field<int>("CodCanal") == codCanal).CopyToDataTable();
                    int tipoCanal = Convert.ToInt16(codTipoCanal.Rows[0]["CodTipo"]);


                    var canHaveEvent = GlobVar.tbl_EventoTipoCanal.AsEnumerable()
                                                    .Where(row => row.Field<int>("CodTipoCanal") == tipoCanal);
                    if(canHaveEvent.Any()) {
                        if(tipoCanal == 13) // forca para que no canal da canula o primeiro evento a ser criado, seja uma hipopneia
                        {
                            GlobVar.lastEvent = 5;
                            return;
                        }
                        else
                        {
                            canHaveEventTable = canHaveEvent.CopyToDataTable();
                            canHaveEventTable.AsEnumerable().OrderBy(row => row.Field<int>("CodCanal"));

                            GlobVar.lastEvent = Convert.ToInt16(canHaveEventTable.Rows[0]["CodEvento"]);
                            return;
                        }
                    }
                    else
                    {
                        GlobVar.lastEvent = null;
                        return;
                    }
                }
                filteredRows.AsEnumerable().OrderBy(row => row.Field<int>("Seq"));

                sequancias = filteredRows.CopyToDataTable();

                // Verifica se o DataTable 'sequancias' está vazio
                if (sequancias == null || sequancias.Rows.Count == 0)
                {
                    return;
                }
                var groupedRows = filteredRows.GroupBy(row => row.Field<int>("Seq"));

                foreach (var group in groupedRows)
                {
                    var firstRow = group.Last();
                    int codEvento = firstRow.Field<int>("CodEvento");
                    GlobVar.lastEvent = codEvento;
                }

            }
            catch (Exception ex) { }
        }

        public static void CreatBomDiaCpapBoaNoite(int inicio, int codEvento)
        {
            try
            {
                if(GlobVar.eventosUpdate.AsEnumerable().Any(row => row.Field<int>("CodEvento") == codEvento))
                {
                    DeleteBomDiaCpapBoaNoite(codEvento);
                }

                DataTable eventos = GlobVar.eventosUpdate;

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
                int auxLoc = inicio / GlobVar.namos;

                int inicioBCB = ((auxLoc * GlobVar.namos) + 1);
                int finalBCB = (((auxLoc + 1) * GlobVar.namos) - 1);

                // Calcular NumPag para início e término
                int numPagInicio = inicioBCB / GlobVar.namos;
                int numPagTermino = finalBCB / GlobVar.namos;
                string numPag = $"{numPagInicio} -- {numPagTermino}";
                // Obter o próximo valor de Seq
                int seq = eventos.Rows.Count > 0 ? eventos.AsEnumerable().Max(row => row.Field<int>("Seq")) + 1 : 1;
                // Adicionar dados ao DataTable
                GlobVar.eventosUpdate.Rows.Add(seq, numPag, codEvento, -1, inicioBCB, finalBCB);
                AlteraBD.GravaEvento(seq, numPagInicio, (int)GlobVar.lastEvent, -1, -1, inicioBCB, finalBCB, GlobVar.namos, numPagTermino);

                // Exportar DataTable para Excel
                string excelFilePath = @"C:\Teste\Teste";
                //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
                eventos.Dispose();

            }
            catch { }
        }

        //Metodo feito para encontrar o Bom dia, Boa boite e o Cpap para deletar do dt
        public static void DeleteBomDiaCpapBoaNoite(int codEvento)
        {
            DataView view = new DataView(GlobVar.eventosUpdate);
            var auxTable = GlobVar.eventosUpdate.AsEnumerable().Where(row => row.Field<int>("CodEvento") == codEvento).CopyToDataTable();
            int Seq = Convert.ToInt16(auxTable.Rows[0]["Seq"]);
            view.RowFilter = $"CodEvento = {codEvento}";

            if (view.Count > 0)
            {
                // Se a linha for encontrada, remova-a usando o DataTable original
                DataRow rowToRemove = view[0].Row;
                GlobVar.eventosUpdate.Rows.Remove(rowToRemove);
            }

            AlteraBD.ExcluiEvento(Seq);
        }

        public static void ListOfEvent()
        {
            try{
            GlobVar.listEventsCanHave.Clear();

            var EventsCan = GlobVar.tbl_EventoTipoCanal.AsEnumerable()
                                                        .Where(row => row.Field<int>("CodTipoCanal") == GlobVar.CodTipoCanalEvent).CopyToDataTable();
                for (int i = 0; i < EventsCan.Rows.Count; i++)
                {
                    int AuxCodCanal = Convert.ToInt16(EventsCan.Rows[i]["CodEvento"]);

                    if (GlobVar.tbl_CadEvento.AsEnumerable().Any(row => row.Field<int>("CodEvento") == AuxCodCanal))
                    {
                        var auxTabelEvents = GlobVar.tbl_CadEvento.AsEnumerable().Where(row => row.Field<int>("CodEvento") == AuxCodCanal).CopyToDataTable();
                        if (auxTabelEvents != null)
                        {
                            GlobVar.listEventsCanHave.Add(auxTabelEvents.Rows[0]["DescrEvento"].ToString());
                        }
                    }
                }
            }
            catch { }
        }
    }
}

