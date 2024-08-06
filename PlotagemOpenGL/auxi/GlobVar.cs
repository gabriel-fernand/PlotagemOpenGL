using SharpGL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Point = System.Drawing.Point;
using System.Text;
using ClassesBDNano;
using System.Data;
using System.Data.Odbc;
using Connection = ADODB.Connection;



namespace PlotagemOpenGL.auxi
{
    public class GlobVar
    {
        public static string STRINGAO;

        public static string textFile = @"C:\Users\dev_i\source\repos\Dat\01368_01.dat";
        public static string bDataFile = @"C:\Users\dev_i\source\repos\Dat\01368_01.mdb";
        public static string configBD = @"C:\Users\dev_i\source\repos\Dat\Configuração.mdb";
        public static string cabecalho;
        public static int npagin;
        public static string npag;
        public static string tipocanais;
        public static int amos;
        public static int startpos;
        public static int sizesample;
        public static int size;
        public static double[] valorout;
        public static int numeroAmos = 8;
        public static int indiceDat = 0;

        //Variaveis para mexer nos eventos
        public static bool drawBordenInAnEvent;
        public static int iniEventoMove;
        public static int durEventoMove;
        public static int seqEvento;
        public static int CodEvento;
        public static int CodCanal;
        public static int CodCanalEvent;
        public static int CodTipoCanalEvent;
        public static string nomeEvento;
        public static string NumPagEvent;
        public static string Event;
        public static string InicioEvent;
        public static string DuracaoEvent;
        public static int MinimumValueEvent = 256;
        public static int? lastEvent;
        public static bool EventHasChange = false;
        public static List<string> listEventsCanHave = new List<string>();
        public static Point rightClickSave;
        public static bool isTheDBOpen = false;

        public static Connection cnn_dbExame = new Connection();
        public static Connection cnn_dbConfig = new Connection();

        //public static cls_dbExame obj_dbEventos = new ClassesBDNano.cls_dbExame();  //Nao esta funcionando, pois da um erro para estanciar
        public static cls_DataSource obj_dbDataSource;
        public static cls_dbConfig obj_dbConfig;

        public static DataTable eventosUpdate = new DataTable();
        public static DataTable eventos = new DataTable();
        public static DataTable codEventos = new DataTable();
        public static DataTable tbl_TipoCanal = new DataTable();
        public static DataTable tbl_EventoTipoCanal = new DataTable();

        public static DataTable tbl_Montagem = new DataTable();
        public static DataTable tbl_MontCanal = new DataTable();
        public static DataTable tbl_MontGrav = new DataTable();
        public static DataTable tbl_TipoExame = new DataTable();
        public static DataTable tbl_MontagemSelecionada = new DataTable();
        public static DataTable tbl_CadTipoCanal = new DataTable();
        public static DataTable tbl_CadEvento = new DataTable();

        public static string tipoExame;

        public static int lastcall;
        public static double metadeavg;
        public static double lisup;
        public static int indice1;
        public static string[] valorout1;
        public static double indicetotal;
        public static double qtdgrafico;

        public static string[] qtdCanais;

        public static Vector2 sizeOpenGl;
        public static Vector2 sizePainelExams;

        public static Vector2 sizeLabelExams;
        public static Vector2 sizeButtons;
        public static Vector2 sizePanelLb;
        public static Vector3[] colors;

        public static Vector2 locBut;
        public static Vector2 locScale;


        public static int[] grafSelected;
        public static int[] codSelected;
        public static short[,] matrizCompleta;
        public static short[,] matrizCanal;
        public static int[] ponteiroI;
        public static int[] ponteiroF;

        public static string[] nomeCanais;
        public static int[] txPorCanal;
        public static int[] codCanal;
        public static double[] scale;
        public static bool[] SomenteNums;
        public static float[] Amplitude = new float[34];
        
        public static int maximaVect = 130000;
        public static int indice = 0;
        public static int indiceNumero = 0;
        public static int maximaNumero; // = (int)GlobVar.sizeOpenGl.X;
        public static float[] desenhoLoc;

        public static float saltoTelas;
        public static float SPEED = 1.0f;

        public static int namos = 512;
        public static int segundos = 30;
        public static int tmpEmTela = 240;
        public static int finalTelaNumerico;

        public static int namosNumerico = 8;
        public static int tmpEmTelaNumerico = 240;

        public static int inicioTela;
        public static int finalTela;

        public static int endX;
        public static int endY;
        public static int? startX;
        public static int startY;
        public static float[] EndY;
        public static float[] StartY;
        public static int canal;
        public static float[] loc;
    }
}