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
        private string _ProgramName { get; set; } = "deasgrearea";
        public string ProgramName
        {
            get { return _ProgramName; }
            set
            {
                _ProgramName = value;
                OnPropertyChanged("ProgramName");
            }
        }
        private static int _TimeRun { get; set; } = 1;
        public int TimeRun
        {
            get { return _TimeRun; }
            set
            {
                _TimeRun = value;
                OnPropertyChanged("TimeRun");
            }
        }
        private DateTime _FinishTime { get; set; } = DateTime.Now + TimeSpan.FromMinutes(_TimeRun);
        public DateTime FinishTime
        {
            get { return _FinishTime; }
            set
            {
                _FinishTime = value;
                OnPropertyChanged("FinishTime");
            }
        }
        private DateTime _StartTime { get; set; }
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
