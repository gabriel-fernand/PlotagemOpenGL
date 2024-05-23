using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using System.Text;

namespace PlotagemOpenGL.auxi
{
    public class GlobVar
    {
        public static string textFile = @"C:\Users\dev_i\source\repos\Dat\01368_01.dat";
        public static string bDataFile = @"C:\Users\dev_i\source\repos\Dat\01368_01.mdb";
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

        public static DataTable eventos = new DataTable();

        public static int lastcall;
        public static double metadeavg;
        public static double lisup;
        public static int indice1;
        public static string[] valorout1;
        public static double indicetotal;
        public static double qtdgrafico;

        public static string[] qtdCanais;

        public static double[] canalA;

        public static double[] canalB;

        public static double[] canalC;

        public static double[] canalD;

        public static double[] canalE;

        public static double[] canalF;

        public static double[] canalG;

        public static double[] canalH;

        public static double[] canalI;

        public static double[] canalJ;

        public static double[] canalK;

        public static double[] canalL;

        public static double[] canalM;

        public static double[] canalN;

        public static double[] canalO;

        public static double[] canalP;

        public static double[] canalQ;

        public static double[] canalR;

        public static double[] canalS;

        public static double[] canalT;

        public static double[] canalU;

        public static double[] canalV;

        public static double[] canalW;


        public static Vector2 sizeOpenGl;
        public static Vector2 sizePainelExams;

        public static Vector2 sizeLabelExams;
        public static Vector2 sizeButtons;
        public static Vector2 sizePanelLb;
        public static Vector3[] colors;

        public static Vector2 locBut;
        public static Vector2 locScale;

        public static int[] grafSelected;
        public static int[,] matrizCompleta;
        public static double[,] matrizCanal;
        public static int[] ponteiro;

        public static string[] nomeCanais;
        public static int[] txPorCanal;
        public static int[] codCanal;
        public static double[] scale;

        public static double[] lowHertz = new double[6] { 0, 0.005f, 0.010f, 0.015f, 0.020f, 0.025f };
        public static double[] highHertz = new double[4] { 0, 0.007f, 0.01f, 0.04f };

        public static int maximaVect = 130000;
        public static int indice = 0;

        public static float saltoTelas;
        public static float SPEED = 1.0f;

        public static int namos = 512;
        public static int segundos = 30;
        public static int tmpEmTela = 240;

        public static int inicioTela;
        public static int finalTela;

        public static float escalaLb1 = 1.0f;
        public static float escalaLb2 = 1.0f;
        public static float escalaLb3 = 1.0f;
        public static float escalaLb4 = 1.0f;
        public static float escalaLb5 = 1.0f;
        public static float escalaLb6 = 1.0f;
        public static float escalaLb7 = 1.0f;
        public static float escalaLb8 = 1.0f;
        public static float escalaLb9 = 1.0f;
        public static float escalaLb10 = 1.0f;
        public static float escalaLb11 = 1.0f;
        public static float escalaLb12 = 1.0f;
        public static float escalaLb13 = 1.0f;
        public static float escalaLb14 = 1.0f;
        public static float escalaLb15 = 1.0f;
        public static float escalaLb16 = 1.0f;
        public static float escalaLb17 = 1.0f;
        public static float escalaLb18 = 1.0f;
        public static float escalaLb19 = 1.0f;
        public static float escalaLb20 = 1.0f;
        public static float escalaLb21 = 1.0f;
        public static float escalaLb22 = 1.0f;
        public static float escalaLb23 = 1.0f;

    }
}
