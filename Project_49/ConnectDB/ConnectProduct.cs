using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using System.Windows;

namespace Project_49.ConnectDB
{
    public static class ConnectProduct
    {
        public static string connectionStringProduct = @"Data Source=DESKTOP-TBFG5D3\SQLEXPRESS;Initial Catalog=Products;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static void AddProduct(string name, int price, int discount, string image, bool availability, bool latest)
        {
            using (SqlConnection connection = new SqlConnection(connectionStringProduct))
            {
                Models.Product product = new Models.Product();
                product.Name = name;
                product.Price = price;
                product.Discount = discount;
                product.Image = image;
                product.Availability = availability;
                product.Latest = latest;
                connection.Insert(product);
                MessageBox.Show("Product added!");
            }
        }
    }
}
