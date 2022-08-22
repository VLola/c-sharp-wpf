using Project_63_Client.ModelView;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Project_63_Client.View
{
    public partial class MyControl : UserControl
    {
        public MyModelView modelView { get; set; } = new MyModelView();
        public MyControl()
        {
            InitializeComponent();
            Loaded += MyControl_Loaded;
            this.DataContext = modelView;
        }

        private void MyControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += window_Closing;
        }

        void window_Closing(object sender, CancelEventArgs e)
        {
            modelView.Close();
        }
    }
}
