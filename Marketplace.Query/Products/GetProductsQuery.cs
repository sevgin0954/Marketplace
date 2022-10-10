using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.Products
{
    public class GetProductsQuery : IRequest<IList<ProductDto>>
    {
        internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IList<ProductDto>>
        {
			private readonly SalesDbContext dbContext;
			private readonly IMapper mapper;

			public GetProductsQueryHandler(SalesDbContext dbContext, IMapper mapper)
			{
				this.dbContext = dbContext;
				this.mapper = mapper;
			}

			public async Task<IList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
				var productDtos = await this.dbContext.Products
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync(cancellationToken);

				return productDtos;
            }
        }
    }
}
