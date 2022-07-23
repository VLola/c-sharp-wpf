using System;

namespace Project_61_GUI.MyModels
{
    public class HistoryWorking
    {
        public string FullName { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
        public HistoryWorking(string FullName, DateTime DateTime, string Status)
        {
            this.FullName = FullName;
            this.DateTime = DateTime;
            this.Status = Status;
        }
    }
}
