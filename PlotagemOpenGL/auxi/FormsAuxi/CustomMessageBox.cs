using System;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

public class CustomMessageBox : Form
{
    private Label lblMessage;
    private Button btnOk;
    private System.Timers.Timer closeTimer;

    public CustomMessageBox(string message, int timeoutSeconds = 2)
    {
        this.Text = "Aviso";
        this.Size = new Size(400, 150);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        InitializeComponent();

        lblMessage = new Label()
        {
            Text = message,
            Dock = DockStyle.Top,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter
        };
        btnOk.Click += (s, e) => this.Close();

        this.Controls.Add(lblMessage);

        closeTimer = new System.Timers.Timer(timeoutSeconds * 1000);
        closeTimer.Elapsed += (s, e) =>
        {
            closeTimer.Stop();
            if (this.InvokeRequired)
                this.Invoke(new Action(() => this.Close()));
            else
                this.Close();
        };
        closeTimer.Start();
    }

    public static void ShowTimed(string message, int timeoutSeconds = 4)
    {
        using (var msgBox = new CustomMessageBox(message, timeoutSeconds))
        {
            msgBox.ShowDialog();
        }
    }

    private void InitializeComponent()
    {
        btnOk = new Button();
        SuspendLayout();
        // 
        // btnOk
        // 
        btnOk.Location = new Point(147, 52);
        btnOk.Name = "btnOk";
        btnOk.Size = new Size(94, 29);
        btnOk.TabIndex = 0;
        btnOk.Text = "Ok";
        btnOk.UseVisualStyleBackColor = true;
        // 
        // CustomMessageBox
        // 
        ClientSize = new Size(382, 83);
        Controls.Add(btnOk);
        Name = "CustomMessageBox";
        ResumeLayout(false);
    }
}
