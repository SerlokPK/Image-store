using ImageStore.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                try
                {
                    var images = context.Images.Where(i => i.UserId == user.Id).ToList();

                    foreach (var image in images)
                    {
                        bitMapImg.Add(LoadImage(image.Path));
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }

                return bitMapImg;
            }
        }

        public static BitmapImage LoadImage(string fileName)
        {
            var image = new BitmapImage();

            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                try
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.DecodePixelWidth = 274;
                    image.DecodePixelHeight = 143;
                    image.EndInit();
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }

            return image;
        }

        public static void SaveImage(ImageModel image)
        {
            using (var context = new ImageStoreEntities())
            {
                try
                {
                    var id = (short)(context.Images.Any() ? context.Images.Max(x => x.Id) + 1 : 0);
                    var images = context.Images.Add(new Image
                    {
                        Id = id,
                        Created = DateTime.Now,
                        Description = image.Description,
                        Title = image.Title,
                        Path = image.Path,
                        UserId = image.UserId,
                    });

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }
    }
}
