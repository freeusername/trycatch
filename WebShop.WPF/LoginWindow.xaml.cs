using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
        }

        private void OnSuccessfullLoginEvent(object sender, EventArgs eventArgs)
        {
            this.Close();
        }
    }

    public interface ILoginWindow
    {
        void Show();
    }
}
