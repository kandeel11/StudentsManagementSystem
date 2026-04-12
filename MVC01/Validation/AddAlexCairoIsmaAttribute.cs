using System.ComponentModel.DataAnnotations;

namespace MVC01.Validation
{
    public class AddAlexCairoIsmaAttribute: ValidationAttribute
    {
        override protected ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            string address = value.ToString()!;
            if (address.Contains("Alex") && address.Contains("Cairo") && address.Contains("Ismalia"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessage ?? "Address must contain 'Alex', 'Cairo', and 'Ismalia'.");
        }
    }
}
