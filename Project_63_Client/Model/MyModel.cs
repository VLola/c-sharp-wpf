using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_63_Client.Model
{
    public class MyModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private string _Email { get; set; }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged("Email");
            }

        }
        private string _Password { get; set; }
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged("Password");
            }

        }
        private bool _IsLogin { get; set; } = true;
        public bool IsLogin
        {
            get { return _IsLogin; }
            set
            {
                _IsLogin = value;
                OnPropertyChanged("IsLogin");
            }
        }
        private bool _IsRegister { get; set; }
        public bool IsRegister
        {
            get { return _IsRegister; }
            set
            {
                _IsRegister = value;
                OnPropertyChanged("IsRegister");
            }
        }
        private bool _IsLoginClient { get; set; }
        public bool IsLoginClient
        {
            get { return _IsLoginClient; }
            set
            {
                _IsLoginClient = value;
                OnPropertyChanged("IsLoginClient");
            }
        }
    }
}
