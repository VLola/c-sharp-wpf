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
    /// Логика взаимодействия для AccountInfoUser.xaml
    /// </summary>
    public partial class AccountInfoUser : UserControl
    {
        public AccountInfoUser(bool admin)
        {
            InitializeComponent();
            if (admin) added_product.Visibility = Visibility.Visible;
        }
    }
}
