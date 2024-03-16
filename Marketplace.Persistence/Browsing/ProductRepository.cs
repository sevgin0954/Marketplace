using AutoMapper;
using Marketplace.Domain.Browsing.ProductAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Browsing
{
	public class ProductRepository : Repository<Product, Id, ProductEntity>
	{
		public ProductRepository(BrowsingDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}