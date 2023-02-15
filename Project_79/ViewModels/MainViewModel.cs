using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Azure.Storage.Blobs;
using Project_79.Models;
using System.Linq;
using System.Windows;
using Azure.Storage.Blobs.Models;

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
                if(Main.SelectedFileBlob.Extension == "txt")
                {
                    MessageBox.Show(Main.SelectedFileBlob.Name);
                }
            }
        }

        private void LoadFiles()
        {
            var listName = backupContainer.ListBlobs().OfType<CloudBlockBlob>().Where(b => b.BlobType == Microsoft.Azure.Storage.Blob.BlobType.BlockBlob).ToList();

            foreach (var item in listName)
            {
                Main.Files.Add(new FileBlob(item.Name, item.Properties.LastModified.Value.DateTime.ToString()));
            }
        }
    }
}
