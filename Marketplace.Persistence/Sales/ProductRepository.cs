using AutoMapper;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class ProductRepository : Repository<Domain.Sales.ProductAggregate.Product, Id, ProductEntity>
	{
		public ProductRepository(SalesDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}
