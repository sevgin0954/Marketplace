using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public class OfferDeclinedEvent : IDomainEvent
	{
		public OfferDeclinedEvent(string productId, string buyerId)
		{
			this.ProductId = productId;
			this.BuyerId = buyerId;
		}

		public string ProductId { get; }

		public string BuyerId { get; }
	}
}
