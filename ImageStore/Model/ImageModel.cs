using ImageStore.Helpers;
using System;
using System.Windows.Media.Imaging;

namespace ImageStore.Model
{
    public class ImageModel : BaseModel
    {
        private string description;
        private string title;
        private string path;
        private BitmapImage data;

        public short Id { get; set; }
        public short UserId { get; set; }
        public DateTime Created { get; set; }
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string Path
        {
            get { return path; }
            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged("Path");
                }
            }
        }

        public BitmapImage Data
        {
            get { return data; }
            set
            {
                if (data != value)
                {
                    data = value;
                    OnPropertyChanged("Data");
                }
            }
        }
    }
}
