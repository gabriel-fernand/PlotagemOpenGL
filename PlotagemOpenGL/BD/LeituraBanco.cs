using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Windows;
using System.Windows.Markup;
using System.Collections.Generic;
using System.Windows.Forms;
using Accord.Math;

public class LeituraBanco
{
    static DataTable sele = new DataTable();
    //private static string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
    //private static string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";
    public static void BancoRead()
    {
        try
        {
            string connectionStringDatBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
            //System.Windows.Forms.MessageBox.Show("Esse e o CodMont " + connectionStringDatBd);

            using var connectionDatBd = new OdbcConnection(connectionStringDatBd);

            connectionDatBd.Open();
            //System.Windows.Forms.MessageBox.Show("Conexão bem-sucedida!");

            string query = "SELECT * FROM tbl_Eventos";
            string quaryTbl_MontGrav = "SELECT * FROM tbl_MontGrav";
            string quaryTipoExame = "SELECT TOP 1 CodTipoExame FROM tbl_DadosExame";
            string queryTbl_Comentarios = "SELECT * FROM tbl_Comentarios";
            string queryTbl_DadosExame = "SELECT * FROM tbl_DadosExame";
            string queryTbl_Paginas = "SELECT * FROM tbl_Paginas";
            string queryTbl_ResumoExame = "SELECT * FROM tbl_ResumoExame";
            string queryTbl_SelImpressao = "SELECT * FROM tbl_selImpressao";
            string queryTbl_SeqEvento = "SELECT * FROM tbl_SeqEvento";
            string queryTbl_ArqVideo = "SELECT * FROM tbl_ArqVideo";
            string queryTbl_CanaisAdquiridos = "SELECT * FROM tbl_CanaisAdquiridos";

            string queryCons_Eventos = "SELECT * FROM Cons_Eventos";

            using var command = new OdbcCommand(query, connectionDatBd);
            using var commandTbl_MontGrav = new OdbcCommand(quaryTbl_MontGrav, connectionDatBd);
            using var commandTipoExame = new OdbcCommand(quaryTipoExame, connectionDatBd);
            using var commandTbl_Comentarios = new OdbcCommand(queryTbl_Comentarios, connectionDatBd);
            using var commandTbl_DadosExame = new OdbcCommand(queryTbl_DadosExame, connectionDatBd);
            using var commandTbl_Paginas = new OdbcCommand(queryTbl_Paginas, connectionDatBd);
            using var commandTbl_ResumoExame = new OdbcCommand(queryTbl_ResumoExame, connectionDatBd);
            using var commanfTbl_SelImpressao = new OdbcCommand(queryTbl_SelImpressao, connectionDatBd);
            using var commandTbl_SeqEvento = new OdbcCommand(queryTbl_SeqEvento, connectionDatBd);
            using var commandTbl_ArqVideo = new OdbcCommand(queryTbl_ArqVideo, connectionDatBd);
            using var commandTbl_CanaisAdquiridos = new OdbcCommand(queryTbl_CanaisAdquiridos, connectionDatBd);


            using var commandCons_Eventos = new OdbcCommand(queryCons_Eventos, connectionDatBd);

            using var adapterTbl_ArqVideo = new OdbcDataAdapter(commandTbl_ArqVideo);

            using var adapter = new OdbcDataAdapter(command);
            using var adapterTbl_MontGrav = new OdbcDataAdapter(commandTbl_MontGrav);
            using var adapterTipoExame = new OdbcDataAdapter(commandTipoExame);
            using var adapterTbl_Comentarios = new OdbcDataAdapter(commandTbl_Comentarios);
            using var adapterTbl_DadosExame = new OdbcDataAdapter(commandTbl_DadosExame);
            using var adapterTbl_Paginas = new OdbcDataAdapter(commandTbl_Paginas);
            using var adapterTbl_ResumoExame = new OdbcDataAdapter(commandTbl_ResumoExame);
            using var adapterTbl_SelImpressao = new OdbcDataAdapter(commanfTbl_SelImpressao);
            using var adapterTbl_SeqEvento = new OdbcDataAdapter(commandTbl_SeqEvento);
            using var adapterTbl_CanaisAdquiridos = new OdbcDataAdapter(commandTbl_CanaisAdquiridos);

            using var adapterCons_Eventos = new OdbcDataAdapter(commandCons_Eventos);
            // Preenche o DataTable com os dados retornados pela consulta
            adapterTbl_ArqVideo.Fill(GlobVar.tbl_ArqVideo);
            adapterTbl_SeqEvento.Fill(GlobVar.tbl_SeqEvento);
            adapterTbl_SelImpressao.Fill(GlobVar.tbl_SelImpressao);
            adapterTbl_ResumoExame.Fill(GlobVar.tbl_ResumoExame);
            adapterTbl_Paginas.Fill(GlobVar.tbl_Paginas);
            adapterTbl_DadosExame.Fill(GlobVar.tbl_DadosExame);
            adapterTbl_Comentarios.Fill(GlobVar.tbl_Comentarios);
            adapterTipoExame.Fill(sele);
            adapter.Fill(GlobVar.eventos);
            adapterTbl_MontGrav.Fill(GlobVar.tbl_MontGrav);
            adapterTbl_CanaisAdquiridos.Fill(GlobVar.tbl_CanaisAdquiridos);

            adapterCons_Eventos.Fill(GlobVar.Cons_Eventos);
            connectionDatBd.Close();


        }
        catch (OdbcException ex)
        {
            System.Windows.Forms.MessageBox.Show($"Erro ODBC:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show($"Erro geral:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static void BancoConifg()
    {
        try
        {
            string connectionStringConfigBd = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.configBD};Uid=Admin;Pwd=;";
            using var connectionConfigBd = new OdbcConnection(connectionStringConfigBd);

            connectionConfigBd.Open();
            //System.Windows.Forms.MessageBox.Show("Conexão bem-sucedida!");


            string queryConfig = "SELECT * FROM tbl_CadCanal";
            string queryTbl_MontCanal = "SELECT * FROM tbl_MontCanal";
            string queryTbl_Montagem = "SELECT * FROM tbl_Montagem";
            string quaryTbl_TipoExame = "SELECT * FROM tbl_TipoExame";
            string queryCadTipoCanal = "SELECT * FROM tbl_CadTipoCanal";
            string queryCadEvento = "SELECT * FROM tbl_CadEvento";
            string queryEventTipCanal = "SELECT * FROM tbl_EventoTipoCanal";
            string queryTipoCanal = "SELECT * FROM tbl_TipoCanal";

            string queryTbl_HipnoGrupos = "SELECT * FROM tbl_HipnoGrupos";
            string queryTbl_HipnoSubGrupos = "SELECT * FROM  tbl_HipnoSubGrupo";
            string queryTbl_JanelaResumoItens = "SELECT * FROM tbl_JanelaResumoItens";
            string queryTbl_JanelaResumo = "SELECT * FROM tbl_JanelaResumo";
            string queryTbl_Estagios = "SELECT * FROM tbl_Estagios";
            string querytbl_ParametrosParaAnalise = "SELECT * FROM tbl_ParametrosParaAnalise";
            string queryTbl_RelatResumo = "SELECT * FROM tbl_RelatResumo";
            string queryTbl_RelatResumoItem = "SELECT * FROM tbl_RelatResumoItem";
            string queryTbl_DadosClinica = "SELECT * FROM tbl_DadosClinica";
            string queryTbl_HipnoLaudos = "SELECT * FROM tbl_HipnoLaudos";
            string queryTbl_EstagioInf = "SELECT * FROM tbl_Estagios_Infantil";

            using var commandTbl_CadTipoCanal = new OdbcCommand(queryCadTipoCanal, connectionConfigBd);
            using var commandConfig = new OdbcCommand(queryConfig, connectionConfigBd);
            using var commandTbl_MontCanal = new OdbcCommand(queryTbl_MontCanal, connectionConfigBd);
            using var commandTbl_Montagem = new OdbcCommand(queryTbl_Montagem, connectionConfigBd);
            using var commandTbl_TipoExam = new OdbcCommand(quaryTbl_TipoExame, connectionConfigBd);
            using var commandCadEvento = new OdbcCommand(queryCadEvento, connectionConfigBd);
            using var commandEventTipCanal = new OdbcCommand(queryEventTipCanal, connectionConfigBd);
            using var commandTipoCanal = new OdbcCommand(queryTipoCanal, connectionConfigBd);
            using var commandtbl_ParametrosParaAnalise = new OdbcCommand(querytbl_ParametrosParaAnalise, connectionConfigBd);
            using var commandtbl_RelatResumo = new OdbcCommand(queryTbl_RelatResumo, connectionConfigBd);
            using var commandtbl_RelatResumoItem = new OdbcCommand(queryTbl_RelatResumoItem, connectionConfigBd);
            using var commandHipnoGruos = new OdbcCommand(queryTbl_HipnoGrupos, connectionConfigBd);
            using var commandSubHipnoGrupos = new OdbcCommand(queryTbl_HipnoSubGrupos, connectionConfigBd);
            using var commandItensJanela = new OdbcCommand(queryTbl_JanelaResumoItens, connectionConfigBd);
            using var commandJanelaResumo = new OdbcCommand(queryTbl_JanelaResumo, connectionConfigBd);
            using var commandTbl_Estagios = new OdbcCommand(queryTbl_Estagios, connectionConfigBd);
            using var commandTbl_HipnoLaudos = new OdbcCommand(queryTbl_HipnoLaudos, connectionConfigBd);
            using var commandTbl_DadosClinica = new OdbcCommand(queryTbl_DadosClinica, connectionConfigBd);
            using var commandTbl_EstagiosInf = new OdbcCommand(queryTbl_EstagioInf, connectionConfigBd);

            using var adapterTbl_CadTipoCanal = new OdbcDataAdapter(commandTbl_CadTipoCanal);
            using var adapterConfig = new OdbcDataAdapter(commandConfig);
            using var adapterTbl_MontCanal = new OdbcDataAdapter(commandTbl_MontCanal);
            using var adapterTbl_Montagem = new OdbcDataAdapter(commandTbl_Montagem);
            using var adapterTbl_TipeExam = new OdbcDataAdapter(commandTbl_TipoExam);
            using var adapterCadExame = new OdbcDataAdapter(commandCadEvento);
            using var adapterEventTipCanal = new OdbcDataAdapter(commandEventTipCanal);
            using var adapterTipoCanal = new OdbcDataAdapter(commandTipoCanal);
            using var adaptertbl_RelatResumoItem = new OdbcDataAdapter(commandtbl_RelatResumoItem);
            using var adaptertbl_RelatResumo = new OdbcDataAdapter(commandtbl_RelatResumo);
            using var adapterHipnoGrupos = new OdbcDataAdapter(commandHipnoGruos);
            using var adapterSubHipno = new OdbcDataAdapter(commandSubHipnoGrupos);
            using var adapterItensJanela = new OdbcDataAdapter(commandItensJanela);
            using var adapterJanelaResumo = new OdbcDataAdapter(commandJanelaResumo);
            using var adapterTbl_Estagios = new OdbcDataAdapter(commandTbl_Estagios);
            using var adaptertbl_ParametrosParaAnalise = new OdbcDataAdapter(commandtbl_ParametrosParaAnalise);
            using var adaptertbl_HipnoLaudo = new OdbcDataAdapter(commandTbl_HipnoLaudos);
            using var adaptertbl_DadosClinica = new OdbcDataAdapter(commandTbl_DadosClinica);
            using var adapterTbl_EstagiosInf = new OdbcDataAdapter(commandTbl_EstagiosInf);

            adapterTbl_EstagiosInf.Fill(GlobVar.tbl_EstagiosInfatil);
            adaptertbl_DadosClinica.Fill(GlobVar.tbl_DadosClinica);
            adaptertbl_HipnoLaudo.Fill(GlobVar.tbl_HipnoLaudo);
            adapterConfig.Fill(GlobVar.tbl_CadCanal);
            adapterTbl_MontCanal.Fill(GlobVar.tbl_MontCanal);
            adapterTbl_Montagem.Fill(GlobVar.tbl_Montagem);
            adapterTbl_Montagem.Fill(GlobVar.tbl_MontagemOriginal);
            adapterTbl_TipeExam.Fill(GlobVar.tbl_TipoExame);
            adapterTbl_CadTipoCanal.Fill(GlobVar.tbl_CadTipoCanal);
            adapterCadExame.Fill(GlobVar.tbl_CadEvento);
            adapterEventTipCanal.Fill(GlobVar.tbl_EventoTipoCanal);
            adapterTipoCanal.Fill(GlobVar.tbl_TipoCanal);
            adaptertbl_RelatResumoItem.Fill(GlobVar.tbl_RelatResumoItem);
            adaptertbl_RelatResumo.Fill(GlobVar.tbl_RelatResumo);
            adapterHipnoGrupos.Fill(GlobVar.tbl_HipnoGrupos);
            adapterSubHipno.Fill(GlobVar.tbl_HipnoSubGrupos);
            adapterItensJanela.Fill(GlobVar.tbl_JanelaResumoItens);
            adapterJanelaResumo.Fill(GlobVar.tbl_JanelaResumo);
            adapterTbl_Estagios.Fill(GlobVar.tbl_Estagios);
            adaptertbl_ParametrosParaAnalise.Fill(GlobVar.tbl_ParametrosParaAnalisar);
            connectionConfigBd.Close();

        }
        catch (OdbcException ex)
        {
            System.Windows.Forms.MessageBox.Show($"Erro ODBC:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            System.Windows.Forms.MessageBox.Show($"Erro geral:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    public static void AjustaMontagem()
    {
        try {

            if (sele.Rows.Count >= 0)
            {
                string p;
                int codTipoExame = 0;
                if (sele.Rows.Count > 0)
                {
                    // Pegando o valor da primeira linha e coluna "CodTipoExame"
                    codTipoExame = Convert.ToInt32(sele.Rows[0]["CodTipoExame"]);
                    if (codTipoExame == 1) p = "P";
                    else p = "E";

                }
                else
                {
                    p = "P";
                }

                //System.Windows.Forms.MessageBox.Show("Esse e o Tipo Exame " + p);

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
                                .Where(row => row.Field<string>("DescrMontagem") == GlobVar.tbl_MontGrav.Rows[0]["NomeMontagem"].ToString());

                        if (matchingRows.Any())
                        {
                            // Existem linhas correspondentes
                            var auxCodMont = matchingRows.CopyToDataTable();
                            int CodMont = Convert.ToInt32(auxCodMont.Rows[0]["CodMontagem"]);
                            GlobVar.tbl_MontagemSelecionada = GlobVar.tbl_MontCanal.AsEnumerable()
                                    .Where(row => row.Field<int>("CodMontagem") == CodMont)
                                    .CopyToDataTable();
                            GlobVar.codMont = CodMont;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show(
                                "Não foi localizada a montagem em que o exame foi realizado.\nO sistema ativou a montagem padrão",
                                "Atenção!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                            var auxCodMont = matchingRows.CopyToDataTable();
                            int CodMont = Convert.ToInt32(auxCodMont.Rows[0]["CodMontagem"]);

                            GlobVar.codMont = CodMont;
                        }
                        break;
                    }
                }

                foreach (DataRow dw in GlobVar.tbl_MontagemSelecionada.Rows)
                {
                    if (dw["CodCanal2"] == DBNull.Value)
                    {
                        dw["CodCanal2"] = -1;
                    }
                }
                GlobVar.tbl_MontagemSelecionada.AcceptChanges();
            }
        }
        catch (IOException e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message);
        }
        catch (Exception e)
        {
            System.Windows.Forms.MessageBox.Show(e.Message);
        }
    }
    public static void AlteraMontagem(int CodMont)
    {
        GlobVar.tbl_MontagemSelecionada.Clear();
        GlobVar.tbl_MontagemSelecionada = GlobVar.tbl_MontCanal.AsEnumerable().Where(row => row.Field<int>("CodMontagem") == CodMont).CopyToDataTable();
        var montagemFiltrada = GlobVar.tbl_Montagem.AsEnumerable()
            .Where(row => row.Field<int>("CodMontagem") == CodMont)
            .FirstOrDefault();
        bool tem19 = GlobVar.codCanal.Contains(19);
        bool tem43 = GlobVar.codCanal.Contains(43);

        // Certifique-se de que o vetor 'GlobVar.codCanal' e a tabela 'GlobVar.tbl_MontCanal' sejam não nulos
        if (GlobVar.codCanal != null && GlobVar.codCanal.Length > 0 && GlobVar.tbl_MontCanal != null)
        {
            var linhasFiltradas = GlobVar.tbl_MontagemSelecionada;
            if (CodMont != 181){
                // Filtra as linhas do DataTable 'GlobVar.tbl_MontCanal' onde a coluna 'CodCanal1' possui valores presentes no vetor 'GlobVar.codCanal'
                linhasFiltradas = GlobVar.tbl_MontagemSelecionada.AsEnumerable()
                    .Where(row => GlobVar.codCanal.Contains(row.Field<int>("CodCanal1"))).CopyToDataTable();
            }
            else
            {
                List<DataRow> linhasParaRemover = new List<DataRow>();

                foreach (DataRow rw in linhasFiltradas.Rows)
                {
                    int cod = Convert.ToInt32(rw["CodCanal1"]);
                    int cod2 = Convert.ToInt32(rw["CodCanal2"]);
                    if (cod != 100 && cod != 101 && cod != 102)
                    {
                        if (!GlobVar.codCanal.Contains(cod) || (!GlobVar.codCanal.Contains(cod2) && cod2 != -1))
                        {
                            linhasParaRemover.Add(rw); // Adiciona a linha para remoção
                        }
                    }
                    else
                    {
                        if (!tem19 || !tem43)
                        {
                            linhasParaRemover.Add(rw); // Adiciona a linha para remoção
                        }
                    }
                }

                // Remove as linhas marcadas fora do loop
                foreach (DataRow rw in linhasParaRemover)
                {
                    linhasFiltradas.Rows.Remove(rw);
                }
            }
            // Atualiza o DataTable 'GlobVar.tbl_MontagemSelecionada' com as linhas filtradas
            GlobVar.tbl_MontagemSelecionada = linhasFiltradas;
            
            foreach(DataRow row in GlobVar.tbl_MontGrav.Rows)
            {
                row["NomeMontagem"] = montagemFiltrada[1];

            }
        }
        else
        {
            // Caso o vetor 'GlobVar.codCanal' esteja vazio ou o DataTable seja nulo, inicializa um DataTable vazio
            GlobVar.tbl_MontagemSelecionada = new DataTable();
        }
    }
    public static void AlteraTable()
    {
        if(GlobVar.eventosUpdate != null) { GlobVar.eventosUpdate.Clear();}
        // Supondo que GlobVar.eventos seja o DataTable original
        DataTable eventos = GlobVar.eventos;
        //DataTable eventosUpdate = new DataTable();
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

            int satu = 0;

            if (firstRow["MenorSat"] == DBNull.Value)
            {

            }
            else
            {
                satu = Convert.ToInt32(firstRow.Field<float>("MenorSat"));
            }

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
    public static void ArrumaTbl_Paginas()
    {
        int codSat = 66;
        int[] sat;

        int numColunas = GlobVar.matrizCanal.GetLength(1);
        sat = new int[numColunas];

        // Verifica se o código de referência existe
        int indexCodReferencia = GlobVar.codCanal.IndexOf(codSat);
        if (indexCodReferencia == -1)
        {
            return;
        }

        // Pega os índices de início e fim uma vez
        int startCol = GlobVar.ponteiroI[indexCodReferencia];
        int endCol = GlobVar.ponteiroF[indexCodReferencia];

        // Copia os valores de matrizCompleta para o array referencia
        int pontRef = 0;
        for (int linhaComp = 0; linhaComp < GlobVar.matrizCompleta.GetLength(0) && pontRef < numColunas; linhaComp++)
        {
            for (int colunaComp = startCol; colunaComp < endCol && pontRef < numColunas; colunaComp++)
            {
                sat[pontRef] = (int)GlobVar.matrizCompleta[linhaComp, colunaComp];
                pontRef++;
            }
        }
        int i = 0;
        foreach(DataRow rw in GlobVar.tbl_Paginas.Rows)
        {
            rw["SatBasal"] = sat[i];
            i += 8;
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
