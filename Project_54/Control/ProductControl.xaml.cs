using System.Windows.Controls;
using System.Windows.Media;

namespace Project_54.Control
{
    public partial class ProductControl : UserControl
    {
        public ImageSource logo_one { get; set; }
        public ImageSource logo_two { get; set; }
        public string name { get; set; }
        public string review { get; set; }
        public string price { get; set; }
        public string discount { get; set; }
        public ProductControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
