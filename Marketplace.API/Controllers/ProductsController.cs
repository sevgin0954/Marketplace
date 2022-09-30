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
        public async Task<ActionResult<ProductDto>> GetProducts()
        {
            var products = await this.mediator.Send(new GetProductsQuery());

            return Ok(products);
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
