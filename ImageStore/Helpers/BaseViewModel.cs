using ImageStore.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ImageStore.Helpers
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private UserModel m_User;
        public BaseViewModel()
        {

        }

        public UserModel User
        {
            get { return m_User; }
            set
            {
                m_User = value;
                OnPropertyChanged("User");
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChanged

        protected virtual void SetProperty<T>(ref T member, T val,
           [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(member, val)) return;

            member = val;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Action CloseAction { get; set; }
    }
}
