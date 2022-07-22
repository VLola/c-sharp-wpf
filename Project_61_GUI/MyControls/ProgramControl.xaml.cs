using Project_61_GUI.MyModels;
using System;
using System.Windows.Controls;

namespace Project_61_GUI.MyControls
{
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
