using Accord.Audio.Filters;
using System.ComponentModel;
using System.Windows.Forms;

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
            contextMenuStripHipno = new ContextMenuStrip();
            Imprimir = new ToolStripMenuItem();
            separador = new ToolStripSeparator();
            separador1 = new ToolStripSeparator();
            separador2 = new ToolStripSeparator();
            BruxismoStrip = new ToolStripMenuItem();
            CardioStrip = new ToolStripMenuItem();
            CO2_ExalStrip = new ToolStripMenuItem();
            CPAPStrip = new ToolStripMenuItem();
            cpapVazStrip = new ToolStripMenuItem();
            DespertarStrip = new ToolStripMenuItem();
            estagioStrip = new System.Windows.Forms.ToolStripMenuItem();
            FreqCardStrip = new ToolStripMenuItem();
            horarioStrip = new System.Windows.Forms.ToolStripMenuItem();
            MicrofoneStrip = new ToolStripMenuItem();
            MovimentodePernaStrip = new ToolStripMenuItem();
            posicaoStip = new System.Windows.Forms.ToolStripMenuItem();
            eventosRespStrip = new System.Windows.Forms.ToolStripMenuItem();
            roncoStip = new ToolStripMenuItem();
            SA02Strip = new ToolStripMenuItem();
            LimSup = new ToolStripMenuItem();
            LinhasInternas = new ToolStripMenuItem();
            NaomostrarQuedasZero = new ToolStripMenuItem();
            ConsBnBd = new ToolStripMenuItem();
            LinhasHorarios = new ToolStripMenuItem();
            CorGraf = new ToolStripMenuItem();
            MarcaDAgua = new ToolStripMenuItem();
            LimInf = new ToolStripMenuItem();
            LinInt = new ToolStripMenuItem();
            DivLeg = new ToolStripMenuItem();

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
            openglHipno.ContextMenuStrip = contextMenuStripHipno;
            openglHipno.MouseMove += OpenGLHipno_MouseMove;
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
            //
            // ContextHipno
            //
            contextMenuStripHipno.ImageScalingSize = new System.Drawing.Size(20, 20);
            contextMenuStripHipno.Items.AddRange(new ToolStripItem[] { Imprimir, separador, BruxismoStrip, CardioStrip, CO2_ExalStrip, CPAPStrip, cpapVazStrip, DespertarStrip, estagioStrip, FreqCardStrip, horarioStrip, MicrofoneStrip 
                                                 , MovimentodePernaStrip, posicaoStip, eventosRespStrip, roncoStip, SA02Strip, LinhasInternas, NaomostrarQuedasZero, ConsBnBd, LinhasHorarios, CorGraf, MarcaDAgua});
            contextMenuStripHipno.Name = "contextMenuStripOpenGl";
            contextMenuStripHipno.Size = new System.Drawing.Size(154, 106);
            contextMenuStripHipno.Opening += ContextMenuStripOpenGl_Opening;
            //contextMenuStripOpenGl.Opening += ContextMenuStripOpenGl_Opening;
            // 
            // toolStripSeparator
            // 
            separador.Name = "separador";
            separador.Size = new System.Drawing.Size(150, 6);
            // 
            // toolStripSeparator2
            // 
            separador2.Name = "separador";
            separador2.Size = new System.Drawing.Size(150, 6);
            // 
            // toolStripSeparator1
            // 
            separador1.Name = "separador";
            separador1.Size = new System.Drawing.Size(150, 6);
            // 
            // Imprimir
            // 
            Imprimir.CheckOnClick = true;
            Imprimir.Name = "Imprimir";
            Imprimir.Size = new System.Drawing.Size(148, 26);
            Imprimir.Text = "Imprimir";
            //Imprimir.Click += MenuItem_Click;
            // /
            // BruxismoStrip
            //
            BruxismoStrip.CheckOnClick = true;
            BruxismoStrip.Name = "BruxismoStrip";
            BruxismoStrip.Size = new System.Drawing.Size(148, 26);
            BruxismoStrip.Text = "Bruxismo";
            BruxismoStrip.Tag = 40;
            BruxismoStrip.Click += ClicaMostraGraf;
            // 
            // CardioStrip
            // 
            CardioStrip.CheckOnClick = true;
            CardioStrip.Name = "CardioStrip";
            CardioStrip.Size = new System.Drawing.Size(148, 26);
            CardioStrip.Text = "Cardio";
            CardioStrip.Tag = 4;
            CardioStrip.Click += ClicaMostraGraf;
            // 
            // CO2_ExalStrip
            // 
            CO2_ExalStrip.CheckOnClick = true;
            CO2_ExalStrip.Name = "CO2_ExalStrip";
            CO2_ExalStrip.Size = new System.Drawing.Size(148, 26);
            CO2_ExalStrip.Text = "CO2_Exalado";
            CO2_ExalStrip.Tag = 39;
            CO2_ExalStrip.Click += ClicaMostraGraf;
            // 
            // CPAPStrip
            // 
            CPAPStrip.CheckOnClick = true;
            CPAPStrip.Name = "CPAPStrip";
            CPAPStrip.Size = new System.Drawing.Size(148, 26);
            CPAPStrip.Text = "CPAP";
            CPAPStrip.Tag = 11;
            CPAPStrip.Click += ClicaMostraGraf;
            // 
            // cpapVazStrip
            // 
            cpapVazStrip.CheckOnClick = true;
            cpapVazStrip.Name = "cpapVazStrip";
            cpapVazStrip.Size = new System.Drawing.Size(148, 26);
            cpapVazStrip.Text = "cpap Vazamento";
            cpapVazStrip.Tag = 18;
            cpapVazStrip.Click += ClicaMostraGraf;
            // 
            // DespertarStrip
            // 
            DespertarStrip.CheckOnClick = true;
            DespertarStrip.Name = "DespertarStrip";
            DespertarStrip.Size = new System.Drawing.Size(148, 26);
            DespertarStrip.Text = "Despertar";
            DespertarStrip.Tag = 3;
            DespertarStrip.Click += ClicaMostraGraf;
            // 
            // estagioStrip
            // 
            estagioStrip.CheckOnClick = true;
            estagioStrip.Name = "estagioStrip";
            estagioStrip.Size = new System.Drawing.Size(148, 26);
            estagioStrip.Text = "Estagio";
            estagioStrip.Tag = 9;
            estagioStrip.Click += ClicaMostraGraf;
            // 
            // FreqCardStrip
            // 
            FreqCardStrip.CheckOnClick = true;
            FreqCardStrip.Name = "FreqCardStrip";
            FreqCardStrip.Size = new System.Drawing.Size(148, 26);
            FreqCardStrip.Text = "Freq. Card.";
            FreqCardStrip.Tag = 12;
            FreqCardStrip.Click += ClicaMostraGraf;
            // 
            // horarioStrip
            // 
            horarioStrip.CheckOnClick = true;
            horarioStrip.Name = "horarioStrip";
            horarioStrip.Size = new System.Drawing.Size(148, 26);
            horarioStrip.Text = "Horario";
            horarioStrip.Tag = 21;
            horarioStrip.Click += ClicaMostraGraf;
            // 
            // MicrofoneStrip
            // 
            MicrofoneStrip.CheckOnClick = true;
            MicrofoneStrip.Name = "MicrofoneStrip";
            MicrofoneStrip.Size = new System.Drawing.Size(148, 26);
            MicrofoneStrip.Text = "Microfone";
            MicrofoneStrip.Tag = 19;
            MicrofoneStrip.Click += ClicaMostraGraf;
            // 
            // MovimentodePernaStrip
            // 
            MovimentodePernaStrip.CheckOnClick = true;
            MovimentodePernaStrip.Name = "MovimentodePernaStrip";
            MovimentodePernaStrip.Size = new System.Drawing.Size(148, 26);
            MovimentodePernaStrip.Text = "Movimento de Perna";
            MovimentodePernaStrip.Tag = 5;
            MovimentodePernaStrip.Click += ClicaMostraGraf;
            // 
            // posicaoStip
            // 
            posicaoStip.CheckOnClick = true;
            posicaoStip.Name = "posicaoStip";
            posicaoStip.Size = new System.Drawing.Size(148, 26);
            posicaoStip.Text = "Posicao";
            posicaoStip.Tag = 7;
            posicaoStip.Click += ClicaMostraGraf;
            // 
            // eventosRespStrip
            // 
            eventosRespStrip.CheckOnClick = true;
            eventosRespStrip.Name = "eventosRespStrip";
            eventosRespStrip.Size = new System.Drawing.Size(148, 26);
            eventosRespStrip.Text = "Respiratorios";
            eventosRespStrip.Tag = 1;
            eventosRespStrip.Click += ClicaMostraGraf;
            // 
            // roncoStip
            // 
            roncoStip.CheckOnClick = true;
            roncoStip.Name = "roncoStip";
            roncoStip.Size = new System.Drawing.Size(148, 26);
            roncoStip.Text = "Ronco";
            roncoStip.Tag = 10;
            roncoStip.Click += ClicaMostraGraf;
            // 
            // SA02Strip
            // 
            SA02Strip.CheckOnClick = true;
            SA02Strip.Name = "SA02Strip";
            SA02Strip.Size = new System.Drawing.Size(148, 26);
            SA02Strip.Text = "SaO2";
            SA02Strip.Tag = 6;
            SA02Strip.Click += ClicaMostraGraf;
            // 
            // LinhasInternas
            // 
            LinhasInternas.CheckOnClick = true;
            LinhasInternas.Name = "LinhasInternas";
            LinhasInternas.Size = new System.Drawing.Size(148, 26);
            LinhasInternas.Text = "Linhas Internas";
            // 
            // NaomostrarQuedasZero
            // 
            NaomostrarQuedasZero.CheckOnClick = true;
            NaomostrarQuedasZero.Name = "NaomostrarQuedasZero";
            NaomostrarQuedasZero.Size = new System.Drawing.Size(148, 26);
            NaomostrarQuedasZero.Text = "Nao mostrar Quedas a Zero";
            NaomostrarQuedasZero.Click += linhadehorario_Click;
            // 
            // ConsBnBd
            // 
            ConsBnBd.CheckOnClick = true;
            ConsBnBd.Name = "ConsBnBd";
            ConsBnBd.Size = new System.Drawing.Size(148, 26);
            ConsBnBd.Text = "Considerar Boa Noite e Bom dia";
            // 
            // LinhasHorarios
            // 
            LinhasHorarios.CheckOnClick = true;
            LinhasHorarios.Name = "LinhasHorarios";
            LinhasHorarios.Size = new System.Drawing.Size(148, 26);
            LinhasHorarios.Text = "Linhas de Horarios";
            LinhasHorarios.Click += linhadehorario_Click;
            // 
            // CorGraf
            // 
            CorGraf.CheckOnClick = true;
            CorGraf.Name = "CorGraf";
            CorGraf.Size = new System.Drawing.Size(148, 26);
            CorGraf.Text = "Cor do Grafico";
            CorGraf.CheckOnClick = false;
            CorGraf.Click += CorSinal_Click;
            // 
            // MarcaDAgua
            // 
            MarcaDAgua.CheckOnClick = true;
            MarcaDAgua.Name = "MarcaDAgua";
            MarcaDAgua.Size = new System.Drawing.Size(148, 26);
            MarcaDAgua.Text = "Marca D'Agua";
            MarcaDAgua.Click += linhadehorario_Click;
            // 
            // LimSup
            // 
            LimSup.CheckOnClick = true;
            LimSup.Name = "LimSup";
            LimSup.Size = new System.Drawing.Size(148, 26);
            LimSup.Text = "Limite Superior";
            LimSup.Click += lmSup_Click;
            LimSup.CheckOnClick = false;
            // 
            // LimInf
            // 
            LimInf.CheckOnClick = true;
            LimInf.Name = "LimInf";
            LimInf.Size = new System.Drawing.Size(148, 26);
            LimInf.Text = "Limite Inferior";
            LimInf.Click += lmInf_Click;
            LimInf.CheckOnClick = false;
            // 
            // LinInt
            // 
            LinInt.CheckOnClick = true;
            LinInt.Name = "LinInt";
            LinInt.Size = new System.Drawing.Size(148, 26);
            LinInt.Text = "Linhas internas";
            LinInt.Click += LinhasInternas_Click;
            LinInt.CheckOnClick = false;
            // 
            // DivLeg
            // 
            DivLeg.CheckOnClick = true;
            DivLeg.Name = "DivLeg";
            DivLeg.Size = new System.Drawing.Size(148, 26);
            DivLeg.Text = "Divisoes das legendas";
            DivLeg.Click += DivLegenda_Click;
            DivLeg.CheckOnClick = false;

        }

        #endregion

        public static SharpGL.OpenGLControl openglHipno;
        public static ContextMenuStrip contextMenuStripHipno;
        public static ToolStripMenuItem Imprimir;
        public ToolStripSeparator separador;
        public ToolStripSeparator separador1;
        public ToolStripSeparator separador2;

        public static ToolStripMenuItem BruxismoStrip;
        public static ToolStripMenuItem CardioStrip;
        public static ToolStripMenuItem CO2_ExalStrip;
        public static ToolStripMenuItem CPAPStrip;
        public static ToolStripMenuItem cpapVazStrip;
        public static ToolStripMenuItem DespertarStrip;
        public static ToolStripMenuItem estagioStrip;
        public static ToolStripMenuItem FreqCardStrip;
        public static ToolStripMenuItem horarioStrip;
        public static ToolStripMenuItem MicrofoneStrip;
        public static ToolStripMenuItem MovimentodePernaStrip;
        public static ToolStripMenuItem posicaoStip;
        public static ToolStripMenuItem eventosRespStrip;
        public static ToolStripMenuItem roncoStip;
        public static ToolStripMenuItem SA02Strip;
        public static ToolStripMenuItem LinhasInternas;
        public static ToolStripMenuItem NaomostrarQuedasZero;
        public static ToolStripMenuItem ConsBnBd;
        public static ToolStripMenuItem LinhasHorarios;
        public static ToolStripMenuItem CorGraf;
        public static ToolStripMenuItem MarcaDAgua;
        public static ToolStripMenuItem LimSup;
        public static ToolStripMenuItem LimInf;
        public static ToolStripMenuItem LinInt;
        public static ToolStripMenuItem DivLeg;
    }
}
