using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WebShop.WPF.Annotations;
using WebShop.WPF.Commands;

namespace WebShop.WPF
{
    public class PagedCollection<T> : NotifyPropertyObject
        where T : class
    {
        private int _currentPage;

        public readonly int PageSize = 5;
        public int CurrentPage
        {
            get { return _currentPage; }
            private set
            {
                _currentPage = value;

                RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private int _totalItems;
        public int TotalItems
        {
            get { return _totalItems; }
            private set
            {
                _totalItems = value;

                RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private ObservableCollection<T> _collection;

        public ObservableCollection<T> Collection
        {
            get
            {
                return _collection;
            }
            private set
            {
                _collection = value;

                RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public BasicCommand NextPageCommand { get; set; }
        public BasicCommand PrevPageCommand { get; set; }

        public EventHandler PagedChanged { get; set; }

        public PagedCollection()
        {
            CurrentPage = 1;

            NextPageCommand = new BasicCommand(() =>
            {
                CurrentPage++;
                RaisePageChangedEvent();
            }, () => CurrentPage * PageSize < TotalItems);

            PrevPageCommand = new BasicCommand(() =>
            {
                CurrentPage--;
                RaisePageChangedEvent();
            }, () => CurrentPage > 1);
        }

        public PagedCollection(IEnumerable<T> collection, int totalItems)
            : this()
        {
            Collection = new ObservableCollection<T>(collection);
            _totalItems = totalItems;
        }

        public void RaiseCanExecuteChanged()
        {
            if (PrevPageCommand != null)
                PrevPageCommand.RaiseCanExecuteChanged();

            if (NextPageCommand != null)
                NextPageCommand.RaiseCanExecuteChanged();
        }

        public void UpdateColelction(IEnumerable<T> collection, int totalItemsCount)
        {
            TotalItems = totalItemsCount;
            Collection = new ObservableCollection<T>(collection);
        }

        private void RaisePageChangedEvent()
        {
            var handler = PagedChanged;
            if (handler != null)
                handler(this, null);
        }
    }
}
