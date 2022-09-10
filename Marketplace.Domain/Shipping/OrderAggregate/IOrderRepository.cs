using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Shipping.OrderAggregate
{
	public interface IOrderRepository : IAggregateRepository<Order, Id>
	{
		public Task<IList<Order>> GetByBuyerAsync(string buyerId);
	}
}
