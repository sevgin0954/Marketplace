using AutoMapper;
using Marketplace.Domain.Browsing.CategoryAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Browsing
{
	public class CategoryRepository : Repository<Category, Id, CategoryEntity>
	{
		public CategoryRepository(BrowsingDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}
