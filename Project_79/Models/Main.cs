namespace Project_79.Models
{
    public class Main : Changed
    {
        private string _text = "valik";
        public string Text { 
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
