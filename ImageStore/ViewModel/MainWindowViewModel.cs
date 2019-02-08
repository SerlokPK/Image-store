using ImageStore.Helpers;
using ImageStore.View;
using System;

namespace ImageStore.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommandHelper RegisterCommand { get; set; }

        public MainWindowViewModel()
        {
            RegisterCommand = new ICommandHelper(OnRegister);
        }


        public void OnRegister()
        {
            var register = new RegistrationWindow();
            CloseAction();
            register.Show();
        }
    }
}
