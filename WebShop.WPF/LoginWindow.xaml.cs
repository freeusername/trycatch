using System;
using System.Windows;
using Ninject;
using WebShop.WPF.ViewModels;

namespace WebShop.WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, ILoginWindow
    {
        private LoginViewModel ViewModel
        {
            get { return DataContext as LoginViewModel; }
        }

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = KernelLocator.Container.Get<ILoginViewModel>();
            ViewModel.OnSuccessfullLoginEvent += OnSuccessfullLoginEvent;

            var mainViewModel = KernelLocator.Container.Get<IMainViewModel>() as MainViewModel;
            if (mainViewModel != null)
                ViewModel.OnSuccessfullLoginEvent += mainViewModel.OnSuccessfullLogin;
        }

        private void OnSuccessfullLoginEvent(object sender, EventArgs eventArgs)
        {
            this.Close();
        }
    }
}
