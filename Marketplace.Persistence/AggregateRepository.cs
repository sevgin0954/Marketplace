using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence
{
	public class AggregateRepository<T, TId> : IAggregateRepository<T, TId> where T : AggregateRoot<TId> where TId : Id
	{
		private readonly MarketplaceDbContext dbContext;
		private readonly PersistenceMapper mapper;
		private readonly AggregatesAndEntityTypes aggregatesAndEntityTypes;

		public AggregateRepository(
			MarketplaceDbContext dbContext,
			PersistenceMapper mapper,
			AggregatesAndEntityTypes aggregatesAndEntityTypes)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
			this.aggregatesAndEntityTypes = aggregatesAndEntityTypes;
		}

		public void Add(T aggregate)
		{
			var entity = this.mapper.MapToEntity<T, TId>(aggregate);
			this.dbContext.Add(entity);
		}

		public Task<IList<T>> GetAllAsync()
		{
			//var entityType = this.aggregatesAndEntityTypes.GetCorrespondingEntityType(typeof(T));

			//var entities = this.dbContext.GetType().GetProperty(entityType.Name).GetValue(this.dbContext);

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