using AutoMapperRegistrar.Interfaces;
using Marketplace.Persistence.IdentityAndAccess;

namespace Marketplace.Query.UserQueries
{
	public class UserDto : IMappableFrom<UserEntity>
	{
		public string Id { get; set; }

		public string Username { get; set; }
	}
}
