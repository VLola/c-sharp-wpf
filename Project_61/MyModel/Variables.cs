using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project_61.MyModel
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
        private double _TimeRun { get; set; } = 200;
        public double TimeRun
        {
            get { return _TimeRun; }
            set
            {
                _TimeRun = value;
                OnPropertyChanged("TimeRun");
            }
        }
        private double _WorkTime { get; set; }
        public double WorkTime
        {
            get { return _WorkTime; }
            set
            {
                _WorkTime = value;
                OnPropertyChanged("WorkTime");
            }
        }
        private DateTime _StartTime { get; set; } = DateTime.Now;
        public DateTime StartTime
        {
            get { return _StartTime; }
            set
            {
                _StartTime = value;
                OnPropertyChanged("StartTime");
            }
        }
    }
}
