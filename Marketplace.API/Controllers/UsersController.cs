using Marketplace.API.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
	[Route("users")]
	public class UsersController : Controller
	{
		private readonly IMediator mediator;

		public UsersController(IMediator mediator)
		{
			this.mediator = mediator;
		}

		[HttpPost]
		public IActionResult Register(RegisterUserBindingModel model)
		{


			return this.Ok();
		}
	}
}
