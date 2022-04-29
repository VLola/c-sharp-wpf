using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace Project_49.Control
{
    public static class Connect
    {
        public static string connectionStringUser = @"Data Source=DESKTOP-TBFG5D3\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static string connectionStringAdmin = @"Data Source=DESKTOP-TBFG5D3\SQLEXPRESS;Initial Catalog=Admins;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void RegistrationUser(string email, string pass)
        {
            if (CheckEmailUser(email)) MessageBox.Show("User exists!");
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionStringUser))
                {
                    User reg = new User();
                    reg.Email = email;
                    reg.Password = Crypt.Generate(pass);
                    connection.Insert(reg);
                    MessageBox.Show("Registration successful!");
                }
            }
        }
        public static bool LoginUser(string email, string pass)
        {
            bool check = false;
            foreach (var it in GetUsers()) if (it.Email == email && Crypt.Veryfy(pass, it.Password)) check = true;
            return check;
        }
        public static bool CheckEmailUser(string email)
        {
            bool check = false;
            foreach (var it in GetUsers()) if (it.Email == email) check = true;
            return check;
        }
        public static List<User> GetUsers()
        {
            using (IDbConnection connection = new SqlConnection(connectionStringUser))
            {
                return connection.Query<User>($"SELECT * FROM Users").ToList();
            }
        }
        public static void RegistrationAdmin(string email, string pass)
        {
            if (CheckEmailAdmin(email)) MessageBox.Show("Admin exists!");
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionStringAdmin))
                {
                    Admin reg = new Admin();
                    reg.Email = email;
                    reg.Password = Crypt.Generate(pass);
                    connection.Insert(reg);
                    MessageBox.Show("Registration successful!");
                }
            }
        }
        public static bool LoginAdmin(string email, string pass)
        {
            bool check = false;
            foreach (var it in GetAdmin()) if (it.Email == email && Crypt.Veryfy(pass, it.Password)) check = true;
            return check;
        }
        public static bool CheckEmailAdmin(string email)
        {
            bool check = false;
            foreach (var it in GetAdmin()) if (it.Email == email) check = true;
            return check;
        }
        public static List<Admin> GetAdmin()
        {
            using (IDbConnection connection = new SqlConnection(connectionStringAdmin))
            {
                return connection.Query<Admin>($"SELECT * FROM Admins").ToList();
            }
        }
    }
}
