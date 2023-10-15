using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Marketplace.Shared;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	internal record Offer : ValueObject
	{
		public Offer(Id buyerId, Id productId, int quantity)
		{
			ArgumentValidator.NotNullValidator(buyerId, nameof(buyerId));
			ArgumentValidator.NotNullValidator(productId, nameof(productId));

			var minValue = 1;
			ArgumentValidator.MinValue(quantity, minValue, nameof(quantity));

			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.Quantity = quantity;
		}

		public Id BuyerId { get; }

		public Id ProductId { get; }

		public int Quantity { get; }
	}
}
