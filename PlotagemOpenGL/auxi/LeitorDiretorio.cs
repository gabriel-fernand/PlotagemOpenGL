using Accord.Math;
using System;
using System.IO;
using System.Windows;
using Tensorflow.Operations.Activation;

namespace PlotagemOpenGL.auxi
{
    internal class LeitorDiretorio
    {
        public static void LeituraDiretorio()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                GlobVar.configBD = Path.Combine(basePath, "Configuração.mdb");

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


                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "Erro na Leitura");
            }
        }
    }
}
