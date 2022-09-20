using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Commands
{
	public class DiscardMakingOfferCommand : IRequest<Result>
	{
		public DiscardMakingOfferCommand(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }
		public string ProductId { get; }

		internal class DiscardMakingOfferCommandHandler : IRequestHandler<DiscardMakingOfferCommand, Result>
		{
			private readonly IRepository<Buyer, Id> buyerRepository;

			public DiscardMakingOfferCommandHandler(IRepository<Buyer, Id> buyerRepository)
			{
				this.buyerRepository = buyerRepository;
			}

			public async Task<Result> Handle(DiscardMakingOfferCommand request, CancellationToken cancellationToken)
			{
				var buyerId = new Id(request.BuyerId);
				var buyer = await this.buyerRepository.GetByIdAsync(buyerId);
				if (buyer == null)
					throw new NotFoundException(nameof(buyer));

				var productId = new Id(request.ProductId);
				buyer.DicardMakingOffer(productId);

				return Result.Ok();
			}
		}
	}
}
