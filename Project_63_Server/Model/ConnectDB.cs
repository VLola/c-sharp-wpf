using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Project_63_Server.Model
{
    internal class ConnectDB
    {
        public static string connectionStringUser = @"Data Source=DESKTOP-TBFG5D3\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static bool RegistrationUser(string email, string pass)
        {
            if (CheckEmailUser(email)) return false;
            else if (!IsValidEmail(email)) return false;
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionStringUser))
                {
                    User reg = new User();
                    reg.Email = email;
                    reg.Password = Crypt.Generate(pass);
                    connection.Insert(reg);
                    return true;
                }
            }
        }
        public static bool LoginUser(string email, string pass)
        {
            foreach (var it in GetUsers()) if (it.Email == email && Crypt.Veryfy(pass, it.Password)) return true;
            return false;
        }
        public static bool CheckEmailUser(string email)
        {
            foreach (var it in GetUsers()) if (it.Email == email) return true;
            return false;
        }
        public static List<User> GetUsers()
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.Query<User>($"SELECT * FROM Users").ToList();
            }
        }
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                if (addr.Address == email)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
