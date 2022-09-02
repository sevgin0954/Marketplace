using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	internal class MakeOfferCommand : IRequest<Result>
	{
		public MakeOfferCommand(string buyerId, string productId, string message, int quantity, string sellerId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.Message = message;
			this.Quantity = quantity;
			this.SellerId = sellerId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string Message { get; }

		public int Quantity { get; }

		public string SellerId { get; }

		internal class MakeOfferCommandHandler : IRequestHandler<MakeOfferCommand, Result>
		{
			private readonly IRepository<Offer> offerRepository;

			public MakeOfferCommandHandler(IRepository<Offer> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(MakeOfferCommand request, CancellationToken cancellationToken)
			{
				var offerId = new OfferId(request.ProductId, request.BuyerId);
				var offer = new Offer(offerId, request.ProductId, request.SellerId, request.Message, request.Quantity);

				var rowsChanged = await this.offerRepository.AddAsync(offer);
				if (rowsChanged == 0)
					return Result.Fail(ErrorConstants.NO_RECORD_ALTERED);
				else
				{
					return Result.Ok();
				}
			}
		}
	}
}
