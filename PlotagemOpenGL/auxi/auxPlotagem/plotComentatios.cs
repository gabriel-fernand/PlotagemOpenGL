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
                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    string comentario = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                    float xLoc = 0;
                    float yLoc = 0;

                    int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);

                    int pag = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]);
                    int pagLoc = pag * GlobVar.namos;

                    xLoc = pagLoc + Xi;
                    //yLoc = Tela_Plotagem.openglControl1.Height;// - Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    //yLoc = Math.Abs(Yi - Tela_Plotagem.openglControl1.Height);

                    gl.Begin(OpenGL.GL_QUADS);
                    gl.PointSize(3.0f); // Define o tamanho dos pontos
                    gl.Color(color[0], color[1], color[2], 0.44f);
                    //gl.ColorMask(3, 6, 7, alpha);
                    gl.Vertex(xLoc, yLoc, -1.8f);
                    gl.Vertex(xLoc + XSize, yLoc, -1.8f);
                    gl.Vertex(xLoc + XSize, yLoc - YSize, -1.8f);
                    gl.Vertex(xLoc, yLoc - YSize, -1.8f);
                    gl.End();
                    gl.Flush();

                }
            }
            catch { }
        }
        public static int AtualizarProxSeqEvento()
        {

            string connectionString = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
            string querySelect = "SELECT ProxSeqEvento FROM tbl_SeqEvento";
            string queryUpdate = "UPDATE tbl_SeqEvento SET ProxSeqEvento = ?";
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
            try {
                seq = AtualizarProxSeqEvento();
                int CodMontagem = Convert.ToInt16(GlobVar.tbl_MontGrav.Rows[0]["CodMontagem"]);
                float outX;
                float outY;
                int XSize = 100 * (GlobVar.segundos / 2);
                int YSize = 100;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);

                int numPag = (int)outX / 512;
                int Xi = Math.Abs((int)(numPag * 512) - (int)outX);

                GlobVar.tbl_Comentarios.Rows.Add(seq, Comentario, CodMontagem, numPag, Xi, Yinicial, XSize, YSize);
                AlteraBD.GravaComentario(seq, Comentario, CodMontagem, numPag, Xi, Yinicial, XSize, YSize);
                //public static long GravaComentario(int seq, string Comentario, int CodMontagem, int NumPag, int xi, int yi, int DuracaoX, int DuracaoY)
            }
            catch
            {

            }
        }

        //Metodos para saber se o mouse esta no evento ou na suas respectivas bordas
        public static bool IsThereAComment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xini + 20 && outX <= Xdur - 20))
                    {
                        if (Yinicial >= Yini + 10 && Yinicial <= Ydur - 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsThereAXstartComment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xini - 20 && outX <= Xini + 20))
                    {
                        if (Yinicial >= Yini + 10 && Yinicial <= Ydur - 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsThereAXEndComment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX <= Xdur + 20 && outX >= Xdur - 20))
                    {
                        if (Yinicial >= Yini + 10 && Yinicial <= Ydur - 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsThereAYstartComment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;
                    if ((outX >= Xini + 35 && outX <= Xdur - 35))
                    {
                        if (Yinicial <= Yini + 10 && Yinicial >= Yini - 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsThereAYEndComment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xini + 35 && outX <= Xdur - 35))
                    {
                        if (Yinicial >= Ydur - 10 && Yinicial <= Ydur + 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsThereX0Y0Comment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xini - 20 && outX <= Xini + 20))
                    {
                        if (Yinicial >= Yini - 10 && Yinicial <= Yini + 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }

        }
        public static bool IsThereX1Y0Comment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xdur - 20 && outX <= Xdur + 20))
                    {
                        if (Yinicial >= Yini - 10 && Yinicial <= Yini + 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }

        }
        public static bool IsThereX0Y1Comment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xini - 20 && outX <= Xini + 20))
                    {
                        if (Yinicial >= Ydur - 10 && Yinicial <= Ydur + 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }

        }
        public static bool IsThereX1Y1Comment(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;
                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);
                int numPag = (int)outX / 512;
                //DataTable existente = new DataTable();


                if (GlobVar.tbl_Comentarios == null || GlobVar.tbl_Comentarios.Rows.Count == 0)
                {
                    return false;
                }

                for (int i = 0; i < GlobVar.tbl_Comentarios.Rows.Count; i++)
                {
                    int XSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoX"]);
                    int YSize = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["DuracaoY"]);
                    int Xini = (Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["NumPag"]) * GlobVar.namos) + Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                    int Xdur = Xini + XSize;
                    int Yini = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                    int Ydur = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]) + YSize;

                    if ((outX >= Xdur - 20 && outX <= Xdur + 20))
                    {
                        if (Yinicial >= Ydur - 10 && Yinicial <= Ydur + 10)
                        {
                            float xLoc = 0;
                            float yLoc = 0;

                            int Yi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Yi"]);
                            int Xi = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Xi"]);
                            Tela_Plotagem.ConvertToOpenGLCoordinates(Xi, Yi, out xLoc, out yLoc);


                            GlobVar.txtComment = GlobVar.tbl_Comentarios.Rows[i]["Comentario"].ToString();
                            GlobVar.XiYi.X = Xini;
                            GlobVar.XiYi.Y = (int)yLoc;
                            GlobVar.Yi = Yi;
                            GlobVar.XfYf.X = Xdur;
                            GlobVar.YSize = YSize;
                            GlobVar.XSize = XSize;
                            GlobVar.XfYf.Y = (int)outY - YSize; //Recebe o valor de tamanho, pois o Yi esta armazedado com valor de tela nao dimensao
                            GlobVar.CommentSeq = Convert.ToInt16(GlobVar.tbl_Comentarios.Rows[i]["Seq"]);
                            return true;
                        }
                    }
                }

                return sim;
            }
            catch
            {
                return false;
            }

        }

        public static void DrawCommentBorder(OpenGL gl, int locPlus = 0)
        {
            float Yloc;
            int x = 0;
            float y = 0;
            Tela_Plotagem.ConvertToOpenGLCoordinates(x, GlobVar.XiYi.Y, out y, out Yloc);


            gl.Begin(OpenGL.GL_LINE_LOOP);
            gl.PointSize(2.0f); // Define o tamanho dos pontos
            gl.Color(0, 0, 0, 0.44f);
            //gl.ColorMask(3, 6, 7, alpha);
            gl.Vertex(GlobVar.XiYi.X - 3, GlobVar.XiYi.Y + 3, -1.0f);
            gl.Vertex(GlobVar.XiYi.X + GlobVar.XSize + 3, GlobVar.XiYi.Y + 3, -1.0f);
            gl.Vertex(GlobVar.XiYi.X + GlobVar.XSize + 3, Math.Abs(GlobVar.XiYi.Y - GlobVar.YSize) - 3, -1.0f);
            gl.Vertex(GlobVar.XiYi.X - 3, Math.Abs(GlobVar.XiYi.Y - GlobVar.YSize) - 3, -1.0f);
            gl.End();
            gl.Flush();

        }
        public static void UpdateComment(int Xinicial, int Yinicial)
        {
            try{
                int CodMontagem = Convert.ToInt16(GlobVar.tbl_MontGrav.Rows[0]["CodMontagem"]);

                int numPag = GlobVar.XiYi.X / 512;

                int Xi = Math.Abs((int)(numPag * 512) - GlobVar.XiYi.X);

                DataView view = new DataView(GlobVar.tbl_Comentarios);
                view.RowFilter = $"Seq = {GlobVar.CommentSeq}";

                if( view.Count > 0 ) 
                {
                    DataRow rowToUpdate = view[0].Row;


                    rowToUpdate["Comentario"] = GlobVar.txtComment;
                    rowToUpdate["CodMontagem"] = CodMontagem;
                    rowToUpdate["NumPag"] = numPag;
                    rowToUpdate["Xi"] = Xi;
                    rowToUpdate["Yi"] = GlobVar.Yi;
                    rowToUpdate["DuracaoX"] = GlobVar.XSize;
                    rowToUpdate["DuracaoY"] = GlobVar.YSize;
                }
                AlteraBD.GravaComentario(GlobVar.CommentSeq,  GlobVar.txtComment, CodMontagem, numPag, Xi, GlobVar.Yi, GlobVar.XSize, GlobVar.YSize);
            }
            catch { }
        }
    }
}
