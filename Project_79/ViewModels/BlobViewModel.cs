using Microsoft.Azure.Storage.Blob;
using Project_79.Models;
using System.IO;
using System.Threading.Tasks;

namespace Project_79.ViewModels
{
    public class BlobViewModel
    {
        string pathDownload = Directory.GetCurrentDirectory() + "/download/";
        public Blob Blob { get; set; } = new();
        private CloudBlockBlob CloudBlockBlob { get; set; }
        public BlobViewModel(CloudBlockBlob cloudBlockBlob)
        {
            CloudBlockBlob = cloudBlockBlob;
            Blob.Name = cloudBlockBlob.Name;
            Blob.DateTime = cloudBlockBlob.Properties.LastModified.Value.DateTime;
        }
        public async Task DeleteAsync() {
            await CloudBlockBlob.DeleteAsync();
        }
        public string DownloadText()
        {
            if (Path.GetExtension(Blob.Name) == ".txt")
            {
                return CloudBlockBlob.DownloadText();
            }
            else
            {
                return "";
            }
        }
        public async void DownloadToFileAsync()
        {
            await Task.Run(() => {

                CloudBlockBlob.DownloadToFile(pathDownload + Blob.Name, FileMode.Create);
            });
        }
        public void UploadText(string text)
        {
            if (Path.GetExtension(Blob.Name) == ".txt")
            {
                CloudBlockBlob.UploadText(text);
            }
        }
    }
}
