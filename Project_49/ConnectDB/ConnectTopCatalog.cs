using Dapper;
using Project_49.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_49.Control
{
    public static class ConnectTopCatalog
    {
        public static string connectionString = @"Data Source=DESKTOP-TBFG5D3\SQLEXPRESS;Initial Catalog=TopCatalogs;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<TopCatalog> GetTopCatalog()
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<TopCatalog>($"SELECT * FROM TopCatalogs").ToList();
            }
        }
    }
}
