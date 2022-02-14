using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.UI.Areas.Users.Controllers
{
	[Authorize]
	[Area("Users")]
	public abstract class BaseUsersController : Controller
	{
	}
}
