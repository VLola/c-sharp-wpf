using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Win32;
using Project_122.Command;
using Project_122.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_122.ViewModels
{
    public class MainViewModel
    {
        string bucketName = "valikbucket";
        IAmazonS3 client = new AmazonS3Client("AKIA6PVL36AGMXQV32FU", "TvPsi8ZFioNPgWtKLfA5A5rOQU9KoHY9Fva5aK9n", RegionEndpoint.USWest2); 
        AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient("AKIA6PVL36AGMXQV32FU", "TvPsi8ZFioNPgWtKLfA5A5rOQU9KoHY9Fva5aK9n", Amazon.RegionEndpoint.USWest2);
        public Main Main { get; set; } = new();

        private RelayCommand? _uploadFileCommand;
        public RelayCommand UploadFileCommand
        {
            get { return _uploadFileCommand ?? (_uploadFileCommand = new RelayCommand(obj => { UploadFile(); })); }
        }
        private RelayCommand? _deleteFileCommand;
        public RelayCommand DeleteFileCommand
        {
            get { return _deleteFileCommand ?? (_deleteFileCommand = new RelayCommand(obj => { DeleteFile(); })); }
        }
        private RelayCommand? _detectedFaceCommand;
        public RelayCommand DetectedFaceCommand
        {
            get { return _detectedFaceCommand ?? (_detectedFaceCommand = new RelayCommand(obj => { DetectedFace(); })); }
        }
        public MainViewModel()
        {
            Show();
        }
        private void UploadFile()
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog().Value)
                {
                    var putRequest = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = Path.GetFileName(openFileDialog.FileName),
                        FilePath = openFileDialog.FileName,
                        ContentType = "text/plain"
                    };

                    putRequest.Metadata.Add("x-amz-meta-title", "someTitle");
                    PutObjectResponse response = client.PutObjectAsync(putRequest).Result;
                    Show();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void DetectedFace()
        {
            try
            {
                DetectFacesRequest detectlabelsRequest = new DetectFacesRequest()
                {
                    Image = new Image()
                    {
                        S3Object = new Amazon.Rekognition.Model.S3Object()
                        { Name = Main.SelectedS3Object.Key, Bucket = bucketName },
                    },
                    Attributes = new List<String>() { "ALL" }
                };
                DetectFacesResponse detectLabelsResponse = await rekognitionClient.DetectFacesAsync(detectlabelsRequest);
                Main.FaceDetails = detectLabelsResponse.FaceDetails;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void DeleteFile()
        {
            try
            {
                await client.DeleteObjectAsync(bucketName, Main.SelectedS3Object.Key);
                Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private async void Show()
        {
            try
            {
                Main.ListObjectsResponse = await client.ListObjectsAsync(bucketName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
