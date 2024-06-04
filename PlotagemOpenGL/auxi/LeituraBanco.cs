using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Net.Mail;
using System.Windows;

public class LeituraBanco
{
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

            string queryConfig = "SELECT * FROM tbl_CadEvento";
            string query = "SELECT * FROM tbl_Eventos";

            using var commandConfig = new OdbcCommand(queryConfig, connectionConfigBd);
            using var command = new OdbcCommand(query, connectionDatBd);

            using var adapterConfig = new OdbcDataAdapter(commandConfig);
            using var adapter = new OdbcDataAdapter(command);

            // Preenche o DataTable com os dados retornados pela consulta
            adapterConfig.Fill(GlobVar.codEventos);
            adapter.Fill(GlobVar.eventos);

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
            inicio += ((firstRow.Field<int>("NumPag")-1) * 512);

            int duracao = lastRow.Field<int>("Duracao");
            duracao += ((lastRow.Field<int>("NumPag")-1) * 512);

            DataRow newRow = GlobVar.eventosUpdate.NewRow();
            newRow["Seq"] = seq;
            newRow["NumPag"] = numPag;
            newRow["CodEvento"] = codEvento;
            newRow["CodCanal1"] = codCanal1;
            newRow["Inicio"] = inicio;
            newRow["Duracao"] = duracao;

            GlobVar.eventosUpdate.Rows.Add(newRow);
        }
    }
}
