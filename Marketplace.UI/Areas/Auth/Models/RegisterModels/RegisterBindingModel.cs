using System.ComponentModel.DataAnnotations;

namespace Marketplace.UI.Areas.Auth.Models.RegisterModels
{
	public class RegisterBindingModel
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
