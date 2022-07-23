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
        private bool _isParentalControl;
        public bool isParentalControl
        {
            get { return _isParentalControl; }
            set
            {
                _isParentalControl = value;
                OnPropertyChanged("isParentalControl");
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
