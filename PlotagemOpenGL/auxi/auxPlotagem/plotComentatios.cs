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
using System.Drawing;
using System.Data.Odbc;

namespace PlotagemOpenGL.auxi.auxPlotagem
{
    internal class plotComentatios {

        static int seq;
        static int XSize = 100 * (GlobVar.segundos / 2);
        static int YSize = 100;
        static int cor = 10485759;
        static float[] color = new float[3];


        public static void DesenhaComentario(OpenGL gl)//(int qtdGraf, OpenGL gl, int Xinicial, int Yinicial)
        {
            try
            {
                int codMontagem = Convert.ToInt16(GlobVar.tbl_MontGrav.Rows[0]["CodMontagem"]);
                color = plotGrafico.ObterComponentesRGB(cor);
                GlobVar.tbl_Comentarios.AsEnumerable().Where(row => row.Field<int>("CodMontagem") == codMontagem);
                for(int i = 0; i< GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    string comentario = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                    float xLoc = 0;
                    float yLoc = 0;

                    int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);

                    int pag = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]);
                    int pagLoc = pag * GlobVar.namos;

                    xLoc = pagLoc + Xi;
                    yLoc = Tela_Plotagem.openglControl1.Height - YSize;// - Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    //yLoc = Math.Abs(Yi - Tela_Plotagem.openglControl1.Height);

                    gl.Begin(OpenGL.GL_QUADS);
                    gl.PointSize(3.0f); // Define o tamanho dos pontos
                    gl.Color(color[0], color[1], color[2], 0.44f);
                    //gl.ColorMask(3, 6, 7, alpha);
                    gl.Vertex(xLoc, yLoc, -1.8f);
                    gl.Vertex(xLoc + XSize, yLoc, -1.8f);
                    gl.Vertex(xLoc + XSize, yLoc + YSize, -1.8f);
                    gl.Vertex(xLoc, yLoc + YSize, -1.8f);
                    gl.End();
                    gl.Flush();

                }
            }
            catch { }
        }
        public static int AtualizarProxSeqEvento()
        {
            string connectionString = @"Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=path_to_your_database_file;Uid=Admin;Pwd=;";
            string querySelect = "SELECT ProxSeqEvento FROM tbl_SeqEventos";
            string queryUpdate = "UPDATE tbl_SeqEventos SET ProxSeqEvento = ?";
            int seq;
            try
            {
                using (var connection = new OdbcConnection(connectionString))
                {
                    connection.Open();

                    // Step 1: Obter o valor atual de ProxSeqEvento
                    int proxSeqEvento;
                    using (var command = new OdbcCommand(querySelect, connection))
                    {
                        proxSeqEvento = (int)command.ExecuteScalar(); // Obtem o valor do banco
                    }
                    seq = proxSeqEvento;
                    // Step 2: Incrementar o valor
                    proxSeqEvento += 1;

                    // Step 3: Atualizar o valor no banco de dados
                    using (var command = new OdbcCommand(queryUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@ProxSeqEvento", proxSeqEvento);
                        command.ExecuteNonQuery(); // Executa o update
                    }
                    connection.Close();
                }
                return seq;
            }
            catch (Exception ex)
            {
                return 0;
                // Trate as exceções adequadamente
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        public static void AdicionarComentario(int Xinicial, int Yinicial, string Comentario)
        {
            try{
                seq = AtualizarProxSeqEvento();

            }
            catch
            {

            }
        }
    }
}
