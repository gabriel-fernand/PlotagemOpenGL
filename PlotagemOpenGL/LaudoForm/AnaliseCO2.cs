using Accord.Math;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.Hipnograma;
using System;
using System.Data;
using System.IO;
using System.Text;

public class AnaliseCO2
{
    private static string _arquivoIni;
    private static string _arquivoExame;
    private static DataTable _tblPaginas = new DataTable();
    private static DataTable _tblEventos = new DataTable();

    public AnaliseCO2()
    {
        _arquivoIni = @"C:\Temp\Config.ini";
        _arquivoExame = Path.GetFileNameWithoutExtension(GlobVar.textFile);
        _tblPaginas = GlobVar.tbl_Paginas.AsEnumerable().CopyToDataTable();
        _tblEventos = GlobVar.eventos.AsEnumerable().CopyToDataTable();
    }

    public static string AnaliseAutomatica()
    {
        _arquivoIni = @"C:\Temp\Config.ini";
        _arquivoExame = Path.GetFileNameWithoutExtension(GlobVar.textFile);
        string _direct = Path.GetDirectoryName(GlobVar.textFile);
        _tblPaginas = GlobVar.tbl_Paginas.AsEnumerable().CopyToDataTable();
        _tblEventos = GlobVar.eventos.AsEnumerable().CopyToDataTable();

        var ini = new IniFile(_arquivoIni);

        int vlrAcima = int.Parse(ini.Read("LAUDO", "CO2", "50"));
        int vlrAcima2 = int.Parse(ini.Read("LAUDO", "CO2B", "55"));
        int co2Minimo = int.Parse(ini.Read("LAUDO", "CO2MINIMO", "30"));
        int co2Maximo = int.Parse(ini.Read("LAUDO", "CO2MAXIMO", "100"));

        // Pega páginas desprezadas
        string paginasDesprezadas = "#";
        foreach (DataRow row in _tblEventos.Rows)
        {
            if (Convert.ToInt32(row["CodEvento"]) == 99)
            {
                paginasDesprezadas += row["NumPag"].ToString().Trim() + "#";
            }
        }

        // Lê o arquivo .CO2
        string arquivoCo2 =Path.Combine(_direct + $"{_arquivoExame}.CO2");
        if (!File.Exists(arquivoCo2))
            throw new FileNotFoundException("Arquivo CO2 não encontrado.", arquivoCo2);

        string texto = File.ReadAllText(arquivoCo2);
        if (string.IsNullOrEmpty(texto))
            return "00000000000000000000000000000000.0%00.0%";

        int co2Maior = 0;
        int co2Menor = 1000;
        long co2Media = 0;
        long qtde = 0;
        long acima = 0;
        long acima2 = 0;

        int indexPagina = 0;
        DataRow[] paginas = _tblPaginas.Select("", "NumPag ASC");

        while (texto.Length >= 4 && indexPagina < paginas.Length)
        {
            int registro = int.Parse(texto.Substring(0, 4));

            int estagio = Convert.ToInt32(paginas[indexPagina]["estagio"]);
            int numPag = Convert.ToInt32(paginas[indexPagina]["NumPag"]);

            if (estagio > 0 && registro >= co2Minimo)
            {
                if (!paginasDesprezadas.Contains($"#{numPag}#"))
                {
                    if (registro <= co2Maximo)
                    {
                        if (registro < co2Menor) co2Menor = registro;
                        if (registro > co2Maior) co2Maior = registro;
                        co2Media += registro;

                        if (registro > vlrAcima) acima++;
                        if (registro > vlrAcima2) acima2++;

                        qtde++;
                    }
                }
            }

            texto = texto.Substring(4);
            indexPagina++;
        }

        double aclimPerc = qtde > 0 ? (double)acima / qtde : 0;
        double aclim2Perc = qtde > 0 ? (double)acima2 / qtde : 0;

        long mediaFinal = qtde > 0 ? co2Media / qtde : 0;

        // Formata exatamente como no VB6
        string resultado = $"{mediaFinal:000000}{co2Maior:000000}{co2Menor:000000}{acima:000000}{vlrAcima:000000}{acima2:000000}{vlrAcima2:000000}"
                         + $"{aclimPerc:00.0%}{aclim2Perc:00.0%}";

        return resultado;
    }

    public static void GetPrimeiroCO2(string filename)
    {
        _arquivoIni = @"C:\Temp\Config.ini";
        _arquivoExame = Path.GetFileNameWithoutExtension(GlobVar.textFile);
        _tblPaginas = GlobVar.tbl_Paginas.AsEnumerable().CopyToDataTable();
        _tblEventos = GlobVar.eventos.AsEnumerable().CopyToDataTable();

        // Leitura de dados da tabela tbl_DadosExame
        int capnoLimiteInfValor, capnoLimiteInfAnal, capnoLimiteSupValor, capnoLimiteSupAnal;
        var row = GlobVar.tbl_DadosExame.Rows[0];
        capnoLimiteInfValor = Convert.ToInt32(row["CapnoEtCO2_LimiteInf_Valor"]);
        capnoLimiteInfAnal = Convert.ToInt32(row["CapnoEtCO2_LimiteInf_Anal"]);
        capnoLimiteSupValor = Convert.ToInt32(row["CapnoEtCO2_LimiteSup_Valor"]);
        capnoLimiteSupAnal = Convert.ToInt32(row["CapnoEtCO2_LimiteSup_Anal"]);

        string direct = Path.GetDirectoryName(GlobVar.textFile);
        string co2File = Path.Combine(direct, Path.GetFileNameWithoutExtension(GlobVar.textFile) + ".CO2");
        if (!File.Exists(co2File))
        {
            int tipo_CO2 = 30;

            int tipo = 0;
            bool achou = false;
            int codCanal = 0;
            foreach (DataRow rw in GlobVar.tbl_CanaisAdquiridos.Rows)
            {
                if (Convert.ToInt32(rw["CodTipoCanal"]) == tipo_CO2 || Convert.ToInt32(rw["CodTipoCanal"]) == 38)
                {
                    codCanal = Convert.ToInt32("CodCanal1");
                    achou = true;
                    break;
                }
            }

            if (!achou)
            {
                foreach (DataRow rw in GlobVar.tbl_CanaisAdquiridos.Rows)
                {
                    if (Convert.ToInt32(rw["CodTipoCanal"]) == 20)
                    {
                        codCanal = Convert.ToInt32(rw["CodCanal1"]);
                        break;
                    }
                }
            }

            string texto = "";

            int indexCod = GlobVar.codCanal.IndexOf(codCanal);

            // Pega os índices de início e fim uma vez
            int startCol = GlobVar.ponteiroI[indexCod];


            using (var sw = new StreamWriter(co2File, false))
            {
                for (int i = 0; i < GlobVar.size; i++)
                {
                    if (tipo == tipo_CO2)
                    {
                        texto += GlobVar.matrizCompleta[i, startCol].ToString("D4");
                    }
                    else
                    {
                        double valor_sao2 = - GlobVar.matrizCompleta[i, startCol];
                        valor_sao2 = capnoLimiteInfValor + Math.Abs(valor_sao2 - capnoLimiteInfAnal) / Math.Abs((capnoLimiteSupAnal - capnoLimiteInfAnal) / (capnoLimiteSupValor - capnoLimiteInfValor));
                        texto += ((int)Math.Round(valor_sao2)).ToString("D4");
                    }
                }

                sw.WriteLine(texto);
            }
        }
    }

}
