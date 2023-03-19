using Amazon.Rekognition.Model;
using Amazon.S3.Model;
using System.Collections.Generic;
using System.Windows;

namespace Project_122.Models
{
    public class Main : Changed
    {
        private ListObjectsResponse _listObjectsResponse;
        public ListObjectsResponse ListObjectsResponse
        {
            get { return _listObjectsResponse; }
            set
            {
                _listObjectsResponse = value;
                OnPropertyChanged("ListObjectsResponse");
            }
        }
        private List<FaceDetail> _faceDetails;
        public List<FaceDetail> FaceDetails
        {
            get { return _faceDetails; }
            set
            {
                _faceDetails = value;
                OnPropertyChanged("FaceDetails");
            }
        }

        private Amazon.S3.Model.S3Object _selectedS3Object;
        public Amazon.S3.Model.S3Object SelectedS3Object
        {
            get { return _selectedS3Object; }
            set
            {
                _selectedS3Object = value;
                OnPropertyChanged("SelectedS3Object");
            }
        }
        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
        private System.Windows.Controls.Image _image;
        public System.Windows.Controls.Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }
        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }
        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

    }
}
