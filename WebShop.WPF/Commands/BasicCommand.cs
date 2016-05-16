using System;
using System.Windows.Input;

namespace WebShop.WPF.Commands
{
    public class BasicCommand<T> : ICommand
        where T : class
    {
        private readonly Action<T> _action;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public BasicCommand(Action<T> action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _action?.Invoke(parameter as T);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }

    public class BasicCommand : BasicCommand<object>
    {
        public BasicCommand(Action action, Func<bool> canExecute = null)
            : base((obj) =>
            {
                action();
            }, canExecute)
        {
        }
    }
}
