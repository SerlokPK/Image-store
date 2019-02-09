using ImageStore.Helpers;
using ImageStore.Model;
using System;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ImageStore.ViewModel.Store
{
    public class AddImageViewModel : BaseViewModel
    {
        private ImageModel image;

        public CommandHelper AddImageCommand { get; set; }
        public AddImageViewModel()
        {
            Image = new ImageModel();
            Image.Data = ShowPicture(@"C:\Users\Serlok\Source\Repos\SerlokPK\ImageStore\ImageStore\Images\ImagePicker.jpg");
            AddImageCommand = new CommandHelper(OnAddImage);
        }


        public ImageModel Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        private void OnAddImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                Image.Path = selectedFileName;
                Image.Data = ShowPicture(selectedFileName);
            }
        }

        private BitmapImage ShowPicture(string filePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath);
            bitmap.EndInit();

            return bitmap;
        }
    }
}
