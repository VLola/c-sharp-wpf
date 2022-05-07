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
    /// Логика взаимодействия для LatestProductControl.xaml
    /// </summary>
    public partial class LatestProductControl : UserControl
    {
        public LatestProductControl(Models.Product product)
        {
            InitializeComponent();
            product_name.Text = product.Name;
            product_image.Source = new BitmapImage(new Uri(product.Image));
            product_price.Content = $"{product.Price} грн";
            grid_main.MouseEnter += Border_product_MouseEnter;
            grid_main.MouseLeave += Border_product_MouseLeave;
        }

        private void Border_product_MouseLeave(object sender, MouseEventArgs e)
        {
            border_product.BorderBrush = Brushes.White;
            image_compare.Visibility = Visibility.Hidden;
        }

        private void Border_product_MouseEnter(object sender, MouseEventArgs e)
        {
            border_product.BorderBrush = Brushes.LightGray;
            image_compare.Visibility = Visibility.Visible;
        }
    }
}
