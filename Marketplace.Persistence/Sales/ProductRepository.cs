using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class ProductRepository : Repository<ProductEntity, Id>
	{
		public ProductRepository(MarketplaceDbContext dbContext)
			: base(dbContext) { }
	}
}
