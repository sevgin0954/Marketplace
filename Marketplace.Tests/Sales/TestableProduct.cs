using Marketplace.Domain.Sales.ProductAggregate;

namespace Marketplace.Tests.Sales
{
	public class TestableProduct : Product
	{
		public TestableProduct(string creatorId)
			: this("123", creatorId) { }

		public TestableProduct(string Id, string creatorId)
			: base("defaultName", 0.1m, "defaultDescription", creatorId)
		{
			this.Id = Id;
		}
	}
}
