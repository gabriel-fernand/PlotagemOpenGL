using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows.Media;

namespace PlotagemOpenGL.auxi.FormColorRGB
{
    partial class Colors
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
            lblSelectedColor = new Label();
            colorPanel = new FlowLayoutPanel();
            btnOkay = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblSelectedColor
            // 
            lblSelectedColor.BorderStyle = BorderStyle.FixedSingle;
            lblSelectedColor.Location = new Point(92, 443);
            lblSelectedColor.Name = "lblSelectedColor";
            lblSelectedColor.Size = new Size(75, 28);
            lblSelectedColor.TabIndex = 3;
            lblSelectedColor.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // colorPanel
            // 
            colorPanel.Location = new Point(12, 12);
            colorPanel.Name = "colorPanel";
            colorPanel.Size = new Size(237, 428);
            colorPanel.TabIndex = 0;
            // 
            // btnOkay
            // 
            btnOkay.Location = new Point(11, 443);
            btnOkay.Name = "btnOkay";
            btnOkay.Size = new Size(75, 28);
            btnOkay.TabIndex = 1;
            btnOkay.Text = "OK";
            btnOkay.UseVisualStyleBackColor = true;
            btnOkay.Click += btnOkay_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(173, 443);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 28);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancelar";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // Colors
            // 
            ClientSize = new Size(263, 480);
            Controls.Add(btnCancel);
            Controls.Add(btnOkay);
            Controls.Add(colorPanel);
            //Controls.Add(lblSelectedColor);
            Name = "Colors";
            Text = "Selecione uma Cor";
            ResumeLayout(false);
        }

        private System.Windows.Forms.FlowLayoutPanel colorPanel;
        private System.Windows.Forms.Button btnOkay;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSelectedColor;
        #endregion
    }
}