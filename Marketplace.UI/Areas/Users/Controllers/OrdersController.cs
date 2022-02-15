using AutoMapper;
using Marketplace.Domain.Shipping.OrderAggregate;
using Marketplace.Infrastructure.Identity;
using Marketplace.UI.Areas.Users.Models.OrdersModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketplace.UI.Areas.Users.Controllers
{
	public class OrdersController : BaseUsersController
	{
		private readonly IOrderRepository orderRepository;
		private readonly UserManager<User> userManager;
		private readonly IMapper mapper;

		public OrdersController(
			IOrderRepository orderRepository,
			UserManager<User> userManager,
			IMapper mapper)
		{
			this.orderRepository = orderRepository;
			this.userManager = userManager;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
			var orderAggregates = await this.orderRepository.GetByBuyerAsync(currentUser.Id);

			var model = this.mapper.Map<OrdersViewModel>(orderAggregates);

			return this.View(model);
		}
	}
}
