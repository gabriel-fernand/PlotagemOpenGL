using PlotagemOpenGL.auxi;
using System;
using System.Data;
using System.Data.Odbc;
using System.Windows;

public class LeituraBanco
{
    private static string connectionString = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";

    public static void BancoRead()
    {
        try
        {
            using var connection = new OdbcConnection(connectionString);
            connection.Open();
            //MessageBox.Show("Conexão bem-sucedida!");

            string query = "SELECT * FROM tbl_Eventos";
            using var command = new OdbcCommand(query, connection);
            using var adapter = new OdbcDataAdapter(command);

            // Preenche o DataTable com os dados retornados pela consulta
            adapter.Fill(GlobVar.eventos);
            connection.Close();
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
