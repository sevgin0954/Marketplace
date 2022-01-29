using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class OfferAcceptedEvent : IDomainEvent
	{
		public OfferAcceptedEvent(string productId, string buyerId)
		{
			this.ProductId = productId;
			this.BuyerId = buyerId;
		}

		public string ProductId { get; }

		public string BuyerId { get; }
	}
}
