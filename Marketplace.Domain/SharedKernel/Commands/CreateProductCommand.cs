using BrowsingContext = Marketplace.Domain.Browsing.ProductAggregate;
using Marketplace.Domain.Common;
using MediatR;
using SalesContext = Marketplace.Domain.Sales.ProductAggregate;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Common.Services;

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
            private readonly IRepository<User, Id> userRepository;
            private readonly IRepository<Seller, Id> sellerRepository;
            private readonly IProductService productService;

            public CreateProductCommandHandler(
                IRepository<User, Id> userRepository,
                IRepository<Seller, Id> sellerRepository,
				IProductService productService)
            {
                this.userRepository = userRepository;
                this.sellerRepository = sellerRepository;
                this.productService = productService;
            }

            public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var isUserIdValid = await this.userRepository.CheckIfExistAsync(request.SellerId);
                if (isUserIdValid == false)
                    throw new InvalidIdException(nameof(request.SellerId));

                var isSellerExist = await this.sellerRepository.CheckIfExistAsync(request.SellerId);
                if (isSellerExist == false)
                {
                    var seller = new Seller(request.SellerId);
					this.sellerRepository.Add(seller);
                    var isSellerAddedSuccessfully = await this.sellerRepository.SaveChangesAsync() > 0;
                    if (isSellerAddedSuccessfully == false)
                        throw new NotPersistentException(nameof(seller));
				}

				var browsingProductId = new Id();
				var browsingProduct = new BrowsingContext.Product(
                    browsingProductId, request.Name, request.Description, request.SellerId);

                var salesContextId = new Id();
				var salesProduct = new SalesContext.Product(salesContextId, request.Price, request.SellerId);

				var isPersistingSuccessfull = await this.productService
                    .CreateAsync(browsingProduct, salesProduct, cancellationToken);
				if (isPersistingSuccessfull)
                    return Result.Ok();
                else
                    throw new NotPersistentException(nameof(browsingProduct), nameof(salesProduct));
			}
        }
    }
}