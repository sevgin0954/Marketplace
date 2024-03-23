using Marketplace.Domain.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Attributes
{
	public class StringCurrencyCodeAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var isValueString = value.GetType() == typeof(string);
			if (isValueString == false)
				return new ValidationResult($"Property {validationContext.MemberName} is not a string type!");

			var errorMessage = $"The field '{validationContext.DisplayName}' is not a valid currency!";

			var isValueValidCurrency = Enum.TryParse<Currency>(value as string, out _);
			if (isValueValidCurrency)
				return ValidationResult.Success;
			else
				return new ValidationResult(errorMessage);
		}
	}
}
