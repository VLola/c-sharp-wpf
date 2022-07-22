using Microsoft.Win32;
using Project_61_ParentalControl.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Project_61_ParentalControl
{
    public partial class MainWindow : Window
    {
        public AppDomain Domain;
        private List<string> _processNames { get; set; } = new List<string>();
        private RegistryKey _registryCurrentUser = Registry.CurrentUser;
        private RegistryKey _registryLocalMachine = Registry.LocalMachine;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        public ObservableCollection<MyProcess> MyProcesses { get; set; } = new ObservableCollection<MyProcess>();
        public Variables MainVariables { get; set; } = new Variables();
        public string Password { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MyPasswordBox.Password == "1111")
            {
                MainVariables.isLogin = true;
                _dispatcherTimer.Tick += _dispatcherTimer_Tick;
                _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
                _dispatcherTimer.Start();
            }
            else MessageBox.Show("Error password!");
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Domain != null) AddProgram();
        }
        private async void AddProgram()
        {
            await Task.Run(async () => {
                await Start(_registryCurrentUser);
                await Start(_registryLocalMachine);
                if (Domain != null) Domain.SetData("parameter", _processNames);
            });
        }

        private async Task Start(RegistryKey registry)
        {
            await Task.Run(() => {
                RegistryKey myAppKey = registry.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
                foreach (var item in myAppKey.GetSubKeyNames())
                {
                    RegistryKey AppKey = registry.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + item);
                    string _path = (string)AppKey.GetValue("DisplayIcon");
                    string _fullName = (string)AppKey.GetValue("DisplayName");
                    if (_path != null && _fullName != null && _path.Contains(".exe"))
                    {
                        string _name = Regex.Match(_path, @".*\\(.*)\.exe").Groups[1].Value;
                        if (_name != "")
                        {
                            foreach (var it in Process.GetProcesses())
                            {
                                if (it.ProcessName == _name && !_processNames.Contains(_name))
                                {
                                    _processNames.Add(_name);
                                    MyProcesses.Add(new MyProcess(_name, _fullName, it.StartTime, Domain));
                                }
                            }
                        }
                    }
                }
            });
        }
        private void LaunchGUI_Click(object sender, RoutedEventArgs e)
        {
            Domain = AppDomain.CreateDomain("Second Domain");
            foreach (var item in MyProcesses)
            {
                item.Domain = Domain;
            }
            Domain.ExecuteAssembly("Project_61_GUI.exe");
        }
    }
}
