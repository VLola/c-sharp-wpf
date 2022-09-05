using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_65.Model
{
    public class SinopticModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private BitmapImage _logo { get; set; }
        public BitmapImage Logo
        {
            get { return _logo; }
            set
            {
                _logo = value;
                OnPropertyChanged("Logo");
            }
        }
        private string _temperature { get; set; }
        public string Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                OnPropertyChanged("Temperature");
            }
        }
        private int _temperatureView { get; set; }
        public int TemperatureView
        {
            get { return _temperatureView; }
            set
            {
                _temperatureView = value;
                OnPropertyChanged("TemperatureView");
            }
        }
    }
}
