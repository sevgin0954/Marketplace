using AutoMapper;
using Marketplace.API.Models.ProductModels;
using Marketplace.Domain.SharedKernel.Commands;
using Marketplace.Query.ProductQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("products")]
    public class ProductsController : BaseController
	{
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public ProductsController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProducts(ProductSearchBindingModel searchModel)
        {
            var isAnyKeywordExist = searchModel.KeyWords.Count > 0;
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
                return NotFound();
            }

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> CreateNewProduct(CreateProductBindingModel model)
        {
            var createProductCommand = this.mapper.Map<CreateProductCommand>(model);
            var result = await this.mediator.Send(createProductCommand);

            if (result.IsFailure)
            {
                return BadRequest();
            }

            return Ok(result.ResultObject);
        }
    }
}