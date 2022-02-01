using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public class ProductArchivedEvent : IDomainEvent
	{
		public ProductArchivedEvent(string productId)
		{
			this.ProductId = productId;
		}

		public string ProductId { get; }
	}
}
