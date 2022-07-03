using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Events
{
	public class OfferStartedAcceptingEvent : INotification
	{
		public OfferStartedAcceptingEvent(string productId, int quantity)
		{
			this.ProductId = productId;
			this.Quantity = quantity;
		}

		public string ProductId { get; }

		public int Quantity { get; }
	}
}
