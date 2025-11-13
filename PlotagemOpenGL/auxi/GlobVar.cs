using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using Point = System.Drawing.Point;
using Connection = ADODB.Connection;
using System.IO;
using System.Data.OleDb;
using System.Drawing;



namespace PlotagemOpenGL.auxi
{
    public class GlobVar
    {
        public static string basePath = AppDomain.CurrentDomain.BaseDirectory;
        public static string STRINGAO;
        public static Point DimXY;
        public static string textFile = @"C:\Users\dev_i\source\repos\Dat\01368_01.dat";
        public static string bDataFile = @"C:\Users\dev_i\source\repos\Dat\01368_01.mdb";
        public static string dbRelatorio = Path.Combine(basePath, "Relatorios.mdb");

        public static string configBD = Path.Combine(basePath, "/Dat/Configuração.mdb");
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
        public static int[] FundoColor;
        public static int LastRowLoaded;
        public static bool MatrizCompleta;
        public static bool FiltroCompleto;
        public static int areaCarregadaAltMont;
        public static bool hipnoOpen = false;
        public static string FileName = "";


        public static string estagioAtual = "0";
        public static int qtdImpressao = 0;
        public static int qtdPeriodos = 0;

        public static string diretorioEstagioAtual0 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN0Select.png");
        public static string diretorioEstagioAtual1 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN1Select.png");
        public static string diretorioEstagioAtual2 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN2Select.png");
        public static string diretorioEstagioAtual3 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN3Select.png");
        public static string diretorioEstagioAtualR = Path.Combine(basePath, "Icones/IcoNumericos/IcoNRSelect.png");
        public static string diretorioEstagioAtualT = Path.Combine(basePath, "Icones/IcoNumericos/IcoNTSelect.png");
        public static string diretorioEstagioAtualN = Path.Combine(basePath, "Icones/IcoNumericos/IcoNNSelect.png");
        

        public static string diretorioEstagioAnteriorProximoNada = Path.Combine(basePath, "Icones/IcoNumericos/IcoNVazio.png");
        public static string diretorioEstagioAnteriorProximo0 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN0.png");
        public static string diretorioEstagioAnteriorProximo1 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN1.png");
        public static string diretorioEstagioAnteriorProximo2 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN2.png");
        public static string diretorioEstagioAnteriorProximo3 = Path.Combine(basePath, "Icones/IcoNumericos/IcoN3.png");
        public static string diretorioEstagioAnteriorProximoR = Path.Combine(basePath, "Icones/IcoNumericos/IcoNR.png");
        public static string diretorioEstagioAnteriorProximoT = Path.Combine(basePath, "Icones/IcoNumericos/IcoNT.png");
        public static string diretorioEstagioAnteriorProximoN = Path.Combine(basePath, "Icones/IcoNumericos/IcoNN.png");

        public static int Pos_C = 0;
        public static int Pos_D = 0;
        public static int Pos_E = 0;
        public static int Pos_B = 0;

        public static string g_Traducoes = "";

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
        public static float DuracaoEvent;
        public static int MinimumValueEvent = 256;
        public static int? lastEvent;
        public static string txtLastEvent = "";
        public static bool EventHasChange = false;
        public static List<string> listEventsCanHave = new List<string>();
        public static Point rightClickSave;
        public static bool isTheDBOpen = false;
        public static int satuMinCanal;
        public static string posiCanal;
        public static int[] minSat;
        public static int[] minPosi;
        public static bool shiftactive = false;

        //variaveis pasra mexer nos coimentasrios
        public static Point XiYi;
        public static Point XfYf;
        public static int Yi;
        public static string txtComment;
        public static int CommentSeq;
        public static int XSize;
        public static int YSize;

        public static int PosCima;
        public static int PosDireita;
        public static int PosEsquerda;
        public static int PosBaixo;
        public static int PosIncremento;

