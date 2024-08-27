using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Point = System.Drawing.Point;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using ADODB;
using OpenTK.Mathematics;
using System.Windows.Interop;
using System.Collections.Generic;
using PlotagemOpenGL.auxi.FormsAuxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using System.Windows.Media;


namespace PlotagemOpenGL
{
    public partial class backLog : Form
    {

        public static float[] yCanais;
        public static string[] canais;
        public int qtdGraf;
        public static float pnSize;
        public static float[] pnSizes;
        public static int labelLocY;
        public static int plusLoc;
        public static int minusLoc;
        private System.Drawing.Size formOriginalSize;
        private Rectangle gridView;
        private Rectangle bt2;
        private Rectangle pncan;

        private Rectangle btPlusLb1;
        private Rectangle btMinusLb1;
        private Rectangle btPlusLb2;
        private Rectangle btMinusLb2;
        private Rectangle btPlusLb3;
        private Rectangle btMinusLb3;
        private Rectangle btPlusLb4;
        private Rectangle btMinusLb4;
        private Rectangle btPlusLb5;
        private Rectangle btMinusLb5;
        private Rectangle btPlusLb6;
        private Rectangle btMinusLb6;
        private Rectangle btPlusLb7;
        private Rectangle btMinusLb7;
        private Rectangle btPlusLb8;
        private Rectangle btMinusLb8;
        private Rectangle btPlusLb9;
        private Rectangle btMinusLb9;
        private Rectangle btPlusLb10;
        private Rectangle btMinusLb10;
        private Rectangle btPlusLb11;
        private Rectangle btMinusLb11;
        private Rectangle btPlusLb12;
        private Rectangle btMinusLb12;
        private Rectangle btPlusLb13;
        private Rectangle btMinusLb13;
        private Rectangle btPlusLb14;
        private Rectangle btMinusLb14;
        private Rectangle btPlusLb15;
        private Rectangle btMinusLb15;
        private Rectangle btPlusLb16;
        private Rectangle btMinusLb16;
        private Rectangle btPlusLb17;
        private Rectangle btMinusLb17;
        private Rectangle btPlusLb18;
        private Rectangle btMinusLb18;
        private Rectangle btPlusLb19;
        private Rectangle btMinusLb19;
        private Rectangle btPlusLb20;
        private Rectangle btMinusLb20;
        private Rectangle btPlusLb21;
        private Rectangle btMinusLb21;
        private Rectangle btPlusLb22;
        private Rectangle btMinusLb22;
        private Rectangle btPlusLb23;
        private Rectangle btMinusLb23;

        private Rectangle lbScale1;
        private Rectangle lbScale2;
        private Rectangle lbScale3;
        private Rectangle lbScale4;
        private Rectangle lbScale5;
        private Rectangle lbScale6;
        private Rectangle lbScale7;
        private Rectangle lbScale8;
        private Rectangle lbScale9;
        private Rectangle lbScale10;
        private Rectangle lbScale11;
        private Rectangle lbScale12;
        private Rectangle lbScale13;
        private Rectangle lbScale14;
        private Rectangle lbScale15;
        private Rectangle lbScale16;
        private Rectangle lbScale17;
        private Rectangle lbScale18;
        private Rectangle lbScale19;
        private Rectangle lbScale20;
        private Rectangle lbScale21;
        private Rectangle lbScale22;
        private Rectangle lbScale23;

        private Rectangle pn1;
        private Rectangle pn2;
        private Rectangle pn3;
        private Rectangle pn4;
        private Rectangle pn5;
        private Rectangle pn6;
        private Rectangle pn7;
        private Rectangle pn8;
        private Rectangle pn9;
        private Rectangle pn10;
        private Rectangle pn11;
        private Rectangle pn12;
        private Rectangle pn13;
        private Rectangle pn14;
        private Rectangle pn15;
        private Rectangle pn16;
        private Rectangle pn17;
        private Rectangle pn18;
        private Rectangle pn19;
        private Rectangle pn20;
        private Rectangle pn21;
        private Rectangle pn22;
        private Rectangle pn23;

