using MediatR;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public record BuyerIsNotBannedEvent : INotification
	{
		public BuyerIsNotBannedEvent(string buyerId, string sellerId)
		{
			this.BuyerId = buyerId;
			this.SellerId = sellerId;
		}

		public string BuyerId { get; }

		public string SellerId { get; }
	}
}
