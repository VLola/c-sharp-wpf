using System.Windows.Controls;
using System.Windows.Media;

namespace Project_54.Control
{
    public partial class CategoryControl : UserControl
    {
        public ImageSource img { get; set; }
        public string text { get; set; }
        public CategoryControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
