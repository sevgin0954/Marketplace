using Marketplace.Domain.Common.Constants;
using System;

namespace Marketplace.Domain.SharedKernel
{
	public class Price
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


		public override bool Equals(object? obj)
		{
			if (obj == null)
				return false;
			if (obj is Price == false)
				return false;

			var castedObj = obj as Price;
			if (castedObj!.Value != this.value || castedObj.Currency != this.Currency)
				return false;

			return true;
		}

		public override int GetHashCode()
		{
			return Tuple.Create(this.value, this.Currency).GetHashCode();
		}

		public static bool operator ==(Price? left, Price? right)
		{
			if (left == null || right == null)
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(Price? left, Price? right)
		{
			return !(left == right);
		}

		public static Price operator +(Price? left, Price? right)
		{
			if (left == null || right == null)
				throw new InvalidOperationException("Can't add price with null value!");
			ThrowExceptionIfCurrenciesNotEqual(left, right);

			var substractedValue = left.value + right.value;
			var price = new Price(substractedValue, left.Currency);

			return price;
		}

		public static Price operator -(Price? left, Price? right)
		{
			if (left == null || right == null)
				throw new InvalidOperationException("Can't substract price with null value!");
			ThrowExceptionIfCurrenciesNotEqual(left, right);

			var substractedValue = left.value - right.value;
			var price = new Price(substractedValue, left.Currency);

			return price;
		}

		private static void ThrowExceptionIfCurrenciesNotEqual(Price left, Price right)
		{
			if (left.Currency != right.Currency)
				throw new InvalidOperationException("Can't make operations on prices with different currencies!");
		}
	}
}
