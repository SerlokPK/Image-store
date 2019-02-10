using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.Services;
using ImageStore.ViewModel.Store;

namespace ImageStore.ViewModel
{
    public class StoreViewModel : BaseViewModel
    {
        private AddImageViewModel addImageViewModel = new AddImageViewModel();
        private ImagesViewModel imagesViewModel = new ImagesViewModel();
        private AccountViewModel accountViewModel = new AccountViewModel();
        private BaseViewModel currentViewModel;

        public CommandHelper<string> NavCommand { get; private set; }


        public StoreViewModel(UserModel user,BaseViewModel viewModel)
        {
            NavCommand = new CommandHelper<string>(OnNav);
            CurrentViewModel = viewModel;
            CurrentViewModel.User = user;
            User = user;
        }

        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "images":
                    imagesViewModel.User = User;
                    imagesViewModel.Images = ImageService.GetImageSources(User);
                    CurrentViewModel = imagesViewModel;
                    break;
                case "add":
                    addImageViewModel.User = User;
                    CurrentViewModel = addImageViewModel;
                    break;
                case "account":
                    CurrentViewModel.User = User;
                    CurrentViewModel = accountViewModel;
                    break;
            }
        }
    }
}
