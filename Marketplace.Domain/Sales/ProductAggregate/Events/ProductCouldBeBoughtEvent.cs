using MediatR;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public class ProductCouldBeBoughtEvent : INotification
	{
		public ProductCouldBeBoughtEvent(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}
