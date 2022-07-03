using CSharpFunctionalExtensions;
using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Commands
{
	public class MoveOfferToAcceptedCommand : IRequest<Result>
	{
		public MoveOfferToAcceptedCommand(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}