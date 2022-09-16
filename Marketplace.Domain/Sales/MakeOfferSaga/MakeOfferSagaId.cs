using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS
{
	public record MakeOfferSagaId : Id
	{
		public MakeOfferSagaId(Id buyerId, Id productId)
			: base(buyerId.Value + productId.Value)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public Id BuyerId { get; }

		public Id ProductId { get; }
	}
}