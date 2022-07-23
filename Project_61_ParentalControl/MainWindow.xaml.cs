using Microsoft.Win32;
using Newtonsoft.Json;
using Project_61_ParentalControl.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Project_61_ParentalControl
{
    public partial class MainWindow : Window
    {
        public AppDomain Domain;
        private string _path = Directory.GetCurrentDirectory() + "/history";
        private string _programsWorkingHistory;
        private List<Program> _programs = new List<Program>();
        private List<HistoryWorking> _listProgramsWorkingHistory = new List<HistoryWorking>();
        private List<string> _processNames { get; set; } = new List<string>();
        private List<string> _processNamesHistory { get; set; } = new List<string>();
        private RegistryKey _registryCurrentUser = Registry.CurrentUser;
        private RegistryKey _registryLocalMachine = Registry.LocalMachine;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private ObservableCollection<MyProcess> _myProcesses { get; set; } = new ObservableCollection<MyProcess>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MainWindow_Loaded;
            Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadingProgramsAsync();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }

        private async void LoadingProgramsAsync()
        {
            await Task.Run(async () => {
                await StartAsync(_registryCurrentUser);
                await StartAsync(_registryLocalMachine);
            });
        }

        private void LaunchGUI_Click(object sender, RoutedEventArgs e)
        {
            Domain = AppDomain.CreateDomain("Second Domain");
            foreach (var item in _myProcesses)
            {
                item.Domain = Domain;
            }
            Domain.ExecuteAssembly("Project_61_GUI.exe");
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Domain != null) AddProgram();
        }

        private async void AddProgram()
        {
            await Task.Run(async () => {
                await CheckProgramStartAsync();
                await TimeControlAsync();
                await FileSyncAsync();
                if (Domain != null) {
                    Domain.SetData("_processNames", _processNames);
                    Domain.SetData("_programsWorkingHistory", _programsWorkingHistory);
                }
            });
        }

        private async Task StartAsync(RegistryKey registry)
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
                            _programs.Add(new Program(_name, _fullName));
                        }
                    }
                }
            });
        }

        private async Task CheckProgramStartAsync()
        {
            await Task.Run(()=> {
                foreach (var item in _programs)
                {
                    foreach (var it in Process.GetProcesses())
                    {
                        if (it.ProcessName == item.Name && !_processNames.Contains(item.Name))
                        {
                            _processNames.Add(item.Name);
                            _myProcesses.Add(new MyProcess(item.Name, item.FullName, it.StartTime, Domain));
                        }
                        if (it.ProcessName == item.Name && !_processNamesHistory.Contains(item.Name))
                        {
                            _processNamesHistory.Add(item.Name);
                            _listProgramsWorkingHistory.Add(new HistoryWorking(item.FullName, it.StartTime, "Start"));
                        }
                    }
                }
            });
        }

        private async Task TimeControlAsync()
        {
            await Task.Run(() =>
            {
                foreach (var item in _programs)
                {
                    bool Working = false;
                    foreach (var it in Process.GetProcesses())
                    {
                        if (item.Name == it.ProcessName) Working = true;
                    }
                    if (!Working && _processNamesHistory.Contains(item.Name)) {
                        _processNamesHistory.Remove(item.Name);
                        _listProgramsWorkingHistory.Add(new HistoryWorking(item.FullName, DateTime.Now, "Close"));
                    }
                }
            });
        }

        private async Task FileSyncAsync()
        {
            await Task.Run(()=> {
                if (File.Exists(_path)) {
                    _programsWorkingHistory = File.ReadAllText(_path);
                    List<HistoryWorking> list = JsonConvert.DeserializeObject<List<HistoryWorking>>(_programsWorkingHistory);
                    foreach (var item in _listProgramsWorkingHistory)
                    {
                        bool check = false;
                        foreach (var it in list)
                        {
                            if (item.FullName == it.FullName && item.DateTime == it.DateTime && item.Status == it.Status) check = true;
                        }
                        if (!check) list.Add(item);
                    }
                    _listProgramsWorkingHistory = list;
                }
                File.WriteAllText(_path ,JsonConvert.SerializeObject(_listProgramsWorkingHistory));
            });
        }
    }
}

