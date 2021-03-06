using Marketplace.Domain.Common;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class Offer : ValueObject
	{
		public Offer(string buyerId, string productId, int quantity)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.Quantity = quantity;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public int Quantity { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.BuyerId;
			yield return this.ProductId;
		}
	}
}
