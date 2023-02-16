using Project_79.ViewModels;
using System.Collections.ObjectModel;

namespace Project_79.Models
{
    public class Main : Changed
    {
        public ObservableCollection<BlobViewModel> Blobs { get; set; } = new();
        private BlobViewModel _selectedBlob;
        public BlobViewModel SelectedBlob { 
            get { return _selectedBlob; }
            set
            {
                _selectedBlob = value;
                OnPropertyChanged("SelectedBlob");
            }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private string _search = "";
        public string Search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged("Search");
            }
        }
    }
}
