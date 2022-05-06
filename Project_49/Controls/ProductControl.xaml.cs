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
        private const string path = "pack://application:,,,/Project_49;component/Resources/Products/";
        public ProductControl(string name_product, string image_product)
        {
            InitializeComponent();
            this.name_product.Text = name_product;
            this.image_product.Source = new BitmapImage(new Uri(path + image_product));

            int discount_number = 0;
            bool is_latest = true;
            bool is_discount = false;
            bool is_availability = true;
            int id_product = 123456;
            int price = 20899;

            string discount_text = "-" + discount_number + "%";

            if (is_latest) { grid_latest.Visibility = Visibility.Visible; }
            else grid_latest.Visibility = Visibility.Hidden;

            if (is_discount)
            {
                discount.Content = discount_text;
                grid_discount.Visibility = Visibility.Visible;
                price_discount.Visibility = Visibility.Visible;
            }
            else
            {
                grid_discount.Visibility = Visibility.Hidden;
                price_discount.Visibility = Visibility.Hidden;
            }
            if (is_availability) availability.Content = "Есть в наличии";
            else availability.Content = "Нет в наличии";
            id.Content = $"код: {IdProduct(id_product)}";
            price_discount.Text = $"{price} грн";
            price_product.Text = $"{Price(price, discount_number)} грн";
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
