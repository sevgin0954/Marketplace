using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class MakeOfferCommand : IRequest<Result>
	{
		public MakeOfferCommand(string buyerId, string productId, string sellerId, string message)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.SellerId = sellerId;
			this.Message = message;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string SellerId { get; }

		public string Message { get; }

		public int Quantity { get; }

		internal class CreateOfferCommandHandler : IRequestHandler<MakeOfferCommand, Result>
		{
			private readonly IRepository<Offer> offerRepository;

			internal CreateOfferCommandHandler(IRepository<Offer> offerRepository)
			{
				this.offerRepository = offerRepository;
			}

			public async Task<Result> Handle(MakeOfferCommand request, CancellationToken cancellationToken)
			{
				Result result = Result.Ok();

				var offer = new Offer(request.ProductId, request.SellerId, request.BuyerId, request.Message);
				await this.offerRepository.AddAsync(offer);

				var changedRowsCount = await this.offerRepository.SaveChangesAsync();
				if (changedRowsCount == 0)
					result = Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

				return result;
			}
		}
	}
}