using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Tests.Sales
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
			var price = 1m;

			return new Product(productId, price, sellerId);
		}
	}
}
