using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class ProductRepository : Repository<Product, Id, ProductEntity>
	{
		public ProductRepository(SalesDbContext dbContext)
			: base(dbContext) { }
	}
}
