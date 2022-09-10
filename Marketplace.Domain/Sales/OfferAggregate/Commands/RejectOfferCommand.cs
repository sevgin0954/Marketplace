using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.OfferAggregate.Commands
{
	public class RejectOfferCommand : IRequest<Result>
	{
		public RejectOfferCommand(string productId, string initiatorId, string rejectReasone)
		{
			this.ProductId = productId;
			this.InitiatorId = initiatorId;
			this.RejectReasone = rejectReasone;
		}

		public string ProductId { get; }

		public string InitiatorId { get; }

		public string RejectReasone { get; }

		//internal class RejectOfferCommandHandler : IRequestHandler<RejectOfferCommand, Result>
		//{
		//	//private readonly IAggregateRepository<Buyer> buyerRepository;

		//	//internal RejectOfferCommandHandler(IAggregateRepository<Buyer> buyerRepository)
		//	//{
		//	//	this.buyerRepository = buyerRepository;
		//	//}

		//	//public async Task<Result> Handle(RejectOfferCommand request, CancellationToken cancellationToken)
		//	//{
		//	//	Result result = Result.Ok();

		//	//	var buyer = await this.buyerRepository.GetByIdAsync(request.ProductId);
		//	//	if (buyer == null)
		//	//		result = Result.Fail(BuyerConstants.BUYER_NOT_FOUND_EXCEPTION);

		//	//	buyer.RejectOffer(request.ProductId, request.InitiatorId, request.RejectReasone);

		//	//	var changedRowsCount = await this.buyerRepository.SaveChangesAsync();
		//	//	if (changedRowsCount == 0)
		//	//		result = Result.Fail(ErrorConstants.NO_RECORD_ALTERED);

		//	//	return result;
		//	//}
		//}
	}
}
