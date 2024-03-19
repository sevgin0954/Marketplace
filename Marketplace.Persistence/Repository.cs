using AutoMapper;
using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Marketplace.Persistence
{
	public abstract class Repository<TDomainAggregate, TEntityId, TPersistenceEntity> : IRepository<TDomainAggregate, TEntityId>
		where TDomainAggregate : AggregateRoot
		where TEntityId : Id
		where TPersistenceEntity : class
	{
		protected readonly MarketplaceDbContext dbContext;
		private readonly IMapper mapper;
		protected readonly DbSet<TPersistenceEntity> entities;

		public Repository(MarketplaceDbContext dbContext, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
			this.entities = dbContext.Set<TPersistenceEntity>();
		}

		public void Add(TDomainAggregate element)
		{
			var persistentEntity = this.mapper.Map<TPersistenceEntity>(element);
			this.entities.Add(persistentEntity);
		}

		public async Task MarkAsDeleted(TEntityId id)
		{
			var entity = await this.entities.FindAsync(id);
			this.entities.Remove(entity);
		}

		public async Task<ICollection<TDomainAggregate>> FindAsync(Expression<Func<TDomainAggregate, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public ICollection<TDomainAggregate> GetAll()
		{
			var persistentEntities = this.entities.AsQueryable();
			var domainEntities = this.mapper.Map<ICollection<TDomainAggregate>>(persistentEntities);

			return domainEntities;
		}

		public async Task<TDomainAggregate> GetByIdAsync(TEntityId id)
		{
			var peristenceEntity = await this.entities.FindAsync(id);
			var domainEntity = this.mapper.Map<TDomainAggregate>(peristenceEntity);

			return domainEntity;
		}

		public async Task<bool> CheckIfExistAsync(TEntityId id)
		{
			return await this.entities.FindAsync(id) == null;;
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return await this.dbContext.SaveChangesAsync(cancellationToken);
		}
	}
}