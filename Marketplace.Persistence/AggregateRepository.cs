using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Marketplace.Persistence
{
	public class AggregateRepository<T, TId> : IAggregateRepository<T, TId> where T : AggregateRoot<TId> where TId : Id
	{
		private readonly MarketplaceDbContext dbContext;
		private readonly DbSet<T> dbSet;

		public AggregateRepository(MarketplaceDbContext dbContext)
		{
			this.dbContext = dbContext;
			this.dbSet = dbContext.Set<T>();
		}

		public void Add(T element)
		{
			this.dbContext.Add(element);
		}
		// TODO: MAP FROM ENTITY TO AGGREGATE
		public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await this.dbSet.Where(predicate).ToListAsync();
		}

		public async Task<IList<T>> GetAllAsync()
		{
			return await this.dbSet.ToListAsync();
		}

		public async Task<T> GetByIdAsync(TId id)
		{
			return await this.dbSet.FindAsync(id);
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await this.dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}