using MediatR;

namespace Marketplace.Query.Products
{
    public class GetProductsQuery : IRequest<IList<ProductDto>>
    {
        internal class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IList<ProductDto>>
        {
            public async Task<IList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
