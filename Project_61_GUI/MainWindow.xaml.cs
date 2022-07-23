using Project_61_GUI.MyControls;
using Project_61_GUI.MyModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _domain = AppDomain.CurrentDomain;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<string> list = (List<string>)_domain.GetData("parameter");
            if (list != null && list.Count > 0)
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
                        History.Add(new HistoryWorking(item, DateTime.Now, "Start"));
                        
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
                        Programs.RowDefinitions.Add(new RowDefinition());
                        ProgramControl control = new ProgramControl(ref myProgram);
                        Grid.SetRow(control, _row++);
                        Programs.Children.Add(control);
                    }
                }
        }
        public class HistoryWorking{
            public string Name { get; set; }
            public DateTime DateTime { get; set; }
            public string Status { get; set; }
            public HistoryWorking(string Name, DateTime DateTime, string Status)
            {
                this.Name = Name;
                this.DateTime = DateTime;
                this.Status = Status;
            }
        }
    }
}
