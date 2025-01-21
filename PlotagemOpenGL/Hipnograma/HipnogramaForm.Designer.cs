namespace PlotagemOpenGL.Hipnograma
{
    partial class HipnogramaForm
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
            openglHipno = new SharpGL.OpenGLControl();
            ((System.ComponentModel.ISupportInitialize)openglHipno).BeginInit();
            SuspendLayout();
            // 
            // openglHipno
            // 
            openglHipno.DrawFPS = false;
            openglHipno.Location = new System.Drawing.Point(0, 0);
            openglHipno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            openglHipno.Name = "openglHipno";
            openglHipno.OpenGLVersion = SharpGL.Version.OpenGLVersion.OpenGL2_1;
            openglHipno.RenderContextType = SharpGL.RenderContextType.DIBSection;
            openglHipno.Size = new System.Drawing.Size(800, 450);
            openglHipno.TabIndex = 0;
            // 
            // HipnogramaForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(openglHipno);
            Name = "HipnogramaForm";
            Text = "HipnogramaForm";
            ((System.ComponentModel.ISupportInitialize)openglHipno).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public static SharpGL.OpenGLControl openglHipno;
    }
}