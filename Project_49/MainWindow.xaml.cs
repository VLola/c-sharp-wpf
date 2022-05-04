using Project_49.Control;
using Project_49.Controls;
using Project_49.Models;
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
            PageCenter();
            SetCatalogProductListOne();
            SetCatalogProductListTwo();
            SetLatestProduct();
            SetStockProduct();
        }

        #region - Load Catalog product -
        private void SetCatalogProductListOne()
        {
            int count = 0;
            List<string> list_product = new List<string>();
            list_product.Add("iPhone 13 Pro");
            list_product.Add("Macbook Pro M1");
            list_product.Add("Used iPhone");
            list_product.Add("Apple Watch");
            foreach (var it in list_product)
            {
                Product product = new Product(it);
                Grid.SetColumn(product, count++);
                ProductListOne.Children.Add(product);
            }
        }
        private void SetCatalogProductListTwo()
        {
            int count = 0;
            List<string> list_product = new List<string>();
            list_product.Add("TV 4K");
            list_product.Add("Xiaomi");
            list_product.Add("Samsung");
            list_product.Add("Acoustics");
            foreach (var it in list_product)
            {
                Product product = new Product(it);
                Grid.SetColumn(product, count);
                ProductListTwo.Children.Add(product);
                count++;
            }
        }
        #endregion

        #region - Load Latest product -
        private void SetLatestProduct()
        {
            int i = 0, j = 0, k = 0;
            ModelProduct modelProduct = new ModelProduct();
            List<Models.Product> list = new List<Models.Product>();
            foreach (var it in modelProduct.Products) list.Add(it);
            list.Reverse();
            foreach (var it in list)
            {
                if (k < 6)
                {
                    if (j == 0) LatestProduct.ColumnDefinitions.Add(new ColumnDefinition());
                    LatestProductControl latestProduct = new LatestProductControl(it.Name, it.Image);
                    Grid.SetColumn(latestProduct, i);
                    Grid.SetRow(latestProduct, j++);
                    LatestProduct.Children.Add(latestProduct);
                    if (j == 2)
                    {
                        i++;
                        j = 0;
                    }
                    k++;
                }
            }
        }
        #endregion

        #region - Load Stock product -
        private void SetStockProduct()
        {
            int i = 0, j = 0, k = 0;
            ModelProduct modelProduct = new ModelProduct();
            List<Models.Product> list = new List<Models.Product>();
            foreach (var it in modelProduct.Products) {
                if (it.Latest) list.Add(it);
            }
            list.Reverse();
            foreach (var it in list)
            {
                if (k < 6)
                {
                    if (j == 0) StockProduct.ColumnDefinitions.Add(new ColumnDefinition());
                    LatestProductControl latestProduct = new LatestProductControl(it.Name, it.Image);
                    Grid.SetColumn(latestProduct, i);
                    Grid.SetRow(latestProduct, j++);
                    StockProduct.Children.Add(latestProduct);
                    if (j == 2)
                    {
                        i++;
                        j = 0;
                    }
                    k++;
                }
            }
        }
        #endregion

        #region - PageCenter -
        private void PageCenter()
        {
            ButtonLogin.Click += ButtonLogin_Click;
            ButtonRegestration.Click += ButtonRegestration_Click;
            ButtonRegistrationReturn.Click += ButtonRegistrationReturn_Click;
            ButtonMain.Click += ButtonMain_Click;
        }
        private void ButtonMain_Click(object sender, RoutedEventArgs e) { ChangeMain(MainBackground); }
        private void ButtonRegistrationReturn_Click(object sender, RoutedEventArgs e) { ChangeMain(Login); }
        private void ButtonRegestration_Click(object sender, RoutedEventArgs e) { ChangeMain(Registration); }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e) { ChangeMain(Login); }
        private void ChangeMain(Grid grid)
        {
            foreach(Grid it in Main.Children)
            {
                if (it == grid) it.Visibility = Visibility.Visible;
                else it.Visibility = Visibility.Hidden;
            }
        }
        #endregion

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

            apple.MouseEnter += Apple_MouseEnter1;
            smartphones.MouseEnter += Smartphones_MouseEnter1;
            laptops_and_pcs.MouseEnter += Laptops_and_pcs_MouseEnter;
            photo_audio_ps5.MouseEnter += Photo_audio_ps5_MouseEnter;
            tvs.MouseEnter += Tvs_MouseEnter;
            gadgets.MouseEnter += Gadgets_MouseEnter1;
            appliances.MouseEnter += Appliances_MouseEnter1;
            apple.MouseLeave += Apple_MouseLeave1;
            smartphones.MouseLeave += Smartphones_MouseLeave1;
            laptops_and_pcs.MouseLeave += Laptops_and_pcs_MouseLeave;
            photo_audio_ps5.MouseLeave += Photo_audio_ps5_MouseLeave;
            tvs.MouseLeave += Tvs_MouseLeave;
            gadgets.MouseLeave += Gadgets_MouseLeave1;
            appliances.MouseLeave += Appliances_MouseLeave1;
        }
        private void Appliances_MouseLeave(object sender, MouseEventArgs e) { appliances.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void Gadgets_MouseLeave(object sender, MouseEventArgs e) { gadgets.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void TVs_MouseLeave(object sender, MouseEventArgs e) { tvs.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void PhotoAudioPS5_MouseLeave(object sender, MouseEventArgs e) { photo_audio_ps5.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void LaptopsAndPCs_MouseLeave(object sender, MouseEventArgs e) { laptops_and_pcs.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void Smartphones_MouseLeave(object sender, MouseEventArgs e) { smartphones.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void Apple_MouseLeave(object sender, MouseEventArgs e) { apple.Visibility = Visibility.Hidden; yellow_main.Background = Brushes.White; }
        private void Appliances_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(appliances); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void Gadgets_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(gadgets); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void TVs_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(tvs); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void PhotoAudioPS5_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(photo_audio_ps5); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void LaptopsAndPCs_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(laptops_and_pcs); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void Smartphones_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(smartphones); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void Apple_MouseEnter(object sender, MouseEventArgs e) { VisibleCategory(apple); yellow_main.Background = new SolidColorBrush(Color.FromRgb(229, 229, 229)); }
        private void VisibleCategory(Grid grid)
        {
            foreach (Grid it in Catalog.Children)
            {
                if (it == grid) it.Visibility = Visibility.Visible;
                else it.Visibility = Visibility.Hidden;
            }
            foreach(ListView it in grid.Children)
            {
                List<string> list = new List<string>();
                it.ItemsSource = list;
                foreach (TopCatalog iterator in ConnectTopCatalog.GetTopCatalog())
                {
                    if (it.Name.ToString() == iterator.Catalog) list.Add(iterator.Resources);
                }
                it.Items.Refresh();
            }
        }

        private void Appliances_MouseLeave1(object sender, MouseEventArgs e) { appliances.Visibility = Visibility.Hidden; }
        private void Gadgets_MouseLeave1(object sender, MouseEventArgs e) { gadgets.Visibility = Visibility.Hidden; }
        private void Tvs_MouseLeave(object sender, MouseEventArgs e) { tvs.Visibility = Visibility.Hidden; }
        private void Photo_audio_ps5_MouseLeave(object sender, MouseEventArgs e) { photo_audio_ps5.Visibility = Visibility.Hidden; }
        private void Laptops_and_pcs_MouseLeave(object sender, MouseEventArgs e) { laptops_and_pcs.Visibility = Visibility.Hidden; }
        private void Smartphones_MouseLeave1(object sender, MouseEventArgs e) { smartphones.Visibility = Visibility.Hidden; }
        private void Apple_MouseLeave1(object sender, MouseEventArgs e) { apple.Visibility = Visibility.Hidden; }
        private void Appliances_MouseEnter1(object sender, MouseEventArgs e) { appliances.Visibility = Visibility.Visible; }
        private void Gadgets_MouseEnter1(object sender, MouseEventArgs e) { gadgets.Visibility = Visibility.Visible; }
        private void Tvs_MouseEnter(object sender, MouseEventArgs e) { tvs.Visibility = Visibility.Visible; }
        private void Photo_audio_ps5_MouseEnter(object sender, MouseEventArgs e) { photo_audio_ps5.Visibility = Visibility.Visible; }
        private void Laptops_and_pcs_MouseEnter(object sender, MouseEventArgs e) { laptops_and_pcs.Visibility = Visibility.Visible; }
        private void Smartphones_MouseEnter1(object sender, MouseEventArgs e) { smartphones.Visibility = Visibility.Visible; }
        private void Apple_MouseEnter1(object sender, MouseEventArgs e) { apple.Visibility = Visibility.Visible; }
        #endregion
    }
}
