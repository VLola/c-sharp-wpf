using HtmlAgilityPack;
using Project_65.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Windows;

namespace Project_65.ModelView
{
    public class SinopticModelView
    {
        public ObservableCollection<RowModel> Values { get; set; } = new();
        public SinopticModel SinopticModel { get; set; } = new SinopticModel();
        public SinopticModelView()
        {
            Load();
        }
        private void Load()
        {
            WebClient webClient = new();
            string html = webClient.DownloadString("https://sinoptik.ua/%D0%BF%D0%BE%D0%B3%D0%BE%D0%B4%D0%B0-%D0%B4%D0%BD%D0%B5%D0%BF%D1%80-303007131");

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(html);

            int i = 0;
            foreach (HtmlNode node in htmlDocument.DocumentNode.SelectNodes("//tbody//tr"))
            {
                if(i != 1)Values.Add(new RowModel(node.InnerText));
                i++;
            }
        }
    }
}
