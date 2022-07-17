using Project_61.MyControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

namespace Project_61
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _list = new List<string>();
        private List<Process> _programs = new List<Process>();
        private int Count { get; set; } = 0;
        public ObservableCollection<ProgramControl> _collection { get; set; } = new ObservableCollection<ProgramControl>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Process item in Process.GetProcesses())
            {
                if (!_list.Contains(item.ProcessName))
                {
                    _list.Add(item.ProcessName);
                    _programs.Add(item);
                }
            }
            foreach (var item in _programs)
            {
                ProgramControl programmControl = new ProgramControl(item);
                ListProgram.RowDefinitions.Add(new RowDefinition());
                Grid.SetRow(programmControl, Count++);
                ListProgram.Children.Add(programmControl);
            }
        }
        public class Program
        {
            public string Name { get; set; }
            public DateTime StartTime { get; set; }
            public Program(string Name, DateTime StartTime)
            {
                this.Name = Name;
                this.StartTime = StartTime;
            }
        }
    }
}
