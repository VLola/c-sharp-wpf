using System.Collections.ObjectModel;
using System.IO;

namespace Project_79.Models
{
    public class Main : Changed
    {
        public ObservableCollection<FileBlob> Files { get; set; } = new();
        private FileBlob _selectedFileBlob;
        public FileBlob SelectedFileBlob { 
            get { return _selectedFileBlob; }
            set
            {
                _selectedFileBlob = value;
                OnPropertyChanged("SelectedFileBlob");
            }
        }
    }
}
