using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.IdentityAndAccess.UserAggregate.Commands;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.UserModels
{
	public class RegisterUserBindingModel : IMappableTo<RegisterUserCommand>
	{
		[StringLength(UserConstants.MAX_USERNAME_LENGTH, MinimumLength = UserConstants.MIN_USERNAME_LENGTH)]
		public string UserName { get; set; }

		[Required]
		[StringLength(Domain.SharedKernel.Email.MaxLength, MinimumLength = 2)]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
