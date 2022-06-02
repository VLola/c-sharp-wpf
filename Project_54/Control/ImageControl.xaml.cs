using Project_54.Model;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_54.Control
{
    /// <summary>
    /// Логика взаимодействия для ImageControl.xaml
    /// </summary>
    public partial class ImageControl : UserControl
    {
        public Logo logo_model = new Logo();
        public ImageControl()
        {
            InitializeComponent();
            this.DataContext = logo_model;
            new Thread(() => { NewLogo(); }).Start();
        }
        private void NewLogo()
        {
            for (; ; )
            {
                Dispatcher.Invoke(new Action(() => {
                    if (logo_model.number_logo == 5) logo_model.number_logo = 0;
                    else logo_model.number_logo++;
                }));

                Thread.Sleep(5000);
            }
        }

        private void Left_Click(object sender, MouseButtonEventArgs e)
        {
            if (logo_model.number_logo != 0) logo_model.number_logo--;
        }
        private void Rigth_Click(object sender, MouseButtonEventArgs e)
        {
            if (logo_model.number_logo != 5) logo_model.number_logo++;
        }

        private void But_1_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 0;
        }
        private void But_2_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 1;
        }
        private void But_3_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 2;
        }
        private void But_4_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 3;
        }
        private void But_5_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 4;
        }
        private void But_6_Click(object sender, RoutedEventArgs e)
        {
            logo_model.number_logo = 5;
        }

    }
}
