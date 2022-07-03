using System;
using Tactical.DDD;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record UnsuccessfulProductPurchaseEvent : DomainEvent
	{
		public UnsuccessfulProductPurchaseEvent(string buyerId, string productId)
			: base(DateTime.UtcNow)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}