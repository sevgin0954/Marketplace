using MediatR;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	internal class ProductCouldNotBeBoughtEvent : INotification
	{
		public ProductCouldNotBeBoughtEvent(string buyerId, string productId, string reason)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.Reason = reason;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string Reason { get; }
	}
}
