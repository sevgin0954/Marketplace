﻿using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot<Id>
	{
		public Buyer(Id id, int pendingOffersCount)
			: base(id)
		{
			this.PendingOffersCount = pendingOffersCount;
			this.StartedPendingOffersIds = new List<string>();
		}

		public IList<string> StartedPendingOffersIds { get; private set; }

		public int PendingOffersCount { get; private set; }

		public void StartMakingOffer(Id productId)
		{
			ArgumentValidator.NotNullValidator(productId, nameof(productId));
			this.ValidateCanAddOffer();

			this.StartedPendingOffersIds.Add(productId.Value);
		}

		public void FinishMakingOffer(Id productId)
		{
			ArgumentValidator.NotNullValidator(productId, nameof(productId));
			this.ValidateCanAddOffer();

			var isOfferRemoved = this.StartedPendingOffersIds.Remove(productId.Value);
			if (isOfferRemoved)
				throw new NotFoundException(nameof(productId));

			this.PendingOffersCount++;

			this.AddDomainEvent(new OfferWasAddedToBuyerEvent(this.Id.Value, productId.Value));
		}

		private void ValidateCanAddOffer()
		{
			if (this.PendingOffersCount + 1 > BuyerConstants.MAX_PENDING_OFFERS_PER_BUYER)
				throw new InvalidOperationException("Maximum pending offers limit is reached!");
		}

		public void DicardMakingOffer(Id productId)
		{
			ArgumentValidator.NotNullValidator(productId, nameof(productId));

			var isOfferRemoved = this.StartedPendingOffersIds.Remove(productId.Value);
			if (isOfferRemoved == false)
				throw new NotFoundException(nameof(productId));
		}
	}
}