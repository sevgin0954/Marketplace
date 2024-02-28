using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.IdentityAndAccess
{
	public class UserRepository : Repository<User, Id, UserEntity>
	{
		public UserRepository(IdeneityAndAccessDbContext dbContext)
			: base(dbContext) { }
	}
}
