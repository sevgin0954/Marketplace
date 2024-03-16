using AutoMapper;
using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class BuyerRepository : Repository<Buyer, Id, BuyerEntity>
	{
		public BuyerRepository(SalesDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}