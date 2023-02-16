using System;

namespace Project_79.Models
{
    public class Blob : Changed
    {
        public string Name { get; set; }
        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }
    }
}