        public backLog()
        {
            InitializeComponent();
            LeitorDiretorio.LeituraDiretorio();

            LeituraBanco.BancoRead();
            LeituraBanco.AlteraTable();
            LeituraBanco.AjustaMontagem();

            Leitura.QuantidadeCanais();
            //Leitura.LeituraDat();
            LeituraBanco.AjustaCadEvent(); // Esta ajustando os valores das teclas rapida para -1 caso o valor seja null, pois estava atrapalhando quando era null

            LeituraEmMatrizTeste.LeituraDat();
            rectangleLoad();
            formOriginalSize = this.Size;
            this.Resize += Tela_Plotagem_Resiz;
            this.Resize += panelLb_Resiz;
            this.Resize += Painel_resiz;

            GlobVar.sizePainelExams.X = PanelCanais.Width;
            GlobVar.sizePainelExams.Y = PanelCanais.Height;

            GlobVar.sizeButtons.X = plusLb1.Width;
            GlobVar.sizeButtons.Y = plusLb1.Height;
            GlobVar.sizeLabelExams.X = label1.Width;
            GlobVar.sizeLabelExams.Y = label1.Height;
            GlobVar.sizePanelLb.X = panel1.Width;
            GlobVar.sizePanelLb.Y = panel1.Height;

            GlobVar.locBut.X = plusLb1.Location.X;
            GlobVar.locScale.X = scalaLb1.Location.X;
            GlobVar.maximaNumero = GlobVar.tmpEmTelaNumerico;

            RealocPanel(23);
            quantidadeGraf(23);
            RealocButton();
            PainelLb_Resize();
            reloc();

            // Faça o mesmo para os outros painéis, ou crie um método que itere sobre todos
            foreach (Control panel in PanelCanais.Controls)
            {
                if (panel is Panel)
                {
                    panel.MouseDown += Panel_MouseDown;
                    panel.MouseMove += Panel_MouseMove;
                    panel.MouseUp += Panel_MouseUp;
                }
            }
            timer3.Start();
        }
        private bool mouseIsDown = false;
        private const int borderWidth = 6; // Largura da borda sensível para redimensionamento
        private bool isResizing = false;
        private bool isResizingFromTop = false;
        private int originalHeight;
        private int originalTop;
        private Point mouseDownLocation;
        private int initialPanelY;
        private Panel movingPanel;
        private bool isMoving = false;
        private auxi.FormsAuxi.OverlayForm overlayForm;
        public bool isOnBottomBorder = false;
        public bool isOnTopBorder = false;

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;


