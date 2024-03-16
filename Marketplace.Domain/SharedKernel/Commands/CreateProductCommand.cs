using BrowsingContext = Marketplace.Domain.Browsing.ProductAggregate;
using Marketplace.Domain.Common;
using MediatR;
using SalesContext = Marketplace.Domain.Sales.ProductAggregate;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Domain.Browsing.ProductAggregate;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.Sales.SellerAggregate;

namespace Marketplace.Domain.SharedKernel.Commands
{
    public class CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Price Price { get; set; } = new Price(0, Currency.BGN);

        public Id SellerId { get; set; } = new Id();

        internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
        {
            private readonly IRepository<Product, Id> browsingProductRepository;
            private readonly IRepository<SalesContext.Product, Id> salesProductRepository;
            private readonly IRepository<User, Id> userRepository;

            public CreateProductCommandHandler(
                IRepository<BrowsingContext.Product, Id> browsingProductRepository,
                IRepository<SalesContext.Product, Id> salesProductRepository,
                IRepository<User, Id> userRepository)
            {
                this.browsingProductRepository = browsingProductRepository;
                this.salesProductRepository = salesProductRepository;
                this.userRepository = userRepository;
            }

            public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var isSellerIdValid = await this.userRepository.CheckIfExistAsync(request.SellerId);
                if (isSellerIdValid == false)
                    throw new InvalidIdException(nameof(request.SellerId));

				var browsingProductId = new Id();
				var browsingProduct = new BrowsingContext.Product(
                    browsingProductId, request.Name, request.Description, request.SellerId);

                var salesContextId = new Id();
				var salesProduct = new SalesContext.Product(salesContextId, request.Price, request.SellerId);

                this.browsingProductRepository.Add(browsingProduct);
                this.salesProductRepository.Add(salesProduct);

                var browsingProductsRowsChanged = await this.browsingProductRepository.SaveChangesAsync(cancellationToken);
                if (browsingProductsRowsChanged == 0)
					throw new NotPersistentException(nameof(browsingProduct));
				if (browsingProductsRowsChanged > 0)
                {
					var salesProductRowsChanged = await this.salesProductRepository.SaveChangesAsync(cancellationToken);
                    if (salesProductRowsChanged == 0)
                    {
                        await this.browsingProductRepository.MarkAsDeleted(browsingProduct.Id);
                        await this.browsingProductRepository.SaveChangesAsync();
						throw new NotPersistentException(nameof(salesProduct));
					}
				}
                
                return Result.Ok();
			}
        }
    }
}