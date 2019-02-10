using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.Services;
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
            User.Validate("registration");
            if (User.IsValid)
            {
                UserService.SaveUser(User);
                var storeWindow = new StoreWindow();
                storeWindow.Content = new StoreViewModel(User, new AddImageViewModel());
                CloseAction();
                storeWindow.Show();
            }
        }
    }
}
