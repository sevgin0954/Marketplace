using Marketplace.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public interface ISellerRepository : IRepository<Seller>
	{
		Task<IList<string>> GetNames(params string[] ids);
	}
}
