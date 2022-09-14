using MediatR;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	internal record BuyerWasNotBannedEvent : INotification
	{
		public BuyerWasNotBannedEvent(string buyerId, string sellerId)
		{
			this.BuyerId = buyerId;
			this.SellerId = sellerId;
		}

		public string BuyerId { get; }

		public string SellerId { get; }
	}
}
