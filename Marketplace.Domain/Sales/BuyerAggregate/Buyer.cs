using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		public Buyer(Id id)
			: base(id) { }
	}
}