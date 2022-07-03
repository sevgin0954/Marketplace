using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Events
{
	public class StartAddingOfferEvent : INotification
	{
		public StartAddingOfferEvent(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }
		public string ProductId { get; }
	}
}