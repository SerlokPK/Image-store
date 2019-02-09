using System;
using System.Linq;
using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.View;
using ImageStore.ViewModel.Store;

namespace ImageStore.ViewModel
{
    public class RegistrationViewModel : BaseViewModel
    {
        #region Commands
        public CommandHelper LogInCommand { get; set; }
        public CommandHelper RegisterCommand { get; set; }
        #endregion

        public RegistrationViewModel()
        {
            LogInCommand = new CommandHelper(OnLogin);
            RegisterCommand = new CommandHelper(OnRegister);
            User = new UserModel();
        }

        private void OnLogin()
        {
            var login = new MainWindow();
            CloseAction();
            login.Show();
        }

        private void OnRegister()
        {
            //TODO VALIDATION
            using (var context = new ImageStoreEntities())
            {
                var id = (short)(context.Users.Any() ? context.Users.Max(x => x.Id) + 1 : 0);
                context.Users.Add(new User
                {
                    Id = id,
                    UserName = User.Username,
                    Password = User.Password,
                });

                context.SaveChanges();
            }

            var storeWindow = new StoreWindow();
            storeWindow.Content = new StoreViewModel(User,new AddImageViewModel());
            CloseAction();
            storeWindow.Show();
        }
    }
}
