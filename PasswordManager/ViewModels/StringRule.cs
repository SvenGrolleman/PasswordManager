using System.Windows.Controls;
using System.Globalization;

namespace PasswordManager.ViewModels
{
    public class StringRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty((string)value))
            {
                return new ValidationResult(false, "Field can't be empty.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