        public static Connection cnn_dbExame = new Connection();
        public static Connection cnn_dbConfig = new Connection();

        //public static cls_dbExame obj_dbEventos = new ClassesBDNano.cls_dbExame();  //Nao esta funcionando, pois da um erro para estanciar
        public static string AtualEvento;
        public static string g_dados_fc_separada = "";
        public static int codMont;
        public static DataTable eventosUpdate = new DataTable();
        public static DataTable eventos = new DataTable();
        public static DataTable tbl_CadCanal = new DataTable();
        public static DataTable tbl_TipoCanal = new DataTable();
        public static DataTable tbl_EventoTipoCanal = new DataTable();

        public static DataTable tbl_Montagem = new DataTable();
        public static DataTable tbl_MontagemOriginal = new DataTable();
        public static DataTable tbl_MontCanal = new DataTable();
        public static DataTable tbl_MontGrav = new DataTable();
        public static DataTable tbl_TipoExame = new DataTable();
        public static DataTable tbl_MontagemSelecionada = new DataTable();
        public static DataTable tbl_CadTipoCanal = new DataTable();
        public static DataTable tbl_CadEvento = new DataTable();
        public static DataTable tbl_Comentarios = new DataTable();
        public static DataTable tbl_DadosExame = new DataTable();
        public static DataTable tbl_Paginas = new DataTable();
        public static DataTable tbl_ResumoExame = new DataTable();
        public static DataTable tbl_SelImpressao = new DataTable();
        public static DataTable tbl_SeqEvento = new DataTable();
        public static DataTable tbl_ArqVideo = new DataTable();
        public static DataTable tbl_CanaisAdquiridos = new DataTable();
        public static DataTable tbl_PosEstagio = new DataTable();

        public static DataTable tbl_HipnoGrupos = new DataTable();
        public static DataTable tbl_HipnoSubGrupos = new DataTable();
        public static DataTable tbl_JanelaResumoItens = new DataTable();
        public static DataTable tbl_JanelaResumo = new DataTable();
        public static DataTable tbl_Estagios = new DataTable();
        public static DataTable tbl_EstagiosInfatil = new DataTable();
        public static DataTable tbl_ParametrosParaAnalisar = new DataTable();
        public static DataTable tbl_RelatResumo = new DataTable();
        public static DataTable tbl_RelatResumoItem = new DataTable();
        public static DataTable tbl_HipnoLaudo = new DataTable();
        public static DataTable tbl_DadosClinica = new DataTable();
        public static DataTable Cons_Eventos = new DataTable();

        public static DataTable grd_HipoVent = new DataTable();

        public static int CodJanela;

        public static int ultimaPag;

        public static int DessatuDesconsiderar = 60;
        public static int[] canaisReferencia;
        public static string[] nomeReferencia;
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
        public static float[,] matrizCanal;
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
        public static float ponteiroVideo;

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

        public static int XRedimension;
        public static float ponteiroRedimension;

        public static int novaLarguraPainel;
        public static int novaLarguraOpenGL;
        public static int larguraMinimaOpenGL;
        public static int larguraMinimaPainel;
        public static int TipoCanalAlt;
        public static int IndexCanal;

        public static OleDbConnection ConnectionBDdat;
        public static OleDbConnection cnn_dbRelatorio;
        public static OleDbConnection ConnectionConfig;

        public static Dictionary<string, Image> imagensEstagio = new Dictionary<string, Image>();
        public static List<(int numPag, int estagio)> Atualizados = new List<(int numPag, int estagio)>();
        public static List<(int seq, int NumPag, int CodEvento, int CodCanal1, int CodCanal2, int Inicio, int duracao, int sizepag, int LasPag, int? MenorSat, string? Posicao)> GravEvent = new List<(int seq, int NumPag, int CodEvento, int CodCanal1, int CodCanal2, int Inicio, int duracao, int sizepag, int LasPag, int? MenorSat, string? Posicao)>();
    }
}