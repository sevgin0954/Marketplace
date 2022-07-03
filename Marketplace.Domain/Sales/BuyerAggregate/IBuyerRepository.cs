using Marketplace.Domain.Common;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public interface IBuyerRepository : IRepository<Buyer>
	{
		Task<Buyer> GetByPendingProductIdAsync(string productId);
		Task<string> GetIdsByPendingProductIdAsync(string productId);
	}
}
