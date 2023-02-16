using Microsoft.Azure.Storage.Blob;
using System;

namespace Project_79.ViewModels
{
    public class BlobViewModel
    {
        public CloudBlockBlob CloudBlockBlob { get; set; }
        public DateTime DateTime { get; set; }
        public BlobViewModel(CloudBlockBlob cloudBlockBlob)
        {
            CloudBlockBlob = cloudBlockBlob;
            DateTime = CloudBlockBlob.Properties.LastModified.Value.DateTime;
        }
    }
}
