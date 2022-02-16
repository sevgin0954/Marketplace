using Marketplace.Domain.Sales.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class SellerRepository : ISellerRepository
	{
		private readonly SellerDbContext sellerDbContext;

		public SellerRepository(SellerDbContext sellerDbContext)
		{
			this.sellerDbContext = sellerDbContext;
		}

		public Task<int> AddAsync(Domain.Sales.SellerAggregate.Seller element)
		{
			throw new System.NotImplementedException();
		}

		public Task<IList<Domain.Sales.SellerAggregate.Seller>> GetAllAsync()
		{
			throw new System.NotImplementedException();
		}

		public Task<Domain.Sales.SellerAggregate.Seller> GetByIdAsync(string id)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IList<string>> GetNames(params string[] ids)
		{
			var sellerIDs = await this.sellerDbContext.Sellers
				.Where(s => ids.Contains(s.Id))
				.Select(s => s.Id)
				.ToListAsync();

			return sellerIDs;
		}

		public Task<int> SaveChangesAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
