using Microsoft.Win32;
using Newtonsoft.Json;
using Project_61_GUI.MyControls;
using Project_61_GUI.MyModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Project_61_GUI
{
    public partial class MainWindow : Window
    {
        private int _row = 0;
        private AppDomain _domain;
        private ObservableCollection<MyProgram> _myPrograms { get; set; } = new ObservableCollection<MyProgram>();
        public ObservableCollection<HistoryWorking> History { get; set; } = new ObservableCollection<HistoryWorking>();
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        public Variables Variables { get; set; } = new Variables();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CreateLicenseKey();
            _domain = AppDomain.CurrentDomain;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            HistoryAsync();
            LoadingData();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (MyPasswordBox.Password == "1111")
            {
                Variables.isLogin = true;
            }
            else MessageBox.Show("Error password!");
        }
        private async void LoadingData()
        {
            await Task.Run(()=> {
                List<string> list = (List<string>)_domain.GetData("_processNames");
                if (list != null && list.Count > 0) {
                    foreach (var item in list)
                    {
                        int i = 0;
                        bool check = false;
                        foreach (var it in _myPrograms)
                        {
                            if (item == it.ProgramName)
                            {
                                check = true;
                                break;
                            }
                            i++;
                        }
                        if (check)
                        {
                            TimeSpan WorkingTime = (TimeSpan)_domain.GetData("WorkingTime:" + item);
                            if (WorkingTime != null) _myPrograms[i].WorkingTime = WorkingTime;
                            bool? isParentalControl = (bool?)_domain.GetData("isParentalControl:" + item);
                            if (isParentalControl != null) _myPrograms[i].isParentalControl = (bool)isParentalControl;
                            double? SelectedWorkingTime = (double?)_domain.GetData("SelectedWorkingTime:" + item);
                            if (SelectedWorkingTime != null) _myPrograms[i].SelectedWorkingTime = (double)SelectedWorkingTime;
                        }
                        else
                        {
                            MyProgram myProgram = new MyProgram();
                            myProgram.ProgramName = item;
                            TimeSpan timeSpan = (TimeSpan)_domain.GetData("WorkingTime:" + item);
                            if (timeSpan != null) myProgram.WorkingTime = timeSpan;
                            string FullName = (string)_domain.GetData("FullName:" + item);
                            if (FullName != null) myProgram.FullName = FullName;
                            bool? isParentalControl = (bool?)_domain.GetData("isParentalControl:" + item);
                            if (isParentalControl != null) myProgram.isParentalControl = (bool)isParentalControl;
                            double? SelectedWorkingTime = (double?)_domain.GetData("SelectedWorkingTime:" + item);
                            if (SelectedWorkingTime != null) myProgram.SelectedWorkingTime = (double)SelectedWorkingTime;
                            _myPrograms.Add(myProgram);
                            Dispatcher.Invoke(new Action(() => {
                                Programs.RowDefinitions.Add(new RowDefinition());
                                ProgramControl control = new ProgramControl(ref myProgram);
                                Grid.SetRow(control, _row++);
                                Programs.Children.Add(control);
                            }));
                        }
                    }
                }
            });
        }
        private async void HistoryAsync()
        {
            await Task.Run(()=> {
                string json = (string)_domain.GetData("_programsWorkingHistory");
                if (json != null)
                {
                    List<HistoryWorking> list = JsonConvert.DeserializeObject<List<HistoryWorking>>(json);
                    if (list.Count > 0 && list.Count > History.Count)
                    {
                        foreach (var item in list)
                        {
                            bool check = false;
                            foreach (var it in History)
                            {
                                if (item.FullName == it.FullName && item.DateTime == it.DateTime && item.Status == it.Status) check = true;
                            }
                            if (!check) Dispatcher.Invoke(new Action(()=> { History.Add(item); }));
                        }
                    }
                }
            });
        }
        private void CreateLicenseKey()
        {
            using (RegistryKey registry = Registry.CurrentUser.CreateSubKey(@"Software\ParentalControl"))
            {
                registry.SetValue("LicenseKey", "lola");
            }
        }
        private void ActivateLicense_Click(object sender, RoutedEventArgs e)
        {
            using (RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"Software\ParentalControl"))
            {
                if (Variables.LicenseKey == (string)registry.GetValue("LicenseKey")) Variables.isLicense = true;
                else MessageBox.Show("Error!");
            }
        }
    }
}
