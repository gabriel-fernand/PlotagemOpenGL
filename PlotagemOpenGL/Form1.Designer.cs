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
            painelTelaGl = new System.Windows.Forms.Panel();
            painelComando = new System.Windows.Forms.Panel();
            qtdGraficos = new System.Windows.Forms.TextBox();
            plusCanula = new System.Windows.Forms.Button();
            minusCanula = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            button6 = new System.Windows.Forms.Button();
            button7 = new System.Windows.Forms.Button();
            button8 = new System.Windows.Forms.Button();
            button9 = new System.Windows.Forms.Button();
            button10 = new System.Windows.Forms.Button();
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
            openglControl1.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
            openglControl1.Size = new System.Drawing.Size(816, 599);
            openglControl1.TabIndex = 50;
            openglControl1.MouseMove += openglControl1_MouseMove;
            // 
            // painelExames
            // 
            painelExames.Controls.Add(button9);
            painelExames.Controls.Add(button10);
            painelExames.Controls.Add(button5);
            painelExames.Controls.Add(button6);
            painelExames.Controls.Add(button7);
            painelExames.Controls.Add(button8);
            painelExames.Controls.Add(button4);
            painelExames.Controls.Add(button3);
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
            // plusCanula
            // 
            plusCanula.Location = new System.Drawing.Point(124, 51);
            plusCanula.Name = "plusCanula";
            plusCanula.Size = new System.Drawing.Size(45, 23);
            plusCanula.TabIndex = 14;
            plusCanula.Text = "+";
            plusCanula.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            plusCanula.UseVisualStyleBackColor = true;
            // 
            // minusCanula
            // 
            minusCanula.Location = new System.Drawing.Point(124, 80);
            minusCanula.Name = "minusCanula";
            minusCanula.Size = new System.Drawing.Size(45, 23);
            minusCanula.TabIndex = 15;
            minusCanula.Text = "-";
            minusCanula.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            minusCanula.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(124, 161);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(45, 23);
            button3.TabIndex = 16;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(124, 190);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(45, 23);
            button4.TabIndex = 17;
            button4.Text = "button4";
            button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(124, 418);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(45, 23);
            button5.TabIndex = 21;
            button5.Text = "button5";
            button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(124, 389);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(45, 23);
            button6.TabIndex = 20;
            button6.Text = "button6";
            button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new System.Drawing.Point(126, 321);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(45, 23);
            button7.TabIndex = 19;
            button7.Text = "button7";
            button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            button8.Location = new System.Drawing.Point(126, 292);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(45, 23);
            button8.TabIndex = 18;
            button8.Text = "button8";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new System.Drawing.Point(124, 555);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(45, 23);
            button9.TabIndex = 23;
            button9.Text = "button9";
            button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            button10.Location = new System.Drawing.Point(124, 526);
            button10.Name = "button10";
            button10.Size = new System.Drawing.Size(45, 23);
            button10.TabIndex = 22;
            button10.Text = "button10";
            button10.UseVisualStyleBackColor = true;
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
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button minusCanula;
        private System.Windows.Forms.Button plusCanula;
    }
}
