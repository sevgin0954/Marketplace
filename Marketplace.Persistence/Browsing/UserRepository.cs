using AutoMapper;
using Marketplace.Domain.Browsing.UserAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Browsing
{
	public class UserRepository : Repository<User, Id, UserEntity>
	{
		public UserRepository(BrowsingDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}
