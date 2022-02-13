using Marketplace.UI.Areas.Auth.Models.LoginModels;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.UI.Areas.Auth.Controllers
{
	public class Login : AuthBaseController
	{
		[HttpGet]
		public IActionResult Index()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult Index(LoginBindingModel model)
		{
			return this.View();
		}
	}
}
