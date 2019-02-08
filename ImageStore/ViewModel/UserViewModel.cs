using ImageStore.Helpers;

namespace ImageStore.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public UserViewModel()
        {
            using (var context = new ImageStoreEntities()) {
                //context.Images;
            }
        }
    }
}
