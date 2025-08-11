using System.Windows.Forms;

namespace PlotagemOpenGL.LaudoForm
{
    partial class FormLaudo
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLaudo));
            tabControl1 = new TabControl();
            Laudo = new TabPage();
            Laudos = new Label();
            Fechar = new Button();
            CriarLaudo = new Button();
            comboBox1 = new ComboBox();
            AbrirLaudo = new Button();
            Hipnograma = new TabPage();
            HipnoMostrando = new ComboBox();
            openglHipno = new SharpGL.OpenGLControl();
            contextMenuStripHipno = new ContextMenuStrip(components);
            Imprimir = new ToolStripMenuItem();
            separador = new ToolStripSeparator();
            BruxismoStrip = new ToolStripMenuItem();
            CardioStrip = new ToolStripMenuItem();
            CO2_ExalStrip = new ToolStripMenuItem();
            CPAPStrip = new ToolStripMenuItem();
            cpapVazStrip = new ToolStripMenuItem();
            DespertarStrip = new ToolStripMenuItem();
            estagioStrip = new ToolStripMenuItem();
            FreqCardStrip = new ToolStripMenuItem();
            horarioStrip = new ToolStripMenuItem();
            MicrofoneStrip = new ToolStripMenuItem();
            MovimentodePernaStrip = new ToolStripMenuItem();
            posicaoStip = new ToolStripMenuItem();
            eventosRespStrip = new ToolStripMenuItem();
            roncoStip = new ToolStripMenuItem();
            SA02Strip = new ToolStripMenuItem();
            LinhasInternas = new ToolStripMenuItem();
            NaomostrarQuedasZero = new ToolStripMenuItem();
            ConsBnBd = new ToolStripMenuItem();
            LinhasHorarios = new ToolStripMenuItem();
            CorGraf = new ToolStripMenuItem();
            MarcaDAgua = new ToolStripMenuItem();
            Comentarios = new TabPage();
            richTextBox1 = new RichTextBox();
            ImprimirComent = new Button();
            separador1 = new ToolStripSeparator();
            separador2 = new ToolStripSeparator();
            LimSup = new ToolStripMenuItem();
            LimInf = new ToolStripMenuItem();
            LinInt = new ToolStripMenuItem();
            DivLeg = new ToolStripMenuItem();
            tabControl1.SuspendLayout();
            Laudo.SuspendLayout();
            Hipnograma.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)openglHipno).BeginInit();
            contextMenuStripHipno.SuspendLayout();
            Comentarios.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Laudo);
            tabControl1.Controls.Add(Hipnograma);
            tabControl1.Controls.Add(Comentarios);
            tabControl1.Location = new System.Drawing.Point(3, 6);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1401, 779);
            tabControl1.TabIndex = 0;
            // 
            // Laudo
            // 
            Laudo.BackgroundImage = Properties.Resources.Fundo;
            Laudo.BackgroundImageLayout = ImageLayout.Stretch;
            Laudo.Controls.Add(Laudos);
            Laudo.Controls.Add(Fechar);
            Laudo.Controls.Add(CriarLaudo);
            Laudo.Controls.Add(comboBox1);
            Laudo.Controls.Add(AbrirLaudo);
            Laudo.Location = new System.Drawing.Point(4, 29);
            Laudo.Name = "Laudo";
            Laudo.Padding = new Padding(3);
            Laudo.Size = new System.Drawing.Size(1393, 746);
            Laudo.TabIndex = 0;
            Laudo.Text = "Laudo";
            Laudo.UseVisualStyleBackColor = true;
            // 
            // Laudos
            // 
            Laudos.AutoSize = true;
            Laudos.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Laudos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            Laudos.Location = new System.Drawing.Point(286, 160);
            Laudos.Name = "Laudos";
            Laudos.Size = new System.Drawing.Size(69, 21);
            Laudos.TabIndex = 4;
            Laudos.Text = "Laudos";
            // 
            // Fechar
            // 
            Fechar.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            Fechar.Location = new System.Drawing.Point(1113, 688);
            Fechar.Name = "Fechar";
            Fechar.Size = new System.Drawing.Size(274, 52);
            Fechar.TabIndex = 3;
            Fechar.Text = "Fechar";
            Fechar.UseVisualStyleBackColor = true;
            Fechar.Click += Fechar_Click;
            // 
            // CriarLaudo
            // 
            CriarLaudo.Font = new System.Drawing.Font("Arial", 9F);
            CriarLaudo.Location = new System.Drawing.Point(286, 222);
            CriarLaudo.Name = "CriarLaudo";
            CriarLaudo.Size = new System.Drawing.Size(274, 52);
            CriarLaudo.TabIndex = 2;
            CriarLaudo.Text = "Criar novo laudo";
            CriarLaudo.UseVisualStyleBackColor = true;
            CriarLaudo.Click += CriarLaudo_Click;
            // 
            // comboBox1
            // 
            comboBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.IntegralHeight = false;
            comboBox1.Location = new System.Drawing.Point(285, 183);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(274, 25);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // AbrirLaudo
            // 
            AbrirLaudo.Font = new System.Drawing.Font("Arial", 9F);
            AbrirLaudo.Location = new System.Drawing.Point(826, 222);
            AbrirLaudo.Name = "AbrirLaudo";
            AbrirLaudo.Size = new System.Drawing.Size(274, 52);
            AbrirLaudo.TabIndex = 0;
            AbrirLaudo.Text = "Abrir laudo ja salvo";
            AbrirLaudo.UseVisualStyleBackColor = true;
            // 
            // Hipnograma
            // 
            Hipnograma.Controls.Add(HipnoMostrando);
            Hipnograma.Controls.Add(openglHipno);
            Hipnograma.Location = new System.Drawing.Point(4, 29);
            Hipnograma.Name = "Hipnograma";
            Hipnograma.Padding = new Padding(3);
            Hipnograma.Size = new System.Drawing.Size(1393, 746);
            Hipnograma.TabIndex = 1;
            Hipnograma.Text = "Hipnograma";
            Hipnograma.UseVisualStyleBackColor = true;
            // 
            // HipnoMostrando
            // 
            HipnoMostrando.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            HipnoMostrando.FormattingEnabled = true;
            HipnoMostrando.Location = new System.Drawing.Point(6, 6);
            HipnoMostrando.Name = "HipnoMostrando";
            HipnoMostrando.Size = new System.Drawing.Size(242, 25);
            HipnoMostrando.TabIndex = 2;
            HipnoMostrando.TabIndexChanged += HipnoMostrando_TabIndexChanged;
            HipnoMostrando.TextChanged += HipnoMostrando_TabIndexChanged;
            // 
            // openglHipno
            // 
            openglHipno.ContextMenuStrip = contextMenuStripHipno;
            openglHipno.DrawFPS = false;
            openglHipno.Location = new System.Drawing.Point(6, 42);
            openglHipno.Margin = new Padding(4, 5, 4, 5);
            openglHipno.Name = "openglHipno";
            openglHipno.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            openglHipno.RenderContextType = SharpGL.RenderContextType.DIBSection;
            openglHipno.Size = new System.Drawing.Size(1381, 698);
            openglHipno.TabIndex = 1;
            openglHipno.MouseDown += OpenGLHipno_MouseDown;
            openglHipno.MouseMove += OpenGLHipno_MouseMove;
            openglHipno.MouseUp += OpenGLHipno_MouseUp;
            // 
            // contextMenuStripHipno
            // 
            contextMenuStripHipno.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStripHipno.Items.AddRange(new ToolStripItem[] { Imprimir, separador, BruxismoStrip, CardioStrip, CO2_ExalStrip, CPAPStrip, cpapVazStrip, DespertarStrip, estagioStrip, FreqCardStrip, horarioStrip, MicrofoneStrip, MovimentodePernaStrip, posicaoStip, eventosRespStrip, roncoStip, SA02Strip, LinhasInternas, NaomostrarQuedasZero, ConsBnBd, LinhasHorarios, CorGraf, MarcaDAgua });
            contextMenuStripHipno.Name = "contextMenuStripOpenGl";
            contextMenuStripHipno.Size = new System.Drawing.Size(293, 538);
            contextMenuStripHipno.Opening += ContextMenuStripOpenGl_Opening;
            // 
            // Imprimir
            // 
            Imprimir.Name = "Imprimir";
            Imprimir.Size = new System.Drawing.Size(292, 24);
            Imprimir.Text = "Imprimir";
            Imprimir.Click += ImprimeTela_Click;
            // 
            // separador
            // 
            separador.Name = "separador";
            separador.Size = new System.Drawing.Size(289, 6);
            // 
            // BruxismoStrip
            // 
            BruxismoStrip.CheckOnClick = true;
            BruxismoStrip.Name = "BruxismoStrip";
            BruxismoStrip.Size = new System.Drawing.Size(292, 24);
            BruxismoStrip.Tag = 40;
            BruxismoStrip.Text = "Bruxismo";
            BruxismoStrip.Click += ClicaMostraGraf;
            // 
            // CardioStrip
            // 
            CardioStrip.CheckOnClick = true;
            CardioStrip.Name = "CardioStrip";
            CardioStrip.Size = new System.Drawing.Size(292, 24);
            CardioStrip.Tag = 4;
            CardioStrip.Text = "Cardio";
            CardioStrip.Click += ClicaMostraGraf;
            // 
            // CO2_ExalStrip
            // 
            CO2_ExalStrip.CheckOnClick = true;
            CO2_ExalStrip.Name = "CO2_ExalStrip";
            CO2_ExalStrip.Size = new System.Drawing.Size(292, 24);
            CO2_ExalStrip.Tag = 39;
            CO2_ExalStrip.Text = "CO2_Exalado";
            CO2_ExalStrip.Click += ClicaMostraGraf;
            // 
            // CPAPStrip
            // 
            CPAPStrip.CheckOnClick = true;
            CPAPStrip.Name = "CPAPStrip";
            CPAPStrip.Size = new System.Drawing.Size(292, 24);
            CPAPStrip.Tag = 11;
            CPAPStrip.Text = "CPAP";
            CPAPStrip.Click += ClicaMostraGraf;
            // 
            // cpapVazStrip
            // 
            cpapVazStrip.CheckOnClick = true;
            cpapVazStrip.Name = "cpapVazStrip";
            cpapVazStrip.Size = new System.Drawing.Size(292, 24);
            cpapVazStrip.Tag = 18;
            cpapVazStrip.Text = "cpap Vazamento";
            cpapVazStrip.Click += ClicaMostraGraf;
            // 
            // DespertarStrip
            // 
            DespertarStrip.CheckOnClick = true;
            DespertarStrip.Name = "DespertarStrip";
            DespertarStrip.Size = new System.Drawing.Size(292, 24);
            DespertarStrip.Tag = 3;
            DespertarStrip.Text = "Despertar";
            DespertarStrip.Click += ClicaMostraGraf;
            // 
            // estagioStrip
            // 
            estagioStrip.CheckOnClick = true;
            estagioStrip.Name = "estagioStrip";
            estagioStrip.Size = new System.Drawing.Size(292, 24);
            estagioStrip.Tag = 9;
            estagioStrip.Text = "Estagio";
            estagioStrip.Click += ClicaMostraGraf;
            // 
            // FreqCardStrip
            // 
            FreqCardStrip.CheckOnClick = true;
            FreqCardStrip.Name = "FreqCardStrip";
            FreqCardStrip.Size = new System.Drawing.Size(292, 24);
            FreqCardStrip.Tag = 12;
            FreqCardStrip.Text = "Freq. Card.";
            FreqCardStrip.Click += ClicaMostraGraf;
            // 
            // horarioStrip
            // 
            horarioStrip.CheckOnClick = true;
            horarioStrip.Name = "horarioStrip";
            horarioStrip.Size = new System.Drawing.Size(292, 24);
            horarioStrip.Tag = 21;
            horarioStrip.Text = "Horario";
            horarioStrip.Click += ClicaMostraGraf;
            // 
            // MicrofoneStrip
            // 
            MicrofoneStrip.CheckOnClick = true;
            MicrofoneStrip.Name = "MicrofoneStrip";
            MicrofoneStrip.Size = new System.Drawing.Size(292, 24);
            MicrofoneStrip.Tag = 19;
            MicrofoneStrip.Text = "Microfone";
            MicrofoneStrip.Click += ClicaMostraGraf;
            // 
            // MovimentodePernaStrip
            // 
            MovimentodePernaStrip.CheckOnClick = true;
            MovimentodePernaStrip.Name = "MovimentodePernaStrip";
            MovimentodePernaStrip.Size = new System.Drawing.Size(292, 24);
            MovimentodePernaStrip.Tag = 5;
            MovimentodePernaStrip.Text = "Movimento de Perna";
            MovimentodePernaStrip.Click += ClicaMostraGraf;
            // 
            // posicaoStip
            // 
            posicaoStip.CheckOnClick = true;
            posicaoStip.Name = "posicaoStip";
            posicaoStip.Size = new System.Drawing.Size(292, 24);
            posicaoStip.Tag = 7;
            posicaoStip.Text = "Posicao";
            posicaoStip.Click += ClicaMostraGraf;
            // 
            // eventosRespStrip
            // 
            eventosRespStrip.CheckOnClick = true;
            eventosRespStrip.Name = "eventosRespStrip";
            eventosRespStrip.Size = new System.Drawing.Size(292, 24);
            eventosRespStrip.Tag = 1;
            eventosRespStrip.Text = "Respiratorios";
            eventosRespStrip.Click += ClicaMostraGraf;
            // 
            // roncoStip
            // 
            roncoStip.CheckOnClick = true;
            roncoStip.Name = "roncoStip";
            roncoStip.Size = new System.Drawing.Size(292, 24);
            roncoStip.Tag = 10;
            roncoStip.Text = "Ronco";
            roncoStip.Click += ClicaMostraGraf;
            // 
            // SA02Strip
            // 
            SA02Strip.CheckOnClick = true;
            SA02Strip.Name = "SA02Strip";
            SA02Strip.Size = new System.Drawing.Size(292, 24);
            SA02Strip.Tag = 6;
            SA02Strip.Text = "SaO2";
            SA02Strip.Click += ClicaMostraGraf;
            // 
            // LinhasInternas
            // 
            LinhasInternas.CheckOnClick = true;
            LinhasInternas.Name = "LinhasInternas";
            LinhasInternas.Size = new System.Drawing.Size(292, 24);
            LinhasInternas.Text = "Linhas Internas";
            // 
            // NaomostrarQuedasZero
            // 
            NaomostrarQuedasZero.CheckOnClick = true;
            NaomostrarQuedasZero.Name = "NaomostrarQuedasZero";
            NaomostrarQuedasZero.Size = new System.Drawing.Size(292, 24);
            NaomostrarQuedasZero.Text = "Nao mostrar Quedas a Zero";
            NaomostrarQuedasZero.Click += linhadehorario_Click;
            // 
            // ConsBnBd
            // 
            ConsBnBd.CheckOnClick = true;
            ConsBnBd.Name = "ConsBnBd";
            ConsBnBd.Size = new System.Drawing.Size(292, 24);
            ConsBnBd.Text = "Considerar Boa Noite e Bom dia";
            ConsBnBd.Click += ConsBnBd_Clic;
            // 
            // LinhasHorarios
            // 
            LinhasHorarios.CheckOnClick = true;
            LinhasHorarios.Name = "LinhasHorarios";
            LinhasHorarios.Size = new System.Drawing.Size(292, 24);
            LinhasHorarios.Text = "Linhas de Horarios";
            LinhasHorarios.Click += linhadehorario_Click;
            // 
            // CorGraf
            // 
            CorGraf.Name = "CorGraf";
            CorGraf.Size = new System.Drawing.Size(292, 24);
            CorGraf.Text = "Cor do Grafico";
            CorGraf.Click += CorSinal_Click;
            // 
            // MarcaDAgua
            // 
            MarcaDAgua.CheckOnClick = true;
            MarcaDAgua.Name = "MarcaDAgua";
            MarcaDAgua.Size = new System.Drawing.Size(292, 24);
            MarcaDAgua.Text = "Marca D'Agua";
            MarcaDAgua.Click += linhadehorario_Click;
            // 
            // Comentarios
            // 
            Comentarios.Controls.Add(richTextBox1);
            Comentarios.Controls.Add(ImprimirComent);
            Comentarios.Location = new System.Drawing.Point(4, 29);
            Comentarios.Name = "Comentarios";
            Comentarios.Size = new System.Drawing.Size(1393, 746);
            Comentarios.TabIndex = 2;
            Comentarios.Text = "Comentarios";
            Comentarios.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            richTextBox1.Location = new System.Drawing.Point(5, 51);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new System.Drawing.Size(1383, 679);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // ImprimirComent
            // 
            ImprimirComent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            ImprimirComent.Location = new System.Drawing.Point(5, 13);
            ImprimirComent.Name = "ImprimirComent";
            ImprimirComent.Size = new System.Drawing.Size(94, 29);
            ImprimirComent.TabIndex = 0;
            ImprimirComent.Text = "Imprimir";
            ImprimirComent.UseVisualStyleBackColor = true;
            // 
            // separador1
            // 
            separador1.Name = "separador1";
            separador1.Size = new System.Drawing.Size(150, 6);
            // 
            // separador2
            // 
            separador2.Name = "separador2";
            separador2.Size = new System.Drawing.Size(150, 6);
            // 
            // LimSup
            // 
            LimSup.Name = "LimSup";
            LimSup.Size = new System.Drawing.Size(148, 26);
            LimSup.Text = "Limite Superior";
            LimSup.Click += lmSup_Click;
            // 
            // LimInf
            // 
            LimInf.Name = "LimInf";
            LimInf.Size = new System.Drawing.Size(148, 26);
            LimInf.Text = "Limite Inferior";
            LimInf.Click += lmInf_Click;
            // 
            // LinInt
            // 
            LinInt.Name = "LinInt";
            LinInt.Size = new System.Drawing.Size(148, 26);
            LinInt.Text = "Linhas internas";
            LinInt.Click += LinhasInternas_Click;
            // 
            // DivLeg
            // 
            DivLeg.Name = "DivLeg";
            DivLeg.Size = new System.Drawing.Size(148, 26);
            DivLeg.Text = "Divisoes das legendas";
            DivLeg.Click += DivLegenda_Click;
            // 
            // FormLaudo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1409, 789);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "FormLaudo";
            Text = "FormLaudo";
            tabControl1.ResumeLayout(false);
            Laudo.ResumeLayout(false);
            Laudo.PerformLayout();
            Hipnograma.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)openglHipno).EndInit();
            contextMenuStripHipno.ResumeLayout(false);
            Comentarios.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public static TabControl tabControl1;
        public static TabPage Laudo;
        public static TabPage Hipnograma;
        public static TabPage Comentarios;
        public static ComboBox HipnoMostrando;
        public static Button AbrirLaudo;
        public static Label Laudos;
        public static Button Fechar;
        public static Button CriarLaudo;
        public static ComboBox comboBox1;
        public static Button ImprimirComent;
        public static RichTextBox richTextBox1;
        public static ToolStripSeparator separador;
        public static ToolStripSeparator separador1;
        public static ToolStripSeparator separador2;
        public static SharpGL.OpenGLControl openglHipno;
        public static ContextMenuStrip contextMenuStripHipno;
        public static ToolStripMenuItem Imprimir;
        public static ToolStripMenuItem BruxismoStrip;
        public static ToolStripMenuItem CardioStrip;
        public static ToolStripMenuItem CO2_ExalStrip;
        public static ToolStripMenuItem CPAPStrip;
        public static ToolStripMenuItem cpapVazStrip;
        public static ToolStripMenuItem DespertarStrip;
        public static ToolStripMenuItem estagioStrip;
        public static ToolStripMenuItem FreqCardStrip;
        public static ToolStripMenuItem horarioStrip;
        public static ToolStripMenuItem MicrofoneStrip;
        public static ToolStripMenuItem MovimentodePernaStrip;
        public static ToolStripMenuItem posicaoStip;
        public static ToolStripMenuItem eventosRespStrip;
        public static ToolStripMenuItem roncoStip;
        public static ToolStripMenuItem SA02Strip;
        public static ToolStripMenuItem LinhasInternas;
        public static ToolStripMenuItem NaomostrarQuedasZero;
        public static ToolStripMenuItem ConsBnBd;
        public static ToolStripMenuItem LinhasHorarios;
        public static ToolStripMenuItem CorGraf;
        public static ToolStripMenuItem MarcaDAgua;
        public static ToolStripMenuItem LimSup;
        public static ToolStripMenuItem LimInf;
        public static ToolStripMenuItem LinInt;
        public static ToolStripMenuItem DivLeg;
    }
}