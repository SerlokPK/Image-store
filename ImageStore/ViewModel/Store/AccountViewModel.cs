using ImageStore.Helpers;
using ImageStore.Services;
using System.Linq;

namespace ImageStore.ViewModel.Store
{
    public class AccountViewModel : BaseViewModel
    {
        private string updated;
        public CommandHelper SaveCommand { get; private set; }
        public AccountViewModel()
        {
            SaveCommand = new CommandHelper(OnSave);
        }

        public string Updated
        {
            get { return updated; }
            set
            {
                if (updated != value)
                {
                    updated = value;
                    OnPropertyChanged("Updated");
                }
            }
        }
        private void OnSave()
        {
            Updated = "";
            User.Validate("registration");
            if (User.IsValid)
            {
                UserService.UpdateUser(User);
                Updated = "Successfully updated";
            }
        }
    }
}
