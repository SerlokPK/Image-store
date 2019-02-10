using ImageStore.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System;
using System.Diagnostics;

namespace ImageStore.Services
{
    public static class UserService
    {
        public static bool CheckIfUsernameExist(string username)
        {
            using (var context = new ImageStoreEntities())
            {
                return context.Users.Any(u => u.UserName == username);
            }
        }

        public static bool CheckIfUserExist(string username, string password)
        {
            using (var context = new ImageStoreEntities())
            {
                return context.Users.Any(u => u.UserName == username && u.Password == password);
            }
        }

        public static UserModel GetUser(UserModel User)
        {
            var user = new UserModel();
            using (var context = new ImageStoreEntities())
            {
                try
                {
                    var dbUser = context.Users.Include(u => u.Images)
                                            .Where(u => u.UserName == User.Username && u.Password == User.Password).Single();
                    var imageList = new ObservableCollection<ImageModel>();
                    foreach (var image in dbUser.Images)
                    {
                        imageList.Add(new ImageModel
                        {
                            Id = image.Id,
                            Created = image.Created,
                            Description = image.Description,
                            Path = image.Path,
                            Title = image.Title,
                            UserId = image.UserId,
                        });
                    }
                    user = new UserModel
                    {
                        Id = dbUser.Id,
                        Username = dbUser.UserName,
                        Password = dbUser.Password,
                        Images = imageList,
                    };
                }catch(Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }

            return user;
        }

        public static void SaveUser(UserModel User)
        {
            using (var context = new ImageStoreEntities())
            {
                try
                {
                    var id = (short)(context.Users.Any() ? context.Users.Max(x => x.Id) + 1 : 0);
                    context.Users.Add(new User
                    {
                        Id = id,
                        UserName = User.Username,
                        Password = User.Password,
                    });

                    context.SaveChanges();
                }catch(Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }
    }
}
