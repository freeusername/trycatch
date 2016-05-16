using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using Ninject;
using WebShop.ApiProxy;
using WebShop.DTO;
using WebShop.WPF.Commands;

namespace WebShop.WPF.ViewModels
{
    public class MainViewModel : NotifyPropertyObject, IMainViewModel
    {
        private readonly IApiProxy _apiProxy;
        private string _token;
        private string _username;
        private string _title;
        private ICartViewModel _cartViewModel;
        private PagedCollection<Article> _articles = new PagedCollection<Article>();

        #region Properties

        public ICartViewModel CartViewModel
        {
            get { return _cartViewModel; }
            set
            {
                _cartViewModel = value;
                OnPropertyChanged();
            }
        }

        public PagedCollection<Article> Articles
        {
            get
            {
                return _articles;
            }
            private set
            {
                _articles = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoggedIn => IsUserLoggedIn();

        public bool IsLoggedOut => !IsUserLoggedIn();

        #endregion

        #region Commands

        public BasicCommand RegisterCommand { get; set; }
        public BasicCommand LoginCommand { get; set; }
        public BasicCommand LogoutCommand { get; set; }
        public BasicCommand CartCommand { get; set; }

        #endregion

        #region Constructors

        public MainViewModel(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;

            CartViewModel = KernelLocator.Container.Get<ICartViewModel>();
            SetupCommands();

            Articles.PagedChanged += (sender, args) =>
            {
                ReloadGrid();
            };

            this.PropertyChanged += OnPropertyChanged;

            ReloadGrid();
            Username = null;
        }

        #endregion

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Username))
            {
                LoginCommand.RaiseCanExecuteChanged();
                LogoutCommand.RaiseCanExecuteChanged();

                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(IsLoggedOut));
            }
        }

        private bool IsUserLoggedIn()
        {
            return !string.IsNullOrEmpty(_token) && !string.IsNullOrEmpty(Username);
        }

        private void SetupCommands()
        {
            RegisterCommand = new BasicCommand(() =>
            {
                var wnd = KernelLocator.Container.Get<IRegistrationWindow>();
                wnd.Show();
            });

            LogoutCommand = new BasicCommand(() =>
            {
                Username = null;
                _token = null;
            }, IsUserLoggedIn);

            LoginCommand = new BasicCommand(() =>
            {
                var wnd = KernelLocator.Container.Get<ILoginWindow>();
                wnd.Show();
            }, () => !IsUserLoggedIn());

            CartCommand = new BasicCommand(() =>
            {
                var wnd = KernelLocator.Container.Get<ICartWindow>();
                wnd.Show();
            });
        }

        private void ReloadGrid()
        {
            var response = _apiProxy.GetArticles(Articles.CurrentPage, Articles.PageSize);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Articles.UpdateColelction(response.Data.Articles, response.Data.TotalItemsCount);
            }
            else
                MessageBox.Show("Internal server error. Contact an administrator.");
        }

        public void OnSuccessfullLogin(object sender, EventArgs args)
        {
            var loginViewModel = sender as LoginViewModel;
            if (loginViewModel != null)
            {
                _token = loginViewModel.Token;
                Username = loginViewModel.Email;
            }
        }
    }
}
