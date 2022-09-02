using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	// TODO: Repeated name in makeoffersaga
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

		public string BuyerId { get; set; }

		public string ProductId { get; set; }

		public string Message { get; set; }

		public int Quantity { get; set; }

		public string SellerId { get; set; }

		internal class MakeOfferCommandHandler : IRequestHandler<MakeOfferCommand, Result>
		{
			private readonly IOfferRepository offerRepository;

			public MakeOfferCommandHandler(IOfferRepository offerRepository)
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
