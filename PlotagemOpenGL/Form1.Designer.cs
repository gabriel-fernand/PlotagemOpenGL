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
            hScrollBar1 = new System.Windows.Forms.HScrollBar();
            ptsEmTela = new System.Windows.Forms.TextBox();
            inicioTela = new System.Windows.Forms.TextBox();
            fimTela = new System.Windows.Forms.TextBox();
            tempoEmTela = new System.Windows.Forms.ComboBox();
            velocidadeScroll = new System.Windows.Forms.ComboBox();
            comboBox3 = new System.Windows.Forms.ComboBox();
            Canula = new System.Windows.Forms.Label();
            Fluxo = new System.Windows.Forms.Label();
            Ronco = new System.Windows.Forms.Label();
            Abdomen = new System.Windows.Forms.Label();
            SaturacaoO2 = new System.Windows.Forms.Label();
            Play = new System.Windows.Forms.Button();
            openglControl1 = new SharpGL.OpenGLControl();
            painelExames = new System.Windows.Forms.Panel();
            button31 = new System.Windows.Forms.Button();
            button32 = new System.Windows.Forms.Button();
            button33 = new System.Windows.Forms.Button();
            button34 = new System.Windows.Forms.Button();
            button35 = new System.Windows.Forms.Button();
            button36 = new System.Windows.Forms.Button();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            button11 = new System.Windows.Forms.Button();
            button12 = new System.Windows.Forms.Button();
            button13 = new System.Windows.Forms.Button();
            button14 = new System.Windows.Forms.Button();
            button15 = new System.Windows.Forms.Button();
            button16 = new System.Windows.Forms.Button();
            button17 = new System.Windows.Forms.Button();
            button18 = new System.Windows.Forms.Button();
            button19 = new System.Windows.Forms.Button();
            button20 = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            button21 = new System.Windows.Forms.Button();
            button22 = new System.Windows.Forms.Button();
            button23 = new System.Windows.Forms.Button();
            button24 = new System.Windows.Forms.Button();
            button25 = new System.Windows.Forms.Button();
            button26 = new System.Windows.Forms.Button();
            button27 = new System.Windows.Forms.Button();
            button28 = new System.Windows.Forms.Button();
            button29 = new System.Windows.Forms.Button();
            button30 = new System.Windows.Forms.Button();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            button6 = new System.Windows.Forms.Button();
            button7 = new System.Windows.Forms.Button();
            button8 = new System.Windows.Forms.Button();
            button9 = new System.Windows.Forms.Button();
            button10 = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            minusSatu = new System.Windows.Forms.Button();
            plusSatu = new System.Windows.Forms.Button();
            minusRonco = new System.Windows.Forms.Button();
            plusRonco = new System.Windows.Forms.Button();
            minusAbdomen = new System.Windows.Forms.Button();
            plusAbdomen = new System.Windows.Forms.Button();
            minusFluxo = new System.Windows.Forms.Button();
            plusFluxo = new System.Windows.Forms.Button();
            minusCanula = new System.Windows.Forms.Button();
            plusCanula = new System.Windows.Forms.Button();
            painelTelaGl = new System.Windows.Forms.Panel();
            painelComando = new System.Windows.Forms.Panel();
            qtdGraficos = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)openglControl1).BeginInit();
            painelExames.SuspendLayout();
            painelTelaGl.SuspendLayout();
            painelComando.SuspendLayout();
            SuspendLayout();
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new System.Drawing.Point(210, 3);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new System.Drawing.Size(787, 28);
            hScrollBar1.TabIndex = 0;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            hScrollBar1.KeyDown += TelaPlotagem_KeyDown;
            // 
            // ptsEmTela
            // 
            ptsEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ptsEmTela.Location = new System.Drawing.Point(3, 3);
            ptsEmTela.Name = "ptsEmTela";
            ptsEmTela.Size = new System.Drawing.Size(86, 25);
            ptsEmTela.TabIndex = 2;
            ptsEmTela.Text = "ptsEmTela";
            ptsEmTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // inicioTela
            // 
            inicioTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            inicioTela.Location = new System.Drawing.Point(100, 3);
            inicioTela.Name = "inicioTela";
            inicioTela.ReadOnly = true;
            inicioTela.Size = new System.Drawing.Size(119, 25);
            inicioTela.TabIndex = 3;
            inicioTela.Text = "00-00-00";
            inicioTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fimTela
            // 
            fimTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            fimTela.Location = new System.Drawing.Point(225, 3);
            fimTela.Name = "fimTela";
            fimTela.ReadOnly = true;
            fimTela.Size = new System.Drawing.Size(119, 25);
            fimTela.TabIndex = 4;
            fimTela.Text = "99-99-99";
            fimTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tempoEmTela
            // 
            tempoEmTela.DisplayMember = "8s";
            tempoEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tempoEmTela.FormatString = "N1";
            tempoEmTela.FormattingEnabled = true;
            tempoEmTela.Items.AddRange(new object[] { "8 seg", "12 seg", "30 seg", "60 seg", "90 seg", "120 seg", "240 seg" });
            tempoEmTela.Location = new System.Drawing.Point(361, 3);
            tempoEmTela.Name = "tempoEmTela";
            tempoEmTela.Size = new System.Drawing.Size(86, 28);
            tempoEmTela.TabIndex = 6;
            tempoEmTela.Text = "30 seg";
            tempoEmTela.SelectedIndexChanged += tempoEmTela_SelectedIndexChanged;
            // 
            // velocidadeScroll
            // 
            velocidadeScroll.DisplayMember = "1.0x";
            velocidadeScroll.FormattingEnabled = true;
            velocidadeScroll.Items.AddRange(new object[] { "1.0x", "1.5x", "2.0x", "2.5x", "5.0x" });
            velocidadeScroll.Location = new System.Drawing.Point(453, 3);
            velocidadeScroll.Name = "velocidadeScroll";
            velocidadeScroll.Size = new System.Drawing.Size(86, 28);
            velocidadeScroll.TabIndex = 7;
            velocidadeScroll.Text = "1.0x";
            velocidadeScroll.SelectedIndexChanged += velocidadeScroll_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Series" });
            comboBox3.Location = new System.Drawing.Point(684, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(451, 28);
            comboBox3.TabIndex = 8;
            comboBox3.Text = "Series";
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // Canula
            // 
            Canula.AutoSize = true;
            Canula.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Canula.Location = new System.Drawing.Point(3, 1);
            Canula.Name = "Canula";
            Canula.Size = new System.Drawing.Size(61, 24);
            Canula.TabIndex = 9;
            Canula.Text = "Canula";
            // 
            // Fluxo
            // 
            Fluxo.AutoSize = true;
            Fluxo.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Fluxo.Location = new System.Drawing.Point(3, 27);
            Fluxo.Name = "Fluxo";
            Fluxo.Size = new System.Drawing.Size(49, 24);
            Fluxo.TabIndex = 10;
            Fluxo.Text = "Fluxo";
            // 
            // Ronco
            // 
            Ronco.AutoSize = true;
            Ronco.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Ronco.Location = new System.Drawing.Point(3, 79);
            Ronco.Name = "Ronco";
            Ronco.Size = new System.Drawing.Size(57, 24);
            Ronco.TabIndex = 12;
            Ronco.Text = "Ronco";
            // 
            // Abdomen
            // 
            Abdomen.AutoSize = true;
            Abdomen.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Abdomen.Location = new System.Drawing.Point(3, 53);
            Abdomen.Name = "Abdomen";
            Abdomen.Size = new System.Drawing.Size(81, 24);
            Abdomen.TabIndex = 11;
            Abdomen.Text = "Abdomen";
            // 
            // SaturacaoO2
            // 
            SaturacaoO2.AutoSize = true;
            SaturacaoO2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            SaturacaoO2.Location = new System.Drawing.Point(3, 105);
            SaturacaoO2.Name = "SaturacaoO2";
            SaturacaoO2.Size = new System.Drawing.Size(111, 24);
            SaturacaoO2.TabIndex = 13;
            SaturacaoO2.Text = "Saturacao O2";
            // 
            // Play
            // 
            Play.Location = new System.Drawing.Point(641, 3);
            Play.Name = "Play";
            Play.Size = new System.Drawing.Size(41, 29);
            Play.TabIndex = 49;
            Play.Text = ">";
            Play.UseVisualStyleBackColor = true;
            Play.Click += Play_Click;
            // 
            // openglControl1
            // 
            openglControl1.DrawFPS = false;
            openglControl1.Location = new System.Drawing.Point(181, 79);
            openglControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            openglControl1.Name = "openglControl1";
            openglControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            openglControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            openglControl1.Size = new System.Drawing.Size(816, 599);
            openglControl1.TabIndex = 50;
            openglControl1.KeyDown += TelaPlotagem_KeyDown;
            openglControl1.MouseMove += openglControl1_MouseMove;
            // 
            // painelExames
            // 
            painelExames.Controls.Add(button31);
            painelExames.Controls.Add(button32);
            painelExames.Controls.Add(button33);
            painelExames.Controls.Add(button34);
            painelExames.Controls.Add(button35);
            painelExames.Controls.Add(button36);
            painelExames.Controls.Add(label16);
            painelExames.Controls.Add(label17);
            painelExames.Controls.Add(label18);
            painelExames.Controls.Add(button11);
            painelExames.Controls.Add(button12);
            painelExames.Controls.Add(button13);
            painelExames.Controls.Add(button14);
            painelExames.Controls.Add(button15);
            painelExames.Controls.Add(button16);
            painelExames.Controls.Add(button17);
            painelExames.Controls.Add(button18);
            painelExames.Controls.Add(button19);
            painelExames.Controls.Add(button20);
            painelExames.Controls.Add(label6);
            painelExames.Controls.Add(label7);
            painelExames.Controls.Add(label8);
            painelExames.Controls.Add(label9);
            painelExames.Controls.Add(label10);
            painelExames.Controls.Add(button21);
            painelExames.Controls.Add(button22);
            painelExames.Controls.Add(button23);
            painelExames.Controls.Add(button24);
            painelExames.Controls.Add(button25);
            painelExames.Controls.Add(button26);
            painelExames.Controls.Add(button27);
            painelExames.Controls.Add(button28);
            painelExames.Controls.Add(button29);
            painelExames.Controls.Add(button30);
            painelExames.Controls.Add(label11);
            painelExames.Controls.Add(label12);
            painelExames.Controls.Add(label13);
            painelExames.Controls.Add(label14);
            painelExames.Controls.Add(label15);
            painelExames.Controls.Add(button1);
            painelExames.Controls.Add(button2);
            painelExames.Controls.Add(button3);
            painelExames.Controls.Add(button4);
            painelExames.Controls.Add(button5);
            painelExames.Controls.Add(button6);
            painelExames.Controls.Add(button7);
            painelExames.Controls.Add(button8);
            painelExames.Controls.Add(button9);
            painelExames.Controls.Add(button10);
            painelExames.Controls.Add(label1);
            painelExames.Controls.Add(label2);
            painelExames.Controls.Add(label3);
            painelExames.Controls.Add(label4);
            painelExames.Controls.Add(label5);
            painelExames.Controls.Add(minusSatu);
            painelExames.Controls.Add(plusSatu);
            painelExames.Controls.Add(minusRonco);
            painelExames.Controls.Add(plusRonco);
            painelExames.Controls.Add(minusAbdomen);
            painelExames.Controls.Add(plusAbdomen);
            painelExames.Controls.Add(minusFluxo);
            painelExames.Controls.Add(plusFluxo);
            painelExames.Controls.Add(minusCanula);
            painelExames.Controls.Add(plusCanula);
            painelExames.Controls.Add(Canula);
            painelExames.Controls.Add(Fluxo);
            painelExames.Controls.Add(Abdomen);
            painelExames.Controls.Add(Ronco);
            painelExames.Controls.Add(SaturacaoO2);
            painelExames.Location = new System.Drawing.Point(2, 79);
            painelExames.Name = "painelExames";
            painelExames.Size = new System.Drawing.Size(172, 599);
            painelExames.TabIndex = 51;
            // 
            // button31
            // 
            button31.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button31.Location = new System.Drawing.Point(120, 585);
            button31.Name = "button31";
            button31.Size = new System.Drawing.Size(45, 13);
            button31.TabIndex = 77;
            button31.Text = "-";
            button31.UseVisualStyleBackColor = true;
            // 
            // button32
            // 
            button32.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button32.Location = new System.Drawing.Point(120, 572);
            button32.Name = "button32";
            button32.Size = new System.Drawing.Size(45, 13);
            button32.TabIndex = 76;
            button32.Text = "+";
            button32.UseVisualStyleBackColor = true;
            // 
            // button33
            // 
            button33.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button33.Location = new System.Drawing.Point(120, 559);
            button33.Name = "button33";
            button33.Size = new System.Drawing.Size(45, 13);
            button33.TabIndex = 75;
            button33.Text = "-";
            button33.UseVisualStyleBackColor = true;
            // 
            // button34
            // 
            button34.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button34.Location = new System.Drawing.Point(120, 546);
            button34.Name = "button34";
            button34.Size = new System.Drawing.Size(45, 13);
            button34.TabIndex = 74;
            button34.Text = "+";
            button34.UseVisualStyleBackColor = true;
            // 
            // button35
            // 
            button35.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button35.Location = new System.Drawing.Point(120, 533);
            button35.Name = "button35";
            button35.Size = new System.Drawing.Size(45, 13);
            button35.TabIndex = 73;
            button35.Text = "-";
            button35.UseVisualStyleBackColor = true;
            // 
            // button36
            // 
            button36.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button36.Location = new System.Drawing.Point(120, 520);
            button36.Name = "button36";
            button36.Size = new System.Drawing.Size(45, 13);
            button36.TabIndex = 72;
            button36.Text = "+";
            button36.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label16.Location = new System.Drawing.Point(3, 521);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(61, 24);
            label16.TabIndex = 69;
            label16.Text = "label16";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label17.Location = new System.Drawing.Point(3, 547);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(61, 24);
            label17.TabIndex = 70;
            label17.Text = "label17";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label18.Location = new System.Drawing.Point(3, 573);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(111, 24);
            label18.TabIndex = 71;
            label18.Text = "Saturacao O2";
            // 
            // button11
            // 
            button11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button11.Location = new System.Drawing.Point(120, 507);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(45, 13);
            button11.TabIndex = 68;
            button11.Text = "-";
            button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            button12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button12.Location = new System.Drawing.Point(120, 494);
            button12.Name = "button12";
            button12.Size = new System.Drawing.Size(45, 13);
            button12.TabIndex = 67;
            button12.Text = "+";
            button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button13.Location = new System.Drawing.Point(120, 481);
            button13.Name = "button13";
            button13.Size = new System.Drawing.Size(45, 13);
            button13.TabIndex = 66;
            button13.Text = "-";
            button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button14.Location = new System.Drawing.Point(120, 468);
            button14.Name = "button14";
            button14.Size = new System.Drawing.Size(45, 13);
            button14.TabIndex = 65;
            button14.Text = "+";
            button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            button15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button15.Location = new System.Drawing.Point(120, 455);
            button15.Name = "button15";
            button15.Size = new System.Drawing.Size(45, 13);
            button15.TabIndex = 64;
            button15.Text = "-";
            button15.UseVisualStyleBackColor = true;
            // 
            // button16
            // 
            button16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button16.Location = new System.Drawing.Point(120, 442);
            button16.Name = "button16";
            button16.Size = new System.Drawing.Size(45, 13);
            button16.TabIndex = 63;
            button16.Text = "+";
            button16.UseVisualStyleBackColor = true;
            // 
            // button17
            // 
            button17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button17.Location = new System.Drawing.Point(120, 416);
            button17.Name = "button17";
            button17.Size = new System.Drawing.Size(45, 13);
            button17.TabIndex = 62;
            button17.Text = "-";
            button17.UseVisualStyleBackColor = true;
            // 
            // button18
            // 
            button18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button18.Location = new System.Drawing.Point(120, 429);
            button18.Name = "button18";
            button18.Size = new System.Drawing.Size(45, 13);
            button18.TabIndex = 61;
            button18.Text = "+";
            button18.UseVisualStyleBackColor = true;
            // 
            // button19
            // 
            button19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button19.Location = new System.Drawing.Point(120, 403);
            button19.Name = "button19";
            button19.Size = new System.Drawing.Size(45, 13);
            button19.TabIndex = 60;
            button19.Text = "-";
            button19.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button19.UseVisualStyleBackColor = true;
            // 
            // button20
            // 
            button20.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button20.Location = new System.Drawing.Point(120, 390);
            button20.Name = "button20";
            button20.Size = new System.Drawing.Size(45, 13);
            button20.TabIndex = 59;
            button20.Text = "+";
            button20.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button20.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(3, 391);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(52, 24);
            label6.TabIndex = 54;
            label6.Text = "label6";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label7.Location = new System.Drawing.Point(3, 417);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 24);
            label7.TabIndex = 55;
            label7.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label8.Location = new System.Drawing.Point(3, 443);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(52, 24);
            label8.TabIndex = 56;
            label8.Text = "label8";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label9.Location = new System.Drawing.Point(3, 469);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(52, 24);
            label9.TabIndex = 57;
            label9.Text = "label9";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label10.Location = new System.Drawing.Point(3, 495);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(111, 24);
            label10.TabIndex = 58;
            label10.Text = "Saturacao O2";
            // 
            // button21
            // 
            button21.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button21.Location = new System.Drawing.Point(120, 377);
            button21.Name = "button21";
            button21.Size = new System.Drawing.Size(45, 13);
            button21.TabIndex = 53;
            button21.Text = "-";
            button21.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            button22.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button22.Location = new System.Drawing.Point(120, 364);
            button22.Name = "button22";
            button22.Size = new System.Drawing.Size(45, 13);
            button22.TabIndex = 52;
            button22.Text = "+";
            button22.UseVisualStyleBackColor = true;
            // 
            // button23
            // 
            button23.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button23.Location = new System.Drawing.Point(120, 351);
            button23.Name = "button23";
            button23.Size = new System.Drawing.Size(45, 13);
            button23.TabIndex = 51;
            button23.Text = "-";
            button23.UseVisualStyleBackColor = true;
            // 
            // button24
            // 
            button24.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button24.Location = new System.Drawing.Point(120, 338);
            button24.Name = "button24";
            button24.Size = new System.Drawing.Size(45, 13);
            button24.TabIndex = 50;
            button24.Text = "+";
            button24.UseVisualStyleBackColor = true;
            // 
            // button25
            // 
            button25.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button25.Location = new System.Drawing.Point(120, 325);
            button25.Name = "button25";
            button25.Size = new System.Drawing.Size(45, 13);
            button25.TabIndex = 49;
            button25.Text = "-";
            button25.UseVisualStyleBackColor = true;
            // 
            // button26
            // 
            button26.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button26.Location = new System.Drawing.Point(120, 312);
            button26.Name = "button26";
            button26.Size = new System.Drawing.Size(45, 13);
            button26.TabIndex = 48;
            button26.Text = "+";
            button26.UseVisualStyleBackColor = true;
            // 
            // button27
            // 
            button27.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button27.Location = new System.Drawing.Point(120, 286);
            button27.Name = "button27";
            button27.Size = new System.Drawing.Size(45, 13);
            button27.TabIndex = 47;
            button27.Text = "-";
            button27.UseVisualStyleBackColor = true;
            // 
            // button28
            // 
            button28.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button28.Location = new System.Drawing.Point(120, 299);
            button28.Name = "button28";
            button28.Size = new System.Drawing.Size(45, 13);
            button28.TabIndex = 46;
            button28.Text = "+";
            button28.UseVisualStyleBackColor = true;
            // 
            // button29
            // 
            button29.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button29.Location = new System.Drawing.Point(120, 273);
            button29.Name = "button29";
            button29.Size = new System.Drawing.Size(45, 13);
            button29.TabIndex = 45;
            button29.Text = "-";
            button29.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button29.UseVisualStyleBackColor = true;
            // 
            // button30
            // 
            button30.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button30.Location = new System.Drawing.Point(120, 260);
            button30.Name = "button30";
            button30.Size = new System.Drawing.Size(45, 13);
            button30.TabIndex = 44;
            button30.Text = "+";
            button30.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button30.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label11.Location = new System.Drawing.Point(3, 261);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(61, 24);
            label11.TabIndex = 39;
            label11.Text = "label11";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label12.Location = new System.Drawing.Point(3, 287);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(61, 24);
            label12.TabIndex = 40;
            label12.Text = "label12";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label13.Location = new System.Drawing.Point(3, 313);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(61, 24);
            label13.TabIndex = 41;
            label13.Text = "label13";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label14.Location = new System.Drawing.Point(3, 339);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(61, 24);
            label14.TabIndex = 42;
            label14.Text = "label14";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label15.Location = new System.Drawing.Point(3, 365);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(111, 24);
            label15.TabIndex = 43;
            label15.Text = "Saturacao O2";
            // 
            // button1
            // 
            button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button1.Location = new System.Drawing.Point(120, 247);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(45, 13);
            button1.TabIndex = 38;
            button1.Text = "-";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button2.Location = new System.Drawing.Point(120, 234);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(45, 13);
            button2.TabIndex = 37;
            button2.Text = "+";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button3.Location = new System.Drawing.Point(120, 221);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(45, 13);
            button3.TabIndex = 36;
            button3.Text = "-";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button4.Location = new System.Drawing.Point(120, 208);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(45, 13);
            button4.TabIndex = 35;
            button4.Text = "+";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button5.Location = new System.Drawing.Point(120, 195);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(45, 13);
            button5.TabIndex = 34;
            button5.Text = "-";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button6.Location = new System.Drawing.Point(120, 182);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(45, 13);
            button6.TabIndex = 33;
            button6.Text = "+";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button7.Location = new System.Drawing.Point(120, 156);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(45, 13);
            button7.TabIndex = 32;
            button7.Text = "-";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button8.Location = new System.Drawing.Point(120, 169);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(45, 13);
            button8.TabIndex = 31;
            button8.Text = "+";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button9.Location = new System.Drawing.Point(120, 143);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(45, 13);
            button9.TabIndex = 30;
            button9.Text = "-";
            button9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            button10.Location = new System.Drawing.Point(120, 130);
            button10.Name = "button10";
            button10.Size = new System.Drawing.Size(45, 13);
            button10.TabIndex = 29;
            button10.Text = "+";
            button10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            button10.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(3, 131);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 24);
            label1.TabIndex = 24;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(3, 157);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(52, 24);
            label2.TabIndex = 25;
            label2.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(3, 183);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 24);
            label3.TabIndex = 26;
            label3.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(3, 209);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 24);
            label4.TabIndex = 27;
            label4.Text = "label4";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(3, 235);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(111, 24);
            label5.TabIndex = 28;
            label5.Text = "Saturacao O2";
            // 
            // minusSatu
            // 
            minusSatu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusSatu.Location = new System.Drawing.Point(120, 117);
            minusSatu.Name = "minusSatu";
            minusSatu.Size = new System.Drawing.Size(45, 13);
            minusSatu.TabIndex = 23;
            minusSatu.Text = "-";
            minusSatu.UseVisualStyleBackColor = true;
            minusSatu.Click += minusSatu_Click;
            // 
            // plusSatu
            // 
            plusSatu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusSatu.Location = new System.Drawing.Point(120, 104);
            plusSatu.Name = "plusSatu";
            plusSatu.Size = new System.Drawing.Size(45, 13);
            plusSatu.TabIndex = 22;
            plusSatu.Text = "+";
            plusSatu.UseVisualStyleBackColor = true;
            plusSatu.Click += plusSatu_Click;
            // 
            // minusRonco
            // 
            minusRonco.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusRonco.Location = new System.Drawing.Point(120, 91);
            minusRonco.Name = "minusRonco";
            minusRonco.Size = new System.Drawing.Size(45, 13);
            minusRonco.TabIndex = 21;
            minusRonco.Text = "-";
            minusRonco.UseVisualStyleBackColor = true;
            minusRonco.Click += minusRonco_Click;
            // 
            // plusRonco
            // 
            plusRonco.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusRonco.Location = new System.Drawing.Point(120, 78);
            plusRonco.Name = "plusRonco";
            plusRonco.Size = new System.Drawing.Size(45, 13);
            plusRonco.TabIndex = 20;
            plusRonco.Text = "+";
            plusRonco.UseVisualStyleBackColor = true;
            plusRonco.Click += plusRonco_Click;
            // 
            // minusAbdomen
            // 
            minusAbdomen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusAbdomen.Location = new System.Drawing.Point(120, 65);
            minusAbdomen.Name = "minusAbdomen";
            minusAbdomen.Size = new System.Drawing.Size(45, 13);
            minusAbdomen.TabIndex = 19;
            minusAbdomen.Text = "-";
            minusAbdomen.UseVisualStyleBackColor = true;
            minusAbdomen.Click += minusAbdomen_Click;
            // 
            // plusAbdomen
            // 
            plusAbdomen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusAbdomen.Location = new System.Drawing.Point(120, 52);
            plusAbdomen.Name = "plusAbdomen";
            plusAbdomen.Size = new System.Drawing.Size(45, 13);
            plusAbdomen.TabIndex = 18;
            plusAbdomen.Text = "+";
            plusAbdomen.UseVisualStyleBackColor = true;
            plusAbdomen.Click += plusAbdomen_Click;
            // 
            // minusFluxo
            // 
            minusFluxo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusFluxo.Location = new System.Drawing.Point(120, 39);
            minusFluxo.Name = "minusFluxo";
            minusFluxo.Size = new System.Drawing.Size(45, 13);
            minusFluxo.TabIndex = 17;
            minusFluxo.Text = "-";
            minusFluxo.UseVisualStyleBackColor = true;
            minusFluxo.Click += minusFluxo_Click;
            // 
            // plusFluxo
            // 
            plusFluxo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusFluxo.Location = new System.Drawing.Point(120, 26);
            plusFluxo.Name = "plusFluxo";
            plusFluxo.Size = new System.Drawing.Size(45, 13);
            plusFluxo.TabIndex = 16;
            plusFluxo.Text = "+";
            plusFluxo.UseVisualStyleBackColor = true;
            plusFluxo.Click += plusFluxo_Click;
            // 
            // minusCanula
            // 
            minusCanula.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusCanula.Location = new System.Drawing.Point(120, 13);
            minusCanula.Name = "minusCanula";
            minusCanula.Size = new System.Drawing.Size(45, 13);
            minusCanula.TabIndex = 15;
            minusCanula.Text = "-";
            minusCanula.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusCanula.UseVisualStyleBackColor = true;
            minusCanula.Click += minusCanula_Click;
            // 
            // plusCanula
            // 
            plusCanula.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusCanula.Location = new System.Drawing.Point(120, 0);
            plusCanula.Name = "plusCanula";
            plusCanula.Size = new System.Drawing.Size(45, 13);
            plusCanula.TabIndex = 14;
            plusCanula.Text = "+";
            plusCanula.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusCanula.UseVisualStyleBackColor = true;
            plusCanula.Click += plusCanula_Click;
            // 
            // painelTelaGl
            // 
            painelTelaGl.AutoSize = true;
            painelTelaGl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            painelTelaGl.Controls.Add(hScrollBar1);
            painelTelaGl.Dock = System.Windows.Forms.DockStyle.Bottom;
            painelTelaGl.Location = new System.Drawing.Point(0, 690);
            painelTelaGl.Margin = new System.Windows.Forms.Padding(0);
            painelTelaGl.Name = "painelTelaGl";
            painelTelaGl.Size = new System.Drawing.Size(1006, 31);
            painelTelaGl.TabIndex = 52;
            // 
            // painelComando
            // 
            painelComando.Controls.Add(qtdGraficos);
            painelComando.Controls.Add(ptsEmTela);
            painelComando.Controls.Add(inicioTela);
            painelComando.Controls.Add(fimTela);
            painelComando.Controls.Add(Play);
            painelComando.Controls.Add(tempoEmTela);
            painelComando.Controls.Add(comboBox3);
            painelComando.Controls.Add(velocidadeScroll);
            painelComando.Location = new System.Drawing.Point(2, 2);
            painelComando.Name = "painelComando";
            painelComando.Size = new System.Drawing.Size(1761, 69);
            painelComando.TabIndex = 53;
            // 
            // qtdGraficos
            // 
            qtdGraficos.Location = new System.Drawing.Point(549, 3);
            qtdGraficos.Name = "qtdGraficos";
            qtdGraficos.Size = new System.Drawing.Size(86, 27);
            qtdGraficos.TabIndex = 50;
            qtdGraficos.TextChanged += qtdGraficos_TextChanged;
            // 
            // Tela_Plotagem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1006, 721);
            Controls.Add(painelExames);
            Controls.Add(painelTelaGl);
            Controls.Add(openglControl1);
            Controls.Add(painelComando);
            Name = "Tela_Plotagem";
            Text = "Tela_Plotagem";
            Load += Tela_Plotagem_Load;
            ((System.ComponentModel.ISupportInitialize)openglControl1).EndInit();
            painelExames.ResumeLayout(false);
            painelExames.PerformLayout();
            painelTelaGl.ResumeLayout(false);
            painelComando.ResumeLayout(false);
            painelComando.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.TextBox ptsEmTela;
        private System.Windows.Forms.TextBox inicioTela;
        private System.Windows.Forms.TextBox fimTela;
        private System.Windows.Forms.ComboBox tempoEmTela;
        private System.Windows.Forms.ComboBox velocidadeScroll;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label Canula;
        private System.Windows.Forms.Label Fluxo;
        private System.Windows.Forms.Label Ronco;
        private System.Windows.Forms.Label Abdomen;
        private System.Windows.Forms.Label SaturacaoO2;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Panel painelExames;
        private System.Windows.Forms.Panel painelTelaGl;
        private System.Windows.Forms.Panel painelComando;
        private System.Windows.Forms.TextBox qtdGraficos;
        public SharpGL.OpenGLControl openglControl1;
        private System.Windows.Forms.Button minusSatu;
        private System.Windows.Forms.Button plusSatu;
        private System.Windows.Forms.Button minusRonco;
        private System.Windows.Forms.Button plusRonco;
        private System.Windows.Forms.Button minusAbdomen;
        private System.Windows.Forms.Button plusAbdomen;
        private System.Windows.Forms.Button minusFluxo;
        private System.Windows.Forms.Button plusFluxo;
        private System.Windows.Forms.Button minusCanula;
        private System.Windows.Forms.Button plusCanula;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button31;
        private System.Windows.Forms.Button button32;
        private System.Windows.Forms.Button button33;
        private System.Windows.Forms.Button button34;
        private System.Windows.Forms.Button button35;
        private System.Windows.Forms.Button button36;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button26;
        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button28;
        private System.Windows.Forms.Button button29;
        private System.Windows.Forms.Button button30;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}
