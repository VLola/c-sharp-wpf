using Project_65.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Project_65.View
{
    public partial class SinopticView : UserControl
    {
        public ObservableCollection<RowModel> Values
        {
            get { return (ObservableCollection<RowModel>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }
        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(ObservableCollection<RowModel>), typeof(SinopticView), new PropertyMetadata(null));
        public SinopticView()
        {
            InitializeComponent();
        }
    }
}
