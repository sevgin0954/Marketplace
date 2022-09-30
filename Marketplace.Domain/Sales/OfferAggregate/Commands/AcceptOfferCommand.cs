using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class AcceptOfferCommand : IRequest<Result>
	{
		public AcceptOfferCommand(string productId, string initiatorId, string buyerId)
		{
			this.ProductId = productId;
			this.InitiatorId = initiatorId;
			this.BuyerId = buyerId;
		}

		public string ProductId { get; }

		public string InitiatorId { get; }

		public string BuyerId { get; }

		internal class AcceptOfferCommandHandler : IRequestHandler<AcceptOfferCommand, Result>
		{
			private readonly IAggregateRepository<Offer, OfferId> offerRepository;

			internal AcceptOfferCommandHandler(IAggregateRepository<Offer, OfferId> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(AcceptOfferCommand request, CancellationToken cancellationToken)
			{
				var productId = new Id(request.ProductId);
				var buyerId = new Id(request.BuyerId);
				var offerId = new OfferId(productId, buyerId);

				var offer = await this.offerRepository.GetByIdAsync(offerId);
				if (offer == null)
				{
					throw new NotFoundException(nameof(offer));
				}

				var sellerId = new Id(request.InitiatorId);
				offer.AcceptOffer(sellerId);

				var changedRowsCount = await this.offerRepository.SaveChangesAsync(cancellationToken);
				if (changedRowsCount == 0)
				{
					return Result.Fail(ErrorConstants.NO_RECORD_ALTERED);
				}

				return Result.Ok();
			}
		}
	}
}