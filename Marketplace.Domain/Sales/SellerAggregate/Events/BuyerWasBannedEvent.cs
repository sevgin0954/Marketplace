﻿using MediatR;

namespace Marketplace.Domain.Sales.SellerAggregate.Events
{
	internal record BuyerWasBannedEvent : INotification
	{
		public BuyerWasBannedEvent(string buyerId, string sellerId)
		{
			this.BuyerId = buyerId;
			this.SellerId = sellerId;
		}

		public string BuyerId { get; }

		public string SellerId { get; }
	}
}
