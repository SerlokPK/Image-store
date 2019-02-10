using ImageResizer;
using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.Services;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ImageStore.ViewModel.Store
{
    public class AddImageViewModel : BaseViewModel
    {
        private ImageModel image;
        private string saved;
        #region Commands
        public CommandHelper AddImageCommand { get; set; }
        public CommandHelper SaveImageCommand { get; set; }
        #endregion
        public AddImageViewModel()
        {
            Image = new ImageModel();
            Image.Data = ImageService.LoadImage(@"C:\Users\Serlok\Source\Repos\SerlokPK\ImageStore\ImageStore\Images\ImagePicker.jpg");
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

        public string Saved
        {
            get { return saved; }
            set
            {
                saved = value;
                OnPropertyChanged("Saved");
            }
        }
        private void OnSaveImage()
        {
            Image.Validate("");
            if (Image.IsValid)
            {
                Image.UserId = User.Id;
                ImageService.SaveImage(Image);
                Saved = "Image saved";
            }
        }
        private void OnAddImage()
        {
            Saved = "";
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = "c:\\";
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            string appPath = Directory.GetCurrentDirectory();
            string imageFolder = Directory.GetParent(appPath).Parent.FullName + "\\Images\\";
            if (Directory.Exists(imageFolder) == false)
            {
                Directory.CreateDirectory(imageFolder);
            }

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                string[] iName = dlg.SafeFileName.Split('.');
                string extension = GetExtension(selectedFileName);
                if (IsImageExtension(extension))
                {
                    string newPath = imageFolder + iName[0];
                    CropImage(selectedFileName, newPath);
                    Image.Path = newPath + "." + iName[1];
                    Image.Data = ImageService.LoadImage(newPath + "." + iName[1]);
                }
            }
        }

        private void CropImage(string source, string dest)
        {
            ImageBuilder.Current.Build(new ImageJob(source, dest, new Instructions("width=274;height=143;format=jpg;"), false, true));
        }

        private string GetExtension(string path)
        {
            return path.Split('.').Last();
        }
        private bool IsImageExtension(string ext)
        {
            string[] _validExtensions = { "jpg", "bmp", "gif", "png" };
            return _validExtensions.Contains(ext.ToLower());
        }
    }
}
