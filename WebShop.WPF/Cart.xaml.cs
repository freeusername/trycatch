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
using WebShop.WPF.ViewModels;

namespace WebShop.WPF
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class Cart : Window, ICartWindow
    {
        public Cart(ICartViewModel cartViewModel)
        {
            InitializeComponent();
            DataContext = cartViewModel;
        }
    }
}
