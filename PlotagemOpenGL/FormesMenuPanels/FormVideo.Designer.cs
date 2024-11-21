namespace PlotagemOpenGL.FormesMenuPanels
{
    partial class FormVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVideo));
            videoPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)videoPlayer).BeginInit();
            SuspendLayout();
            // 
            // videoPlayer
            // 
            videoPlayer.Enabled = true;
            videoPlayer.Location = new System.Drawing.Point(2, 3);
            videoPlayer.Name = "videoPlayer";
            videoPlayer.OcxState = (System.Windows.Forms.AxHost.State)resources.GetObject("videoPlayer.OcxState");
            videoPlayer.Size = new System.Drawing.Size(381, 245);
            videoPlayer.TabIndex = 0;
            // 
            // FormVideo
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(386, 251);
            Controls.Add(videoPlayer);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Name = "FormVideo";
            Text = "Video";
            ((System.ComponentModel.ISupportInitialize)videoPlayer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public AxWMPLib.AxWindowsMediaPlayer videoPlayer;
    }
}