using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Sales.ReviewAggregate
{
	public record CommentDescription : ValueObject
	{
		private readonly string text = null!;

		public CommentDescription(string text)
		{
			this.Text = text;
		}

		public string Text
		{
			get { return text; }
			init
			{
				var argumentName = nameof(this.Text);

				ArgumentValidator.NotNullOrEmpty(value, argumentName);

				var trimedValue = value.Trim();

				ArgumentValidator.MinLength(trimedValue, ReviewConstants.DESCRIPTION_MIN_LENGTH, argumentName);
				ArgumentValidator.MaxLength(trimedValue, ReviewConstants.DESCRIPTION_MAX_LENGTH, argumentName);

				this.text = trimedValue;
			}
		}
	}
}
