using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public class OfferDeclinedEvent : IDomainEvent
	{
		public OfferDeclinedEvent(string productId, string buyerId, int quantity)
		{
			this.ProductId = productId;
			this.BuyerId = buyerId;
			Quantity = quantity;
		}

		public string ProductId { get; }
		public string BuyerId { get; }
		public int Quantity { get; }
	}
}
