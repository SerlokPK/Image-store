using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.View;
using ImageStore.ViewModel.Store;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace ImageStore.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Commands
        public CommandHelper RegisterCommand { get; set; }
        public CommandHelper LoginCommand { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            RegisterCommand = new CommandHelper(OnRegister);
            LoginCommand = new CommandHelper(OnLogin);
            User = new UserModel();
        }



        private void OnRegister()
        {
            var register = new RegistrationWindow();
            CloseAction();
            register.Show();
        }

        private void OnLogin()
        {
            //TODO VALIDATION
            var user = new UserModel();
            using (var context = new ImageStoreEntities())
            {
                var dbUser = context.Users.Include(u => u.Images).Where(u => u.UserName == User.Username).SingleOrDefault();
                var imageList = new ObservableCollection<ImageModel>();
                foreach(var image in dbUser.Images)
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
            }

            var storeWindow = new StoreWindow();
            var imageVM = new ImagesViewModel();
            imageVM.Images = imageVM.GetImages(user);
            storeWindow.DataContext = new StoreViewModel(user, imageVM);
            CloseAction();
            storeWindow.Show();
        }
    }
}
