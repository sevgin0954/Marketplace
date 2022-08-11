using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class RejectOfferCommand : IRequest<Result>
	{
		public RejectOfferCommand(string offerId, string initiatorId, string rejectReasone)
		{
			this.OfferId = offerId;
			this.InitiatorId = initiatorId;
			RejectReasone = rejectReasone;
		}

		public string OfferId { get; }

		public string InitiatorId { get; }

		public string RejectReasone { get; }

		internal class RejectOfferCommandHandler : IRequestHandler<RejectOfferCommand, Result>
		{
			private readonly IRepository<Offer> offerRepository;

			internal RejectOfferCommandHandler(IRepository<Offer> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(RejectOfferCommand request, CancellationToken cancellationToken)
			{
				Result result = Result.Ok();

				var offer = await this.offerRepository.GetByIdAsync(request.OfferId);
				if (offer == null)
					result = Result.Fail("Offer was not found!");

				offer.RejectOffer(request.InitiatorId, request.RejectReasone);

				var changedRowsCount = await this.offerRepository.SaveChangesAsync();
				if (changedRowsCount == 0)
					result = Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

				return result;
			}
		}
	}
}
