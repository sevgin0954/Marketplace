using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Events
{
	public class OfferWasMadeEvent : INotification
	{
		public OfferWasMadeEvent(string initiatorId, string productId)
		{
			this.InitiatorId = initiatorId;
			this.ProductId = productId;
		}

		public string InitiatorId { get; }

		public string ProductId { get; }
	}
}
