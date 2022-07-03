using CSharpFunctionalExtensions;
using MediatR;

namespace Marketplace.Domain.Sales.BuyerAggregate.Commands
{
	public class CreateOfferCommand : IRequest<Result>
	{
		public CreateOfferCommand(string buyerId, string productId, int quantity)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.Quantity = quantity;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public int Quantity { get; }
	}
}