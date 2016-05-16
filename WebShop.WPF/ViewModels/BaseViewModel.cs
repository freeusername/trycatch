using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebShop.WPF.ViewModels
{
    public class BaseViewModel : NotifyPropertyObject, IDataErrorInfo
    {
        protected IDictionary<string, string> _errors = new Dictionary<string, string>();
        protected bool _validated;

        public string this[string columnName]
        {
            get
            {
                if (!_validated)
                    return null;

                var validationResults = new List<ValidationResult>();

                if (Validator.TryValidateProperty(
                    GetType().GetProperty(columnName).GetValue(this)
                    , new ValidationContext(this)
                    {
                        MemberName = columnName
                    }
                    , validationResults))
                {
                    if (_errors.ContainsKey(columnName))
                        _errors.Remove(columnName);

                    return null;
                }

                var message = validationResults.First().ErrorMessage;
                _errors[columnName] = message;
                return message;
            }
        }

        public string Error { get; }
    }
}
