using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.IdentityAndAccess.UserAggregate.Commands;

namespace Marketplace.API.Models.UserModels
{
	public class RegisterUserBindingModel : IMappableTo<RegisterUserCommand>
	{
		public string UserName { get; set; }

		public string Email { get; set; }
	}
}
