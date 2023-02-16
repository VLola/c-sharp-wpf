using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Project_79.Models;
using System.Linq;
using System.Windows;
using System.IO;
using Project_79.Command;
using Microsoft.Win32;
using System.ComponentModel;
using System;
using Azure.Storage.Blobs;
using System.Threading.Tasks;

namespace Project_79.ViewModels
{
    internal class MainViewModel
    {
        private RelayCommand? _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(obj => { Delete(); })); }
        }
        private RelayCommand? _addCommand;
        public RelayCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(obj => { Add(); })); }
        }
        string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=valik;AccountKey=KMcTOADr+UnVoRIBTMkQNGj1DkmzIJW1f2CMbiaIQN9W8jhabvoYNIrvxtsa1OVLkwi0BItQvElx+AStWdlqTQ==;EndpointSuffix=core.windows.net";
        string BlobStorageContainerName = "blob";
        public Main Main { get; set; } = new();
        CloudBlobClient backupBlobClient;
        CloudBlobContainer backupContainer;
        BlobContainerClient container;
        public MainViewModel() {
            backupBlobClient = CloudStorageAccount.Parse(BlobStorageConnectionString).CreateCloudBlobClient();
            backupContainer = backupBlobClient.GetContainerReference(BlobStorageContainerName);

            BlobServiceClient blobServiceClient = new BlobServiceClient(BlobStorageConnectionString);
            BlobContainerClient cont = blobServiceClient.GetBlobContainerClient("fileupload");
            container = new BlobContainerClient(BlobStorageConnectionString, BlobStorageContainerName);

            LoadFiles();
            Main.PropertyChanged += Main_PropertyChanged;
        }

        private void Main_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedBlob")
            {
                if (Path.GetExtension(Main.SelectedBlob.CloudBlockBlob.Name) == ".txt")
                {
                    MessageBox.Show(Main.SelectedBlob.CloudBlockBlob.Name);
                }
            }
        }

        private void LoadFiles()
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
            
        }
        private void Delete()
        {
            if (Main.SelectedBlob != null)
            {
                Main.SelectedBlob.Delete();
                LoadFiles();
            }
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

                    LoadFiles();
                }
            });
            
        }
    }
}
