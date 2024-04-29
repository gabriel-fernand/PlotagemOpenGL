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
            // Canula
            // 
            Canula.AutoSize = true;
            Canula.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Canula.Location = new System.Drawing.Point(5, 60);
            Canula.Name = "Canula";
            Canula.Size = new System.Drawing.Size(70, 27);
            Canula.TabIndex = 9;
            Canula.Text = "Canula";
            // 
            // Fluxo
            // 
            Fluxo.AutoSize = true;
            Fluxo.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Fluxo.Location = new System.Drawing.Point(5, 173);
            Fluxo.Name = "Fluxo";
            Fluxo.Size = new System.Drawing.Size(57, 27);
            Fluxo.TabIndex = 10;
            Fluxo.Text = "Fluxo";
            // 
            // Ronco
            // 
            Ronco.AutoSize = true;
            Ronco.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Ronco.Location = new System.Drawing.Point(5, 398);
            Ronco.Name = "Ronco";
            Ronco.Size = new System.Drawing.Size(65, 27);
            Ronco.TabIndex = 12;
            Ronco.Text = "Ronco";
            // 
            // Abdomen
            // 
            Abdomen.AutoSize = true;
            Abdomen.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Abdomen.Location = new System.Drawing.Point(5, 291);
            Abdomen.Name = "Abdomen";
            Abdomen.Size = new System.Drawing.Size(91, 27);
            Abdomen.TabIndex = 11;
            Abdomen.Text = "Abdomen";
            // 
            // SaturacaoO2
            // 
            SaturacaoO2.AutoSize = true;
            SaturacaoO2.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            SaturacaoO2.Location = new System.Drawing.Point(5, 538);
            SaturacaoO2.Name = "SaturacaoO2";
            SaturacaoO2.Size = new System.Drawing.Size(125, 27);
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
            openglControl1.MouseMove += openglControl1_MouseMove;
            openglControl1.KeyDown += TelaPlotagem_KeyDown;
            // 
            // painelExames
            // 
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
            // minusSatu
            // 
            minusSatu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusSatu.Location = new System.Drawing.Point(124, 555);
            minusSatu.Name = "minusSatu";
            minusSatu.Size = new System.Drawing.Size(45, 23);
            minusSatu.TabIndex = 23;
            minusSatu.Text = "-";
            minusSatu.UseVisualStyleBackColor = true;
            minusSatu.Click += minusSatu_Click;
            // 
            // plusSatu
            // 
            plusSatu.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusSatu.Location = new System.Drawing.Point(124, 526);
            plusSatu.Name = "plusSatu";
            plusSatu.Size = new System.Drawing.Size(45, 23);
            plusSatu.TabIndex = 22;
            plusSatu.Text = "+";
            plusSatu.UseVisualStyleBackColor = true;
            plusSatu.Click += plusSatu_Click;
            // 
            // minusRonco
            // 
            minusRonco.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusRonco.Location = new System.Drawing.Point(124, 418);
            minusRonco.Name = "minusRonco";
            minusRonco.Size = new System.Drawing.Size(45, 23);
            minusRonco.TabIndex = 21;
            minusRonco.Text = "-";
            minusRonco.UseVisualStyleBackColor = true;
            minusRonco.Click += minusRonco_Click;
            // 
            // plusRonco
            // 
            plusRonco.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusRonco.Location = new System.Drawing.Point(124, 389);
            plusRonco.Name = "plusRonco";
            plusRonco.Size = new System.Drawing.Size(45, 23);
            plusRonco.TabIndex = 20;
            plusRonco.Text = "+";
            plusRonco.UseVisualStyleBackColor = true;
            plusRonco.Click += plusRonco_Click;
            // 
            // minusAbdomen
            // 
            minusAbdomen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusAbdomen.Location = new System.Drawing.Point(126, 321);
            minusAbdomen.Name = "minusAbdomen";
            minusAbdomen.Size = new System.Drawing.Size(45, 23);
            minusAbdomen.TabIndex = 19;
            minusAbdomen.Text = "-";
            minusAbdomen.UseVisualStyleBackColor = true;
            minusAbdomen.Click += minusAbdomen_Click;
            // 
            // plusAbdomen
            // 
            plusAbdomen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusAbdomen.Location = new System.Drawing.Point(126, 292);
            plusAbdomen.Name = "plusAbdomen";
            plusAbdomen.Size = new System.Drawing.Size(45, 23);
            plusAbdomen.TabIndex = 18;
            plusAbdomen.Text = "+";
            plusAbdomen.UseVisualStyleBackColor = true;
            plusAbdomen.Click += plusAbdomen_Click;
            // 
            // minusFluxo
            // 
            minusFluxo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusFluxo.Location = new System.Drawing.Point(124, 190);
            minusFluxo.Name = "minusFluxo";
            minusFluxo.Size = new System.Drawing.Size(45, 23);
            minusFluxo.TabIndex = 17;
            minusFluxo.Text = "-";
            minusFluxo.UseVisualStyleBackColor = true;
            minusFluxo.Click += minusFluxo_Click;
            // 
            // plusFluxo
            // 
            plusFluxo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusFluxo.Location = new System.Drawing.Point(124, 161);
            plusFluxo.Name = "plusFluxo";
            plusFluxo.Size = new System.Drawing.Size(45, 23);
            plusFluxo.TabIndex = 16;
            plusFluxo.Text = "+";
            plusFluxo.UseVisualStyleBackColor = true;
            plusFluxo.Click += plusFluxo_Click;
            // 
            // minusCanula
            // 
            minusCanula.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            minusCanula.Location = new System.Drawing.Point(124, 80);
            minusCanula.Name = "minusCanula";
            minusCanula.Size = new System.Drawing.Size(45, 23);
            minusCanula.TabIndex = 15;
            minusCanula.Text = "-";
            minusCanula.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusCanula.UseVisualStyleBackColor = true;
            minusCanula.Click += minusCanula_Click;
            // 
            // plusCanula
            // 
            plusCanula.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            plusCanula.Location = new System.Drawing.Point(124, 51);
            plusCanula.Name = "plusCanula";
            plusCanula.Size = new System.Drawing.Size(45, 23);
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
    }
}
