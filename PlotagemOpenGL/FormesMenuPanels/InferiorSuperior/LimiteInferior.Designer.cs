namespace PlotagemOpenGL.FormesMenuPanels.InferiorSuperior
{
    partial class LimiteInferior
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            labelLimiteInferior = new System.Windows.Forms.Label();
            textBoxLimiteInferior = new System.Windows.Forms.TextBox();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // labelLimiteInferior
            // 
            labelLimiteInferior.AutoSize = true;
            labelLimiteInferior.Location = new System.Drawing.Point(13, 15);
            labelLimiteInferior.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelLimiteInferior.Name = "labelLimiteInferior";
            labelLimiteInferior.Size = new System.Drawing.Size(163, 20);
            labelLimiteInferior.TabIndex = 0;
            labelLimiteInferior.Text = "Digite o Limite Inferior:";
            // 
            // textBoxLimiteInferior
            // 
            textBoxLimiteInferior.Location = new System.Drawing.Point(13, 46);
            textBoxLimiteInferior.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBoxLimiteInferior.Name = "textBoxLimiteInferior";
            textBoxLimiteInferior.Size = new System.Drawing.Size(345, 27);
            textBoxLimiteInferior.TabIndex = 1;
            textBoxLimiteInferior.KeyPress += textBoxLimiteInferior_KeyPress;
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
            // LimiteInferior
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(373, 154);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxLimiteInferior);
            Controls.Add(labelLimiteInferior);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LimiteInferior";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Limite Inferior";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public static System.Windows.Forms.Label labelLimiteInferior;
        public static System.Windows.Forms.TextBox textBoxLimiteInferior;
        public static System.Windows.Forms.Button buttonOK;
        public static System.Windows.Forms.Button buttonCancel;
    }
}