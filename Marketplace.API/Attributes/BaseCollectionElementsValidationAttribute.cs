using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Attributes
{
    public abstract class BaseCollectionElementsValidationAttribute : ValidationAttribute
    {
        protected abstract Type ElementsType { get; }

        protected abstract Predicate<object> IsValidPredicate { get; }

        protected abstract string ErrorMessage { get; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var isCorrectType = value is IEnumerable && value.GetType().GenericTypeArguments[0] == ElementsType;
            if (isCorrectType)
                throw new InvalidOperationException($"Can't use {value.GetType().Name} on this attribute!");

            var elements = value as IEnumerable;
            var invalidElements = new List<object>();
            foreach (var currentElement in elements)
            {
                if (IsValidPredicate(currentElement) == false)
                    invalidElements.Add(currentElement);
            }

            if (invalidElements.Count > 0)
            {
                var invalidElementsNames = invalidElements.Select(e => e.GetType().Name);
                return new ValidationResult(ErrorMessage, invalidElementsNames);
            }

            return ValidationResult.Success;
        }
    }
}
