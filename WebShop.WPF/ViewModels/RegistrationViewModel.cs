using System;
using System.Windows.Input;
using WebShop.WPF.Commands;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Windows;
using WebShop.ApiProxy;
using WebShop.DTO.Model;

namespace WebShop.WPF.ViewModels
{
    public class RegistrationViewModel : BaseViewModel, IRegistrationViewModel
    {
        #region fields

        private readonly IApiProxy _apiProxy;
        private string _title;
        private string _firstName;
        private string _lastName;
        private string _address;
        private string _houseNumber;
        private string _zipCode;
        private string _city;
        private string _email;
        private string _password;
        private string _confirmPassword;

        #endregion

        #region Properties
        public EventHandler OnSuccessfullyRegisteredEvent { get; set; }

        [Required]
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                    return;

                _title = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value)
                    return;

                _firstName = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value)
                    return;

                _lastName = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string Address
        {
            get { return _address; }
            set
            {
                if (_address == value)
                    return;

                _address = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string HouseNumber
        {
            get { return _houseNumber; }
            set
            {
                if (_houseNumber == value)
                    return;

                _houseNumber = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string ZipCode
        {
            get { return _zipCode; }
            set
            {
                if (_zipCode == value)
                    return;

                _zipCode = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string City
        {
            get { return _city; }
            set
            {
                if (_city == value)
                    return;

                _city = value;
                OnPropertyChanged();
            }
        }

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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
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

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (_confirmPassword == value)
                    return;

                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand RegisterCommand { get; set; }

        #endregion

        public RegistrationViewModel(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
            RegisterCommand = new BasicCommand(() =>
            {
                _validated = true;
                OnPropertyChanged(String.Empty);

                if (!_errors.Any())
                {
                    var model = AutoMapper.Mapper.Map<RegisterModel>(this);
                    var response = _apiProxy.Register(model);

                    if (response.StatusCode != HttpStatusCode.OK)
                        MessageBox.Show("no errors!");
                    else
                    {
                        MessageBox.Show("Successfully registered");
                        OnSuccessfullyRegistered();
                    }
                }
            });
        }

        private void OnSuccessfullyRegistered()
        {
            var handler = OnSuccessfullyRegisteredEvent;
            if (handler != null)
                handler(this, null);
        }
    }
}
