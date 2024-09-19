using System.Windows.Forms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(montagem));
            checkedListBox1 = new CheckedListBox();
            Play = new Button();
            label1 = new Label();
            InconeAzul = new Panel();
            button1 = new Button();
            Profile = new Button();
            button2 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            InconeAzul.SuspendLayout();
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
            // InconeAzul
            // 
            InconeAzul.Controls.Add(button4);
            InconeAzul.Controls.Add(button5);
            InconeAzul.Controls.Add(button6);
            InconeAzul.Controls.Add(button7);
            InconeAzul.Controls.Add(button2);
            InconeAzul.Controls.Add(button1);
            InconeAzul.Controls.Add(Profile);
            InconeAzul.Location = new System.Drawing.Point(219, 26);
            InconeAzul.Name = "InconeAzul";
            InconeAzul.Size = new System.Drawing.Size(313, 45);
            InconeAzul.TabIndex = 60;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.PaleTurquoise;
            button1.BackgroundImage = (System.Drawing.Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button1.Location = new System.Drawing.Point(262, 3);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(43, 39);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = false;
            // 
            // Profile
            // 
            Profile.BackColor = System.Drawing.Color.PaleTurquoise;
            Profile.BackgroundImage = (System.Drawing.Image)resources.GetObject("Profile.BackgroundImage");
            Profile.BackgroundImageLayout = ImageLayout.Stretch;
            Profile.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            Profile.Location = new System.Drawing.Point(3, 3);
            Profile.Name = "Profile";
            Profile.Size = new System.Drawing.Size(43, 39);
            Profile.TabIndex = 0;
            Profile.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.PaleTurquoise;
            button2.BackgroundImage = (System.Drawing.Image)resources.GetObject("button2.BackgroundImage");
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button2.Location = new System.Drawing.Point(46, 3);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(43, 39);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = System.Drawing.Color.PaleTurquoise;
            button4.BackgroundImage = (System.Drawing.Image)resources.GetObject("button4.BackgroundImage");
            button4.BackgroundImageLayout = ImageLayout.Stretch;
            button4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button4.Location = new System.Drawing.Point(213, 3);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(43, 39);
            button4.TabIndex = 62;
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = System.Drawing.Color.PaleTurquoise;
            button5.BackgroundImage = (System.Drawing.Image)resources.GetObject("button5.BackgroundImage");
            button5.BackgroundImageLayout = ImageLayout.Stretch;
            button5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button5.Location = new System.Drawing.Point(171, 3);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(43, 39);
            button5.TabIndex = 63;
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = System.Drawing.Color.PaleTurquoise;
            button6.BackgroundImage = (System.Drawing.Image)resources.GetObject("button6.BackgroundImage");
            button6.BackgroundImageLayout = ImageLayout.Stretch;
            button6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button6.Location = new System.Drawing.Point(129, 3);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(43, 39);
            button6.TabIndex = 64;
            button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = System.Drawing.Color.PaleTurquoise;
            button7.BackgroundImage = (System.Drawing.Image)resources.GetObject("button7.BackgroundImage");
            button7.BackgroundImageLayout = ImageLayout.Stretch;
            button7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            button7.Location = new System.Drawing.Point(87, 3);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(43, 39);
            button7.TabIndex = 65;
            button7.UseVisualStyleBackColor = false;
            // 
            // montagem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(573, 521);
            Controls.Add(InconeAzul);
            Controls.Add(label1);
            Controls.Add(Play);
            Controls.Add(checkedListBox1);
            Name = "montagem";
            Text = "montagem";
            InconeAzul.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public System.Windows.Forms.CheckedListBox checkedListBox1;
        public System.Windows.Forms.Button Play;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel InconeAzul;
        private System.Windows.Forms.Button Profile;
        private System.Windows.Forms.Button button1;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button2;
    }
}