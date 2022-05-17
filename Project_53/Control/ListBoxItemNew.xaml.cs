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

namespace Project_53.Control
{
    /// <summary>
    /// Логика взаимодействия для ListBoxItemNew.xaml
    /// </summary>
    public partial class ListBoxItemNew : UserControl
    {
        public ListBoxItemNew()
        {
            InitializeComponent(); 
            this.DataContext = this;
        }
        public ImageSource icon_user { get; set; }
        public bool online { get; set; }
        public bool image_double_tick { get; set; }
        public bool image_clip { get; set; }
        public bool label_green { get; set; }
        public string label_green_content { get; set; }
        public bool label_gray { get; set; }
        public string label_gray_content { get; set; }
        public string text_title { get; set; }
        public string text_black { get; set; }
        public string text_gray { get; set; }
        public string text_time { get; set; }
    }
}
