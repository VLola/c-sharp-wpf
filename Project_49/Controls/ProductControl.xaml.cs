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
    /// Логика взаимодействия для ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
        public ProductControl(Models.Product product)
        {
            InitializeComponent();
            name_product.Text = product.Name;
            image_product.Source = new BitmapImage(new Uri(product.Image));


            string discount_text = "-" + product.Discount + "%";

            bool latest = (bool)product.Latest;
            

            if (product.Discount > 0)
            {
                latest = false;
                discount.Content = discount_text;
                grid_discount.Visibility = Visibility.Visible;
                price_discount.Visibility = Visibility.Visible;
            }
            else
            {
                grid_discount.Visibility = Visibility.Hidden;
                price_discount.Visibility = Visibility.Hidden;
            }

            if (latest == true) { grid_latest.Visibility = Visibility.Visible; }
            else grid_latest.Visibility = Visibility.Hidden;

            if (product.Availability == true) availability.Content = "Есть в наличии";
            else availability.Content = "Нет в наличии";
            id.Content = $"код: {IdProduct(product.Id)}";
            price_discount.Text = $"{product.Price} грн";
            price_product.Text = $"{Price(product.Price, (int)product.Discount)} грн";
        }
        private string IdProduct(int id_product)
        {
            string number = "00000000";
            string new_number = number + id_product.ToString();
            return new_number.Remove(0, new_number.Length - 9);
        }
        private int Price(int price, int percent)
        {
            if(percent == 0)
            {
                return price;
            }
            else
            {
                return price - (price / 100 * percent);
            }
        }
        
    }
}
