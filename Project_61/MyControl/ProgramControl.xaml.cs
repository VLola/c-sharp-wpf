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
        public Variables _variables { get; set; } = new Variables();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private Process process;
        public ProgramControl(Process process)
        {
            InitializeComponent();
            this.DataContext = this;
            _variables.ProgramName = process.ProcessName;
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
            this.process = process;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //if (DateTime.Now > _variables.FinishTime) ProcessKill(process.ProcessName);
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
            if (_variables.TimeRun > 0)
            {
                _variables.FinishTime = DateTime.Now + TimeSpan.FromMinutes(_variables.TimeRun);
            }
        }
    }
}
