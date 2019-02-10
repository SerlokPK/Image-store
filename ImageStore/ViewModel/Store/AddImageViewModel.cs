using ImageResizer;
using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.Services;
using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ImageStore.ViewModel.Store
{
    public class AddImageViewModel : BaseViewModel
    {
        private ImageModel image;

        public CommandHelper AddImageCommand { get; set; }
        public CommandHelper SaveImageCommand { get; set; }
        public AddImageViewModel()
        {
            Image = new ImageModel();
            Image.Data = ShowPicture(@"C:\Users\Serlok\Source\Repos\SerlokPK\ImageStore\ImageStore\Images\ImagePicker.jpg");
            AddImageCommand = new CommandHelper(OnAddImage);
            SaveImageCommand = new CommandHelper(OnSaveImage);
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
        private void OnSaveImage()
        {
            Image.UserId = User.Id;
            ImageService.SaveImage(Image);
        }
        private void OnAddImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            string appPath = Directory.GetCurrentDirectory();
            string imageFolder = Directory.GetParent(appPath).Parent.FullName + "\\Images\\";
            if (Directory.Exists(imageFolder) == false)
            {
                Directory.CreateDirectory(imageFolder);
                //string iName = dlg.SafeFileName;
                //if (!File.Exists(imageFolder + iName))
                //{
                //    File.Copy(selectedFileName, imageFolder + iName);
                //}
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                string[] iName = dlg.SafeFileName.Split('.');
                string newPath = imageFolder + iName[0];
                CropImage(selectedFileName, newPath);
                Image.Path = newPath + "." + iName[1];
                Image.Data = ShowPicture(newPath + "." + iName[1]);
                //if (File.Exists(newPath + "." + iName[1]))
                //{
                //    File.Delete(newPath + "." + iName[1]);
                //}
            }
        }

        private BitmapImage ShowPicture(string filePath)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(filePath);
            bitmap.DecodePixelWidth = 274;
            bitmap.DecodePixelHeight = 143;
            bitmap.EndInit();

            return bitmap;
        }

        private void CropImage(string source, string dest)
        {
            ImageBuilder.Current.Build(new ImageJob(source, dest, new Instructions("width=274;height=143;format=jpg;"), false, true));
        }
    }
}
