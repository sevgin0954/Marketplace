﻿using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class RejectOfferCommand : IRequest<Result>
	{
		public RejectOfferCommand(string productId, string initiatorId, string buyerId, string rejectReasone)
		{
			this.ProductId = productId;
			this.InitiatorId = initiatorId;
			this.BuyerId = buyerId;
			this.RejectReasone = rejectReasone;
		}

		public string ProductId { get; }

		public string InitiatorId { get; }

		public string RejectReasone { get; }

		public string BuyerId { get; }

		internal class RejectOfferCommandHandler : IRequestHandler<RejectOfferCommand, Result>
		{
			private readonly IAggregateRepository<Offer, OfferId> offerRepository;

			internal RejectOfferCommandHandler(IAggregateRepository<Offer, OfferId> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(RejectOfferCommand request, CancellationToken cancellationToken)
			{
				var productId = new Id(request.ProductId);
				var buyerId = new Id(request.BuyerId);
				var offerId = new OfferId(productId, buyerId);

				var offer = await this.offerRepository.GetByIdAsync(offerId);

				if (offer == null)
				{
					const string EXCEPTION_MESSAGE = "The buyer was not found";
					throw new NotFoundException(EXCEPTION_MESSAGE);
				}

				var initiatorId = new Id(request.InitiatorId);
				offer.RejectOffer(initiatorId, request.RejectReasone);

				var changedRowsCount = await this.offerRepository.SaveChangesAsync();
				if (changedRowsCount == 0)
				{
					return Result.Fail(ErrorConstants.NO_RECORD_ALTERED);
				}

				return Result.Ok();
			}
		}
	}
}
