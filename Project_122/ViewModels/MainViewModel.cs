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
using System.Net;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private BitmapImage ByteToImage(byte[] array)
        {
            using (var ms = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
        public void circle(double x, double y, int width, int height, System.Windows.Controls.Image img)
        {
            try
            {

                Rect rect = new Rect(0, 0, img.Width, img.Height);
                DrawingVisual visual = new DrawingVisual();

                using (DrawingContext dc = visual.RenderOpen())
                {
                    dc.DrawImage(img.Source, rect);
                    dc.DrawEllipse(null, new Pen(Brushes.Red, 6), new System.Windows.Point(x + width / 2, y + height / 2), width / 2, height / 2);
                }

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
                rtb.Render(visual);
                img.Source = rtb;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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
                using (var client = new WebClient())
                {
                    BitmapImage bitmap = ByteToImage(client.DownloadData("https://" + bucketName + ".s3.us-west-2.amazonaws.com/" + Main.SelectedS3Object.Key));
                    System.Windows.Controls.Image img = new();
                    img.Source = bitmap;
                    img.Width = bitmap.Width;
                    img.Height = bitmap.Height;
                    Main.Image = img;

                    foreach (var item in Main.FaceDetails)
                    {
                        circle((double)(item.BoundingBox.Left * img.Width), (double)(item.BoundingBox.Top * img.Height), (int)(item.BoundingBox.Width * img.Width), (int)(item.BoundingBox.Height * img.Height), img);
                    }
                }
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
