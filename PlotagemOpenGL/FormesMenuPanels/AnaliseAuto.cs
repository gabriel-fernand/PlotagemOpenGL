using PlotagemOpenGL.auxi;
using PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class AnaliseAuto : Form
    {
        AnaliseAutomaticaApneia anApneiHipo;
        AnaliseAutomaticaDessaturacao anDessatu;
        AnaliseAutomaticaRonco anRonco;
        AutoAnalisePLM anPLM;
        RegraPLM regraPLM;
        Apneia ap;
        Dessaturacao desa;
        Ronco ronc;
        PLM plm;
        int codCanal;
        public bool cancelar = false;
        public CancellationTokenSource cancellationTokenSource;
        public CancellationToken cancellationToken;

        public AnaliseAuto()
        {
            InitializeComponent();
            progresso.Hide();
            Analisando.Hide();
            Cancelar.Tag = 0;
            ap = new Apneia();
            desa = new Dessaturacao();
            ronc = new Ronco();
            plm = new PLM();
            this.FormClosing += AnaliseAuto_FormClosing;
            SatuBasal.TextChanged += SatuBasal_TextChanged;
            RecCPAP.Hide();

            GlobVar.ConnectionConfig = new OleDbConnection($@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={GlobVar.configBD};");
            GlobVar.ConnectionConfig.Open();

        }

        private void AnaliseAuto_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobVar.ConnectionConfig.Close();

            if (ap != null)
            {
                ap.Close();
                desa.Close();
                ronc.Close();
                plm.Close();
            }
        }
        public void realoctxt()
        {
            // Calcula a metade da largura do formulário
            int meioSize = this.Size.Width / 2;

            // Calcula a metade da largura do label
            int metadeLabel = Analisando.Width / 2;

            // Define a nova localização para centralizar o label
            Analisando.Location = new Point(meioSize - metadeLabel, Analisando.Location.Y);
        }
        private void AlteraFormSizeAnalisando()
        {
            Invoke(() =>
            {
                this.Size = new Size(478, 538);
                Processar.Location = new Point(22, 450);
                Cancelar.Location = new Point(325, 450);
            });
        }
        public void AtualizarProgresso(int valor)
        {
            if (progresso.InvokeRequired)
            {
                progresso.Invoke(new Action(() => AtualizarProgresso(valor)));
            }
            else
            {
                progresso.Value = valor;

            }
        }
        public float TamanhoProgresso()
        {
            float sizeP = 0;
            int a = 0;
            if (ApHipo.Checked) a++;
            if (Dessatu.Checked) a++;
            if (Ronco.Checked) a++;
            if (MovPernaPLM.Checked) a++;
            if (AplicaRegraPLM.Checked) a++;

            sizeP = 100 / a;
            return sizeP;
        }
        public async void Processar_Click(object sender, EventArgs e)
        {
            AlteraFormSizeAnalisando();
            progresso.Show();
            Analisando.Show();
            Cancelar.Tag = 1; // Altera a tag do cancelar, para modo de cancelamento de processo
            AtualizarProgresso(0);
            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;

            try
            {
                await Task.Run(() =>
                {
                    int valorBar = 0;
                    float incremento = TamanhoProgresso();

                    if (cancellationToken.IsCancellationRequested) return;

                    if (ApHipo.Checked)
                    {
                        Invoke(() => {
                            Analisando.Text = "Analisando Apneia e Hipopneia";
                            realoctxt();
                        });

                        int QualAnalisar = ap.Canula.Checked ? 13 : 8;
                        var row = GlobVar.tbl_MontagemSelecionada.AsEnumerable().FirstOrDefault(r => r.Field<int>("CodTipoCanal") == QualAnalisar);
                        codCanal = Convert.ToInt32(row["CodCanal1"]);
                        anApneiHipo = new AnaliseAutomaticaApneia(80, 50, 10, 3, 30, 5, codCanal, DeleteApnHip.Checked, this);

                        valorBar += (int)incremento;
                        AtualizarProgresso(valorBar);
                    }

                    if (cancellationToken.IsCancellationRequested) return;

                    if (Dessatu.Checked)
                    {
                        Invoke(() => {
                            Analisando.Text = "Analisando Dessaturação";
                            realoctxt();
                        });

                        anDessatu = new AnaliseAutomaticaDessaturacao(DeleteDess.Checked, SatuBasal.Text, this);
                        valorBar += (int)incremento;
                        AtualizarProgresso(valorBar);
                    }

                    if (cancellationToken.IsCancellationRequested) return;

                    if (MovPernaPLM.Checked)
                    {
                        Invoke(() => {
                            Analisando.Text = "Analisando Movimento de Perna e PLM";
                            realoctxt();
                        });

                        int QualAnalisar;

                        if (plm.PnAmbas.Checked)
                        {
                            QualAnalisar = 2;
                            anPLM = new AutoAnalisePLM(QualAnalisar, DeleteMovPPLM.Checked, this);
                            QualAnalisar = 26;
                            anPLM = new AutoAnalisePLM(QualAnalisar, DeleteMovPPLM.Checked, this);
                        }
                        else
                        {
                            QualAnalisar = plm.PnDireita.Checked ? 2 : 26;
                            anPLM = new AutoAnalisePLM(QualAnalisar, DeleteMovPPLM.Checked, this);
                        }

                        valorBar += (int)incremento;
                        AtualizarProgresso(valorBar);
                    }

                    if (cancellationToken.IsCancellationRequested) return;

                    if (AplicaRegraPLM.Checked)
                    {
                        Invoke(() => {
                            Analisando.Text = "Aplicando Regra PLM";
                            realoctxt();
                        });

                        int QualAnalisar;

                        if (plm.PnAmbas.Checked)
                        {
                            QualAnalisar = 2;
                            regraPLM = new RegraPLM(QualAnalisar, this);
                            QualAnalisar = 26;
                            regraPLM = new RegraPLM(QualAnalisar, this);
                        }
                        else
                        {
                            QualAnalisar = plm.PnDireita.Checked ? 2 : 26;
                            regraPLM = new RegraPLM(QualAnalisar, this);
                        }

                        valorBar += (int)incremento;
                        AtualizarProgresso(valorBar);
                    }

                    if (cancellationToken.IsCancellationRequested) return;

                    if (Ronco.Checked)
                    {
                        Invoke(() => {
                            Analisando.Text = "Analisando Ronco";
                            realoctxt();
                        });

                        int QualAnalisar = ronc.microfone.Checked ? 5 : 32;
                        anRonco = new AnaliseAutomaticaRonco(QualAnalisar, DeleteRonco.Checked, this);
                        valorBar += (int)incremento;
                        AtualizarProgresso(valorBar);
                    }

                    AtualizarProgresso(100);
                });

                this.Close();
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Análise cancelada.", "Cancelamento", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }
        private void Cancelar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Cancelar.Tag) != 0) // Faz com que o cancele sirva tanto para cancelar o processo, como para fechar separadamente
            {
                cancelar = true;
                cancellationTokenSource?.Cancel();
            }
            else
            {
                this.Close();
            }
        }
        private void SatuBasal_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(SatuBasal.Text, out int valor))
            {
                if (valor > 100)
                {
                    SatuBasal.Text = "100";
                }
            }
            else
            {
                SatuBasal.Text = "0"; // Caso o valor não seja um número válido
            }
        }

        private void AmpliApneia_Click(object sender, EventArgs e)
        {

            if (AmpliApneia.Text.Equals(">>"))
            {
                AmpliApneia.Text = "<<";
                // Obtém a posição do formulário principal
                int x = this.Location.X + this.Width - 7; // Posição à direita do formulário atual
                int y = this.Location.Y + 39;              // Alinhado na mesma altura do formulário atual

                // Define a posição do novo formulário
                ap.StartPosition = FormStartPosition.Manual;
                ap.Location = new Point(x, y);

                if (AmpliDessatu.Text.Equals("<<")) desa.Hide(); AmpliDessatu.Text = ">>";
                if (AmpliRonco.Text.Equals("<<")) ronc.Hide(); AmpliRonco.Text = ">>";
                if (AmpliMovPPLM.Text.Equals("<<")) plm.Hide(); AmpliMovPPLM.Text = ">>";

                ap.Owner = this;
                ap.TopMost = true;
                ap.Show();

            }
            else
            {
                AmpliApneia.Text = ">>";
                ap.Hide();
            }
        }

        private void AmpliDessatu_Click(object sender, EventArgs e)
        {
            if (AmpliDessatu.Text.Equals(">>"))
            {
                AmpliDessatu.Text = "<<";

                int x = this.Location.X + this.Width - 7;
                int y = this.Location.Y + 39;

                desa.StartPosition = FormStartPosition.Manual;
                desa.Location = new Point(x, y);
                desa.Owner = this;
                desa.TopMost = true;

                if (AmpliApneia.Text.Equals("<<")) ap.Hide(); AmpliApneia.Text = ">>";
                if (AmpliRonco.Text.Equals("<<")) ronc.Hide(); AmpliRonco.Text = ">>";
                if (AmpliMovPPLM.Text.Equals("<<")) plm.Hide(); AmpliMovPPLM.Text = ">>";

                desa.Show();
            }
            else
            {
                AmpliDessatu.Text = ">>";
                desa.Hide();
            }
        }

        private void AmpliRonco_Click(object sender, EventArgs e)
        {
            if (AmpliRonco.Text.Equals(">>"))
            {
                AmpliRonco.Text = "<<";

                int x = this.Location.X + this.Width - 7;
                int y = this.Location.Y + 39;

                ronc.StartPosition = FormStartPosition.Manual;
                ronc.Location = new Point(x, y);
                ronc.Owner = this;
                ronc.TopMost = true;

                if (AmpliApneia.Text.Equals("<<")) ap.Hide(); AmpliApneia.Text = ">>";
                if (AmpliDessatu.Text.Equals("<<")) desa.Hide(); AmpliDessatu.Text = ">>";
                if (AmpliMovPPLM.Text.Equals("<<")) plm.Hide(); AmpliMovPPLM.Text = ">>";

                ronc.Show();
            }
            else
            {
                AmpliRonco.Text = ">>";
                ronc.Hide();
            }
        }

        private void AmpliMovPPLM_Click(object sender, EventArgs e)
        {
            if (AmpliMovPPLM.Text.Equals(">>"))
            {
                AmpliMovPPLM.Text = "<<";

                int x = this.Location.X + this.Width - 7;
                int y = this.Location.Y + 39;

                plm.StartPosition = FormStartPosition.Manual;
                plm.Location = new Point(x, y);
                plm.Owner = this;
                plm.TopMost = true;

                if (AmpliApneia.Text.Equals("<<")) ap.Hide(); AmpliApneia.Text = ">>";
                if (AmpliDessatu.Text.Equals("<<")) desa.Hide(); AmpliDessatu.Text = ">>";
                if (AmpliRonco.Text.Equals("<<")) ronc.Hide(); AmpliRonco.Text = ">>";

                plm.Show();
            }
            else
            {
                AmpliMovPPLM.Text = ">>";
                plm.Hide();
            }
        }

    }
}
