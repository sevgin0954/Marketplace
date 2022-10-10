using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Persistence.Sales;

namespace Marketplace.Persistence
{
	public class AggregatesAndEntityTypes
	{
		private Dictionary<Type, Type> entitiesAndAggregatesTypes;
		private Dictionary<Type, Type> aggregatesAndEntitiesTypes;

		public AggregatesAndEntityTypes()
		{
			this.entitiesAndAggregatesTypes = new Dictionary<Type, Type>()
			{
				{ typeof(Buyer), typeof(BuyerEntity) }
			};
			this.aggregatesAndEntitiesTypes = this.entitiesAndAggregatesTypes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
		}

		public IReadOnlyDictionary<Type, Type> EntitiesAndAggregatesTypes => this.entitiesAndAggregatesTypes;

		public IReadOnlyDictionary<Type, Type> AggregatesAndEntitiesTypes => this.aggregatesAndEntitiesTypes;

		public Type GetCorrespondingEntityType(Type aggregateType)
		{
			return this.aggregatesAndEntitiesTypes[aggregateType];
		}

		public Type GetCorrespondingAggregateType(Type entityType)
		{
			return this.entitiesAndAggregatesTypes[entityType];
		}
	}
}
