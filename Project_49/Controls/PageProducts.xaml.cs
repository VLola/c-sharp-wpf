using Project_49.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Project_49.Controls
{
    /// <summary>
    /// Логика взаимодействия для PageProducts.xaml
    /// </summary>
    public partial class PageProducts : UserControl
    {
        public ModelProduct modelProduct = new ModelProduct();
        public List<Models.Product> list = new List<Models.Product>();
        public string search { get; set; }
        public int Minimum { get; set; } = 0;
        public int LowerValue { get; set; }
        public int UpperValue { get; set; }
        public int Maximum { get; set; } = 0;
        public string MinimumPrice { get; set; }
        public string MaximumPrice { get; set; }
        public bool OneSim { get; set; } = false;
        public bool TwoSim { get; set; } = false;
        public bool PacificBlue { get; set; } = false;
        public bool Silver { get; set; } = false;
        public bool Gold { get; set; } = false;
        public bool Graphite { get; set; } = false;
        public bool Ram128 { get; set; } = false;
        public bool Ram256 { get; set; } = false;
        public bool Ram512 { get; set; } = false;

        public PageProducts(string search)
        {
            InitializeComponent();
            this.search = search;
            SetProduct();
            this.DataContext = this;
        }

        private void SetProduct()
        {
            foreach (var it in modelProduct.Products)
            {
                if (CheckName(it))
                {
                    list.Add(it);
                    if (Minimum == 0) Minimum = it.Price;
                    if (Maximum == 0) Maximum = it.Price;
                    if (it.Price < Minimum) Minimum = it.Price;
                    if (it.Price > Maximum) Maximum = it.Price;
                }
            }
            LowerValue = Minimum;
            UpperValue = Maximum;
            MinimumPrice = MinPrice();
            MaximumPrice = MaxPrice();

            AddControl();
        }
        private bool CheckName(Models.Product product)
        {
            if (product.Name.Contains(search)) return true;
            else return false;
        }
        private bool CheckPrice(Models.Product product)
        {
            if (product.Price >= LowerValue && product.Price <= UpperValue) return true;
            else return false;
        }
        private bool CheckRam(Models.Product product)
        {
            if (!Ram128 && !Ram256 && !Ram512) return true;
            else if (Ram128 && product.Name.Contains("128GB")) return true;
            else if (Ram256 && product.Name.Contains("256GB")) return true;
            else if (Ram512 && product.Name.Contains("512GB")) return true;
            else return false;
        }
        private bool CheckSim(Models.Product product)
        {
            if (!OneSim && !TwoSim) return true;
            else if (OneSim && !product.Name.Contains("Dual SIM")) return true;
            else if (TwoSim && product.Name.Contains("Dual SIM")) return true;
            else return false;
        }
        private bool CheckColor(Models.Product product)
        {
            if (!PacificBlue && !Silver && !Gold && !Graphite) return true;
            else if(PacificBlue && product.Name.Contains("Pacific Blue")) return true;
            else if (Silver && product.Name.Contains("Silver")) return true;
            else if (Gold && product.Name.Contains("Gold")) return true;
            else if (Graphite && product.Name.Contains("Graphite")) return true;
            else return false;
        }

        private void AddControl()
        {
            int i = 0, j = 0;
            //list.Reverse();
            foreach (var it in list)
            {
                if (j == 0) products.RowDefinitions.Add(new RowDefinition());
                ProductControl Product = new ProductControl(it);
                Grid.SetColumn(Product, j);
                Grid.SetRow(Product, i);
                products.Children.Add(Product);
                j++;
                if (j == 5)
                {
                    j = 0;
                    i++;
                }
            }

            list.Clear();
        }
        private void ClearChildren()
        {
            products.Children.Clear();
            products.RowDefinitions.Clear();
        }
        private void MinMaxPrice()
        {

            ClearChildren();

            foreach (var it in modelProduct.Products)
            {
                if (CheckName(it) && CheckPrice(it) && CheckRam(it) && CheckSim(it) && CheckColor(it))
                {
                    list.Add(it);
                    if (Minimum == 0) Minimum = it.Price;
                    if (Maximum == 0) Maximum = it.Price;
                    if (it.Price < Minimum) Minimum = it.Price;
                    if (it.Price > Maximum) Maximum = it.Price;
                }
            }

            AddControl();
        }

        //Double slider

        private void UpperSlider_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinMaxPrice();
        }

        private void LowerSlider_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MinMaxPrice();
        }

        private void LowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpperSlider.Value = Math.Max(UpperSlider.Value, LowerSlider.Value);
        }

        private void UpperSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            LowerSlider.Value = Math.Min(UpperSlider.Value, LowerSlider.Value);
        }

        private void OneSim_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void TwoSim_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void PacificBlue_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void Silver_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void Gold_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void Graphite_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void Ram128_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void Ram256_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        private void Ram512_Click(object sender, RoutedEventArgs e)
        {
            MinMaxPrice();
        }
        public string MinPrice() { return Minimum.ToString() + " грн"; }
        public string MaxPrice() { return Maximum.ToString() + " грн"; }

    }
}
