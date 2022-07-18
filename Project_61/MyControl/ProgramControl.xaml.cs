using Project_61.MyModel;
using System;
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
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public ProgramControl(string name, string fullname, DateTime startTime)
        {
            InitializeComponent();
            this.DataContext = this;
            Variables.ProgramName = name;
            Variables.FullName = fullname;
            Variables.StartTime = startTime;
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Variables.WorkTime = Math.Round((DateTime.Now - Variables.StartTime).TotalMinutes);
            if(Variables.WorkTime > Variables.TimeRun)
            {
                ProcessKill(Variables.ProgramName);
            }
        }
        private void ProcessKill(string name)
        {
            foreach (Process item in Process.GetProcesses())
            {
                if (item.ProcessName == name) item.Kill();
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Variables.TimeRun > 0)
            {
                //Variables.TimeRun = DateTime.Now + TimeSpan.FromMinutes(Variables.TimeRun);
            }
        }
    }
}
