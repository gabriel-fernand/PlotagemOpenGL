using Accord.Statistics.Running;
using AxWMPLib;
using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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
        private Rectangle vidi;
        private Size formOriginalSize;
        public static float locVideo;
        private Thread videoUpdateThread;
        private bool isRunning = false; // Controla a execução da thread

        public FormVideo()
        {
            InitializeComponent();
            videoPlayer.PlayStateChange += VideoPlayer_PlayStateChange; // Associa o evento
            
            formOriginalSize = this.Size;
            this.Resize += res;
            vidi = new Rectangle(videoPlayer.Location, videoPlayer.Size);
        }
        public void Resiz(Control c, Rectangle r)
        {
            // Calcula a razão de redimensionamento com base no tamanho atual do formulário
            float xRatio = (float)this.ClientSize.Width / (float)formOriginalSize.Width;
            float yRatio = (float)this.ClientSize.Height / (float)formOriginalSize.Height;

            // Ajusta a posição e tamanho do controle proporcionalmente
            int newX = (int)(r.X * xRatio);
            int newY = (int)(r.Y * yRatio);
            int newWidth = (int)(r.Width * xRatio);
            int newHeight = (int)(r.Height * yRatio);

            // Aplica as novas dimensões e localização ao controle
            c.Location = new Point(0,0);
            c.Size = new Size(newWidth, newHeight);
        }
        public void res(object sender, EventArgs e)
        {
            Resiz(videoPlayer, vidi);

        }
        private DateTime lastUpdateTime; // Para rastrear o tempo da última atualização
        private double lastVideoPosition; // Para armazenar a última posição do vídeo

        public async void VideoPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (isProgrammaticChange)
            {
                videoPlayer.Ctlcontrols.pause();
                isRunning = false;
                return;
            }

            switch ((WMPLib.WMPPlayState)e.newState)
            {
                case WMPLib.WMPPlayState.wmppsPlaying:
                    if (!isRunning)
                    {
                        isRunning = true;
                        lastUpdateTime = DateTime.Now; // Marca o início do ciclo
                        lastVideoPosition = videoPlayer.Ctlcontrols.currentPosition; // Armazena a posição inicial
                        videoUpdateThread = new Thread(UpdateVideoPosition);
                        videoUpdateThread.IsBackground = true; // Permite encerrar a thread com o aplicativo
                        videoUpdateThread.Start();
                    }
                    break;

                case WMPLib.WMPPlayState.wmppsPaused:
                case WMPLib.WMPPlayState.wmppsStopped:
                    isRunning = false; // Interrompe a execução da thread
                    break;
            }
        }

        private void UpdateVideoPosition()
        {
            while (isRunning)
            {
                try
                {
                    // Obtém a posição atual do vídeo
                    double currentVideoPosition = videoPlayer.Ctlcontrols.currentPosition;

                    // Calcula o tempo decorrido desde a última atualização
                    DateTime now = DateTime.Now;
                    double elapsedSeconds = (now - lastUpdateTime).TotalSeconds;

                    // Atualiza o ponteiro apenas se o vídeo estiver realmente avançando
                    if (currentVideoPosition > lastVideoPosition)
                    {
                        double delta = currentVideoPosition - lastVideoPosition; // Diferença entre as posições do vídeo
                        GlobVar.ponteiroVideo += (float)(delta * GlobVar.namos); // Ajusta com base no valor de GlobVar.namos
                        Tela_Plotagem.OnVideoStateChanged(true);

                        // Atualiza os valores para o próximo ciclo
                        lastUpdateTime = now;
                        lastVideoPosition = currentVideoPosition;
                    }

                    // Dorme por um curto período para reduzir o uso de CPU
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro na atualização da posição do vídeo: {ex.Message}");
                }
            }
        }


        public void videoCarregado()
        {
            try
            {
                int pag = (int)GlobVar.ponteiroVideo / GlobVar.namos;

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
                            videoPlayer.Ctlcontrols.play(); // Pausa o vídeo

                            videoPlayer.Ctlcontrols.currentPosition = videoInitialPosition; // Define a posição inicial

                            videoPlayer.Ctlcontrols.pause(); // Pausa o vídeo
                            isProgrammaticChange = false; // Libera eventos

                            // Aguarda brevemente para garantir a renderização
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
            int pag = (int)GlobVar.ponteiroVideo / GlobVar.namos;
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
                        videoPlayer.Ctlcontrols.play(); // Dá um breve play para renderizar o quadro

                        videoPlayer.Ctlcontrols.currentPosition = Math.Abs(Convert.ToInt32(matchingRow["TickIni"]) - tickini) / 1000.0;

                        videoPlayer.Ctlcontrols.pause(); // Pausa o vídeo
                        isProgrammaticChange = false; // Libera eventos

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
        public static bool ponteiroCoord(int Xinicial, int Yinicial)
        {
            try
            {
                bool sim = false;

                float outX;
                float outY;

                Tela_Plotagem.ConvertToOpenGLCoordinates(Xinicial, Yinicial, out outX, out outY);

                if (outX >= GlobVar.ponteiroVideo - 10 && outX <= GlobVar.ponteiroVideo + 10)
                {
                    sim = true;
                }
                else
                {
                    sim = false;
                }

                return sim;
            }
            catch { return false; }
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
