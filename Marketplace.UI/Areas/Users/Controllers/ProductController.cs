using Microsoft.AspNetCore.Mvc;

namespace Marketplace.UI.Areas.Users.Controllers
{
	public class ProductController : BaseUsersController
	{
		public IActionResult AddNew()
		{
			return View();
		}
	}
}
