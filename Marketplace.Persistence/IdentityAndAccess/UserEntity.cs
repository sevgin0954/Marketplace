using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.IdentityAndAccess
{
	public class UserEntity
	{
		public string Id { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }
	}
}
