using ImageStore.Helpers;
using System.Collections.ObjectModel;

namespace ImageStore.Model
{
    public class UserModel : BaseModel
    {
        private string username;
        private string password;

        public short Id { get; set; }
        public ObservableCollection<ImageModel>  Images { get; set; }
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
    }
}
