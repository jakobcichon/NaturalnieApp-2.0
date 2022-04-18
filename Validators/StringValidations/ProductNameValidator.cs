using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Validators.StringValidations
{
    public class ProductNameValidator : ValidationRule
    {
        private const int maxLength = 10;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
               var stringValue = value.ToString();

                if (stringValue.Length > 10)
                {
                    return new ValidationResult(false, $"Maksumalna długość nazwy to {maxLength}. Podana nazwa ma {stringValue.Length} znaków.");
                }
            }
            catch (NullReferenceException ex)
            {
                return new ValidationResult(false, "Podano pustą nazwę!");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Nie udało się dokonać walidacji!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
