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
    public bool FormIsFocused { get; set; }

    public event Action LeftKeyPressed;
    public event Action RightKeyPressed;
    public event Action UpKeyPressed;
    public event Action DownKeyPressed;

    public event Action NumPad0;
    public event Action D0;

    public event Action NumPad5;
    public event Action D5;
    public event Action R;

    public event Action NumPad1;
    public event Action D1;

    public event Action NumPad2;
    public event Action D2;

    public event Action NumPad3;
    public event Action D3;

    public event Action N;
    public event Action T;



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

            if (!FormIsFocused)
            {
                Thread.Sleep(50);
                continue;
            }
            if (FormIsFocused)
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
                // Verifica a tecla Right (seta Cima)
                if ((GetAsyncKeyState((int)Keys.Up) & 0x8000) != 0)
                {
                    UpKeyPressed?.Invoke();
                }
                // Verifica a tecla Right (seta baixo)
                if ((GetAsyncKeyState((int)Keys.Down) & 0x8000) != 0)
                {
                    DownKeyPressed?.Invoke();
                }


                if ((GetAsyncKeyState((int)Keys.NumPad0) & 0x8000) != 0)
                {
                    NumPad0?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.D0) & 0x8000) != 0)
                {
                    D0?.Invoke();
                }


                if ((GetAsyncKeyState((int)Keys.NumPad5) & 0x8000) != 0)
                {
                    NumPad5?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.D5) & 0x8000) != 0)
                {
                    D5?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.R) & 0x8000) != 0)
                {
                    R?.Invoke();
                }


                if ((GetAsyncKeyState((int)Keys.NumPad1) & 0x8000) != 0)
                {
                    NumPad1?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.D1) & 0x8000) != 0)
                {
                    D1?.Invoke();
                }


                if ((GetAsyncKeyState((int)Keys.NumPad2) & 0x8000) != 0)
                {
                    NumPad2?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.D2) & 0x8000) != 0)
                {
                    D2?.Invoke();
                }


                if ((GetAsyncKeyState((int)Keys.NumPad3) & 0x8000) != 0)
                {
                    NumPad3?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.D3) & 0x8000) != 0)
                {
                    D3?.Invoke();
                }


                if ((GetAsyncKeyState((int)Keys.N) & 0x8000) != 0)
                {
                    N?.Invoke();
                }
                if ((GetAsyncKeyState((int)Keys.T) & 0x8000) != 0)
                {
                    T?.Invoke();
                }
                Thread.Sleep(10); // Evita sobrecarga de CPU
            }
        }
    }

    public void Stop()
    {
        isRunning = false;
        keyCheckThread.Join();
    }
}
