using System;
using System.Runtime.InteropServices;
using System.Text;

public class IniFile
{
    private readonly string _path;

    public IniFile(string path)
    {
        _path = path;
    }

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(
        string section,
        string key,
        string defaultValue,
        StringBuilder retVal,
        int size,
        string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern long WritePrivateProfileString(
        string section,
        string key,
        string value,
        string filePath);

    public string Read(string section, string key, string defaultValue = "")
    {
        var result = new StringBuilder(1024);
        GetPrivateProfileString(section, key, defaultValue, result, result.Capacity, _path);
        return result.ToString();
    }

    public bool Write(string section, string key, string value)
    {
        return WritePrivateProfileString(section, key, value, _path) != 0;
    }

    public bool DeleteKey(string section, string key)
    {
        return WritePrivateProfileString(section, key, null, _path) != 0;
    }

    public bool DeleteSection(string section)
    {
        return WritePrivateProfileString(section, null, null, _path) != 0;
    }
}

/* Como Usar
         var ini = new IniFile("C:\\Caminho\\Para\\Config.ini");

        string nse = ini.Read("ICELERA", "NSE");
        string eq = ini.Read("ICELERA", "EQ");
        string temVideo = ini.Read("ICELERA", "TEMVIDEO");

        Console.WriteLine($"NSE: {nse}, EQ: {eq}, TEMVIDEO: {temVideo}");

 */