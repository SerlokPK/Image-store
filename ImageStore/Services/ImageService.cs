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
        public static ObservableCollection<ImageModel> GetImageModels(UserModel user)
        {
            ObservableCollection<ImageModel> images = new ObservableCollection<ImageModel>();
            using (var context = new ImageStoreEntities())
            {
                try
                {
                    var dbImages = context.Images.Where(i => i.UserId == user.Id);
                    foreach(var image in dbImages)
                    {
                        images.Add(new ImageModel
                        {
                            Id = image.Id,
                            Created = image.Created,
                            Description = image.Description,
                            Path = image.Path,
                            Title = image.Title,
                            UserId = image.UserId,
                            Data = LoadImage(image.Path),
                        });
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }

                return images;
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
