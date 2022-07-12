using MediatR;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record SuccessfulProductPurchaseEvent : INotification
	{
		public SuccessfulProductPurchaseEvent(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}
