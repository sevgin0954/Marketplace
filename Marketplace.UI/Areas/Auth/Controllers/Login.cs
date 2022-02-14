using Marketplace.Infrastructure.Identity;
using Marketplace.UI.Areas.Auth.Models.LoginModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketplace.UI.Areas.Auth.Controllers
{
	public class Login : AuthBaseController
	{
		private readonly SignInManager<User> signInManager;

		public Login(SignInManager<User> signInManager)
		{
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return this.View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginBindingModel model)
		{
			if (this.ModelState.IsValid == false)
			{
				return this.View();
			}
			
			var lockOnFailure = true;
			var result = await this.signInManager
				.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockOnFailure);

			if (result.Succeeded)
			{
				return this.Redirect("/");
			}
			else
			{
				return this.View();
			}
		}
	}
}