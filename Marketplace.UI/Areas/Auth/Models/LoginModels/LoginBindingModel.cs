using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.UI.Areas.Auth.Models.LoginModels
{
	public class LoginBindingModel
	{
		[Required]
		public string Email { get; set; }

		[Required]
		[Compare(nameof(ConfirmPassword))]
		public string Password { get; set; }

		[Required]
		[DisplayName("Confirm password")]
		public string ConfirmPassword { get; set; }

		[Required]
		public bool RememberMe { get; set; }
	}
}
