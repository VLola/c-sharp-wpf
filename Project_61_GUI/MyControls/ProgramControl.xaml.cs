using Project_61_GUI.MyModels;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Project_61_GUI.MyControls
{
    public partial class ProgramControl : UserControl
    {
        AppDomain Domain = AppDomain.CurrentDomain;
        public MyProgram myProgram { get; set; }
        public ObservableCollection<double> WorkingTime { get; set; } = new ObservableCollection<double>();
        public ProgramControl(ref MyProgram myProgram)
        {
            this.myProgram = myProgram;
            InitializeComponent();
            this.DataContext = this;
            WorkingTime.Add(0);
            WorkingTime.Add(5);
            WorkingTime.Add(10);
            WorkingTime.Add(15);
            WorkingTime.Add(30);
            WorkingTime.Add(45);
            WorkingTime.Add(60);
            WorkingTime.Add(120);
            WorkingTime.Add(180);
            WorkingTime.Add(360);
            WorkingTime.Add(720);
            myProgram.PropertyChanged += MyProgram_PropertyChanged;
        }

        private void MyProgram_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "isParentalControl") Domain.SetData("isParentalControl:" + myProgram.ProgramName, myProgram.isParentalControl);
            if (e.PropertyName == "SelectedWorkingTime") Domain.SetData("SelectedWorkingTime:" + myProgram.ProgramName, myProgram.SelectedWorkingTime);
        }
    }
}
