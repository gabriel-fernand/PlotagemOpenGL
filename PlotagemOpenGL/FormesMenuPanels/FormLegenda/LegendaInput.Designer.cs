namespace PlotagemOpenGL.auxi.FormLegenda
{
    partial class LegendaInput
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar recursos utilizados.
        /// </summary>
        /// <param name="disposing">Se for necessário liberar recursos gerenciados.</param>
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
        /// Método necessário para suporte ao Designer - Não modifique
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            labelLegenda = new System.Windows.Forms.Label();
            textBoxLegenda = new System.Windows.Forms.TextBox();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // labelLegenda
            // 
            labelLegenda.AutoSize = true;
            labelLegenda.Location = new System.Drawing.Point(13, 15);
            labelLegenda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelLegenda.Name = "labelLegenda";
            labelLegenda.Size = new System.Drawing.Size(123, 20);
            labelLegenda.TabIndex = 0;
            labelLegenda.Text = "Digite a legenda:";
            // 
            // textBoxLegenda
            // 
            textBoxLegenda.Location = new System.Drawing.Point(13, 46);
            textBoxLegenda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBoxLegenda.Name = "textBoxLegenda";
            textBoxLegenda.Size = new System.Drawing.Size(345, 27);
            textBoxLegenda.TabIndex = 1;
            // 
            // buttonOK
            // 
            buttonOK.Location = new System.Drawing.Point(107, 92);
            buttonOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(100, 35);
            buttonOK.TabIndex = 2;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new System.Drawing.Point(213, 92);
            buttonCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(100, 35);
            buttonCancel.TabIndex = 3;
            buttonCancel.Text = "Cancelar";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // LegendaInput
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(373, 154);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxLegenda);
            Controls.Add(labelLegenda);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LegendaInput";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Inserir Legenda";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.Label labelLegenda;
        public static System.Windows.Forms.TextBox textBoxLegenda;
        public System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.Button buttonCancel;
    }
}
