using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Reviewing.ReviewAggregate
{
	public record CommentDescription
	{
		private const int TEXT_MIN_LENGTH = 20;
		private const int TEXT_MAX_LENGTH = 1000;

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

				ArgumentValidator.MinLength(trimedValue, TEXT_MIN_LENGTH, argumentName);
				ArgumentValidator.MaxLength(trimedValue, TEXT_MAX_LENGTH, argumentName);

				this.text = trimedValue;
			}
		}
	}
}
