using AutoMapper;
using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.UI.Models.HomeModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.UI.Controllers
{
	public class HomeController : Controller
	{
		private readonly IRepository<Product> productRepository;
		private readonly IMapper mapper;

		public HomeController(
			IRepository<Product> productRepository,
			IMapper mapper)
		{
			this.productRepository = productRepository;
			this.mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var allProducts = await this.productRepository.GetAll();
			var productModels = this.mapper.Map<ICollection<ProductViewModel>>(allProducts);
			var model = new HomeViewModel()
			{
				Products = productModels
			};

			return View(model);
		}

		public IActionResult Privacy()
		{
			return View();
		}
	}
}