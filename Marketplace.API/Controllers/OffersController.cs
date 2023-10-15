using Marketplace.API.Models.OfferModels;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
	[Route("offers")]
	public class OffersController : BaseController
	{
		[HttpGet]
		public ActionResult GetOffers([FromQuery]OffersBindingModel model)
		{
			return this.Ok();
		}
	}
}
