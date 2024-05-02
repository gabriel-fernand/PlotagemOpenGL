using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PlotagemOpenGL.auxi
{
    internal class Canais
    {
        public static float[] yCanais;
        public static float[] yButtons;
        public static string[] canais;
        public static void LerCanais()
        {
            string filePath = @"C:\Users\dev_i\source\repos\PlotagemOpenGL\PlotagemOpenGL\Txt's\Canais.txt";

            string[] values = File.ReadAllLines(filePath);
            canais = values;
        }
        public static void RealocLabel(int qtdGraf, float sizeLabelY)
        {
            yCanais = new float[qtdGraf];

            float loc = GlobVar.sizePainelExams.Y/ qtdGraf;
            float aux = loc / 2;
            for (int i = 0; i < yCanais.Length; i++)
            {
                yCanais[i] = aux;
                aux += loc;
            }
            for(int i  = 0;i < yCanais.Length;i++)
            {
                yCanais[i] -= (sizeLabelY / 2);
            }
        }
        public static void RealocButton(int qtdGraf,float sizeButtonY)
        {
            yButtons = new float[qtdGraf * 2];

            float loc = GlobVar.sizePainelExams.Y/ qtdGraf;
            float aux = loc /2;
            for(int i = 0;i < yButtons.Length; i += 2)
            {

                yButtons[i] = aux - (sizeButtonY - 2);
                yButtons[i + 1] = (aux + 2);
                aux += loc;

            }
        }
        public static void quantidadeGraf(int qtdGraf)
        {
            switch (qtdGraf)
            {
                case 1:
                    Tela_Plotagem.Canula.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusCanula.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusCanula.Location = new System.Drawing.Point(120, (int)yButtons[1]);
                    break;
                case 2:
                    Tela_Plotagem.Canula.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusCanula.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusCanula.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.Fluxo.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[3]);
                    break;
                case 3:
                    Tela_Plotagem.Canula.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusCanula.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusCanula.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.Fluxo.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.Abdomen.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusAbdomen.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusAbdomen.Location = new System.Drawing.Point(120, (int)yButtons[5]);
                    break;
                case 4:

                    Tela_Plotagem.Canula.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusCanula.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusCanula.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.Fluxo.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.Abdomen.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusAbdomen.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusAbdomen.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.Ronco.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusRonco.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusRonco.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    break;
                case 5:
                    Tela_Plotagem.Canula.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusCanula.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusCanula.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.Fluxo.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusFluxo.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.Abdomen.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusAbdomen.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusAbdomen.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.Ronco.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusRonco.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusRonco.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.SaturacaoO2.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusSatu.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusSatu.Location = new System.Drawing.Point(120, (int)yButtons[9]);
                    break;
                case 6:

                    break;
                case 7:

                    break;
                case 8:

                    break;
                case 9:

                    break;
                case 10:

                    break;
                case 11:

                    break;
                case 12:

                    break;
                case 13:

                    break;
                case 14:

                    break;
                case 15:

                    break;
                case 16:

                    break;
                case 17:

                    break;
                case 18:

                    break;
                case 19:

                    break;
                case 20:

                    break;
                case 21:

                    break;
                case 22:

                    break;
                case 23:

                    break;
                default: 
                    break;

            }
        }
    }
}
