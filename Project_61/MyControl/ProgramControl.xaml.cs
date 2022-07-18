using Project_61.MyModel;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Threading;
using static Project_61.MainWindow;

namespace Project_61.MyControl
{
    /// <summary>
    /// Логика взаимодействия для ProgramControl.xaml
    /// </summary>
    public partial class ProgramControl : UserControl
    {
        public Variables Variables { get; set; } = new Variables();
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        public ObservableCollection<double> WorkingTime { get; set; } = new ObservableCollection<double>();
        public ProgramControl(string name, string fullname, DateTime startTime)
        {
            InitializeComponent();
            this.DataContext = this;
            Variables.PropertyChanged += Variables_PropertyChanged;
            Variables.ProgramName = name;
            Variables.FullName = fullname;
            Variables.StartTime = startTime;
            WorkingTime.Add(1);
            WorkingTime.Add(5);
            WorkingTime.Add(10);
            WorkingTime.Add(15);
            WorkingTime.Add(30);
            WorkingTime.Add(45);
            WorkingTime.Add(60);
            WorkingTime.Add(120);
            WorkingTime.Add(180);
            WorkingTime.Add(360);
            WorkingTime.Add(720);
            Variables.SelectedWorkingTime = 720;
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }

        private void Variables_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ParentalControl" && !Variables.ParentalControl) Variables.SelectedWorkingTime = 720;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (!Variables.Finish)
            {
                Variables.WorkingTime = DateTime.Now - Variables.StartTime;
            }
            if (Variables.ParentalControl && Variables.WorkingTime.TotalMinutes > Variables.SelectedWorkingTime)
            {
                Variables.Finish = true;
                ProcessKill(Variables.ProgramName);
            }
            else if (Variables.Finish && Variables.WorkingTime.TotalMinutes <= Variables.SelectedWorkingTime)
            {
                Variables.Finish = false;
            }
        }
        private void ProcessKill(string name)
        {
            foreach (Process item in Process.GetProcesses())
            {
                if (item.ProcessName == name) item.Kill();
            }
        }
    }
}
