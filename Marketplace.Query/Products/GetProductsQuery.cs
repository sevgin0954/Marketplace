using AutoMapper;
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
				var products = await this.dbContext.Products.ToListAsync(cancellationToken);
				var dtos = this.mapper.Map<IList<ProductDto>>(products);

				return dtos;
            }
        }
    }
}
