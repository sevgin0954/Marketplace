using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public class OfferId : Id
	{
		public OfferId(string productId, string buyerId)
			: base(productId + buyerId) { }
	}
}
