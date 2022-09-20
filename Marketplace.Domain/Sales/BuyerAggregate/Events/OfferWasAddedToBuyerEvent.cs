using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Events
{
	internal class OfferWasAddedToBuyerEvent : INotification
	{
		public OfferWasAddedToBuyerEvent(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }
		public string ProductId { get; }
	}
}
