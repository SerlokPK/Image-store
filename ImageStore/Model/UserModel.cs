using ImageStore.Helpers;
using ImageStore.Services;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace ImageStore.Model
{
    public class UserModel : ValidationBase
    {
        private string username;
        private string password;

        public short Id { get; set; }
        public ObservableCollection<ImageModel> Images { get; set; }
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

        protected override void ValidateSelf(string type)
        {
            if (string.IsNullOrWhiteSpace(this.username))
            {
                this.ValidationErrors["Username"] = "Username is required.";
            }
            if (string.IsNullOrWhiteSpace(this.password))
            {
                this.ValidationErrors["Password"] = "Password is required.";
            }
            if (type == "login")
            {
                if (!UserService.CheckIfUserExist(this.username,this.password))
                {
                    this.ValidationErrors["Username"] = "User doesn't exist.";
                }
            }
            else if(type == "registration")
            {
                if (UserService.CheckIfUsernameExist(this.username))
                {
                    this.ValidationErrors["Username"] = "Username is already taken.";
                }
                if (this.username != null && Regex.IsMatch(this.username, @"^\d"))
                {
                    this.ValidationErrors["Username"] = "Username can't start with digit.";
                }
                if (this.password != null && this.password.Length <= 6)
                {
                    this.ValidationErrors["Password"] = "Password must be at least 7 characters.";
                }
            }
        }
    }
}
