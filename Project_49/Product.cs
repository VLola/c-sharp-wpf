using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_49
{
    public class Product : Grid
    {
        public Border border;
        public Image image;
        public string name;
        public Label label;
        public Grid grid;
        public Product(string name)
        {
            this.name = name;
            grid = new Grid();
            Margin = new Thickness(5, 0, 5, 0);
            ClipToBounds = true;
            RowProduct();
            LabelProduct();
            ImageProduct();
            BorderProduct();
        }
        public void BorderProduct()
        {
            border = new Border();
            border.Background = new SolidColorBrush(Color.FromRgb(36, 36, 46));
            border.CornerRadius = new CornerRadius(5);
            border.BorderThickness = new Thickness(0);
            border.Child = grid;
            Children.Add(border);

            grid.MouseEnter += Product_MouseEnter;
            grid.MouseLeave += Product_MouseLeave;
        }

        private void Product_MouseLeave(object sender, MouseEventArgs e)
        {
            border.Background = new SolidColorBrush(Color.FromRgb(36, 36, 46));
            label.Foreground = Brushes.White;
            image.Margin = new Thickness(0);
        }

        private void Product_MouseEnter(object sender, MouseEventArgs e)
        {
            border.Background = new SolidColorBrush(Color.FromRgb(255, 209, 0));
            label.Foreground = Brushes.Black;
            image.Margin = new Thickness(0, 20, 0, -20);
        }

        private void ImageProduct()
        {
            image = new Image();
            image.Source = new BitmapImage(new Uri("pack://application:,,,/Project_49;component/Resources/Categorys/" + name + ".png"));
            SetRow(image, 0);
            SetRowSpan(image, 2);
            grid.Children.Add(image);
        }
        public void LabelProduct()
        {
            label = new Label();
            label.Content = name;
            label.VerticalAlignment = VerticalAlignment.Center;
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.Foreground = Brushes.White;
            label.FontSize = 24;
            SetRow(label, 0);
            grid.Children.Add(label);
        }
        public void RowProduct()
        {
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
        }
    }
}
