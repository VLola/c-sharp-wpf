using Microsoft.Win32;
using Project_49.ConnectDB;
using Project_49.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_49.Controls
{
    /// <summary>
    /// Логика взаимодействия для PageUserInfo.xaml
    /// </summary>
    public partial class PageUserInfo : UserControl
    {
        public OpenFileDialog openFileDialog = new OpenFileDialog();
        public List<string> list_name_product = new List<string>();
        public PageUserInfo(bool admin, string email)
        {
            InitializeComponent();
            if (admin) added_product.Visibility = Visibility.Visible;
            image_product_button.Click += Background_image_button_Click;
            added_product_button.Click += Added_product_button_Click;
            list_product.ItemsSource = list_name_product;
            LoadListProduct();
            delete_product_button.Click += Delete_product_button_Click;
        }


        private void Added_product_button_Click(object sender, RoutedEventArgs e)
        {
            if (name_product.Text != "" && price_product.Text != "" && discount_product.Text != "" && image_product.Text != "")
            {
                ConnectProduct.AddProduct(name_product.Text, Convert.ToInt32(price_product.Text), Convert.ToInt32(discount_product.Text), image_product.Text, (bool)availability_product.IsChecked, (bool)latest_product.IsChecked);
                name_product.Text = "";
                price_product.Text = "";
                image_product.Text = "";
            }
        }

        private void Background_image_button_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.ShowDialog();
            string path = "";
            path += openFileDialog.FileName;
            if (path != "")
            {
                image_product.Text = path;
            }
            openFileDialog.Reset();
        }
        private void LoadListProduct()
        {
            ModelProduct modelProduct = new ModelProduct();
            foreach (Models.Product it in modelProduct.Products)
            {
                list_name_product.Add(it.Name);
            }
            list_product.Items.Refresh();
            list_product.SelectedIndex = 0;
        }
        private void Delete_product_button_Click(object sender, RoutedEventArgs e)
        {
            ModelProduct modelProduct = new ModelProduct();
            foreach (Models.Product it in modelProduct.Products) if (it.Name == (string)list_product.SelectedItem) ConnectProduct.DeleteProduct(it);
            list_name_product.Clear();
            LoadListProduct();
        }
    }
}
