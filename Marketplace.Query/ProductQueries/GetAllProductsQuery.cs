using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Browsing;
using Marketplace.Persistence.IdentityAndAccess;
using Marketplace.Persistence.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.ProductQueries
{
    public class GetAllProductsQuery : IRequest<IList<ProductDto>>
    {
        internal class GetProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
        {
			private readonly SalesDbContext salesDdContext;
			private readonly BrowsingDbContext browsingDbContext;
			private readonly IdentityAndAccessDbContext identityAndAccessDbContext;
			private readonly IMapper mapper;
			public GetProductsQueryHandler(
				SalesDbContext salesDbContext,
				BrowsingDbContext browsingDbContext,
				IdentityAndAccessDbContext identityAndAccessDbContext,
				IMapper mapper)
			{
				this.salesDdContext = salesDbContext;
				this.browsingDbContext = browsingDbContext;
				this.identityAndAccessDbContext = identityAndAccessDbContext;
				this.mapper = mapper;
			}

			public async Task<IList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
			{
				var salesProductDtos = await this.salesDdContext.Products
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				var browsingProductDtos = await this.browsingDbContext.Products
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				var identityAndAcessProductDtos = await this.identityAndAccessDbContext.Users
					.Where(u => browsingProductDtos.Select(p => p.SellerId).Contains(u.Id))
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync();

				var combinedProductDtoes = browsingProductDtos
					.Join(
						salesProductDtos,
						browsingProduct => browsingProduct.Id,
						salesProduct => salesProduct.Id,
						(browsingProduct, salesProduct) => new { id = browsingProduct.Id, browsingProduct, salesProduct }
					).Join(
						identityAndAcessProductDtos,
						browsingAndSalesProduct => browsingAndSalesProduct.id,
						identityAndAccessProduct => identityAndAccessProduct.Id,
						(browsingAndSalesProduct, identityAndAccessProduct) => new ProductDto()
						{
							Id = browsingAndSalesProduct.id,
							Name = browsingAndSalesProduct.browsingProduct.Name,
							Price = browsingAndSalesProduct.salesProduct.Price,
							SellerId = browsingAndSalesProduct.salesProduct.SellerId,
							SellerName = identityAndAccessProduct.SellerName,
							Status = browsingAndSalesProduct.salesProduct.Status
						}).ToList();

				return combinedProductDtoes;
            }
        }
    }
}
