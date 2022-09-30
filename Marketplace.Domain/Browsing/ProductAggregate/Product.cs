using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Browsing.ProductAggregate
{
	public class Product : AggregateRoot<Id>
	{
		public Product(Id id, string name)
			: base(id)
		{
			this.Name = name;
		}

		public string Name { get; }
	}
}
