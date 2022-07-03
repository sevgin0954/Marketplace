using System;
using Tactical.DDD;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record SuccessfulProductPurchaseEvent : DomainEvent
	{
		public SuccessfulProductPurchaseEvent(string buyerId, string productId)
			: base(DateTime.UtcNow)
		{
			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; }

		public string ProductId { get; }
	}
}
