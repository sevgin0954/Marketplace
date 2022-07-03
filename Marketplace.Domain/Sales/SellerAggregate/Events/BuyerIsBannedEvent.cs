using MediatR;
using System;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	public record BuyerIsBannedEvent : INotification
	{
		public BuyerIsBannedEvent(string buyerId)
		{
			this.BuyerId = buyerId;
		}

		public string BuyerId { get; }
	}
}
