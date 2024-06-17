using Accord.Math;
using System;
using System.IO;

namespace PlotagemOpenGL.auxi
{
    internal class LeitorDiretorio
    {
        public static void LeituraDiretorio()
        {
            string filePath = @"C:\Diretorios.txt";

            using (FileStream fl = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                string[] lines = File.ReadAllLines(filePath);
                string[] valoresStr;
                

                // Iterando sobre as linhas do arquivo
                foreach (string line in lines)
                {
                    // Separando os valores por vírgula
                    valoresStr = line.Split(',');
                    
                    GlobVar.textFile = valoresStr[0];
                    GlobVar.bDataFile = valoresStr[1];
                    GlobVar.configBD = valoresStr[2];

                }

            }
        }
    }
}
