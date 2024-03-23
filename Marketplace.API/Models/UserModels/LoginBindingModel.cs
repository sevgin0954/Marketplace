using AutoMapperRegistrar.Interfaces;
using Marketplace.Query.UserQueries;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.UserModels
{
	public class LoginBindingModel : IMappableTo<GetPasswordSaltQuery>, IMappableTo<LogInUserQuery>
	{
		[Required]
		[StringLength(Domain.SharedKernel.Email.MaxLength, MinimumLength = 2)]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
