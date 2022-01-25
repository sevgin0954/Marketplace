using Marketplace.Domain.Common;

namespace Marketplace.Domain.Shipping
{
	public class User : AggregateRoot
	{
		public string Name { get; private set; }
	}
}
