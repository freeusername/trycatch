using System;
using System.Windows.Input;
using WebShop.WPF.Commands;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Windows;
using WebShop.ApiProxy;

namespace WebShop.WPF.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        #region fields

        private readonly IApiProxy _apiProxy;

        private string _email;
        private string _password;

        #endregion

        #region Properties
        public EventHandler OnSuccessfullLoginEvent { get; set; }
   
        [Required]
        [EmailAddress]
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value)
                    return;

                _email = value;
                OnPropertyChanged();
            }
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                _password = value;
                OnPropertyChanged();
            }
        }

        public string Token { get; private set; }
        
        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion

        public LoginViewModel(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
            LoginCommand = new BasicCommand(() =>
            {
                _validated = true;
                OnPropertyChanged(String.Empty);

                if (!_errors.Any())
                {
                    var response = _apiProxy.RequestToken(_email, _password);

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show("no errors!");
                    }
                    else
                    {
                        MessageBox.Show("Successfully logged in");
                        Token = response.Data.Access_Token; 
                        OnSuccessfullLogin();
                    }
                }
            });
        }

        private void OnSuccessfullLogin()
        {
            var handler = OnSuccessfullLoginEvent;
            if (handler != null)
                handler(this, null);
        }
    }
}