            if ((isOnBottomBorder || isOnTopBorder) && e.Button == MouseButtons.Left)
            {
                // Inicia o redimensionamento
                isResizing = true;
                isResizingFromTop = isOnTopBorder;
                originalHeight = panel.Height;
                originalTop = panel.Top;
                movingPanel = panel;

                overlayForm = new OverlayForm();
                overlayForm.Bounds = PanelCanais.RectangleToScreen(PanelCanais.ClientRectangle);
                overlayForm.TempRect = new Rectangle(panel.Left, originalTop, panel.Width, originalHeight);

                mouseDownLocation = e.Location;

                overlayForm.Show();
            }
            else if (e.Button == MouseButtons.Left)
            {
                mouseIsDown = true;
                movingPanel = panel;
                mouseDownLocation = e.Location;
                initialPanelY = movingPanel.Top;

                overlayForm = new OverlayForm();
                overlayForm.Bounds = PanelCanais.RectangleToScreen(PanelCanais.ClientRectangle);
                overlayForm.TempRect = new Rectangle(movingPanel.Left, movingPanel.Top, movingPanel.Width, movingPanel.Height);
                overlayForm.Show();

                isMoving = true;
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;

            if(!mouseIsDown || !isResizing)
            {
                // Verifica se o mouse está na borda inferior ou superior do painel ao clicar
                isOnBottomBorder = Math.Abs(e.Y - panel.Height) <= borderWidth;
                isOnTopBorder = Math.Abs(e.Y) <= borderWidth;

                if(isOnBottomBorder || isOnTopBorder)
                {
                    this.Cursor = Cursors.SizeNS;
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
            if (isResizing && overlayForm != null)
            {
                int deltaY = e.Y - mouseDownLocation.Y;

                if (isResizingFromTop)
                {
                    int newTop = originalTop + deltaY;
                    int newHeight = originalHeight - deltaY;

                    newTop = Math.Max(0, Math.Min(newTop, PanelCanais.Height - newHeight));
                    newHeight = Math.Max(0, Math.Min(newHeight, PanelCanais.Height - newTop));

                    overlayForm.TempRect = new Rectangle(overlayForm.TempRect.X, newTop, overlayForm.TempRect.Width, newHeight);
                }
                else
                {
                    int newHeight = originalHeight + deltaY;

                    newHeight = Math.Max(0, Math.Min(newHeight, PanelCanais.Height - originalTop));

                    overlayForm.TempRect = new Rectangle(overlayForm.TempRect.X, overlayForm.TempRect.Y, overlayForm.TempRect.Width, newHeight);
                }

                overlayForm.Invalidate(); // Redesenha o retângulo no overlay
            }
            else if (isMoving && overlayForm != null)
            {
                int deltaY = e.Y - mouseDownLocation.Y;
                int newTop = initialPanelY + deltaY;

                // Garantir que o retângulo temporário não saia do topo ou da parte inferior do PanelCanais
                newTop = Math.Max(0, Math.Min(newTop, PanelCanais.Height - overlayForm.TempRect.Height));

                overlayForm.TempRect = new Rectangle(overlayForm.TempRect.X, newTop, overlayForm.TempRect.Width, overlayForm.TempRect.Height);
                overlayForm.Invalidate(); // Redesenha o retângulo no overlay
            }
        }

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;

            if (isResizing && overlayForm != null)
            {
                // Finaliza o redimensionamento
                isResizing = false;
                isResizingFromTop = false;
                panel.Cursor = Cursors.Default;

                // Ajusta a altura e a posição do painel
                if (isOnTopBorder)
                {
                    movingPanel.Top = overlayForm.TempRect.Top;
                    movingPanel.Height = overlayForm.TempRect.Height;
                }
                else if (isOnBottomBorder)
                {
                    movingPanel.Height = overlayForm.TempRect.Height;
                }

                // Fecha o overlay
                overlayForm.Close();
                overlayForm.Dispose();
                overlayForm = null;
                AdjustPanelsAfterResize(movingPanel);
                // Atualiza a altura no DataTable após o redimensionamento
                UpdatePanelHeightInDataTable(movingPanel);
            }

            if (isMoving && overlayForm != null)
            {
                int newTop = overlayForm.TempRect.Top;

                // Reposiciona o painel
                movingPanel.Top = newTop;

                // Fecha o overlay
                overlayForm.Close();
                overlayForm.Dispose();
                overlayForm = null;

                // Reposicionar todos os painéis para garantir a ordem correta
                RepositionPanels();
                // Atualizar a linha do DataTable com base na nova posição do painel
                UpdateDataTableRow(movingPanel);

                isMoving = false;
                movingPanel = null;
            }
        }
        // Ajusta os painéis após o redimensionamento de um painel
        private void AdjustPanelsAfterResize(Panel resizedPanel)
        {
            // Definir o espaçamento entre os painéis
            const int spacing = 0;

            // Obter a altura total do container
            int totalHeight = PanelCanais.Height;
            // Calcular a altura restante após o redimensionamento
            int remainingHeight = totalHeight - resizedPanel.Height;

            // Obter os painéis que não são o que foi redimensionado
            var otherPanels = PanelCanais.Controls.OfType<Panel>().Where(p => p != resizedPanel).ToList();

            if (otherPanels.Count > 0)
            {
                // Calcula a nova altura proporcional para os outros painéis
                int newHeightPerPanel = remainingHeight / otherPanels.Count;

                // Ordenar os painéis por sua posição Top para garantir que o reposicionamento
                // seja feito corretamente
                var panels = PanelCanais.Controls.OfType<Panel>()
                                                 .OrderBy(p => p.Top)
                                                 .ToList();

                int currentTop = 0;

                // Ajustar os painéis para manter o layout correto
                foreach (Panel panel in panels)
                {
                    if (panel == resizedPanel)
                    {
                        // O painel redimensionado mantém a posição e altura
                        panel.Top = currentTop;
                        currentTop += panel.Height + spacing;
                    }
                    else
                    {
                        // Os outros painéis são ajustados proporcionalmente
                        panel.Top = currentTop;
                        panel.Height = newHeightPerPanel;
                        currentTop += panel.Height + spacing;
                    }
                }
            }
        }

        // Reposiciona todos os painéis após uma alteração de tamanho ou movimento
        private void RepositionPanels()
        {
            // Definir o espaçamento entre os painéis
            const int spacing = 0;

            // Ordenar os controles por sua posição Top para garantir que o reposicionamento
            // seja feito corretamente
            var panels = PanelCanais.Controls.OfType<Panel>()
                                             .OrderBy(p => p.Top)
                                             .ToList();

            int y = 0;
            foreach (Panel panel in panels)
            {
                panel.Top = y;
                y = panel.Bottom + spacing;
            }
        }

        // Atualiza a altura do painel correspondente na tabela de dados
        private void UpdatePanelHeightInDataTable(Panel panel)
        {
            var codCanal = panel.Tag.ToString();

            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                if (row["CodCanal1"].ToString().Equals(codCanal))
                {
                    row["Altura"] = (float)panel.Height / PanelCanais.Height * 100;
                    break;
                }
            }
        }

