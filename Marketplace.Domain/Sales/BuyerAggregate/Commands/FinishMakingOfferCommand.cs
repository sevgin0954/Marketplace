using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Commands
{
    internal class FinishMakingOfferCommand : IRequest<Result>
	{
		public FinishMakingOfferCommand(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }
		public string ProductId { get; }

		internal class FinishMakingOfferCommandHandler : IRequestHandler<FinishMakingOfferCommand, Result>
		{
			private readonly IRepository<Buyer, Id> buyerRepository;

			public FinishMakingOfferCommandHandler(IRepository<Buyer, Id> buyerRepository)
			{
				this.buyerRepository = buyerRepository;
			}

			public async Task<Result> Handle(FinishMakingOfferCommand request, CancellationToken cancellationToken)
			{
				var buyerId = new Id(request.BuyerId);
				var buyer = await this.buyerRepository.GetByIdAsync(buyerId);
				if (buyer == null)
					throw new NotFoundException(nameof(buyer));

				var productId = new Id(request.ProductId);
				buyer.FinishMakingOffer(productId);

				return Result.Ok();
			}
		}
	}
}