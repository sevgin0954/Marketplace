using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Tests.Sales.BuyerAggregateSpecs
{
	internal static class BuyerFactory
	{
		public static Buyer Create()
		{
			var id = new Id();
			var pendingOffersCount = 1;

			return new Buyer(id, pendingOffersCount);
		}
	}
}
