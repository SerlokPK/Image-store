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
                imageList.Add(dbUser.Images.Where(i => i.UserId == dbUser.Id).Select(x => new ImageModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Created = x.Created,
                    Path = x.Path,
                    Title = x.Title,
                    UserId = x.UserId,
                }).Single());
                user = new UserModel
                {
                    Id = dbUser.Id,
                    Username = dbUser.UserName,
                    Password = dbUser.Password,
                    Images = imageList,
                };
            }

            var storeWindow = new StoreWindow();
            storeWindow.DataContext = new StoreViewModel(user, new ImagesViewModel());
            CloseAction();
            storeWindow.Show();
        }
    }
}
