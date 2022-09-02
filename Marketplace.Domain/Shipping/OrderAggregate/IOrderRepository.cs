using Marketplace.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Shipping.OrderAggregate
{
	public interface IOrderRepository : IAggregateRepository<Order>
	{
		public Task<IList<Order>> GetByBuyerAsync(string buyerId);
	}
}
