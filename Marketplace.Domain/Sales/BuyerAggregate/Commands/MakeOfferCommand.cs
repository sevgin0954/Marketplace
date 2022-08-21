using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Commands
{
	public class MakeOfferCommand : IRequest<Result>
	{
		public MakeOfferCommand(string buyerId, string productId, string sellerId, string message, int quantity)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.SellerId = sellerId;
			this.Message = message;
			this.Quantity = quantity;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string SellerId { get; }

		public string Message { get; }

		public int Quantity { get; }

		internal class MakeOfferCommandHandler : IRequestHandler<MakeOfferCommand, Result>
		{
			private readonly IRepository<Buyer> buyerRepository;

			internal MakeOfferCommandHandler(IRepository<Buyer> buyerRepository)
			{
				this.buyerRepository = buyerRepository;
			}

			public async Task<Result> Handle(MakeOfferCommand request, CancellationToken cancellationToken)
			{
				Result result = Result.Ok();

				var buyer = await this.buyerRepository.GetByIdAsync(request.ProductId);
				if (buyer == null)
					result = Result.Fail(BuyerConstants.BUYER_NOT_FOUND_EXCEPTION);

				buyer.MakeOffer(request.ProductId, request.SellerId, request.Message, request.Quantity);

				var changedRowsCount = await this.buyerRepository.SaveChangesAsync();
				if (changedRowsCount == 0)
					result = Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

				return result;
			}
		}
	}
}