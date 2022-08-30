using Project_64_Client.ModelView;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Project_64_Client.View
{
    public partial class UserView : UserControl
    {
        public UserModelView modelView { get; set; }
        public UserView()
        {
            InitializeComponent();
            Loaded += MyControl_Loaded;
            modelView = new(Password);
            this.DataContext = modelView;
        }
        private void MyControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += window_Closing;
        }

        void window_Closing(object? sender, CancelEventArgs e)
        {
            modelView.Close();
        }
    }
}
