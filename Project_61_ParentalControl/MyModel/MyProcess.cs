using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Project_61_ParentalControl.MyModel
{
    public class MyProcess
    {
        public AppDomain Domain;
        private TimeSpan _oneSecond = TimeSpan.FromSeconds(1);
        public Variables Variables { get; set; } = new Variables();
        public MyProcess(string Name, string FullName, DateTime StartTime, AppDomain Domain)
        {
            this.Domain = Domain;
            Variables.ProgramName = Name;
            Variables.FullName = FullName;
            Variables.WorkingTime = DateTime.Now - StartTime;
            Variables.SelectedWorkingTime = 720;
            RunAsync();
        }

        private async void RunAsync()
        {
            await Task.Run(async () => {
                while (true)
                {
                    if (Variables.isParentalControl && Variables.WorkingTime.TotalMinutes > Variables.SelectedWorkingTime) ProcessKillAsync();
                    if (Variables.isWorkingProcess) Variables.WorkingTime += _oneSecond;
                    TimeControlAsync();
                    if (Domain != null) SetDataAsync();
                    await Task.Delay(1000);
                }
            });
        }
        private async void SetDataAsync()
        {
            await Task.Run(()=> {
                bool? isParentalControl = (bool?)Domain.GetData("isParentalControl:" + Variables.ProgramName);
                if (isParentalControl != null) Variables.isParentalControl = (bool)isParentalControl;
                double? SelectedWorkingTime = (double?)Domain.GetData("SelectedWorkingTime:" + Variables.ProgramName);
                if (SelectedWorkingTime != null) Variables.SelectedWorkingTime = (double)SelectedWorkingTime;
                Domain.SetData("WorkingTime:" + Variables.ProgramName, Variables.WorkingTime);
                Domain.SetData("FullName:" + Variables.ProgramName, Variables.FullName);
                Domain.SetData("isParentalControl:" + Variables.ProgramName, Variables.isParentalControl);
                Domain.SetData("SelectedWorkingTime:" + Variables.ProgramName, Variables.SelectedWorkingTime);
            });
        }
        private async void ProcessKillAsync()
        {
            await Task.Run(() => {
                foreach (Process item in Process.GetProcesses()) if (item.ProcessName == Variables.ProgramName) item.Kill();
            });
        }

        private async void TimeControlAsync()
        {
            await Task.Run(() =>
            {
                bool Working = false;
                foreach (var it in Process.GetProcesses()) if (it.ProcessName == Variables.ProgramName) Working = true;
                if (Working) Variables.isWorkingProcess = true;
                else Variables.isWorkingProcess = false;
            });
        }
    }
}
