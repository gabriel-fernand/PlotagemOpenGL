﻿using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Accord.Math; 
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.auxi.FormsAuxi;
using PlotagemOpenGL.Filtros;
using SharpGL.SceneGraph;
using Cyotek.Windows.Forms;


namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class InformaçõesDoCanal : Form
    {
        public static int tagCodCanal;
        public static DataRow dt;
        public static int tipo;
        public InformaçõesDoCanal()
        {
            InitializeComponent();
            LoadComponentes();
            trackBarR.Scroll += TrackBar_Scroll;
            trackBarG.Scroll += TrackBar_Scroll;
            trackBarB.Scroll += TrackBar_Scroll;
            Cancelar.Click += Cancelar_Click;
            Aplicar.Click += Aplicar_Click;
            Confirmar.Click += Confirmar_Click;
            comboNotch.TextChanged += ComboNotch_TabIndexChanged;
            comboPassaAlta.TextChanged += ComboPassaAlta_TabIndexChanged;
            comboPassaBaixa.TextChanged += ComboPassaBaixa_TabIndexChanged;
            boxReferencia.TextChanged += BoxReferencia_TabIndexChanged;
            Color.Click += Color_Click;
        }

        private void Color_Click(object sender, EventArgs e)
        {
            Color c = System.Drawing.Color.Black;
            ColorPickerDialog minhasCores = new ColorPickerDialog();
            if (minhasCores.ShowDialog() == DialogResult.OK)
            {
                c = minhasCores.Color;
                int cor = c.R | (c.G << 8) | (c.B << 16);
                trackBarR.Value = c.R;
                trackBarG.Value = c.G;
                trackBarB.Value = c.B;
                dt["Cor"] = cor;
                UpdateButtonColor();
            }
        }

        private void Confirmar_Click(object sender, EventArgs e)
        {
            if (dt != null)
            {
                // Atualiza os valores na DataRow com base nos controles da janela
                dt["Legenda"] = textLegenda.Text;
                dt["LimiteInferior"] = (int)numericUpDownLmInf.Value;
                dt["LimiteSuperior"] = (int)numericUpDownLmSup.Value;

                // Converte a cor dos trackbars para um formato RGB int e atualiza a cor na DataRow
                int cor = trackBarR.Value | (trackBarG.Value << 8) | (trackBarB.Value << 16);
                dt["Cor"] = cor;
                if (tipo == 12)
                {

                    //Posicional
                    if (MostrarSetas.Checked)
                    {
                        dt["InverteSinal"] = true;
                        dt["EliminaFreqInf"] = DBNull.Value;

                    }
                    else if (Configurar.Checked)
                    {
                        dt["InverteSinal"] = false;
                        dt["EliminaFreqInf"] = 1;
                    }
                    else if (GráficoSetas.Checked)
                    {
                        dt["InverteSinal"] = false;
                        dt["EliminaFreqInf"] = DBNull.Value;
                    }
                }
                else if (tipo == 20 || tipo == 21 || tipo == 23 || tipo == 24 || tipo == 15 || tipo == 16
                        || tipo == 28 || tipo == 29 || tipo == 32 || tipo == 31
                            || tipo == 15 || tipo == 30) // canal que tem numeros

                {

                    //Numero
                    if (Horizontal.Checked)
                    {
                        dt["AutoEscala"] = true;
                    }
                    else
                    {
                        dt["AutoEscala"] = false;
                    }

                    if (Números.Checked)
                    {
                        dt["InverteSinal"] = true;
                        dt["EliminaFreqInf"] = DBNull.Value;
                    }
                    else if (GraficoeNumeros.Checked)
                    {
                        dt["EliminaFreqInf"] = 1;
                        dt["InverteSinal"] = false;
                    }
                    else if (Gráfico.Checked)
                    {
                        dt["InverteSinal"] = false;
                        dt["EliminaFreqInf"] = DBNull.Value;
                    }
                }
                else
                {
                    dt["InverteSinal"] = InverteSinal.Checked;
                    dt["AutoEscala"] = AutoEscala.Checked;
                }

                if (Configurar.Checked || GraficoeNumeros.Checked)
                {
                    dt["EliminaFreqInf"] = 1;
                }
                if (comboAmplitude.SelectedItem != null)
                {
                    dt["AmplitudeMin"] = comboAmplitude.SelectedItem;

                    float scala = (float)(comboAmplitude.SelectedItem) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(Tela_Plotagem.index));

                    GlobVar.scale[Tela_Plotagem.index] = scala;
                }

                if (comboPassaBaixa.SelectedItem != null)
                {
                    dt["PassaBaixa"] = comboPassaBaixa.SelectedItem;
                }

                if (comboPassaAlta.SelectedItem != null)
                {
                    dt["PassaAlta"] = comboPassaAlta.SelectedItem;
                }

                if (comboNotch.SelectedItem != null)
                {
                    dt["Notch"] = comboNotch.SelectedItem;
                }
                //Troca a legenda do Label que esta no panel
                foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                {
                    if ((int)pn.Tag == tagCodCanal)
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if (lb.Tag.Equals("min"))
                            {
                                lb.Text = dt["LimiteInferior"].ToString();
                            }
                            if (lb.Tag.Equals("max"))
                            {
                                lb.Text = dt["LimiteSuperior"].ToString();
                            }

                            if (lb.Tag.Equals("scala"))
                            {
                                lb.Text = $"{comboAmplitude.SelectedItem} μV";
                            }

                            if (lb.Tag is int intTag)
                            {
                                if (intTag == tagCodCanal)
                                {
                                    lb.Text = textLegenda.Text;
                                }
                            }
                            else if (lb.Tag is string strTag)
                            {
                                if (int.TryParse(strTag, out int stringTagAsInt) && stringTagAsInt == tagCodCanal)
                                {
                                    lb.Text = textLegenda.Text;
                                }
                            }
                        }
                    }
                }



                // Atualiza o DataTable para refletir as mudanças (não é sempre necessário, depende da configuração)
                GlobVar.tbl_MontagemSelecionada.AcceptChanges();

                Tela_Plotagem.TelaClearAndReload();
                this.Close();
                // Exibe uma mensagem de sucesso ou atualiza a interface conforme necessário
                MessageBox.Show("Configurações aplicadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Não foi possível encontrar a linha correspondente para atualização.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Aplicar_Click(object sender, EventArgs e)
        {
            // Verifica se a linha atual está disponível
            if (dt != null)
            {
                // Atualiza os valores na DataRow com base nos controles da janela
                dt["Legenda"] = textLegenda.Text;
                dt["LimiteInferior"] = (int)numericUpDownLmInf.Value;
                dt["LimiteSuperior"] = (int)numericUpDownLmSup.Value;

                // Converte a cor dos trackbars para um formato RGB int e atualiza a cor na DataRow
                int cor = trackBarR.Value | (trackBarG.Value << 8) | (trackBarB.Value << 16);
                dt["Cor"] = cor;
                if (tipo == 12)
                {

                    //Posicional
                    if (MostrarSetas.Checked)
                    {
                        dt["InverteSinal"] = true;
                        dt["EliminaFreqInf"] = DBNull.Value;

                    }
                    else if (Configurar.Checked)
                    {
                        dt["InverteSinal"] = false;
                        dt["EliminaFreqInf"] = 1;
                    }
                    else if (GráficoSetas.Checked)
                    {
                        dt["InverteSinal"] = false;
                        dt["EliminaFreqInf"] = DBNull.Value;
                    }
                }
                else if (tipo == 20 || tipo == 21 || tipo == 23 || tipo == 24 || tipo == 15 || tipo == 16
                        || tipo == 28 || tipo == 29 || tipo == 32 || tipo == 31
                            || tipo == 15 || tipo == 30) // canal que tem numeros

                {

                    //Numero
                    if (Horizontal.Checked)
                    {
                        dt["AutoEscala"] = true;
                    }
                    else
                    {
                        dt["AutoEscala"] = false;
                    }

                    if (Números.Checked)
                    {
                        dt["InverteSinal"] = true;
                        dt["EliminaFreqInf"] = DBNull.Value;
                    }
                    else if (GraficoeNumeros.Checked)
                    {
                        dt["EliminaFreqInf"] = 1;
                        dt["InverteSinal"] = false;
                    }
                    else if (Gráfico.Checked)
                    {
                        dt["InverteSinal"] = false;
                        dt["EliminaFreqInf"] = DBNull.Value;
                    }
                }
                else
                {
                    dt["InverteSinal"] = InverteSinal.Checked;
                    dt["AutoEscala"] = AutoEscala.Checked;
                }

                if (Configurar.Checked || GraficoeNumeros.Checked)
                {
                    dt["EliminaFreqInf"] = 1;
                }
                if (comboAmplitude.SelectedItem != null)
                {
                    dt["AmplitudeMin"] = comboAmplitude.SelectedItem;
                    float scala = (float)(comboAmplitude.SelectedItem) / LeituraEmMatrizTeste.Ampli(LeituraEmMatrizTeste.CodTipo(Tela_Plotagem.index));

                    GlobVar.scale[Tela_Plotagem.index] = scala;

                }

                if (comboPassaBaixa.SelectedItem != null)
                {
                    dt["PassaBaixa"] = comboPassaBaixa.SelectedItem;
                }

                if (comboPassaAlta.SelectedItem != null)
                {
                    dt["PassaAlta"] = comboPassaAlta.SelectedItem;
                }

                if (comboNotch.SelectedItem != null)
                {
                    dt["Notch"] = comboNotch.SelectedItem;
                }

                // Atualiza o DataTable para refletir as mudanças (não é sempre necessário, depende da configuração)
                GlobVar.tbl_MontagemSelecionada.AcceptChanges();

                foreach (Panel pn in Tela_Plotagem.painelExames.Controls)
                {
                    if ((int)pn.Tag == tagCodCanal)
                    {
                        foreach (Label lb in pn.Controls.OfType<Label>())
                        {
                            if (lb.Tag.Equals("scala"))
                            {
                                lb.Text = $"{comboAmplitude.SelectedItem} μV";
                            }

                            if (lb.Tag is int intTag)
                            {
                                if (intTag == tagCodCanal)
                                {
                                    lb.Text = textLegenda.Text;
                                }
                            }
                            else if (lb.Tag is string strTag)
                            {
                                if (int.TryParse(strTag, out int stringTagAsInt) && stringTagAsInt == tagCodCanal)
                                {
                                    lb.Text = textLegenda.Text;
                                }
                            }
                        }
                    }
                }
                Tela_Plotagem.TelaClearAndReload();

                // Exibe uma mensagem de sucesso ou atualiza a interface conforme necessário
                MessageBox.Show("Configurações aplicadas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Não foi possível encontrar a linha correspondente para atualização.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static void LoadComponentes()
        {
            tagCodCanal = Tela_Plotagem.tagCodCanal;
            if (GlobVar.Amplitude != null)
            {
                // Converte o vetor de inteiros para um array de objetos e adiciona ao ComboBox
                comboAmplitude.Items.AddRange(Array.ConvertAll(GlobVar.Amplitude, item => (object)item));
            }
            dt = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .FirstOrDefault(row => row.Field<int>("CodCanal1") == Tela_Plotagem.tagCodCanal);

            var rowNumerico = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                                .Where(row => row.Field<int>("CodCanal1") == Tela_Plotagem.tagCodCanal).CopyToDataTable();

            if (rowNumerico != null)
            {
                var CodTipoCanal = GlobVar.tbl_TipoCanal.AsEnumerable()
                                            .Where(row => row.Field<int>("CodCanal") == Tela_Plotagem.tagCodCanal).CopyToDataTable();
                int TipoCanal = Convert.ToInt16(CodTipoCanal.Rows[0]["CodTipo"]);
                tipo = TipoCanal;
                //If feito para mudar com base no tipo de canal quando de Posicao, Numero e Grafico -- faz abrir o que responde aoq no menu de inverter canal e 
                // mudar de grafico para numero ou seta
                if (TipoCanal == 12)
                {
                    Posicao.Show();
                    Grafico.Hide();
                    NumerosPosi.Hide();
                    comboAmplitude.Enabled = false;
                    comboNotch.Enabled = false;
                    comboPassaAlta.Enabled = false;
                    comboPassaBaixa.Enabled = false;
                    numericUpDownLmInf.Enabled = false;
                    numericUpDownLmSup.Enabled = false;

                    if ((bool)rowNumerico.Rows[0]["InverteSinal"])
                    {
                        MostrarSetas.Checked = true;
                    }
                    else if (rowNumerico.Rows[0]["EliminaFreqInf"] != DBNull.Value)
                    {
                        Configurar.Checked = true;
                    }
                    else if(!(bool)rowNumerico.Rows[0]["InverteSinal"])
                    {
                        GráficoSetas.Checked = true;
                    }
                } //Canal de posi
                else if (TipoCanal == 20 || TipoCanal == 21 || TipoCanal == 23 || TipoCanal == 24 || TipoCanal == 15 || TipoCanal == 16 
                    || TipoCanal == 28 || TipoCanal == 29 || TipoCanal == 32 || TipoCanal == 31
                                || TipoCanal == 15 || TipoCanal == 30) // canal que tem numeros
                {
                    Posicao.Hide();
                    Grafico.Hide();
                    NumerosPosi.Show();
                    comboAmplitude.Enabled = false;
                    comboNotch.Enabled = false;
                    comboPassaAlta.Enabled = false;
                    comboPassaBaixa.Enabled = false;
                    numericUpDownLmInf.Value = Convert.ToInt16(rowNumerico.Rows[0]["LimiteInferior"]); 
                    numericUpDownLmSup.Value = Convert.ToInt16(rowNumerico.Rows[0]["LimiteSuperior"]);

                    if ((bool)rowNumerico.Rows[0]["AutoEscala"])
                    {
                        Horizontal.Checked = true;
                    }
                    else
                    {
                        Vertical.Checked = true;
                    }

                    if ((bool)rowNumerico.Rows[0]["InverteSinal"])
                    {
                        Números.Checked = true;
                    }
                    else if (rowNumerico.Rows[0]["EliminaFreqInf"] != DBNull.Value)
                    {
                        GraficoeNumeros.Checked = true;
                    }
                    else if(!(bool)rowNumerico.Rows[0]["InverteSinal"])
                    {
                        Gráfico.Checked = true;
                    }

                }
                else
                {
                    Posicao.Hide();
                    Grafico.Show();
                    NumerosPosi.Hide();
                    if(TipoCanal == 1)
                    {
                        boxReferencia.Show();
                        Referencia.Show();

                        boxReferencia.Items.AddRange(GlobVar.nomeReferencia);
                        boxReferencia.SelectedIndex = GlobVar.canaisReferencia.IndexOf(Convert.ToInt16(rowNumerico.Rows[0]["CodCanal2"]));
                    }
                    else
                    {
                        boxReferencia.Hide();
                        Referencia.Hide();
                    }

                    // Verifica se o valor é DBNull antes de converter para int e definir o SelectedIndex
                    int indexAmplitude = rowNumerico.Rows[0]["AmplitudeMin"] != DBNull.Value ? Convert.ToInt16(rowNumerico.Rows[0]["AmplitudeMin"]) : -1;
                    int indexNotch = rowNumerico.Rows[0]["Notch"] != DBNull.Value ? Convert.ToInt16(rowNumerico.Rows[0]["Notch"]) : -1;
                    int indexPassaBaixa = rowNumerico.Rows[0]["PassaBaixa"] != DBNull.Value ? Convert.ToInt16(rowNumerico.Rows[0]["PassaBaixa"]) : -1;
                    int indexPassaAlta = rowNumerico.Rows[0]["PassaAlta"] != DBNull.Value ? Convert.ToInt16(rowNumerico.Rows[0]["PassaAlta"]) : -1;
                    
                    int SelectA = indexAmplitude != -1 ? GlobVar.Amplitude.IndexOf(indexAmplitude) : -1;
                    int SelectPB = indexPassaBaixa != -1 ? comboPassaBaixa.Items.IndexOf(indexPassaBaixa) : -1;
                    int SelectPA = indexPassaAlta != -1 ? comboPassaAlta.Items.IndexOf(indexPassaAlta) : -1;
                    int SelectN = indexNotch != -1 ? comboNotch.Items.IndexOf(indexNotch) : -1;

                    // Define o SelectedIndex dos ComboBox baseando-se no valor obtido ou em -1 se for DBNull
                    comboAmplitude.SelectedIndex = SelectA;
                    comboNotch.SelectedIndex = SelectN;
                    comboPassaAlta.SelectedIndex = SelectPA;
                    comboPassaBaixa.SelectedIndex = SelectPB;

                    numericUpDownLmInf.Enabled = false;
                    numericUpDownLmSup.Enabled = false;

                    InverteSinal.Checked = (bool)rowNumerico.Rows[0]["InverteSinal"];
                    AutoEscala.Checked = (bool)rowNumerico.Rows[0]["AutoEscala"];
                } //Canal de graficos
                //Fazendo o preencimento das informacoes do Canal
                var cadCanal = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == Tela_Plotagem.tagCodCanal).CopyToDataTable();
                textNome.Text = $"{cadCanal.Rows[0]["NomeCanal"].ToString()}";

                var cadTipoCanal = GlobVar.tbl_CadTipoCanal.AsEnumerable().Where(row => row.Field<int>("CodTipo") == TipoCanal).CopyToDataTable();
                textTipoDeCanal.Text = $"{cadTipoCanal.Rows[0]["DescrTipo"].ToString()}";

                textAmostragem.Text = $"{rowNumerico.Rows[0]["QtdAmostras"].ToString()}";
                textLegenda.Text = $"{rowNumerico.Rows[0]["Legenda"]}";

                int[] color = new int[3];
                color = ObterComponentesRGB(Convert.ToInt32(rowNumerico.Rows[0]["Cor"]));
                int[] colorint = new int[3];

                trackBarR.Value = color[0];
                trackBarG.Value = color[1];
                trackBarB.Value = color[2];
                Color butColor = System.Drawing.Color.FromArgb(trackBarR.Value, trackBarG.Value, trackBarB.Value);
                Color.BackColor = butColor;
            }
        }
        private void textBox1_Enter(object sender, EventArgs e)
        {
            // Remove o foco do TextBox
            this.ActiveControl = null; // Remove o foco do TextBox
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            // Impede que o TextBox receba o foco quando clicado
            this.ActiveControl = null;
        }

        public static int[] ObterComponentesRGB(int formatoRGB)
        {
            string hexa = formatoRGB.ToString("X");
            int hexValue = int.Parse(hexa, System.Globalization.NumberStyles.HexNumber);

            int[] colorRGB = new int[3];
            int red = (hexValue >> 16) & 0xFF;
            int green = (hexValue >> 8) & 0xFF;
            int blue = hexValue & 0xFF;
            float a = 1.0f; // Fully opaque

            // Normalizar os componentes RGB para o intervalo [0, 1]
            colorRGB[0] = blue;
            colorRGB[1] = green;
            colorRGB[2] = red;

            // Retornar os componentes RGB como um array de floats
            return colorRGB;
        }
        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            UpdateButtonColor();
        }

        private void ComboNotch_TabIndexChanged(object sender, System.EventArgs e)
        {
            float hertzSelectLow = 0;
            bool isAnyFilterSelectedLow ;

            float hertzSelectHigh = 0;
            bool isAnyFilterSelectedHigh ;

            if (comboPassaBaixa.SelectedItem != null)
            {
                hertzSelectLow = (float)comboPassaBaixa.SelectedItem;

            } //Verifica se tem algum filtro aplicado de LowPass
            if (comboPassaAlta.SelectedItem != null)
            {
                hertzSelectHigh = (float)comboPassaAlta.SelectedItem;
            } //Verifica se tem algum filtro aplicado de HighPass

            if (hertzSelectLow == 0 && hertzSelectHigh == 0)
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                if (comboNotch.SelectedItem == null)
                {
                    dt["Notch"] = DBNull.Value;
                }
                else
                {
                    float hertzSelect = (float)comboNotch.SelectedItem;
                    dt["Notch"] = hertzSelect;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }
            }
            else if (hertzSelectLow != 0 && hertzSelectHigh == 0)
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectLow, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));


                if (comboNotch.SelectedItem == null)
                {
                    dt["Notch"] = DBNull.Value;
                }
                else
                {
                    float hertzSelect = (float)comboNotch.SelectedItem;
                    dt["Notch"] = hertzSelect;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }

            }
            else if (hertzSelectLow == 0 && hertzSelectHigh != 0)
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));


                if (comboNotch.SelectedItem == null)
                {
                    dt["Notch"] = DBNull.Value;
                }
                else
                {
                    float hertzSelect = (float)comboNotch.SelectedItem;
                    dt["Notch"] = hertzSelect;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }
            }
            else
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectLow, (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                if (comboNotch.SelectedItem == null)
                {
                    dt["Notch"] = DBNull.Value;
                }
                else
                {
                    float hertzSelect = (float)comboNotch.SelectedItem;
                    dt["Notch"] = hertzSelect;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, 10, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }

            }
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();
        }

        private void ComboPassaBaixa_TabIndexChanged(object sender, System.EventArgs e)
        {
            float hertzSelectHigh = 0;
            bool isAnyFilterSelectedHigh;

            if (comboPassaAlta.SelectedItem != null)
            {
                hertzSelectHigh = (float)comboPassaAlta.SelectedItem;
            } //Verifica se tem algum filtro aplicado de HighPass

            if (hertzSelectHigh == 0)
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                if (comboPassaBaixa.SelectedItem == null)
                {
                    dt["PassaBaixa"] = DBNull.Value;

                }
                else
                {
                    float hertzSelect = Convert.ToInt16(comboPassaBaixa.SelectedItem);
                    dt["PassaBaixa"] = hertzSelect;

                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }                
            }
            else
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                if (comboPassaBaixa.SelectedItem == null)
                {
                    dt["PassaBaixa"] = DBNull.Value;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                        LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                }
                else
                {
                    float hertzSelect = Convert.ToInt16(comboPassaBaixa.SelectedItem);
                    dt["PassaBaixa"] = hertzSelect;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                }

            }
            if (comboNotch.SelectedItem != null)
            {
                float NotchHertz = Convert.ToInt16(comboNotch.SelectedItem);
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), 
                    (float)NotchHertz, 
                    10, 
                    GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
            }
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();

        }

        private void ComboPassaAlta_TabIndexChanged(object sender, System.EventArgs e)
        {
            float hertzSelectLow = 0;
            bool isAnyFilterSelectedLow;
            if (comboPassaBaixa.SelectedItem != null)
            {
                hertzSelectLow = (float)comboPassaBaixa.SelectedItem;

            } //Verifica se tem algum filtro aplicado de LowPass
            if(hertzSelectLow == 0)
            {
                int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                if (comboPassaAlta.SelectedItem == null)
                {
                    dt["PassaAlta"] = DBNull.Value;
                }
                else
                {
                    float hertzSelect = Convert.ToInt16(comboPassaAlta.SelectedItem);
                    dt["PassaAlta"] = hertzSelect;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                        LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                }
            }
            else
            {
                if (comboPassaAlta.SelectedItem == null)
                {
                    dt["PassaAlta"] = DBNull.Value;
                    GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                        LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectLow, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                }
                float hertzSelect = Convert.ToInt16(comboPassaAlta.SelectedItem);
                dt["PassaAlta"] = hertzSelect;
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                    LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectLow, (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));


            }
            if (comboNotch.SelectedItem != null)
            {
                float NotchHertz = Convert.ToInt16(comboNotch.SelectedItem);
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))),
                    (float)NotchHertz,
                    10,
                    GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
            }
            GlobVar.tbl_MontagemSelecionada.AcceptChanges();

        }

        private void BoxReferencia_TabIndexChanged(object sender, System.EventArgs e)
        {
            if (Convert.ToInt16(dt["CodCanal2"]) != -1)
            {
                dt["CodCanal2"] = GlobVar.canaisReferencia[(boxReferencia.SelectedIndex)];
                GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])), LeituraEmMatrizTeste.SetReferencia(Convert.ToInt16(dt["CodCanal1"]), Convert.ToInt16(dt["CodCanal2"])));
                var cadCanal = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == Tela_Plotagem.tagCodCanal).CopyToDataTable();
                var cadCanal2 = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == Convert.ToInt16(dt["CodCanal2"])).CopyToDataTable();
                string newLegenda = $"{cadCanal.Rows[0]["NomeCanal"]} - {cadCanal2.Rows[0]["NomeCanal"]}";
                textLegenda.Text = newLegenda;

                if (dt["PassaBaixa"] != DBNull.Value || dt["PassaAlta"] != DBNull.Value || dt["Notch"] != DBNull.Value)
                {
                    float hertzSelectHigh = 0;
                    bool isAnyFilterSelectedHigh;

                    if (comboPassaAlta.SelectedItem != null)
                    {
                        hertzSelectHigh = (float)comboPassaAlta.SelectedItem;
                    } //Verifica se tem algum filtro aplicado de HighPass

                    if (hertzSelectHigh == 0)
                    {
                        int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                        filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);

                        if (comboPassaBaixa.SelectedItem == null)
                        {
                            dt["PassaBaixa"] = DBNull.Value;

                        }
                        else
                        {
                            float hertzSelect = Convert.ToInt16(comboPassaBaixa.SelectedItem);
                            dt["PassaBaixa"] = hertzSelect;

                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                            LeituraEmMatrizTeste.ShortToFloat(PaissaBaixa.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                        }
                    }
                    else
                    {
                        int selec = GlobVar.grafSelected.IndexOf(Tela_Plotagem.index);
                        filtrosSinais.VoltaMatriz((short)GlobVar.codSelected[selec]);
                        if (comboPassaBaixa.SelectedItem == null)
                        {
                            dt["PassaBaixa"] = DBNull.Value;
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                                LeituraEmMatrizTeste.ShortToFloat(PaissaAlta.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                        }
                        else
                        {
                            float hertzSelect = Convert.ToInt16(comboPassaBaixa.SelectedItem);
                            dt["PassaBaixa"] = hertzSelect;
                            GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                            LeituraEmMatrizTeste.ShortToFloat(BandPass.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))), (float)hertzSelect, (float)hertzSelectHigh, GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));

                        }

                    }
                    if (comboNotch.SelectedItem != null)
                    {
                        float NotchHertz = Convert.ToInt16(comboNotch.SelectedItem);
                        GlobVar.matrizCanal.SetRow<short>(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])),
                        LeituraEmMatrizTeste.ShortToFloat(PlotagemOpenGL.Filtros.Notch.ApplyFilter(LeituraEmMatrizTeste.FloatToShort(GlobVar.matrizCanal.GetRow(GlobVar.codSelected.IndexOf(Convert.ToInt16(dt["CodCanal1"])))),
                            (float)NotchHertz,
                            10,
                            GlobVar.txPorCanal[GlobVar.codCanal.IndexOf(GlobVar.codSelected[Tela_Plotagem.index])])));
                    }

                }

            }
        }

        private void UpdateButtonColor()
        {
            // Cria a cor com base nos valores das trackbars
            Color buttonColor = System.Drawing.Color.FromArgb(trackBarR.Value, trackBarG.Value, trackBarB.Value);

            // Define a cor de fundo do botão
            Color.BackColor = buttonColor;
        }
    }
}
