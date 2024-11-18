using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

public class KeyChecker
{
    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    private Thread keyCheckThread;
    private bool isRunning;

    public event Action LeftKeyPressed;
    public event Action RightKeyPressed;
    public event Action UpKeyPressed;
    public event Action DownKeyPressed;

    public KeyChecker()
    {
        isRunning = true;
        keyCheckThread = new Thread(CheckKeys);
        keyCheckThread.Start();
    }

    private void CheckKeys()
    {
        while (isRunning)
        {
            // Verifica a tecla Left (seta esquerda)
            if ((GetAsyncKeyState((int)Keys.Left) & 0x8000) != 0)
            {
                LeftKeyPressed?.Invoke();
            }

            // Verifica a tecla Right (seta direita)
            if ((GetAsyncKeyState((int)Keys.Right) & 0x8000) != 0)
            {
                RightKeyPressed?.Invoke();
            }

            if ((GetAsyncKeyState((int)Keys.Up) & 0x8000) != 0)
            {
                UpKeyPressed?.Invoke();
            }
            if ((GetAsyncKeyState((int)Keys.Down) & 0x8000) != 0)
            {
                DownKeyPressed?.Invoke();
            }

            Thread.Sleep(10); // Evita sobrecarga de CPU
        }
    }

    public void Stop()
    {
        isRunning = false;
        keyCheckThread.Join();
    }
}
