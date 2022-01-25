using Marketplace.Domain.Common;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales
{
	public class Offer : ValueObject
	{
		public Offer(string buyerId, string productId, string sellerId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.SellerId = sellerId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string SellerId { get; }

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.BuyerId;
			yield return this.ProductId;
			yield return this.SellerId;
		}
	}
}
