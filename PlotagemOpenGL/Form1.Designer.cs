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
            ExameExemplo1 = new System.Windows.Forms.Label();
            ExameExemplo2 = new System.Windows.Forms.Label();
            ExameExemplo4 = new System.Windows.Forms.Label();
            ExameExemplo3 = new System.Windows.Forms.Label();
            ExameExemplo5 = new System.Windows.Forms.Label();
            ExameExempl20 = new System.Windows.Forms.Label();
            ExameExempl19 = new System.Windows.Forms.Label();
            ExameExempl18 = new System.Windows.Forms.Label();
            ExameExempl17 = new System.Windows.Forms.Label();
            Scalle1 = new System.Windows.Forms.VScrollBar();
            Scalle2 = new System.Windows.Forms.VScrollBar();
            Scalle3 = new System.Windows.Forms.VScrollBar();
            Scalle4 = new System.Windows.Forms.VScrollBar();
            Scalle5 = new System.Windows.Forms.VScrollBar();
            Scalle15 = new System.Windows.Forms.VScrollBar();
            Scalle16 = new System.Windows.Forms.VScrollBar();
            Scalle18 = new System.Windows.Forms.VScrollBar();
            Scalle19 = new System.Windows.Forms.VScrollBar();
            Scalle20 = new System.Windows.Forms.VScrollBar();
            Scalle17 = new System.Windows.Forms.VScrollBar();
            Play = new System.Windows.Forms.Button();
            openglControl1 = new SharpGL.OpenGLControl();
            painelExames = new System.Windows.Forms.Panel();
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
            tempoEmTela.Location = new System.Drawing.Point(361, 3);
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
            velocidadeScroll.Location = new System.Drawing.Point(453, 3);
            velocidadeScroll.Name = "velocidadeScroll";
            velocidadeScroll.Size = new System.Drawing.Size(86, 28);
            velocidadeScroll.TabIndex = 7;
            velocidadeScroll.Text = "1.0x";
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
            // ExameExemplo1
            // 
            ExameExemplo1.AutoSize = true;
            ExameExemplo1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo1.Location = new System.Drawing.Point(5, 60);
            ExameExemplo1.Name = "ExameExemplo1";
            ExameExemplo1.Size = new System.Drawing.Size(135, 24);
            ExameExemplo1.TabIndex = 9;
            ExameExemplo1.Text = "ExameExemplo1";
            // 
            // ExameExemplo2
            // 
            ExameExemplo2.AutoSize = true;
            ExameExemplo2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo2.Location = new System.Drawing.Point(5, 173);
            ExameExemplo2.Name = "ExameExemplo2";
            ExameExemplo2.Size = new System.Drawing.Size(135, 24);
            ExameExemplo2.TabIndex = 10;
            ExameExemplo2.Text = "ExameExemplo2";
            // 
            // ExameExemplo4
            // 
            ExameExemplo4.AutoSize = true;
            ExameExemplo4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo4.Location = new System.Drawing.Point(5, 398);
            ExameExemplo4.Name = "ExameExemplo4";
            ExameExemplo4.Size = new System.Drawing.Size(135, 24);
            ExameExemplo4.TabIndex = 12;
            ExameExemplo4.Text = "ExameExemplo4";
            // 
            // ExameExemplo3
            // 
            ExameExemplo3.AutoSize = true;
            ExameExemplo3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo3.Location = new System.Drawing.Point(5, 291);
            ExameExemplo3.Name = "ExameExemplo3";
            ExameExemplo3.Size = new System.Drawing.Size(135, 24);
            ExameExemplo3.TabIndex = 11;
            ExameExemplo3.Text = "ExameExemplo3";
            // 
            // ExameExemplo5
            // 
            ExameExemplo5.AutoSize = true;
            ExameExemplo5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExemplo5.Location = new System.Drawing.Point(5, 538);
            ExameExemplo5.Name = "ExameExemplo5";
            ExameExemplo5.Size = new System.Drawing.Size(135, 24);
            ExameExemplo5.TabIndex = 13;
            ExameExemplo5.Text = "ExameExemplo5";
            // 
            // ExameExempl20
            // 
            ExameExempl20.AutoSize = true;
            ExameExempl20.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl20.Location = new System.Drawing.Point(5, 824);
            ExameExempl20.Name = "ExameExempl20";
            ExameExempl20.Size = new System.Drawing.Size(200, 35);
            ExameExempl20.TabIndex = 28;
            ExameExempl20.Text = "ExameExempl20";
            // 
            // ExameExempl19
            // 
            ExameExempl19.AutoSize = true;
            ExameExempl19.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl19.Location = new System.Drawing.Point(5, 781);
            ExameExempl19.Name = "ExameExempl19";
            ExameExempl19.Size = new System.Drawing.Size(200, 35);
            ExameExempl19.TabIndex = 27;
            ExameExempl19.Text = "ExameExempl19";
            // 
            // ExameExempl18
            // 
            ExameExempl18.AutoSize = true;
            ExameExempl18.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl18.Location = new System.Drawing.Point(5, 737);
            ExameExempl18.Name = "ExameExempl18";
            ExameExempl18.Size = new System.Drawing.Size(200, 35);
            ExameExempl18.TabIndex = 26;
            ExameExempl18.Text = "ExameExempl18";
            // 
            // ExameExempl17
            // 
            ExameExempl17.AutoSize = true;
            ExameExempl17.Font = new System.Drawing.Font("Arial Narrow", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            ExameExempl17.Location = new System.Drawing.Point(5, 693);
            ExameExempl17.Name = "ExameExempl17";
            ExameExempl17.Size = new System.Drawing.Size(200, 35);
            ExameExempl17.TabIndex = 25;
            ExameExempl17.Text = "ExameExempl17";
            // 
            // Scalle1
            // 
            Scalle1.Location = new System.Drawing.Point(143, 54);
            Scalle1.Name = "Scalle1";
            Scalle1.Size = new System.Drawing.Size(19, 30);
            Scalle1.TabIndex = 29;
            // 
            // Scalle2
            // 
            Scalle2.Location = new System.Drawing.Point(143, 167);
            Scalle2.Name = "Scalle2";
            Scalle2.Size = new System.Drawing.Size(19, 30);
            Scalle2.TabIndex = 30;
            // 
            // Scalle3
            // 
            Scalle3.Location = new System.Drawing.Point(143, 291);
            Scalle3.Name = "Scalle3";
            Scalle3.Size = new System.Drawing.Size(19, 30);
            Scalle3.TabIndex = 31;
            // 
            // Scalle4
            // 
            Scalle4.Location = new System.Drawing.Point(143, 392);
            Scalle4.Name = "Scalle4";
            Scalle4.Size = new System.Drawing.Size(19, 30);
            Scalle4.TabIndex = 32;
            // 
            // Scalle5
            // 
            Scalle5.Location = new System.Drawing.Point(143, 532);
            Scalle5.Name = "Scalle5";
            Scalle5.Size = new System.Drawing.Size(19, 30);
            Scalle5.TabIndex = 33;
            // 
            // Scalle15
            // 
            Scalle15.Location = new System.Drawing.Point(208, 604);
            Scalle15.Name = "Scalle15";
            Scalle15.Size = new System.Drawing.Size(19, 30);
            Scalle15.TabIndex = 43;
            // 
            // Scalle16
            // 
            Scalle16.Location = new System.Drawing.Point(208, 649);
            Scalle16.Name = "Scalle16";
            Scalle16.Size = new System.Drawing.Size(19, 30);
            Scalle16.TabIndex = 44;
            // 
            // Scalle18
            // 
            Scalle18.Location = new System.Drawing.Point(208, 737);
            Scalle18.Name = "Scalle18";
            Scalle18.Size = new System.Drawing.Size(19, 30);
            Scalle18.TabIndex = 45;
            // 
            // Scalle19
            // 
            Scalle19.Location = new System.Drawing.Point(208, 781);
            Scalle19.Name = "Scalle19";
            Scalle19.Size = new System.Drawing.Size(19, 30);
            Scalle19.TabIndex = 46;
            // 
            // Scalle20
            // 
            Scalle20.Location = new System.Drawing.Point(208, 824);
            Scalle20.Name = "Scalle20";
            Scalle20.Size = new System.Drawing.Size(19, 30);
            Scalle20.TabIndex = 47;
            // 
            // Scalle17
            // 
            Scalle17.Location = new System.Drawing.Point(208, 693);
            Scalle17.Name = "Scalle17";
            Scalle17.Size = new System.Drawing.Size(19, 30);
            Scalle17.TabIndex = 48;
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
            openglControl1.MouseMove += openglControl1_MouseMove;
            // 
            // painelExames
            // 
            painelExames.Controls.Add(ExameExemplo1);
            painelExames.Controls.Add(Scalle1);
            painelExames.Controls.Add(Scalle17);
            painelExames.Controls.Add(ExameExemplo2);
            painelExames.Controls.Add(Scalle20);
            painelExames.Controls.Add(ExameExemplo3);
            painelExames.Controls.Add(Scalle19);
            painelExames.Controls.Add(ExameExemplo4);
            painelExames.Controls.Add(Scalle18);
            painelExames.Controls.Add(ExameExemplo5);
            painelExames.Controls.Add(Scalle16);
            painelExames.Controls.Add(Scalle15);
            painelExames.Controls.Add(ExameExempl17);
            painelExames.Controls.Add(Scalle5);
            painelExames.Controls.Add(ExameExempl18);
            painelExames.Controls.Add(Scalle4);
            painelExames.Controls.Add(ExameExempl19);
            painelExames.Controls.Add(Scalle3);
            painelExames.Controls.Add(ExameExempl20);
            painelExames.Controls.Add(Scalle2);
            painelExames.Location = new System.Drawing.Point(2, 79);
            painelExames.Name = "painelExames";
            painelExames.Size = new System.Drawing.Size(172, 599);
            painelExames.TabIndex = 51;
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
        private System.Windows.Forms.Label ExameExemplo1;
        private System.Windows.Forms.Label ExameExemplo2;
        private System.Windows.Forms.Label ExameExemplo4;
        private System.Windows.Forms.Label ExameExemplo3;
        private System.Windows.Forms.Label ExameExemplo5;
        private System.Windows.Forms.Label ExameExempl20;
        private System.Windows.Forms.Label ExameExempl19;
        private System.Windows.Forms.Label ExameExempl18;
        private System.Windows.Forms.Label ExameExempl17;
        private System.Windows.Forms.VScrollBar Scalle1;
        private System.Windows.Forms.VScrollBar Scalle2;
        private System.Windows.Forms.VScrollBar Scalle3;
        private System.Windows.Forms.VScrollBar Scalle4;
        private System.Windows.Forms.VScrollBar Scalle5;
        private System.Windows.Forms.VScrollBar Scalle15;
        private System.Windows.Forms.VScrollBar Scalle16;
        private System.Windows.Forms.VScrollBar Scalle18;
        private System.Windows.Forms.VScrollBar Scalle19;
        private System.Windows.Forms.VScrollBar Scalle20;
        private System.Windows.Forms.VScrollBar Scalle17;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Panel painelExames;
        private System.Windows.Forms.Panel painelTelaGl;
        private System.Windows.Forms.Panel painelComando;
        private System.Windows.Forms.TextBox qtdGraficos;
        public SharpGL.OpenGLControl openglControl1;
    }
}
