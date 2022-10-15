using Marketplace.API.Models.ProductModels;
using Marketplace.Query.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProducts([FromQuery]ProductSearchBindingModel searchModel)
        {
            var isAnyKeywordExist = searchModel.KeyWords != null && searchModel.KeyWords.Count > 0;
			if (isAnyKeywordExist)
            {
                var filteredProducts = await this.mediator.Send(new GetFilteredProductQuery(searchModel.KeyWords!));
				return this.Ok(filteredProducts);
            }

			var products = await this.mediator.Send(new GetAllProductsQuery());
			return this.Ok(products);
		}

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var product = await this.mediator.Send(new GetProductQuery(id));

            if (product == null)
            {
                return NotFound(product);
            }

            return Ok(product);
        }
    }
}