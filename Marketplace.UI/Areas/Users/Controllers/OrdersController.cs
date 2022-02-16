using AutoMapper;
using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Shipping.OrderAggregate;
using Marketplace.Infrastructure.Identity;
using Marketplace.UI.Areas.Users.Models.OrdersModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.UI.Areas.Users.Controllers
{
	public class OrdersController : BaseUsersController
	{
		private readonly IOrderRepository orderRepository;
		private readonly UserManager<User> userManager;
		private readonly ISellerRepository sellerRepository;
		private readonly IMapper mapper;

		public OrdersController(
			IOrderRepository orderRepository,
			UserManager<User> userManager,
			ISellerRepository sellerRepository,
			IMapper mapper)
		{
			this.orderRepository = orderRepository;
			this.userManager = userManager;
			this.sellerRepository = sellerRepository;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
			var orderAggregates = await this.orderRepository.GetByBuyerAsync(currentUser.Id);

			var model = new OrdersViewModel
			{
				TotalOrders = orderAggregates.Count,
				Orders = await this.MapOrderModels(orderAggregates)
			};

			return this.View(model);
		}

		private async Task<IList<OrderViewModel>> MapOrderModels(IList<Order> orders)
		{
			var orderModels = new List<OrderViewModel>();

			foreach (var order in orders)
			{
				var orderModel = this.mapper.Map<OrderViewModel>(order);
				var sellerName = (await this.sellerRepository.GetNames(order.SellerId)).FirstOrDefault();
				orderModel.SellerName = sellerName;
			}

			return orderModels;
		}
	}
}