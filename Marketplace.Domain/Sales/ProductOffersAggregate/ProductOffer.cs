using Marketplace.Domain.Common;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.ProductOffersAggregate
{
	public class ProductOffer : ValueObject
	{
		public ProductOffer(int quantity, string buyerId, string productId)
		{
			this.Quantity = quantity;
			this.BuyerId = buyerId;
			ProductId = productId;
		}

		public int Quantity { get; }
		public string BuyerId { get; }
		public string ProductId { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Quantity;
			yield return this.BuyerId;
			yield return this.ProductId;
		}
	}
}
