using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public class OfferAddedEvent : IDomainEvent
	{
		public OfferAddedEvent(string productId, string buyerId, int quantity)
		{
			ProductId = productId;
			BuyerId = buyerId;
			Quantity = quantity;
		}

		public string ProductId { get; }
		public int Quantity { get; }
		public string BuyerId { get; }
	}
}
