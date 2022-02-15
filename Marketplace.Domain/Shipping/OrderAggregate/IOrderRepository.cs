using Marketplace.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Shipping.OrderAggregate
{
	public interface IOrderRepository : IRepository<Order>
	{
		public Task<IList<Order>> GetByBuyerAsync(string buyerId);
	}
}
