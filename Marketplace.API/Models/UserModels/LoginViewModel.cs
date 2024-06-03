using AutoMapperRegistrar.Interfaces;
using Marketplace.Query.UserQueries;

namespace Marketplace.API.Models.UserModels
{
	public class LoginViewModel : IMappableFrom<UserDto>
	{
		public string UserName { get; set; }

		public string Id { get; set; }

		public string Token { get; set; }
	}
}
