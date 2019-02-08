using ImageStore.Helpers;
using ImageStore.View;

namespace ImageStore.ViewModel
{
    public class RegistrationViewModel : BaseViewModel
    {
        public ICommandHelper LogInCommand { get; set; }

        public RegistrationViewModel()
        {
            LogInCommand = new ICommandHelper(OnLogin);
        }


        public void OnLogin()
        {
            var login = new MainWindow();
            CloseAction();
            login.Show();
        }
    }
}