        // Atualiza a linha do DataTable com base na nova posição do painel
        private void UpdateDataTableRow(Panel panel)
        {
            var codCanal = panel.Tag.ToString();

            DataRow rowToMove = null;
            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                if (row["CodCanal1"].ToString().Equals(codCanal))
                {
                    rowToMove = row;
                    break;
                }
            }

            if (rowToMove != null)
            {
                DataRow newRow = GlobVar.tbl_MontagemSelecionada.NewRow();
                newRow.ItemArray = rowToMove.ItemArray.Clone() as object[];

                double auxIndex = PanelCanais.Height / GlobVar.tbl_MontagemSelecionada.Rows.Count;

                int newRowIndex = (int)Math.Round(panel.Top / (double)auxIndex);

                GlobVar.tbl_MontagemSelecionada.Rows.Remove(rowToMove);

                if (newRowIndex >= GlobVar.tbl_MontagemSelecionada.Rows.Count)
                {
                    GlobVar.tbl_MontagemSelecionada.Rows.Add(newRow);
                }
                else
                {
                    GlobVar.tbl_MontagemSelecionada.Rows.InsertAt(newRow, newRowIndex);
                }
            }
        }



        /*private void RepositionPanels()
        {
            // Definir o espaçamento entre os painéis
            const int spacing = 0;

            // Ordenar os controles por sua posição Top para garantir que o reposicionamento
            // seja feito corretamente
            var panels = PanelCanais.Controls.OfType<Panel>()
                                             .OrderBy(p => p.Top)
                                             .ToList();

            int y = 0;
            foreach (Panel panel in panels)
            {
                panel.Top = y;
                y = panel.Bottom + spacing;
            }
        }*/

