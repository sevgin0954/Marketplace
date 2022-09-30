using Marketplace.Domain.Common.Constants;
using System;

namespace Marketplace.Domain.SharedKernel
{
	public record Price
	{
		private decimal value;

		public Price(decimal value, Currency currency)
		{
			this.Value = value;
			this.Currency = currency;
		}

		public decimal Value
		{
			get { return this.value; }
			init
			{
				if (value < 0)
					throw new InvalidOperationException(ErrorConstants.NUMBER_CANT_BE_NEGATIVE);

				this.value = value;
			}
		}

		public Currency Currency { get; }

		public Price SetValue(decimal value)
		{
			return new Price(value, this.Currency);
		}

		public Price SetCurrency(Currency currency)
		{
			return new Price(this.Value, currency);
		}
	}
}
