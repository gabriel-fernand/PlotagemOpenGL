using Accord.IO;
using System.Windows.Forms;

namespace PlotagemOpenGL
{
    partial class Tela_Plotagem
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tela_Plotagem));
            hScrollBar1 = new HScrollBar();
            ptsEmTela = new TextBox();
            inicioTela = new TextBox();
            fimTela = new TextBox();
            tempoEmTela = new ComboBox();
            velocidadeScroll = new ComboBox();
            MontagemBox = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            label3 = new Label();
            label5 = new Label();
            Play = new Button();
            openglControl1 = new SharpGL.OpenGLControl();
            contextMenuStripOpenGl = new ContextMenuStrip(components);
            BomDia = new ToolStripTextBox();
            BoaNoite = new ToolStripTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            LowPassFilterGl = new ToolStripMenuItem();
            NenhumLowGl = new ToolStripMenuItem();
            hertz70Gl = new ToolStripMenuItem();
            hertz50Gl = new ToolStripMenuItem();
            hertz40Gl = new ToolStripMenuItem();
            hertz35Gl = new ToolStripMenuItem();
            hertz30Gl = new ToolStripMenuItem();
            hertz25Gl = new ToolStripMenuItem();
            hertz20Gl = new ToolStripMenuItem();
            hertz15Gl = new ToolStripMenuItem();
            hertz10Gl = new ToolStripMenuItem();
            hertz5Gl = new ToolStripMenuItem();
            OutroLowGl = new ToolStripMenuItem();
            HighPassFilterGl = new ToolStripMenuItem();
            NenhumHighGl = new ToolStripMenuItem();
            hertz10HGl = new ToolStripMenuItem();
            hertz7Gl = new ToolStripMenuItem();
            hertz5HGl = new ToolStripMenuItem();
            hertz3Gl = new ToolStripMenuItem();
            hertz1Gl = new ToolStripMenuItem();
            hertz07Gl = new ToolStripMenuItem();
            hertz05Gl = new ToolStripMenuItem();
            hertz03Gl = new ToolStripMenuItem();
            hertz01Gl = new ToolStripMenuItem();
            OutroHighGl = new ToolStripMenuItem();
            contextMenuStrip1 = new ContextMenuStrip(components);
            LowPassFilter = new ToolStripMenuItem();
            NenhumLow = new ToolStripMenuItem();
            hertz70 = new ToolStripMenuItem();
            hertz50 = new ToolStripMenuItem();
            hertz40 = new ToolStripMenuItem();
            hertz35 = new ToolStripMenuItem();
            hertz30 = new ToolStripMenuItem();
            hertz25 = new ToolStripMenuItem();
            hertz20 = new ToolStripMenuItem();
            hertz15 = new ToolStripMenuItem();
            hertz10 = new ToolStripMenuItem();
            hertz5 = new ToolStripMenuItem();
            OutroLow = new ToolStripMenuItem();
            HighPassFilter = new ToolStripMenuItem();
            NenhumHigh = new ToolStripMenuItem();
            hertz10H = new ToolStripMenuItem();
            hertz7 = new ToolStripMenuItem();
            hertz5H = new ToolStripMenuItem();
            hertz3 = new ToolStripMenuItem();
            hertz1 = new ToolStripMenuItem();
            hertz07 = new ToolStripMenuItem();
            hertz05 = new ToolStripMenuItem();
            hertz03 = new ToolStripMenuItem();
            hertz01 = new ToolStripMenuItem();
            outroHigh = new ToolStripMenuItem();
            NotchPassFilter = new ToolStripMenuItem();
            painelExames = new Panel();
            panel1 = new Panel();
            minusLb1 = new Button();
            plusLb1 = new Button();
            scalaLb1 = new Label();
            panel2 = new Panel();
            minusLb2 = new Button();
            plusLb2 = new Button();
            scalaLb2 = new Label();
            panel3 = new Panel();
            minusLb3 = new Button();
            plusLb3 = new Button();
            scalaLb3 = new Label();
            panel4 = new Panel();
            minusLb4 = new Button();
            plusLb4 = new Button();
            scalaLb4 = new Label();
            panel5 = new Panel();
            minusLb5 = new Button();
            plusLb5 = new Button();
            scalaLb5 = new Label();
            panel6 = new Panel();
            minusLb6 = new Button();
            plusLb6 = new Button();
            label6 = new Label();
            scalaLb6 = new Label();
            panel7 = new Panel();
            plusLb7 = new Button();
            minusLb7 = new Button();
            label7 = new Label();
            scalaLb7 = new Label();
            panel8 = new Panel();
            minusLb8 = new Button();
            plusLb8 = new Button();
            label8 = new Label();
            scalaLb8 = new Label();
            panel9 = new Panel();
            minusLb9 = new Button();
            plusLb9 = new Button();
            label9 = new Label();
            scalaLb9 = new Label();
            panel10 = new Panel();
            minusLb10 = new Button();
            plusLb10 = new Button();
            label10 = new Label();
            scalaLb10 = new Label();
            panel11 = new Panel();
            minusLb11 = new Button();
            plusLb11 = new Button();
            label11 = new Label();
            scalaLb11 = new Label();
            panel12 = new Panel();
            plusLb12 = new Button();
            minusLb12 = new Button();
            label12 = new Label();
            scalaLb12 = new Label();
            panel13 = new Panel();
            minusLb13 = new Button();
            plusLb13 = new Button();
            label13 = new Label();
            scalaLb13 = new Label();
            panel14 = new Panel();
            minusLb14 = new Button();
            plusLb14 = new Button();
            label14 = new Label();
            scalaLb14 = new Label();
            panel15 = new Panel();
            minusLb15 = new Button();
            plusLb15 = new Button();
            label15 = new Label();
            scalaLb15 = new Label();
            panel16 = new Panel();
            minusLb16 = new Button();
            plusLb16 = new Button();
            label16 = new Label();
            scalaLb16 = new Label();
            panel17 = new Panel();
            plusLb17 = new Button();
            minusLb17 = new Button();
            label17 = new Label();
            scalaLb17 = new Label();
            panel18 = new Panel();
            minusLb18 = new Button();
            plusLb18 = new Button();
            label18 = new Label();
            scalaLb18 = new Label();
            panel19 = new Panel();
            minusLb19 = new Button();
            plusLb19 = new Button();
            label19 = new Label();
            scalaLb19 = new Label();
            panel20 = new Panel();
            minusLb20 = new Button();
            plusLb20 = new Button();
            label20 = new Label();
            scalaLb20 = new Label();
            panel21 = new Panel();
            minusLb21 = new Button();
            plusLb21 = new Button();
            label21 = new Label();
            scalaLb21 = new Label();
            panel22 = new Panel();
            minusLb22 = new Button();
            plusLb22 = new Button();
            label22 = new Label();
            scalaLb22 = new Label();
            panel23 = new Panel();
            minusLb23 = new Button();
            label23 = new Label();
            plusLb23 = new Button();
            scalaLb23 = new Label();
            painelTelaGl = new Panel();
            painelComando = new Panel();
            MouseLoc = new TextBox();
            playSelect = new Button();
            minusAll = new Button();
            plusAll = new Button();
            qtdGraficos = new TextBox();
            toolTip1 = new ToolTip(components);
            timer1 = new Timer(components);
            timer2 = new Timer(components);
            OutroNotch = new ToolStripMenuItem();
            hertz50N = new ToolStripMenuItem();
            hertz60N = new ToolStripMenuItem();
            NenhumNotch = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)openglControl1).BeginInit();
            contextMenuStripOpenGl.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            painelExames.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel12.SuspendLayout();
            panel13.SuspendLayout();
            panel14.SuspendLayout();
            panel15.SuspendLayout();
            panel16.SuspendLayout();
            panel17.SuspendLayout();
            panel18.SuspendLayout();
            panel19.SuspendLayout();
            panel20.SuspendLayout();
            panel21.SuspendLayout();
            panel22.SuspendLayout();
            panel23.SuspendLayout();
            painelTelaGl.SuspendLayout();
            painelComando.SuspendLayout();
            SuspendLayout();
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new System.Drawing.Point(5, 1);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new System.Drawing.Size(992, 31);
            hScrollBar1.TabIndex = 0;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            hScrollBar1.KeyDown += TelaPlotagem_KeyDown;
            // 
            // ptsEmTela
            // 
            ptsEmTela.Anchor = AnchorStyles.None;
            ptsEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            ptsEmTela.Location = new System.Drawing.Point(5, 4);
            ptsEmTela.Name = "ptsEmTela";
            ptsEmTela.ReadOnly = true;
            ptsEmTela.Size = new System.Drawing.Size(81, 25);
            ptsEmTela.TabIndex = 2;
            ptsEmTela.Text = "ptsEmTela";
            ptsEmTela.TextAlign = HorizontalAlignment.Center;
            // 
            // inicioTela
            // 
            inicioTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            inicioTela.Location = new System.Drawing.Point(100, 4);
            inicioTela.Name = "inicioTela";
            inicioTela.ReadOnly = true;
            inicioTela.Size = new System.Drawing.Size(72, 25);
            inicioTela.TabIndex = 3;
            inicioTela.Text = "00-00-00";
            inicioTela.TextAlign = HorizontalAlignment.Center;
            // 
            // fimTela
            // 
            fimTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            fimTela.Location = new System.Drawing.Point(178, 4);
            fimTela.Name = "fimTela";
            fimTela.ReadOnly = true;
            fimTela.Size = new System.Drawing.Size(72, 25);
            fimTela.TabIndex = 4;
            fimTela.Text = "99-99-99";
            fimTela.TextAlign = HorizontalAlignment.Center;
            // 
            // tempoEmTela
            // 
            tempoEmTela.DisplayMember = "8s";
            tempoEmTela.DropDownStyle = ComboBoxStyle.DropDownList;
            tempoEmTela.FlatStyle = FlatStyle.System;
            tempoEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            tempoEmTela.FormatString = "N1";
            tempoEmTela.FormattingEnabled = true;
            tempoEmTela.IntegralHeight = false;
            tempoEmTela.Items.AddRange(new object[] { "8 seg", "12 seg", "30 seg", "60 seg", "90 seg", "120 seg", "240 seg" });
            tempoEmTela.Location = new System.Drawing.Point(256, 4);
            tempoEmTela.Name = "tempoEmTela";
            tempoEmTela.Size = new System.Drawing.Size(65, 28);
            tempoEmTela.TabIndex = 6;
            tempoEmTela.SelectedIndexChanged += tempoEmTela_SelectedIndexChanged;
            // 
            // velocidadeScroll
            // 
            velocidadeScroll.DisplayMember = "1.0x";
            velocidadeScroll.DropDownStyle = ComboBoxStyle.DropDownList;
            velocidadeScroll.FlatStyle = FlatStyle.System;
            velocidadeScroll.FormattingEnabled = true;
            velocidadeScroll.IntegralHeight = false;
            velocidadeScroll.Items.AddRange(new object[] { "1.0x", "1.5x", "2.0x", "2.5x", "5.0x" });
            velocidadeScroll.Location = new System.Drawing.Point(327, 4);
            velocidadeScroll.Name = "velocidadeScroll";
            velocidadeScroll.Size = new System.Drawing.Size(63, 28);
            velocidadeScroll.TabIndex = 7;
            velocidadeScroll.SelectedIndexChanged += velocidadeScroll_SelectedIndexChanged;
            // 
            // MontagemBox
            // 
            MontagemBox.DropDownStyle = ComboBoxStyle.DropDownList;
            MontagemBox.FlatStyle = FlatStyle.System;
            MontagemBox.FormattingEnabled = true;
            MontagemBox.IntegralHeight = false;
            MontagemBox.Items.AddRange(new object[] { "Series" });
            MontagemBox.Location = new System.Drawing.Point(535, 4);
            MontagemBox.Name = "MontagemBox";
            MontagemBox.Size = new System.Drawing.Size(340, 28);
            MontagemBox.TabIndex = 8;
            MontagemBox.SelectedIndexChanged += MontagemBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label1.Location = new System.Drawing.Point(3, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 24);
            label1.TabIndex = 9;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label2.Location = new System.Drawing.Point(3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(52, 24);
            label2.TabIndex = 10;
            label2.Text = "label2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label4.Location = new System.Drawing.Point(3, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 24);
            label4.TabIndex = 12;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label3.Location = new System.Drawing.Point(3, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 24);
            label3.TabIndex = 11;
            label3.Text = "label3";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label5.Location = new System.Drawing.Point(3, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 24);
            label5.TabIndex = 13;
            label5.Text = "label5";
            // 
            // Play
            // 
            Play.Location = new System.Drawing.Point(488, 4);
            Play.Name = "Play";
            Play.Size = new System.Drawing.Size(41, 29);
            Play.TabIndex = 49;
            Play.Text = ">";
            Play.UseVisualStyleBackColor = true;
            Play.Click += Play_Click;
            // 
            // openglControl1
            // 
            openglControl1.ContextMenuStrip = contextMenuStripOpenGl;
            openglControl1.DrawFPS = false;
            openglControl1.Location = new System.Drawing.Point(122, 81);
            openglControl1.Margin = new Padding(4, 5, 4, 5);
            openglControl1.Name = "openglControl1";
            openglControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            openglControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            openglControl1.Size = new System.Drawing.Size(879, 597);
            openglControl1.TabIndex = 50;
            openglControl1.Scroll += hScrollBar1_Scroll;
            openglControl1.KeyDown += TelaPlotagem_KeyDown;
            openglControl1.MouseDown += OpenGLControl_MouseDown;
            openglControl1.MouseMove += OpenGLControl_MouseMove;
            openglControl1.MouseUp += OpenGLControl_MouseUp;
            openglControl1.MouseWheel += OpenglControl1_MouseWheel;
            // 
            // contextMenuStripOpenGl
            // 
            contextMenuStripOpenGl.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStripOpenGl.Items.AddRange(new ToolStripItem[] { BomDia, BoaNoite, toolStripSeparator1, LowPassFilterGl, HighPassFilterGl });
            contextMenuStripOpenGl.Name = "contextMenuStripOpenGl";
            contextMenuStripOpenGl.Size = new System.Drawing.Size(179, 116);
            contextMenuStripOpenGl.Opening += ContextMenuStripOpenGl_Opening;
            // 
            // BomDia
            // 
            BomDia.Name = "BomDia";
            BomDia.Size = new System.Drawing.Size(100, 27);
            BomDia.Text = "Bom Dia";
            // 
            // BoaNoite
            // 
            BoaNoite.Name = "BoaNoite";
            BoaNoite.Size = new System.Drawing.Size(100, 27);
            BoaNoite.Text = "Boa Noite";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // LowPassFilterGl
            // 
            LowPassFilterGl.DropDownItems.AddRange(new ToolStripItem[] { NenhumLowGl, hertz70Gl, hertz50Gl, hertz40Gl, hertz35Gl, hertz30Gl, hertz25Gl, hertz20Gl, hertz15Gl, hertz10Gl, hertz5Gl, OutroLowGl });
            LowPassFilterGl.Name = "LowPassFilterGl";
            LowPassFilterGl.Size = new System.Drawing.Size(178, 24);
            LowPassFilterGl.Text = "Low Pass Filter";
            // 
            // NenhumLowGl
            // 
            NenhumLowGl.CheckOnClick = true;
            NenhumLowGl.Name = "NenhumLowGl";
            NenhumLowGl.Size = new System.Drawing.Size(148, 26);
            NenhumLowGl.Text = "Nenhum";
            // 
            // hertz70Gl
            // 
            hertz70Gl.CheckOnClick = true;
            hertz70Gl.Name = "hertz70Gl";
            hertz70Gl.Size = new System.Drawing.Size(148, 26);
            hertz70Gl.Text = "70 hz";
            // 
            // hertz50Gl
            // 
            hertz50Gl.CheckOnClick = true;
            hertz50Gl.Name = "hertz50Gl";
            hertz50Gl.Size = new System.Drawing.Size(148, 26);
            hertz50Gl.Text = "50 hz";
            // 
            // hertz40Gl
            // 
            hertz40Gl.CheckOnClick = true;
            hertz40Gl.Name = "hertz40Gl";
            hertz40Gl.Size = new System.Drawing.Size(148, 26);
            hertz40Gl.Text = "40 hz";
            // 
            // hertz35Gl
            // 
            hertz35Gl.CheckOnClick = true;
            hertz35Gl.Name = "hertz35Gl";
            hertz35Gl.Size = new System.Drawing.Size(148, 26);
            hertz35Gl.Text = "35 hz";
            // 
            // hertz30Gl
            // 
            hertz30Gl.CheckOnClick = true;
            hertz30Gl.Name = "hertz30Gl";
            hertz30Gl.Size = new System.Drawing.Size(148, 26);
            hertz30Gl.Text = "30 hz";
            // 
            // hertz25Gl
            // 
            hertz25Gl.CheckOnClick = true;
            hertz25Gl.Name = "hertz25Gl";
            hertz25Gl.Size = new System.Drawing.Size(148, 26);
            hertz25Gl.Text = "25 hz";
            // 
            // hertz20Gl
            // 
            hertz20Gl.CheckOnClick = true;
            hertz20Gl.Name = "hertz20Gl";
            hertz20Gl.Size = new System.Drawing.Size(148, 26);
            hertz20Gl.Text = "20 hz";
            // 
            // hertz15Gl
            // 
            hertz15Gl.CheckOnClick = true;
            hertz15Gl.Name = "hertz15Gl";
            hertz15Gl.Size = new System.Drawing.Size(148, 26);
            hertz15Gl.Text = "15 hz";
            // 
            // hertz10Gl
            // 
            hertz10Gl.CheckOnClick = true;
            hertz10Gl.Name = "hertz10Gl";
            hertz10Gl.Size = new System.Drawing.Size(148, 26);
            hertz10Gl.Text = "10 hz";
            // 
            // hertz5Gl
            // 
            hertz5Gl.CheckOnClick = true;
            hertz5Gl.Name = "hertz5Gl";
            hertz5Gl.Size = new System.Drawing.Size(148, 26);
            hertz5Gl.Text = "5  hz";
            // 
            // OutroLowGl
            // 
            OutroLowGl.CheckOnClick = true;
            OutroLowGl.Name = "OutroLowGl";
            OutroLowGl.Size = new System.Drawing.Size(148, 26);
            OutroLowGl.Text = "Outro";
            // 
            // HighPassFilterGl
            // 
            HighPassFilterGl.DropDownItems.AddRange(new ToolStripItem[] { NenhumHighGl, hertz10HGl, hertz7Gl, hertz5HGl, hertz3Gl, hertz1Gl, hertz07Gl, hertz05Gl, hertz03Gl, hertz01Gl, OutroHighGl });
            HighPassFilterGl.Name = "HighPassFilterGl";
            HighPassFilterGl.Size = new System.Drawing.Size(178, 24);
            HighPassFilterGl.Text = "High Pass Filter";
            // 
            // NenhumHighGl
            // 
            NenhumHighGl.CheckOnClick = true;
            NenhumHighGl.Name = "NenhumHighGl";
            NenhumHighGl.Size = new System.Drawing.Size(148, 26);
            NenhumHighGl.Text = "Nenhum";
            // 
            // hertz10HGl
            // 
            hertz10HGl.CheckOnClick = true;
            hertz10HGl.Name = "hertz10HGl";
            hertz10HGl.Size = new System.Drawing.Size(148, 26);
            hertz10HGl.Text = "10 hz";
            // 
            // hertz7Gl
            // 
            hertz7Gl.CheckOnClick = true;
            hertz7Gl.Name = "hertz7Gl";
            hertz7Gl.Size = new System.Drawing.Size(148, 26);
            hertz7Gl.Text = "7  hz";
            // 
            // hertz5HGl
            // 
            hertz5HGl.CheckOnClick = true;
            hertz5HGl.Name = "hertz5HGl";
            hertz5HGl.Size = new System.Drawing.Size(148, 26);
            hertz5HGl.Text = "5  hz";
            // 
            // hertz3Gl
            // 
            hertz3Gl.CheckOnClick = true;
            hertz3Gl.Name = "hertz3Gl";
            hertz3Gl.Size = new System.Drawing.Size(148, 26);
            hertz3Gl.Text = "3  hz";
            // 
            // hertz1Gl
            // 
            hertz1Gl.CheckOnClick = true;
            hertz1Gl.Name = "hertz1Gl";
            hertz1Gl.Size = new System.Drawing.Size(148, 26);
            hertz1Gl.Text = "1  hz";
            // 
            // hertz07Gl
            // 
            hertz07Gl.CheckOnClick = true;
            hertz07Gl.Name = "hertz07Gl";
            hertz07Gl.Size = new System.Drawing.Size(148, 26);
            hertz07Gl.Text = "0,7 hz";
            // 
            // hertz05Gl
            // 
            hertz05Gl.CheckOnClick = true;
            hertz05Gl.Name = "hertz05Gl";
            hertz05Gl.Size = new System.Drawing.Size(148, 26);
            hertz05Gl.Text = "0,5 hz";
            // 
            // hertz03Gl
            // 
            hertz03Gl.CheckOnClick = true;
            hertz03Gl.Name = "hertz03Gl";
            hertz03Gl.Size = new System.Drawing.Size(148, 26);
            hertz03Gl.Text = "0,3 hz";
            // 
            // hertz01Gl
            // 
            hertz01Gl.CheckOnClick = true;
            hertz01Gl.Name = "hertz01Gl";
            hertz01Gl.Size = new System.Drawing.Size(148, 26);
            hertz01Gl.Text = "0,1 hz";
            // 
            // OutroHighGl
            // 
            OutroHighGl.CheckOnClick = true;
            OutroHighGl.Name = "OutroHighGl";
            OutroHighGl.Size = new System.Drawing.Size(148, 26);
            OutroHighGl.Text = "Outro";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { LowPassFilter, HighPassFilter, NotchPassFilter });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new System.Drawing.Size(211, 104);
            contextMenuStrip1.Opening += contextMenuStrip1_Opening;
            // 
            // LowPassFilter
            // 
            LowPassFilter.DropDownItems.AddRange(new ToolStripItem[] { NenhumLow, hertz70, hertz50, hertz40, hertz35, hertz30, hertz25, hertz20, hertz15, hertz10, hertz5, OutroLow });
            LowPassFilter.Name = "LowPassFilter";
            LowPassFilter.Size = new System.Drawing.Size(210, 24);
            LowPassFilter.Text = "Low Pass Filter";
            LowPassFilter.DropDownOpening += toolTripItemDropDown_OpeningLow;
            // 
            // NenhumLow
            // 
            NenhumLow.CheckOnClick = true;
            NenhumLow.Name = "NenhumLow";
            NenhumLow.Size = new System.Drawing.Size(224, 26);
            NenhumLow.Text = "Nenhum";
            NenhumLow.Click += MenuItem_Click;
            // 
            // hertz70
            // 
            hertz70.CheckOnClick = true;
            hertz70.Name = "hertz70";
            hertz70.Size = new System.Drawing.Size(224, 26);
            hertz70.Text = "70 hz";
            hertz70.Click += MenuItem_Click;
            // 
            // hertz50
            // 
            hertz50.CheckOnClick = true;
            hertz50.Name = "hertz50";
            hertz50.Size = new System.Drawing.Size(224, 26);
            hertz50.Text = "50 hz";
            hertz50.Click += MenuItem_Click;
            // 
            // hertz40
            // 
            hertz40.CheckOnClick = true;
            hertz40.Name = "hertz40";
            hertz40.Size = new System.Drawing.Size(224, 26);
            hertz40.Text = "40 hz";
            hertz40.Click += MenuItem_Click;
            // 
            // hertz35
            // 
            hertz35.CheckOnClick = true;
            hertz35.Name = "hertz35";
            hertz35.Size = new System.Drawing.Size(224, 26);
            hertz35.Text = "35 hz";
            hertz35.Click += MenuItem_Click;
            // 
            // hertz30
            // 
            hertz30.CheckOnClick = true;
            hertz30.Name = "hertz30";
            hertz30.Size = new System.Drawing.Size(224, 26);
            hertz30.Text = "30 hz";
            hertz30.Click += MenuItem_Click;
            // 
            // hertz25
            // 
            hertz25.CheckOnClick = true;
            hertz25.Name = "hertz25";
            hertz25.Size = new System.Drawing.Size(224, 26);
            hertz25.Text = "25 hz";
            hertz25.Click += MenuItem_Click;
            // 
            // hertz20
            // 
            hertz20.CheckOnClick = true;
            hertz20.Name = "hertz20";
            hertz20.Size = new System.Drawing.Size(224, 26);
            hertz20.Text = "20 hz";
            hertz20.Click += MenuItem_Click;
            // 
            // hertz15
            // 
            hertz15.CheckOnClick = true;
            hertz15.Name = "hertz15";
            hertz15.Size = new System.Drawing.Size(224, 26);
            hertz15.Text = "15 hz";
            hertz15.Click += MenuItem_Click;
            // 
            // hertz10
            // 
            hertz10.CheckOnClick = true;
            hertz10.Name = "hertz10";
            hertz10.Size = new System.Drawing.Size(224, 26);
            hertz10.Text = "10 hz";
            hertz10.Click += MenuItem_Click;
            // 
            // hertz5
            // 
            hertz5.CheckOnClick = true;
            hertz5.Name = "hertz5";
            hertz5.Size = new System.Drawing.Size(224, 26);
            hertz5.Text = "5  hz";
            hertz5.Click += MenuItem_Click;
            // 
            // OutroLow
            // 
            OutroLow.CheckOnClick = true;
            OutroLow.Name = "OutroLow";
            OutroLow.Size = new System.Drawing.Size(224, 26);
            OutroLow.Text = "Outro";
            OutroLow.Click += MenuItem_Click;
            // 
            // HighPassFilter
            // 
            HighPassFilter.DropDownItems.AddRange(new ToolStripItem[] { NenhumHigh, hertz10H, hertz7, hertz5H, hertz3, hertz1, hertz07, hertz05, hertz03, hertz01, outroHigh });
            HighPassFilter.Name = "HighPassFilter";
            HighPassFilter.Size = new System.Drawing.Size(210, 24);
            HighPassFilter.Text = "High Pass Filter";
            HighPassFilter.DropDownOpening += toolTripItemDropDown_OpeningHigh;
            // 
            // NenhumHigh
            // 
            NenhumHigh.CheckOnClick = true;
            NenhumHigh.Name = "NenhumHigh";
            NenhumHigh.Size = new System.Drawing.Size(224, 26);
            NenhumHigh.Text = "Nenhum";
            NenhumHigh.Click += MenuItem_Click;
            // 
            // hertz10H
            // 
            hertz10H.CheckOnClick = true;
            hertz10H.Name = "hertz10H";
            hertz10H.Size = new System.Drawing.Size(224, 26);
            hertz10H.Text = "10 hz";
            hertz10H.Click += MenuItem_Click;
            // 
            // hertz7
            // 
            hertz7.CheckOnClick = true;
            hertz7.Name = "hertz7";
            hertz7.Size = new System.Drawing.Size(224, 26);
            hertz7.Text = "7  hz";
            hertz7.Click += MenuItem_Click;
            // 
            // hertz5H
            // 
            hertz5H.CheckOnClick = true;
            hertz5H.Name = "hertz5H";
            hertz5H.Size = new System.Drawing.Size(224, 26);
            hertz5H.Text = "5  hz";
            hertz5H.Click += MenuItem_Click;
            // 
            // hertz3
            // 
            hertz3.CheckOnClick = true;
            hertz3.Name = "hertz3";
            hertz3.Size = new System.Drawing.Size(224, 26);
            hertz3.Text = "3  hz";
            hertz3.Click += MenuItem_Click;
            // 
            // hertz1
            // 
            hertz1.CheckOnClick = true;
            hertz1.Name = "hertz1";
            hertz1.Size = new System.Drawing.Size(224, 26);
            hertz1.Text = "1  hz";
            hertz1.Click += MenuItem_Click;
            // 
            // hertz07
            // 
            hertz07.CheckOnClick = true;
            hertz07.Name = "hertz07";
            hertz07.Size = new System.Drawing.Size(224, 26);
            hertz07.Text = "0,7 hz";
            hertz07.Click += MenuItem_Click;
            // 
            // hertz05
            // 
            hertz05.CheckOnClick = true;
            hertz05.Name = "hertz05";
            hertz05.Size = new System.Drawing.Size(224, 26);
            hertz05.Text = "0,5 hz";
            hertz05.Click += MenuItem_Click;
            // 
            // hertz03
            // 
            hertz03.CheckOnClick = true;
            hertz03.Name = "hertz03";
            hertz03.Size = new System.Drawing.Size(224, 26);
            hertz03.Text = "0,3 hz";
            hertz03.Click += MenuItem_Click;
            // 
            // hertz01
            // 
            hertz01.CheckOnClick = true;
            hertz01.Name = "hertz01";
            hertz01.Size = new System.Drawing.Size(224, 26);
            hertz01.Text = "0,1 hz";
            hertz01.Click += MenuItem_Click;
            // 
            // outroHigh
            // 
            outroHigh.CheckOnClick = true;
            outroHigh.Name = "outroHigh";
            outroHigh.Size = new System.Drawing.Size(224, 26);
            outroHigh.Text = "Outro";
            outroHigh.Click += MenuItem_Click;
            // 
            // NotchPassFilter
            // 
            NotchPassFilter.DropDownItems.AddRange(new ToolStripItem[] { NenhumNotch, hertz60N, hertz50N, OutroNotch });
            NotchPassFilter.Name = "NotchPassFilter";
            NotchPassFilter.Size = new System.Drawing.Size(210, 24);
            NotchPassFilter.Text = "Notch Pass Filter";
            NotchPassFilter.DropDownOpening += toolTripItemDropDown_OpeningNotch;
            // 
            // painelExames
            // 
            painelExames.Controls.Add(panel1);
            painelExames.Controls.Add(panel2);
            painelExames.Controls.Add(panel3);
            painelExames.Controls.Add(panel4);
            painelExames.Controls.Add(panel5);
            painelExames.Controls.Add(panel6);
            painelExames.Controls.Add(panel7);
            painelExames.Controls.Add(panel8);
            painelExames.Controls.Add(panel9);
            painelExames.Controls.Add(panel10);
            painelExames.Controls.Add(panel11);
            painelExames.Controls.Add(panel12);
            painelExames.Controls.Add(panel13);
            painelExames.Controls.Add(panel14);
            painelExames.Controls.Add(panel15);
            painelExames.Controls.Add(panel16);
            painelExames.Controls.Add(panel17);
            painelExames.Controls.Add(panel18);
            painelExames.Controls.Add(panel19);
            painelExames.Controls.Add(panel20);
            painelExames.Controls.Add(panel21);
            painelExames.Controls.Add(panel22);
            painelExames.Controls.Add(panel23);
            painelExames.Location = new System.Drawing.Point(2, 79);
            painelExames.Name = "painelExames";
            painelExames.Size = new System.Drawing.Size(117, 599);
            painelExames.TabIndex = 51;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(minusLb1);
            panel1.Controls.Add(plusLb1);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(scalaLb1);
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(117, 25);
            panel1.TabIndex = 78;
            // 
            // minusLb1
            // 
            minusLb1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb1.Location = new System.Drawing.Point(85, 10);
            minusLb1.Name = "minusLb1";
            minusLb1.Size = new System.Drawing.Size(22, 18);
            minusLb1.TabIndex = 15;
            minusLb1.Text = "-";
            minusLb1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb1.UseVisualStyleBackColor = true;
            minusLb1.Click += minusLb1_Click;
            minusLb1.MouseUp += MinusLb1_MouseUp;
            // 
            // plusLb1
            // 
            plusLb1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb1.Location = new System.Drawing.Point(85, -3);
            plusLb1.Name = "plusLb1";
            plusLb1.Size = new System.Drawing.Size(22, 18);
            plusLb1.TabIndex = 14;
            plusLb1.Text = "+";
            plusLb1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb1.UseVisualStyleBackColor = true;
            plusLb1.Click += plusLb1_Click;
            // 
            // scalaLb1
            // 
            scalaLb1.AutoSize = true;
            scalaLb1.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb1.Location = new System.Drawing.Point(60, 0);
            scalaLb1.Name = "scalaLb1";
            scalaLb1.Size = new System.Drawing.Size(29, 20);
            scalaLb1.TabIndex = 16;
            scalaLb1.Text = "1.0f";
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(minusLb2);
            panel2.Controls.Add(plusLb2);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(scalaLb2);
            panel2.Location = new System.Drawing.Point(0, 26);
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size(117, 25);
            panel2.TabIndex = 79;
            // 
            // minusLb2
            // 
            minusLb2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb2.Location = new System.Drawing.Point(85, 10);
            minusLb2.Name = "minusLb2";
            minusLb2.Size = new System.Drawing.Size(22, 18);
            minusLb2.TabIndex = 17;
            minusLb2.Text = "-";
            minusLb2.UseVisualStyleBackColor = true;
            minusLb2.Click += minusLb2_Click;
            // 
            // plusLb2
            // 
            plusLb2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb2.Location = new System.Drawing.Point(85, -3);
            plusLb2.Name = "plusLb2";
            plusLb2.Size = new System.Drawing.Size(22, 18);
            plusLb2.TabIndex = 16;
            plusLb2.Text = "+";
            plusLb2.UseVisualStyleBackColor = true;
            plusLb2.Click += plusLb2_Click;
            // 
            // scalaLb2
            // 
            scalaLb2.AutoSize = true;
            scalaLb2.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb2.Location = new System.Drawing.Point(60, 0);
            scalaLb2.Name = "scalaLb2";
            scalaLb2.Size = new System.Drawing.Size(29, 20);
            scalaLb2.TabIndex = 17;
            scalaLb2.Text = "1.0f";
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(minusLb3);
            panel3.Controls.Add(plusLb3);
            panel3.Controls.Add(scalaLb3);
            panel3.Controls.Add(label3);
            panel3.Location = new System.Drawing.Point(0, 52);
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size(117, 25);
            panel3.TabIndex = 80;
            // 
            // minusLb3
            // 
            minusLb3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb3.Location = new System.Drawing.Point(85, 10);
            minusLb3.Name = "minusLb3";
            minusLb3.Size = new System.Drawing.Size(22, 18);
            minusLb3.TabIndex = 19;
            minusLb3.Text = "-";
            minusLb3.UseVisualStyleBackColor = true;
            minusLb3.Click += minusLb3_Click;
            // 
            // plusLb3
            // 
            plusLb3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb3.Location = new System.Drawing.Point(85, -3);
            plusLb3.Name = "plusLb3";
            plusLb3.Size = new System.Drawing.Size(22, 18);
            plusLb3.TabIndex = 18;
            plusLb3.Text = "+";
            plusLb3.UseVisualStyleBackColor = true;
            plusLb3.Click += plusLb3_Click;
            // 
            // scalaLb3
            // 
            scalaLb3.AutoSize = true;
            scalaLb3.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb3.Location = new System.Drawing.Point(60, 0);
            scalaLb3.Name = "scalaLb3";
            scalaLb3.Size = new System.Drawing.Size(29, 20);
            scalaLb3.TabIndex = 18;
            scalaLb3.Text = "1.0f";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.Fixed3D;
            panel4.Controls.Add(minusLb4);
            panel4.Controls.Add(plusLb4);
            panel4.Controls.Add(scalaLb4);
            panel4.Controls.Add(label4);
            panel4.Location = new System.Drawing.Point(0, 78);
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size(117, 25);
            panel4.TabIndex = 80;
            // 
            // minusLb4
            // 
            minusLb4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb4.Location = new System.Drawing.Point(85, 10);
            minusLb4.Name = "minusLb4";
            minusLb4.Size = new System.Drawing.Size(22, 18);
            minusLb4.TabIndex = 21;
            minusLb4.Text = "-";
            minusLb4.UseVisualStyleBackColor = true;
            minusLb4.Click += minusLb4_Click;
            // 
            // plusLb4
            // 
            plusLb4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb4.Location = new System.Drawing.Point(85, -3);
            plusLb4.Name = "plusLb4";
            plusLb4.Size = new System.Drawing.Size(22, 18);
            plusLb4.TabIndex = 20;
            plusLb4.Text = "+";
            plusLb4.UseVisualStyleBackColor = true;
            plusLb4.Click += plusLb4_Click;
            // 
            // scalaLb4
            // 
            scalaLb4.AutoSize = true;
            scalaLb4.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb4.Location = new System.Drawing.Point(60, 0);
            scalaLb4.Name = "scalaLb4";
            scalaLb4.Size = new System.Drawing.Size(29, 20);
            scalaLb4.TabIndex = 20;
            scalaLb4.Text = "1.0f";
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.Fixed3D;
            panel5.Controls.Add(minusLb5);
            panel5.Controls.Add(plusLb5);
            panel5.Controls.Add(label5);
            panel5.Controls.Add(scalaLb5);
            panel5.Location = new System.Drawing.Point(0, 104);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(117, 25);
            panel5.TabIndex = 80;
            // 
            // minusLb5
            // 
            minusLb5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb5.Location = new System.Drawing.Point(85, 10);
            minusLb5.Name = "minusLb5";
            minusLb5.Size = new System.Drawing.Size(22, 18);
            minusLb5.TabIndex = 23;
            minusLb5.Text = "-";
            minusLb5.UseVisualStyleBackColor = true;
            minusLb5.Click += minusLb5_Click;
            // 
            // plusLb5
            // 
            plusLb5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb5.Location = new System.Drawing.Point(85, -3);
            plusLb5.Name = "plusLb5";
            plusLb5.Size = new System.Drawing.Size(22, 18);
            plusLb5.TabIndex = 22;
            plusLb5.Text = "+";
            plusLb5.UseVisualStyleBackColor = true;
            plusLb5.Click += plusLb5_Click;
            // 
            // scalaLb5
            // 
            scalaLb5.AutoSize = true;
            scalaLb5.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb5.Location = new System.Drawing.Point(60, 0);
            scalaLb5.Name = "scalaLb5";
            scalaLb5.Size = new System.Drawing.Size(29, 20);
            scalaLb5.TabIndex = 22;
            scalaLb5.Text = "1.0f";
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.Fixed3D;
            panel6.Controls.Add(minusLb6);
            panel6.Controls.Add(plusLb6);
            panel6.Controls.Add(label6);
            panel6.Controls.Add(scalaLb6);
            panel6.Location = new System.Drawing.Point(0, 130);
            panel6.Name = "panel6";
            panel6.Size = new System.Drawing.Size(117, 25);
            panel6.TabIndex = 80;
            // 
            // minusLb6
            // 
            minusLb6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb6.Location = new System.Drawing.Point(85, 10);
            minusLb6.Name = "minusLb6";
            minusLb6.Size = new System.Drawing.Size(22, 18);
            minusLb6.TabIndex = 30;
            minusLb6.Text = "-";
            minusLb6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb6.UseVisualStyleBackColor = true;
            minusLb6.Click += minusLb6_Click;
            // 
            // plusLb6
            // 
            plusLb6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb6.Location = new System.Drawing.Point(85, -3);
            plusLb6.Name = "plusLb6";
            plusLb6.Size = new System.Drawing.Size(22, 18);
            plusLb6.TabIndex = 29;
            plusLb6.Text = "+";
            plusLb6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb6.UseVisualStyleBackColor = true;
            plusLb6.Click += plusLb6_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label6.Location = new System.Drawing.Point(3, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(52, 24);
            label6.TabIndex = 24;
            label6.Text = "label6";
            // 
            // scalaLb6
            // 
            scalaLb6.AutoSize = true;
            scalaLb6.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb6.Location = new System.Drawing.Point(60, 0);
            scalaLb6.Name = "scalaLb6";
            scalaLb6.Size = new System.Drawing.Size(29, 20);
            scalaLb6.TabIndex = 24;
            scalaLb6.Text = "1.0f";
            // 
            // panel7
            // 
            panel7.BorderStyle = BorderStyle.Fixed3D;
            panel7.Controls.Add(plusLb7);
            panel7.Controls.Add(minusLb7);
            panel7.Controls.Add(label7);
            panel7.Controls.Add(scalaLb7);
            panel7.Location = new System.Drawing.Point(0, 156);
            panel7.Name = "panel7";
            panel7.Size = new System.Drawing.Size(117, 25);
            panel7.TabIndex = 80;
            // 
            // plusLb7
            // 
            plusLb7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb7.Location = new System.Drawing.Point(85, -3);
            plusLb7.Name = "plusLb7";
            plusLb7.Size = new System.Drawing.Size(22, 18);
            plusLb7.TabIndex = 32;
            plusLb7.Text = "-";
            plusLb7.UseVisualStyleBackColor = true;
            plusLb7.Click += plusLb7_Click;
            // 
            // minusLb7
            // 
            minusLb7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb7.Location = new System.Drawing.Point(85, 10);
            minusLb7.Name = "minusLb7";
            minusLb7.Size = new System.Drawing.Size(22, 18);
            minusLb7.TabIndex = 31;
            minusLb7.Text = "+";
            minusLb7.UseVisualStyleBackColor = true;
            minusLb7.Click += minusLb7_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label7.Location = new System.Drawing.Point(3, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 24);
            label7.TabIndex = 25;
            label7.Text = "label7";
            // 
            // scalaLb7
            // 
            scalaLb7.AutoSize = true;
            scalaLb7.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb7.Location = new System.Drawing.Point(60, 0);
            scalaLb7.Name = "scalaLb7";
            scalaLb7.Size = new System.Drawing.Size(29, 20);
            scalaLb7.TabIndex = 31;
            scalaLb7.Text = "1.0f";
            // 
            // panel8
            // 
            panel8.BorderStyle = BorderStyle.Fixed3D;
            panel8.Controls.Add(minusLb8);
            panel8.Controls.Add(plusLb8);
            panel8.Controls.Add(label8);
            panel8.Controls.Add(scalaLb8);
            panel8.Location = new System.Drawing.Point(0, 182);
            panel8.Name = "panel8";
            panel8.Size = new System.Drawing.Size(117, 25);
            panel8.TabIndex = 80;
            // 
            // minusLb8
            // 
            minusLb8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb8.Location = new System.Drawing.Point(85, 10);
            minusLb8.Name = "minusLb8";
            minusLb8.Size = new System.Drawing.Size(22, 18);
            minusLb8.TabIndex = 34;
            minusLb8.Text = "-";
            minusLb8.UseVisualStyleBackColor = true;
            minusLb8.Click += minusLb8_Click;
            // 
            // plusLb8
            // 
            plusLb8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb8.Location = new System.Drawing.Point(85, -3);
            plusLb8.Name = "plusLb8";
            plusLb8.Size = new System.Drawing.Size(22, 18);
            plusLb8.TabIndex = 33;
            plusLb8.Text = "+";
            plusLb8.UseVisualStyleBackColor = true;
            plusLb8.Click += plusLb8_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label8.Location = new System.Drawing.Point(3, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(52, 24);
            label8.TabIndex = 26;
            label8.Text = "label8";
            // 
            // scalaLb8
            // 
            scalaLb8.AutoSize = true;
            scalaLb8.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb8.Location = new System.Drawing.Point(60, 0);
            scalaLb8.Name = "scalaLb8";
            scalaLb8.Size = new System.Drawing.Size(29, 20);
            scalaLb8.TabIndex = 33;
            scalaLb8.Text = "1.0f";
            // 
            // panel9
            // 
            panel9.BorderStyle = BorderStyle.Fixed3D;
            panel9.Controls.Add(minusLb9);
            panel9.Controls.Add(plusLb9);
            panel9.Controls.Add(label9);
            panel9.Controls.Add(scalaLb9);
            panel9.Location = new System.Drawing.Point(0, 208);
            panel9.Name = "panel9";
            panel9.Size = new System.Drawing.Size(117, 25);
            panel9.TabIndex = 80;
            // 
            // minusLb9
            // 
            minusLb9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb9.Location = new System.Drawing.Point(85, 10);
            minusLb9.Name = "minusLb9";
            minusLb9.Size = new System.Drawing.Size(22, 18);
            minusLb9.TabIndex = 36;
            minusLb9.Text = "-";
            minusLb9.UseVisualStyleBackColor = true;
            minusLb9.Click += minusLb9_Click;
            // 
            // plusLb9
            // 
            plusLb9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb9.Location = new System.Drawing.Point(85, -3);
            plusLb9.Name = "plusLb9";
            plusLb9.Size = new System.Drawing.Size(22, 18);
            plusLb9.TabIndex = 35;
            plusLb9.Text = "+";
            plusLb9.UseVisualStyleBackColor = true;
            plusLb9.Click += plusLb9_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label9.Location = new System.Drawing.Point(3, 0);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(52, 24);
            label9.TabIndex = 27;
            label9.Text = "label9";
            // 
            // scalaLb9
            // 
            scalaLb9.AutoSize = true;
            scalaLb9.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb9.Location = new System.Drawing.Point(60, 0);
            scalaLb9.Name = "scalaLb9";
            scalaLb9.Size = new System.Drawing.Size(29, 20);
            scalaLb9.TabIndex = 35;
            scalaLb9.Text = "1.0f";
            // 
            // panel10
            // 
            panel10.BorderStyle = BorderStyle.Fixed3D;
            panel10.Controls.Add(minusLb10);
            panel10.Controls.Add(plusLb10);
            panel10.Controls.Add(label10);
            panel10.Controls.Add(scalaLb10);
            panel10.Location = new System.Drawing.Point(0, 234);
            panel10.Name = "panel10";
            panel10.Size = new System.Drawing.Size(117, 25);
            panel10.TabIndex = 80;
            // 
            // minusLb10
            // 
            minusLb10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb10.Location = new System.Drawing.Point(85, 10);
            minusLb10.Name = "minusLb10";
            minusLb10.Size = new System.Drawing.Size(22, 18);
            minusLb10.TabIndex = 38;
            minusLb10.Text = "-";
            minusLb10.UseVisualStyleBackColor = true;
            minusLb10.Click += minusLb10_Click;
            // 
            // plusLb10
            // 
            plusLb10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb10.Location = new System.Drawing.Point(85, -3);
            plusLb10.Name = "plusLb10";
            plusLb10.Size = new System.Drawing.Size(22, 18);
            plusLb10.TabIndex = 37;
            plusLb10.Text = "+";
            plusLb10.UseVisualStyleBackColor = true;
            plusLb10.Click += plusLb10_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label10.Location = new System.Drawing.Point(3, 0);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(61, 24);
            label10.TabIndex = 28;
            label10.Text = "label10";
            // 
            // scalaLb10
            // 
            scalaLb10.AutoSize = true;
            scalaLb10.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb10.Location = new System.Drawing.Point(60, 0);
            scalaLb10.Name = "scalaLb10";
            scalaLb10.Size = new System.Drawing.Size(29, 20);
            scalaLb10.TabIndex = 37;
            scalaLb10.Text = "1.0f";
            // 
            // panel11
            // 
            panel11.BorderStyle = BorderStyle.Fixed3D;
            panel11.Controls.Add(minusLb11);
            panel11.Controls.Add(plusLb11);
            panel11.Controls.Add(label11);
            panel11.Controls.Add(scalaLb11);
            panel11.Location = new System.Drawing.Point(0, 260);
            panel11.Name = "panel11";
            panel11.Size = new System.Drawing.Size(117, 25);
            panel11.TabIndex = 80;
            // 
            // minusLb11
            // 
            minusLb11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb11.Location = new System.Drawing.Point(85, 10);
            minusLb11.Name = "minusLb11";
            minusLb11.Size = new System.Drawing.Size(22, 18);
            minusLb11.TabIndex = 45;
            minusLb11.Text = "-";
            minusLb11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb11.UseVisualStyleBackColor = true;
            minusLb11.Click += minusLb11_Click;
            // 
            // plusLb11
            // 
            plusLb11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb11.Location = new System.Drawing.Point(85, -3);
            plusLb11.Name = "plusLb11";
            plusLb11.Size = new System.Drawing.Size(22, 18);
            plusLb11.TabIndex = 44;
            plusLb11.Text = "+";
            plusLb11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb11.UseVisualStyleBackColor = true;
            plusLb11.Click += plusLb11_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label11.Location = new System.Drawing.Point(3, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(61, 24);
            label11.TabIndex = 39;
            label11.Text = "label11";
            // 
            // scalaLb11
            // 
            scalaLb11.AutoSize = true;
            scalaLb11.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb11.Location = new System.Drawing.Point(60, 0);
            scalaLb11.Name = "scalaLb11";
            scalaLb11.Size = new System.Drawing.Size(29, 20);
            scalaLb11.TabIndex = 39;
            scalaLb11.Text = "1.0f";
            // 
            // panel12
            // 
            panel12.BorderStyle = BorderStyle.Fixed3D;
            panel12.Controls.Add(plusLb12);
            panel12.Controls.Add(minusLb12);
            panel12.Controls.Add(label12);
            panel12.Controls.Add(scalaLb12);
            panel12.Location = new System.Drawing.Point(0, 286);
            panel12.Name = "panel12";
            panel12.Size = new System.Drawing.Size(117, 25);
            panel12.TabIndex = 80;
            // 
            // plusLb12
            // 
            plusLb12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb12.Location = new System.Drawing.Point(85, -3);
            plusLb12.Name = "plusLb12";
            plusLb12.Size = new System.Drawing.Size(22, 18);
            plusLb12.TabIndex = 47;
            plusLb12.Text = "-";
            plusLb12.UseVisualStyleBackColor = true;
            plusLb12.Click += plusLb12_Click;
            // 
            // minusLb12
            // 
            minusLb12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb12.Location = new System.Drawing.Point(85, 10);
            minusLb12.Name = "minusLb12";
            minusLb12.Size = new System.Drawing.Size(22, 18);
            minusLb12.TabIndex = 46;
            minusLb12.Text = "+";
            minusLb12.UseVisualStyleBackColor = true;
            minusLb12.Click += minusLb12_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label12.Location = new System.Drawing.Point(3, 0);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(61, 24);
            label12.TabIndex = 40;
            label12.Text = "label12";
            // 
            // scalaLb12
            // 
            scalaLb12.AutoSize = true;
            scalaLb12.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb12.Location = new System.Drawing.Point(60, 0);
            scalaLb12.Name = "scalaLb12";
            scalaLb12.Size = new System.Drawing.Size(29, 20);
            scalaLb12.TabIndex = 46;
            scalaLb12.Text = "1.0f";
            // 
            // panel13
            // 
            panel13.BorderStyle = BorderStyle.Fixed3D;
            panel13.Controls.Add(minusLb13);
            panel13.Controls.Add(plusLb13);
            panel13.Controls.Add(label13);
            panel13.Controls.Add(scalaLb13);
            panel13.Location = new System.Drawing.Point(0, 312);
            panel13.Name = "panel13";
            panel13.Size = new System.Drawing.Size(117, 25);
            panel13.TabIndex = 80;
            // 
            // minusLb13
            // 
            minusLb13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb13.Location = new System.Drawing.Point(85, 10);
            minusLb13.Name = "minusLb13";
            minusLb13.Size = new System.Drawing.Size(22, 18);
            minusLb13.TabIndex = 49;
            minusLb13.Text = "-";
            minusLb13.UseVisualStyleBackColor = true;
            minusLb13.Click += minusLb13_Click;
            // 
            // plusLb13
            // 
            plusLb13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb13.Location = new System.Drawing.Point(85, -3);
            plusLb13.Name = "plusLb13";
            plusLb13.Size = new System.Drawing.Size(22, 18);
            plusLb13.TabIndex = 48;
            plusLb13.Text = "+";
            plusLb13.UseVisualStyleBackColor = true;
            plusLb13.Click += plusLb13_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label13.Location = new System.Drawing.Point(3, 0);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(61, 24);
            label13.TabIndex = 41;
            label13.Text = "label13";
            // 
            // scalaLb13
            // 
            scalaLb13.AutoSize = true;
            scalaLb13.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb13.Location = new System.Drawing.Point(60, 0);
            scalaLb13.Name = "scalaLb13";
            scalaLb13.Size = new System.Drawing.Size(29, 20);
            scalaLb13.TabIndex = 48;
            scalaLb13.Text = "1.0f";
            // 
            // panel14
            // 
            panel14.BorderStyle = BorderStyle.Fixed3D;
            panel14.Controls.Add(minusLb14);
            panel14.Controls.Add(plusLb14);
            panel14.Controls.Add(label14);
            panel14.Controls.Add(scalaLb14);
            panel14.Location = new System.Drawing.Point(0, 338);
            panel14.Name = "panel14";
            panel14.Size = new System.Drawing.Size(117, 25);
            panel14.TabIndex = 80;
            // 
            // minusLb14
            // 
            minusLb14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb14.Location = new System.Drawing.Point(85, 10);
            minusLb14.Name = "minusLb14";
            minusLb14.Size = new System.Drawing.Size(22, 18);
            minusLb14.TabIndex = 51;
            minusLb14.Text = "-";
            minusLb14.UseVisualStyleBackColor = true;
            minusLb14.Click += minusLb14_Click;
            // 
            // plusLb14
            // 
            plusLb14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb14.Location = new System.Drawing.Point(85, -3);
            plusLb14.Name = "plusLb14";
            plusLb14.Size = new System.Drawing.Size(22, 18);
            plusLb14.TabIndex = 50;
            plusLb14.Text = "+";
            plusLb14.UseVisualStyleBackColor = true;
            plusLb14.Click += plusLb14_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label14.Location = new System.Drawing.Point(3, 0);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(61, 24);
            label14.TabIndex = 42;
            label14.Text = "label14";
            // 
            // scalaLb14
            // 
            scalaLb14.AutoSize = true;
            scalaLb14.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb14.Location = new System.Drawing.Point(60, 0);
            scalaLb14.Name = "scalaLb14";
            scalaLb14.Size = new System.Drawing.Size(29, 20);
            scalaLb14.TabIndex = 50;
            scalaLb14.Text = "1.0f";
            // 
            // panel15
            // 
            panel15.BorderStyle = BorderStyle.Fixed3D;
            panel15.Controls.Add(minusLb15);
            panel15.Controls.Add(plusLb15);
            panel15.Controls.Add(label15);
            panel15.Controls.Add(scalaLb15);
            panel15.Location = new System.Drawing.Point(0, 363);
            panel15.Name = "panel15";
            panel15.Size = new System.Drawing.Size(117, 25);
            panel15.TabIndex = 80;
            // 
            // minusLb15
            // 
            minusLb15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb15.Location = new System.Drawing.Point(85, 10);
            minusLb15.Name = "minusLb15";
            minusLb15.Size = new System.Drawing.Size(22, 18);
            minusLb15.TabIndex = 53;
            minusLb15.Text = "-";
            minusLb15.UseVisualStyleBackColor = true;
            minusLb15.Click += minusLb15_Click;
            // 
            // plusLb15
            // 
            plusLb15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb15.Location = new System.Drawing.Point(85, -3);
            plusLb15.Name = "plusLb15";
            plusLb15.Size = new System.Drawing.Size(22, 18);
            plusLb15.TabIndex = 52;
            plusLb15.Text = "+";
            plusLb15.UseVisualStyleBackColor = true;
            plusLb15.Click += plusLb15_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label15.Location = new System.Drawing.Point(3, 0);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(61, 24);
            label15.TabIndex = 43;
            label15.Text = "label15";
            // 
            // scalaLb15
            // 
            scalaLb15.AutoSize = true;
            scalaLb15.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb15.Location = new System.Drawing.Point(60, 0);
            scalaLb15.Name = "scalaLb15";
            scalaLb15.Size = new System.Drawing.Size(29, 20);
            scalaLb15.TabIndex = 52;
            scalaLb15.Text = "1.0f";
            // 
            // panel16
            // 
            panel16.BorderStyle = BorderStyle.Fixed3D;
            panel16.Controls.Add(minusLb16);
            panel16.Controls.Add(plusLb16);
            panel16.Controls.Add(label16);
            panel16.Controls.Add(scalaLb16);
            panel16.Location = new System.Drawing.Point(0, 390);
            panel16.Name = "panel16";
            panel16.Size = new System.Drawing.Size(117, 25);
            panel16.TabIndex = 80;
            // 
            // minusLb16
            // 
            minusLb16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb16.Location = new System.Drawing.Point(85, 10);
            minusLb16.Name = "minusLb16";
            minusLb16.Size = new System.Drawing.Size(22, 18);
            minusLb16.TabIndex = 60;
            minusLb16.Text = "-";
            minusLb16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb16.UseVisualStyleBackColor = true;
            minusLb16.Click += minusLb16_Click;
            // 
            // plusLb16
            // 
            plusLb16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb16.Location = new System.Drawing.Point(85, -3);
            plusLb16.Name = "plusLb16";
            plusLb16.Size = new System.Drawing.Size(22, 18);
            plusLb16.TabIndex = 59;
            plusLb16.Text = "+";
            plusLb16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb16.UseVisualStyleBackColor = true;
            plusLb16.Click += plusLb16_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label16.Location = new System.Drawing.Point(3, 0);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(61, 24);
            label16.TabIndex = 54;
            label16.Text = "label16";
            // 
            // scalaLb16
            // 
            scalaLb16.AutoSize = true;
            scalaLb16.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb16.Location = new System.Drawing.Point(60, 0);
            scalaLb16.Name = "scalaLb16";
            scalaLb16.Size = new System.Drawing.Size(29, 20);
            scalaLb16.TabIndex = 54;
            scalaLb16.Text = "1.0f";
            // 
            // panel17
            // 
            panel17.BorderStyle = BorderStyle.Fixed3D;
            panel17.Controls.Add(plusLb17);
            panel17.Controls.Add(minusLb17);
            panel17.Controls.Add(label17);
            panel17.Controls.Add(scalaLb17);
            panel17.Location = new System.Drawing.Point(0, 416);
            panel17.Name = "panel17";
            panel17.Size = new System.Drawing.Size(117, 25);
            panel17.TabIndex = 80;
            // 
            // plusLb17
            // 
            plusLb17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb17.Location = new System.Drawing.Point(85, -3);
            plusLb17.Name = "plusLb17";
            plusLb17.Size = new System.Drawing.Size(22, 18);
            plusLb17.TabIndex = 62;
            plusLb17.Text = "-";
            plusLb17.UseVisualStyleBackColor = true;
            plusLb17.Click += plusLb17_Click;
            // 
            // minusLb17
            // 
            minusLb17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb17.Location = new System.Drawing.Point(85, 10);
            minusLb17.Name = "minusLb17";
            minusLb17.Size = new System.Drawing.Size(22, 18);
            minusLb17.TabIndex = 61;
            minusLb17.Text = "+";
            minusLb17.UseVisualStyleBackColor = true;
            minusLb17.Click += minusLb17_Click;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label17.Location = new System.Drawing.Point(3, 0);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(61, 24);
            label17.TabIndex = 55;
            label17.Text = "label17";
            // 
            // scalaLb17
            // 
            scalaLb17.AutoSize = true;
            scalaLb17.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb17.Location = new System.Drawing.Point(60, 0);
            scalaLb17.Name = "scalaLb17";
            scalaLb17.Size = new System.Drawing.Size(29, 20);
            scalaLb17.TabIndex = 61;
            scalaLb17.Text = "1.0f";
            // 
            // panel18
            // 
            panel18.BorderStyle = BorderStyle.Fixed3D;
            panel18.Controls.Add(minusLb18);
            panel18.Controls.Add(plusLb18);
            panel18.Controls.Add(label18);
            panel18.Controls.Add(scalaLb18);
            panel18.Location = new System.Drawing.Point(0, 442);
            panel18.Name = "panel18";
            panel18.Size = new System.Drawing.Size(117, 25);
            panel18.TabIndex = 81;
            // 
            // minusLb18
            // 
            minusLb18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb18.Location = new System.Drawing.Point(85, 10);
            minusLb18.Name = "minusLb18";
            minusLb18.Size = new System.Drawing.Size(22, 18);
            minusLb18.TabIndex = 64;
            minusLb18.Text = "-";
            minusLb18.UseVisualStyleBackColor = true;
            minusLb18.Click += minusLb18_Click;
            // 
            // plusLb18
            // 
            plusLb18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb18.Location = new System.Drawing.Point(85, -3);
            plusLb18.Name = "plusLb18";
            plusLb18.Size = new System.Drawing.Size(22, 18);
            plusLb18.TabIndex = 63;
            plusLb18.Text = "+";
            plusLb18.UseVisualStyleBackColor = true;
            plusLb18.Click += plusLb18_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label18.Location = new System.Drawing.Point(3, 0);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(61, 24);
            label18.TabIndex = 56;
            label18.Text = "label18";
            // 
            // scalaLb18
            // 
            scalaLb18.AutoSize = true;
            scalaLb18.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb18.Location = new System.Drawing.Point(60, 0);
            scalaLb18.Name = "scalaLb18";
            scalaLb18.Size = new System.Drawing.Size(29, 20);
            scalaLb18.TabIndex = 63;
            scalaLb18.Text = "1.0f";
            // 
            // panel19
            // 
            panel19.BorderStyle = BorderStyle.Fixed3D;
            panel19.Controls.Add(minusLb19);
            panel19.Controls.Add(plusLb19);
            panel19.Controls.Add(label19);
            panel19.Controls.Add(scalaLb19);
            panel19.Location = new System.Drawing.Point(0, 468);
            panel19.Name = "panel19";
            panel19.Size = new System.Drawing.Size(117, 25);
            panel19.TabIndex = 82;
            // 
            // minusLb19
            // 
            minusLb19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb19.Location = new System.Drawing.Point(85, 10);
            minusLb19.Name = "minusLb19";
            minusLb19.Size = new System.Drawing.Size(22, 18);
            minusLb19.TabIndex = 66;
            minusLb19.Text = "-";
            minusLb19.UseVisualStyleBackColor = true;
            minusLb19.Click += minusLb19_Click;
            // 
            // plusLb19
            // 
            plusLb19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb19.Location = new System.Drawing.Point(85, -3);
            plusLb19.Name = "plusLb19";
            plusLb19.Size = new System.Drawing.Size(22, 18);
            plusLb19.TabIndex = 65;
            plusLb19.Text = "+";
            plusLb19.UseVisualStyleBackColor = true;
            plusLb19.Click += plusLb19_Click;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label19.Location = new System.Drawing.Point(3, 0);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(61, 24);
            label19.TabIndex = 57;
            label19.Text = "label19";
            // 
            // scalaLb19
            // 
            scalaLb19.AutoSize = true;
            scalaLb19.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb19.Location = new System.Drawing.Point(60, 0);
            scalaLb19.Name = "scalaLb19";
            scalaLb19.Size = new System.Drawing.Size(29, 20);
            scalaLb19.TabIndex = 65;
            scalaLb19.Text = "1.0f";
            // 
            // panel20
            // 
            panel20.BorderStyle = BorderStyle.Fixed3D;
            panel20.Controls.Add(minusLb20);
            panel20.Controls.Add(plusLb20);
            panel20.Controls.Add(label20);
            panel20.Controls.Add(scalaLb20);
            panel20.Location = new System.Drawing.Point(0, 494);
            panel20.Name = "panel20";
            panel20.Size = new System.Drawing.Size(117, 25);
            panel20.TabIndex = 82;
            // 
            // minusLb20
            // 
            minusLb20.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb20.Location = new System.Drawing.Point(85, 10);
            minusLb20.Name = "minusLb20";
            minusLb20.Size = new System.Drawing.Size(22, 18);
            minusLb20.TabIndex = 68;
            minusLb20.Text = "-";
            minusLb20.UseVisualStyleBackColor = true;
            minusLb20.Click += minusLb20_Click;
            // 
            // plusLb20
            // 
            plusLb20.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb20.Location = new System.Drawing.Point(85, -3);
            plusLb20.Name = "plusLb20";
            plusLb20.Size = new System.Drawing.Size(22, 18);
            plusLb20.TabIndex = 67;
            plusLb20.Text = "+";
            plusLb20.UseVisualStyleBackColor = true;
            plusLb20.Click += plusLb20_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label20.Location = new System.Drawing.Point(3, 0);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(61, 24);
            label20.TabIndex = 58;
            label20.Text = "label20";
            // 
            // scalaLb20
            // 
            scalaLb20.AutoSize = true;
            scalaLb20.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb20.Location = new System.Drawing.Point(60, 0);
            scalaLb20.Name = "scalaLb20";
            scalaLb20.Size = new System.Drawing.Size(29, 20);
            scalaLb20.TabIndex = 67;
            scalaLb20.Text = "1.0f";
            // 
            // panel21
            // 
            panel21.BorderStyle = BorderStyle.Fixed3D;
            panel21.Controls.Add(minusLb21);
            panel21.Controls.Add(plusLb21);
            panel21.Controls.Add(label21);
            panel21.Controls.Add(scalaLb21);
            panel21.Location = new System.Drawing.Point(0, 520);
            panel21.Name = "panel21";
            panel21.Size = new System.Drawing.Size(117, 25);
            panel21.TabIndex = 82;
            // 
            // minusLb21
            // 
            minusLb21.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb21.Location = new System.Drawing.Point(85, 10);
            minusLb21.Name = "minusLb21";
            minusLb21.Size = new System.Drawing.Size(22, 18);
            minusLb21.TabIndex = 73;
            minusLb21.Text = "-";
            minusLb21.UseVisualStyleBackColor = true;
            minusLb21.Click += minusLb21_Click;
            // 
            // plusLb21
            // 
            plusLb21.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb21.Location = new System.Drawing.Point(85, -3);
            plusLb21.Name = "plusLb21";
            plusLb21.Size = new System.Drawing.Size(22, 18);
            plusLb21.TabIndex = 72;
            plusLb21.Text = "+";
            plusLb21.UseVisualStyleBackColor = true;
            plusLb21.Click += plusLb21_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label21.Location = new System.Drawing.Point(3, 0);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(61, 24);
            label21.TabIndex = 69;
            label21.Text = "label21";
            // 
            // scalaLb21
            // 
            scalaLb21.AutoSize = true;
            scalaLb21.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb21.Location = new System.Drawing.Point(60, 0);
            scalaLb21.Name = "scalaLb21";
            scalaLb21.Size = new System.Drawing.Size(29, 20);
            scalaLb21.TabIndex = 69;
            scalaLb21.Text = "1.0f";
            // 
            // panel22
            // 
            panel22.BorderStyle = BorderStyle.Fixed3D;
            panel22.Controls.Add(minusLb22);
            panel22.Controls.Add(plusLb22);
            panel22.Controls.Add(label22);
            panel22.Controls.Add(scalaLb22);
            panel22.Location = new System.Drawing.Point(0, 546);
            panel22.Name = "panel22";
            panel22.Size = new System.Drawing.Size(117, 25);
            panel22.TabIndex = 82;
            // 
            // minusLb22
            // 
            minusLb22.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb22.Location = new System.Drawing.Point(85, 10);
            minusLb22.Name = "minusLb22";
            minusLb22.Size = new System.Drawing.Size(22, 18);
            minusLb22.TabIndex = 75;
            minusLb22.Text = "-";
            minusLb22.UseVisualStyleBackColor = true;
            minusLb22.Click += minusLb22_Click;
            // 
            // plusLb22
            // 
            plusLb22.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb22.Location = new System.Drawing.Point(85, -3);
            plusLb22.Name = "plusLb22";
            plusLb22.Size = new System.Drawing.Size(22, 18);
            plusLb22.TabIndex = 74;
            plusLb22.Text = "+";
            plusLb22.UseVisualStyleBackColor = true;
            plusLb22.Click += plusLb22_Click;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label22.Location = new System.Drawing.Point(3, 0);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(61, 24);
            label22.TabIndex = 70;
            label22.Text = "label22";
            // 
            // scalaLb22
            // 
            scalaLb22.AutoSize = true;
            scalaLb22.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb22.Location = new System.Drawing.Point(60, 0);
            scalaLb22.Name = "scalaLb22";
            scalaLb22.Size = new System.Drawing.Size(29, 20);
            scalaLb22.TabIndex = 74;
            scalaLb22.Text = "1.0f";
            // 
            // panel23
            // 
            panel23.BorderStyle = BorderStyle.Fixed3D;
            panel23.Controls.Add(minusLb23);
            panel23.Controls.Add(label23);
            panel23.Controls.Add(plusLb23);
            panel23.Controls.Add(scalaLb23);
            panel23.Location = new System.Drawing.Point(0, 572);
            panel23.Name = "panel23";
            panel23.Size = new System.Drawing.Size(117, 25);
            panel23.TabIndex = 82;
            // 
            // minusLb23
            // 
            minusLb23.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb23.Location = new System.Drawing.Point(85, 10);
            minusLb23.Name = "minusLb23";
            minusLb23.Size = new System.Drawing.Size(22, 18);
            minusLb23.TabIndex = 77;
            minusLb23.Text = "-";
            minusLb23.UseVisualStyleBackColor = true;
            minusLb23.Click += minusLb23_Click;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new System.Drawing.Font("Arial Narrow", 12F);
            label23.Location = new System.Drawing.Point(3, 0);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(61, 24);
            label23.TabIndex = 71;
            label23.Text = "label23";
            // 
            // plusLb23
            // 
            plusLb23.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb23.Location = new System.Drawing.Point(85, -3);
            plusLb23.Name = "plusLb23";
            plusLb23.Size = new System.Drawing.Size(22, 18);
            plusLb23.TabIndex = 76;
            plusLb23.Text = "+";
            plusLb23.UseVisualStyleBackColor = true;
            plusLb23.Click += plusLb23_Click;
            // 
            // scalaLb23
            // 
            scalaLb23.AutoSize = true;
            scalaLb23.Font = new System.Drawing.Font("Arial Narrow", 9F);
            scalaLb23.Location = new System.Drawing.Point(60, 0);
            scalaLb23.Name = "scalaLb23";
            scalaLb23.Size = new System.Drawing.Size(29, 20);
            scalaLb23.TabIndex = 76;
            scalaLb23.Text = "1.0f";
            // 
            // painelTelaGl
            // 
            painelTelaGl.AutoSize = true;
            painelTelaGl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            painelTelaGl.Controls.Add(hScrollBar1);
            painelTelaGl.Dock = DockStyle.Bottom;
            painelTelaGl.Location = new System.Drawing.Point(0, 689);
            painelTelaGl.Margin = new Padding(0);
            painelTelaGl.Name = "painelTelaGl";
            painelTelaGl.Size = new System.Drawing.Size(1005, 32);
            painelTelaGl.TabIndex = 52;
            // 
            // painelComando
            // 
            painelComando.Controls.Add(MouseLoc);
            painelComando.Controls.Add(playSelect);
            painelComando.Controls.Add(minusAll);
            painelComando.Controls.Add(plusAll);
            painelComando.Controls.Add(qtdGraficos);
            painelComando.Controls.Add(ptsEmTela);
            painelComando.Controls.Add(inicioTela);
            painelComando.Controls.Add(fimTela);
            painelComando.Controls.Add(Play);
            painelComando.Controls.Add(tempoEmTela);
            painelComando.Controls.Add(MontagemBox);
            painelComando.Controls.Add(velocidadeScroll);
            painelComando.Location = new System.Drawing.Point(0, 2);
            painelComando.Name = "painelComando";
            painelComando.Size = new System.Drawing.Size(997, 65);
            painelComando.TabIndex = 53;
            // 
            // MouseLoc
            // 
            MouseLoc.Anchor = AnchorStyles.None;
            MouseLoc.Font = new System.Drawing.Font("Arial Narrow", 9F);
            MouseLoc.Location = new System.Drawing.Point(881, 35);
            MouseLoc.Name = "MouseLoc";
            MouseLoc.ReadOnly = true;
            MouseLoc.Size = new System.Drawing.Size(110, 25);
            MouseLoc.TabIndex = 58;
            MouseLoc.Text = "Loc";
            MouseLoc.TextAlign = HorizontalAlignment.Center;
            MouseLoc.TextChanged += textBox1_TextChanged;
            // 
            // playSelect
            // 
            playSelect.Location = new System.Drawing.Point(881, 4);
            playSelect.Name = "playSelect";
            playSelect.Size = new System.Drawing.Size(110, 29);
            playSelect.TabIndex = 57;
            playSelect.Text = "Montagem";
            playSelect.UseVisualStyleBackColor = true;
            playSelect.Click += playSelect_Click;
            // 
            // minusAll
            // 
            minusAll.Location = new System.Drawing.Point(5, 34);
            minusAll.Name = "minusAll";
            minusAll.Size = new System.Drawing.Size(41, 29);
            minusAll.TabIndex = 55;
            minusAll.Text = "-";
            minusAll.UseVisualStyleBackColor = true;
            minusAll.Click += minusAll_Click;
            // 
            // plusAll
            // 
            plusAll.Location = new System.Drawing.Point(52, 34);
            plusAll.Name = "plusAll";
            plusAll.Size = new System.Drawing.Size(41, 29);
            plusAll.TabIndex = 54;
            plusAll.Text = "+";
            plusAll.UseVisualStyleBackColor = true;
            plusAll.Click += plusAll_Click;
            // 
            // qtdGraficos
            // 
            qtdGraficos.Location = new System.Drawing.Point(396, 4);
            qtdGraficos.Name = "qtdGraficos";
            qtdGraficos.Size = new System.Drawing.Size(86, 27);
            qtdGraficos.TabIndex = 50;
            qtdGraficos.TextChanged += qtdGraficos_TextChanged;
            // 
            // timer1
            // 
            timer1.Interval = 20;
            timer1.Tick += timer1_Tick;            
            // 
            // timer2
            // 
            timer2.Tick += timer2_Tick;

            // 
            // OutroNotch
            // 
            OutroNotch.CheckOnClick = true;
            OutroNotch.Name = "OutroNotch";
            OutroNotch.Size = new System.Drawing.Size(224, 26);
            OutroNotch.Text = "Outro";
            OutroNotch.Click += MenuItem_Click;
            // 
            // hertz50N
            // 
            hertz50N.CheckOnClick = true;
            hertz50N.Name = "hertz50N";
            hertz50N.Size = new System.Drawing.Size(224, 26);
            hertz50N.Text = "50 hz";
            hertz50N.Click += MenuItem_Click;
            // 
            // hertz60N
            // 
            hertz60N.CheckOnClick = true;
            hertz60N.Name = "hertz60N";
            hertz60N.Size = new System.Drawing.Size(224, 26);
            hertz60N.Text = "60 hz";
            hertz60N.Click += MenuItem_Click;
            // 
            // NenhumNotch
            // 
            NenhumNotch.CheckOnClick = true;
            NenhumNotch.Name = "NenhumNotch";
            NenhumNotch.Size = new System.Drawing.Size(224, 26);
            NenhumNotch.Text = "Nenhum";
            NenhumNotch.Click += MenuItem_Click;
            // 
            // Tela_Plotagem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1005, 721);
            Controls.Add(painelExames);
            Controls.Add(painelTelaGl);
            Controls.Add(openglControl1);
            Controls.Add(painelComando);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Tela_Plotagem";
            Text = "Tela_Plotagem";
            Load += Tela_Plotagem_Load;
            ResizeBegin += Tela_Plotagem_ResizeBegin;
            ((System.ComponentModel.ISupportInitialize)openglControl1).EndInit();
            contextMenuStripOpenGl.ResumeLayout(false);
            contextMenuStripOpenGl.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            painelExames.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel16.ResumeLayout(false);
            panel16.PerformLayout();
            panel17.ResumeLayout(false);
            panel17.PerformLayout();
            panel18.ResumeLayout(false);
            panel18.PerformLayout();
            panel19.ResumeLayout(false);
            panel19.PerformLayout();
            panel20.ResumeLayout(false);
            panel20.PerformLayout();
            panel21.ResumeLayout(false);
            panel21.PerformLayout();
            panel22.ResumeLayout(false);
            panel22.PerformLayout();
            panel23.ResumeLayout(false);
            panel23.PerformLayout();
            painelTelaGl.ResumeLayout(false);
            painelComando.ResumeLayout(false);
            painelComando.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion
        public static ToolStripItem item1ToolStripMenuItem;
        public static ToolStripItem item2ToolStripMenuItem;
        public static ToolStripItem item3ToolStripMenuItem;
        public static ToolStripMenuItem NenhumLow1;
        public static TextBox MouseLoc;
        public static ToolTip toolTip1;
        public static Timer timer1;
        public static Timer timer2;
        public static ContextMenuStrip contextMenuStripOpenGl;
        public static ToolStripMenuItem OutroLow;
        public static ToolStripMenuItem LowPassFilter;
        public static ToolStripMenuItem HighPassFilter;
        public static ContextMenuStrip contextMenuStrip1;
        public static Button playSelect;
        public static Label scalaLb1;
        public static Panel panel23;
        public static Panel panel22;
        public static Panel panel21;
        public static Panel panel20;
        public static Panel panel19;
        public static Panel panel18;
        public static Panel panel17;
        public static Panel panel16;
        public static Panel panel15;
        public static Panel panel14;
        public static Panel panel13;
        public static Panel panel12;
        public static Panel panel11;
        public static Panel panel10;
        public static Panel panel9;
        public static Panel panel8;
        public static Panel panel7;
        public static Panel panel6;
        public static Panel panel5;
        public static Panel panel4;
        public static Panel panel2;
        public static Panel panel3;
        public static Panel panel1;
        public static Button plusAll;
        public static Button minusAll;
        public static HScrollBar hScrollBar1;
        public static TextBox ptsEmTela;
        public static TextBox inicioTela;
        public static TextBox fimTela;
        public static ComboBox tempoEmTela;
        public static ComboBox velocidadeScroll;
        public static ComboBox MontagemBox;
        public static Label label1;
        public static Label label2;
        public static Label label4;
        public static Label label3;
        public static Label label5;
        public static Button Play;
        public static Panel painelExames;
        public static Panel painelTelaGl;
        public static Panel painelComando;
        public static TextBox qtdGraficos;
        public static SharpGL.OpenGLControl openglControl1;
        public static Button minusLb5;
        public static Button plusLb5;
        public static Button minusLb4;
        public static Button plusLb4;
        public static Button minusLb3;
        public static Button plusLb3;
        public static Button minusLb2;
        public static Button plusLb2;
        public static Button minusLb1;
        public static Button plusLb1;
        public static Button minusLb10;
        public static Button plusLb10;
        public static Button minusLb9;
        public static Button plusLb9;
        public static Button minusLb8;
        public static Button plusLb8;
        public static Button plusLb7;
        public static Button minusLb7;
        public static Button minusLb6;
        public static Button plusLb6;
        public static Label label6;
        public static Label label7;
        public static Label label8;
        public static Label label9;
        public static Label label10;
        public static Button minusLb23;
        public static Button plusLb23;
        public static Button minusLb22;
        public static Button plusLb22;
        public static Button minusLb21;
        public static Button plusLb21;
        public static Label label21;
        public static Label label22;
        public static Label label23;
        public static Button minusLb20;
        public static Button plusLb20;
        public static Button minusLb19;
        public static Button plusLb19;
        public static Button minusLb18;
        public static Button plusLb18;
        public static Button plusLb17;
        public static Button minusLb17;
        public static Button minusLb16;
        public static Button plusLb16;
        public static Label label16;
        public static Label label17;
        public static Label label18;
        public static Label label19;
        public static Label label20;
        public static Button minusLb15;
        public static Button plusLb15;
        public static Button minusLb14;
        public static Button plusLb14;
        public static Button minusLb13;
        public static Button plusLb13;
        public static Button plusLb12;
        public static Button minusLb12;
        public static Button minusLb11;
        public static Button plusLb11;
        public static Label label11;
        public static Label label12;
        public static Label label13;
        public static Label label14;
        public static Label label15;
        public static Label scalaLb2;
        public static Label scalaLb3;
        public static Label scalaLb4;
        public static Label scalaLb5;
        public static Label scalaLb6;
        public static Label scalaLb7;
        public static Label scalaLb8;
        public static Label scalaLb9;
        public static Label scalaLb10;
        public static Label scalaLb11;
        public static Label scalaLb12;
        public static Label scalaLb13;
        public static Label scalaLb14;
        public static Label scalaLb15;
        public static Label scalaLb16;
        public static Label scalaLb17;
        public static Label scalaLb18;
        public static Label scalaLb19;
        public static Label scalaLb20;
        public static Label scalaLb21;
        public static Label scalaLb22;
        public static Label scalaLb23;
        public static ToolStripMenuItem NenhumLow;
        public static ToolStripMenuItem hertz70;
        public static ToolStripMenuItem hertz50;
        public static ToolStripMenuItem hertz40;
        public static ToolStripMenuItem hertz35;
        public static ToolStripMenuItem hertz30;
        public static ToolStripMenuItem hertz25;
        public static ToolStripMenuItem hertz20;
        public static ToolStripMenuItem hertz15;
        public static ToolStripMenuItem hertz10;
        public static ToolStripMenuItem hertz5;
        public static ToolStripMenuItem NenhumHigh;
        public static ToolStripMenuItem hertz10H;
        public static ToolStripMenuItem hertz7;
        public static ToolStripMenuItem hertz5H;
        public static ToolStripMenuItem hertz3;
        public static ToolStripMenuItem hertz1;
        public static ToolStripMenuItem hertz07;
        public static ToolStripMenuItem hertz05;
        public static ToolStripMenuItem hertz03;
        public static ToolStripMenuItem hertz01;
        public static ToolStripMenuItem outroHigh;
        public static ToolStripTextBox BomDia;
        public static ToolStripSeparator toolStripSeparator1;
        public static ToolStripTextBox BoaNoite;
        public static ToolStripMenuItem LowPassFilterGl;
        public static ToolStripMenuItem NenhumLowGl;
        public static ToolStripMenuItem hertz70Gl;
        public static ToolStripMenuItem hertz50Gl;
        public static ToolStripMenuItem hertz40Gl;
        public static ToolStripMenuItem hertz35Gl;
        public static ToolStripMenuItem hertz30Gl;
        public static ToolStripMenuItem hertz25Gl;
        public static ToolStripMenuItem hertz20Gl;
        public static ToolStripMenuItem hertz15Gl;
        public static ToolStripMenuItem hertz10Gl;
        public static ToolStripMenuItem hertz5Gl;
        public static ToolStripMenuItem OutroLowGl;
        public static ToolStripMenuItem HighPassFilterGl;
        public static ToolStripMenuItem NenhumHighGl;
        public static ToolStripMenuItem hertz10HGl;
        public static ToolStripMenuItem hertz7Gl;
        public static ToolStripMenuItem hertz5HGl;
        public static ToolStripMenuItem hertz3Gl;
        public static ToolStripMenuItem hertz1Gl;
        public static ToolStripMenuItem hertz07Gl;
        public static ToolStripMenuItem hertz05Gl;
        public static ToolStripMenuItem hertz03Gl;
        public static ToolStripMenuItem hertz01Gl;
        public static ToolStripMenuItem OutroHighGl;
        public static ToolStripMenuItem NenhumNotch;
        public static ToolStripMenuItem hertz60N;
        public static ToolStripMenuItem OutroNotch;
        public static ToolStripMenuItem hertz50N;
        public static ToolStripMenuItem NotchPassFilter;
        public static ToolStripMenuItem toolStripMenuItem3;
    }
}
