using MediatR;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record UnsuccessfulProductPurchaseEvent : INotification
	{
		public UnsuccessfulProductPurchaseEvent(string buyerId, string productId)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}