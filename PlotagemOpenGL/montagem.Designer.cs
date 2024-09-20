using System.Windows.Forms;

namespace PlotagemOpenGL
{
    partial class montagem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(montagem));
            checkedListBox1 = new CheckedListBox();
            Play = new Button();
            label1 = new Label();
            InconeAzul = new Panel();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button2 = new Button();
            button1 = new Button();
            Profile = new Button();
            painelComando = new Panel();
            panel1 = new Panel();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            button13 = new Button();
            button14 = new Button();
            playSelect = new Button();
            minusAll = new Button();
            plusAll = new Button();
            qtdGraficos = new TextBox();
            ptsEmTela = new TextBox();
            inicioTela = new TextBox();
            fimTela = new TextBox();
            button3 = new Button();
            tempoEmTela = new ComboBox();
            MontagemBox = new ComboBox();
            velocidadeScroll = new ComboBox();
            InconeAzul.SuspendLayout();
            painelComando.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new System.Drawing.Point(808, 320);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(193, 466);
            checkedListBox1.TabIndex = 57;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // Play
            // 
            Play.Location = new System.Drawing.Point(808, 785);
            Play.Name = "Play";
            Play.Size = new System.Drawing.Size(94, 29);
            Play.TabIndex = 58;
            Play.Text = "Play";
            Play.UseVisualStyleBackColor = true;
            Play.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            label1.Location = new System.Drawing.Point(951, 789);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 20);
            label1.TabIndex = 59;
            label1.Text = "label1";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // InconeAzul
            // 
            InconeAzul.Controls.Add(button4);
            InconeAzul.Controls.Add(button5);
            InconeAzul.Controls.Add(button6);
            InconeAzul.Controls.Add(button7);
            InconeAzul.Controls.Add(button2);
            InconeAzul.Controls.Add(button1);
            InconeAzul.Controls.Add(Profile);
            InconeAzul.Location = new System.Drawing.Point(201, 5);
            InconeAzul.Name = "InconeAzul";
            InconeAzul.Size = new System.Drawing.Size(311, 47);
            InconeAzul.TabIndex = 60;
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.PaleTurquoise;
            button4.BackgroundImage = (System.Drawing.Image)resources.GetObject("button4.BackgroundImage");
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.FlatStyle = FlatStyle.Flat;
            button4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button4.Location = new System.Drawing.Point(212, 3);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(39, 39);
            button4.TabIndex = 62;
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.Color.PaleTurquoise;
            button5.BackgroundImage = (System.Drawing.Image)resources.GetObject("button5.BackgroundImage");
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.FlatStyle = FlatStyle.Flat;
            button5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button5.Location = new System.Drawing.Point(170, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(39, 39);
            button5.TabIndex = 63;
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = System.Drawing.Color.PaleTurquoise;
            button6.BackgroundImage = (System.Drawing.Image)resources.GetObject("button6.BackgroundImage");
            button6.BackgroundImageLayout = ImageLayout.Stretch;
            button6.FlatStyle = FlatStyle.Flat;
            button6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button6.Location = new System.Drawing.Point(128, 3);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(39, 39);
            button6.TabIndex = 64;
            button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = System.Drawing.Color.PaleTurquoise;
            button7.BackgroundImage = (System.Drawing.Image)resources.GetObject("button7.BackgroundImage");
            button7.BackgroundImageLayout = ImageLayout.Stretch;
            button7.FlatStyle = FlatStyle.Flat;
            button7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button7.Location = new System.Drawing.Point(85, 3);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(39, 39);
            button7.TabIndex = 65;
            button7.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.PaleTurquoise;
            button2.BackgroundImage = (System.Drawing.Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button2.Location = new System.Drawing.Point(44, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(39, 39);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.PaleTurquoise;
            button1.BackgroundImage = (System.Drawing.Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button1.Location = new System.Drawing.Point(262, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(43, 39);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = false;
            // 
            // Profile
            // 
            Profile.BackColor = System.Drawing.Color.PaleTurquoise;
            Profile.BackgroundImage = (System.Drawing.Image)resources.GetObject("Profile.BackgroundImage");
            Profile.BackgroundImageLayout = ImageLayout.Stretch;
            Profile.FlatStyle = FlatStyle.Flat;
            Profile.ForeColor = System.Drawing.Color.Transparent;
            Profile.Location = new System.Drawing.Point(3, 3);
            Profile.Name = "Profile";
            Profile.Size = new System.Drawing.Size(39, 39);
            Profile.TabIndex = 0;
            Profile.UseVisualStyleBackColor = false;
            // 
            // painelComando
            // 
            painelComando.Controls.Add(panel1);
            painelComando.Controls.Add(playSelect);
            painelComando.Controls.Add(InconeAzul);
            painelComando.Controls.Add(minusAll);
            painelComando.Controls.Add(plusAll);
            painelComando.Controls.Add(qtdGraficos);
            painelComando.Controls.Add(ptsEmTela);
            painelComando.Controls.Add(inicioTela);
            painelComando.Controls.Add(fimTela);
            painelComando.Controls.Add(button3);
            painelComando.Controls.Add(tempoEmTela);
            painelComando.Controls.Add(MontagemBox);
            painelComando.Controls.Add(velocidadeScroll);
            painelComando.Location = new System.Drawing.Point(4, 37);
            painelComando.Name = "painelComando";
            painelComando.Size = new System.Drawing.Size(1913, 121);
            painelComando.TabIndex = 61;
            painelComando.Paint += painelComando_Paint;
            // 
            // panel1
            // 
            panel1.Controls.Add(button8);
            panel1.Controls.Add(button9);
            panel1.Controls.Add(button10);
            panel1.Controls.Add(button11);
            panel1.Controls.Add(button12);
            panel1.Controls.Add(button13);
            panel1.Controls.Add(button14);
            panel1.Location = new System.Drawing.Point(518, 5);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(311, 47);
            panel1.TabIndex = 66;
            // 
            // button8
            // 
            button8.BackColor = System.Drawing.Color.Ivory;
            button8.BackgroundImage = (System.Drawing.Image)resources.GetObject("button8.BackgroundImage");
            button8.BackgroundImageLayout = ImageLayout.Stretch;
            button8.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button8.Location = new System.Drawing.Point(213, 3);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(43, 39);
            button8.TabIndex = 62;
            button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            button9.BackColor = System.Drawing.Color.Ivory;
            button9.BackgroundImage = (System.Drawing.Image)resources.GetObject("button9.BackgroundImage");
            button9.BackgroundImageLayout = ImageLayout.Stretch;
            button9.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button9.Location = new System.Drawing.Point(171, 3);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(43, 39);
            button9.TabIndex = 63;
            button9.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            button10.BackColor = System.Drawing.Color.Ivory;
            button10.BackgroundImage = (System.Drawing.Image)resources.GetObject("button10.BackgroundImage");
            button10.BackgroundImageLayout = ImageLayout.Stretch;
            button10.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button10.Location = new System.Drawing.Point(129, 3);
            button10.Name = "button10";
            button10.Size = new System.Drawing.Size(43, 39);
            button10.TabIndex = 64;
            button10.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            button11.BackColor = System.Drawing.Color.Ivory;
            button11.BackgroundImage = (System.Drawing.Image)resources.GetObject("button11.BackgroundImage");
            button11.BackgroundImageLayout = ImageLayout.Stretch;
            button11.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button11.Location = new System.Drawing.Point(87, 3);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(43, 39);
            button11.TabIndex = 65;
            button11.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            button12.BackColor = System.Drawing.Color.Ivory;
            button12.BackgroundImage = (System.Drawing.Image)resources.GetObject("button12.BackgroundImage");
            button12.BackgroundImageLayout = ImageLayout.Stretch;
            button12.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button12.Location = new System.Drawing.Point(46, 3);
            button12.Name = "button12";
            button12.Size = new System.Drawing.Size(43, 39);
            button12.TabIndex = 2;
            button12.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            button13.BackColor = System.Drawing.Color.Ivory;
            button13.BackgroundImage = (System.Drawing.Image)resources.GetObject("button13.BackgroundImage");
            button13.BackgroundImageLayout = ImageLayout.Stretch;
            button13.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button13.Location = new System.Drawing.Point(262, 3);
            button13.Name = "button13";
            button13.Size = new System.Drawing.Size(43, 39);
            button13.TabIndex = 1;
            button13.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            button14.BackColor = System.Drawing.Color.Ivory;
            button14.BackgroundImage = (System.Drawing.Image)resources.GetObject("button14.BackgroundImage");
            button14.BackgroundImageLayout = ImageLayout.Stretch;
            button14.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button14.Location = new System.Drawing.Point(3, 3);
            button14.Name = "button14";
            button14.Size = new System.Drawing.Size(43, 39);
            button14.TabIndex = 0;
            button14.UseVisualStyleBackColor = false;
            // 
            // playSelect
            // 
            playSelect.Location = new System.Drawing.Point(1171, 14);
            playSelect.Name = "playSelect";
            playSelect.Size = new System.Drawing.Size(110, 29);
            playSelect.TabIndex = 57;
            playSelect.Text = "Montagem";
            playSelect.UseVisualStyleBackColor = true;
            // 
            // minusAll
            // 
            minusAll.Location = new System.Drawing.Point(3, 89);
            minusAll.Name = "minusAll";
            minusAll.Size = new System.Drawing.Size(41, 29);
            minusAll.TabIndex = 55;
            minusAll.Text = "-";
            minusAll.UseVisualStyleBackColor = true;
            // 
            // plusAll
            // 
            plusAll.Location = new System.Drawing.Point(50, 89);
            plusAll.Name = "plusAll";
            plusAll.Size = new System.Drawing.Size(41, 29);
            plusAll.TabIndex = 54;
            plusAll.Text = "+";
            plusAll.UseVisualStyleBackColor = true;
            // 
            // qtdGraficos
            // 
            qtdGraficos.Location = new System.Drawing.Point(97, 90);
            qtdGraficos.Name = "qtdGraficos";
            qtdGraficos.Size = new System.Drawing.Size(86, 27);
            qtdGraficos.TabIndex = 50;
            // 
            // ptsEmTela
            // 
            ptsEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            ptsEmTela.Location = new System.Drawing.Point(3, 14);
            ptsEmTela.Name = "ptsEmTela";
            ptsEmTela.ReadOnly = true;
            ptsEmTela.Size = new System.Drawing.Size(63, 25);
            ptsEmTela.TabIndex = 2;
            ptsEmTela.Text = "ptsEmTela";
            ptsEmTela.TextAlign = HorizontalAlignment.Center;
            // 
            // inicioTela
            // 
            inicioTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            inicioTela.Location = new System.Drawing.Point(74, 13);
            inicioTela.Name = "inicioTela";
            inicioTela.ReadOnly = true;
            inicioTela.Size = new System.Drawing.Size(56, 25);
            inicioTela.TabIndex = 3;
            inicioTela.Text = "00-00-00";
            inicioTela.TextAlign = HorizontalAlignment.Center;
            // 
            // fimTela
            // 
            fimTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            fimTela.Location = new System.Drawing.Point(136, 13);
            fimTela.Name = "fimTela";
            fimTela.ReadOnly = true;
            fimTela.Size = new System.Drawing.Size(59, 25);
            fimTela.TabIndex = 4;
            fimTela.Text = "99-99-99";
            fimTela.TextAlign = HorizontalAlignment.Center;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(189, 90);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(41, 29);
            button3.TabIndex = 49;
            button3.Text = ">";
            button3.UseVisualStyleBackColor = true;
            // 
            // tempoEmTela
            // 
            tempoEmTela.DropDownStyle = ComboBoxStyle.DropDownList;
            tempoEmTela.FlatStyle = FlatStyle.System;
            tempoEmTela.Font = new System.Drawing.Font("Arial Narrow", 9F);
            tempoEmTela.FormatString = "N1";
            tempoEmTela.FormattingEnabled = true;
            tempoEmTela.IntegralHeight = false;
            tempoEmTela.Items.AddRange(new object[] { "1 seg", "2 seg", "4 seg", "8 seg", "12 seg", "30 seg", "60 seg", "90 seg", "120 seg", "240 seg" });
            tempoEmTela.Location = new System.Drawing.Point(1034, 14);
            tempoEmTela.Name = "tempoEmTela";
            tempoEmTela.Size = new System.Drawing.Size(65, 28);
            tempoEmTela.TabIndex = 5;
            // 
            // MontagemBox
            // 
            MontagemBox.DropDownStyle = ComboBoxStyle.DropDownList;
            MontagemBox.FlatStyle = FlatStyle.System;
            MontagemBox.FormattingEnabled = true;
            MontagemBox.IntegralHeight = false;
            MontagemBox.Items.AddRange(new object[] { "Series" });
            MontagemBox.Location = new System.Drawing.Point(835, 14);
            MontagemBox.Name = "MontagemBox";
            MontagemBox.Size = new System.Drawing.Size(193, 28);
            MontagemBox.TabIndex = 8;
            // 
            // velocidadeScroll
            // 
            velocidadeScroll.DisplayMember = "1.0x";
            velocidadeScroll.DropDownStyle = ComboBoxStyle.DropDownList;
            velocidadeScroll.FlatStyle = FlatStyle.System;
            velocidadeScroll.FormattingEnabled = true;
            velocidadeScroll.IntegralHeight = false;
            velocidadeScroll.Items.AddRange(new object[] { "1.0x", "1.5x", "2.0x", "2.5x", "5.0x" });
            velocidadeScroll.Location = new System.Drawing.Point(1105, 14);
            velocidadeScroll.Name = "velocidadeScroll";
            velocidadeScroll.Size = new System.Drawing.Size(63, 28);
            velocidadeScroll.TabIndex = 7;
            // 
            // montagem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1920, 991);
            Controls.Add(painelComando);
            Controls.Add(label1);
            Controls.Add(Play);
            Controls.Add(checkedListBox1);
            Name = "montagem";
            Text = "montagem";
            InconeAzul.ResumeLayout(false);
            painelComando.ResumeLayout(false);
            painelComando.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel InconeAzul;
        private System.Windows.Forms.Button Profile;
        private System.Windows.Forms.Button button1;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button2;
        public Panel painelComando;
        public Button playSelect;
        public Button minusAll;
        public Button plusAll;
        public TextBox qtdGraficos;
        public TextBox ptsEmTela;
        public TextBox inicioTela;
        public TextBox fimTela;
        public Button button3;
        public ComboBox tempoEmTela;
        public ComboBox MontagemBox;
        public ComboBox velocidadeScroll;
        private Panel panel1;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
        private Button button14;
    }
}