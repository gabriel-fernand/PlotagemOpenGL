using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Text;
using System.Windows.Documents;

namespace PlotagemOpenGL.auxi
{
    public class Canais
    {
        public static float[] yCanais;
        public static string[] canais;
        public int qtdGraf;
        public static int pnSize;
        public static int labelLocY;
        public static int plusLoc;
        public static int minusLoc;
        public Canais(int qtdGraf)
        {
            this.qtdGraf = qtdGraf;
            pnSize = (int)GlobVar.sizePainelExams.Y / qtdGraf;
        }
        public void LerCanais()
        {
            string filePath = @"C:\Users\dev_i\source\repos\PlotagemOpenGL\PlotagemOpenGL\Txt's\Canais.txt";

            string[] values = File.ReadAllLines(filePath);
            canais = values;
        }
        public void PainelLb_Resize()
        {
            int sizeLb = (int)GlobVar.sizeLabelExams.Y / 2;
            int pnSizeY = pnSize / 2;
            labelLocY = pnSizeY - sizeLb;

        }
        public void RealocButton()
        {
            int sizePn = pnSize / 2;
            int sizeBut = (int)GlobVar.sizeButtons.Y;

            plusLoc = sizePn - sizeBut;
            minusLoc = sizePn + 1;
        }

        public void RealocPanel(int qtdGraf)
        {
            if (qtdGraf == 1)
            {
                yCanais = new float[qtdGraf];
                yCanais[0] = 0;
            }
            else
            {
                yCanais = new float[qtdGraf];
                yCanais[0] = 0;
                float loc = GlobVar.sizePainelExams.Y / qtdGraf;
                float aux = loc;
                for (int i = 1; i < yCanais.Length; i++)
                {
                    yCanais[i] = aux;
                    aux += loc;
                }
                //for (int i = 0; i < yCanais.Length; i++)
                //{
                //   yCanais[i] -= (sizeLabelY / 2);
                //}
            }
        }
        public void reloc()
        {
            Tela_Plotagem.label1.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb1.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb1.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb1.Location = new System.Drawing.Point(87, labelLocY);


            Tela_Plotagem.label2.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb2.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb2.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb2.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label3.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb3.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb3.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb3.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label4.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb4.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb4.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb4.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label5.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb5.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb5.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb5.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label6.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb6.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb6.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb6.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label7.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb7.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb7.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb7.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label8.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb8.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb8.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb8.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label9.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb9.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb9.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb9.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label10.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb10.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb10.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb10.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label11.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb11.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb11.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb11.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label12.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb12.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb12.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb12.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label13.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb13.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb13.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb13.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label14.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb14.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb14.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb14.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label15.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb15.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb15.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb15.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label16.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb16.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb16.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb16.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label17.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb17.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb17.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb17.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label18.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb18.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb18.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb18.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label19.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb19.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb19.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb19.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label20.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb20.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb20.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb20.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label21.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb21.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb21.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb21.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label22.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb22.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb22.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb22.Location = new System.Drawing.Point(87, labelLocY);

            Tela_Plotagem.label23.Location = new System.Drawing.Point(3, labelLocY);
            Tela_Plotagem.plusLb23.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
            Tela_Plotagem.minusLb23.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
            Tela_Plotagem.scalaLb23.Location = new System.Drawing.Point(87, labelLocY);

        }
        public void quantidadeGraf(int qtdGraf)
        {
            
            int pnSizeX = (int)GlobVar.sizePainelExams.X;
            switch (qtdGraf)
            {
                case 1:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(-500, 26);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, 25);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(-500, 52);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, 25);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(-500, 78);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, 25);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(-500, 104);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(-500, 130);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(-500, 156);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 182);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 2:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(-500, 52);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(-500, 78);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(-500, 104);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(-500, 130);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(-500, 156);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 182);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 3:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(-500, 78);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(-500, 104);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(-500, 130);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(-500, 156);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 182);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 4:

                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(-500, 104);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(-500, 130);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(-500, 156);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 182);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 5:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(-500, 130);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(-500, 156);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 182);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 6:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(-500, 156);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 182);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 7:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 208);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 8:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 234);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 9:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 260);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 10:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 286);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 11:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 312);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 12:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 338);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 13:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 363);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 14:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 390);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 15:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 416);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 16:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(-500, 0);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);
                    break;
                case 17:
                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(-500, 442);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(172, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 18:

                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(0, (int)yCanais[17]);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(-500, 468);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 19:


                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(0, (int)yCanais[17]);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(0, (int)yCanais[18]);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(-500, 494);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 20:



                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(0, (int)yCanais[17]);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(0, (int)yCanais[18]);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(0, (int)yCanais[19]);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(-500, 520);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 21:




                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(0, (int)yCanais[17]);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(0, (int)yCanais[18]);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(0, (int)yCanais[19]);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(0, (int)yCanais[20]);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(-500, 546);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(172, 25);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 22:




                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(0, (int)yCanais[17]);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(0, (int)yCanais[18]);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(0, (int)yCanais[19]);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(0, (int)yCanais[20]);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(0, (int)yCanais[21]);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(-500, 572);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(172, 25);

                    break;
                case 23:

                    Tela_Plotagem.panel1.Location = new System.Drawing.Point(0, (int)yCanais[0]);
                    Tela_Plotagem.panel1.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel2.Location = new System.Drawing.Point(0, (int)yCanais[1]);
                    Tela_Plotagem.panel2.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel3.Location = new System.Drawing.Point(0, (int)yCanais[2]);
                    Tela_Plotagem.panel3.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel4.Location = new System.Drawing.Point(0, (int)yCanais[3]);
                    Tela_Plotagem.panel4.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel5.Location = new System.Drawing.Point(0, (int)yCanais[4]);
                    Tela_Plotagem.panel5.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel6.Location = new System.Drawing.Point(0, (int)yCanais[5]);
                    Tela_Plotagem.panel6.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel7.Location = new System.Drawing.Point(0, (int)yCanais[6]);
                    Tela_Plotagem.panel7.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel8.Location = new System.Drawing.Point(0, (int)yCanais[7]);
                    Tela_Plotagem.panel8.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel9.Location = new System.Drawing.Point(0, (int)yCanais[8]);
                    Tela_Plotagem.panel9.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel10.Location = new System.Drawing.Point(0, (int)yCanais[9]);
                    Tela_Plotagem.panel10.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel11.Location = new System.Drawing.Point(0, (int)yCanais[10]);
                    Tela_Plotagem.panel11.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel12.Location = new System.Drawing.Point(0, (int)yCanais[11]);
                    Tela_Plotagem.panel12.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel13.Location = new System.Drawing.Point(0, (int)yCanais[12]);
                    Tela_Plotagem.panel13.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel14.Location = new System.Drawing.Point(0, (int)yCanais[13]);
                    Tela_Plotagem.panel14.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel15.Location = new System.Drawing.Point(0, (int)yCanais[14]);
                    Tela_Plotagem.panel15.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel16.Location = new System.Drawing.Point(0, (int)yCanais[15]);
                    Tela_Plotagem.panel16.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel17.Location = new System.Drawing.Point(0, (int)yCanais[16]);
                    Tela_Plotagem.panel17.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel18.Location = new System.Drawing.Point(0, (int)yCanais[17]);
                    Tela_Plotagem.panel18.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel19.Location = new System.Drawing.Point(0, (int)yCanais[18]);
                    Tela_Plotagem.panel19.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel20.Location = new System.Drawing.Point(0, (int)yCanais[19]);
                    Tela_Plotagem.panel20.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel21.Location = new System.Drawing.Point(0, (int)yCanais[20]);
                    Tela_Plotagem.panel21.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel22.Location = new System.Drawing.Point(0, (int)yCanais[21]);
                    Tela_Plotagem.panel22.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    Tela_Plotagem.panel23.Location = new System.Drawing.Point(0, (int)yCanais[22]);
                    Tela_Plotagem.panel23.Size = new System.Drawing.Size(pnSizeX, pnSize);

                    break;
                default:
                    break;

            }
            try
            {
            }
            catch
            {
                
            }
        }
    }
}
