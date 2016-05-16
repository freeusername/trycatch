using System;
using System.Windows;
using Ninject;
using WebShop.WPF.ViewModels;

namespace WebShop.WPF
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window, IRegistrationWindow
    {
        private RegistrationViewModel ViewModel
        {
            get { return DataContext as RegistrationViewModel; }
        }

        public RegistrationWindow()
        {
            InitializeComponent();
            DataContext = KernelLocator.Container.Get<IRegistrationViewModel>();
            ViewModel.OnSuccessfullyRegisteredEvent += OnSuccessfullyRegisteredEvent;
        }

        private void OnSuccessfullyRegisteredEvent(object sender, EventArgs eventArgs)
        {
            this.Close();
        }
    }
}
