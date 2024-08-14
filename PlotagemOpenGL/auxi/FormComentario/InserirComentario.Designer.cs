namespace PlotagemOpenGL.auxi.FormComentario
{
    partial class InserirComentario
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
            textBox1 = new System.Windows.Forms.TextBox();
            Confirmar = new System.Windows.Forms.Button();
            Cancelar = new System.Windows.Forms.Button();
            ListaComentarios = new System.Windows.Forms.ListBox();
            ListComent = new System.Windows.Forms.Label();
            Comentario = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(12, 173);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(341, 128);
            textBox1.TabIndex = 0;
            // 
            // Confirmar
            // 
            Confirmar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            Confirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Confirmar.Location = new System.Drawing.Point(47, 307);
            Confirmar.Name = "Confirmar";
            Confirmar.Size = new System.Drawing.Size(94, 29);
            Confirmar.TabIndex = 1;
            Confirmar.Text = "Confirmar";
            Confirmar.UseVisualStyleBackColor = true;
            Confirmar.Click += Confirmar_Click;
            // 
            // Cancelar
            // 
            Cancelar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            Cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Cancelar.Location = new System.Drawing.Point(218, 307);
            Cancelar.Name = "Cancelar";
            Cancelar.Size = new System.Drawing.Size(94, 29);
            Cancelar.TabIndex = 2;
            Cancelar.Text = "Cancelar";
            Cancelar.UseVisualStyleBackColor = true;
            Cancelar.Click += Cancelar_Click;
            // 
            // ListaComentarios
            // 
            ListaComentarios.FormattingEnabled = true;
            ListaComentarios.Location = new System.Drawing.Point(12, 37);
            ListaComentarios.Name = "ListaComentarios";
            ListaComentarios.Size = new System.Drawing.Size(341, 104);
            ListaComentarios.TabIndex = 4;
            ListaComentarios.SelectedIndexChanged += ListaComentarios_SelectedIndexChanged;
            // 
            // ListComent
            // 
            ListComent.AutoSize = true;
            ListComent.Location = new System.Drawing.Point(12, 14);
            ListComent.Name = "ListComent";
            ListComent.Size = new System.Drawing.Size(148, 20);
            ListComent.TabIndex = 5;
            ListComent.Text = "Lista de Comentários";
            // 
            // Comentario
            // 
            Comentario.AutoSize = true;
            Comentario.Location = new System.Drawing.Point(12, 150);
            Comentario.Name = "Comentario";
            Comentario.Size = new System.Drawing.Size(87, 20);
            Comentario.TabIndex = 6;
            Comentario.Text = "Comentário";
            // 
            // InserirComentario
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(365, 349);
            Controls.Add(Comentario);
            Controls.Add(ListComent);
            Controls.Add(ListaComentarios);
            Controls.Add(Cancelar);
            Controls.Add(Confirmar);
            Controls.Add(textBox1);
            Name = "InserirComentario";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Comentário";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Confirmar;
        private System.Windows.Forms.Button Cancelar;
        private System.Windows.Forms.ListBox ListaComentarios;
        private System.Windows.Forms.Label ListComent;
        private System.Windows.Forms.Label Comentario;
    }
}