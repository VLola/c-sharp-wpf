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
    /// Логика взаимодействия для PageProducts.xaml
    /// </summary>
    public partial class PageProducts : UserControl
    {
        private string search;
        public PageProducts(string search)
        {
            InitializeComponent();
            this.search = search;
            SetProduct();
        }
        private void SetProduct()
        {
            int i = 0, j = 0;
            ModelProduct modelProduct = new ModelProduct();
            List<Models.Product> list = new List<Models.Product>();
            foreach (var it in modelProduct.Products)
            {
                if (it.Name.Contains(search)) list.Add(it);
            }
            list.Reverse();
            foreach (var it in list)
            {
                if (j == 0) products.RowDefinitions.Add(new RowDefinition());
                ProductControl Product = new ProductControl(it);
                Grid.SetColumn(Product, j);
                Grid.SetRow(Product, i);
                products.Children.Add(Product);
                j++;
                if (j == 5)
                {
                    j = 0;
                    i++;
                }
            }
        }
    }
}
