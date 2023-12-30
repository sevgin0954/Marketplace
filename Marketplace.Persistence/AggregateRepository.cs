using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence
{
	public abstract class AggregateRepository<T, TId> : Repository<T, TId>, IAggregateRepository<T, TId>
		where T : AggregateRoot<TId> 
		where TId : Id
	{
		public AggregateRepository(MarketplaceDbContext dbContext)
			: base(dbContext) { }
	}
}