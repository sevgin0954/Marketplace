using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.IdentityAndAccess
{
	public class UserEntity
	{
		public string Id { get; set; }

		public string UserName { get; }

		public string Email { get; }
	}
}
