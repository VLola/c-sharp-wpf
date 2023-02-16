using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Project_79.Models;
using System.Linq;
using System.Windows;
using System.IO;

namespace Project_79.ViewModels
{
    internal class MainViewModel
    {
        string BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=valik;AccountKey=KMcTOADr+UnVoRIBTMkQNGj1DkmzIJW1f2CMbiaIQN9W8jhabvoYNIrvxtsa1OVLkwi0BItQvElx+AStWdlqTQ==;EndpointSuffix=core.windows.net";
        string BlobStorageContainerName = "blob";
        public Main Main { get; set; } = new();
        CloudBlobClient backupBlobClient;
        CloudBlobContainer backupContainer;
        public MainViewModel() {
            backupBlobClient = CloudStorageAccount.Parse(BlobStorageConnectionString).CreateCloudBlobClient();
            backupContainer = backupBlobClient.GetContainerReference(BlobStorageContainerName);

            //BlobServiceClient blobServiceClient = new BlobServiceClient(BlobStorageConnectionString);
            //BlobContainerClient cont = blobServiceClient.GetBlobContainerClient("fileupload");
            //var container = new BlobContainerClient(BlobStorageConnectionString, BlobStorageContainerName);
            LoadFiles();
            Main.PropertyChanged += Main_PropertyChanged;
        }

        private void Main_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "SelectedFileBlob")
            {
                if (Path.GetExtension(Main.SelectedBlob.CloudBlockBlob.Name) == "txt")
                {
                    MessageBox.Show(Main.SelectedBlob.CloudBlockBlob.Name);
                }
            }
        }

        private void LoadFiles()
        {
            var listName = backupContainer.ListBlobs().OfType<CloudBlockBlob>().Where(b => b.BlobType == Microsoft.Azure.Storage.Blob.BlobType.BlockBlob).ToList();

            foreach (var item in listName)
            {
                Main.Blobs.Add(new BlobViewModel(item));
            }
        }
    }
}
