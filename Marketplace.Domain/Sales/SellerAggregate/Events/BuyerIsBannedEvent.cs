using MediatR;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public record BuyerIsBannedEvent : INotification
	{
		public BuyerIsBannedEvent(string buyerId, string sellerId)
		{
			this.BuyerId = buyerId;
			this.SellerId = sellerId;
		}

		public string BuyerId { get; }

		public string SellerId { get; }
	}
}
