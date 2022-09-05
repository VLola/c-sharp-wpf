using HtmlAgilityPack;
using Project_65.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
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

            var imgSrc = htmlDocument.DocumentNode.SelectNodes("//div[@class='imgBlock']//div[@class='img']//img");
            LoadingImage("https:" + imgSrc[0].Attributes[2].Value);

        }
        private void LoadingImage(string html)
        {
            var image = new BitmapImage();
            int BytesToRead = 100;

            WebRequest request = WebRequest.Create(new Uri(html, UriKind.Absolute));
            request.Timeout = -1;
            WebResponse response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            BinaryReader reader = new BinaryReader(responseStream);
            MemoryStream memoryStream = new MemoryStream();

            byte[] bytebuffer = new byte[BytesToRead];
            int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

            while (bytesRead > 0)
            {
                memoryStream.Write(bytebuffer, 0, bytesRead);
                bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
            }

            image.BeginInit();
            memoryStream.Seek(0, SeekOrigin.Begin);

            image.StreamSource = memoryStream;
            image.EndInit();
            SinopticModel.Logo = image;
        }
    }
}
