using Microsoft.Azure.Storage.Blob;
using Project_79.Models;
using System;

namespace Project_79.ViewModels
{
    public class BlobViewModel
    {
        public Blob Blob { get; set; } = new();
        public CloudBlockBlob CloudBlockBlob { get; set; }
        public BlobViewModel(CloudBlockBlob cloudBlockBlob)
        {
            CloudBlockBlob = cloudBlockBlob;
            Blob.DateTime = CloudBlockBlob.Properties.LastModified.Value.DateTime;
        }
    }
}
