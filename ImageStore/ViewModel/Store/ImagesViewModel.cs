using ImageStore.Helpers;
using ImageStore.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ImageStore.ViewModel.Store
{
    public class ImagesViewModel : BaseViewModel
    {
        public ObservableCollection<BitmapImage> Images { get; set; }
        public ImagesViewModel()
        {

        }

        public ObservableCollection<BitmapImage> GetImages(UserModel user)
        {
            ObservableCollection<BitmapImage> bitMapImg = new ObservableCollection<BitmapImage>();
            using (var context = new ImageStoreEntities())
            {
                var images = context.Images.Where(i => i.UserId == user.Id).ToList();

                foreach (var image in images)
                {
                    bitMapImg.Add(LoadImage(image.Path));
                }

                return bitMapImg;
            }
        }

        public static BitmapImage LoadImage(string fileName)
        {
            var image = new BitmapImage();

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }

            return image;
        }
    }
}
