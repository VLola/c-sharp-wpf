﻿using System;
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

namespace Project_54.Controls
{
    /// <summary>
    /// Логика взаимодействия для CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public ImageSource img { get; set; }
        public string text { get; set; }
        public CategoryControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
