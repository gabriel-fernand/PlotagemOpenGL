using Accord.Math;
using System;
using System.IO;
using Tensorflow.Operations.Activation;

namespace PlotagemOpenGL.auxi
{
    internal class LeitorDiretorio
    {
        public static void LeituraDiretorio()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            // Se o arquivo está na raiz do projeto (copiado para o bin/Debug ou bin/Release):
            string filePath = Path.Combine(basePath, "Diretorios.txt");
            //string filePath = "\Diretorios.txt";

            using (FileStream fl = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                string[] lines = File.ReadAllLines(filePath);
                string[] valoresStr;
                

                // Iterando sobre as linhas do arquivo
                foreach (string line in lines)
                {
                    //Separando os valores por vírgula
                    valoresStr = line.Split(',');
                    
                    GlobVar.textFile = Path.Combine(basePath, valoresStr[0]);
                    GlobVar.bDataFile = Path.Combine(basePath, valoresStr[1]);

                    GlobVar.configBD = Path.Combine(basePath, valoresStr[2]);

                }

            }
        }
    }
}
