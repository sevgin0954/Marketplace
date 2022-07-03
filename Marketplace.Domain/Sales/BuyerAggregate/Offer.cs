using Marketplace.Domain.Common;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.BuyerProductOffersAggregate
{
	public class Offer : ValueObject
	{
		public Offer(int quantity)
		{
			this.Quantity = quantity;
		}

		public int Quantity { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Quantity;
		}
	}
}
