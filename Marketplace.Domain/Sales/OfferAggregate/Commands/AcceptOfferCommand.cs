using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Sales.BuyerAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class AcceptOfferCommand : IRequest<Result>
	{
		public AcceptOfferCommand(string productId, string sellerId)
		{
			this.ProductId = productId;
			this.InitiatorId = sellerId;
		}

		public string ProductId { get; }

		public string InitiatorId { get; }

		//internal class AcceptOfferCommandHandler : IRequestHandler<AcceptOfferCommand, Result>
		//{
		//	private readonly IAggregateRepository<Buyer> buyerRepository;

		//	internal AcceptOfferCommandHandler(IAggregateRepository<Buyer> buyerRepository)
		//	{
		//		this.buyerRepository = buyerRepository;
		//	}

		//	public async Task<Result> Handle(AcceptOfferCommand request, CancellationToken cancellationToken)
		//	{
		//		//Result result = Result.Ok();

		//		//var buyer = await this.buyerRepository.GetByIdAsync(request.ProductId);
		//		//if (buyer == null)
		//		//	result = Result.Fail(BuyerConstants.BUYER_NOT_FOUND_EXCEPTION);

		//		//buyer.AcceptOffer(request.ProductId, request.InitiatorId);

		//		//var changedRowsCount = await this.buyerRepository.SaveChangesAsync();
		//		//if (changedRowsCount == 0)
		//		//	result = Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

		//		//return result;
		//	}
		//}
	}
}