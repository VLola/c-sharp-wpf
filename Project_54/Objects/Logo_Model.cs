using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Project_54.Objects
{
    public class Logo_Model : INotifyPropertyChanged
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
        public Logo_Model()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
