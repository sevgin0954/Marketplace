using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public record OfferId : Id
	{
		public OfferId(Id productId, Id buyerId)
			: base(productId.Value + buyerId.Value)
		{
			this.ProductId = productId;
			this.BuyerId = buyerId;
		}

		public Id ProductId { get; }

		public Id BuyerId { get; }
	}
}
