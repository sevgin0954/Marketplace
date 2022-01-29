using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class OfferCreatedEvent : IDomainEvent
	{
		public OfferCreatedEvent(string sellerId, string productId, string buyerId)
		{
			this.SellerId = sellerId;
			this.ProductId = productId;
			this.BuyerId = buyerId;
		}

		public string SellerId { get; }

		public string ProductId { get; }

		public string BuyerId { get; }
	}
}
