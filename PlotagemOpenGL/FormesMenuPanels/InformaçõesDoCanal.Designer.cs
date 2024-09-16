using MetroFramework.Forms;
using PlotagemOpenGL.auxi;
namespace PlotagemOpenGL.FormesMenuPanels
{
    partial class InformaçõesDoCanal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InformaçõesDoCanal));
            Canal = new System.Windows.Forms.GroupBox();
            textAmostragem = new System.Windows.Forms.TextBox();
            textTipoDeCanal = new System.Windows.Forms.TextBox();
            textNome = new System.Windows.Forms.TextBox();
            Amostragem = new System.Windows.Forms.Label();
            TipoDeCanal = new System.Windows.Forms.Label();
            Nome = new System.Windows.Forms.Label();
            Filtros = new System.Windows.Forms.GroupBox();
            HzNotch = new System.Windows.Forms.Label();
            HzAlta = new System.Windows.Forms.Label();
            HzBaixa = new System.Windows.Forms.Label();
            comboNotch = new System.Windows.Forms.ComboBox();
            comboPassaAlta = new System.Windows.Forms.ComboBox();
            comboPassaBaixa = new System.Windows.Forms.ComboBox();
            Notch = new System.Windows.Forms.Label();
            PassaAlta = new System.Windows.Forms.Label();
            PassaBaixa = new System.Windows.Forms.Label();
            Display = new System.Windows.Forms.GroupBox();
            NumerosPosi = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            Gráfico = new System.Windows.Forms.RadioButton();
            Números = new System.Windows.Forms.RadioButton();
            GraficoeNumeros = new System.Windows.Forms.RadioButton();
            groupBox1 = new System.Windows.Forms.GroupBox();
            Vertical = new System.Windows.Forms.RadioButton();
            Horizontal = new System.Windows.Forms.RadioButton();
            Posicao = new System.Windows.Forms.GroupBox();
            Configurar = new System.Windows.Forms.RadioButton();
            GráficoSetas = new System.Windows.Forms.RadioButton();
            MostrarSetas = new System.Windows.Forms.RadioButton();
            Grafico = new System.Windows.Forms.GroupBox();
            boxReferencia = new System.Windows.Forms.ComboBox();
            Referencia = new System.Windows.Forms.Label();
            AutoEscala = new System.Windows.Forms.CheckBox();
            InverteSinal = new System.Windows.Forms.CheckBox();
            comboAmplitude = new System.Windows.Forms.ComboBox();
            textLegenda = new System.Windows.Forms.TextBox();
            Cor = new System.Windows.Forms.GroupBox();
            trackBarB = new System.Windows.Forms.TrackBar();
            trackBarG = new System.Windows.Forms.TrackBar();
            trackBarR = new System.Windows.Forms.TrackBar();
            Color = new System.Windows.Forms.Button();
            numericUpDownAzul = new System.Windows.Forms.NumericUpDown();
            numericUpDownVerde = new System.Windows.Forms.NumericUpDown();
            numericUpDownVermelho = new System.Windows.Forms.NumericUpDown();
            Azul = new System.Windows.Forms.Label();
            Verde = new System.Windows.Forms.Label();
            Vermelho = new System.Windows.Forms.Label();
            Limites = new System.Windows.Forms.GroupBox();
            numericUpDownLmInf = new System.Windows.Forms.NumericUpDown();
            numericUpDownLmSup = new System.Windows.Forms.NumericUpDown();
            LimiteInferior = new System.Windows.Forms.Label();
            LimiteSuperior = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            Confirmar = new System.Windows.Forms.Button();
            Aplicar = new System.Windows.Forms.Button();
            Cancelar = new System.Windows.Forms.Button();
            Canal.SuspendLayout();
            Filtros.SuspendLayout();
            Display.SuspendLayout();
            NumerosPosi.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            Posicao.SuspendLayout();
            Grafico.SuspendLayout();
            Cor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAzul).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVerde).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVermelho).BeginInit();
            Limites.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLmInf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLmSup).BeginInit();
            SuspendLayout();
            // 
            // Canal
            // 
            Canal.Controls.Add(textAmostragem);
            Canal.Controls.Add(textTipoDeCanal);
            Canal.Controls.Add(textNome);
            Canal.Controls.Add(Amostragem);
            Canal.Controls.Add(TipoDeCanal);
            Canal.Controls.Add(Nome);
            Canal.Font = new System.Drawing.Font("Arial", 9F);
            Canal.Location = new System.Drawing.Point(11, 22);
            Canal.Name = "Canal";
            Canal.Size = new System.Drawing.Size(354, 203);
            Canal.TabIndex = 0;
            Canal.TabStop = false;
            Canal.Text = "Canal";
            // 
            // textAmostragem
            // 
            textAmostragem.BackColor = System.Drawing.Color.GhostWhite;
            textAmostragem.HideSelection = false;
            textAmostragem.Location = new System.Drawing.Point(138, 136);
            textAmostragem.Name = "textAmostragem";
            textAmostragem.ReadOnly = true;
            textAmostragem.Size = new System.Drawing.Size(191, 25);
            textAmostragem.TabIndex = 5;
            textAmostragem.TabStop = false;
            textAmostragem.WordWrap = false;
            textAmostragem.MouseDown += textBox1_MouseDown;
            textAmostragem.MouseEnter += textBox1_Enter;
            // 
            // textTipoDeCanal
            // 
            textTipoDeCanal.BackColor = System.Drawing.Color.GhostWhite;
            textTipoDeCanal.HideSelection = false;
            textTipoDeCanal.Location = new System.Drawing.Point(138, 84);
            textTipoDeCanal.Name = "textTipoDeCanal";
            textTipoDeCanal.ReadOnly = true;
            textTipoDeCanal.Size = new System.Drawing.Size(191, 25);
            textTipoDeCanal.TabIndex = 4;
            textTipoDeCanal.TabStop = false;
            textTipoDeCanal.WordWrap = false;
            textTipoDeCanal.MouseDown += textBox1_MouseDown;
            textTipoDeCanal.MouseEnter += textBox1_Enter;
            // 
            // textNome
            // 
            textNome.BackColor = System.Drawing.Color.GhostWhite;
            textNome.HideSelection = false;
            textNome.Location = new System.Drawing.Point(138, 35);
            textNome.Name = "textNome";
            textNome.ReadOnly = true;
            textNome.Size = new System.Drawing.Size(191, 25);
            textNome.TabIndex = 3;
            textNome.TabStop = false;
            textNome.WordWrap = false;
            textNome.MouseDown += textBox1_MouseDown;
            textNome.MouseEnter += textBox1_Enter;
            // 
            // Amostragem
            // 
            Amostragem.AutoSize = true;
            Amostragem.Location = new System.Drawing.Point(15, 138);
            Amostragem.Name = "Amostragem";
            Amostragem.Size = new System.Drawing.Size(92, 17);
            Amostragem.TabIndex = 2;
            Amostragem.Text = "Amostragem";
            // 
            // TipoDeCanal
            // 
            TipoDeCanal.AutoSize = true;
            TipoDeCanal.Location = new System.Drawing.Point(15, 86);
            TipoDeCanal.Name = "TipoDeCanal";
            TipoDeCanal.Size = new System.Drawing.Size(97, 17);
            TipoDeCanal.TabIndex = 1;
            TipoDeCanal.Text = "Tipo de Canal";
            // 
            // Nome
            // 
            Nome.AutoSize = true;
            Nome.Location = new System.Drawing.Point(15, 37);
            Nome.Name = "Nome";
            Nome.Size = new System.Drawing.Size(47, 17);
            Nome.TabIndex = 0;
            Nome.Text = "Nome";
            // 
            // Filtros
            // 
            Filtros.Controls.Add(HzNotch);
            Filtros.Controls.Add(HzAlta);
            Filtros.Controls.Add(HzBaixa);
            Filtros.Controls.Add(comboNotch);
            Filtros.Controls.Add(comboPassaAlta);
            Filtros.Controls.Add(comboPassaBaixa);
            Filtros.Controls.Add(Notch);
            Filtros.Controls.Add(PassaAlta);
            Filtros.Controls.Add(PassaBaixa);
            Filtros.Font = new System.Drawing.Font("Arial", 9F);
            Filtros.Location = new System.Drawing.Point(11, 231);
            Filtros.Name = "Filtros";
            Filtros.Size = new System.Drawing.Size(354, 169);
            Filtros.TabIndex = 1;
            Filtros.TabStop = false;
            Filtros.Text = "Filtros";
            // 
            // HzNotch
            // 
            HzNotch.AutoSize = true;
            HzNotch.Location = new System.Drawing.Point(263, 122);
            HzNotch.Name = "HzNotch";
            HzNotch.Size = new System.Drawing.Size(26, 17);
            HzNotch.TabIndex = 12;
            HzNotch.Text = "Hz";
            // 
            // HzAlta
            // 
            HzAlta.AutoSize = true;
            HzAlta.Location = new System.Drawing.Point(263, 83);
            HzAlta.Name = "HzAlta";
            HzAlta.Size = new System.Drawing.Size(26, 17);
            HzAlta.TabIndex = 11;
            HzAlta.Text = "Hz";
            // 
            // HzBaixa
            // 
            HzBaixa.AutoSize = true;
            HzBaixa.Location = new System.Drawing.Point(263, 44);
            HzBaixa.Name = "HzBaixa";
            HzBaixa.Size = new System.Drawing.Size(26, 17);
            HzBaixa.TabIndex = 10;
            HzBaixa.Text = "Hz";
            // 
            // comboNotch
            // 
            comboNotch.FormattingEnabled = true;
            comboNotch.Items.AddRange(new object[] { 50, 60 });
            comboNotch.Location = new System.Drawing.Point(138, 119);
            comboNotch.Name = "comboNotch";
            comboNotch.Size = new System.Drawing.Size(119, 25);
            comboNotch.TabIndex = 0;
            comboNotch.TabStop = false;
            //comboNotch.TabIndexChanged += ComboNotch_TabIndexChanged;
            comboNotch.TextChanged += ComboNotch_TabIndexChanged;
            // 
            // comboPassaAlta
            // 
            comboPassaAlta.FormattingEnabled = true;
            comboPassaAlta.Items.AddRange(new object[] { 75, 65, 40, 10, 7, 5, 3, 1, 0.5D, 0.3D, 0.1D });
            comboPassaAlta.Location = new System.Drawing.Point(138, 80);
            comboPassaAlta.Name = "comboPassaAlta";
            comboPassaAlta.Size = new System.Drawing.Size(119, 25);
            comboPassaAlta.TabIndex = 0;
            comboPassaAlta.TabStop = false;
            //comboPassaAlta.TabIndexChanged += ComboPassaAlta_TabIndexChanged;
            comboPassaAlta.TextChanged += ComboPassaAlta_TabIndexChanged;
            // 
            // comboPassaBaixa
            // 
            comboPassaBaixa.FormattingEnabled = true;
            comboPassaBaixa.Items.AddRange(new object[] { 100, 70, 60, 50, 40, 35, 30, 20, 15, 10, 5, 3 });
            comboPassaBaixa.Location = new System.Drawing.Point(138, 41);
            comboPassaBaixa.Name = "comboPassaBaixa";
            comboPassaBaixa.Size = new System.Drawing.Size(119, 25);
            comboPassaBaixa.TabIndex = 0;
            comboPassaBaixa.TabStop = false;
            //comboPassaBaixa.TabIndexChanged += ComboPassaBaixa_TabIndexChanged;
            comboPassaBaixa.TextChanged += ComboPassaBaixa_TabIndexChanged;
            // 
            // Notch
            // 
            Notch.AutoSize = true;
            Notch.Location = new System.Drawing.Point(15, 122);
            Notch.Name = "Notch";
            Notch.Size = new System.Drawing.Size(46, 17);
            Notch.TabIndex = 3;
            Notch.Text = "Notch";
            // 
            // PassaAlta
            // 
            PassaAlta.AutoSize = true;
            PassaAlta.Location = new System.Drawing.Point(15, 83);
            PassaAlta.Name = "PassaAlta";
            PassaAlta.Size = new System.Drawing.Size(77, 17);
            PassaAlta.TabIndex = 2;
            PassaAlta.Text = "Passa Alta";
            // 
            // PassaBaixa
            // 
            PassaBaixa.AutoSize = true;
            PassaBaixa.Location = new System.Drawing.Point(15, 44);
            PassaBaixa.Name = "PassaBaixa";
            PassaBaixa.Size = new System.Drawing.Size(90, 17);
            PassaBaixa.TabIndex = 1;
            PassaBaixa.Text = "Passa Baixa";
            // 
            // Display
            // 
            Display.Controls.Add(NumerosPosi);
            Display.Controls.Add(Posicao);
            Display.Controls.Add(Grafico);
            Display.Controls.Add(comboAmplitude);
            Display.Controls.Add(textLegenda);
            Display.Controls.Add(Cor);
            Display.Controls.Add(Limites);
            Display.Controls.Add(label8);
            Display.Controls.Add(label7);
            Display.Font = new System.Drawing.Font("Arial", 9F);
            Display.Location = new System.Drawing.Point(387, 22);
            Display.Name = "Display";
            Display.Size = new System.Drawing.Size(352, 378);
            Display.TabIndex = 2;
            Display.TabStop = false;
            Display.Text = "Display";
            // 
            // NumerosPosi
            // 
            NumerosPosi.Controls.Add(groupBox2);
            NumerosPosi.Controls.Add(groupBox1);
            NumerosPosi.Location = new System.Drawing.Point(17, 110);
            NumerosPosi.Name = "NumerosPosi";
            NumerosPosi.Size = new System.Drawing.Size(321, 113);
            NumerosPosi.TabIndex = 10;
            NumerosPosi.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(Gráfico);
            groupBox2.Controls.Add(Números);
            groupBox2.Controls.Add(GraficoeNumeros);
            groupBox2.Location = new System.Drawing.Point(9, 13);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(162, 94);
            groupBox2.TabIndex = 11;
            groupBox2.TabStop = false;
            // 
            // Gráfico
            // 
            Gráfico.AutoSize = true;
            Gráfico.Font = new System.Drawing.Font("Arial", 7F);
            Gráfico.Location = new System.Drawing.Point(12, 14);
            Gráfico.Name = "Gráfico";
            Gráfico.Size = new System.Drawing.Size(67, 19);
            Gráfico.TabIndex = 7;
            Gráfico.TabStop = true;
            Gráfico.Text = "Gráfico";
            Gráfico.UseVisualStyleBackColor = true;
            // 
            // Números
            // 
            Números.AutoSize = true;
            Números.Font = new System.Drawing.Font("Arial", 7F);
            Números.Location = new System.Drawing.Point(12, 44);
            Números.Name = "Números";
            Números.Size = new System.Drawing.Size(80, 19);
            Números.TabIndex = 8;
            Números.TabStop = true;
            Números.Text = "Números";
            Números.UseVisualStyleBackColor = true;
            // 
            // GraficoeNumeros
            // 
            GraficoeNumeros.AutoSize = true;
            GraficoeNumeros.Font = new System.Drawing.Font("Arial", 7F);
            GraficoeNumeros.Location = new System.Drawing.Point(12, 73);
            GraficoeNumeros.Name = "GraficoeNumeros";
            GraficoeNumeros.Size = new System.Drawing.Size(132, 19);
            GraficoeNumeros.TabIndex = 9;
            GraficoeNumeros.TabStop = true;
            GraficoeNumeros.Text = "Gráfico e Números";
            GraficoeNumeros.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Vertical);
            groupBox1.Controls.Add(Horizontal);
            groupBox1.Location = new System.Drawing.Point(183, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(118, 94);
            groupBox1.TabIndex = 10;
            groupBox1.TabStop = false;
            // 
            // Vertical
            // 
            Vertical.AutoSize = true;
            Vertical.Font = new System.Drawing.Font("Arial", 7F);
            Vertical.Location = new System.Drawing.Point(11, 14);
            Vertical.Name = "Vertical";
            Vertical.Size = new System.Drawing.Size(67, 19);
            Vertical.TabIndex = 9;
            Vertical.TabStop = true;
            Vertical.Text = "Vertical";
            Vertical.UseVisualStyleBackColor = true;
            // 
            // Horizontal
            // 
            Horizontal.AutoSize = true;
            Horizontal.Font = new System.Drawing.Font("Arial", 7F);
            Horizontal.Location = new System.Drawing.Point(11, 39);
            Horizontal.Name = "Horizontal";
            Horizontal.Size = new System.Drawing.Size(83, 19);
            Horizontal.TabIndex = 8;
            Horizontal.TabStop = true;
            Horizontal.Text = "Horizontal";
            Horizontal.UseVisualStyleBackColor = true;
            // 
            // Posicao
            // 
            Posicao.Controls.Add(Configurar);
            Posicao.Controls.Add(GráficoSetas);
            Posicao.Controls.Add(MostrarSetas);
            Posicao.Location = new System.Drawing.Point(17, 110);
            Posicao.Name = "Posicao";
            Posicao.Size = new System.Drawing.Size(321, 113);
            Posicao.TabIndex = 11;
            Posicao.TabStop = false;
            // 
            // Configurar
            // 
            Configurar.AutoSize = true;
            Configurar.Font = new System.Drawing.Font("Arial", 7F);
            Configurar.Location = new System.Drawing.Point(37, 83);
            Configurar.Name = "Configurar";
            Configurar.Size = new System.Drawing.Size(86, 19);
            Configurar.TabIndex = 9;
            Configurar.TabStop = true;
            Configurar.Text = "Configurar";
            Configurar.UseVisualStyleBackColor = true;
            // 
            // GráficoSetas
            // 
            GráficoSetas.AutoSize = true;
            GráficoSetas.Font = new System.Drawing.Font("Arial", 7F);
            GráficoSetas.Location = new System.Drawing.Point(37, 54);
            GráficoSetas.Name = "GráficoSetas";
            GráficoSetas.Size = new System.Drawing.Size(67, 19);
            GráficoSetas.TabIndex = 8;
            GráficoSetas.TabStop = true;
            GráficoSetas.Text = "Gráfico";
            GráficoSetas.UseVisualStyleBackColor = true;
            // 
            // MostrarSetas
            // 
            MostrarSetas.AutoSize = true;
            MostrarSetas.Font = new System.Drawing.Font("Arial", 7F);
            MostrarSetas.Location = new System.Drawing.Point(37, 24);
            MostrarSetas.Name = "MostrarSetas";
            MostrarSetas.Size = new System.Drawing.Size(104, 19);
            MostrarSetas.TabIndex = 7;
            MostrarSetas.TabStop = true;
            MostrarSetas.Text = "Mostrar Setas";
            MostrarSetas.UseVisualStyleBackColor = true;
            // 
            // Grafico
            // 
            Grafico.Controls.Add(boxReferencia);
            Grafico.Controls.Add(Referencia);
            Grafico.Controls.Add(AutoEscala);
            Grafico.Controls.Add(InverteSinal);
            Grafico.Location = new System.Drawing.Point(17, 110);
            Grafico.Name = "Grafico";
            Grafico.Size = new System.Drawing.Size(321, 113);
            Grafico.TabIndex = 11;
            Grafico.TabStop = false;
            // 
            // boxReferencia
            // 
            boxReferencia.FormattingEnabled = true;
            boxReferencia.Location = new System.Drawing.Point(119, 18);
            boxReferencia.Name = "boxReferencia";
            boxReferencia.Size = new System.Drawing.Size(58, 25);
            boxReferencia.TabIndex = 8;
            // 
            // Referencia
            // 
            Referencia.AutoSize = true;
            Referencia.Location = new System.Drawing.Point(9, 21);
            Referencia.Name = "Referencia";
            Referencia.Size = new System.Drawing.Size(79, 17);
            Referencia.TabIndex = 7;
            Referencia.Text = "Referencia";
            // 
            // AutoEscala
            // 
            AutoEscala.AutoSize = true;
            AutoEscala.Location = new System.Drawing.Point(198, 53);
            AutoEscala.Name = "AutoEscala";
            AutoEscala.Size = new System.Drawing.Size(108, 21);
            AutoEscala.TabIndex = 1;
            AutoEscala.Text = "Auto Escala";
            AutoEscala.UseVisualStyleBackColor = true;
            // 
            // InverteSinal
            // 
            InverteSinal.AutoSize = true;
            InverteSinal.Location = new System.Drawing.Point(9, 55);
            InverteSinal.Name = "InverteSinal";
            InverteSinal.Size = new System.Drawing.Size(109, 21);
            InverteSinal.TabIndex = 0;
            InverteSinal.Text = "Inverte Sinal";
            InverteSinal.UseVisualStyleBackColor = true;
            // 
            // comboAmplitude
            // 
            comboAmplitude.FormattingEnabled = true;
            comboAmplitude.Location = new System.Drawing.Point(136, 79);
            comboAmplitude.Name = "comboAmplitude";
            comboAmplitude.Size = new System.Drawing.Size(98, 25);
            comboAmplitude.TabIndex = 6;
            comboAmplitude.TabStop = false;
            // 
            // textLegenda
            // 
            textLegenda.Location = new System.Drawing.Point(136, 30);
            textLegenda.Name = "textLegenda";
            textLegenda.Size = new System.Drawing.Size(211, 25);
            textLegenda.TabIndex = 5;
            textLegenda.TabStop = false;
            // 
            // Cor
            // 
            Cor.Controls.Add(trackBarB);
            Cor.Controls.Add(trackBarG);
            Cor.Controls.Add(trackBarR);
            Cor.Controls.Add(Color);
            Cor.Controls.Add(numericUpDownAzul);
            Cor.Controls.Add(numericUpDownVerde);
            Cor.Controls.Add(numericUpDownVermelho);
            Cor.Controls.Add(Azul);
            Cor.Controls.Add(Verde);
            Cor.Controls.Add(Vermelho);
            Cor.Location = new System.Drawing.Point(6, 226);
            Cor.Name = "Cor";
            Cor.Size = new System.Drawing.Size(187, 146);
            Cor.TabIndex = 3;
            Cor.TabStop = false;
            Cor.Text = "Cor";
            // 
            // trackBarB
            // 
            trackBarB.AutoSize = false;
            trackBarB.Location = new System.Drawing.Point(36, 102);
            trackBarB.Maximum = 255;
            trackBarB.Name = "trackBarB";
            trackBarB.Size = new System.Drawing.Size(100, 34);
            trackBarB.TabIndex = 10;
            // 
            // trackBarG
            // 
            trackBarG.AutoSize = false;
            trackBarG.Location = new System.Drawing.Point(36, 64);
            trackBarG.Maximum = 255;
            trackBarG.Name = "trackBarG";
            trackBarG.Size = new System.Drawing.Size(100, 34);
            trackBarG.TabIndex = 9;
            // 
            // trackBarR
            // 
            trackBarR.AutoSize = false;
            trackBarR.Location = new System.Drawing.Point(36, 30);
            trackBarR.Maximum = 255;
            trackBarR.Name = "trackBarR";
            trackBarR.Size = new System.Drawing.Size(100, 34);
            trackBarR.TabIndex = 0;
            // 
            // Color
            // 
            Color.Location = new System.Drawing.Point(142, 32);
            Color.Name = "Color";
            Color.Size = new System.Drawing.Size(40, 105);
            Color.TabIndex = 8;
            Color.UseVisualStyleBackColor = true;
            // 
            // numericUpDownAzul
            // 
            numericUpDownAzul.Location = new System.Drawing.Point(89, 111);
            numericUpDownAzul.Name = "numericUpDownAzul";
            numericUpDownAzul.Size = new System.Drawing.Size(47, 25);
            numericUpDownAzul.TabIndex = 6;
            // 
            // numericUpDownVerde
            // 
            numericUpDownVerde.Location = new System.Drawing.Point(89, 72);
            numericUpDownVerde.Name = "numericUpDownVerde";
            numericUpDownVerde.Size = new System.Drawing.Size(47, 25);
            numericUpDownVerde.TabIndex = 5;
            // 
            // numericUpDownVermelho
            // 
            numericUpDownVermelho.Location = new System.Drawing.Point(89, 33);
            numericUpDownVermelho.Name = "numericUpDownVermelho";
            numericUpDownVermelho.Size = new System.Drawing.Size(47, 25);
            numericUpDownVermelho.TabIndex = 4;
            // 
            // Azul
            // 
            Azul.AutoSize = true;
            Azul.Location = new System.Drawing.Point(11, 113);
            Azul.Name = "Azul";
            Azul.Size = new System.Drawing.Size(18, 17);
            Azul.TabIndex = 3;
            Azul.Text = "B";
            // 
            // Verde
            // 
            Verde.AutoSize = true;
            Verde.Location = new System.Drawing.Point(11, 74);
            Verde.Name = "Verde";
            Verde.Size = new System.Drawing.Size(19, 17);
            Verde.TabIndex = 2;
            Verde.Text = "G";
            // 
            // Vermelho
            // 
            Vermelho.AutoSize = true;
            Vermelho.Location = new System.Drawing.Point(11, 35);
            Vermelho.Name = "Vermelho";
            Vermelho.Size = new System.Drawing.Size(19, 17);
            Vermelho.TabIndex = 1;
            Vermelho.Text = "R";
            // 
            // Limites
            // 
            Limites.Controls.Add(numericUpDownLmInf);
            Limites.Controls.Add(numericUpDownLmSup);
            Limites.Controls.Add(LimiteInferior);
            Limites.Controls.Add(LimiteSuperior);
            Limites.Location = new System.Drawing.Point(200, 226);
            Limites.Name = "Limites";
            Limites.Size = new System.Drawing.Size(146, 146);
            Limites.TabIndex = 4;
            Limites.TabStop = false;
            Limites.Text = "Limites";
            // 
            // numericUpDownLmInf
            // 
            numericUpDownLmInf.Location = new System.Drawing.Point(26, 106);
            numericUpDownLmInf.Maximum = new decimal(new int[] { 160, 0, 0, 0 });
            numericUpDownLmInf.Name = "numericUpDownLmInf";
            numericUpDownLmInf.Size = new System.Drawing.Size(57, 25);
            numericUpDownLmInf.TabIndex = 6;
            numericUpDownLmInf.TabStop = false;
            // 
            // numericUpDownLmSup
            // 
            numericUpDownLmSup.Location = new System.Drawing.Point(26, 53);
            numericUpDownLmSup.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numericUpDownLmSup.Name = "numericUpDownLmSup";
            numericUpDownLmSup.Size = new System.Drawing.Size(57, 25);
            numericUpDownLmSup.TabIndex = 5;
            numericUpDownLmSup.TabStop = false;
            // 
            // LimiteInferior
            // 
            LimiteInferior.AutoSize = true;
            LimiteInferior.Location = new System.Drawing.Point(22, 83);
            LimiteInferior.Name = "LimiteInferior";
            LimiteInferior.Size = new System.Drawing.Size(95, 17);
            LimiteInferior.TabIndex = 3;
            LimiteInferior.Text = "Limite Inferior";
            // 
            // LimiteSuperior
            // 
            LimiteSuperior.AutoSize = true;
            LimiteSuperior.Location = new System.Drawing.Point(22, 30);
            LimiteSuperior.Name = "LimiteSuperior";
            LimiteSuperior.Size = new System.Drawing.Size(106, 17);
            LimiteSuperior.TabIndex = 1;
            LimiteSuperior.Text = "Limite Superior";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(26, 82);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(72, 17);
            label8.TabIndex = 2;
            label8.Text = "Amplitude";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(26, 33);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(64, 17);
            label7.TabIndex = 1;
            label7.Text = "Legenda";
            // 
            // Confirmar
            // 
            Confirmar.Font = new System.Drawing.Font("Arial", 9F);
            Confirmar.Location = new System.Drawing.Point(11, 415);
            Confirmar.Name = "Confirmar";
            Confirmar.Size = new System.Drawing.Size(102, 35);
            Confirmar.TabIndex = 3;
            Confirmar.Text = "Confirmar";
            Confirmar.UseVisualStyleBackColor = true;
            // 
            // Aplicar
            // 
            Aplicar.Font = new System.Drawing.Font("Arial", 9F);
            Aplicar.Location = new System.Drawing.Point(326, 415);
            Aplicar.Name = "Aplicar";
            Aplicar.Size = new System.Drawing.Size(102, 35);
            Aplicar.TabIndex = 4;
            Aplicar.Text = "Aplicar";
            Aplicar.UseVisualStyleBackColor = true;
            // 
            // Cancelar
            // 
            Cancelar.Font = new System.Drawing.Font("Arial", 9F);
            Cancelar.Location = new System.Drawing.Point(637, 415);
            Cancelar.Name = "Cancelar";
            Cancelar.Size = new System.Drawing.Size(102, 35);
            Cancelar.TabIndex = 5;
            Cancelar.Text = "Cancelar";
            Cancelar.UseVisualStyleBackColor = true;
            // 
            // InformaçõesDoCanal
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.GhostWhite;
            ClientSize = new System.Drawing.Size(754, 469);
            Controls.Add(Cancelar);
            Controls.Add(Aplicar);
            Controls.Add(Confirmar);
            Controls.Add(Display);
            Controls.Add(Filtros);
            Controls.Add(Canal);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "InformaçõesDoCanal";
            Text = "Informações do Canal";
            Canal.ResumeLayout(false);
            Canal.PerformLayout();
            Filtros.ResumeLayout(false);
            Filtros.PerformLayout();
            Display.ResumeLayout(false);
            Display.PerformLayout();
            NumerosPosi.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            Posicao.ResumeLayout(false);
            Posicao.PerformLayout();
            Grafico.ResumeLayout(false);
            Grafico.PerformLayout();
            Cor.ResumeLayout(false);
            Cor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarB).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarG).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarR).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownAzul).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVerde).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVermelho).EndInit();
            Limites.ResumeLayout(false);
            Limites.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLmInf).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLmSup).EndInit();
            ResumeLayout(false);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.PerformLayout();
        }




        #endregion

        public static System.Windows.Forms.GroupBox Canal;
        public static System.Windows.Forms.Label Amostragem;
        public static System.Windows.Forms.Label TipoDeCanal;
        public static System.Windows.Forms.Label Nome;
        public static System.Windows.Forms.GroupBox Filtros;
        public static System.Windows.Forms.Label Notch;
        public static System.Windows.Forms.Label PassaAlta;
        public static System.Windows.Forms.Label PassaBaixa;
        public static System.Windows.Forms.GroupBox Display;
        public static System.Windows.Forms.GroupBox Cor;
        public static System.Windows.Forms.Label Azul;
        public static System.Windows.Forms.Label Verde;
        public static System.Windows.Forms.Label Vermelho;
        public static System.Windows.Forms.Label label8;
        public static System.Windows.Forms.Label label7;
        public static System.Windows.Forms.Button Confirmar;
        public static System.Windows.Forms.Button Aplicar;
        public static System.Windows.Forms.Button Cancelar;
        public static System.Windows.Forms.GroupBox Limites;
        public static System.Windows.Forms.Label LimiteInferior;
        public static System.Windows.Forms.Label LimiteSuperior;
        public static System.Windows.Forms.TextBox textAmostragem;
        public static System.Windows.Forms.TextBox textTipoDeCanal;
        public static System.Windows.Forms.TextBox textNome;
        public static System.Windows.Forms.TextBox textLegenda;
        public static System.Windows.Forms.ComboBox comboPassaBaixa;
        public static System.Windows.Forms.ComboBox comboAmplitude;
        public static System.Windows.Forms.ComboBox comboNotch;
        public static System.Windows.Forms.ComboBox comboPassaAlta;
        public static System.Windows.Forms.Label HzNotch;
        public static System.Windows.Forms.Label HzAlta;
        public static System.Windows.Forms.Label HzBaixa;
        public static System.Windows.Forms.NumericUpDown numericUpDownAzul;
        public static System.Windows.Forms.NumericUpDown numericUpDownVerde;
        public static System.Windows.Forms.NumericUpDown numericUpDownVermelho;
        public static System.Windows.Forms.NumericUpDown numericUpDownLmInf;
        public static System.Windows.Forms.NumericUpDown numericUpDownLmSup;
        public static System.Windows.Forms.RadioButton Gráfico;
        public static System.Windows.Forms.Button Color;
        public static System.Windows.Forms.RadioButton GraficoeNumeros;
        public static System.Windows.Forms.RadioButton Números;
        public static System.Windows.Forms.GroupBox NumerosPosi;
        public static System.Windows.Forms.RadioButton Horizontal;
        public static System.Windows.Forms.RadioButton Vertical;
        public static System.Windows.Forms.TrackBar trackBarR;
        public static System.Windows.Forms.TrackBar trackBarB;
        public static System.Windows.Forms.TrackBar trackBarG;
        public static System.Windows.Forms.GroupBox Grafico;
        public static System.Windows.Forms.CheckBox InverteSinal;
        public static System.Windows.Forms.CheckBox AutoEscala;
        public static System.Windows.Forms.GroupBox Posicao;
        public static System.Windows.Forms.RadioButton Configurar;
        public static System.Windows.Forms.RadioButton GráficoSetas;
        public static System.Windows.Forms.RadioButton MostrarSetas;
        public static System.Windows.Forms.ComboBox boxReferencia;
        public static System.Windows.Forms.Label Referencia;
        public static System.Windows.Forms.GroupBox groupBox1;
        public static System.Windows.Forms.GroupBox groupBox2;
    }
}