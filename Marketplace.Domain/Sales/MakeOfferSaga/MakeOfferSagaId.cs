using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.MakeOfferSaga
{
	public class MakeOfferSagaId : Id
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