namespace PlotagemOpenGL.FormesMenuPanels
{
    partial class MontagemForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MontagemForm));
            Montagens = new System.Windows.Forms.GroupBox();
            Canais = new System.Windows.Forms.GroupBox();
            panelImagem = new System.Windows.Forms.Panel();
            SuspendLayout();
            // 
            // Montagens
            // 
            Montagens.Location = new System.Drawing.Point(12, 12);
            Montagens.Name = "Montagens";
            Montagens.Size = new System.Drawing.Size(195, 499);
            Montagens.TabIndex = 0;
            Montagens.TabStop = false;
            Montagens.Text = "Montagens";
            // 
            // Canais
            // 
            Canais.Location = new System.Drawing.Point(834, 12);
            Canais.Name = "Canais";
            Canais.Size = new System.Drawing.Size(426, 163);
            Canais.TabIndex = 1;
            Canais.TabStop = false;
            Canais.Text = "Canais";
            // 
            // panelImagem
            // 
            panelImagem.BackColor = System.Drawing.Color.Transparent;
            panelImagem.BackgroundImage = (System.Drawing.Image)resources.GetObject("panelImagem.BackgroundImage");
            panelImagem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            panelImagem.Location = new System.Drawing.Point(214, 12);
            panelImagem.Name = "panelImagem";
            panelImagem.Size = new System.Drawing.Size(614, 496);
            panelImagem.TabIndex = 2;
            // 
            // MontagemForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1272, 643);
            Controls.Add(panelImagem);
            Controls.Add(Canais);
            Controls.Add(Montagens);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MontagemForm";
            Text = "Montagem";
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox Montagens;
        private System.Windows.Forms.GroupBox Canais;
        private System.Windows.Forms.Panel panelImagem;
    }
}