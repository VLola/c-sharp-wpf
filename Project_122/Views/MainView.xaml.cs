using Project_122.ViewModels;
using System.Windows.Controls;

namespace Project_122.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