        private void rectangleLoad()
        {
            gridView = new Rectangle(dataGridView1.Location, dataGridView1.Size);
            bt2 = new Rectangle(button2.Location, button2.Size);
            pncan = new Rectangle(PanelCanais.Location, PanelCanais.Size);

            btPlusLb1 = new Rectangle(plusLb1.Location, plusLb1.Size);
            btMinusLb1 = new Rectangle(minusLb1.Location, minusLb1.Size);
            btPlusLb2 = new Rectangle(plusLb2.Location, plusLb2.Size);
            btMinusLb2 = new Rectangle(minusLb2.Location, minusLb2.Size);
            btPlusLb3 = new Rectangle(plusLb4.Location, plusLb4.Size);
            btMinusLb3 = new Rectangle(minusLb4.Location, minusLb4.Size);
            btPlusLb4 = new Rectangle(plusLb3.Location, plusLb3.Size);
            btMinusLb4 = new Rectangle(minusLb3.Location, minusLb3.Size);
            btPlusLb5 = new Rectangle(plusLb5.Location, plusLb5.Size);
            btMinusLb5 = new Rectangle(minusLb5.Location, minusLb5.Size);

            lbScale1 = new Rectangle(scalaLb1.Location, scalaLb1.Size);
            lbScale2 = new Rectangle(scalaLb2.Location, scalaLb2.Size);
            lbScale3 = new Rectangle(scalaLb3.Location, scalaLb3.Size);
            lbScale4 = new Rectangle(scalaLb4.Location, scalaLb4.Size);
            lbScale5 = new Rectangle(scalaLb5.Location, scalaLb5.Size);
            lbScale6 = new Rectangle(scalaLb6.Location, scalaLb6.Size);
            lbScale7 = new Rectangle(scalaLb7.Location, scalaLb7.Size);
            lbScale8 = new Rectangle(scalaLb8.Location, scalaLb8.Size);
            lbScale9 = new Rectangle(scalaLb9.Location, scalaLb9.Size);
            lbScale10 = new Rectangle(scalaLb10.Location, scalaLb10.Size);
            lbScale11 = new Rectangle(scalaLb11.Location, scalaLb11.Size);
            lbScale12 = new Rectangle(scalaLb12.Location, scalaLb12.Size);
            lbScale13 = new Rectangle(scalaLb13.Location, scalaLb13.Size);
            lbScale14 = new Rectangle(scalaLb14.Location, scalaLb14.Size);
            lbScale15 = new Rectangle(scalaLb15.Location, scalaLb15.Size);
            lbScale16 = new Rectangle(scalaLb16.Location, scalaLb16.Size);
            lbScale17 = new Rectangle(scalaLb17.Location, scalaLb17.Size);
            lbScale18 = new Rectangle(scalaLb18.Location, scalaLb18.Size);
            lbScale19 = new Rectangle(scalaLb19.Location, scalaLb19.Size);
            lbScale20 = new Rectangle(scalaLb20.Location, scalaLb20.Size);
            lbScale21 = new Rectangle(scalaLb21.Location, scalaLb21.Size);
            lbScale22 = new Rectangle(scalaLb22.Location, scalaLb22.Size);
            lbScale23 = new Rectangle(scalaLb23.Location, scalaLb23.Size);


            btPlusLb6 = new Rectangle(plusLb6.Location, plusLb6.Size);
            btPlusLb7 = new Rectangle(plusLb7.Location, plusLb7.Size);
            btPlusLb8 = new Rectangle(plusLb8.Location, plusLb8.Size);
            btPlusLb9 = new Rectangle(plusLb9.Location, plusLb9.Size);
            btPlusLb10 = new Rectangle(plusLb10.Location, plusLb10.Size);
            btPlusLb11 = new Rectangle(plusLb11.Location, plusLb11.Size);
            btPlusLb12 = new Rectangle(plusLb12.Location, plusLb12.Size);
            btPlusLb13 = new Rectangle(plusLb13.Location, plusLb13.Size);
            btPlusLb14 = new Rectangle(plusLb14.Location, plusLb14.Size);
            btPlusLb15 = new Rectangle(plusLb15.Location, plusLb15.Size);
            btPlusLb16 = new Rectangle(plusLb16.Location, plusLb16.Size);
            btPlusLb17 = new Rectangle(plusLb17.Location, plusLb17.Size);
            btPlusLb18 = new Rectangle(plusLb18.Location, plusLb18.Size);
            btPlusLb19 = new Rectangle(plusLb19.Location, plusLb19.Size);
            btPlusLb20 = new Rectangle(plusLb20.Location, plusLb20.Size);
            btPlusLb21 = new Rectangle(plusLb21.Location, plusLb21.Size);
            btPlusLb22 = new Rectangle(plusLb22.Location, plusLb22.Size);
            btPlusLb23 = new Rectangle(plusLb23.Location, plusLb23.Size);

            btMinusLb6 = new Rectangle(minusLb6.Location, minusLb6.Size);
            btMinusLb7 = new Rectangle(minusLb7.Location, minusLb7.Size);
            btMinusLb8 = new Rectangle(minusLb8.Location, minusLb8.Size);
            btMinusLb9 = new Rectangle(minusLb9.Location, minusLb9.Size);
            btMinusLb10 = new Rectangle(minusLb10.Location, minusLb10.Size);
            btMinusLb11 = new Rectangle(minusLb11.Location, minusLb11.Size);
            btMinusLb12 = new Rectangle(minusLb12.Location, minusLb12.Size);
            btMinusLb13 = new Rectangle(minusLb13.Location, minusLb13.Size);
            btMinusLb14 = new Rectangle(minusLb14.Location, minusLb14.Size);
            btMinusLb15 = new Rectangle(minusLb15.Location, minusLb15.Size);
            btMinusLb16 = new Rectangle(minusLb16.Location, minusLb16.Size);
            btMinusLb17 = new Rectangle(minusLb17.Location, minusLb17.Size);
            btMinusLb18 = new Rectangle(minusLb18.Location, minusLb18.Size);
            btMinusLb19 = new Rectangle(minusLb19.Location, minusLb19.Size);
            btMinusLb20 = new Rectangle(minusLb20.Location, minusLb20.Size);
            btMinusLb21 = new Rectangle(minusLb21.Location, minusLb21.Size);
            btMinusLb22 = new Rectangle(minusLb22.Location, minusLb22.Size);
            btMinusLb23 = new Rectangle(minusLb23.Location, minusLb23.Size);

            pn1 = new Rectangle(panel1.Location, panel1.Size);
            pn2 = new Rectangle(panel2.Location, panel2.Size);
            pn3 = new Rectangle(panel3.Location, panel3.Size);
            pn4 = new Rectangle(panel4.Location, panel4.Size);
            pn5 = new Rectangle(panel5.Location, panel5.Size);
            pn6 = new Rectangle(panel6.Location, panel6.Size);
            pn7 = new Rectangle(panel7.Location, panel7.Size);
            pn8 = new Rectangle(panel8.Location, panel8.Size);
            pn9 = new Rectangle(panel9.Location, panel9.Size);
            pn10 = new Rectangle(panel10.Location, panel10.Size);
            pn11 = new Rectangle(panel11.Location, panel11.Size);
            pn12 = new Rectangle(panel12.Location, panel12.Size);
            pn13 = new Rectangle(panel13.Location, panel13.Size);
            pn14 = new Rectangle(panel14.Location, panel14.Size);
            pn15 = new Rectangle(panel15.Location, panel15.Size);
            pn16 = new Rectangle(panel16.Location, panel16.Size);
            pn17 = new Rectangle(panel17.Location, panel17.Size);
            pn18 = new Rectangle(panel18.Location, panel18.Size);
            pn19 = new Rectangle(panel19.Location, panel19.Size);
            pn20 = new Rectangle(panel20.Location, panel20.Size);
            pn21 = new Rectangle(panel21.Location, panel21.Size);
            pn22 = new Rectangle(panel22.Location, panel22.Size);
            pn23 = new Rectangle(panel23.Location, panel23.Size);

        }

