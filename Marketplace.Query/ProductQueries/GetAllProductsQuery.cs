using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.ProductQueries
{
    public class GetAllProductsQuery : IRequest<IList<ProductDto>>
    {
        internal class GetProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IList<ProductDto>>
        {
			private readonly SalesDbContext dbContext;
			private readonly IMapper mapper;

			public GetProductsQueryHandler(SalesDbContext dbContext, IMapper mapper)
			{
				this.dbContext = dbContext;
				this.mapper = mapper;
			}

			public async Task<IList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
				var productDtos = await this.dbContext.Products
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				return productDtos;
            }
        }
    }
}
