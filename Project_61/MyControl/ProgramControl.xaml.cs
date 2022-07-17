using Project_61.MyModel;
using System;
using System.Windows.Controls;
using static Project_61.MainWindow;

namespace Project_61.MyControl
{
    /// <summary>
    /// Логика взаимодействия для ProgramControl.xaml
    /// </summary>
    public partial class ProgramControl : UserControl
    {
        public Variables _variables { get; set; } = new Variables();

        public ProgramControl(Program program)
        {
            InitializeComponent();
            this.DataContext = this;
            _variables.ProgramName = program.Name;
            _variables.StartTime = program.StartTime;
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
