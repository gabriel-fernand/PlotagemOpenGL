using AxWMPLib;
using PlotagemOpenGL.auxi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.FormesMenuPanels
{
    public partial class FormVideo : Form
    {
        int videoloc;
        string videoname;
        string locvideo = @"C:\Temp\Dat\";
        public FormVideo()
        {
            InitializeComponent();

        }
        public void videoCarregado()
        {
            try{
            //int pag = (GlobVar.ultimaPag - 1) * 30;
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
                        int tickNormalizado = Convert.ToInt32(matchingRow["TickFim"]) - Convert.ToInt32(matchingRow["TickIni"]);
                        tickini = Math.Abs(Convert.ToInt32(matchingRow["TickIni"]) - tickini);
                        videoname = matchingRow["Nome_Arquivo"].ToString();

                        // Constrói o caminho completo do arquivo de vídeo
                        string videoPath = System.IO.Path.Combine(locvideo, videoname);

                        if (System.IO.File.Exists(videoPath))
                        {
                            // Carrega o vídeo no Windows Media Player
                            videoPlayer.URL = videoPath;


                            // Configura a posição inicial e pausa logo após o carregamento
                            videoPlayer.Ctlcontrols.currentPosition = tickini / 1000.0;
                            videoPlayer.Ctlcontrols.play();
                            videoPlayer.Ctlcontrols.pause();

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
                    int tickNormalizado = Convert.ToInt32(matchingRow["TickFim"]) - Convert.ToInt32(matchingRow["TickIni"]);
                    tickini = Math.Abs(Convert.ToInt32(matchingRow["TickIni"]) - tickini);
                    string newvideoname = matchingRow["Nome_Arquivo"].ToString();

                    if (string.Equals(videoname, newvideoname, StringComparison.OrdinalIgnoreCase))
                    {

                        // Configura a posição inicial e pausa logo após o carregamento
                        videoPlayer.Ctlcontrols.currentPosition = tickini / 1000.0;
                        videoPlayer.Ctlcontrols.play();
                        videoPlayer.Ctlcontrols.pause();

                    }
                    else
                    {
                        videoname = newvideoname;
                        // Constrói o caminho completo do arquivo de vídeo
                        string videoPath = System.IO.Path.Combine(locvideo, videoname);

                        if (System.IO.File.Exists(videoPath))
                        {
                            // Carrega o vídeo no Windows Media Player
                            videoPlayer.URL = videoPath;

                            // Configura a posição inicial e pausa logo após o carregamento
                            videoPlayer.Ctlcontrols.currentPosition = tickini / 1000.0;
                            videoPlayer.Ctlcontrols.play();
                            videoPlayer.Ctlcontrols.pause();

                        }
                        else
                        {
                            videoPlayer.URL = string.Empty;  // Ou você pode usar um arquivo de vídeo preto se preferir
                                                             // Ou para um vídeo preto temporário, caso tenha um arquivo de "black screen" em formato MP4
                                                             // videoPlayer.URL = @"C:\path\to\black_screen_video.mp4";

                            // Pausa o vídeo para que ele não comece a rodar
                            videoPlayer.Ctlcontrols.pause();

                        }
                    }
                }
            }
        }
    
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Verifica se o fechamento foi iniciado pelo usuário (não por encerramento do programa)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; // Cancela o fechamento
                this.Hide(); // Oculta o formulário
            }
            else
            {
                base.OnFormClosing(e); // Permite o fechamento em outros casos (por exemplo, encerramento do aplicativo)
            }
        }
        private void videoPlayer_Enter(object sender, EventArgs e)
        {

        }
    }
}
