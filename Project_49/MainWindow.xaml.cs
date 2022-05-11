using Project_49.Control;
using Project_49.Controls;
using Project_49.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Project_49
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool login = false;
        public PageUserInfo page_user_info;
        public PageMain page_main;
        public PageLogin page_login;
        public PageRegistration page_registration;
        public PageProducts page_products;
        public MainWindow()
        {
            InitializeComponent();
            Categorys();
            PageCenter();
        }
        
        #region - PageCenter -
        private void PageCenter()
        {
            page_main = new PageMain();
            NewLogin();
            NewRegistration();
            FullMain.Children.Add(page_main);
            
            
            ButtonLogin.Click += ButtonLogin_Click;
            ButtonMain.Click += ButtonMain_Click;
            foreach (Product it in page_main.ProductListOne.Children)
            {
                it.PreviewMouseLeftButtonDown += It_PreviewMouseLeftButtonDown;
            }
            foreach (Product it in page_main.ProductListTwo.Children)
            {
                it.PreviewMouseLeftButtonDown += It_PreviewMouseLeftButtonDown;
            }
        }
        private void NewLogin()
        {
            login = false;
            page_login = new PageLogin();
            page_login.ButtonRegestration.Click += ButtonRegestration_Click;
            page_login.button_login.Click += Button_login_Click;
        }
        private void NewRegistration()
        {
            page_registration = new PageRegistration();
            page_registration.ButtonRegistrationReturn.Click += ButtonRegistrationReturn_Click;
            page_registration.ButtonRegistration.Click += ButtonRegistration_Click;
        }
        private void It_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as Product).name;
            if (item != null)
            {
                page_products = new PageProducts(item);
                FullMain.Children.Clear();
                FullMain.Children.Add(page_products);
            }
        }

        private void ButtonAccountReturn_Click(object sender, RoutedEventArgs e)
        {
            NewLogin();
            FullMain.Children.Clear();
            FullMain.Children.Add(page_login);
        }
        
        private void Button_login_Click(object sender, RoutedEventArgs e)
        {
            if(page_login.email.Text != "" && page_login.parol.Password != "")
            {
                if (page_login.administrator.IsChecked == true)
                {
                    if (Connect.LoginAdmin(page_login.email.Text, page_login.parol.Password))
                    {
                        login = true;
                        page_user_info = new PageUserInfo(true, page_login.email.Text);
                        page_user_info.ButtonAccountReturn.Click += ButtonAccountReturn_Click;
                        FullMain.Children.Clear();
                        FullMain.Children.Add(page_user_info);
                    }
                    else login = false;
                }
                else
                {
                    if (Connect.LoginUser(page_login.email.Text, page_login.parol.Password))
                    {
                        login = true;
                        page_user_info = new PageUserInfo(false, page_login.email.Text);
                        page_user_info.ButtonAccountReturn.Click += ButtonAccountReturn_Click;
                        FullMain.Children.Clear();
                        FullMain.Children.Add(page_user_info);
                    }
                    else login = false;
                }
            }
               
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (page_registration.parol_registration.Password == page_registration.parol_verification_registration.Password)
                if (page_registration.email_registration.Text != "" && page_registration.parol_registration.Password != "")
                    if (page_registration.administrator.IsChecked == true) Connect.RegistrationAdmin(page_registration.email_registration.Text, page_registration.parol_registration.Password);
                    else Connect.RegistrationUser(page_registration.email_registration.Text, page_registration.parol_registration.Password);
        }

        private void ButtonMain_Click(object sender, RoutedEventArgs e) {
            FullMain.Children.Clear();
            FullMain.Children.Add(page_main); 
        }
        private void ButtonLogin_Click(object sender, RoutedEventArgs e) {
            FullMain.Children.Clear();
            if(login) FullMain.Children.Add(page_user_info);
            else FullMain.Children.Add(page_login);
        }
        private void ButtonRegestration_Click(object sender, RoutedEventArgs e)
        {
            FullMain.Children.Clear();
            FullMain.Children.Add(page_registration);
        }
        private void ButtonRegistrationReturn_Click(object sender, RoutedEventArgs e) {
            FullMain.Children.Clear();
            FullMain.Children.Add(page_login);
        }
        #endregion

        #region - Categorys -
        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                page_products = new PageProducts(item.ToString());
                FullMain.Children.Clear();
                FullMain.Children.Add(page_products);
            }
        }
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
