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
                int TipoCanal = Convert.ToInt16(CodTipoCanal.Rows[0]["CodTipo"]);

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
            for (int i = 1; i <= 30; i++) //Reloca os panel para fora do form fazendo com eles desaparecam da tela
            {
                FieldInfo field = typeof(Tela_Plotagem).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                if (field != null)
                {
                    Panel panel = (Panel)field.GetValue(this);

                    if (panel != null)
                    {
                        panel.Location = new System.Drawing.Point(-500, 26);
                        panel.Size = new System.Drawing.Size(pnSizeX, (int)pnSize);
                    }
                }
            }

            int j = 0;
            for (int i = 1; i <= qtdGraf; i++) //traz os painel para o form de acordo com a quantidade de graficos plotados
            {
                FieldInfo labelName = typeof(Tela_Plotagem).GetField($"label{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo field = typeof(Tela_Plotagem).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo scale = typeof(Tela_Plotagem).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);

                if (field != null )
                {
                    Panel panel = (Panel)field.GetValue(this);
                    Label label = (Label)labelName.GetValue(this);
                    Label scala = (Label)scale.GetValue(this);

                    if (panel != null || label != null)
                    {
                        //Faz a verificacao no banco de dados para saber quantos %aquele panel tem, deacordo com o a diferenca dele
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

                        var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                                                .Where(row => row.Field<int>("CodCanal") == tag).CopyToDataTable();
                        int TipoCanal = Convert.ToInt16(CodTipoCanal.Rows[0]["CodTipo"]);
                        if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                                || TipoCanal == 15 || TipoCanal == 30)
                        {
                            panel.Controls.Remove(scala);

                            Point maxLoc = new Point(0, 0);
                            Point minLoc = new Point(0, 0);

                            maxLoc.X = panel.Width - 2;
                            maxLoc.Y = 1;

                            minLoc.X = panel.Width - 2;
                            minLoc.Y = panel.Height - 15;

                            Label maxima = new Label();
                            maxima.Name = "Maxima";
                            maxima.Location = new System.Drawing.Point(maxLoc.X, maxLoc.Y);   
                            maxima.Text = $"{(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["LimiteSuperior"])}";
                            maxima.Font = new Font(maxima.Font.FontFamily, 6, maxima.Font.Style);
                            maxima.Tag = "max";
                            maxima.Hide();

                            Label minima = new Label();
                            minima.Name = "Minima";
                            minima.Location = new System.Drawing.Point(minLoc.X, minLoc.Y);
                            minima.Text = $"{(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["LimiteInferior"])}";
                            minima.Font = new Font(minima.Font.FontFamily, 6, minima.Font.Style);
                            minima.Tag = "min";
                            minima.Hide();

                            panel.Controls.Add(maxima);
                            panel.Controls.Add(minima);
                        }
                        if (TipoCanal == 12)
                        {
                            // Remove o label existente que exibia todas as setas juntas
                            panel.Controls.Remove(scala);

                            // Lista dos caracteres correspondentes às setas na fonte Wingdings 3
                            char[] setasChars = { 'ã', 'á', 'â', 'ä' }; // Cima, Direita, Esquerda, Baixo

                            // Define a fonte Wingdings 3
                            Font fonteSetas = new Font("Wingdings 3", 6, System.Drawing.FontStyle.Regular);

                            // Define as localizações iniciais e espaçamento entre as setas
                            int espacoEntreSetas = 10; // Espaçamento vertical entre os labels
                            int larguraLabel = 20; // Largura padrão dos labels
                            int alturaLabel = 10; // Altura padrão dos labels

                            // Posição inicial para o primeiro label
                            int posicaoX = panel.Width - larguraLabel - 5; // Ajusta para alinhar à direita do painel
                            int posicaoY = 0;

                            // Criação dos labels das setas
                            for (int p = 0; p < setasChars.Length; p++)
                            {
                                Label setaLabel = new Label
                                {
                                    Name = $"seta{p + 1}",
                                    Text = setasChars[p].ToString(),
                                    Font = fonteSetas,
                                    //Size = new System.Drawing.Size(larguraLabel, alturaLabel),
                                    Location = new Point(posicaoX, posicaoY),
                                    Tag = $"setas"
                                };

                                // Adiciona o label ao painel
                                panel.Controls.Add(setaLabel);

                                // Atualiza a posição Y para o próximo label
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
            
            //Canal = GlobVar.nomeCanais[GlobVar.codCanal.IndexOf(Convert.ToInt16(GlobVar.tbl_MontagemSelecionada.Rows[YAdjusted]["CodCanal1"]))];
            Canal = "Teste";
            return Canal;
        }
    }
}
