using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_61_GUI.MyModels
{
    public class Variables : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private bool _isLicense;
        public bool isLicense
        {
            get { return _isLicense; }
            set
            {
                _isLicense = value;
                OnPropertyChanged("isLicense");
            }
        }
        private string _LicenseKey;
        public string LicenseKey
        {
            get { return _LicenseKey; }
            set
            {
                _LicenseKey = value;
                OnPropertyChanged("LicenseKey");
            }
        }

    }
}
