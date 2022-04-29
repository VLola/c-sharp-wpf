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

namespace Project_49
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> top_Apple = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            Categorys();
            
            top_Apple.Add("Apple");
            top_Apple.Add("iPhone 13");
            top_Apple.Add("iPhone 13 Pro");
            top_Apple.Add("Apple");
            top_Apple.Add("Apple");
            top_Apple.Add("Apple");
            top_Apple.Add("Apple");
            top_Apple.Add("Apple");
            top_Apple.Add("Apple");
            top_Apple.Add("Apple");

            top_iPhone.ItemsSource = top_Apple;
            top_iMac.ItemsSource = top_Apple;
            apple.MouseEnter += Apple_MouseEnter1;
            apple.MouseLeave += Apple_MouseLeave1;
        }

        private void Apple_MouseLeave1(object sender, MouseEventArgs e)
        {
            apple.Visibility = Visibility.Hidden;
        }

        private void Apple_MouseEnter1(object sender, MouseEventArgs e)
        {
            apple.Visibility = Visibility.Visible;
        }
        #region - Categorys -
        private void Categorys()
        {
            Apple.MouseEnter += Apple_MouseEnter;
            Smartphones.MouseEnter += Smartphones_MouseEnter;
            LaptopsAndPCs.MouseEnter += LaptopsAndPCs_MouseEnter;
            PhotoAudioPS5.MouseEnter += PhotoAudioPS5_MouseEnter;
            TVs.MouseEnter += TVs_MouseEnter;
            Gadgets.MouseEnter += Gadgets_MouseEnter;
            Appliances.MouseEnter += Appliances_MouseEnter;
            Apple.MouseLeave += Apple_MouseLeave;
            Smartphones.MouseLeave += Smartphones_MouseLeave;
            LaptopsAndPCs.MouseLeave += LaptopsAndPCs_MouseLeave;
            PhotoAudioPS5.MouseLeave += PhotoAudioPS5_MouseLeave;
            TVs.MouseLeave += TVs_MouseLeave;
            Gadgets.MouseLeave += Gadgets_MouseLeave;
            Appliances.MouseLeave += Appliances_MouseLeave;
        }
        private void Appliances_MouseLeave(object sender, MouseEventArgs e) { appliances.Visibility = Visibility.Hidden; }
        private void Gadgets_MouseLeave(object sender, MouseEventArgs e) { gadgets.Visibility = Visibility.Hidden; }
        private void TVs_MouseLeave(object sender, MouseEventArgs e) { tvs.Visibility = Visibility.Hidden; }
        private void PhotoAudioPS5_MouseLeave(object sender, MouseEventArgs e) { photo_audio_ps5.Visibility = Visibility.Hidden; }
        private void LaptopsAndPCs_MouseLeave(object sender, MouseEventArgs e) { laptops_and_pcs.Visibility = Visibility.Hidden; }
        private void Smartphones_MouseLeave(object sender, MouseEventArgs e) { smartphones.Visibility = Visibility.Hidden; }
        private void Apple_MouseLeave(object sender, MouseEventArgs e) { apple.Visibility = Visibility.Hidden; }
        private void Appliances_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(appliances); }
        private void Gadgets_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(gadgets); }
        private void TVs_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(tvs); }
        private void PhotoAudioPS5_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(photo_audio_ps5); }
        private void LaptopsAndPCs_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(laptops_and_pcs); }
        private void Smartphones_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(smartphones); }
        private void Apple_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(apple); }
        private void VisibleCategory(Grid grid)
        {
            foreach (Grid it in Catalog.Children)
            {
                if (it == grid) it.Visibility = Visibility.Visible;
                else it.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}
