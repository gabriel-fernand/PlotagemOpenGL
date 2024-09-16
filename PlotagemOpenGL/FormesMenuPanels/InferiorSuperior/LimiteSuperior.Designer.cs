namespace PlotagemOpenGL.FormesMenuPanels.InferiorSuperior
{
    partial class LimiteSuperior
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
            labelLimiteSuperior = new System.Windows.Forms.Label();
            textBoxLimiteSuperior = new System.Windows.Forms.TextBox();
            buttonOK = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // labelLimiteSuperior
            // 
            labelLimiteSuperior.AutoSize = true;
            labelLimiteSuperior.Location = new System.Drawing.Point(13, 15);
            labelLimiteSuperior.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelLimiteSuperior.Name = "labelLimiteSuperior";
            labelLimiteSuperior.Size = new System.Drawing.Size(171, 20);
            labelLimiteSuperior.TabIndex = 0;
            labelLimiteSuperior.Text = "Digite o Limite Superior:";
            // 
            // textBoxLimiteSuperior
            // 
            textBoxLimiteSuperior.Location = new System.Drawing.Point(13, 46);
            textBoxLimiteSuperior.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBoxLimiteSuperior.Name = "textBoxLimiteSuperior";
            textBoxLimiteSuperior.Size = new System.Drawing.Size(345, 27);
            textBoxLimiteSuperior.TabIndex = 1;
            textBoxLimiteSuperior.KeyPress += textBoxLimiteSuperior_KeyPress;
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
            // LimiteSuperior
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(373, 154);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(textBoxLimiteSuperior);
            Controls.Add(labelLimiteSuperior);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LimiteSuperior";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Limite Superior";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public static System.Windows.Forms.Label labelLimiteSuperior;
        public static System.Windows.Forms.TextBox textBoxLimiteSuperior;
        public static System.Windows.Forms.Button buttonOK;
        public static System.Windows.Forms.Button buttonCancel;
    }
}