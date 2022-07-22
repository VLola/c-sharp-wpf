using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_61_GUI.MyModels
{
    public class MyProgram : INotifyPropertyChanged
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
    }
}
