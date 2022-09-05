using HtmlAgilityPack;
using Project_65.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_65.ViewModel
{
    public class SinopticViewModel
    {
        public ObservableCollection<RowModel> Values { get; set; } = new();
        public SinopticModel SinopticModel { get; set; } = new SinopticModel();
        public SinopticViewModel()
        {
            Load();
        }
        private void Load()
        {

            WebClient webClient = new();
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(webClient.DownloadString("https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%B4%D0%BD%D0%B5%D0%BF%D1%80-303007131"));

            int i = 0;
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//tbody//tr"))
            {
                if (i != 1) Values.Add(new RowModel(node.InnerText));
                i++;
            }

            var temp = htmlDocument.DocumentNode.SelectNodes("//p[@class='today-temp']");
            int temperature = Int32.Parse(temp[0].InnerText.Replace("&deg;C", ""));
            SinopticModel.TemperatureView = temperature + 50;
            if (temperature >= 0)SinopticModel.Temperature = $"+{temperature}°C";
            else SinopticModel.Temperature = $"-{temperature}°C";
            var imgLogo = htmlDocument.DocumentNode.SelectNodes("//div[@class='imgBlock']//div[@class='img']//img");
            SinopticModel.Logo = ByteToImage(webClient.DownloadData("https:" + imgLogo[0].Attributes[2].Value));
            
        }
        private BitmapImage ByteToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
