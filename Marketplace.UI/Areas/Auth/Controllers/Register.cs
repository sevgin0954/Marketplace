using Marketplace.Infrastructure.Identity;
using Marketplace.UI.Areas.Auth.Models.RegisterModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketplace.UI.Areas.Auth.Controllers
{
	public class Register : AuthBaseController
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;

		public Register(
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return this.View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(RegisterBindingModel model)
		{
			var newUser = new User();
			newUser.Email = model.Email;
			newUser.UserName = model.Email;

			var result = await this.userManager.CreateAsync(newUser, model.Password);
			if (result.Succeeded)
			{
				var isPersistent = false;
				await this.signInManager.SignInAsync(newUser, isPersistent);
			}

			return this.Redirect("/");
		}
	}
}
