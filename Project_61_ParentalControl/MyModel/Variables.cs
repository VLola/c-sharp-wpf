using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_61_ParentalControl.MyModel
{
    public class Variables : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private bool _isLogin;
        public bool isLogin
        {
            get { return _isLogin; }
            set
            {
                _isLogin = value;
                OnPropertyChanged("isLogin");
            }
        }

        private string _ProgramName { get; set; }
        public string ProgramName
        {
            get { return _ProgramName; }
            set
            {
                _ProgramName = value;
                OnPropertyChanged("ProgramName");
            }
        }
        private string _FullName { get; set; }
        public string FullName
        {
            get { return _FullName; }
            set
            {
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }
        private TimeSpan _WorkingTime { get; set; }
        public TimeSpan WorkingTime
        {
            get { return _WorkingTime; }
            set
            {
                _WorkingTime = value;
                OnPropertyChanged("WorkingTime");
            }
        }
        private double _SelectedWorkingTime { get; set; }
        public double SelectedWorkingTime
        {
            get { return _SelectedWorkingTime; }
            set
            {
                _SelectedWorkingTime = value;
                OnPropertyChanged("SelectedWorkingTime");
            }
        }
        private bool _Finish { get; set; }
        public bool Finish
        {
            get { return _Finish; }
            set
            {
                _Finish = value;
                OnPropertyChanged("Finish");
            }
        }
        private bool _ParentalControl { get; set; }
        public bool ParentalControl
        {
            get { return _ParentalControl; }
            set
            {
                _ParentalControl = value;
                OnPropertyChanged("ParentalControl");
            }
        }
        private bool _TimeControl { get; set; }
        public bool TimeControl
        {
            get { return _TimeControl; }
            set
            {
                _TimeControl = value;
                OnPropertyChanged("TimeControl");
            }
        }
        private bool _isWorkingProcess = true;
        public bool isWorkingProcess
        {
            get { return _isWorkingProcess; }
            set
            {
                _isWorkingProcess = value;
                OnPropertyChanged("isWorkingProcess");
            }
        }
    }
}
