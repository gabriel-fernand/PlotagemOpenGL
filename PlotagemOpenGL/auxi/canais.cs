using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Documents;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using Point = System.Drawing.Point;
using Accord.Math;
using System.Linq;
using PlotagemOpenGL.auxi.auxPlotagem;
using System.Windows;
using PlotagemOpenGL.auxi.FormsAuxi;
using System.Data;

namespace PlotagemOpenGL.auxi
{
    public class Canais
    {
        public static float[] yCanais;
        public static string[] canais;
        public int qtdGraf;
        public static float pnSize;
        public static float[] pnSizes;
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
            int pnSizeY = (int)pnSize / 2;
            labelLocY = pnSizeY - sizeLb;

        }
        public void RealocButton()
        {
            int sizePn = (int)pnSize / 2;
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
            for (int i = 1; i <= 23; i++) //Relaloca os Label's os butoes dentro do panel
            {
                FieldInfo field = typeof(Tela_Plotagem).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo labelName = typeof(Tela_Plotagem).GetField($"label{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo plus = typeof(Tela_Plotagem).GetField($"plusLb{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo minus = typeof(Tela_Plotagem).GetField($"minusLb{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo scale = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                if (field != null)
                { //(Panel)field.GetValue(this);
                    Label label = (Label)labelName.GetValue(this);
                    Button btnPlus = (Button)plus.GetValue(this);
                    Button btnMinus = (Button)minus.GetValue(this);
                    Label scala = (Label)scale.GetValue(this);
                    if (label != null || btnPlus != null || btnMinus != null || scala != null)
                    {
                        label.Location = new System.Drawing.Point(3, labelLocY);
                        btnPlus.Location = new System.Drawing.Point((int)GlobVar.locBut.X, plusLoc);
                        btnMinus.Location = new System.Drawing.Point((int)GlobVar.locBut.X, minusLoc);
                        scala.Tag = "scala";
                        scala.Location = new System.Drawing.Point((int)GlobVar.locScale.X, labelLocY);
                        btnPlus.Tag = "+";
                        btnMinus.Tag = "-";
                    }
                }
            }
            foreach(Panel pn in Tela_Plotagem.painelExames.Controls)
            {
                /*var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                            .Where(row => row.Field<int>("CodCanal") == Convert.ToInt32(pn.Tag)).CopyToDataTable();
                int TipoCanal = Convert.ToInt32(CodTipoCanal.Rows[0]["CodTipo"]);

                if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                    || TipoCanal == 15 || TipoCanal == 30 || TipoCanal == 12)
                {

                }*/

                foreach(Label lb in pn.Controls.OfType<Label>())
                {
                    if(lb.Tag != null){
                        if (lb.Tag.Equals("min"))
                        {

                            Point minLoc = new Point(0, 0);

                            minLoc.X = (int)GlobVar.locScale.X;
                            minLoc.Y = pn.Height - 15;

                            lb.Location = new System.Drawing.Point(minLoc.X, minLoc.Y);
                        }
                        else if (lb.Tag.Equals("max"))
                        {
                            Point maxLoc = new Point(0, 0);

                            maxLoc.X = (int)GlobVar.locScale.X;
                            maxLoc.Y = 1;

                            lb.Location = new System.Drawing.Point(maxLoc.X, maxLoc.Y);

                        }
                    }
                }
            }
        }
        public void quantidadeGraf(int qtdGraf)
        {
            pnSizes = new float[qtdGraf + 1];
            float auzpnSize = 0;
            int pnSizeX = (int)GlobVar.sizePainelExams.X;

            // Esconder todos os painéis inicialmente, movendo-os para fora da tela
            for (int i = 1; i <= 30; i++)
            {
                FieldInfo field = typeof(Tela_Plotagem).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                if (field != null)
                {
                    Panel panel = (Panel)field.GetValue(this);
                    if (panel != null)
                    {
                        panel.Location = new System.Drawing.Point(-500, 26);
                        panel.Size = new System.Drawing.Size(pnSizeX, (int)pnSize);
                        panel.Tag = -1;
                    }
                }
            }

            int j = 0;
            for (int i = 1; i <= qtdGraf; i++)
            {
                FieldInfo labelName = typeof(Tela_Plotagem).GetField($"label{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo field = typeof(Tela_Plotagem).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo scale = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);

                // Verificar se a linha atual do DataTable existe
                if (j >= GlobVar.tbl_MontagemSelecionada.Rows.Count)
                {
                    // Sai do loop se não houver mais linhas no DataTable
                    break;
                }

                // Procurar pelo valor CodCanal1 no vetor GlobVar.codCanal
                int canalIndex = GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[j]["CodCanal1"]));
                if (Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[j]["CodCanal1"]) == 100 || Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[j]["CodCanal1"]) == 101 || Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[j]["CodCanal1"]) == 102)
                {

                }
                // Se não encontrar o canal, pular para o próximo sem alterar os painéis
                else if (canalIndex == -1)
                {
                    j++; // Avança para a próxima linha
                    continue;
                }

                if (field != null)
                {
                    Panel panel = (Panel)field.GetValue(this);
                    Label label = (Label)labelName.GetValue(this);
                    Label scala = (Label)scale.GetValue(this);

                    if (panel != null || label != null)
                    {
                        panel.Controls.Add(label);
                        panel.Controls.Add(scala);

                        // Ajustar o tamanho e localização do painel
                        float ySizeAux = (float)(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["Altura"]) / 100;
                        pnSize = (int)GlobVar.sizePainelExams.Y * ySizeAux;
                        auzpnSize += pnSize;
                        pnSizes[i] = auzpnSize;
                        int tag = (int)(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["CodCanal1"]);
                        panel.Location = new System.Drawing.Point(0, (int)yCanais[j]);
                        panel.Size = new System.Drawing.Size(pnSizeX, (int)pnSize);
                        panel.Tag = tag;
                        label.Text = GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["Legenda"].ToString();
                        label.Tag = panel.Tag;
                        scala.Tag = "scala";

                        // Verificar o tipo de canal para ajustar o comportamento
                        var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                            .Where(row => row.Field<int>("CodCanal") == tag).CopyToDataTable();
                        int TipoCanal = Convert.ToInt32(CodTipoCanal.Rows[0]["CodTipo"]);

                        if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31 || TipoCanal == 30 || tag == 65)
                        {
                            //scala.Location = new Point(-500, 0);
                            panel.Controls.Remove(scala);

                            // Adicionar labels de máximas e mínimas
                            Label maxima = new Label();
                            maxima.Name = "Maxima";
                            maxima.Text = $"{(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["LimiteSuperior"])}";
                            maxima.Font = new Font(maxima.Font.FontFamily, 6);
                            maxima.Hide();

                            Label minima = new Label();
                            minima.Name = "Minima";
                            minima.Text = $"{(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["LimiteInferior"])}";
                            minima.Font = new Font(minima.Font.FontFamily, 6);
                            minima.Hide();

                            panel.Controls.Add(maxima);
                            panel.Controls.Add(minima);
                        }

                        if (TipoCanal == 12)
                        {
                            scala.Location = new Point(-500, 0);
                            panel.Controls.Remove(scala);

                            // Adicionar setas com a fonte Wingdings 3
                            char[] setasChars = { 'ã', 'á', 'â', 'ä' }; // Cima, Direita, Esquerda, Baixo
                            Font fonteSetas = new Font("Wingdings 3", 6);
                            int espacoEntreSetas = 10;
                            int larguraLabel = 20;
                            int alturaLabel = 10;
                            int posicaoX = panel.Width - larguraLabel - 5;
                            int posicaoY = 0;

                            for (int p = 0; p < setasChars.Length; p++)
                            {
                                Label setaLabel = new Label
                                {
                                    Name = $"seta{p + 1}",
                                    Text = setasChars[p].ToString(),
                                    Font = fonteSetas,
                                    Location = new Point(posicaoX, posicaoY),
                                    Tag = $"setas"
                                };
                                setaLabel.BringToFront();
                                setaLabel.Hide();
                                panel.Controls.Add(setaLabel);

                                posicaoY += alturaLabel + espacoEntreSetas;
                            }
                        }
                        j++;
                    }
                }
            }
        }
        public static string UpMouseLoc(float startY, float[] desenhoLoc)
        {
            string Canal;
            float[] desenhoLocRev = new float[desenhoLoc.Length];
            desenhoLocRev = desenhoLoc.Reverse().ToArray();
            int YAdjusted = Plotagem.EncontrarValorMaisProximo(desenhoLocRev, startY);
            
            //Canal = GlobVar.nomeCanais[GlobVar.codCanal.IndexOf(Convert.ToInt32(GlobVar.tbl_MontagemSelecionada.Rows[YAdjusted]["CodCanal1"]))];
            Canal = "Teste";
            return Canal;
        }

        public static int Get_BoaNoite()
        {
            int BoaNoite = 0;

            var Row_BoaNoite = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 18).FirstOrDefault();
            if(Row_BoaNoite != null)
            {
                BoaNoite = Convert.ToInt32(Row_BoaNoite["NumPag"]);
            }

            return BoaNoite;
        }
        public static int Get_BomDia()
        {
            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().OrderByDescending(row => row.Field<int>("NumPag")).CopyToDataTable();
            int BomDia = Convert.ToInt32(lastRow.Rows[0]["NumPag"]);

            var Row_BomDia = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 19).FirstOrDefault();
            if (Row_BomDia != null)
            {
                BomDia = Convert.ToInt32(Row_BomDia["NumPag"]);
            }

            return BomDia;
        }
        public static int Get_InicioCPAP()
        {
            var lastRow = GlobVar.tbl_Paginas.AsEnumerable().OrderByDescending(row => row.Field<int>("NumPag")).CopyToDataTable();
            int InicioCPAP = Convert.ToInt32(lastRow.Rows[0]["NumPag"]);

            var Row_InicioCPAP = GlobVar.eventos.AsEnumerable().Where(row => row.Field<int>("CodEvento") == 50).FirstOrDefault();
            if (Row_InicioCPAP != null)
            {
                InicioCPAP = Convert.ToInt32(Row_InicioCPAP["NumPag"]);
            }

            return InicioCPAP;
        }

        internal static int F_Get1ValorDoCanalFC(int codCanal, int pagina)
        {
            int valorCanalFC = 0;

            // Verifica se o código de referência existe
            int indexCod = GlobVar.codCanal.IndexOf(codCanal);

            // Pega os índices de início e fim uma vez
            int startCol = GlobVar.ponteiroI[indexCod];
            int endCol = GlobVar.ponteiroF[indexCod];

            valorCanalFC = (int)GlobVar.matrizCompleta[pagina, startCol];

            return valorCanalFC;
        }
        public static int F_Get1ValorDoCanalSAO2(int pagina)
        {
            int SA02 = 0;

            SA02 = Convert.ToInt32(GlobVar.tbl_Paginas.Rows[pagina]["SatBasal"]);
            return SA02;
        }


    }
}
