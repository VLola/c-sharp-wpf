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
        private const string path = "pack://application:,,,/Project_49;component/Resources/Products/";
        public LatestProductControl(string name_product, string image_product)
        {
            InitializeComponent();
            product.Text = name_product;
            image.Source = new BitmapImage(new Uri(path + image_product));
            grid_main.MouseEnter += Border_product_MouseEnter;
            grid_main.MouseLeave += Border_product_MouseLeave;
        }

        private void Border_product_MouseLeave(object sender, MouseEventArgs e)
        {
            //border_product.BorderThickness = new Thickness(2);
            border_product.BorderBrush = Brushes.White;
            image_compare.Visibility = Visibility.Hidden;
        }

        private void Border_product_MouseEnter(object sender, MouseEventArgs e)
        {
            //border_product.BorderThickness = new Thickness(2);
            border_product.BorderBrush = Brushes.LightGray;
            image_compare.Visibility = Visibility.Visible;
        }
    }
}
