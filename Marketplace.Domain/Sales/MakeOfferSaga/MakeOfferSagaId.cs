using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.MakeOfferSaga
{
	// TODO: Should I make struct???
	public record MakeOfferSagaId : Id
	{
		public MakeOfferSagaId(string buyerId, string productId)
			: base(buyerId + productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}