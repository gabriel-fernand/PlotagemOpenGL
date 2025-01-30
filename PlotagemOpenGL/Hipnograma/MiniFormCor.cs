using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

public class MiniFormCor : Form
{
    private RadioButton rbPreto;
    private RadioButton rbCorEvento;
    private Button btnOK;

    private void InitializeComponent()
    {
        SuspendLayout();
        // 
        // MiniFormCor
        // 
        ClientSize = new Size(227, 130);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Name = "MiniFormCor";
        ResumeLayout(false);
    }

    public int Resultado { get; private set; } = -1; // -1 = Nenhuma seleção feita

    public MiniFormCor(string titulo, int valorAtual)
    {
        // Configurações básicas do Form
        this.Text = titulo;
        this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        this.StartPosition = FormStartPosition.CenterParent;
        this.Size = new System.Drawing.Size(240, 177);
        this.BackColor = Color.White;
        this.Font = new Font("Segoe UI", 10);

        // RadioButton Preto
        rbPreto = new RadioButton
        {
            Text = "Preto",
            Location = new Point(20, 15),
            AutoSize = true,
            Checked = (valorAtual == 0)
        };
        this.Controls.Add(rbPreto);

        // RadioButton Cor do Evento
        rbCorEvento = new RadioButton
        {
            Text = "Cor do evento",
            Location = new Point(20, 45),
            AutoSize = true,
            Checked = (valorAtual == 1)
        };
        this.Controls.Add(rbCorEvento);

        // Botão OK
        btnOK = new Button
        {
            Text = "OK",
            Location = new Point(150, 90),
            Size = new Size(60, 30),
            BackColor = Color.LightGray
        };
        btnOK.Click += (sender, e) =>
        {
            Resultado = rbCorEvento.Checked ? 1 : 0;
            this.DialogResult = DialogResult.OK;
            this.Close();
        };
        this.Controls.Add(btnOK);
    }
}
