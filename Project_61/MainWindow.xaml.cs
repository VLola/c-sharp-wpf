using Microsoft.Win32;
using Project_61.MyControl;
using Project_61.MyModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project_61
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RegistryKey _registryCurrentUser = Registry.CurrentUser;
        private RegistryKey _registryLocalMachine = Registry.LocalMachine;
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        private List<string> _list = new List<string>();
        private int _row { get; set; } = 0;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Start();
        }

        private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Start(_registryCurrentUser);
            Start(_registryLocalMachine);
        }

        private async void Start(RegistryKey registry)
        {
            await Task.Run(()=> {
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
                                if (it.ProcessName == _name && !_list.Contains(_name))
                                {
                                    _list.Add(_name);
                                    Dispatcher.Invoke(new Action(()=> {
                                        ProgramControl programmControl = new ProgramControl(_name, _fullName, it.StartTime);
                                        ListProgram.RowDefinitions.Add(new RowDefinition());
                                        Grid.SetRow(programmControl, _row++);
                                        ListProgram.Children.Add(programmControl);
                                    }));
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}
