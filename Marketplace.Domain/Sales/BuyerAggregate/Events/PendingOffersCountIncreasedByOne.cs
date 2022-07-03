using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Events
{
	public class PendingOffersCountIncreasedByOne : INotification
	{
		public PendingOffersCountIncreasedByOne(string productId, int quantity)
		{
			this.ProductId = productId;
			this.Quantity = quantity;
		}

		public string ProductId { get; }
		public int Quantity { get; }
	}
}
