using Marketplace.Shared;
using Microsoft.VisualBasic;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Attributes
{
	public class MinCollectionLengthAttribute : ValidationAttribute
	{
		private readonly int minLength;

		public MinCollectionLengthAttribute(int minLength)
		{
			ArgumentValidator.MinValue(minLength, 1, nameof(minLength));
			this.minLength = minLength;
		}

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value == null)
				return ValidationResult.Success;

			var isCorrectType = value is IEnumerable;
			if (isCorrectType)
				throw new InvalidOperationException($"Propery type is not an IEnumerable!");

			var valueAsCollection = value as Collection;
			if (valueAsCollection == null)
				throw new NullReferenceException("Collection is null!");

			var valueColletionCount = valueAsCollection.Count;
			if (valueColletionCount < this.minLength)
				return new ValidationResult($"Length is {valueColletionCount} which is less than the minimum {this.minLength}!");

			return ValidationResult.Success;
		}
	}
}
