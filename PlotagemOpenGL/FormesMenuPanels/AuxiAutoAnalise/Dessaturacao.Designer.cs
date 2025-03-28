namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    partial class Dessaturacao
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
        public void InitializeComponent()
        {
            aplicar = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            quedaSup = new System.Windows.Forms.TextBox();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            limRec = new System.Windows.Forms.TextBox();
            label12 = new System.Windows.Forms.Label();
            Desprezar = new System.Windows.Forms.TextBox();
            SuspendLayout();
            // 
            // aplicar
            // 
            aplicar.Location = new System.Drawing.Point(115, 216);
            aplicar.Name = "aplicar";
            aplicar.Size = new System.Drawing.Size(114, 40);
            aplicar.TabIndex = 0;
            aplicar.Text = "Aplicar";
            aplicar.UseVisualStyleBackColor = true;
            aplicar.Click += aplicar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = System.Drawing.Color.Transparent;
            label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            label1.Location = new System.Drawing.Point(103, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(140, 24);
            label1.TabIndex = 1;
            label1.Text = "Dessaturação";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.Color.Transparent;
            label2.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label2.Location = new System.Drawing.Point(6, 55);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(218, 38);
            label2.TabIndex = 2;
            label2.Text = "1 - Considerar dessaturação\n uma queda superio a:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.Color.Transparent;
            label3.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label3.Location = new System.Drawing.Point(6, 105);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(232, 19);
            label3.TabIndex = 3;
            label3.Text = "2 - Tempo limite para recálculo";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.Color.Transparent;
            label4.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label4.Location = new System.Drawing.Point(6, 147);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(259, 19);
            label4.TabIndex = 4;
            label4.Text = "3 - Desprezar valores inferiores a:";
            // 
            // quedaSup
            // 
            quedaSup.Location = new System.Drawing.Point(274, 51);
            quedaSup.Name = "quedaSup";
            quedaSup.Size = new System.Drawing.Size(37, 27);
            quedaSup.TabIndex = 10;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.Color.Transparent;
            label10.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label10.Location = new System.Drawing.Point(313, 55);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(24, 19);
            label10.TabIndex = 11;
            label10.Text = "%";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = System.Drawing.Color.Transparent;
            label11.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label11.Location = new System.Drawing.Point(313, 147);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(24, 19);
            label11.TabIndex = 13;
            label11.Text = "%";
            // 
            // limRec
            // 
            limRec.Location = new System.Drawing.Point(274, 101);
            limRec.Name = "limRec";
            limRec.Size = new System.Drawing.Size(37, 27);
            limRec.TabIndex = 12;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.Color.Transparent;
            label12.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label12.Location = new System.Drawing.Point(313, 108);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(34, 16);
            label12.TabIndex = 15;
            label12.Text = "Seg.";
            // 
            // Desprezar
            // 
            Desprezar.Location = new System.Drawing.Point(274, 143);
            Desprezar.Name = "Desprezar";
            Desprezar.Size = new System.Drawing.Size(37, 27);
            Desprezar.TabIndex = 14;
            // 
            // Dessaturacao
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Fundo_Recovered___Copia;
            BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            ClientSize = new System.Drawing.Size(347, 285);
            Controls.Add(label12);
            Controls.Add(Desprezar);
            Controls.Add(label11);
            Controls.Add(limRec);
            Controls.Add(label10);
            Controls.Add(quedaSup);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(aplicar);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "Dessaturacao";
            Text = "Apneia";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public System.Windows.Forms.Button aplicar;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox quedaSup;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox limRec;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox Desprezar;

    }
}