using MediatR;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	// TODO: Make event imutable
	public class ProductCouldNotBeBoughtEvent : INotification
	{
		public ProductCouldNotBeBoughtEvent(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}
