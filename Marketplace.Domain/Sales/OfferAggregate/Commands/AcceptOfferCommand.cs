using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class AcceptOfferCommand : IRequest<Result>
	{
		public AcceptOfferCommand(string offerId, string initiatorId)
		{
			this.OfferId = offerId;
			this.InitiatorId = initiatorId;
		}

		public string OfferId { get; }

		public string InitiatorId { get; }

		internal class AcceptOfferCommandHandler : IRequestHandler<AcceptOfferCommand, Result>
		{
			private readonly IRepository<Offer> offerRepository;

			internal AcceptOfferCommandHandler(IRepository<Offer> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(AcceptOfferCommand request, CancellationToken cancellationToken)
			{
				Result result = Result.Ok();

				var offer = await this.offerRepository.GetByIdAsync(request.OfferId);
				if (offer == null)
					result = Result.Fail("Offer was not found!");

				offer.AcceptOffer(request.InitiatorId);

				var changedRowsCount = await this.offerRepository.SaveChangesAsync();
				if (changedRowsCount == 0)
					result = Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

				return result;
			}
		}
	}
}