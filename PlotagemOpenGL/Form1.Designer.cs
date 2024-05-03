using Accord.IO;

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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            Play = new System.Windows.Forms.Button();
            openglControl1 = new SharpGL.OpenGLControl();
            painelExames = new System.Windows.Forms.Panel();
            minusLb23 = new System.Windows.Forms.Button();
            label23 = new System.Windows.Forms.Label();
            plusLb23 = new System.Windows.Forms.Button();
            minusLb22 = new System.Windows.Forms.Button();
            plusLb22 = new System.Windows.Forms.Button();
            minusLb21 = new System.Windows.Forms.Button();
            plusLb21 = new System.Windows.Forms.Button();
            label21 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            minusLb20 = new System.Windows.Forms.Button();
            plusLb20 = new System.Windows.Forms.Button();
            minusLb19 = new System.Windows.Forms.Button();
            plusLb19 = new System.Windows.Forms.Button();
            minusLb18 = new System.Windows.Forms.Button();
            plusLb18 = new System.Windows.Forms.Button();
            plusLb17 = new System.Windows.Forms.Button();
            minusLb17 = new System.Windows.Forms.Button();
            minusLb16 = new System.Windows.Forms.Button();
            plusLb16 = new System.Windows.Forms.Button();
            label16 = new System.Windows.Forms.Label();
            label17 = new System.Windows.Forms.Label();
            label18 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            minusLb15 = new System.Windows.Forms.Button();
            plusLb15 = new System.Windows.Forms.Button();
            minusLb14 = new System.Windows.Forms.Button();
            plusLb14 = new System.Windows.Forms.Button();
            minusLb13 = new System.Windows.Forms.Button();
            plusLb13 = new System.Windows.Forms.Button();
            plusLb12 = new System.Windows.Forms.Button();
            minusLb12 = new System.Windows.Forms.Button();
            minusLb11 = new System.Windows.Forms.Button();
            plusLb11 = new System.Windows.Forms.Button();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
            minusLb10 = new System.Windows.Forms.Button();
            plusLb10 = new System.Windows.Forms.Button();
            minusLb9 = new System.Windows.Forms.Button();
            plusLb9 = new System.Windows.Forms.Button();
            minusLb8 = new System.Windows.Forms.Button();
            plusLb8 = new System.Windows.Forms.Button();
            plusLb7 = new System.Windows.Forms.Button();
            minusLb7 = new System.Windows.Forms.Button();
            minusLb6 = new System.Windows.Forms.Button();
            plusLb6 = new System.Windows.Forms.Button();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            minusLb5 = new System.Windows.Forms.Button();
            plusLb5 = new System.Windows.Forms.Button();
            minusLb4 = new System.Windows.Forms.Button();
            plusLb4 = new System.Windows.Forms.Button();
            minusLb3 = new System.Windows.Forms.Button();
            plusLb3 = new System.Windows.Forms.Button();
            minusLb2 = new System.Windows.Forms.Button();
            plusLb2 = new System.Windows.Forms.Button();
            minusLb1 = new System.Windows.Forms.Button();
            plusLb1 = new System.Windows.Forms.Button();
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
            hScrollBar1.Location = new System.Drawing.Point(5, 1);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new System.Drawing.Size(992, 31);
            hScrollBar1.TabIndex = 0;
            hScrollBar1.Scroll += hScrollBar1_Scroll;
            hScrollBar1.KeyDown += TelaPlotagem_KeyDown;
            // 
            // ptsEmTela
            // 
            ptsEmTela.Anchor = System.Windows.Forms.AnchorStyles.None;
            ptsEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ptsEmTela.Location = new System.Drawing.Point(10, 5);
            ptsEmTela.Name = "ptsEmTela";
            ptsEmTela.ReadOnly = true;
            ptsEmTela.Size = new System.Drawing.Size(81, 25);
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
            inicioTela.Size = new System.Drawing.Size(72, 25);
            inicioTela.TabIndex = 3;
            inicioTela.Text = "00-00-00";
            inicioTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fimTela
            // 
            fimTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            fimTela.Location = new System.Drawing.Point(178, 3);
            fimTela.Name = "fimTela";
            fimTela.ReadOnly = true;
            fimTela.Size = new System.Drawing.Size(72, 25);
            fimTela.TabIndex = 4;
            fimTela.Text = "99-99-99";
            fimTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tempoEmTela
            // 
            tempoEmTela.DisplayMember = "8s";
            tempoEmTela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            tempoEmTela.FlatStyle = System.Windows.Forms.FlatStyle.System;
            tempoEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            tempoEmTela.FormatString = "N1";
            tempoEmTela.FormattingEnabled = true;
            tempoEmTela.IntegralHeight = false;
            tempoEmTela.Items.AddRange(new object[] { "8 seg", "12 seg", "30 seg", "60 seg", "90 seg", "120 seg", "240 seg" });
            tempoEmTela.Location = new System.Drawing.Point(271, 3);
            tempoEmTela.Name = "tempoEmTela";
            tempoEmTela.Size = new System.Drawing.Size(65, 28);
            tempoEmTela.TabIndex = 6;
            tempoEmTela.SelectedIndexChanged += tempoEmTela_SelectedIndexChanged;
            // 
            // velocidadeScroll
            // 
            velocidadeScroll.DisplayMember = "1.0x";
            velocidadeScroll.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            velocidadeScroll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            velocidadeScroll.FormattingEnabled = true;
            velocidadeScroll.IntegralHeight = false;
            velocidadeScroll.Items.AddRange(new object[] { "1.0x", "1.5x", "2.0x", "2.5x", "5.0x" });
            velocidadeScroll.Location = new System.Drawing.Point(342, 3);
            velocidadeScroll.Name = "velocidadeScroll";
            velocidadeScroll.Size = new System.Drawing.Size(63, 28);
            velocidadeScroll.TabIndex = 7;
            velocidadeScroll.SelectedIndexChanged += velocidadeScroll_SelectedIndexChanged;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { "Series" });
            comboBox3.Location = new System.Drawing.Point(634, 3);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(358, 28);
            comboBox3.TabIndex = 8;
            comboBox3.Text = "Series";
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(-500, 1);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(52, 24);
            label1.TabIndex = 9;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label2.Location = new System.Drawing.Point(-500, 27);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(52, 24);
            label2.TabIndex = 10;
            label2.Text = "label2";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label4.Location = new System.Drawing.Point(-500, 79);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(52, 24);
            label4.TabIndex = 12;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label3.Location = new System.Drawing.Point(-500, 53);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(52, 24);
            label3.TabIndex = 11;
            label3.Text = "label3";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label5.Location = new System.Drawing.Point(-500, 105);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(52, 24);
            label5.TabIndex = 13;
            label5.Text = "label5";
            // 
            // Play
            // 
            Play.Location = new System.Drawing.Point(564, 3);
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
            openglControl1.Scroll += hScrollBar1_Scroll;
            // 
            // painelExames
            // 
            painelExames.Controls.Add(minusLb23);
            painelExames.Controls.Add(label23);
            painelExames.Controls.Add(plusLb23);
            painelExames.Controls.Add(minusLb22);
            painelExames.Controls.Add(plusLb22);
            painelExames.Controls.Add(minusLb21);
            painelExames.Controls.Add(plusLb21);
            painelExames.Controls.Add(label21);
            painelExames.Controls.Add(label22);
            painelExames.Controls.Add(minusLb20);
            painelExames.Controls.Add(plusLb20);
            painelExames.Controls.Add(minusLb19);
            painelExames.Controls.Add(plusLb19);
            painelExames.Controls.Add(minusLb18);
            painelExames.Controls.Add(plusLb18);
            painelExames.Controls.Add(plusLb17);
            painelExames.Controls.Add(minusLb17);
            painelExames.Controls.Add(minusLb16);
            painelExames.Controls.Add(plusLb16);
            painelExames.Controls.Add(label16);
            painelExames.Controls.Add(label17);
            painelExames.Controls.Add(label18);
            painelExames.Controls.Add(label19);
            painelExames.Controls.Add(label20);
            painelExames.Controls.Add(minusLb15);
            painelExames.Controls.Add(plusLb15);
            painelExames.Controls.Add(minusLb14);
            painelExames.Controls.Add(plusLb14);
            painelExames.Controls.Add(minusLb13);
            painelExames.Controls.Add(plusLb13);
            painelExames.Controls.Add(plusLb12);
            painelExames.Controls.Add(minusLb12);
            painelExames.Controls.Add(minusLb11);
            painelExames.Controls.Add(plusLb11);
            painelExames.Controls.Add(label11);
            painelExames.Controls.Add(label12);
            painelExames.Controls.Add(label13);
            painelExames.Controls.Add(label14);
            painelExames.Controls.Add(label15);
            painelExames.Controls.Add(minusLb10);
            painelExames.Controls.Add(plusLb10);
            painelExames.Controls.Add(minusLb9);
            painelExames.Controls.Add(plusLb9);
            painelExames.Controls.Add(minusLb8);
            painelExames.Controls.Add(plusLb8);
            painelExames.Controls.Add(plusLb7);
            painelExames.Controls.Add(minusLb7);
            painelExames.Controls.Add(minusLb6);
            painelExames.Controls.Add(plusLb6);
            painelExames.Controls.Add(label6);
            painelExames.Controls.Add(label7);
            painelExames.Controls.Add(label8);
            painelExames.Controls.Add(label9);
            painelExames.Controls.Add(label10);
            painelExames.Controls.Add(minusLb5);
            painelExames.Controls.Add(plusLb5);
            painelExames.Controls.Add(minusLb4);
            painelExames.Controls.Add(plusLb4);
            painelExames.Controls.Add(minusLb3);
            painelExames.Controls.Add(plusLb3);
            painelExames.Controls.Add(minusLb2);
            painelExames.Controls.Add(plusLb2);
            painelExames.Controls.Add(minusLb1);
            painelExames.Controls.Add(plusLb1);
            painelExames.Controls.Add(label1);
            painelExames.Controls.Add(label2);
            painelExames.Controls.Add(label3);
            painelExames.Controls.Add(label4);
            painelExames.Controls.Add(label5);
            painelExames.Location = new System.Drawing.Point(2, 79);
            painelExames.Name = "painelExames";
            painelExames.Size = new System.Drawing.Size(172, 599);
            painelExames.TabIndex = 51;
            // 
            // minusLb23
            // 
            minusLb23.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb23.Location = new System.Drawing.Point(-500, 585);
            minusLb23.Name = "minusLb23";
            minusLb23.Size = new System.Drawing.Size(45, 13);
            minusLb23.TabIndex = 77;
            minusLb23.Text = "-";
            minusLb23.UseVisualStyleBackColor = true;
            minusLb23.Click += minusLb23_Click;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label23.Location = new System.Drawing.Point(-500, 573);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(61, 24);
            label23.TabIndex = 71;
            label23.Text = "label23";
            // 
            // plusLb23
            // 
            plusLb23.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb23.Location = new System.Drawing.Point(-500, 572);
            plusLb23.Name = "plusLb23";
            plusLb23.Size = new System.Drawing.Size(45, 13);
            plusLb23.TabIndex = 76;
            plusLb23.Text = "+";
            plusLb23.UseVisualStyleBackColor = true;
            plusLb23.Click += plusLb23_Click;
            // 
            // minusLb22
            // 
            minusLb22.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb22.Location = new System.Drawing.Point(-500, 559);
            minusLb22.Name = "minusLb22";
            minusLb22.Size = new System.Drawing.Size(45, 13);
            minusLb22.TabIndex = 75;
            minusLb22.Text = "-";
            minusLb22.UseVisualStyleBackColor = true;
            minusLb22.Click += minusLb22_Click;
            // 
            // plusLb22
            // 
            plusLb22.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb22.Location = new System.Drawing.Point(-500, 546);
            plusLb22.Name = "plusLb22";
            plusLb22.Size = new System.Drawing.Size(45, 13);
            plusLb22.TabIndex = 74;
            plusLb22.Text = "+";
            plusLb22.UseVisualStyleBackColor = true;
            plusLb22.Click += plusLb22_Click;
            // 
            // minusLb21
            // 
            minusLb21.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb21.Location = new System.Drawing.Point(-500, 533);
            minusLb21.Name = "minusLb21";
            minusLb21.Size = new System.Drawing.Size(45, 13);
            minusLb21.TabIndex = 73;
            minusLb21.Text = "-";
            minusLb21.UseVisualStyleBackColor = true;
            minusLb21.Click += minusLb21_Click;
            // 
            // plusLb21
            // 
            plusLb21.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb21.Location = new System.Drawing.Point(-500, 520);
            plusLb21.Name = "plusLb21";
            plusLb21.Size = new System.Drawing.Size(45, 13);
            plusLb21.TabIndex = 72;
            plusLb21.Text = "+";
            plusLb21.UseVisualStyleBackColor = true;
            plusLb21.Click += plusLb21_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label21.Location = new System.Drawing.Point(-500, 521);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(61, 24);
            label21.TabIndex = 69;
            label21.Text = "label21";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label22.Location = new System.Drawing.Point(-500, 547);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(61, 24);
            label22.TabIndex = 70;
            label22.Text = "label22";
            // 
            // minusLb20
            // 
            minusLb20.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb20.Location = new System.Drawing.Point(-500, 507);
            minusLb20.Name = "minusLb20";
            minusLb20.Size = new System.Drawing.Size(45, 13);
            minusLb20.TabIndex = 68;
            minusLb20.Text = "-";
            minusLb20.UseVisualStyleBackColor = true;
            minusLb20.Click += minusLb20_Click;
            // 
            // plusLb20
            // 
            plusLb20.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb20.Location = new System.Drawing.Point(-500, 494);
            plusLb20.Name = "plusLb20";
            plusLb20.Size = new System.Drawing.Size(45, 13);
            plusLb20.TabIndex = 67;
            plusLb20.Text = "+";
            plusLb20.UseVisualStyleBackColor = true;
            plusLb20.Click += plusLb20_Click;
            // 
            // minusLb19
            // 
            minusLb19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb19.Location = new System.Drawing.Point(-500, 481);
            minusLb19.Name = "minusLb19";
            minusLb19.Size = new System.Drawing.Size(45, 13);
            minusLb19.TabIndex = 66;
            minusLb19.Text = "-";
            minusLb19.UseVisualStyleBackColor = true;
            minusLb19.Click += minusLb19_Click;
            // 
            // plusLb19
            // 
            plusLb19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb19.Location = new System.Drawing.Point(-500, 468);
            plusLb19.Name = "plusLb19";
            plusLb19.Size = new System.Drawing.Size(45, 13);
            plusLb19.TabIndex = 65;
            plusLb19.Text = "+";
            plusLb19.UseVisualStyleBackColor = true;
            plusLb19.Click += plusLb19_Click;
            // 
            // minusLb18
            // 
            minusLb18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb18.Location = new System.Drawing.Point(-500, 455);
            minusLb18.Name = "minusLb18";
            minusLb18.Size = new System.Drawing.Size(45, 13);
            minusLb18.TabIndex = 64;
            minusLb18.Text = "-";
            minusLb18.UseVisualStyleBackColor = true;
            minusLb18.Click += minusLb18_Click;
            // 
            // plusLb18
            // 
            plusLb18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb18.Location = new System.Drawing.Point(-500, 442);
            plusLb18.Name = "plusLb18";
            plusLb18.Size = new System.Drawing.Size(45, 13);
            plusLb18.TabIndex = 63;
            plusLb18.Text = "+";
            plusLb18.UseVisualStyleBackColor = true;
            plusLb18.Click += plusLb18_Click;
            // 
            // plusLb17
            // 
            plusLb17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb17.Location = new System.Drawing.Point(-500, 416);
            plusLb17.Name = "plusLb17";
            plusLb17.Size = new System.Drawing.Size(45, 13);
            plusLb17.TabIndex = 62;
            plusLb17.Text = "-";
            plusLb17.UseVisualStyleBackColor = true;
            plusLb17.Click += plusLb17_Click;
            // 
            // minusLb17
            // 
            minusLb17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb17.Location = new System.Drawing.Point(-500, 429);
            minusLb17.Name = "minusLb17";
            minusLb17.Size = new System.Drawing.Size(45, 13);
            minusLb17.TabIndex = 61;
            minusLb17.Text = "+";
            minusLb17.UseVisualStyleBackColor = true;
            minusLb17.Click += minusLb17_Click;
            // 
            // minusLb16
            // 
            minusLb16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb16.Location = new System.Drawing.Point(-500, 403);
            minusLb16.Name = "minusLb16";
            minusLb16.Size = new System.Drawing.Size(45, 13);
            minusLb16.TabIndex = 60;
            minusLb16.Text = "-";
            minusLb16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb16.UseVisualStyleBackColor = true;
            minusLb16.Click += minusLb16_Click;
            // 
            // plusLb16
            // 
            plusLb16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb16.Location = new System.Drawing.Point(-500, 390);
            plusLb16.Name = "plusLb16";
            plusLb16.Size = new System.Drawing.Size(45, 13);
            plusLb16.TabIndex = 59;
            plusLb16.Text = "+";
            plusLb16.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb16.UseVisualStyleBackColor = true;
            plusLb16.Click += plusLb16_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label16.Location = new System.Drawing.Point(-500, 391);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(61, 24);
            label16.TabIndex = 54;
            label16.Text = "label16";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label17.Location = new System.Drawing.Point(-500, 417);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(61, 24);
            label17.TabIndex = 55;
            label17.Text = "label17";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label18.Location = new System.Drawing.Point(-500, 443);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(61, 24);
            label18.TabIndex = 56;
            label18.Text = "label18";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label19.Location = new System.Drawing.Point(-500, 469);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(61, 24);
            label19.TabIndex = 57;
            label19.Text = "label19";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label20.Location = new System.Drawing.Point(-500, 495);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(61, 24);
            label20.TabIndex = 58;
            label20.Text = "label20";
            // 
            // minusLb15
            // 
            minusLb15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb15.Location = new System.Drawing.Point(-500, 377);
            minusLb15.Name = "minusLb15";
            minusLb15.Size = new System.Drawing.Size(45, 13);
            minusLb15.TabIndex = 53;
            minusLb15.Text = "-";
            minusLb15.UseVisualStyleBackColor = true;
            minusLb15.Click += minusLb15_Click;
            // 
            // plusLb15
            // 
            plusLb15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb15.Location = new System.Drawing.Point(-500, 364);
            plusLb15.Name = "plusLb15";
            plusLb15.Size = new System.Drawing.Size(45, 13);
            plusLb15.TabIndex = 52;
            plusLb15.Text = "+";
            plusLb15.UseVisualStyleBackColor = true;
            plusLb15.Click += plusLb15_Click;
            // 
            // minusLb14
            // 
            minusLb14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb14.Location = new System.Drawing.Point(-500, 351);
            minusLb14.Name = "minusLb14";
            minusLb14.Size = new System.Drawing.Size(45, 13);
            minusLb14.TabIndex = 51;
            minusLb14.Text = "-";
            minusLb14.UseVisualStyleBackColor = true;
            minusLb14.Click += minusLb14_Click;
            // 
            // plusLb14
            // 
            plusLb14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb14.Location = new System.Drawing.Point(-500, 338);
            plusLb14.Name = "plusLb14";
            plusLb14.Size = new System.Drawing.Size(45, 13);
            plusLb14.TabIndex = 50;
            plusLb14.Text = "+";
            plusLb14.UseVisualStyleBackColor = true;
            plusLb14.Click += plusLb14_Click;
            // 
            // minusLb13
            // 
            minusLb13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb13.Location = new System.Drawing.Point(-500, 325);
            minusLb13.Name = "minusLb13";
            minusLb13.Size = new System.Drawing.Size(45, 13);
            minusLb13.TabIndex = 49;
            minusLb13.Text = "-";
            minusLb13.UseVisualStyleBackColor = true;
            minusLb13.Click += minusLb13_Click;
            // 
            // plusLb13
            // 
            plusLb13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb13.Location = new System.Drawing.Point(-500, 312);
            plusLb13.Name = "plusLb13";
            plusLb13.Size = new System.Drawing.Size(45, 13);
            plusLb13.TabIndex = 48;
            plusLb13.Text = "+";
            plusLb13.UseVisualStyleBackColor = true;
            plusLb13.Click += plusLb13_Click;
            // 
            // plusLb12
            // 
            plusLb12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb12.Location = new System.Drawing.Point(-500, 286);
            plusLb12.Name = "plusLb12";
            plusLb12.Size = new System.Drawing.Size(45, 13);
            plusLb12.TabIndex = 47;
            plusLb12.Text = "-";
            plusLb12.UseVisualStyleBackColor = true;
            plusLb12.Click += plusLb12_Click;
            // 
            // minusLb12
            // 
            minusLb12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb12.Location = new System.Drawing.Point(-500, 299);
            minusLb12.Name = "minusLb12";
            minusLb12.Size = new System.Drawing.Size(45, 13);
            minusLb12.TabIndex = 46;
            minusLb12.Text = "+";
            minusLb12.UseVisualStyleBackColor = true;
            minusLb12.Click += minusLb12_Click;
            // 
            // minusLb11
            // 
            minusLb11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb11.Location = new System.Drawing.Point(-500, 273);
            minusLb11.Name = "minusLb11";
            minusLb11.Size = new System.Drawing.Size(45, 13);
            minusLb11.TabIndex = 45;
            minusLb11.Text = "-";
            minusLb11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb11.UseVisualStyleBackColor = true;
            minusLb11.Click += minusLb11_Click;
            // 
            // plusLb11
            // 
            plusLb11.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb11.Location = new System.Drawing.Point(-500, 260);
            plusLb11.Name = "plusLb11";
            plusLb11.Size = new System.Drawing.Size(45, 13);
            plusLb11.TabIndex = 44;
            plusLb11.Text = "+";
            plusLb11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb11.UseVisualStyleBackColor = true;
            plusLb11.Click += plusLb11_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label11.Location = new System.Drawing.Point(-500, 261);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(61, 24);
            label11.TabIndex = 39;
            label11.Text = "label11";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label12.Location = new System.Drawing.Point(-500, 287);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(61, 24);
            label12.TabIndex = 40;
            label12.Text = "label12";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label13.Location = new System.Drawing.Point(-500, 313);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(61, 24);
            label13.TabIndex = 41;
            label13.Text = "label13";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label14.Location = new System.Drawing.Point(-500, 339);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(61, 24);
            label14.TabIndex = 42;
            label14.Text = "label14";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label15.Location = new System.Drawing.Point(-500, 365);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(61, 24);
            label15.TabIndex = 43;
            label15.Text = "label15";
            // 
            // minusLb10
            // 
            minusLb10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb10.Location = new System.Drawing.Point(-500, 247);
            minusLb10.Name = "minusLb10";
            minusLb10.Size = new System.Drawing.Size(45, 13);
            minusLb10.TabIndex = 38;
            minusLb10.Text = "-";
            minusLb10.UseVisualStyleBackColor = true;
            minusLb10.Click += minusLb10_Click;
            // 
            // plusLb10
            // 
            plusLb10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb10.Location = new System.Drawing.Point(-500, 234);
            plusLb10.Name = "plusLb10";
            plusLb10.Size = new System.Drawing.Size(45, 13);
            plusLb10.TabIndex = 37;
            plusLb10.Text = "+";
            plusLb10.UseVisualStyleBackColor = true;
            plusLb10.Click += plusLb10_Click;
            // 
            // minusLb9
            // 
            minusLb9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb9.Location = new System.Drawing.Point(-500, 221);
            minusLb9.Name = "minusLb9";
            minusLb9.Size = new System.Drawing.Size(45, 13);
            minusLb9.TabIndex = 36;
            minusLb9.Text = "-";
            minusLb9.UseVisualStyleBackColor = true;
            minusLb9.Click += minusLb9_Click;
            // 
            // plusLb9
            // 
            plusLb9.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb9.Location = new System.Drawing.Point(-500, 208);
            plusLb9.Name = "plusLb9";
            plusLb9.Size = new System.Drawing.Size(45, 13);
            plusLb9.TabIndex = 35;
            plusLb9.Text = "+";
            plusLb9.UseVisualStyleBackColor = true;
            plusLb9.Click += plusLb9_Click;
            // 
            // minusLb8
            // 
            minusLb8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb8.Location = new System.Drawing.Point(-500, 195);
            minusLb8.Name = "minusLb8";
            minusLb8.Size = new System.Drawing.Size(45, 13);
            minusLb8.TabIndex = 34;
            minusLb8.Text = "-";
            minusLb8.UseVisualStyleBackColor = true;
            minusLb8.Click += minusLb8_Click;
            // 
            // plusLb8
            // 
            plusLb8.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb8.Location = new System.Drawing.Point(-500, 182);
            plusLb8.Name = "plusLb8";
            plusLb8.Size = new System.Drawing.Size(45, 13);
            plusLb8.TabIndex = 33;
            plusLb8.Text = "+";
            plusLb8.UseVisualStyleBackColor = true;
            plusLb8.Click += plusLb8_Click;
            // 
            // plusLb7
            // 
            plusLb7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb7.Location = new System.Drawing.Point(-500, 156);
            plusLb7.Name = "plusLb7";
            plusLb7.Size = new System.Drawing.Size(45, 13);
            plusLb7.TabIndex = 32;
            plusLb7.Text = "-";
            plusLb7.UseVisualStyleBackColor = true;
            plusLb7.Click += plusLb7_Click;
            // 
            // minusLb7
            // 
            minusLb7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb7.Location = new System.Drawing.Point(-500, 169);
            minusLb7.Name = "minusLb7";
            minusLb7.Size = new System.Drawing.Size(45, 13);
            minusLb7.TabIndex = 31;
            minusLb7.Text = "+";
            minusLb7.UseVisualStyleBackColor = true;
            minusLb7.Click += minusLb7_Click;
            // 
            // minusLb6
            // 
            minusLb6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb6.Location = new System.Drawing.Point(-500, 143);
            minusLb6.Name = "minusLb6";
            minusLb6.Size = new System.Drawing.Size(45, 13);
            minusLb6.TabIndex = 30;
            minusLb6.Text = "-";
            minusLb6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb6.UseVisualStyleBackColor = true;
            minusLb6.Click += minusLb6_Click;
            // 
            // plusLb6
            // 
            plusLb6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb6.Location = new System.Drawing.Point(-500, 130);
            plusLb6.Name = "plusLb6";
            plusLb6.Size = new System.Drawing.Size(45, 13);
            plusLb6.TabIndex = 29;
            plusLb6.Text = "+";
            plusLb6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb6.UseVisualStyleBackColor = true;
            plusLb6.Click += plusLb6_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label6.Location = new System.Drawing.Point(-500, 131);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(52, 24);
            label6.TabIndex = 24;
            label6.Text = "label6";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label7.Location = new System.Drawing.Point(-500, 157);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(52, 24);
            label7.TabIndex = 25;
            label7.Text = "label7";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label8.Location = new System.Drawing.Point(-500, 183);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(52, 24);
            label8.TabIndex = 26;
            label8.Text = "label8";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label9.Location = new System.Drawing.Point(-500, 209);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(52, 24);
            label9.TabIndex = 27;
            label9.Text = "label9";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label10.Location = new System.Drawing.Point(-500, 235);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(61, 24);
            label10.TabIndex = 28;
            label10.Text = "label10";
            // 
            // minusLb5
            // 
            minusLb5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb5.Location = new System.Drawing.Point(-500, 117);
            minusLb5.Name = "minusLb5";
            minusLb5.Size = new System.Drawing.Size(45, 13);
            minusLb5.TabIndex = 23;
            minusLb5.Text = "-";
            minusLb5.UseVisualStyleBackColor = true;
            minusLb5.Click += minusLb5_Click;
            // 
            // plusLb5
            // 
            plusLb5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb5.Location = new System.Drawing.Point(-500, 104);
            plusLb5.Name = "plusLb5";
            plusLb5.Size = new System.Drawing.Size(45, 13);
            plusLb5.TabIndex = 22;
            plusLb5.Text = "+";
            plusLb5.UseVisualStyleBackColor = true;
            plusLb5.Click += plusLb5_Click;
            // 
            // minusLb4
            // 
            minusLb4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb4.Location = new System.Drawing.Point(-500, 91);
            minusLb4.Name = "minusLb4";
            minusLb4.Size = new System.Drawing.Size(45, 13);
            minusLb4.TabIndex = 21;
            minusLb4.Text = "-";
            minusLb4.UseVisualStyleBackColor = true;
            minusLb4.Click += minusLb4_Click;
            // 
            // plusLb4
            // 
            plusLb4.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb4.Location = new System.Drawing.Point(-500, 78);
            plusLb4.Name = "plusLb4";
            plusLb4.Size = new System.Drawing.Size(45, 13);
            plusLb4.TabIndex = 20;
            plusLb4.Text = "+";
            plusLb4.UseVisualStyleBackColor = true;
            plusLb4.Click += plusLb4_Click;
            // 
            // minusLb3
            // 
            minusLb3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb3.Location = new System.Drawing.Point(-500, 65);
            minusLb3.Name = "minusLb3";
            minusLb3.Size = new System.Drawing.Size(45, 13);
            minusLb3.TabIndex = 19;
            minusLb3.Text = "-";
            minusLb3.UseVisualStyleBackColor = true;
            minusLb3.Click += minusLb3_Click;
            // 
            // plusLb3
            // 
            plusLb3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb3.Location = new System.Drawing.Point(-500, 52);
            plusLb3.Name = "plusLb3";
            plusLb3.Size = new System.Drawing.Size(45, 13);
            plusLb3.TabIndex = 18;
            plusLb3.Text = "+";
            plusLb3.UseVisualStyleBackColor = true;
            plusLb3.Click += plusLb3_Click;
            // 
            // minusLb2
            // 
            minusLb2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb2.Location = new System.Drawing.Point(-500, 39);
            minusLb2.Name = "minusLb2";
            minusLb2.Size = new System.Drawing.Size(45, 13);
            minusLb2.TabIndex = 17;
            minusLb2.Text = "-";
            minusLb2.UseVisualStyleBackColor = true;
            minusLb2.Click += minusLb2_Click;
            // 
            // plusLb2
            // 
            plusLb2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb2.Location = new System.Drawing.Point(-500, 26);
            plusLb2.Name = "plusLb2";
            plusLb2.Size = new System.Drawing.Size(45, 13);
            plusLb2.TabIndex = 16;
            plusLb2.Text = "+";
            plusLb2.UseVisualStyleBackColor = true;
            plusLb2.Click += plusLb2_Click;
            // 
            // minusLb1
            // 
            minusLb1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb1.Location = new System.Drawing.Point(-500, 13);
            minusLb1.Name = "minusLb1";
            minusLb1.Size = new System.Drawing.Size(45, 13);
            minusLb1.TabIndex = 15;
            minusLb1.Text = "-";
            minusLb1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusLb1.UseVisualStyleBackColor = true;
            minusLb1.Click += minusLb1_Click;
            // 
            // plusLb1
            // 
            plusLb1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb1.Location = new System.Drawing.Point(-500, 0);
            plusLb1.Name = "plusLb1";
            plusLb1.Size = new System.Drawing.Size(45, 13);
            plusLb1.TabIndex = 14;
            plusLb1.Text = "+";
            plusLb1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusLb1.UseVisualStyleBackColor = true;
            plusLb1.Click += plusLb1_Click;
            // 
            // painelTelaGl
            // 
            painelTelaGl.AutoSize = true;
            painelTelaGl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            painelTelaGl.Controls.Add(hScrollBar1);
            painelTelaGl.Dock = System.Windows.Forms.DockStyle.Bottom;
            painelTelaGl.Location = new System.Drawing.Point(0, 689);
            painelTelaGl.Margin = new System.Windows.Forms.Padding(0);
            painelTelaGl.Name = "painelTelaGl";
            painelTelaGl.Size = new System.Drawing.Size(1006, 32);
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
            qtdGraficos.Location = new System.Drawing.Point(472, 3);
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

        public static System.Windows.Forms.HScrollBar hScrollBar1;
        public static System.Windows.Forms.TextBox ptsEmTela;
        public static System.Windows.Forms.TextBox inicioTela;
        public static System.Windows.Forms.TextBox fimTela;
        public static System.Windows.Forms.ComboBox tempoEmTela;
        public static System.Windows.Forms.ComboBox velocidadeScroll;
        public static System.Windows.Forms.ComboBox comboBox3;
        public static System.Windows.Forms.Label label1;
        public static System.Windows.Forms.Label label2;
        public static System.Windows.Forms.Label label4;
        public static System.Windows.Forms.Label label3;
        public static System.Windows.Forms.Label label5;
        public static System.Windows.Forms.Button Play;
        public static System.Windows.Forms.Panel painelExames;
        public static System.Windows.Forms.Panel painelTelaGl;
        public static System.Windows.Forms.Panel painelComando;
        public static System.Windows.Forms.TextBox qtdGraficos;
        public static SharpGL.OpenGLControl openglControl1;
        public static System.Windows.Forms.Button minusLb5;
        public static System.Windows.Forms.Button plusLb5;
        public static System.Windows.Forms.Button minusLb4;
        public static System.Windows.Forms.Button plusLb4;
        public static System.Windows.Forms.Button minusLb3;
        public static System.Windows.Forms.Button plusLb3;
        public static System.Windows.Forms.Button minusLb2;
        public static System.Windows.Forms.Button plusLb2;
        public static System.Windows.Forms.Button minusLb1;
        public static System.Windows.Forms.Button plusLb1;
        public static System.Windows.Forms.Button minusLb10;
        public static System.Windows.Forms.Button plusLb10;
        public static System.Windows.Forms.Button minusLb9;
        public static System.Windows.Forms.Button plusLb9;
        public static System.Windows.Forms.Button minusLb8;
        public static System.Windows.Forms.Button plusLb8;
        public static System.Windows.Forms.Button plusLb7;
        public static System.Windows.Forms.Button minusLb7;
        public static System.Windows.Forms.Button minusLb6;
        public static System.Windows.Forms.Button plusLb6;
        public static System.Windows.Forms.Label label6;
        public static System.Windows.Forms.Label label7;
        public static System.Windows.Forms.Label label8;
        public static System.Windows.Forms.Label label9;
        public static System.Windows.Forms.Label label10;
        public static System.Windows.Forms.Button minusLb23;
        public static System.Windows.Forms.Button plusLb23;
        public static System.Windows.Forms.Button minusLb22;
        public static System.Windows.Forms.Button plusLb22;
        public static System.Windows.Forms.Button minusLb21;
        public static System.Windows.Forms.Button plusLb21;
        public static System.Windows.Forms.Label label21;
        public static System.Windows.Forms.Label label22;
        public static System.Windows.Forms.Label label23;
        public static System.Windows.Forms.Button minusLb20;
        public static System.Windows.Forms.Button plusLb20;
        public static System.Windows.Forms.Button minusLb19;
        public static System.Windows.Forms.Button plusLb19;
        public static System.Windows.Forms.Button minusLb18;
        public static System.Windows.Forms.Button plusLb18;
        public static System.Windows.Forms.Button plusLb17;
        public static System.Windows.Forms.Button minusLb17;
        public static System.Windows.Forms.Button minusLb16;
        public static System.Windows.Forms.Button plusLb16;
        public static System.Windows.Forms.Label label16;
        public static System.Windows.Forms.Label label17;
        public static System.Windows.Forms.Label label18;
        public static System.Windows.Forms.Label label19;
        public static System.Windows.Forms.Label label20;
        public static System.Windows.Forms.Button minusLb15;
        public static System.Windows.Forms.Button plusLb15;
        public static System.Windows.Forms.Button minusLb14;
        public static System.Windows.Forms.Button plusLb14;
        public static System.Windows.Forms.Button minusLb13;
        public static System.Windows.Forms.Button plusLb13;
        public static System.Windows.Forms.Button plusLb12;
        public static System.Windows.Forms.Button minusLb12;
        public static System.Windows.Forms.Button minusLb11;
        public static System.Windows.Forms.Button plusLb11;
        public static System.Windows.Forms.Label label11;
        public static System.Windows.Forms.Label label12;
        public static System.Windows.Forms.Label label13;
        public static System.Windows.Forms.Label label14;
        public static System.Windows.Forms.Label label15;

    }
}
