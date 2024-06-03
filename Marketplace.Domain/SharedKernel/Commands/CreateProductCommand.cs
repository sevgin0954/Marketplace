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
using System.Collections.Generic;
using System.Linq;
using Marketplace.Domain.Browsing.CategoryAggregate;

namespace Marketplace.Domain.SharedKernel.Commands
{
    public class CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Price Price { get; set; }

        public Id SellerId { get; set; }

        public IEnumerable<string> ImageIds { get; set; }

        public Category Category { get; set; }

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
                    var isSellerAddedSuccessfully = await this.sellerRepository.SaveChangesAsync();
                    if (isSellerAddedSuccessfully == false)
                        throw new NotPersistentException(nameof(seller));
				}

				var productId = new Id();
                var images = request.ImageIds.Select((id, n) => new Image(id, n));
				var browsingProduct = new BrowsingContext.Product(
                    productId, request.Name, 
                    request.Description, 
                    request.SellerId, 
                    images,
                    request.Category);

				var salesProduct = new SalesContext.Product(productId, request.Price, request.SellerId);

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