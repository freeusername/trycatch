using System.Windows;
using Ninject;
using WebShop.WPF.ViewModels;

namespace WebShop.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = KernelLocator.Container.Get<IMainViewModel>();
        }
    }
}
