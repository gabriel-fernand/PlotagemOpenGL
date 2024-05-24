using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
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
}
