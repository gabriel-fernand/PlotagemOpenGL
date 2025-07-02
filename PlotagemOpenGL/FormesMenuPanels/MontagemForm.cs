using Cyotek.Windows.Forms;
using Microsoft.Office.Interop.Word;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.Filtros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class MontagemForm : Form
    {
        public static ToolTip NomesCanais = new ToolTip();
        private bool _isManuallyChangingRadios = false;
        public MontagemForm()
        {
            InitializeComponent();
            ConfigurarTooltips(panelImagem); // Configura os tooltips para todos os botões do painel principal
            NomesCanais.SetToolTip(EOE, "EOE");
            ConfigurarDataGridView();
            GetIniThings();
            Montagem.SelectedIndexChanged += Montagem_SelectedIndexChanged;
            TaxaBox.Items.AddRange(new object[] { 1, 2, 4, 8, 16, 32, 64, 128, 256, 512 });
            foreach (Control ctrl in panelImagem.Controls)
            {
                if (ctrl is RadioButton rb)
                {
                    rb.AutoCheck = false;
                    rb.Click += CustomRadioClick;
                    rb.CheckedChanged += Rb_CheckedChanged;
                }
            }
            cima.Click += btnCima_Click;
            baixo.Click += btnBaixo_Click;
            CanalBox.SelectedIndexChanged += CanalBoxSelectedIndexChanged;
            RefBox.SelectedIndexChanged += RefBox_SelectedIndexChanged;
            this.MaximizeBox = false; // Remove o botão maximizar
        }

        private void RefBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TituloBox.Text = $"{CanalBox.Text} - {RefBox.Text}";
            var rowCadCanl = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<string>("NomeCanal").Equals(RefBox.Text)).FirstOrDefault();
            var selecionados = panelImagem.Controls
                                    .OfType<RadioButton>()
                                    .Where(r => r.Checked)
                                    .ToList();

            if (rowCadCanl != null && !RefBox.Text.Equals("") && selecionados.Count < 2)
            {

                int codCanal = Convert.ToInt16(rowCadCanl["CodCanal"]);

                foreach (RadioButton rb in panelImagem.Controls.OfType<RadioButton>())
                {
                    int tag = Convert.ToInt32(rb.Tag);
                    if (tag == codCanal)
                    {
                        rb.Checked = true;
                    }
                }
            }
        }

        // Defina isso na sua classe:
        //private bool _isManuallyChangingRadios = false;

        private void CanalBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (CanalBox.Text.Equals("")) return;
            TituloBox.Text = CanalBox.Text;
            if (RefBox.Text != null && !RefBox.Text.Equals(""))
            {
                TituloBox.Text = $"{CanalBox.Text} - {RefBox.Text}";
            }

            var rowCadCanl = GlobVar.tbl_CadCanal.AsEnumerable()
                .Where(row => row.Field<string>("NomeCanal").Equals(CanalBox.Text))
                .FirstOrDefault();
            int codCanal = Convert.ToInt16(rowCadCanl["CodCanal"]);
            var rowTipoCad = GlobVar.tbl_TipoCanal.AsEnumerable()
                .Where(row => row.Field<int>("CodCanal") == codCanal)
                .FirstOrDefault();
            int codTipoCanal = Convert.ToInt16(rowTipoCad["CodTipo"]);
            var rowCadTipoCanal = GlobVar.tbl_CadTipoCanal.AsEnumerable()
                .Where(row => row.Field<int>("CodTipo") == codTipoCanal)
                .FirstOrDefault();

            string descrTipo = rowCadTipoCanal["DescrTipo"].ToString();

            int idx = 0;
            for (int i = 0; i < TipoCanBox.Items.Count; i++)
            {
                var item = TipoCanBox.Items[i] as ComboItem;
                if (item != null && item.Texto.Equals(descrTipo))
                {
                    idx = i; break;
                }
            }
            TipoCanBox.SelectedIndex = idx;

            foreach (RadioButton rb in panelImagem.Controls.OfType<RadioButton>())
            {
                int tag = Convert.ToInt32(rb.Tag);
                rb.Checked = (tag == codCanal); // só seleciona deste canal
            }
            // FIM DA PROTEÇÃO

            if (codCanal == 150)
            {
                lbInferior.Visible = false;
                lbSuperior.Visible = false;
                LMSuperior.Visible = false;
                LMInferior.Visible = false;
            }
            else if (codCanal == 76 || codCanal == 66 || codCanal == 67)
            {
                lbInferior.Visible = true;
                lbSuperior.Visible = true;
                LMSuperior.Visible = true;
                LMInferior.Visible = true;
            }
            else
            {
                lbInferior.Visible = false;
                lbSuperior.Visible = false;
                LMSuperior.Visible = false;
                LMInferior.Visible = false;
            }
        }

        private void CustomRadioClick(object sender, EventArgs e)
        {

            var clicado = sender as RadioButton;
            // Alternar o estado do botão clicado (marcado/desmarcado)
            clicado.Checked = !clicado.Checked;

            var selecionados = panelImagem.Controls
                .OfType<RadioButton>()
                .Where(r => r.Checked)
                .ToList();

            if (selecionados.Count > 2)
            {
                // Desmarca todos e marca só o último clicado
                foreach (var r in selecionados)
                    r.Checked = false;
                clicado.Checked = true;
                RefBox.Text = "";
            }

        }
        private void Rb_CheckedChanged(object sender, EventArgs e)
        {
            var checado = sender as RadioButton;
            if (checado.Tag != null)
            {

                var selecionados = panelImagem.Controls
                    .OfType<RadioButton>()
                    .Where(r => r.Checked)
                    .ToList();

                if (selecionados.Count > 2)
                {
                    // Desmarca todos e marca só o último clicado
                    foreach (var r in selecionados)
                        r.Checked = false;
                    checado.Checked = true;
                    RefBox.Text = "";
                }

                int tag = Convert.ToInt32(checado.Tag);
                var rowTag = GlobVar.tbl_CadCanal.AsEnumerable().Where(row => row.Field<int>("CodCanal") == tag).FirstOrDefault();
                //var selecionados = panelImagem.Controls
                  //                  .OfType<RadioButton>()
                    //                .Where(r => r.Checked)
                      //              .ToList();
                if(selecionados.Count == 1 || selecionados.Count > 2)
                {
                    var canalValue = rowTag["NomeCanal"].ToString();
                    int idx = 0;
                    for (int i = 0; i < CanalBox.Items.Count; i++)
                    {
                        var item = CanalBox.Items[i] as ComboItem;
                        if (item != null && item.Texto.Equals(canalValue))
                        {
                            idx = i;
                            break;
                        }
                    }
                    CanalBox.SelectedIndex = idx;
                    RefBox.SelectedIndex = 0;


                }
                else if(selecionados.Count == 2)
                {
                    var TipocanalValue = rowTag["NomeCanal"].ToString();
                    int idx = 0;
                    for (int i = 0; i < RefBox.Items.Count; i++)
                    {
                        var item = RefBox.Items[i] as ComboItem;
                        if (item != null && item.Texto.Equals(TipocanalValue))
                        {
                            idx = i;
                            break;
                        }
                    }
                    RefBox.SelectedIndex = idx;
                }
            }
        }

        public static IniFile ini = new IniFile(@"C:\Temp\Config.ini");

        public void GetIniThings()
        {
            // Pegando as possibilidades de amplitude
            string ampli = ini.Read("CONTROLES", "AMPLITUDE");

            AmplitudeBox.Items.Clear(); // Limpa a lista antes de adicionar novos valores

            if (!string.IsNullOrWhiteSpace(ampli))
            {
                string[] valores = ampli.Split(';');
                foreach (var valor in valores)
                {
                    if (int.TryParse(valor.Trim(), out int numero))
                    {
                        AmplitudeBox.Items.Add(numero);
                    }
                    // else pode logar erro ou ignorar valores inválidos silenciosamente
                }
            }

            // Pegando as possibilidades pro filtro notch
            string notch = ini.Read("CONTROLES", "FILTRO_NOTCH");

            NotchBox.Items.Clear();
            NotchBox.Items.Add("");
            if (!string.IsNullOrWhiteSpace(notch))
            {
                string[] valores = notch.Split(";");
                foreach(var valor in valores)
                {
                    if(int.TryParse(valor.Trim(),out int numero))
                    {
                        NotchBox.Items.Add(numero);
                    }
                }
            }

            // Pegando as montagem, que ja existem no banco de dados
            if(GlobVar.tbl_MontagemOriginal != null)
            {
                var tblOrdenado = GlobVar.tbl_MontagemOriginal
                                        .AsEnumerable()
                                        .OrderBy(row => row.Field<int>("CodMontagem"))
                                        .CopyToDataTable();

                Montagem.Items.Clear();
                Montagem.Items.Add("");
                foreach (DataRow rw in tblOrdenado.Rows)
                {
                    string descricao = rw["DescrMontagem"].ToString();
                    var tag = rw["CodMontagem"]; // Ou qualquer campo/tag que queira associar
                    Montagem.Items.Add(new ComboItem(descricao, tag));

                }
            }

            if(GlobVar.tbl_CadCanal != null)
            {
                var tblOrdenado = GlobVar.tbl_CadCanal
                    .AsEnumerable()
                    .OrderBy(row => row.Field<string>("NomeCanal"))
                    .CopyToDataTable();
                CanalBox.Items.Clear();

                RefBox.Items.Clear();
                RefBox.Items.Add("");

                foreach(DataRow row in tblOrdenado.Rows)
                {
                    string NomeCanal = row["NomeCanal"].ToString();
                    var tag = row["CodCanal"]; // Ou qualquer campo/tag que queira associar

                    CanalBox.Items.Add(new ComboItem(NomeCanal, tag));
                    RefBox.Items.Add(new ComboItem(NomeCanal, tag));


                }
            }

            // Pegando as montagem, que ja existem no banco de dados
            if (GlobVar.tbl_CadTipoCanal != null)
            {
                var tblOrdenado = GlobVar.tbl_CadTipoCanal
                                        .AsEnumerable()
                                        .OrderBy(row => row.Field<string>("DescrTipo"))
                                        .CopyToDataTable();

                TipoCanBox.Items.Clear();
                TipoCanBox.Items.Add("");
                foreach (DataRow rw in tblOrdenado.Rows)
                {
                    string descricao = rw["DescrTipo"].ToString();
                    var tag = rw["CodTipo"]; // Ou qualquer campo/tag que queira associar
                    TipoCanBox.Items.Add(new ComboItem(descricao, tag));

                }
            }

        }
        private void Montagem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Montagem.SelectedItem is ComboItem item)
            {
                var minhaTag = item.Tag;
                // Agora você pode usar a tag para qualquer lógica adicional
                //MessageBox.Show($"Tag: {minhaTag}");
                var rowTblMontagem = GlobVar.tbl_Montagem.AsEnumerable().Where(rw => rw.Field<int>("CodMontagem") == Convert.ToInt32(minhaTag)).FirstOrDefault();
                if(rowTblMontagem != null)
                {
                    if (Convert.ToInt32(rowTblMontagem["TeclaRapida"]) != -1)
                    {
                        TeclaRapida.Text = rowTblMontagem["TeclaRapida"].ToString();
                    }

                    if (rowTblMontagem["TipoMontagem"].ToString().Equals("P"))
                    {
                        Polissonografia.Checked = true;
                    }
                    else if (rowTblMontagem["TipoMontagem"].ToString().Equals("M"))
                    {
                        MultiplaLatencia.Checked = true;
                    }
                    else if (rowTblMontagem["TipoMontagem"].ToString().Equals("C"))
                    {
                        Polissonografia.Checked = false;
                        MultiplaLatencia.Checked = false;
                        aEEG.Checked = false;
                        Respiratorio.Checked = false;
                        EEG.Checked = false;
                    }
                    else if (rowTblMontagem["TipoMontagem"].ToString().Equals("R"))
                    {
                        Respiratorio.Checked = true;
                    }
                    else if (rowTblMontagem["TipoMontagem"].ToString().Equals("E"))
                    {
                        EEG.Checked = true;
                    }
                    else if (rowTblMontagem["TipoMontagem"].ToString().Equals("A"))
                    {
                        aEEG.Checked = true;
                    }
                    else
                    {
                        Polissonografia.Checked = false;
                        MultiplaLatencia.Checked = false;
                        aEEG.Checked = false;
                        Respiratorio.Checked = false;
                        EEG.Checked = false;
                    }
                    if ((bool)rowTblMontagem["Padrao"])
                    {
                        MontagemPadrao.Checked = true;
                    }
                    else
                    {
                        MontagemPadrao.Checked = false;
                    }
                }
                var dt = GlobVar.tbl_MontCanal.AsEnumerable().Where(rw => rw.Field<int>("CodMontagem") == Convert.ToInt32(item.Tag)).CopyToDataTable();

                dataGridView1.Rows.Clear();


                foreach(DataRow dr in dt.Rows)
                {
                    //Canal
                    var rwCanal = GlobVar.tbl_CadCanal.AsEnumerable().Where(rw => rw.Field<int>("CodCanal") == Convert.ToInt32(dr["CodCanal1"])).FirstOrDefault();
                    string Canal = rwCanal["NomeCanal"].ToString();

                    //Referencia
                    string Ref = "";
                    if (Convert.ToInt32(dr["CodCanal2"]) != -1)
                    {
                        var rwRef = GlobVar.tbl_CadCanal.AsEnumerable().Where(rw => rw.Field<int>("CodCanal") == Convert.ToInt32(dr["CodCanal2"])).FirstOrDefault();
                        Ref = rwRef["NomeCanal"].ToString();
                    }

                    string titulo = dr["Legenda"].ToString();
                    int qtdAmostra = Convert.ToInt32(dr["QtdAmostras"]);

                    var rwTipoCan = GlobVar.tbl_CadTipoCanal.AsEnumerable().Where(rw => rw.Field<int>("CodTipo") == Convert.ToInt32(dr["CodTipoCanal"])).FirstOrDefault();
                    string TipoCanal = rwTipoCan["DescrTipo"].ToString();
                    int ampli = Convert.ToInt32(dr["AmplitudeMin"]);
                    bool inverteSinal = (bool)dr["InverteSinal"];
                    bool AutoEscala = (bool)dr["AutoEscala"];
                    string baixa = dr["PassaBaixa"] == DBNull.Value ? "" : $"{dr["PassaBaixa"]} Hz";
                    string alta = dr["PassaAlta"] == DBNull.Value ? "" : $"{dr["PassaAlta"]} Hz";
                    string notch = dr["Notch"] == DBNull.Value ? "" : $"{dr["Notch"]} Hz";

                    int rgb = Convert.ToInt32(dr["Cor"]);

                    dataGridView1.Rows.Add(Canal, Ref, titulo, qtdAmostra, TipoCanal, ampli, inverteSinal, AutoEscala, baixa, alta, notch, rgb);


                }

                dataGridView1.Rows[0].Selected = true;
                dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[0];
                dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, 0));
            }
            else
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < 50; i++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].HeaderCell.Value = $"";
                }

            }
        }
        private void ConfigurarDataGridView()
        {
            dataGridView1.Columns.Clear();

            // Canal (fixa e com largura fixa)
            var colCanal = new DataGridViewTextBoxColumn();
            colCanal.Name = "Canal";
            colCanal.HeaderText = "Canal";
            colCanal.Width = 60;
            colCanal.Frozen = true; // opcional, deixa "fixa"
            colCanal.ReadOnly = true;
            dataGridView1.Columns.Add(colCanal);

            // Demais colunas com preenchimento automático
            AdicionarColunaPreenchida("Referencia", "Referência");
            AdicionarColunaPreenchida("Titulo", "título");
            AdicionarColunaPreenchida("QtdAmostras", "Qtd Amostras");
            AdicionarColunaPreenchida("TipoCanal", "Tipo de canal");
            AdicionarColunaPreenchida("Amplitude", "Amplitude");

            var colInverteSinal = new DataGridViewCheckBoxColumn();
            colInverteSinal.Name = "InverteSinal";
            colInverteSinal.HeaderText = "Inverte sinal";
            colInverteSinal.Width = 75;
            colInverteSinal.ReadOnly = true; // ou false, se quiser editar
            dataGridView1.Columns.Add(colInverteSinal);

            var colAutoEscala = new DataGridViewCheckBoxColumn();
            colAutoEscala.Name = "AutoEscala";
            colAutoEscala.HeaderText = "Auto escala";
            colAutoEscala.Width = 75;
            colAutoEscala.ReadOnly = true;
            dataGridView1.Columns.Add(colAutoEscala);

            AdicionarColunaPreenchida("PassaBaixa", "Passa Baixa");
            AdicionarColunaPreenchida("PassaAlta", "Passa alta");
            AdicionarColunaPreenchida("Notch", "Notch");
            AdicionarColunaPreenchida("Cor", "Cor");
            // No final da configuração:


            // Estilo e comportamento
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false; // sem a seta lateral
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellClick += dataGridView1_CellClick;

            dataGridView1.Rows.Clear();
            for (int i = 0; i < 50; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = $"";
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Cor" && e.Value != null)
            {
                int corInt;
                if (int.TryParse(e.Value.ToString(), out corInt))
                {
                    // Converte de INT para cor RGB
                    int b = (corInt >> 16) & 0xFF;
                    int g = (corInt >> 8) & 0xFF;
                    int r = corInt & 0xFF;

                    var color = System.Drawing.Color.FromArgb(r, g, b);
                    e.CellStyle.BackColor = color;
                    e.CellStyle.ForeColor = (color.GetBrightness() < 0.5f) ? Color.White : Color.Black;
                    e.Value = ""; // Esconde o número, deixa só a cor aparecer
                    e.FormattingApplied = true;
                }
            }
        }
        private void AdicionarColunaPreenchida(string name, string header)
        {
            var col = new DataGridViewTextBoxColumn();
            col.Name = name;
            col.HeaderText = header;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(col);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Garante que não foi o header (índice -1)
            if (e.RowIndex >= 0)
            {
                // Acesso à linha selecionada
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                TituloBox.Text = row.Cells["Titulo"].Value?.ToString();

                // 1. Lê o valor da célula (pode ser null)
                var cellValue = row.Cells["InverteSinal"].Value;

                // 2. Converte de forma segura para bool
                bool isChecked = false;
                if (cellValue != null)
                {
                    // Caso seja já bool:
                    if (cellValue is bool)
                        isChecked = (bool)cellValue;
                    // Caso seja int
                    else if (cellValue is int)
                        isChecked = ((int)cellValue) != 0;
                    // Caso seja string
                    else if (cellValue is string)
                        isChecked = cellValue.ToString().ToLower() == "true" || cellValue.ToString() == "1";
                }

                // 3. Marca/desmarca o CheckBox
                invertSinal.Checked = isChecked;

                // 1. Lê o valor da célula (pode ser null)
                cellValue = row.Cells["AutoEscala"].Value;

                // 2. Converte de forma segura para bool
                isChecked = false;
                if (cellValue != null)
                {
                    // Caso seja já bool:
                    if (cellValue is bool)
                        isChecked = (bool)cellValue;
                    // Caso seja int
                    else if (cellValue is int)
                        isChecked = ((int)cellValue) != 0;
                    // Caso seja string
                    else if (cellValue is string)
                        isChecked = cellValue.ToString().ToLower() == "true" || cellValue.ToString() == "1";
                }

                // 3. Marca/desmarca o CheckBox
                AutoEscala.Checked = isChecked;

                // Supondo que você esteja no evento CellClick/SelectionChanged
                var corValue = row.Cells["Cor"].Value;

                Color color = Color.Transparent; // Valor padrão caso dê erro

                if (corValue != null)
                {
                    int corInt;
                    if (int.TryParse(corValue.ToString(), out corInt))
                    {
                        // Converte de INT para cor RGB
                        int b = (corInt >> 16) & 0xFF;
                        int g = (corInt >> 8) & 0xFF;
                        int r = corInt & 0xFF;

                        color = System.Drawing.Color.FromArgb(r, g, b);
                    }                    
                }

                // Atribui à cor do botão:
                ButColor.BackColor = color;

                AmplitudeBox.SelectedIndex = AmplitudeBox.Items.IndexOf(Convert.ToInt32(row.Cells["Amplitude"].Value));

                int baixa = 0;
                string passaBaixaStr = row.Cells["PassaBaixa"].Value?.ToString() ?? "";

                Match m = Regex.Match(passaBaixaStr, @"\d+");
                if (m.Success)
                    pBaixaBox.Text = $"{int.Parse(m.Value)}";

                // Repita para PassaAlta e Notch
                int alta = 0, notch = 0;
                string passaAltaStr = row.Cells["PassaAlta"].Value?.ToString() ?? "";
                string notchStr = row.Cells["Notch"].Value?.ToString() ?? "";

                Match mAlta = Regex.Match(passaAltaStr, @"\d+");
                if (mAlta.Success)
                    pAltaBox.Text = $"{int.Parse(mAlta.Value)}";

                Match mNotch = Regex.Match(notchStr, @"\d+");
                if (mNotch.Success)
                    NotchBox.Text = $"{int.Parse(mNotch.Value)}";

                var canalValue = row.Cells["Canal"].Value?.ToString();
                int idx = 0;
                for (int i = 0; i < CanalBox.Items.Count; i++)
                {
                    var item = CanalBox.Items[i] as ComboItem;
                    if (item != null && item.Texto.Equals(canalValue))
                    {
                        idx = i;
                        break;
                    }
                }
                CanalBox.SelectedIndex = idx;

                var refValue = row.Cells["Referencia"].Value?.ToString();
                idx = 0;
                for (int i = 0; i < RefBox.Items.Count; i++)
                {
                    var item = RefBox.Items[i] as ComboItem;
                    if (item != null && item.Texto.Equals(refValue))
                    {
                        idx = i;
                        break;
                    }
                }
                RefBox.SelectedIndex = idx;

                var TipocanalValue = row.Cells["TipoCanal"].Value?.ToString();
                idx = 0;
                for (int i = 0; i < TipoCanBox.Items.Count; i++)
                {
                    var item = TipoCanBox.Items[i] as ComboItem;
                    if (item != null && item.Texto.Equals(TipocanalValue))
                    {
                        idx = i;
                        break;
                    }
                }
                TipoCanBox.SelectedIndex = idx;

                int taxa = 0;
                int.TryParse(row.Cells["QtdAmostras"].Value?.ToString(), out taxa);

                TaxaBox.SelectedIndex = TaxaBox.Items.IndexOf(taxa);
                /*
                // Exemplo: pegar o valor da coluna "Nome"
                var nome = row.Cells["Canal"].Value?.ToString();

                // Exemplo: pegar outros valores
                var cor = row.Cells["Cor"].Value; // Pode ser int ou string, dependendo do tipo da coluna

                // Aqui você pode usar as informações como quiser:
                MessageBox.Show($"Linha selecionada: Nome = {nome}, Cor = {cor}");
                */
            }
        }
        private void ConfigurarTooltips(Control container)
        {
            foreach (Control controle in container.Controls)
            {
                if (controle is RadioButton botao)
                {
                    // Adiciona os eventos para exibir e esconder o tooltip
                    botao.MouseEnter += (s, e) => MostrarTooltip(botao);
                    botao.MouseLeave += (s, e) => EsconderTooltip();
                }
            }
        }
        private void MostrarTooltip(RadioButton botao)
        {
            // Configura o conteúdo e a posição do tooltip
            NomesCanais.Show(botao.Name, botao, 0, botao.Height); // Dura 2 segundos
        }
        private void EsconderTooltip()
        {
            NomesCanais.Hide(panelImagem); // Esconde o tooltip do painel principal
        }
        private float[] rgb = new float[3];
        public static int rgbNumero;
        private void ButColor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorPickerDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Color corEscolhida = dialog.Color;
                    rgbNumero = corEscolhida.R | (corEscolhida.G << 8) | (corEscolhida.B << 16);

                    rgb[0] = corEscolhida.R / 255f;
                    rgb[1] = corEscolhida.G / 255f;
                    rgb[2] = corEscolhida.B / 255f;

                    ButColor.BackColor = corEscolhida;
                }
            }
        }
        private void btnCima_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var rowIndex = dataGridView1.SelectedRows[0].Index;
            if (rowIndex == 0) return; // já está no topo!

            // Copie o conteúdo da linha selecionada
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            DataGridViewRow rowCima = dataGridView1.Rows[rowIndex - 1];

            // Troque valores de cada célula
            for (int i = 0; i < row.Cells.Count; i++)
            {
                var temp = rowCima.Cells[i].Value;
                rowCima.Cells[i].Value = row.Cells[i].Value;
                row.Cells[i].Value = temp;
            }

            // Atualize a seleção visual
            dataGridView1.ClearSelection();
            dataGridView1.Rows[rowIndex - 1].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex - 1].Cells[0];
            dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, rowIndex - 1));
        }
        private void btnBaixo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            var rowIndex = dataGridView1.SelectedRows[0].Index;
            if (rowIndex >= dataGridView1.Rows.Count - 1) return; // já está na última linha

            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            DataGridViewRow rowBaixo = dataGridView1.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                var temp = rowBaixo.Cells[i].Value;
                rowBaixo.Cells[i].Value = row.Cells[i].Value;
                row.Cells[i].Value = temp;
            }

            dataGridView1.ClearSelection();
            dataGridView1.Rows[rowIndex + 1].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex + 1].Cells[0];
            dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, rowIndex + 1));
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            // Verifica se há uma linha selecionada (exceto a linha "nova" no final, caso AllowUserToAddRows=true)
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Evita tentar deletar a "linha fantasma" de inclusão de novo registro
                if (dataGridView1.AllowUserToAddRows && rowIndex == dataGridView1.Rows.Count - 1)
                {
                    MessageBox.Show("Selecione uma linha válida para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    "Deseja realmente excluir esta linha?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(rowIndex);

                    // Seleciona a próxima linha automaticamente (caso exista)
                    if (dataGridView1.Rows.Count > 0)
                    {
                        int nextIndex = Math.Min(rowIndex, dataGridView1.Rows.Count - (dataGridView1.AllowUserToAddRows ? 2 : 1));
                        if (nextIndex >= 0)
                        {
                            dataGridView1.Rows[nextIndex].Selected = true;
                            dataGridView1.CurrentCell = dataGridView1.Rows[nextIndex].Cells[0];
                            dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, nextIndex));
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void limpar_Click(object sender, EventArgs e)
        {
            var selecionados = panelImagem.Controls
                                    .OfType<RadioButton>()
                                    .Where(r => r.Checked)
                                    .ToList();

            // Desmarca todos e marca só o último clicado
            foreach (var r in selecionados) r.Checked = false;


            RefBox.Text = "";
            TituloBox.Text = "";
            TipoCanBox.SelectedIndex = 0;
            TaxaBox.SelectedIndex = 0;
            pBaixaBox.Text = "";
            pAltaBox.Text = "";
            NotchBox.SelectedIndex = 0;

            AmplitudeBox.SelectedIndex = 0;
            invertSinal.Checked = false;
            AutoEscala.Checked = false;
            LMInferior.Text = "";
            LMSuperior.Text = "";

            CanalBox.Text = "";
            CanalBox.Focus();

        }

        private void alterar_Click(object sender, EventArgs e)
        {
            // Garante que uma linha está selecionada
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma linha para alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = dataGridView1.SelectedRows[0].Index;

            // Ignora a linha de inclusão automática
            if (dataGridView1.AllowUserToAddRows && rowIndex == dataGridView1.Rows.Count - 1)
            {
                MessageBox.Show("Selecione uma linha válida para alterar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Monta o valor ARGB manualmente, usando os componentes corretos
            Color cor = ButColor.BackColor;
            int a = cor.A;
            int r = cor.B;
            int g = cor.G;
            int b = cor.R;
            int valorEmInt = (a << 24) | (r << 16) | (g << 8) | b;

            // Atualiza as células da linha selecionada
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            row.Cells[0].Value = CanalBox.Text;
            row.Cells[1].Value = RefBox.Text;
            row.Cells[2].Value = TituloBox.Text;
            row.Cells[3].Value = TaxaBox.Text;
            row.Cells[4].Value = TipoCanBox.Text;
            row.Cells[5].Value = AmplitudeBox.Text;
            row.Cells[6].Value = invertSinal.Checked;
            row.Cells[7].Value = AutoEscala.Checked;
            row.Cells[8].Value = pBaixaBox.Text;
            row.Cells[9].Value = pAltaBox.Text;
            row.Cells[10].Value = NotchBox.Text;
            row.Cells[11].Value = valorEmInt;

            // Seleciona de novo a linha alterada (opcional, mas pode ajudar na navegação)
            dataGridView1.ClearSelection();
            row.Selected = true;
            dataGridView1.CurrentCell = row.Cells[0];
            //dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, rowIndex));
        }
        private void incluir_Click(Object sender, EventArgs e)
        {
            Color cor = ButColor.BackColor;
            int a = cor.A;
            int r = cor.B;
            int g = cor.G;
            int b = cor.R;
            int valorEmInt = (a << 24) | (r << 16) | (g << 8) | b; 
            
            dataGridView1.Rows.Add(CanalBox.Text, RefBox.Text, TituloBox.Text, TaxaBox.Text, TipoCanBox.Text, AmplitudeBox.Text, invertSinal.Checked, AutoEscala.Checked, pBaixaBox.Text, pAltaBox.Text, NotchBox.Text, valorEmInt);
            int rowIndex = dataGridView1.Rows.Count - 1;
            dataGridView1.ClearSelection();
            dataGridView1.Rows[rowIndex].Selected = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[0];
            //dataGridView1_CellClick(dataGridView1, new DataGridViewCellEventArgs(0, rowIndex));
        }





        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}


public class ComboItem
{
    public string Texto { get; set; }
    public object Tag { get; set; }

    public ComboItem(string texto, object tag)
    {
        Texto = texto;
        Tag = tag;
    }

    // Isso define o que aparece no ComboBox (texto)
    public override string ToString()
    {
        return Texto;
    }
};