using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence
{
	public abstract class Repository<TDomainEntity, TEntityId, TPersistenceEntity> : IRepository<TDomainEntity, TEntityId>
		where TDomainEntity : class
		where TEntityId : Id
		where TPersistenceEntity : class
	{
		private readonly MarketplaceDbContext dbContext;

		public Repository(MarketplaceDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Add(TDomainEntity element)
		{
			throw new NotImplementedException();
		}

		public Task<IList<TDomainEntity>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<TDomainEntity> GetByIdAsync(TEntityId id)
		{
			throw new NotImplementedException();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await this.dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
