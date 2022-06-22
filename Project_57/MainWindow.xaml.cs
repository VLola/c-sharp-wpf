using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Project_57
{
    public partial class MainWindow : Window
    {
        public class ListProcess
        {
            public string ProcessName { get; set; }
            public Process SelectedProcess { get; set; }
            public int CountProcess { get; set; } = 0;
            public ObservableCollection<Process> list_processes { get; set; } = new ObservableCollection<Process>();
        }

        public ObservableCollection<ListProcess> all_list_process { get; set; } = new ObservableCollection<ListProcess>();
        public ListProcess SelectedListProcess { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            MainProcesses();
        }
        private void MainProcesses()
        {
            if (all_list_process.Count > 0) all_list_process.Clear();
            var myProcess = from proc in Process.GetProcesses(".") orderby proc.Id select proc;
            foreach (var it in myProcess)
            {
                if (!Search(it))
                {
                    ListProcess list = new ListProcess();
                    list.ProcessName = it.ProcessName;
                    list.list_processes.Add(it);
                    list.CountProcess++;
                    all_list_process.Add(list);
                }
            }
        }

        private bool Search(Process pr)
        {
            bool check = false;
            foreach (var it in all_list_process)
            {
                if (it.ProcessName == pr.ProcessName)
                {
                    it.list_processes.Add(pr);
                    it.CountProcess++;
                    check = true;
                }
            }
            return check;
        }
        private void Click_KillAll(object sender, RoutedEventArgs e)
        {
            if (SelectedListProcess != null)
            {
                foreach (var it in SelectedListProcess.list_processes)
                {
                    it.Kill();
                }
                all_list_process.Remove(SelectedListProcess);
            }
            else MessageBox.Show("Selected processes!");
        }
        public void Click_Kill(object sender, RoutedEventArgs e)
        {
            bool check_delete = false;
            ListProcess listProcess = new ListProcess();
            foreach (var it in all_list_process)
            {
                if (it.SelectedProcess != null)
                {
                    it.SelectedProcess.Kill();
                    check_delete = true;
                    it.list_processes.Remove(it.SelectedProcess);
                    if (it.list_processes.Count == 0) listProcess = it;
                }
            }
            if (check_delete) all_list_process.Remove(listProcess);
            else MessageBox.Show("Selected process!");
        }
    }
}
