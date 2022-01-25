using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.Events
{
	public class ProbuctPublishedForSaleEvent : IDomainEvent
	{
		public ProbuctPublishedForSaleEvent(string productId)
		{
			this.ProductId = productId;
		}

		public string ProductId { get; }
	}
}
