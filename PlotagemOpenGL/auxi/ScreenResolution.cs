using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class ScreenResolution
{
    [StructLayout(LayoutKind.Sequential)]
    public struct DEVMODE
    {
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        public string dmDeviceName;
        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        public string dmFormName;
        public short dmLogPixels;
        public int dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;
    }

    [DllImport("user32.dll")]
    public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

    public const int ENUM_CURRENT_SETTINGS = -1;
    public const int CDS_UPDATEREGISTRY = 0x01;
    public const int DISP_CHANGE_SUCCESSFUL = 0;

    public static void SetResolution(int width, int height)
    {
        DEVMODE dm = new DEVMODE();
        dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
        EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref dm);

        dm.dmPelsWidth = width;
        dm.dmPelsHeight = height;
        dm.dmFields = 0x80000 | 0x100000; // DM_PELSWIDTH | DM_PELSHEIGHT

        int iRet = ChangeDisplaySettings(ref dm, CDS_UPDATEREGISTRY);

        if (iRet != DISP_CHANGE_SUCCESSFUL)
        {
            MessageBox.Show("Não foi possível alterar a resolução de tela.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    [DllImport("user32.dll")]
    public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);
}