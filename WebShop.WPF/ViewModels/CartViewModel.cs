using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WebShop.ApiProxy;
using WebShop.DTO;
using WebShop.WPF.Commands;

namespace WebShop.WPF.ViewModels
{
    public class CartViewModel : NotifyPropertyObject, ICartViewModel
    {
        public ObservableCollection<Article> Articles { get; set; }

        public BasicCommand<Article> AddToCartCommand { get; set; }
        public BasicCommand CheckoutCommand { get; set; }

        public EventHandler CheckOutEvent { get; set; }

        public int ItemsInCart => Articles.Count;
        public decimal TotalPriceExclVat => Articles.Sum(o => o.PriceExclVat);
        public decimal Vat => Articles.Sum(o => o.PriceExclVat * 0.2M); //TODO
        public decimal TotalPriceInclVat => Articles.Sum(o => o.PriceExclVat * 1.2M); //TODO

        public CartViewModel(IApiProxy proxy)
        {
            Articles = new ObservableCollection<Article>();
            AddToCartCommand = new BasicCommand<Article>(OnAddToCart);
            CheckoutCommand = new BasicCommand(OnCheckout, () => Articles.Count > 0);
        }

        private void OnCheckout()
        {
            MessageBox.Show("Checkout");
        }

        private void OnAddToCart(Article article)
        {
            if (Articles.All(o => o.Id != article.Id))
                Articles.Add(article);
            else
            {
                MessageBox.Show("It's already in the Cart.");
            }

            OnPropertyChanged(nameof(ItemsInCart));
            OnPropertyChanged(nameof(TotalPriceExclVat));
            OnPropertyChanged(nameof(Vat));
            OnPropertyChanged(nameof(TotalPriceInclVat));

            CheckoutCommand.RaiseCanExecuteChanged();
        }
    }
}
