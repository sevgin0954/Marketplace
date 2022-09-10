using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public record ProductId : Id
	{
		public ProductId(string id)
		{
			this.Id = id;
		}

		public string Id { get; }
	}
}
