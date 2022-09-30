using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
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
			private readonly IRepository<Offer, OfferId> offerRepository;

			public MakeOfferCommandHandler(IRepository<Offer, OfferId> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(MakeOfferCommand request, CancellationToken cancellationToken)
			{
				var productId = new Id(request.ProductId);
				var buyerId = new Id(request.BuyerId);
				var offerId = new OfferId(productId, buyerId);

				var sellerId = new Id(request.SellerId);
				var offer = new Offer(offerId, sellerId, request.Message);

				var rowsChanged = await this.offerRepository.AddAsync(offer, cancellationToken);
				if (rowsChanged == 0)
				{
					return Result.Fail(ErrorConstants.NO_RECORD_ALTERED);
				}

				return Result.Ok();
			}
		}
	}
}
