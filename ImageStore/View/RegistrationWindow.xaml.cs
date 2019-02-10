using ImageStore.ViewModel;
using System;
using System.Windows;

namespace ImageStore.View
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
            var rw = new RegistrationViewModel();
            this.DataContext = rw;

            if (rw.CloseAction == null)
                rw.CloseAction = new Action(() => this.Close());
        }
    }
}
