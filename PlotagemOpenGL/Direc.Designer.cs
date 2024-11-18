using System.Drawing;
using System.Windows.Forms;

namespace PlotagemOpenGL
{
    partial class Direc
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
            teclado = new Label();
            SuspendLayout();
            // 
            // teclado
            // 
            teclado.AutoSize = true;
            teclado.Location = new Point(134, 60);
            teclado.Name = "teclado";
            teclado.Size = new Size(50, 20);
            teclado.TabIndex = 0;
            teclado.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(320, 139);
            Controls.Add(teclado);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }
        private Label teclado;

        #endregion
    }
}