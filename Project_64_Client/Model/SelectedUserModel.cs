using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_64_Client.Model
{
    public class SelectedUserModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private UserModel _user { get; set; }
        public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                OnPropertyChanged("User");
            }
        }
        private string _message { get; set; }
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }
        private string _addNameUser { get; set; }
        public string AddNameUser
        {
            get { return _addNameUser; }
            set
            {
                _addNameUser = value;
                OnPropertyChanged("AddNameUser");
            }
        }
    }
}
