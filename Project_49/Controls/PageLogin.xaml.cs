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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : UserControl
    {
        public Grid grid;
        public PageLogin(Grid grid)
        {
            InitializeComponent();
            this.grid = grid;
            ButtonRegestration.Click += ButtonRegestration_Click;
        }
        private void ButtonRegestration_Click(object sender, RoutedEventArgs e) {
            grid.Children.Clear();
            grid.Children.Add(new PageRegestration());
        }
    }
}
