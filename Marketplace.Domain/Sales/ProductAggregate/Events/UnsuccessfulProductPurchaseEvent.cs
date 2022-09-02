using MediatR;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record UnsuccessfulProductPurchaseEvent : INotification
	{
		public UnsuccessfulProductPurchaseEvent(string buyerId, string productId, string rejectReason)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
			this.RejectReason = rejectReason;
		}

		public string BuyerId { get; }

		public string ProductId { get; }

		public string RejectReason { get; }
	}
}