using AutoMapper;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.IdentityAndAccess
{
	public class UserRepository : Repository<User, Id, UserEntity>
	{
		public UserRepository(IdentityAndAccessDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}