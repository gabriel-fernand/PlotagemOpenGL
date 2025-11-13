namespace PlotagemOpenGL.FormesMenuPanels
{
    partial class SelecionarAquivo
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
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            Abrir = new System.Windows.Forms.Button();
            Renomear = new System.Windows.Forms.Button();
            Copiar = new System.Windows.Forms.Button();
            Excluir = new System.Windows.Forms.Button();
            Fechar = new System.Windows.Forms.Button();
            Exames = new System.Windows.Forms.DataGridView();
            Discos = new System.Windows.Forms.ComboBox();
            Diretorios = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)Exames).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(34, 15);
            label1.TabIndex = 0;
            label1.Text = "Drive";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(213, 21);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(53, 15);
            label2.TabIndex = 1;
            label2.Text = "Diretório";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(10, 266);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(106, 15);
            label4.TabIndex = 3;
            label4.Text = "Relação de Exames";
            // 
            // Abrir
            // 
            Abrir.Font = new System.Drawing.Font("Segoe UI", 7.20000029F);
            Abrir.Location = new System.Drawing.Point(10, 446);
            Abrir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Abrir.Name = "Abrir";
            Abrir.Size = new System.Drawing.Size(69, 26);
            Abrir.TabIndex = 4;
            Abrir.Text = "Abrir";
            Abrir.UseVisualStyleBackColor = true;
            Abrir.Click += Abrir_Click;
            // 
            // Renomear
            // 
            Renomear.Font = new System.Drawing.Font("Segoe UI", 7.20000029F);
            Renomear.Location = new System.Drawing.Point(85, 446);
            Renomear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Renomear.Name = "Renomear";
            Renomear.Size = new System.Drawing.Size(69, 26);
            Renomear.TabIndex = 5;
            Renomear.Text = "Renomear";
            Renomear.UseVisualStyleBackColor = true;
            Renomear.Click += Renomear_Click;
            // 
            // Copiar
            // 
            Copiar.Font = new System.Drawing.Font("Segoe UI", 7.20000029F);
            Copiar.Location = new System.Drawing.Point(159, 446);
            Copiar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Copiar.Name = "Copiar";
            Copiar.Size = new System.Drawing.Size(69, 26);
            Copiar.TabIndex = 6;
            Copiar.Text = "Copiar";
            Copiar.UseVisualStyleBackColor = true;
            Copiar.Click += Copiar_Click;
            // 
            // Excluir
            // 
            Excluir.Font = new System.Drawing.Font("Segoe UI", 7.20000029F);
            Excluir.Location = new System.Drawing.Point(234, 446);
            Excluir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Excluir.Name = "Excluir";
            Excluir.Size = new System.Drawing.Size(69, 26);
            Excluir.TabIndex = 7;
            Excluir.Text = "Excluir";
            Excluir.UseVisualStyleBackColor = true;
            Excluir.Click += Excluir_Click;
            // 
            // Fechar
            // 
            Fechar.Font = new System.Drawing.Font("Segoe UI", 7.20000029F);
            Fechar.Location = new System.Drawing.Point(390, 446);
            Fechar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Fechar.Name = "Fechar";
            Fechar.Size = new System.Drawing.Size(69, 26);
            Fechar.TabIndex = 8;
            Fechar.Text = "Fechar";
            Fechar.UseVisualStyleBackColor = true;
            Fechar.Click += Fechar_Click;
            // 
            // Exames
            // 
            Exames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Exames.Location = new System.Drawing.Point(10, 283);
            Exames.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Exames.Name = "Exames";
            Exames.RowHeadersWidth = 51;
            Exames.Size = new System.Drawing.Size(449, 158);
            Exames.TabIndex = 9;
            Exames.CellDoubleClick += Exames_CellDoubleClick;
            // 
            // Discos
            // 
            Discos.FormattingEnabled = true;
            Discos.Location = new System.Drawing.Point(10, 38);
            Discos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Discos.Name = "Discos";
            Discos.Size = new System.Drawing.Size(144, 23);
            Discos.TabIndex = 11;
            // 
            // Diretorios
            // 
            Diretorios.Location = new System.Drawing.Point(213, 38);
            Diretorios.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Diretorios.Name = "Diretorios";
            Diretorios.Size = new System.Drawing.Size(246, 223);
            Diretorios.TabIndex = 12;
            // 
            // SelecionarAquivo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(467, 481);
            Controls.Add(Diretorios);
            Controls.Add(Discos);
            Controls.Add(Exames);
            Controls.Add(Fechar);
            Controls.Add(Excluir);
            Controls.Add(Copiar);
            Controls.Add(Renomear);
            Controls.Add(Abrir);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "SelecionarAquivo";
            Text = "Selecionar o Exame";
            ((System.ComponentModel.ISupportInitialize)Exames).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Abrir;
        private System.Windows.Forms.Button Renomear;
        private System.Windows.Forms.Button Copiar;
        private System.Windows.Forms.Button Excluir;
        private System.Windows.Forms.Button Fechar;
        private System.Windows.Forms.DataGridView Exames;
        private System.Windows.Forms.ComboBox Discos;
        private System.Windows.Forms.TreeView Diretorios;
    }
}