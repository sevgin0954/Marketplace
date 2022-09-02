using MediatR;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record SuccessfulProductPurchaseEvent : INotification
	{
		public SuccessfulProductPurchaseEvent(string productId, string initiatorId)
		{
			this.ProductId = productId;
			this.InitiatorId = initiatorId;
		}

		public string InitiatorId { get; }

		public string ProductId { get; }
	}
}
