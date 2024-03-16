using AutoMapper;
using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Marketplace.Persistence
{
	public abstract class Repository<TDomainEntity, TEntityId, TPersistenceEntity> : IRepository<TDomainEntity, TEntityId>
		where TDomainEntity : AggregateRoot
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

		public void Add(TDomainEntity element)
		{
			var persistentEntity = this.mapper.Map<TPersistenceEntity>(element);
			this.entities.Add(persistentEntity);
		}

		public async Task MarkAsDeleted(TEntityId id)
		{
			var entity = await this.entities.FindAsync(id);
			this.entities.Remove(entity);
		}

		public async Task<ICollection<TDomainEntity>> FindAsync(Expression<Func<TDomainEntity, bool>> predicate)
		{
			throw new NotImplementedException();
		}

		public ICollection<TDomainEntity> GetAll()
		{
			var persistentEntities = this.entities.AsQueryable();
			var domainEntities = this.mapper.Map<ICollection<TDomainEntity>>(persistentEntities);

			return domainEntities;
		}

		public async Task<TDomainEntity> GetByIdAsync(TEntityId id)
		{
			var peristenceEntity = await this.entities.FindAsync(id);
			var domainEntity = this.mapper.Map<TDomainEntity>(peristenceEntity);

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