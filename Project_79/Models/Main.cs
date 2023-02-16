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
    }
}
