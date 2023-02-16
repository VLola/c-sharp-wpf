using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Project_79.Models;
using System.Linq;
using System.IO;
using Project_79.Command;
using Microsoft.Win32;
using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace Project_79.ViewModels
{
    internal class MainViewModel
    {
        string pathDownload = Directory.GetCurrentDirectory() + "/download/";
        private RelayCommand? _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(obj => { DeleteAsync(); })); }
        }
        private RelayCommand? _addCommand;
        public RelayCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(obj => { Add(); })); }
        }
        private RelayCommand? _downloadCommand;
        public RelayCommand DownloadCommand
        {
            get { return _downloadCommand ?? (_downloadCommand = new RelayCommand(obj => { Download(); })); }
        }
        private RelayCommand? _saveCommand;
        public RelayCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(obj => { Save(); })); }
        }
        string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=valik;AccountKey=KMcTOADr+UnVoRIBTMkQNGj1DkmzIJW1f2CMbiaIQN9W8jhabvoYNIrvxtsa1OVLkwi0BItQvElx+AStWdlqTQ==;EndpointSuffix=core.windows.net";
        string BlobStorageContainerName = "blob";
        public Main Main { get; set; } = new();
        CloudBlobClient backupBlobClient;
        CloudBlobContainer backupContainer;
        BlobContainerClient container;
        public MainViewModel() {
            if(!Directory.Exists(pathDownload)) Directory.CreateDirectory(pathDownload);
            backupBlobClient = CloudStorageAccount.Parse(BlobStorageConnectionString).CreateCloudBlobClient();
            backupContainer = backupBlobClient.GetContainerReference(BlobStorageContainerName);

            BlobServiceClient blobServiceClient = new BlobServiceClient(BlobStorageConnectionString);
            BlobContainerClient cont = blobServiceClient.GetBlobContainerClient("fileupload");
            container = new BlobContainerClient(BlobStorageConnectionString, BlobStorageContainerName);

            LoadFilesAsync();
            Main.PropertyChanged += Main_PropertyChanged;
        }

        private void Main_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedBlob")
            {
                Main.Text = Main.SelectedBlob.DownloadText();
            }
        }

        private async void LoadFilesAsync()
        {
            await Task.Run(() =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Main.Blobs.Clear();
                });

                var listName = backupContainer.ListBlobs().OfType<CloudBlockBlob>().Where(b => b.BlobType == Microsoft.Azure.Storage.Blob.BlobType.BlockBlob).ToList();

                App.Current.Dispatcher.Invoke(() =>
                {
                    foreach (var item in listName)
                    {
                        Main.Blobs.Add(new BlobViewModel(item));
                    }
                });
            });
        }
        private async void DeleteAsync()
        {
            await Task.Run(async () => {
                if (Main.SelectedBlob != null)
                {
                    await Main.SelectedBlob.DeleteAsync();
                    LoadFilesAsync();
                }
            });
        }
        private async void Add()
        {
            await Task.Run(async () =>
            {
                FileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                if (fileDialog.FileName != "")
                {

                    string path = fileDialog.FileName;
                    string name = Path.GetFileName(path);
                    var blob = container.GetBlobClient(name);

                    var stream = File.OpenRead(path);
                    await blob.UploadAsync(stream, overwrite: true);

                    LoadFilesAsync();
                }
            });

        }
        private void Download()
        {
            if (Main.SelectedBlob != null) Main.SelectedBlob.DownloadToFileAsync();
        }
        private void Save()
        {
            if (Main.SelectedBlob != null) Main.SelectedBlob.UploadText(Main.Text);
        }
    }
}
