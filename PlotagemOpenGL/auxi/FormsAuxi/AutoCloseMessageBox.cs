using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlotagemOpenGL.auxi.FormsAuxi
{
    public partial class AutoCloseMessageBox : Form
    {
        private Timer timer;

        public AutoCloseMessageBox(string message, string title, int timeout)
        {
            // Cria o Label para a mensagem
            Label lblMessage = new Label()
            {
                Text = message,
                AutoSize = true,
                Location = new System.Drawing.Point(10, 10)
            };

            // Cria o botão OK
            Button btnOK = new Button()
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(100, lblMessage.Bottom + 10)
            };

            // Adiciona os controles ao formulário
            Controls.Add(lblMessage);
            Controls.Add(btnOK);

            // Configura o título e o tamanho da caixa de diálogo
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            StartPosition = FormStartPosition.CenterScreen;

            // Define as propriedades para remover os botões e a barra de título
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;
            Text = ""; // Remove o título

            // Inicializa o temporizador
            timer = new Timer();
            timer.Interval = timeout; // Tempo em milissegundos (3000 = 3 segundos)
            timer.Tick += (s, e) => { Close(); };
            timer.Start();

            // Fecha o formulário quando o botão OK é clicado
            btnOK.Click += (s, e) => { Close(); };
        }

        public static DialogResult Show(string message, string title, int timeout)
        {
            using (AutoCloseMessageBox autoCloseMessageBox = new AutoCloseMessageBox(message, title, timeout))
            {
                return autoCloseMessageBox.ShowDialog();
            }
        }
    }
}
