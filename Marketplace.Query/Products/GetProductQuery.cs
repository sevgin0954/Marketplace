using AutoMapper;
using Marketplace.Persistence.Sales;
using MediatR;

namespace Marketplace.Query.Products
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
                var product = await this.dbContext.Products.FindAsync(request.ProductId, cancellationToken);
                if (product == null)
                    throw new KeyNotFoundException(nameof(product));

                var dto = this.mapper.Map<ProductDto>(product);

                return dto;
            }
        }
    }
}
