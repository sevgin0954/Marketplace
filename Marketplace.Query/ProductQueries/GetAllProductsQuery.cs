using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Browsing;
using Marketplace.Persistence.IdentityAndAccess;
using Marketplace.Persistence.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime;

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
				var browsingProductDtos = await this.browsingDbContext.Products
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				var salesProductDtos = await this.salesDdContext.Products
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				var identityAndAcessProductDtos = await this.identityAndAccessDbContext.Users
					.Where(u => browsingProductDtos.Select(p => p.SellerId).Contains(u.Id))
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				var combinedProducts = salesProductDtos
					.Join(browsingProductDtos, sp => sp.Id, bp => bp.Id, (salesProduct, browsingProduct) => new ProductDto()
					{
						Id = browsingProduct.Id,
						Name = browsingProduct.Name,
						Price = salesProduct.Price,
						SellerId = salesProduct.SellerId,
						Status = salesProduct.Status
					}).Join(identityAndAcessProductDtos, p => p.SellerId, i => i.Id, (product, identityProduct) => new ProductDto()
					{
						Id = product.Id,
						Name = product.Name,
						Price = product.Price,
						SellerId = identityProduct.Id,
						Status = product.Status,
						SellerName = identityProduct.SellerName
					}).ToList();

				return combinedProducts;
            }
        }
    }
}
