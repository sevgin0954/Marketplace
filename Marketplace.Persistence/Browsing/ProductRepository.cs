using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Browsing
{
	public class ProductRepository : Repository<ProductEntity, Id>
	{
		public ProductRepository(MarketplaceDbContext dbContext)
			: base(dbContext) { }
	}
}