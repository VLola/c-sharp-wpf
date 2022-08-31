using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_64_Client.Model
{
    public class ClientModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private string _firstName { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }

        }
        private bool _isLogin { get; set; } = true;
        public bool IsLogin
        {
            get { return _isLogin; }
            set
            {
                _isLogin = value;
                OnPropertyChanged("IsLogin");
            }
        }
        private bool _isRegister { get; set; }
        public bool IsRegister
        {
            get { return _isRegister; }
            set
            {
                _isRegister = value;
                OnPropertyChanged("IsRegister");
            }
        }
        private bool _isLoginClient { get; set; }
        public bool IsLoginClient
        {
            get { return _isLoginClient; }
            set
            {
                _isLoginClient = value;
                OnPropertyChanged("IsLoginClient");
            }
        }
        private bool _isRecord { get; set; }
        public bool IsRecord
        {
            get { return _isRecord; }
            set
            {
                _isRecord = value;
                OnPropertyChanged("IsRecord");
            }
        }
    }
}
