using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class BuyerRepository : Repository<BuyerEntity, Id>
	{
		public BuyerRepository(MarketplaceDbContext dbContext)
			: base(dbContext) { }
	}
}