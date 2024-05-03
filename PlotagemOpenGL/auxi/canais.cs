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
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(-500, 27);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(-500, 39);
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(-500, 26);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(-500, 53);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(-500, 65);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(-500, 52);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(-500, 79);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(-500, 91);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(-500, 78);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(-500, 105);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(-500, 117);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(-500, 104);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(-500, 131);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(-500, 143);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(-500, 130);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(-500, 157);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(-500, 169);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(-500, 156);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);



                    break;
                case 2:
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(-500, 53);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(-500, 65);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(-500, 52);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(-500, 79);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(-500, 91);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(-500, 78);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(-500, 105);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(-500, 117);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(-500, 104);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(-500, 131);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(-500, 143);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(-500, 130);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(-500, 157);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(-500, 169);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(-500, 156);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 3:
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(-500, 79);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(-500, 91);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(-500, 78);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(-500, 105);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(-500, 117);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(-500, 104);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(-500, 131);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(-500, 143);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(-500, 130);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(-500, 157);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(-500, 169);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(-500, 156);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 4:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(-500, 105);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(-500, 117);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(-500, 104);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(-500, 131);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(-500, 143);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(-500, 130);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(-500, 157);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(-500, 169);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(-500, 156);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);

                    break;
                case 5:
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(-500, 131);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(-500, 143);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(-500, 130);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(-500, 157);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(-500, 169);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(-500, 156);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 6:
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(-500, 157);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(-500, 169);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(-500, 156);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 7:
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(-500, 183);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(-500, 195);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(-500, 182);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 8:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(-500, 209);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(-500, 221);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(-500, 208);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 9:
                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(-500, 235);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(-500, 247);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(-500, 234);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);

                    break;
                case 10:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(-500, 261);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(-500, 273);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(-500, 260);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 11:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(-500, 287);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(-500, 299);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(-500, 286);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 12:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(-500, 313);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(-500, 325);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(-500, 312);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 13:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(-500, 339);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(-500, 351);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(-500, 338);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 14:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);


                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(-500, 365);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(-500, 377);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(-500, 364);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 15:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(-500, 391);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(-500, 403);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(-500, 390);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 16:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(-500, 417);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(-500, 429);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(-500, 416);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 17:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(-500, 443);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(-500, 455);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(-500, 442);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 18:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(3, (int)yCanais[17]);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(120, (int)yButtons[35]);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(120, (int)yButtons[34]);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(-500, 469);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(-500, 481);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(-500, 468);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 19:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(3, (int)yCanais[17]);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(120, (int)yButtons[35]);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(120, (int)yButtons[34]);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(3, (int)yCanais[18]);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(120, (int)yButtons[37]);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(120, (int)yButtons[36]);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(-500, 495);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(-500, 507);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(-500, 507);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 20:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(3, (int)yCanais[17]);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(120, (int)yButtons[35]);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(120, (int)yButtons[34]);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(3, (int)yCanais[18]);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(120, (int)yButtons[37]);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(120, (int)yButtons[36]);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(3, (int)yCanais[19]);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(120, (int)yButtons[39]);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(120, (int)yButtons[38]);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(-500, 521);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(-500, 533);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(-500, 520);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 21:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(3, (int)yCanais[17]);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(120, (int)yButtons[35]);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(120, (int)yButtons[34]);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(3, (int)yCanais[18]);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(120, (int)yButtons[37]);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(120, (int)yButtons[36]);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(3, (int)yCanais[19]);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(120, (int)yButtons[39]);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(120, (int)yButtons[38]);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(3, (int)yCanais[20]);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(120, (int)yButtons[41]);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(120, (int)yButtons[40]);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(-500, 547);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(-500, 559);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(-500, 546);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 22:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(3, (int)yCanais[17]);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(120, (int)yButtons[35]);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(120, (int)yButtons[34]);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(3, (int)yCanais[18]);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(120, (int)yButtons[37]);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(120, (int)yButtons[36]);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(3, (int)yCanais[19]);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(120, (int)yButtons[39]);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(120, (int)yButtons[38]);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(3, (int)yCanais[20]);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(120, (int)yButtons[41]);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(120, (int)yButtons[40]);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(3, (int)yCanais[21]);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(120, (int)yButtons[43]);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(120, (int)yButtons[42]);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(-500, 573);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(-500, 585);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(-500, 572);
                    break;
                case 23:

                    Tela_Plotagem.label1.Location = new System.Drawing.Point(3, (int)yCanais[0]);
                    Tela_Plotagem.plusLb1.Location = new System.Drawing.Point(120, (int)yButtons[0]);
                    Tela_Plotagem.minusLb1.Location = new System.Drawing.Point(120, (int)yButtons[1]);

                    Tela_Plotagem.label2.Location = new System.Drawing.Point(3, (int)(yCanais[1]));
                    Tela_Plotagem.plusLb2.Location = new System.Drawing.Point(120, (int)yButtons[2]);
                    Tela_Plotagem.minusLb2.Location = new System.Drawing.Point(120, (int)yButtons[3]);

                    Tela_Plotagem.label3.Location = new System.Drawing.Point(3, (int)yCanais[2]);
                    Tela_Plotagem.plusLb3.Location = new System.Drawing.Point(120, (int)yButtons[4]);
                    Tela_Plotagem.minusLb3.Location = new System.Drawing.Point(120, (int)yButtons[5]);

                    Tela_Plotagem.label4.Location = new System.Drawing.Point(3, (int)yCanais[3]);
                    Tela_Plotagem.plusLb4.Location = new System.Drawing.Point(120, (int)yButtons[6]);
                    Tela_Plotagem.minusLb4.Location = new System.Drawing.Point(120, (int)yButtons[7]);

                    Tela_Plotagem.label5.Location = new System.Drawing.Point(3, (int)yCanais[4]);
                    Tela_Plotagem.plusLb5.Location = new System.Drawing.Point(120, (int)yButtons[8]);
                    Tela_Plotagem.minusLb5.Location = new System.Drawing.Point(120, (int)yButtons[9]);

                    Tela_Plotagem.label6.Location = new System.Drawing.Point(3, (int)yCanais[5]);
                    Tela_Plotagem.minusLb6.Location = new System.Drawing.Point(120, (int)yButtons[11]);
                    Tela_Plotagem.plusLb6.Location = new System.Drawing.Point(120, (int)yButtons[10]);

                    Tela_Plotagem.label7.Location = new System.Drawing.Point(3, (int)yCanais[6]);
                    Tela_Plotagem.minusLb7.Location = new System.Drawing.Point(120, (int)yButtons[13]);
                    Tela_Plotagem.plusLb7.Location = new System.Drawing.Point(120, (int)yButtons[12]);

                    Tela_Plotagem.label8.Location = new System.Drawing.Point(3, (int)yCanais[7]);
                    Tela_Plotagem.minusLb8.Location = new System.Drawing.Point(120, (int)yButtons[15]);
                    Tela_Plotagem.plusLb8.Location = new System.Drawing.Point(120, (int)yButtons[14]);

                    Tela_Plotagem.label9.Location = new System.Drawing.Point(3, (int)yCanais[8]);
                    Tela_Plotagem.minusLb9.Location = new System.Drawing.Point(120, (int)yButtons[17]);
                    Tela_Plotagem.plusLb9.Location = new System.Drawing.Point(120, (int)yButtons[16]);

                    Tela_Plotagem.label10.Location = new System.Drawing.Point(3, (int)yCanais[9]);
                    Tela_Plotagem.minusLb10.Location = new System.Drawing.Point(120, (int)yButtons[19]);
                    Tela_Plotagem.plusLb10.Location = new System.Drawing.Point(120, (int)yButtons[18]);

                    Tela_Plotagem.label11.Location = new System.Drawing.Point(3, (int)yCanais[10]);
                    Tela_Plotagem.minusLb11.Location = new System.Drawing.Point(120, (int)yButtons[21]);
                    Tela_Plotagem.plusLb11.Location = new System.Drawing.Point(120, (int)yButtons[20]);

                    Tela_Plotagem.label12.Location = new System.Drawing.Point(3, (int)yCanais[11]);
                    Tela_Plotagem.minusLb12.Location = new System.Drawing.Point(120, (int)yButtons[23]);
                    Tela_Plotagem.plusLb12.Location = new System.Drawing.Point(120, (int)yButtons[22]);

                    Tela_Plotagem.label13.Location = new System.Drawing.Point(3, (int)yCanais[12]);
                    Tela_Plotagem.minusLb13.Location = new System.Drawing.Point(120, (int)yButtons[25]);
                    Tela_Plotagem.plusLb13.Location = new System.Drawing.Point(120, (int)yButtons[24]);

                    Tela_Plotagem.label14.Location = new System.Drawing.Point(3, (int)yCanais[13]);
                    Tela_Plotagem.minusLb14.Location = new System.Drawing.Point(120, (int)yButtons[27]);
                    Tela_Plotagem.plusLb14.Location = new System.Drawing.Point(120, (int)yButtons[26]);

                    Tela_Plotagem.label15.Location = new System.Drawing.Point(3, (int)yCanais[14]);
                    Tela_Plotagem.minusLb15.Location = new System.Drawing.Point(120, (int)yButtons[29]);
                    Tela_Plotagem.plusLb15.Location = new System.Drawing.Point(120, (int)yButtons[28]);

                    Tela_Plotagem.label16.Location = new System.Drawing.Point(3, (int)yCanais[15]);
                    Tela_Plotagem.minusLb16.Location = new System.Drawing.Point(120, (int)yButtons[31]);
                    Tela_Plotagem.plusLb16.Location = new System.Drawing.Point(120, (int)yButtons[30]);

                    Tela_Plotagem.label17.Location = new System.Drawing.Point(3, (int)yCanais[16]);
                    Tela_Plotagem.minusLb17.Location = new System.Drawing.Point(120, (int)yButtons[33]);
                    Tela_Plotagem.plusLb17.Location = new System.Drawing.Point(120, (int)yButtons[32]);

                    Tela_Plotagem.label18.Location = new System.Drawing.Point(3, (int)yCanais[17]);
                    Tela_Plotagem.minusLb18.Location = new System.Drawing.Point(120, (int)yButtons[35]);
                    Tela_Plotagem.plusLb18.Location = new System.Drawing.Point(120, (int)yButtons[34]);

                    Tela_Plotagem.label19.Location = new System.Drawing.Point(3, (int)yCanais[18]);
                    Tela_Plotagem.minusLb19.Location = new System.Drawing.Point(120, (int)yButtons[37]);
                    Tela_Plotagem.plusLb19.Location = new System.Drawing.Point(120, (int)yButtons[36]);

                    Tela_Plotagem.label20.Location = new System.Drawing.Point(3, (int)yCanais[19]);
                    Tela_Plotagem.minusLb20.Location = new System.Drawing.Point(120, (int)yButtons[39]);
                    Tela_Plotagem.plusLb20.Location = new System.Drawing.Point(120, (int)yButtons[38]);

                    Tela_Plotagem.label21.Location = new System.Drawing.Point(3, (int)yCanais[20]);
                    Tela_Plotagem.minusLb21.Location = new System.Drawing.Point(120, (int)yButtons[41]);
                    Tela_Plotagem.plusLb21.Location = new System.Drawing.Point(120, (int)yButtons[40]);

                    Tela_Plotagem.label22.Location = new System.Drawing.Point(3, (int)yCanais[21]);
                    Tela_Plotagem.minusLb22.Location = new System.Drawing.Point(120, (int)yButtons[43]);
                    Tela_Plotagem.plusLb22.Location = new System.Drawing.Point(120, (int)yButtons[42]);

                    Tela_Plotagem.label23.Location = new System.Drawing.Point(3, (int)yCanais[22]);
                    Tela_Plotagem.minusLb23.Location = new System.Drawing.Point(120, (int)yButtons[45]);
                    Tela_Plotagem.plusLb23.Location = new System.Drawing.Point(120, (int)yButtons[44]);
                    break;
                default: 
                    break;

            }
        }
    }
}
