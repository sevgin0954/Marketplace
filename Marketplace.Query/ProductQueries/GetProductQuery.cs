using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.ProductQueries
{
    public class GetProductQuery : IRequest<ProductDto>
    {
        public GetProductQuery(string productId)
        {
            this.ProductId = productId;
        }

        public string ProductId { get; }

        internal class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
        {
            private readonly SalesDbContext dbContext;
            private readonly IMapper mapper;

            public GetProductQueryHandler(SalesDbContext dbContext, IMapper mapper)
            {
                this.dbContext = dbContext;
                this.mapper = mapper;
            }

            public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                var productDto = await this.dbContext.Products
                    .Where(p => p.Id == request.ProductId)
                    .ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
                    .FirstAsync(cancellationToken);

                return productDto;
            }
        }
    }
}
