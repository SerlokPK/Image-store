using ImageStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ImageStore.Services
{
    public static class ImageService
    {
        public static ObservableCollection<BitmapImage> GetImageSources(UserModel user)
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

        private static BitmapImage LoadImage(string fileName)
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
