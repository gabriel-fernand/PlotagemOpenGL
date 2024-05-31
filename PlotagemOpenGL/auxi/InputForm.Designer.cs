namespace PlotagemOpenGL.auxi
{
    partial class InputForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnOk;

        /// <summary>
        /// Limpar todos os recursos em uso.
        /// </summary>
        /// <param name="disposing">true se os recursos gerenciados devem ser descartados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary>
        /// Método necessário para o suporte ao Designer - não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            txtInput = new System.Windows.Forms.TextBox();
            btnOk = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.Location = new System.Drawing.Point(12, 15);
            txtInput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            txtInput.Name = "txtInput";
            txtInput.Size = new System.Drawing.Size(260, 27);
            txtInput.TabIndex = 0;
            // 
            // btnOk
            // 
            btnOk.Location = new System.Drawing.Point(173, 50);
            btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(75, 29);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // InputForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(284, 94);
            Controls.Add(btnOk);
            Controls.Add(txtInput);
            Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            Name = "InputForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}