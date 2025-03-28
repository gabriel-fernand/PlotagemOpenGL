using Accord.Math;
using ClassesBDNano;
using PlotagemOpenGL.auxi;
using PlotagemOpenGL.auxi.auxPlotagem;
using PlotagemOpenGL.BD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlotagemOpenGL.FormesMenuPanels.AuxiAutoAnalise
{
    internal class AnaliseAutomaticaDessaturacao
    {
        int CodAnalisar = 66;

        bool excluirEvento = false;
        int CodEvento = 17;
        private int SaO2_100 = 511;
        private int Sat_Basal_inicial = 100;
        private int menor_sat, maior_sat, acum, qtd, Abaixo90, Abaixo80, Abaixo70;
        private int Sat_Segundos = 60, Sat_Desvio = 100, Sat_QuedaAbaixoDe = 4;
        private int Sat_DuracaoMinima = 10, Sat_Recalcular = 900, Sat_DesprezarAbaixo = 40;
        int Sat_Valor;
        string Sat_Media = "";
        string Sat_Media_Calc;
        private List<int> PagDesprezadas;

        public AnaliseAutomaticaDessaturacao(bool exclui, string sat_val)
        {

            if (!verificaExistenciaDoCanalNaMontagem(CodAnalisar)) return;
            this.excluirEvento = exclui;
            this.Sat_Valor = int.Parse(sat_val);
            PagDesprezadas = new List<int>();
            LerCodigo();
        }
        private bool verificaExistenciaDoCanalNaMontagem(int CodCanal)
        {
            return GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                .Any(row => row.Field<int>("CodCanal1") == CodCanal);
        }

        private void LerCodigo()
        {
            int ini_evento = -1;
            int vPico = 0;
            int vVale = 0;
            int fim_evento = 0;
            int vIntervalo = 0;

            if (excluirEvento)
            {
                // Exclui eventos no DataTable `GlobVar.eventosUpdate`
                List<int> codigosExcluir = new List<int> {17};

                foreach (int a in codigosExcluir)
                {
                    ExcluiEvento(a);
                }
                foreach (DataRow row in GlobVar.eventosUpdate.Rows.Cast<DataRow>().ToList())
                {
                    if (codigosExcluir.Contains(Convert.ToInt32(row["CodEvento"])))
                    {
                        GlobVar.eventosUpdate.Rows.Remove(row);
                    }
                }

                GlobVar.eventosUpdate.AcceptChanges();
            }

            // Obtendo a primeira linha de tbl_DadosExame, se existir
            if (GlobVar.tbl_DadosExame.Rows.Count > 0)
            {
                var rw = GlobVar.tbl_DadosExame.Rows[0];
                SaO2_100 = rw["SaO2_100"] != DBNull.Value ? Convert.ToInt32(rw["SaO2_100"]) : SaO2_100;
                Sat_Basal_inicial = rw["SatBasal"] != DBNull.Value ? Convert.ToInt32(rw["SatBasal"]) : Sat_Basal_inicial;
            }

            // Inicializa variáveis
            menor_sat = SaO2_100;
            maior_sat = 0;
            acum = qtd = Abaixo90 = Abaixo80 = Abaixo70 = 0;

            double ref_90 = SaO2_100 * 0.9;
            double ref_80 = SaO2_100 * 0.8;
            double ref_70 = SaO2_100 * 0.7;

            // Obtendo a primeira linha de tbl_ParametrosParaAnalisar, se existir
            if (GlobVar.tbl_ParametrosParaAnalisar.Rows.Count > 0)
            {
                var rs = GlobVar.tbl_ParametrosParaAnalisar.Rows[0];
                Sat_QuedaAbaixoDe = rs["Sat_QuedaAbaixoDe"] != DBNull.Value ? Convert.ToInt32(rs["Sat_QuedaAbaixoDe"]) : Sat_QuedaAbaixoDe;
                Sat_DuracaoMinima = rs["Sat_DuracaoMinima"] != DBNull.Value ? Convert.ToInt32(rs["Sat_DuracaoMinima"]) : Sat_DuracaoMinima;
                Sat_Recalcular = rs["Sat_Recalcular"] != DBNull.Value ? Convert.ToInt32(rs["Sat_Recalcular"]) : Sat_Recalcular;
                Sat_DesprezarAbaixo = rs["Sat_DesprezarAbaixo"] != DBNull.Value ? Convert.ToInt32(rs["Sat_DesprezarAbaixo"]) : Sat_DesprezarAbaixo;
                Sat_Segundos = rs["Sat_Tempo_Medio"] != DBNull.Value ? Convert.ToInt32(rs["Sat_Tempo_Medio"]) : Sat_Segundos;
                Sat_Desvio = rs["Sat_Tolerancia_Desvio"] != DBNull.Value ? Convert.ToInt32(rs["Sat_Tolerancia_Desvio"]) : Sat_Desvio;
            }

            Sat_Desvio = 100; // Definição fixa no código original

            double despreza = SaO2_100 / 100.0 * Sat_DesprezarAbaixo;
            double ref_queda = Sat_Basal_inicial / 100.0 * (100 - Sat_QuedaAbaixoDe);

            // Buscar eventos já processados
            foreach (DataRow row in GlobVar.eventos.Rows)
            {
                if (row.Field<int>("CodEvento") == 100 && row.Field<int>("CodCanal1") == CodAnalisar)
                {
                    PagDesprezadas.Add(Convert.ToInt32(row["NumPag"]));
                }
            }

            foreach(DataRow tbl_Pagina in GlobVar.tbl_Paginas.Rows)// (int pag = pag_ini; pag <= pag_fim; pag++)
            {
                int pag = Convert.ToInt32(tbl_Pagina["NumPag"]);
                double valor = Convert.ToInt32(tbl_Pagina["SatBasal"]);

                if (valor > despreza && valor <= 100)
                {
                    if (!PagDesprezadas.Contains(pag))
                    {
                        int Sat_Estagio = Convert.ToInt32(tbl_Pagina["estagio"]);
                        //if (!tbl.EOF) tbl.MoveNext();

                        if (Sat_Estagio >= 0 && Sat_Estagio <= 9)
                        {
                            if (Sat_Media.Length < Sat_Segundos * 4)
                            {
                                Sat_Media += valor.ToString("000") + "#";
                            }
                            else
                            {
                                Sat_Media = Sat_Media.Substring(4) + valor.ToString("000") + "#";
                                string Sat_Media_Calc = Sat_Media;
                                int Sat_Valor = 0;

                                while (Sat_Media_Calc.Length > 1)
                                {
                                    Sat_Valor += int.Parse(Sat_Media_Calc.Substring(0, 3));
                                    Sat_Media_Calc = Sat_Media_Calc.Substring(4);
                                }

                                double mediaSat = Sat_Valor / (double)Sat_Segundos;
                                if (valor < mediaSat + (mediaSat * (Sat_Desvio / 100.0)) &&
                                    valor > mediaSat - (mediaSat * (Sat_Desvio / 100.0)))
                                {
                                    Sat_Media = Sat_Media.Substring(4 * (Sat_Segundos - 1)) + valor.ToString("000") + "#";

                                    acum += (int)valor;
                                    qtd++;
                                    maior_sat = (int)Math.Max(maior_sat, valor);
                                    menor_sat = (int)Math.Min(menor_sat, valor);

                                    if (valor < ref_90)
                                    {
                                        Abaixo90 += 8;
                                        if (valor < ref_80)
                                        {
                                            Abaixo80 += 8;
                                        }
                                    }

                                    //g_porc = 10 + (pag / (double)pag_fim * 90);
                                    vIntervalo++;

                                    if (ini_evento == -1)
                                    {
                                        if (valor >= vPico)
                                        {
                                            vPico = vVale = (int)valor;
                                            vIntervalo = 0;
                                        }
                                        else
                                        {
                                            if (valor <= vVale)
                                            {
                                                vVale = (int)valor;
                                            }
                                            else
                                            {
                                                vPico = vVale = (int)valor;
                                                vIntervalo = 0;
                                            }

                                            if (vVale < vPico - Sat_QuedaAbaixoDe)
                                            {
                                                if (vIntervalo <= Sat_Recalcular)
                                                {
                                                    ini_evento = pag - vIntervalo;
                                                    fim_evento = pag;
                                                }
                                                else
                                                {
                                                    vPico = (int)F_Get1ValorDoCanalSAO2(pag - Sat_Recalcular);
                                                    vIntervalo = Sat_Recalcular;

                                                    if (vVale < vPico - Sat_QuedaAbaixoDe)
                                                    {
                                                        int a = 0;
                                                        for (int j = Sat_Recalcular - 1; j >= 1; j--)
                                                        {
                                                            if (vPico > F_Get1ValorDoCanalSAO2(pag - j)) break;
                                                            a = j;
                                                        }
                                                        vIntervalo = a + 1;
                                                        ini_evento = pag - vIntervalo;
                                                        fim_evento = pag;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (valor <= vVale)
                                        {
                                            fim_evento = pag;
                                            vVale = (int)valor;
                                        }
                                        else
                                        {
                                            if (vIntervalo <= Sat_Recalcular)
                                            {
                                                AdicionarEventoAoDataTable(ini_evento, fim_evento, CodEvento, CodAnalisar);

                                            }
                                            vPico = vVale = (int)valor;
                                            vIntervalo = 0;
                                            ini_evento = -1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ini_evento != -1)
                    {
                        if (vVale < vPico - Sat_QuedaAbaixoDe)
                        {
                            if (vIntervalo <= Sat_Recalcular)
                            {
                                ini_evento = pag - vIntervalo;
                                fim_evento = pag - 1;
                            }
                            else
                            {
                                vPico = (int)F_Get1ValorDoCanalSAO2(pag - Sat_Recalcular);
                                vIntervalo = Sat_Recalcular;

                                if (vVale < vPico - Sat_QuedaAbaixoDe)
                                {
                                    int a = 0;
                                    for (int j = Sat_Recalcular - 1; j >= 1; j--)
                                    {
                                        if (vPico > F_Get1ValorDoCanalSAO2(pag - j)) break;
                                        a = j;
                                    }
                                    vIntervalo = a + 1;
                                    ini_evento = pag - vIntervalo;
                                    fim_evento = pag - 1;
                                }
                            }
                            AdicionarEventoAoDataTable(ini_evento, fim_evento, CodEvento, CodAnalisar);
                            vPico = vVale = (int)valor;
                            vIntervalo = 0;
                            ini_evento = -1;
                        }
                    }
                    else
                    {
                        vPico = vVale = (int)valor;
                        vIntervalo = 0;
                    }
                }
            }
            // Após o loop, se necessário, calcular a média de saturação:
            //media_sat = qtd > 0 ? acum / qtd : 0;
        }

        private float F_Get1ValorDoCanalSAO2(int pag)
        {
            // Simula a obtenção de valores para o canal SAO2 (ajustar conforme necessário)
            var sa02 = GlobVar.tbl_Paginas.AsEnumerable().Where(row => row.Field<int>("NumPag") == pag).FirstOrDefault();
            if (sa02 != null)
            {
                return Convert.ToInt32(sa02["SatBasal"]);
            }
            else
            {
                return 0;
            }
        }
        public static void AdicionarEventoAoDataTable(int inicio, int termino, int codEvento, int codcanal1)
        {
            try
            {
                if (GlobVar.lastEvent != null)
                {
                    DataTable eventos = GlobVar.eventosUpdate;

                    //int loc = EncontrarValorMaisProximo(desenhoLoc, startY);

                    // Adicionar colunas ao DataTable se não existirem
                    if (eventos.Columns.Count == 0)
                    {
                        eventos.Columns.Add("Seq", typeof(int));
                        eventos.Columns.Add("NumPag", typeof(string));
                        eventos.Columns.Add("CodEvento", typeof(int));
                        eventos.Columns.Add("CodCanal1", typeof(int));
                        eventos.Columns.Add("Inicio", typeof(int));
                        eventos.Columns.Add("Duracao", typeof(int));
                        eventos.Columns.Add("MenorSat", typeof(int));
                        eventos.Columns.Add("Posicao", typeof(string));

                    }

                    // Calcular NumPag para início e término
                    int numPagInicio = inicio;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    int numPagTermino = termino;//GlobVar.txPorCanal[GlobVar.grafSelected[YAdjusted]];
                    string numPag = $"{numPagInicio} -- {numPagTermino}";
                    // Obter o próximo valor de Seq
                    int seq = plotComentatios.AtualizarProxSeqEvento();

                    plotEventos.minSaturacao(numPagInicio, numPagTermino);
                    int minSat = GlobVar.minSat.Min();
                    string posi = plotEventos.Posicao(numPagInicio, numPagTermino);
                    inicio = inicio * 512;
                    termino = termino * 512;
                    // Adicionar dados ao DataTable
                    GlobVar.eventosUpdate.Rows.Add(seq, numPag, codEvento, codcanal1, inicio, termino, minSat, posi);
                    AlteraBD.GravaEvento(seq, numPagInicio, codEvento, codcanal1, -1, inicio, termino, GlobVar.namos, numPagTermino, minSat, posi);
                    // Exportar DataTable para Excel
                    string excelFilePath = @"C:\Teste\Teste";
                    //CreateCSVFile(GlobVar.eventosUpdate, excelFilePath);
                    eventos.Dispose();
                }
            }
            catch { }
        }
        public static int ExcluiEvento(int codEvento)
        {
            try
            {
                int i = -1;
                using var connectionDatBd = new OdbcConnection(connectionStringDatBd);
                connectionDatBd.Open();

                string queryDelete = $"DELETE FROM tbl_Eventos WHERE CodEvento = {codEvento};";

                using var DeleteCommand = new OdbcCommand(queryDelete, connectionDatBd);

                DeleteCommand.ExecuteNonQuery();

                connectionDatBd.Close();
                return i;
            }
            catch { int i = 0; return i; }
        }
        private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";

        private void DetectarEvento(int pag, float valor)
        {
            // Implementação para detectar eventos com base na queda de saturação
            // Lógica a ser ajustada conforme necessidade do projeto
        }
    }
}
