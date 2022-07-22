using Project_61_GUI.MyModels;
using System;
using System.Collections.Generic;
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

namespace Project_61_GUI.MyControls
{
    /// <summary>
    /// Логика взаимодействия для ProgramControl.xaml
    /// </summary>
    public partial class ProgramControl : UserControl
    {
        AppDomain Domain = AppDomain.CurrentDomain;
        public MyProgram myProgram { get; set; }
        public ProgramControl(ref MyProgram myProgram)
        {
            this.myProgram = myProgram;
            InitializeComponent();
            this.DataContext = this;
            myProgram.PropertyChanged += MyProgram_PropertyChanged;
        }

        private void MyProgram_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TimeControl")
            {
                Domain.SetData("GUI TimeControl:" + myProgram.ProgramName, myProgram.TimeControl);
            }
        }
    }
}
