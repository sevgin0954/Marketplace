using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate.Commands
{
    internal class CheckCanBuyProductCommand : IRequest<Result>
	{
		public CheckCanBuyProductCommand(string productId, string initiatorId)
		{
			this.ProductId = productId;
			this.InitiatorId = initiatorId;
		}

		public string ProductId { get; }

		public string InitiatorId { get; }

		internal class CheckCanBuyProductCommandHandler : IRequestHandler<CheckCanBuyProductCommand, Result>
		{
			private readonly IRepository<Product, Id> productRepository;

			public CheckCanBuyProductCommandHandler(IRepository<Product, Id> productRepository)
			{
				this.productRepository = productRepository;
			}

			public async Task<Result> Handle(CheckCanBuyProductCommand request, CancellationToken cancellationToken)
			{
				var productId = new Id(request.ProductId);
				var product = await this.productRepository.GetByIdAsync(productId);
				if (product == null)
					throw new NotFoundException(nameof(product));

				var initiatorId = new Id(request.InitiatorId);
				product.CheckIsEligibleForBuyEventCheck(initiatorId);

				return Result.Ok();
			}
		}
	}
}