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
            public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
