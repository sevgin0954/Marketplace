using MediatR;
using System;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public record BuyerIsNotBannedEvent : INotification
	{
		public BuyerIsNotBannedEvent(string buyerId)
		{
			this.BuyerId = buyerId;
		}

		public string BuyerId { get; }
	}
}
