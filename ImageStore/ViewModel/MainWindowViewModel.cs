using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.Services;
using ImageStore.View;
using ImageStore.ViewModel.Store;

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
            User.Validate("login");
            if (User.IsValid)
            {
                var user = UserService.GetUser(User);
                var storeWindow = new StoreWindow();
                var imageVM = new ImagesViewModel();
                imageVM.Images = ImageService.GetImageSources(user);
                storeWindow.DataContext = new StoreViewModel(user, imageVM);
                CloseAction();
                storeWindow.Show();
            }
        }
    }
}
