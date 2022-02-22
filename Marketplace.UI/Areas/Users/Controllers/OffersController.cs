using AutoMapper;
using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Infrastructure.Identity;
using Marketplace.UI.Areas.Users.Models.OffersModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.UI.Areas.Users.Controllers
{
	public class OffersController : BaseUsersController
	{
		private readonly ISellerRepository sellerRepository;
		private readonly UserManager<User> userManager;
		private readonly IMapper mapper;

		public OffersController(
			ISellerRepository sellerRepository,
			UserManager<User> userManager,
			IMapper mapper)
		{
			this.sellerRepository = sellerRepository;
			this.userManager = userManager;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
			var seller = await this.sellerRepository.GetByIdAsync(currentUser.Id);

			var model = this.mapper.Map<OffersViewModel>(seller);
			this.MapOffers(seller.ReceivedOffers.ToList(), model.PendingOffers);
			this.MapOffers(seller.DeclinedOffers.ToList(), model.DeclinedOffers);
			// this.MapOffers(seller.SoldProductIds.ToList(), model.AcceptedOffers);

			return View(model);
		}

		private void MapOffers(IList<Offer> src, IList<OfferViewModel> dest)
		{

		}
	}
}