using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
//using Accord.Math;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Threading;
//using CenterSpace.NMath.Core;
using System.Reflection;
using SharpGL.SceneGraph.Raytracing;

namespace PlotagemOpenGL.auxi
{
    internal class Leitura
    {
        public static void QuantidadeCanais()
        {
            byte[] buffer0 = new byte[395];
            byte[] buffer1 = new byte[8];
            byte[] buffer2 = new byte[8];

            try
            {
                using (FileStream fs = new FileStream(GlobVar.textFile, FileMode.Open, FileAccess.Read))
                {
                    fs.Read(buffer0, 0, buffer0.Length);

                    GlobVar.cabecalho = (Encoding.UTF8.GetString(buffer0));

                    fs.Read(buffer1, 0, buffer1.Length);

                    string npag1 = (Encoding.UTF8.GetString(buffer1));

                    GlobVar.npagin = Convert.ToInt32(npag1);

                    GlobVar.npag = npag1.Replace(" ", "");

                    fs.Read(buffer2, 0, buffer2.Length);

                    string tipocanais1 = (Encoding.UTF8.GetString(buffer2));

                    GlobVar.tipocanais = tipocanais1.Replace(" ", "");

                    int ncanint = Convert.ToInt16(tipocanais1.Replace(" ", "")); //Int16.Parse(tipocanais, System.Globalization.NumberStyles.HexNumber);
                    GlobVar.qtdCanais = new string[ncanint];

                    fs.Close();
                }
            }
            catch { }
        }
    }
}