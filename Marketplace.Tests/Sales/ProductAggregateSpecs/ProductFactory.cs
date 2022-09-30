using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Tests.Sales.ProductAggregateSpecs
{
	public static class ProductFactory
	{
		public static Product Create()
		{
			var sellerId = new Id();

			return Create(sellerId);
		}

		public static Product Create(Id sellerId)
		{
			var productId = new Id();
			var price = new Price(1, Currency.ADP);

			return new Product(productId, price, sellerId);
		}
	}
}
