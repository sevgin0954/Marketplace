﻿using Marketplace.Domain.Common;

namespace Marketplace.Domain.Shipping.UserAggregate
{
	public class OrderArrivedEvent : IDomainEvent
	{
		public OrderArrivedEvent(string orderId, string buyerId)
		{
			this.OrderId = orderId;
			this.BuyerId = buyerId;
		}

		public string OrderId { get; }

		public string BuyerId { get; }
	}
}
