using AutoMapper;
using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Domain.SharedKernel;
using Marketplace.Persistence.Sales;

namespace Marketplace.Persistence
{
	public class PersistenceMapper
	{
		private readonly IMapper mapper;

		public PersistenceMapper(IMapper mapper)
		{

		}

		public object MapToEntity<TAggregate, TAggregateId>(TAggregate aggregate) 
			where TAggregate : AggregateRoot<TAggregateId> 
			where TAggregateId : Id
		{
			var aggregateType = aggregate.GetType();
			// var entityType = this.GetCorrespondingEntityType(aggregateType);
			// var entity = this.mapper.Map(aggregate, aggregateType, entityType);
			throw new NotImplementedException();
			// return entity;
		}
	}
}
