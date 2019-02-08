using ImageStore.ViewModel;
using System;
using System.Windows;

namespace ImageStore.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mv = new MainWindowViewModel();
            this.DataContext = mv;

            if (mv.CloseAction == null)
                mv.CloseAction = new Action(() => this.Close());
        }
    }
}
