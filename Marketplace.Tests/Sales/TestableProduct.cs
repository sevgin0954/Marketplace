using Marketplace.Domain.Sales;

namespace Marketplace.Tests.Sales
{
	public class TestableProduct : Product
	{
		public TestableProduct(string Id, Seller creator)
			: base("defaultName", 0.1m, "defaultDescription", creator)
		{
			this.Id = Id;
		}
	}
}
