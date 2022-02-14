using Marketplace.Domain.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuyerAggregate = Marketplace.Domain.Sales.BuyerAggregate;

namespace Marketplace.Infrastructure.Sales.BuyerPersistence
{
	public class BuyerRepository : IRepository<BuyerAggregate.Buyer>
	{
		public Task<int> AddAsync(BuyerAggregate.Buyer element)
		{
			throw new System.NotImplementedException();
		}

		public Task<IList<BuyerAggregate.Buyer>> GetAllAsync()
		{
			throw new System.NotImplementedException();
		}

		public Task<BuyerAggregate.Buyer> GetByIdAsync(string id)
		{
			throw new System.NotImplementedException();
		}

		public Task<int> SaveChangesAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
