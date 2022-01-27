using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.Sales.SellerAggregate;

namespace Marketplace.Tests.Sales
{
	public class TestableProduct : Product
	{
		public TestableProduct(Seller creator)
			: this("123", creator) { }

		public TestableProduct(string Id, Seller creator)
			: base("defaultName", 0.1m, "defaultDescription", creator)
		{
			this.Id = Id;
		}
	}
}
