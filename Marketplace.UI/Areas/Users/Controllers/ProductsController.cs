using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Infrastructure.Identity;
using Marketplace.UI.Areas.Users.Models.ProductsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Marketplace.UI.Areas.Users.Controllers
{
	public class ProductsController : BaseUsersController
	{
		private readonly IRepository<Product> productRepository;
		private readonly UserManager<User> userManager;

		public ProductsController(
			IRepository<Product> productRepository,
			UserManager<User> userManager)
		{
			this.productRepository = productRepository;
			this.userManager = userManager;
		}

		[HttpGet]
		public IActionResult AddNew()
		{
			var model = new CreateProductBindingModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> AddNew(CreateProductBindingModel model)
		{
			if (this.ModelState.IsValid == false)
				return this.View();

			var currentUser = await this.userManager.GetUserAsync(this.HttpContext.User);
			var product = new Product(model.Name, model.Price, model.Description, currentUser.Id);
			await this.productRepository.AddAsync(product);

			return this.Redirect("/");
		}
	}
}