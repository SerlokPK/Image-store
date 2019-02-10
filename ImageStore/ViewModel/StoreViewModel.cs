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
        private bool imagesChecked = false;
        private bool addChecked= false;
        private bool accountChecked = false;

        public StoreViewModel(UserModel user,BaseViewModel viewModel)
        {
            NavCommand = new CommandHelper<string>(OnNav);
            CurrentViewModel = viewModel;
            CurrentViewModel.User = user;
            User = user;
            ImagesChecked = true;
        }

        public BaseViewModel CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        public bool ImagesChecked
        {
            get { return imagesChecked; }
            set
            {
                imagesChecked = value;
                OnPropertyChanged("ImagesChecked");
            }
        }

        public bool AddChecked
        {
            get { return addChecked; }
            set
            {
                addChecked = value;
                OnPropertyChanged("addChecked");
            }
        }

        public bool AccountChecked
        {
            get { return accountChecked; }
            set
            {
                accountChecked = value;
                OnPropertyChanged("accountChecked");
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "images":
                    imagesViewModel.User = User;
                    imagesViewModel.ImageModels = ImageService.GetImageModels(User);
                    SetAllToFalse();
                    ImagesChecked = true;
                    CurrentViewModel = imagesViewModel;
                    break;
                case "add":
                    addImageViewModel.User = User;
                    SetAllToFalse();
                    AddChecked = true;
                    CurrentViewModel = addImageViewModel;
                    break;
                case "account":
                    SetAllToFalse();
                    AccountChecked = true;
                    CurrentViewModel = accountViewModel;
                    CurrentViewModel.User = User;
                    break;
            }
        }

        private void SetAllToFalse()
        {
            ImagesChecked = false;
            AddChecked = false;
            AccountChecked = false;
        }
    }
}
