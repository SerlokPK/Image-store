using ImageStore.Helpers;
using ImageStore.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ImageStore.ViewModel.Store
{
    public class ImagesViewModel : BaseViewModel
    {
        public ObservableCollection<BitmapImage> Images { get; set; }
        public ImagesViewModel()
        {

        }
    }
}
