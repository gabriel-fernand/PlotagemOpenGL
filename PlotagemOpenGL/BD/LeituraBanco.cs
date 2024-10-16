using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Markup;
using ClassesBDNano;

public class LeituraBanco
{
    static DataTable sele = new DataTable();
    private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
    private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";
    public static void BancoRead()
    {
        try
        {
            using var connectionConfigBd = new OdbcConnection(connectionStringConfigBd);
            using var connectionDatBd = new OdbcConnection(connectionStringDatBd);

            connectionConfigBd.Open();
            connectionDatBd.Open();
            //MessageBox.Show("Conexão bem-sucedida!");

            string queryConfig = "SELECT * FROM tbl_CadCanal";
            string query = "SELECT * FROM tbl_Eventos";
            string queryTbl_MontCanal = "SELECT * FROM tbl_MontCanal";
            string queryTbl_Montagem = "SELECT * FROM tbl_Montagem";
            string quaryTbl_MontGrav = "SELECT * FROM tbl_MontGrav";
            string quaryTbl_TipoExame = "SELECT * FROM tbl_TipoExame";
            string quaryTipoExame = "SELECT TOP 1 CodTipoExame FROM tbl_DadosExame";
            string queryCadTipoCanal = "SELECT * FROM tbl_CadTipoCanal";
            string queryCadEvento = "SELECT * FROM tbl_CadEvento";
            string queryEventTipCanal = "SELECT * FROM tbl_EventoTipoCanal";
            string queryTipoCanal = "SELECT * FROM tbl_TipoCanal";
            string queryTbl_Comentarios = "SELECT * FROM tbl_Comentarios";
            string queryTbl_DadosExame = "SELECT * FROM tbl_DadosExame";
            string queryTbl_Paginas = "SELECT * FROM tbl_Paginas";
            string queryTbl_ResumoExame = "SELECT * FROM tbl_ResumoExame";
            string queryTbl_SelImpressao = "SELECT * FROM tbl_selImpressao";
            string queryTbl_SeqEvento = "SELECT * FROM tbl_SeqEvento";



            using var commandTbl_CadTipoCanal = new OdbcCommand(queryCadTipoCanal, connectionConfigBd);
            using var commandConfig = new OdbcCommand(queryConfig, connectionConfigBd);
            using var commandTbl_MontCanal = new OdbcCommand(queryTbl_MontCanal, connectionConfigBd);
            using var commandTbl_Montagem = new OdbcCommand(queryTbl_Montagem, connectionConfigBd);
            using var commandTbl_TipoExam = new OdbcCommand(quaryTbl_TipoExame, connectionConfigBd);
            using var commandCadEvento = new OdbcCommand(queryCadEvento, connectionConfigBd);
            using var commandEventTipCanal = new OdbcCommand(queryEventTipCanal, connectionConfigBd);
            using var commandTipoCanal = new OdbcCommand(queryTipoCanal, connectionConfigBd);

            using var command = new OdbcCommand(query, connectionDatBd);
            using var commandTbl_MontGrav = new OdbcCommand(quaryTbl_MontGrav, connectionDatBd);
            using var commandTipoExame = new OdbcCommand(quaryTipoExame, connectionDatBd);
            using var commandTbl_Comentarios = new OdbcCommand(queryTbl_Comentarios, connectionDatBd);
            using var commandTbl_DadosExame = new OdbcCommand(queryTbl_DadosExame, connectionDatBd);
            using var commandTbl_Paginas = new OdbcCommand(queryTbl_Paginas, connectionDatBd);
            using var commandTbl_ResumoExame = new OdbcCommand(queryTbl_ResumoExame, connectionDatBd);
            using var commanfTbl_SelImpressao = new OdbcCommand(queryTbl_SelImpressao, connectionDatBd);
            using var commandTbl_SeqEvento = new OdbcCommand(queryTbl_SeqEvento, connectionDatBd);

            using var adapterTbl_CadTipoCanal = new OdbcDataAdapter(commandTbl_CadTipoCanal);
            using var adapterConfig = new OdbcDataAdapter(commandConfig);
            using var adapterTbl_MontCanal = new OdbcDataAdapter(commandTbl_MontCanal);
            using var adapterTbl_Montagem = new OdbcDataAdapter(commandTbl_Montagem);
            using var adapterTbl_TipeExam = new OdbcDataAdapter(commandTbl_TipoExam);
            using var adapterCadExame = new OdbcDataAdapter(commandCadEvento);
            using var adapterEventTipCanal = new OdbcDataAdapter(commandEventTipCanal);
            using var adapterTipoCanal = new OdbcDataAdapter(commandTipoCanal);

            using var adapter = new OdbcDataAdapter(command);
            using var adapterTbl_MontGrav = new OdbcDataAdapter(commandTbl_MontGrav);
            using var adapterTipoExame = new OdbcDataAdapter(commandTipoExame);
            using var adapterTbl_Comentarios = new OdbcDataAdapter(commandTbl_Comentarios);
            using var adapterTbl_DadosExame = new OdbcDataAdapter(commandTbl_DadosExame);
            using var adapterTbl_Paginas = new OdbcDataAdapter(commandTbl_Paginas);
            using var adapterTbl_ResumoExame = new OdbcDataAdapter(commandTbl_ResumoExame);
            using var adapterTbl_SelImpressao = new OdbcDataAdapter(commanfTbl_SelImpressao);
            using var adapterTbl_SeqEvento = new OdbcDataAdapter(commandTbl_SeqEvento);

            // Preenche o DataTable com os dados retornados pela consulta
            adapterTbl_SeqEvento.Fill(GlobVar.tbl_SeqEvento);
            adapterTbl_SelImpressao.Fill(GlobVar.tbl_SelImpressao);
            adapterTbl_ResumoExame.Fill(GlobVar.tbl_ResumoExame);
            adapterTbl_Paginas.Fill(GlobVar.tbl_Paginas);
            adapterTbl_DadosExame.Fill(GlobVar.tbl_DadosExame);
            adapterTbl_Comentarios.Fill(GlobVar.tbl_Comentarios);
            adapterTipoExame.Fill(sele);
            adapterConfig.Fill(GlobVar.tbl_CadCanal);
            adapter.Fill(GlobVar.eventos);
            adapterTbl_MontCanal.Fill(GlobVar.tbl_MontCanal);
            adapterTbl_Montagem.Fill(GlobVar.tbl_Montagem);
            adapterTbl_MontGrav.Fill(GlobVar.tbl_MontGrav);
            adapterTbl_TipeExam.Fill(GlobVar.tbl_TipoExame);
            adapterTbl_CadTipoCanal.Fill(GlobVar.tbl_CadTipoCanal);
            adapterCadExame.Fill(GlobVar.tbl_CadEvento);
            adapterEventTipCanal.Fill(GlobVar.tbl_EventoTipoCanal);
            adapterTipoCanal.Fill(GlobVar.tbl_TipoCanal);


            connectionConfigBd.Close();
            connectionDatBd.Close();
            
        }
        catch (OdbcException ex)
        {
            // Tratamento de exceções específicas do ODBC
            Console.WriteLine($"Erro ao acessar o banco de dados: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Tratamento de outras exceções
            Console.WriteLine($"Erro inesperado: {ex.Message}");
        }
    }
    public static void AjustaMontagem()
    {
        try { 
        if (sele.Rows.Count > 0)
        {
            string p;
            // Pegando o valor da primeira linha e coluna "CodTipoExame"
            int codTipoExame = Convert.ToInt32(sele.Rows[0]["CodTipoExame"]) ;
            if (codTipoExame == 1) p = "P";
            else p = "E";

            string tipoExame = null;
            foreach (DataRow dr in GlobVar.tbl_TipoExame.Rows)
            {
                if (Convert.ToInt32(dr["CodTipoExame"]) == codTipoExame)
                {
                    tipoExame = dr["TipoExame"].ToString();

                    var codMontagensFiltrados = GlobVar.tbl_Montagem.AsEnumerable()
                                                .Where(row => row.Field<string>("TipoMontagem") == p)
                                                .Select(row => row.Field<int>("CodMontagem"))
                                                .ToList();

                    // Criando um novo DataTable com as linhas filtradas de tbl_MontCanal
                    DataTable tbl_MontCanalFiltrado = GlobVar.tbl_MontCanal.Clone(); // Clona a estrutura do DataTable original

                    foreach (DataRow row in GlobVar.tbl_MontCanal.Rows)
                    {
                        if (codMontagensFiltrados.Contains(row.Field<int>("CodMontagem")))
                        {
                            tbl_MontCanalFiltrado.ImportRow(row);
                        }
                    }

                    // Ordenando as linhas filtradas
                    var orderedRows = tbl_MontCanalFiltrado.AsEnumerable()
                                                            .OrderBy(row => row.Field<int>("CodMontagem"))
                                                            .ThenBy(row => row.Field<int>("Ordem"));

                    // Limpando GlobVar.tbl_MontCanal e importando as linhas ordenadas
                    GlobVar.tbl_MontCanal.Clear();

                    foreach (var row in orderedRows)
                    {
                        GlobVar.tbl_MontCanal.ImportRow(row);
                    }

                    // Filtrando e atualizando GlobVar.tbl_Montagem
                    var montagemFiltrada = GlobVar.tbl_Montagem.AsEnumerable()
                                                    .Where(row => row.Field<string>("TipoMontagem") == p)
                                                    .CopyToDataTable();

                    GlobVar.tbl_Montagem.Clear();
                    foreach (DataRow row in montagemFiltrada.Rows)
                    {
                        GlobVar.tbl_Montagem.ImportRow(row);
                    }

                        var matchingRows = GlobVar.tbl_Montagem.AsEnumerable()
    .                                               Where(row => row.Field<string>("DescrMontagem") == GlobVar.tbl_MontGrav.Rows[0]["NomeMontagem"].ToString());

                        if (matchingRows.Any())
                        {
                            // Existem linhas correspondentes
                            var auxCodMont = matchingRows.CopyToDataTable();
                            int CodMont = Convert.ToInt16(auxCodMont.Rows[0]["CodMontagem"]);
                            GlobVar.tbl_MontagemSelecionada = GlobVar.tbl_MontCanal.AsEnumerable()
                                    .Where(row => row.Field<int>("CodMontagem") == CodMont)
                                    .CopyToDataTable();
                        }
                        else
                        {
                            // Não existem linhas correspondentes
                            GlobVar.tbl_MontagemSelecionada = GlobVar.tbl_MontGrav;
                        }
                        break;
                }
                }
            }
        }
        catch (IOException e)
        {
            MessageBox.Show(e.Message);
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }
    public static void AlteraMontagem(int CodMont)
    {
        GlobVar.tbl_MontagemSelecionada = GlobVar.tbl_MontCanal.AsEnumerable().Where(row => row.Field<int>("CodMontagem") == CodMont).CopyToDataTable();

        // Certifique-se de que o vetor 'GlobVar.codCanal' e a tabela 'GlobVar.tbl_MontCanal' sejam não nulos
        if (GlobVar.codCanal != null && GlobVar.codCanal.Length > 0 && GlobVar.tbl_MontCanal != null)
        {
            // Filtra as linhas do DataTable 'GlobVar.tbl_MontCanal' onde a coluna 'CodCanal1' possui valores presentes no vetor 'GlobVar.codCanal'
            var linhasFiltradas = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                .Where(row => GlobVar.codCanal.Contains(row.Field<int>("CodCanal1"))).CopyToDataTable();

            // Atualiza o DataTable 'GlobVar.tbl_MontagemSelecionada' com as linhas filtradas
            GlobVar.tbl_MontagemSelecionada = linhasFiltradas;
        }
        else
        {
            // Caso o vetor 'GlobVar.codCanal' esteja vazio ou o DataTable seja nulo, inicializa um DataTable vazio
            GlobVar.tbl_MontagemSelecionada = new DataTable();
        }
    }
    public static void AlteraTable()
    {
        // Supondo que GlobVar.eventos seja o DataTable original
        DataTable eventos = GlobVar.eventos;

        // Crie um novo DataTable para armazenar os resultados
        GlobVar.eventosUpdate.Columns.Add("Seq", typeof(int));
        GlobVar.eventosUpdate.Columns.Add("NumPag", typeof(string));
        GlobVar.eventosUpdate.Columns.Add("CodEvento", typeof(int));
        GlobVar.eventosUpdate.Columns.Add("CodCanal1", typeof(int));
        GlobVar.eventosUpdate.Columns.Add("Inicio", typeof(int));
        GlobVar.eventosUpdate.Columns.Add("Duracao", typeof(int));
        GlobVar.eventosUpdate.Columns.Add("MenorSat", typeof(int));
        GlobVar.eventosUpdate.Columns.Add("Posicao", typeof(string));

        // Ordenar por NumPag e Seq
        var filteredRows = eventos.AsEnumerable()
                                  .OrderBy(row => row.Field<int>("NumPag"))
                                  .ThenBy(row => row.Field<int>("Seq"));

        // Agrupar por Seq e processar cada grupo
        var groupedRows = filteredRows.GroupBy(row => row.Field<int>("Seq"));

        foreach (var group in groupedRows)
        {
            var firstRow = group.First();
            var lastRow = group.Last();

            int seq = firstRow.Field<int>("Seq");
            string numPag = $"{firstRow.Field<int>("NumPag")} -- {lastRow.Field<int>("NumPag")}";
            int codEvento = firstRow.Field<int>("CodEvento");
            int codCanal1 = firstRow.Field<int>("CodCanal1");

            int inicio = firstRow.Field<int>("Inicio");
            inicio += ((firstRow.Field<int>("NumPag")) * 512);

            int duracao = lastRow.Field<int>("Duracao");
            duracao += ((lastRow.Field<int>("NumPag")) * 512);

            int? satu = Convert.ToInt16(firstRow.Field<float>("MenorSat"));
            string posi = firstRow.Field<string>("Posicao");

            DataRow newRow = GlobVar.eventosUpdate.NewRow();
            newRow["Seq"] = seq;
            newRow["NumPag"] = numPag;
            newRow["CodEvento"] = codEvento;
            newRow["CodCanal1"] = codCanal1;
            newRow["Inicio"] = inicio;
            newRow["Duracao"] = duracao;
            newRow["MenorSat"] = satu;
            newRow["Posicao"] = posi;

            GlobVar.eventosUpdate.Rows.Add(newRow);
        }
    }

    public static void AjustaCadEvent()// Esta ajustando os valores das teclas rapida para -1 caso o valor seja null, pois estava atrapalhando quando era null
    {
        for(int i = 0; i < GlobVar.tbl_CadEvento.Rows.Count; i++)
        {
            if (GlobVar.tbl_CadEvento.Rows[i]["TeclaRapida"] == DBNull.Value)
            {
                GlobVar.tbl_CadEvento.Rows[i]["TeclaRapida"] = -1;
            }
        }
    }
}
