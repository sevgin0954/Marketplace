using AutoMapperRegistrar.Interfaces;
using Marketplace.Query.UserQueries;

namespace Marketplace.API.Models.UserModels
{
	public class LoginBindingModel : IMappableTo<GetPasswordSaltQuery>, IMappableTo<LogInUserQuery>
	{
		public string Email { get; set; }

		public string Password { get; set; }
	}
}
