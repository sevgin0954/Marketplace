using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class Seller : AggregateRoot
	{
		private readonly HashSet<string> bannedBuyerIds = new HashSet<string>();

		public void CheckIsBuyerBanned(string buyerId)
		{
			if (this.bannedBuyerIds.Contains(buyerId))
			{
				this.AddDomainEvent(new BuyerIsBannedEvent(buyerId));
			}
			else
			{
				this.AddDomainEvent(new BuyerIsNotBannedEvent(buyerId));
			}
		}

		public void BanBuyerFromOffering(string buyerId)
		{
			if (string.IsNullOrWhiteSpace(buyerId))
				throw new InvalidOperationException();

			var addingResult = this.bannedBuyerIds.Add(buyerId);
			if (addingResult == false)
				throw new InvalidOperationException();
		}

		public void UnbanBuyerFromOffering(string buyerId)
		{
			if (string.IsNullOrWhiteSpace(buyerId))
				throw new InvalidOperationException();

			var removingResult = this.bannedBuyerIds.Remove(buyerId);
			if (removingResult == false)
				throw new InvalidOperationException();
		}
	}
}