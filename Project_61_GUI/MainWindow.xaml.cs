using Project_61_GUI.MyControls;
using Project_61_GUI.MyModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace Project_61_GUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _row = 0;
        static AppDomain Domain;
        private ObservableCollection<MyProgram> _list { get; set; } = new ObservableCollection<MyProgram>();
        private DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Domain = AppDomain.CurrentDomain;
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<string> list = (List<string>)Domain.GetData("parameter");
            if (list != null && list.Count > 0)
                foreach (var item in list)
                {
                    int i = 0;
                    bool check = false;
                    foreach (var it in _list)
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
                        TimeSpan WorkingTime = (TimeSpan)Domain.GetData("WorkingTime:" + item);
                        if (WorkingTime != null) _list[i].WorkingTime = WorkingTime;
                        string FullName = (string)Domain.GetData("FullName:" + item);
                        if (FullName != null) _list[i].FullName = FullName;
                    }
                    else
                    {
                        MyProgram myProgram = new MyProgram();
                        myProgram.ProgramName = item;
                        TimeSpan timeSpan = (TimeSpan)Domain.GetData("WorkingTime:" + item);
                        if (timeSpan != null) myProgram.WorkingTime = timeSpan;
                        string FullName = (string)Domain.GetData("FullName:" + item);
                        if (FullName != null) myProgram.FullName = FullName;
                        bool? TimeControl = (bool?)Domain.GetData("TimeControl:" + item);
                        if (TimeControl != null) myProgram.TimeControl = (bool)TimeControl;
                        _list.Add(myProgram);
                        Programs.RowDefinitions.Add(new RowDefinition());
                        ProgramControl control = new ProgramControl(ref myProgram);
                        Grid.SetRow(control, _row++);
                        Programs.Children.Add(control);
                    }
                }
        }
    }
}
