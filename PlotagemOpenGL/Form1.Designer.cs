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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tela_Plotagem));
            hScrollBar1 = new System.Windows.Forms.HScrollBar();
            openglControl1 = new SharpGL.OpenGLControl();
            ptsEmTela = new System.Windows.Forms.TextBox();
            inicioTela = new System.Windows.Forms.TextBox();
            fimTela = new System.Windows.Forms.TextBox();
            tempoEmTela = new System.Windows.Forms.ComboBox();
            velocidadeScroll = new System.Windows.Forms.ComboBox();
            comboBox3 = new System.Windows.Forms.ComboBox();
            ExameExemplo1 = new System.Windows.Forms.Label();
            ExameExemplo2 = new System.Windows.Forms.Label();
            ExameExemplo4 = new System.Windows.Forms.Label();
            ExameExemplo3 = new System.Windows.Forms.Label();
            ExameExemplo8 = new System.Windows.Forms.Label();
            ExameExemplo7 = new System.Windows.Forms.Label();
            ExameExemplo6 = new System.Windows.Forms.Label();
            ExameExemplo5 = new System.Windows.Forms.Label();
            ExameExempl16 = new System.Windows.Forms.Label();
            ExameExempl15 = new System.Windows.Forms.Label();
            ExameExempl14 = new System.Windows.Forms.Label();
            ExameExempl13 = new System.Windows.Forms.Label();
            ExameExempl12 = new System.Windows.Forms.Label();
            ExameExempl11 = new System.Windows.Forms.Label();
            ExameExempl10 = new System.Windows.Forms.Label();
            ExameExemplo9 = new System.Windows.Forms.Label();
            ExameExempl20 = new System.Windows.Forms.Label();
            ExameExempl19 = new System.Windows.Forms.Label();
            ExameExempl18 = new System.Windows.Forms.Label();
            ExameExempl17 = new System.Windows.Forms.Label();
            Scalle1 = new System.Windows.Forms.VScrollBar();
            Scalle2 = new System.Windows.Forms.VScrollBar();
            Scalle3 = new System.Windows.Forms.VScrollBar();
            Scalle4 = new System.Windows.Forms.VScrollBar();
            Scalle5 = new System.Windows.Forms.VScrollBar();
            Scalle6 = new System.Windows.Forms.VScrollBar();
            Scalle7 = new System.Windows.Forms.VScrollBar();
            Scalle8 = new System.Windows.Forms.VScrollBar();
            Scalle9 = new System.Windows.Forms.VScrollBar();
            Scalle10 = new System.Windows.Forms.VScrollBar();
            Scalle11 = new System.Windows.Forms.VScrollBar();
            Scalle12 = new System.Windows.Forms.VScrollBar();
            Scalle13 = new System.Windows.Forms.VScrollBar();
            Scalle14 = new System.Windows.Forms.VScrollBar();
            Scalle15 = new System.Windows.Forms.VScrollBar();
            Scalle16 = new System.Windows.Forms.VScrollBar();
            Scalle18 = new System.Windows.Forms.VScrollBar();
            Scalle19 = new System.Windows.Forms.VScrollBar();
            Scalle20 = new System.Windows.Forms.VScrollBar();
            Scalle17 = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)openglControl1).BeginInit();
            SuspendLayout();
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new System.Drawing.Point(3, 969);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new System.Drawing.Size(1899, 28);
            hScrollBar1.TabIndex = 0;
            // 
            // openglControl1
            // 
            openglControl1.DrawFPS = false;
            openglControl1.Location = new System.Drawing.Point(265, 100);
            openglControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            openglControl1.Name = "openglControl1";
            openglControl1.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            openglControl1.RenderContextType = SharpGL.RenderContextType.DIBSection;
            openglControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            openglControl1.Size = new System.Drawing.Size(1637, 864);
            openglControl1.TabIndex = 1;
            // 
            // ptsEmTela
            // 
            ptsEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ptsEmTela.Location = new System.Drawing.Point(10, 25);
            ptsEmTela.Name = "ptsEmTela";
            ptsEmTela.Size = new System.Drawing.Size(86, 25);
            ptsEmTela.TabIndex = 2;
            ptsEmTela.Text = "ptsEmTela";
            ptsEmTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // inicioTela
            // 
            inicioTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            inicioTela.Location = new System.Drawing.Point(116, 25);
            inicioTela.Name = "inicioTela";
            inicioTela.Size = new System.Drawing.Size(119, 25);
            inicioTela.TabIndex = 3;
            inicioTela.Text = "00-00-00";
            inicioTela.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fimTela
            // 
            fimTela.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            fimTela.Location = new System.Drawing.Point(241, 25);
            fimTela.Name = "fimTela";
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
            tempoEmTela.Items.AddRange(new object[] { "8s", "12s", "30s", "60s", "90s", "120s" });
            tempoEmTela.Location = new System.Drawing.Point(389, 25);
            tempoEmTela.Name = "tempoEmTela";
            tempoEmTela.Size = new System.Drawing.Size(86, 28);
            tempoEmTela.TabIndex = 6;
            tempoEmTela.Text = "8s";
            // 
            // velocidadeScroll
            // 
            velocidadeScroll.DisplayMember = "1.0x";
            velocidadeScroll.FormattingEnabled = true;
            velocidadeScroll.Items.AddRange(new object[] { "1.0x", "1.5x", "2.0x", "2.5x", "5.0x" });
            velocidadeScroll.Location = new System.Drawing.Point(481, 25);
            velocidadeScroll.Name = "velocidadeScroll";
            velocidadeScroll.Size = new System.Drawing.Size(86, 28);
            velocidadeScroll.TabIndex = 7;
            velocidadeScroll.Text = "1.0x";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new System.Drawing.Point(775, 24);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(451, 28);
            comboBox3.TabIndex = 8;
            // 
            // ExameExemplo1
            // 
            ExameExemplo1.AutoSize = true;
            ExameExemplo1.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo1.Location = new System.Drawing.Point(10, 100);
            ExameExemplo1.Name = "ExameExemplo1";
            ExameExemplo1.Size = new System.Drawing.Size(231, 40);
            ExameExemplo1.TabIndex = 9;
            ExameExemplo1.Text = "ExameExemplo1";
            // 
            // ExameExemplo2
            // 
            ExameExemplo2.AutoSize = true;
            ExameExemplo2.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo2.Location = new System.Drawing.Point(10, 143);
            ExameExemplo2.Name = "ExameExemplo2";
            ExameExemplo2.Size = new System.Drawing.Size(231, 40);
            ExameExemplo2.TabIndex = 10;
            ExameExemplo2.Text = "ExameExemplo2";
            // 
            // ExameExemplo4
            // 
            ExameExemplo4.AutoSize = true;
            ExameExemplo4.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo4.Location = new System.Drawing.Point(10, 229);
            ExameExemplo4.Name = "ExameExemplo4";
            ExameExemplo4.Size = new System.Drawing.Size(231, 40);
            ExameExemplo4.TabIndex = 12;
            ExameExemplo4.Text = "ExameExemplo4";
            // 
            // ExameExemplo3
            // 
            ExameExemplo3.AutoSize = true;
            ExameExemplo3.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo3.Location = new System.Drawing.Point(10, 186);
            ExameExemplo3.Name = "ExameExemplo3";
            ExameExemplo3.Size = new System.Drawing.Size(231, 40);
            ExameExemplo3.TabIndex = 11;
            ExameExemplo3.Text = "ExameExemplo3";
            // 
            // ExameExemplo8
            // 
            ExameExemplo8.AutoSize = true;
            ExameExemplo8.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo8.Location = new System.Drawing.Point(10, 403);
            ExameExemplo8.Name = "ExameExemplo8";
            ExameExemplo8.Size = new System.Drawing.Size(231, 40);
            ExameExemplo8.TabIndex = 16;
            ExameExemplo8.Text = "ExameExemplo8";
            // 
            // ExameExemplo7
            // 
            ExameExemplo7.AutoSize = true;
            ExameExemplo7.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo7.Location = new System.Drawing.Point(10, 360);
            ExameExemplo7.Name = "ExameExemplo7";
            ExameExemplo7.Size = new System.Drawing.Size(231, 40);
            ExameExemplo7.TabIndex = 15;
            ExameExemplo7.Text = "ExameExemplo7";
            // 
            // ExameExemplo6
            // 
            ExameExemplo6.AutoSize = true;
            ExameExemplo6.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo6.Location = new System.Drawing.Point(10, 316);
            ExameExemplo6.Name = "ExameExemplo6";
            ExameExemplo6.Size = new System.Drawing.Size(231, 40);
            ExameExemplo6.TabIndex = 14;
            ExameExemplo6.Text = "ExameExemplo6";
            // 
            // ExameExemplo5
            // 
            ExameExemplo5.AutoSize = true;
            ExameExemplo5.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo5.Location = new System.Drawing.Point(10, 273);
            ExameExemplo5.Name = "ExameExemplo5";
            ExameExemplo5.Size = new System.Drawing.Size(231, 40);
            ExameExemplo5.TabIndex = 13;
            ExameExemplo5.Text = "ExameExemplo5";
            // 
            // ExameExempl16
            // 
            ExameExempl16.AutoSize = true;
            ExameExempl16.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl16.Location = new System.Drawing.Point(10, 749);
            ExameExempl16.Name = "ExameExempl16";
            ExameExempl16.Size = new System.Drawing.Size(231, 40);
            ExameExempl16.TabIndex = 24;
            ExameExempl16.Text = "ExameExempl16";
            // 
            // ExameExempl15
            // 
            ExameExempl15.AutoSize = true;
            ExameExempl15.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl15.Location = new System.Drawing.Point(10, 704);
            ExameExempl15.Name = "ExameExempl15";
            ExameExempl15.Size = new System.Drawing.Size(231, 40);
            ExameExempl15.TabIndex = 23;
            ExameExempl15.Text = "ExameExempl15";
            // 
            // ExameExempl14
            // 
            ExameExempl14.AutoSize = true;
            ExameExempl14.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl14.Location = new System.Drawing.Point(10, 660);
            ExameExempl14.Name = "ExameExempl14";
            ExameExempl14.Size = new System.Drawing.Size(231, 40);
            ExameExempl14.TabIndex = 22;
            ExameExempl14.Text = "ExameExempl14";
            // 
            // ExameExempl13
            // 
            ExameExempl13.AutoSize = true;
            ExameExempl13.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl13.Location = new System.Drawing.Point(10, 616);
            ExameExempl13.Name = "ExameExempl13";
            ExameExempl13.Size = new System.Drawing.Size(231, 40);
            ExameExempl13.TabIndex = 21;
            ExameExempl13.Text = "ExameExempl13";
            // 
            // ExameExempl12
            // 
            ExameExempl12.AutoSize = true;
            ExameExempl12.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl12.Location = new System.Drawing.Point(10, 572);
            ExameExempl12.Name = "ExameExempl12";
            ExameExempl12.Size = new System.Drawing.Size(231, 40);
            ExameExempl12.TabIndex = 20;
            ExameExempl12.Text = "ExameExempl12";
            // 
            // ExameExempl11
            // 
            ExameExempl11.AutoSize = true;
            ExameExempl11.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl11.Location = new System.Drawing.Point(10, 530);
            ExameExempl11.Name = "ExameExempl11";
            ExameExempl11.Size = new System.Drawing.Size(231, 40);
            ExameExempl11.TabIndex = 19;
            ExameExempl11.Text = "ExameExempl11";
            // 
            // ExameExempl10
            // 
            ExameExempl10.AutoSize = true;
            ExameExempl10.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl10.Location = new System.Drawing.Point(10, 489);
            ExameExempl10.Name = "ExameExempl10";
            ExameExempl10.Size = new System.Drawing.Size(231, 40);
            ExameExempl10.TabIndex = 18;
            ExameExempl10.Text = "ExameExempl10";
            // 
            // ExameExemplo9
            // 
            ExameExemplo9.AutoSize = true;
            ExameExemplo9.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo9.Location = new System.Drawing.Point(10, 446);
            ExameExemplo9.Name = "ExameExemplo9";
            ExameExemplo9.Size = new System.Drawing.Size(231, 40);
            ExameExemplo9.TabIndex = 17;
            ExameExemplo9.Text = "ExameExemplo9";
            // 
            // ExameExempl20
            // 
            ExameExempl20.AutoSize = true;
            ExameExempl20.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl20.Location = new System.Drawing.Point(10, 924);
            ExameExempl20.Name = "ExameExempl20";
            ExameExempl20.Size = new System.Drawing.Size(231, 40);
            ExameExempl20.TabIndex = 28;
            ExameExempl20.Text = "ExameExempl20";
            // 
            // ExameExempl19
            // 
            ExameExempl19.AutoSize = true;
            ExameExempl19.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl19.Location = new System.Drawing.Point(10, 881);
            ExameExempl19.Name = "ExameExempl19";
            ExameExempl19.Size = new System.Drawing.Size(231, 40);
            ExameExempl19.TabIndex = 27;
            ExameExempl19.Text = "ExameExempl19";
            // 
            // ExameExempl18
            // 
            ExameExempl18.AutoSize = true;
            ExameExempl18.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl18.Location = new System.Drawing.Point(10, 837);
            ExameExempl18.Name = "ExameExempl18";
            ExameExempl18.Size = new System.Drawing.Size(231, 40);
            ExameExempl18.TabIndex = 26;
            ExameExempl18.Text = "ExameExempl18";
            // 
            // ExameExempl17
            // 
            ExameExempl17.AutoSize = true;
            ExameExempl17.Font = new System.Drawing.Font("Arial Narrow", 19.8000011F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl17.Location = new System.Drawing.Point(10, 793);
            ExameExempl17.Name = "ExameExempl17";
            ExameExempl17.Size = new System.Drawing.Size(231, 40);
            ExameExempl17.TabIndex = 25;
            ExameExempl17.Text = "ExameExempl17";
            // 
            // Scalle1
            // 
            Scalle1.Location = new System.Drawing.Point(238, 100);
            Scalle1.Name = "Scalle1";
            Scalle1.Size = new System.Drawing.Size(19, 40);
            Scalle1.TabIndex = 29;
            // 
            // Scalle2
            // 
            Scalle2.Location = new System.Drawing.Point(238, 143);
            Scalle2.Name = "Scalle2";
            Scalle2.Size = new System.Drawing.Size(19, 40);
            Scalle2.TabIndex = 30;
            // 
            // Scalle3
            // 
            Scalle3.Location = new System.Drawing.Point(238, 186);
            Scalle3.Name = "Scalle3";
            Scalle3.Size = new System.Drawing.Size(19, 40);
            Scalle3.TabIndex = 31;
            // 
            // Scalle4
            // 
            Scalle4.Location = new System.Drawing.Point(238, 229);
            Scalle4.Name = "Scalle4";
            Scalle4.Size = new System.Drawing.Size(19, 40);
            Scalle4.TabIndex = 32;
            // 
            // Scalle5
            // 
            Scalle5.Location = new System.Drawing.Point(238, 273);
            Scalle5.Name = "Scalle5";
            Scalle5.Size = new System.Drawing.Size(19, 40);
            Scalle5.TabIndex = 33;
            // 
            // Scalle6
            // 
            Scalle6.Location = new System.Drawing.Point(238, 316);
            Scalle6.Name = "Scalle6";
            Scalle6.Size = new System.Drawing.Size(19, 40);
            Scalle6.TabIndex = 34;
            // 
            // Scalle7
            // 
            Scalle7.Location = new System.Drawing.Point(238, 360);
            Scalle7.Name = "Scalle7";
            Scalle7.Size = new System.Drawing.Size(19, 40);
            Scalle7.TabIndex = 35;
            // 
            // Scalle8
            // 
            Scalle8.Location = new System.Drawing.Point(238, 403);
            Scalle8.Name = "Scalle8";
            Scalle8.Size = new System.Drawing.Size(19, 40);
            Scalle8.TabIndex = 36;
            // 
            // Scalle9
            // 
            Scalle9.Location = new System.Drawing.Point(238, 446);
            Scalle9.Name = "Scalle9";
            Scalle9.Size = new System.Drawing.Size(19, 40);
            Scalle9.TabIndex = 37;
            // 
            // Scalle10
            // 
            Scalle10.Location = new System.Drawing.Point(238, 489);
            Scalle10.Name = "Scalle10";
            Scalle10.Size = new System.Drawing.Size(19, 40);
            Scalle10.TabIndex = 38;
            // 
            // Scalle11
            // 
            Scalle11.Location = new System.Drawing.Point(238, 530);
            Scalle11.Name = "Scalle11";
            Scalle11.Size = new System.Drawing.Size(19, 40);
            Scalle11.TabIndex = 39;
            // 
            // Scalle12
            // 
            Scalle12.Location = new System.Drawing.Point(238, 572);
            Scalle12.Name = "Scalle12";
            Scalle12.Size = new System.Drawing.Size(19, 40);
            Scalle12.TabIndex = 40;
            // 
            // Scalle13
            // 
            Scalle13.Location = new System.Drawing.Point(238, 616);
            Scalle13.Name = "Scalle13";
            Scalle13.Size = new System.Drawing.Size(19, 40);
            Scalle13.TabIndex = 41;
            // 
            // Scalle14
            // 
            Scalle14.Location = new System.Drawing.Point(238, 660);
            Scalle14.Name = "Scalle14";
            Scalle14.Size = new System.Drawing.Size(19, 40);
            Scalle14.TabIndex = 42;
            // 
            // Scalle15
            // 
            Scalle15.Location = new System.Drawing.Point(238, 704);
            Scalle15.Name = "Scalle15";
            Scalle15.Size = new System.Drawing.Size(19, 40);
            Scalle15.TabIndex = 43;
            // 
            // Scalle16
            // 
            Scalle16.Location = new System.Drawing.Point(238, 749);
            Scalle16.Name = "Scalle16";
            Scalle16.Size = new System.Drawing.Size(19, 40);
            Scalle16.TabIndex = 44;
            // 
            // Scalle18
            // 
            Scalle18.Location = new System.Drawing.Point(238, 837);
            Scalle18.Name = "Scalle18";
            Scalle18.Size = new System.Drawing.Size(19, 40);
            Scalle18.TabIndex = 45;
            // 
            // Scalle19
            // 
            Scalle19.Location = new System.Drawing.Point(238, 881);
            Scalle19.Name = "Scalle19";
            Scalle19.Size = new System.Drawing.Size(19, 40);
            Scalle19.TabIndex = 46;
            // 
            // Scalle20
            // 
            Scalle20.Location = new System.Drawing.Point(238, 924);
            Scalle20.Name = "Scalle20";
            Scalle20.Size = new System.Drawing.Size(19, 40);
            Scalle20.TabIndex = 47;
            // 
            // Scalle17
            // 
            Scalle17.Location = new System.Drawing.Point(238, 793);
            Scalle17.Name = "Scalle17";
            Scalle17.Size = new System.Drawing.Size(19, 40);
            Scalle17.TabIndex = 48;
            // 
            // Tela_Plotagem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1902, 1003);
            Controls.Add(Scalle17);
            Controls.Add(Scalle20);
            Controls.Add(Scalle19);
            Controls.Add(Scalle18);
            Controls.Add(Scalle16);
            Controls.Add(Scalle15);
            Controls.Add(Scalle14);
            Controls.Add(Scalle13);
            Controls.Add(Scalle12);
            Controls.Add(Scalle11);
            Controls.Add(Scalle10);
            Controls.Add(Scalle9);
            Controls.Add(Scalle8);
            Controls.Add(Scalle7);
            Controls.Add(Scalle6);
            Controls.Add(Scalle5);
            Controls.Add(Scalle4);
            Controls.Add(Scalle3);
            Controls.Add(Scalle2);
            Controls.Add(Scalle1);
            Controls.Add(ExameExempl20);
            Controls.Add(ExameExempl19);
            Controls.Add(ExameExempl18);
            Controls.Add(ExameExempl17);
            Controls.Add(ExameExempl16);
            Controls.Add(ExameExempl15);
            Controls.Add(ExameExempl14);
            Controls.Add(ExameExempl13);
            Controls.Add(ExameExempl12);
            Controls.Add(ExameExempl11);
            Controls.Add(ExameExempl10);
            Controls.Add(ExameExemplo9);
            Controls.Add(ExameExemplo8);
            Controls.Add(ExameExemplo7);
            Controls.Add(ExameExemplo6);
            Controls.Add(ExameExemplo5);
            Controls.Add(ExameExemplo4);
            Controls.Add(ExameExemplo3);
            Controls.Add(ExameExemplo2);
            Controls.Add(ExameExemplo1);
            Controls.Add(comboBox3);
            Controls.Add(velocidadeScroll);
            Controls.Add(tempoEmTela);
            Controls.Add(fimTela);
            Controls.Add(inicioTela);
            Controls.Add(ptsEmTela);
            Controls.Add(openglControl1);
            Controls.Add(hScrollBar1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Tela_Plotagem";
            Text = "Tela_Plotagem";
            Load += Tela_Plotagem_Load;
            ((System.ComponentModel.ISupportInitialize)openglControl1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.HScrollBar hScrollBar1;
        private SharpGL.OpenGLControl openglControl1;
        private System.Windows.Forms.TextBox ptsEmTela;
        private System.Windows.Forms.TextBox inicioTela;
        private System.Windows.Forms.TextBox fimTela;
        private System.Windows.Forms.ComboBox tempoEmTela;
        private System.Windows.Forms.ComboBox velocidadeScroll;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label ExameExemplo1;
        private System.Windows.Forms.Label ExameExemplo2;
        private System.Windows.Forms.Label ExameExemplo4;
        private System.Windows.Forms.Label ExameExemplo3;
        private System.Windows.Forms.Label ExameExemplo8;
        private System.Windows.Forms.Label ExameExemplo7;
        private System.Windows.Forms.Label ExameExemplo6;
        private System.Windows.Forms.Label ExameExemplo5;
        private System.Windows.Forms.Label ExameExempl16;
        private System.Windows.Forms.Label ExameExempl15;
        private System.Windows.Forms.Label ExameExempl14;
        private System.Windows.Forms.Label ExameExempl13;
        private System.Windows.Forms.Label ExameExempl12;
        private System.Windows.Forms.Label ExameExempl11;
        private System.Windows.Forms.Label ExameExempl10;
        private System.Windows.Forms.Label ExameExemplo9;
        private System.Windows.Forms.Label ExameExempl20;
        private System.Windows.Forms.Label ExameExempl19;
        private System.Windows.Forms.Label ExameExempl18;
        private System.Windows.Forms.Label ExameExempl17;
        private System.Windows.Forms.VScrollBar Scalle1;
        private System.Windows.Forms.VScrollBar Scalle2;
        private System.Windows.Forms.VScrollBar Scalle3;
        private System.Windows.Forms.VScrollBar Scalle4;
        private System.Windows.Forms.VScrollBar Scalle5;
        private System.Windows.Forms.VScrollBar Scalle6;
        private System.Windows.Forms.VScrollBar Scalle7;
        private System.Windows.Forms.VScrollBar Scalle8;
        private System.Windows.Forms.VScrollBar Scalle9;
        private System.Windows.Forms.VScrollBar Scalle10;
        private System.Windows.Forms.VScrollBar Scalle11;
        private System.Windows.Forms.VScrollBar Scalle12;
        private System.Windows.Forms.VScrollBar Scalle13;
        private System.Windows.Forms.VScrollBar Scalle14;
        private System.Windows.Forms.VScrollBar Scalle15;
        private System.Windows.Forms.VScrollBar Scalle16;
        private System.Windows.Forms.VScrollBar Scalle18;
        private System.Windows.Forms.VScrollBar Scalle19;
        private System.Windows.Forms.VScrollBar Scalle20;
        private System.Windows.Forms.VScrollBar Scalle17;
    }
}
