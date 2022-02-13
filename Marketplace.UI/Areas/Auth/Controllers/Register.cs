using Marketplace.UI.Areas.Auth.Models.Register;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.UI.Areas.Auth.Controllers
{
	public class Register : AuthBaseController
	{
		[HttpGet]
		public IActionResult Index()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult Index(RegisterBindingModel model)
		{
			return this.View();
		}
	}
}
