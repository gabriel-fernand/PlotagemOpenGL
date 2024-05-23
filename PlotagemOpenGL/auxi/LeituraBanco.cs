using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Text;

namespace PlotagemOpenGL.auxi
{
    public class LeituraBanco
    {
        static string connectionString = $@"Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={GlobVar.bDataFile};Uid=Admin;Pwd=;";
        public static void BancoRead()
        {
            using (var conection = new OdbcConnection(connectionString))
            {
                conection.Open();
            }

        }
    }
}
