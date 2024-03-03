using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence
{
	public abstract class Repository<TDomainEntity, TEntityId, TPersistenceEntity> : IRepository<TPersistenceEntity, TEntityId>
		where TDomainEntity : class
		where TEntityId : Id
		where TPersistenceEntity : class
	{
		private readonly MarketplaceDbContext dbContext;

		public Repository(MarketplaceDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Add(TPersistenceEntity element)
		{
			throw new NotImplementedException();
		}

		public void Remove(TEntityId id)
		{
			throw new NotImplementedException();
		}

		public IQueryable<TPersistenceEntity> GetAll()
		{
			throw new NotImplementedException();
		}

		public IQueryable<TPersistenceEntity> GetById(TEntityId id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> CheckIfExistAsync(TEntityId id)
		{
			throw new NotImplementedException();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await this.dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
