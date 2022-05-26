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
        public bool Availability { get; set; }
        public bool Discount { get; set; }
        public bool Latest { get; set; }
        public string id{ get; set; }
        public string name_product { get; set; }
        public string availability { get; set; }
        public string button_content { get; set; }
        public string discount_text { get; set; }
        public string price_discount { get; set; }
        public ImageSource image_product { get; set; }
        public string price_product { get; set; }

        public ProductControl(Models.Product product)
        {
            InitializeComponent();
            name_product = product.Name;
            image_product = new BitmapImage(new Uri(product.Image));
            Availability = (bool)product.Availability;
            id = $"код: {IdProduct(product.Id)}";
            price_discount = $"{product.Price} грн";
            discount_text = "-" + product.Discount + "%";
            Latest = (bool)product.Latest;

            if (product.Discount > 0)
            {
                Latest = false;
                Discount = true;
            }
            else Discount = false;

            price_product = $"{Price(product.Price, (int)product.Discount)} грн";
            this.DataContext = this;
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
