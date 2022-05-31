using Project_54.Objects;
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

namespace Project_54.Controls
{
    /// <summary>
    /// Логика взаимодействия для ImageControl.xaml
    /// </summary>
    public partial class ImageControl : UserControl
    {
        public Logo_Model logo_model = new Logo_Model();
        public ImageControl()
        {
            InitializeComponent();
            this.DataContext = logo_model;
        }

        private void But_1_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 0;
        }
        private void But_2_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 1;
        }
        private void But_3_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 2;
        }
        private void But_4_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 3;
        }
        private void But_5_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 4;
        }
        private void But_6_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 5;
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo--;
            if (logo_model.number_logo < 0) logo_model.number_logo = 5;
        }
        private void Rigth_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo++;
            if (logo_model.number_logo > 5) logo_model.number_logo = 0;
        }
    }
}
