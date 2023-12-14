using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Edn2023B
{
    internal class konekcija
    {
        static public SqlConnection povezi()
        {
            // string CS = "Data source=PROFESOR_UCIONI\\SQLEXPRESS;Initial catalog=ednevnik2023b;Integrated security=True";
            string CS;
            CS = ConfigurationManager.ConnectionStrings["skola49b"].ConnectionString;
            Console.WriteLine(CS); 
            SqlConnection conn = new SqlConnection(CS);
            return conn;
        }
    }
}
