using Project_49.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Project_49.Controls
{
    /// <summary>
    /// Логика взаимодействия для PageMain.xaml
    /// </summary>
    public partial class PageMain : UserControl
    {
        public PageMain()
        {
            InitializeComponent();

            SetCatalogProductListOne();
            SetCatalogProductListTwo();
            SetLatestProduct();
            SetDiscountedProduct();
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
            foreach (var it in modelProduct.Products) if(it.Latest == true)list.Add(it);
            list.Reverse();
            foreach (var it in list)
            {
                if (k < 6)
                {
                    if (j == 0) LatestProduct.ColumnDefinitions.Add(new ColumnDefinition());
                    LatestProductControl latestProduct = new LatestProductControl(it);
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

        #region - Load Discounted product -
        private void SetDiscountedProduct()
        {
            int i = 0, j = 0, k = 0;
            ModelProduct modelProduct = new ModelProduct();
            List<Models.Product> list = new List<Models.Product>();
            foreach (var it in modelProduct.Products)
            {
                if (it.Discount > 0) list.Add(it);
            }
            list.Reverse();
            foreach (var it in list)
            {
                if (k < 6)
                {
                    if (j == 0) DiscountedProduct.ColumnDefinitions.Add(new ColumnDefinition());
                    DiscountedProductControl discountProduct = new DiscountedProductControl(it);
                    Grid.SetColumn(discountProduct, i);
                    Grid.SetRow(discountProduct, j++);
                    DiscountedProduct.Children.Add(discountProduct);
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

    }
}
