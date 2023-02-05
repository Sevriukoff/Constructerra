using System.Globalization;
using System.Windows.Controls;

namespace TerrariaConstructor.Common.ValidationRules;

public class NumberRangeValidationRule : ValidationRule
{
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
    
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (!int.TryParse(value.ToString(), out var number))
        {
            return new ValidationResult(false, "Input must be a number.");
        }

        if (number <= MinValue || number >= MaxValue)
        {
            return new ValidationResult(false, 
                $"Number must be greater than {MinValue} and less than or equal to {MaxValue}.");
        }
        
        return ValidationResult.ValidResult;
    }
}