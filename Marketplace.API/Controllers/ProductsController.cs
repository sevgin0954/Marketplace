using AutoMapper;
using Marketplace.API.Models.ProductModels;
using Marketplace.API.Services;
using Marketplace.Domain.Browsing.UserAggregate.Commands;
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
		private readonly IKeywordsService keywordsService;

		public ProductsController(
            IMediator mediator, 
            IMapper mapper,
            IKeywordsService keywordsService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
			this.keywordsService = keywordsService;
		}

        [HttpGet]
        public async Task<ActionResult<ProductDto>> GetProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<ActionResult<ProductDto>> Search([FromQuery] ProductSearchBindingModel searchModel)
        {
			var isAnyKeywordExist = searchModel.KeyWords.Count > 0;
			if (isAnyKeywordExist == false)
			{
				return this.BadRequest("No keywords!");
			}
            
			var products = await this.mediator.Send(new GetFilteredProductQuery(searchModel.KeyWords!));

            var productNames = products.Select(p => p.Name);
            var matchingKeywords = this.keywordsService.GetMatchingKeywordsOrderedByImportance(productNames, searchModel.KeyWords);

            var userIdClaim = this.HttpContext.User.Claims.Where(c => c.Type == GlobalConstants.JWT_TOKEN_ID_CLAIM_NAME).FirstOrDefault();
            if (userIdClaim != null)
				this.mediator.Send(new SearchProductsCommand(userIdClaim.Value, matchingKeywords));

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