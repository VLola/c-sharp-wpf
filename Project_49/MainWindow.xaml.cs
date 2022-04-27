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
        public MainWindow()
        {
            InitializeComponent();
            Categorys();
            apple.Items.Add("valik1");
            smartphones.Items.Add("valik2");
            laptops_and_pcs.Items.Add("valik3");
            photo_audio_ps5.Items.Add("valik4");
            tvs.Items.Add("valik5");
            gadgets.Items.Add("valik6");
            appliances.Items.Add("valik7");
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
        private void VisibleCategory(ListBox list)
        {
            foreach (ListBox it in Catalog.Children)
            {
                if (it == list) it.Visibility = Visibility.Visible;
                else it.Visibility = Visibility.Hidden;
            }
        }
        #endregion
    }
}
