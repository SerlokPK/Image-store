using ImageStore.Helpers;
using System.Linq;

namespace ImageStore.ViewModel.Store
{
    public class AccountViewModel : BaseViewModel
    {
        public CommandHelper SaveCommand { get; private set; }
        public AccountViewModel()
        {
            SaveCommand = new CommandHelper(OnSave);
        }

        private void OnSave()
        {
            using (var context = new ImageStoreEntities())
            {
                var user = context.Users.Where(u => u.Id == User.Id).Single();

                user.UserName = User.Username;
                user.Password = User.Password;

                context.SaveChanges();
            }
        }
    }
}
