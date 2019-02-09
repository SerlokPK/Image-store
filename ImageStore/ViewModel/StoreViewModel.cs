using ImageStore.Helpers;
using ImageStore.Model;
using ImageStore.ViewModel.Store;

namespace ImageStore.ViewModel
{
    public class StoreViewModel : BaseViewModel
    {
        private AddImageViewModel addImageViewModel = new AddImageViewModel();
        private ImagesViewModel ImagesViewModel = new ImagesViewModel();
        private BaseViewModel currentViewModel;

        public CommandHelper<string> NavCommand { get; private set; }


        public StoreViewModel(UserModel user,BaseViewModel viewModel)
        {
            NavCommand = new CommandHelper<string>(OnNav);
            CurrentViewModel = viewModel;
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
                    ImagesViewModel.User = User;
                    CurrentViewModel = ImagesViewModel;
                    break;
                case "add":
                    addImageViewModel.User = User;
                    CurrentViewModel = addImageViewModel;
                    break;
                case "account":
                    //CurrentViewModel = studentViewModel;
                    break;
            }
        }
    }
}