        private void resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(this.Width) / (float)(formOriginalSize.Width);
            float yRatio = (float)(this.Height) / (float)(formOriginalSize.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Location = new System.Drawing.Point(newX, newY);
            c.Size = new System.Drawing.Size(newWidth, newHeight);
        }
        private void Tela_Plotagem_Resiz(object sender, EventArgs e)
        {
            resize_Control(PanelCanais, pncan);
            resize_Control(dataGridView1, gridView);
            resize_Control(button2, bt2);
        }
        private void button2_Click(object sender, EventArgs e)
        {

            // Exibir os dados agrupados no DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            //to get columns
            foreach (DataColumn col in GlobVar.tbl_MontagemSelecionada.Columns)
            {
                var c = new DataGridViewTextBoxColumn() { HeaderText = col.ColumnName }; //Let say that the default column template of DataGridView is DataGridViewTextBoxColumn
                dataGridView1.Columns.Add(c);
            }

            //to get rows
            foreach (DataRow row in GlobVar.tbl_MontagemSelecionada.Rows)
            {
                dataGridView1.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17], row[18], row[19]);
            }
        }

        private void painel_Resize_Control(Control c, Rectangle r)
        {
            float xRatio = (float)(PanelCanais.Width) / (float)(PanelCanais.Width);
            float yRatio = (float)(PanelCanais.Height) / (float)(PanelCanais.Height);
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);
            c.Location = new System.Drawing.Point(newX, newY);
            c.Size = new System.Drawing.Size(newWidth, newHeight);
        }
        private void panelLb_Resiz(object sender, EventArgs e)
        {
            painel_Resize_Control(plusLb1, btPlusLb1);
            painel_Resize_Control(plusLb2, btPlusLb2);
            painel_Resize_Control(plusLb3, btPlusLb3);
            painel_Resize_Control(plusLb4, btPlusLb4);
            painel_Resize_Control(plusLb5, btPlusLb5);
            painel_Resize_Control(plusLb6, btPlusLb6);
            painel_Resize_Control(plusLb7, btPlusLb7);
            painel_Resize_Control(plusLb8, btPlusLb8);
            painel_Resize_Control(plusLb9, btPlusLb9);
            painel_Resize_Control(plusLb10, btPlusLb10);
            painel_Resize_Control(plusLb11, btPlusLb11);
            painel_Resize_Control(plusLb12, btPlusLb12);
            painel_Resize_Control(plusLb13, btPlusLb13);
            painel_Resize_Control(plusLb14, btPlusLb14);
            painel_Resize_Control(plusLb15, btPlusLb15);
            painel_Resize_Control(plusLb16, btPlusLb16);
            painel_Resize_Control(plusLb17, btPlusLb17);
            painel_Resize_Control(plusLb18, btPlusLb18);
            painel_Resize_Control(plusLb19, btPlusLb19);
            painel_Resize_Control(plusLb20, btPlusLb20);
            painel_Resize_Control(plusLb21, btPlusLb21);
            painel_Resize_Control(plusLb22, btPlusLb22);
            painel_Resize_Control(plusLb23, btPlusLb23);

            painel_Resize_Control(minusLb1, btMinusLb1);
            painel_Resize_Control(minusLb2, btMinusLb2);
            painel_Resize_Control(minusLb3, btMinusLb3);
            painel_Resize_Control(minusLb4, btMinusLb4);
            painel_Resize_Control(minusLb5, btMinusLb5);
            painel_Resize_Control(minusLb6, btMinusLb6);
            painel_Resize_Control(minusLb7, btMinusLb7);
            painel_Resize_Control(minusLb8, btMinusLb8);
            painel_Resize_Control(minusLb9, btMinusLb9);
            painel_Resize_Control(minusLb10, btMinusLb10);
            painel_Resize_Control(minusLb11, btMinusLb11);
            painel_Resize_Control(minusLb12, btMinusLb12);
            painel_Resize_Control(minusLb13, btMinusLb13);
            painel_Resize_Control(minusLb14, btMinusLb14);
            painel_Resize_Control(minusLb15, btMinusLb15);
            painel_Resize_Control(minusLb16, btMinusLb16);
            painel_Resize_Control(minusLb17, btMinusLb17);
            painel_Resize_Control(minusLb18, btMinusLb18);
            painel_Resize_Control(minusLb19, btMinusLb19);
            painel_Resize_Control(minusLb20, btMinusLb20);
            painel_Resize_Control(minusLb21, btMinusLb21);
            painel_Resize_Control(minusLb22, btMinusLb22);
            painel_Resize_Control(minusLb23, btMinusLb23);

            painel_Resize_Control(scalaLb1, lbScale1);
            painel_Resize_Control(scalaLb2, lbScale2);
            painel_Resize_Control(scalaLb3, lbScale3);
            painel_Resize_Control(scalaLb4, lbScale4);
            painel_Resize_Control(scalaLb5, lbScale5);
            painel_Resize_Control(scalaLb6, lbScale6);
            painel_Resize_Control(scalaLb7, lbScale7);
            painel_Resize_Control(scalaLb8, lbScale8);
            painel_Resize_Control(scalaLb9, lbScale9);
            painel_Resize_Control(scalaLb10, lbScale10);
            painel_Resize_Control(scalaLb11, lbScale11);
            painel_Resize_Control(scalaLb12, lbScale12);
            painel_Resize_Control(scalaLb13, lbScale13);
            painel_Resize_Control(scalaLb14, lbScale14);
            painel_Resize_Control(scalaLb15, lbScale15);
            painel_Resize_Control(scalaLb16, lbScale16);
            painel_Resize_Control(scalaLb17, lbScale17);
            painel_Resize_Control(scalaLb18, lbScale18);
            painel_Resize_Control(scalaLb19, lbScale19);
            painel_Resize_Control(scalaLb20, lbScale20);
            painel_Resize_Control(scalaLb21, lbScale21);
            painel_Resize_Control(scalaLb22, lbScale22);
            painel_Resize_Control(scalaLb23, lbScale23);

            GlobVar.locBut.X = plusLb1.Location.X;
            GlobVar.locScale.X = scalaLb1.Location.X;

        }

        private void Painel_resiz(object sender, EventArgs e)
        {
            painel_Resize_Control(panel1, pn1);
            painel_Resize_Control(panel2, pn2);
            painel_Resize_Control(panel3, pn3);
            painel_Resize_Control(panel4, pn4);
            painel_Resize_Control(panel5, pn5);
            painel_Resize_Control(panel6, pn6);
            painel_Resize_Control(panel7, pn7);
            painel_Resize_Control(panel8, pn8);
            painel_Resize_Control(panel9, pn9);
            painel_Resize_Control(panel10, pn10);
            painel_Resize_Control(panel11, pn11);
            painel_Resize_Control(panel12, pn12);
            painel_Resize_Control(panel13, pn13);
            painel_Resize_Control(panel14, pn14);
            painel_Resize_Control(panel15, pn15);
            painel_Resize_Control(panel16, pn16);
            painel_Resize_Control(panel17, pn17);
            painel_Resize_Control(panel18, pn18);
            painel_Resize_Control(panel19, pn19);
            painel_Resize_Control(panel20, pn20);
            painel_Resize_Control(panel21, pn21);
            painel_Resize_Control(panel22, pn22);
            painel_Resize_Control(panel23, pn23);

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
                FieldInfo field = typeof(backLog).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo labelName = typeof(backLog).GetField($"label{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo plus = typeof(backLog).GetField($"plusLb{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo minus = typeof(backLog).GetField($"minusLb{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo scale = typeof(backLog).GetField($"scalaLb{i}", BindingFlags.Static | BindingFlags.Public);
                if (field != null) ;
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
                        scala.Location = new System.Drawing.Point((int)GlobVar.locScale.X, labelLocY);
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
                FieldInfo field = typeof(backLog).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
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
                FieldInfo labelName = typeof(backLog).GetField($"label{i}", BindingFlags.Static | BindingFlags.Public);
                FieldInfo field = typeof(backLog).GetField($"panel{i}", BindingFlags.Static | BindingFlags.Public);
                if (field != null)
                {
                    Panel panel = (Panel)field.GetValue(this);
                    Label label = (Label)labelName.GetValue(this);

                    if (panel != null || label != null)
                    {
                        //Faz a verificacao no banco de dados para saber quantos %aquele panel tem, deacordo com o a diferenca dele
                        float ySizeAux = (float)(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["Altura"]) / 100;
                        pnSize = (int)GlobVar.sizePainelExams.Y * ySizeAux;
                        auzpnSize += pnSize;
                        pnSizes[i] = auzpnSize;

                        panel.Location = new System.Drawing.Point(0, (int)yCanais[j]);
                        panel.Size = new System.Drawing.Size(pnSizeX, (int)pnSize);
                        panel.Tag = (int)(GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["CodCanal1"]);
                        label.Text = GlobVar.tbl_MontagemSelecionada.Rows[GlobVar.grafSelected[j]]["Legenda"].ToString();
                        j++;
                    }
                }
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {

            Stringao.Text = $"isOnBottomBorder: {isOnBottomBorder} | isOnTopBorder: {isOnTopBorder}";
        }


        private void PanelCanais_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
