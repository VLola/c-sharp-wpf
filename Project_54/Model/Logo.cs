using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Project_54.Model
{
    public class Logo : INotifyPropertyChanged
    {
        private int _number_logo;
        public int number_logo
        {
            get { return _number_logo; }
            set
            {
                _number_logo = value;
                OnPropertyChanged("number_logo");
            }
        }
        public Logo()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
