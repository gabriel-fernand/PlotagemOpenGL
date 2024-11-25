using AxWMPLib;
using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class FormVideo : Form
    {
        private bool isProgrammaticChange = false; // Flag para controlar alterações programáticas
        private double videoInitialPosition = 0.0; // Posição inicial do vídeo em segundos
        private string videoname;
        private string locvideo = @"C:\Temp\Dat\";

        public FormVideo()
        {
            InitializeComponent();
            videoPlayer.PlayStateChange += VideoPlayer_PlayStateChange; // Associa o evento
        }

        private void VideoPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (isProgrammaticChange) return; // Ignora alterações programáticas

            switch ((WMPLib.WMPPlayState)e.newState)
            {
                case WMPLib.WMPPlayState.wmppsPlaying:
                    // Pausa imediatamente após começar a tocar
                    if (isProgrammaticChange)
                    {
                        videoPlayer.Ctlcontrols.pause();
                    }
                    break;

                case WMPLib.WMPPlayState.wmppsPaused:
                    // Nenhuma ação adicional necessária aqui
                    break;

                case WMPLib.WMPPlayState.wmppsStopped:
                    // Nenhuma ação adicional necessária aqui
                    break;
            }
        }

        public void videoCarregado()
        {
            try
            {
                int pag = GlobVar.indice / GlobVar.namos;

                var row = GlobVar.tbl_Paginas.AsEnumerable()
                           .FirstOrDefault(r => r.Field<int>("NumPag") == pag);

                if (row != null)
                {
                    int tickini = Convert.ToInt32(row["TickIni"]);

                    // Localiza a linha na tabela ArqVideo onde TickIni < tickini < TickFim
                    var matchingRow = GlobVar.tbl_ArqVideo.AsEnumerable()
                                       .FirstOrDefault(r => r.Field<int>("TickIni") < tickini &&
                                                            r.Field<int>("TickFim") > tickini);

                    if (matchingRow != null)
                    {
                        videoname = matchingRow["Nome_Arquivo"].ToString();
                        string videoPath = Path.Combine(locvideo, videoname);

                        // Calcula a posição inicial do vídeo em segundos
                        videoInitialPosition = Math.Abs(Convert.ToInt32(matchingRow["TickIni"]) - tickini) / 1000.0;

                        if (File.Exists(videoPath))
                        {
                            isProgrammaticChange = true; // Bloqueia eventos durante alteração programática

                            videoPlayer.URL = videoPath; // Carrega o vídeo
                            videoPlayer.Ctlcontrols.currentPosition = videoInitialPosition; // Define a posição inicial
                            videoPlayer.Ctlcontrols.play(); // Dá um breve play para renderizar o quadro


                            // Aguarda brevemente para garantir a renderização
                            Task.Delay(20).ContinueWith(_ =>
                            {
                                videoPlayer.Ctlcontrols.currentPosition = videoInitialPosition; // Define a posição inicial
                                videoPlayer.Ctlcontrols.pause(); // Pausa o vídeo
                                isProgrammaticChange = false; // Libera eventos
                            });
                        }
                        else
                        {
                            MessageBox.Show($"Arquivo de vídeo não encontrado: {videoPath}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o vídeo: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void attLocVideo()
        {
            int pag = GlobVar.indice / GlobVar.namos;
            var row = GlobVar.tbl_Paginas.AsEnumerable()
                       .FirstOrDefault(r => r.Field<int>("NumPag") == pag);

            if (row != null)
            {
                int tickini = Convert.ToInt32(row["TickIni"]);

                // Localiza a linha na tabela ArqVideo onde TickIni < tickini < TickFim
                var matchingRow = GlobVar.tbl_ArqVideo.AsEnumerable()
                                   .FirstOrDefault(r => r.Field<int>("TickIni") < tickini &&
                                                        r.Field<int>("TickFim") > tickini);

                if (matchingRow != null)
                {
                    string newvideoname = matchingRow["Nome_Arquivo"].ToString();

                    if (string.Equals(videoname, newvideoname, StringComparison.OrdinalIgnoreCase))
                    {
                        isProgrammaticChange = true; // Bloqueia eventos durante alteração programática

                        videoPlayer.Ctlcontrols.currentPosition = Math.Abs(Convert.ToInt32(matchingRow["TickIni"]) - tickini) / 1000.0;
                        videoPlayer.Ctlcontrols.play(); // Dá um breve play para renderizar o quadro

                        Task.Delay(2).ContinueWith(_ =>
                        {
                            videoPlayer.Ctlcontrols.pause(); // Pausa o vídeo
                            isProgrammaticChange = false; // Libera eventos
                        });
                    }
                    else
                    {
                        videoname = newvideoname;
                        string videoPath = Path.Combine(locvideo, videoname);

                        if (File.Exists(videoPath))
                        {
                            isProgrammaticChange = true; // Bloqueia eventos durante alteração programática

                            videoPlayer.URL = videoPath; // Carrega o vídeo
                            videoPlayer.Ctlcontrols.currentPosition = Math.Abs(Convert.ToInt32(matchingRow["TickIni"]) - tickini) / 1000.0;
                            videoPlayer.Ctlcontrols.play(); // Dá um breve play para renderizar o quadro

                            Task.Delay(2).ContinueWith(_ =>
                            {
                                videoPlayer.Ctlcontrols.pause(); // Pausa o vídeo
                                isProgrammaticChange = false; // Libera eventos
                            });
                        }
                        else
                        {
                            videoPlayer.URL = string.Empty; // Limpa o player
                        }
                    }
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancela o fechamento
                this.Hide(); // Oculta o formulário
            }
            else
            {
                base.OnFormClosing(e); // Permite o fechamento em outros casos
            }
        }
    }
}
