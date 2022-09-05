using System.Text.RegularExpressions;
using System.Windows;

namespace Project_65.Model
{
    public class RowModel
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        public string Value5 { get; set; }
        public string Value6 { get; set; }
        public string Value7 { get; set; }
        public string Value8 { get; set; }
        public RowModel() { }
        public RowModel(string text)
        {
            if (text.Contains("&deg;")) text = text.Replace("&deg;", "°");
            if (text.Contains(":")) text = text.Replace(" :", ":");
            text = text.Replace("   ", " ");
            text = text.Replace("  ", " ");
            text = text.Replace(" ", "_");
            var matchCollection = Regex.Match(text, "_(.*)_(.*)_(.*)_(.*)_(.*)_(.*)_(.*)_(.*)_");
            Value1 = matchCollection.Groups[1].Value;
            Value2 = matchCollection.Groups[2].Value;
            Value3 = matchCollection.Groups[3].Value;
            Value4 = matchCollection.Groups[4].Value;
            Value5 = matchCollection.Groups[5].Value;
            Value6 = matchCollection.Groups[6].Value;
            Value7 = matchCollection.Groups[7].Value;
            Value8 = matchCollection.Groups[8].Value;
        }
    }
}
