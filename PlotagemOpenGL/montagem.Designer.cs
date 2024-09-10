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
            checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            Play = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new System.Drawing.Point(12, 12);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new System.Drawing.Size(193, 466);
            checkedListBox1.TabIndex = 57;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // Play
            // 
            Play.Location = new System.Drawing.Point(12, 480);
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
            label1.Location = new System.Drawing.Point(155, 484);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 20);
            label1.TabIndex = 59;
            label1.Text = "label1";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // montagem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(224, 521);
            Controls.Add(label1);
            Controls.Add(Play);
            Controls.Add(checkedListBox1);
            Name = "montagem";
            Text = "montagem";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label label1;
    }
}