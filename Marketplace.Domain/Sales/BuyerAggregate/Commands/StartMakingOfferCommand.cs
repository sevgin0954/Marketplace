using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Commands
{
	internal class StartMakingOfferCommand : IRequest<Result>
	{
		public StartMakingOfferCommand(
			string buyerId, 
			string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		internal class StartMakingOfferCommandHandler : IRequestHandler<StartMakingOfferCommand, Result>
		{
			private readonly IRepository<Buyer, Id> buyerRepository;

			public StartMakingOfferCommandHandler(IRepository<Buyer, Id> buyerRepository)
			{
				this.buyerRepository = buyerRepository;
			}

			public async Task<Result> Handle(StartMakingOfferCommand request, CancellationToken cancellationToken)
			{
				var buyerId = new Id(request.BuyerId);
				var buyer = await this.buyerRepository.GetByIdAsync(buyerId);
				if (buyer == null)
					throw new NotFoundException(nameof(buyer));

				var productId = new Id(request.ProductId);
				buyer.StartMakingOffer(productId);

				// TODO: Move to generic base class
				var alteredRows = await this.buyerRepository.SaveChangesAsync();
				if (alteredRows <= 0)
					throw new NotPersistentException(nameof(buyer));

				return Result.Ok();
			}
		}
	}
}