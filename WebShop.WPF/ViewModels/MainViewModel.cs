using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using Ninject;
using WebShop.ApiProxy;
using WebShop.DTO;
using WebShop.WPF.Annotations;
using WebShop.WPF.Commands;

namespace WebShop.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged, IMainViewModel
    {
        private readonly IApiProxy _apiProxy;

        private ObservableCollection<Article> _articles;
        private int _currentPage;

        public ObservableCollection<Article> Articles
        {
            get
            {
                return _articles;
            }
            set
            {
                _articles = value;
                OnPropertyChanged();
            }
        }

        public const int PageSize = 5;

        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;

                OnPropertyChanged();
            }
        }

        public int TotalItems { get; private set; }

        public BasicCommand NextPageCommand { get; set; }
        public BasicCommand PrevPageCommand { get; set; }
        public BasicCommand RegisterCommand { get; set; }
        public BasicCommand LoginCommand { get; set; }

        public MainViewModel(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
            CurrentPage = 1;

            NextPageCommand = new BasicCommand(() =>
            {
                CurrentPage++;
                ReloadGrid();
            }, () => CurrentPage * PageSize < TotalItems);

            PrevPageCommand = new BasicCommand(() =>
            {
                CurrentPage--;
                ReloadGrid();
            }, () => CurrentPage > 1);

            RegisterCommand = new BasicCommand(() =>
            {
                var wnd = KernelLocator.Container.Get<IRegistrationWindow>();
                wnd.Show();
            });

            LoginCommand = new BasicCommand(() =>
            {
                var wnd = KernelLocator.Container.Get<ILoginWindow>();
                wnd.Show();
            });

            this.PropertyChanged += OnPropertyChanged;

            ReloadGrid();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CurrentPage")
            {
                PrevPageCommand.RaiseCanExecuteChanged();
                NextPageCommand.RaiseCanExecuteChanged();
            }
        }

        private void ReloadGrid()
        {
            var response = _apiProxy.GetArticles(CurrentPage, PageSize);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Articles = new ObservableCollection<Article>(response.Data.Articles);
                TotalItems = response.Data.TotalItemsCount;
            }
            else
                throw new Exception("Smth went wrong"); // TODO
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IMainViewModel
    {
    }
}
