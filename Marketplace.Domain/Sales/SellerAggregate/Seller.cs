using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using Marketplace.Domain.SharedKernel;
using Marketplace.Shared;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class Seller : AggregateRoot<Id>
	{
		private readonly HashSet<string> bannedBuyerIds = new HashSet<string>();

		public Seller(Id id)
			: base(id) { }

		public void CheckIsBuyerBannedEventCheck(Id buyerId)
		{
			ArgumentValidator.NotNullValidator(buyerId, nameof(buyerId));

			if (this.bannedBuyerIds.Contains(buyerId.Value))
			{
				this.AddDomainEvent(new BuyerWasBannedEvent(buyerId.Value, this.Id.Value));
			}
			else
			{
				this.AddDomainEvent(new BuyerWasNotBannedEvent(buyerId.Value, this.Id.Value));
			}
		}

		public void BanBuyerFromOffering(Id buyerId)
		{
			ArgumentValidator.NotNullValidator(buyerId, nameof(buyerId));

			if (buyerId == this.Id)
			{
				var exceptionMessage = "Seller can't ban itself!";
				throw new InvalidOperationException(exceptionMessage);
			}

			var isBuyerBanned = this.bannedBuyerIds.Contains(buyerId.Value);
			if (isBuyerBanned)
			{
				var exceptionMessage = "This buyer is already banned!";
				throw new InvalidOperationException(exceptionMessage);
			}

			var addingResult = this.bannedBuyerIds.Add(buyerId.Value);
			if (addingResult == false)
				throw new InvalidOperationException();
		}

		public void UnbanBuyerFromOffering(Id buyerId)
		{
			ArgumentValidator.NotNullValidator(buyerId, nameof(buyerId));

			var removingResult = this.bannedBuyerIds.Remove(buyerId.Value);
			if (removingResult == false)
			{
				var exceptionMessage = "This buyer is not banned!";
				throw new InvalidOperationException(exceptionMessage);
			}
		}
	}
}