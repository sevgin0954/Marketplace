using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence
{
	// TODO: Replace TId with Id class
	public abstract class Repository<T, TId> : IRepository<T, TId>
		where T : class
		where TId : Id
	{
		private readonly MarketplaceDbContext dbContext;

		public Repository(MarketplaceDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public void Add(T element)
		{
			throw new NotImplementedException();
		}

		public Task<IList<T>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetByIdAsync(TId id)
		{
			throw new NotImplementedException();
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await this.dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}
