using Project_54.Objects;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Project_54.Controls
{
    /// <summary>
    /// Логика взаимодействия для BigCategoryControl.xaml
    /// </summary>
    public partial class BigCategoryControl : UserControl
    {
        public SubCategoris sub_categoris { get; set; } = new SubCategoris();
        public string categoris { get; set; } = "";
        public ImageSource img { get; set; }
        public string text { get; set; }
        public BigCategoryControl()
        {
            InitializeComponent(); 
            this.DataContext = this;
            Loaded += BigCategoryControl_Loaded;
        }

        private void BigCategoryControl_Loaded(object sender, RoutedEventArgs e)
        {
            sub_categoris.list = new List<string>();
            string category = "";
            foreach (var it in categoris)
            {
                if (it == '_')
                {
                    sub_categoris.list.Add(category);
                    category = "";
                }else category += it;
            }
        }
    }
}
