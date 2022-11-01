using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Exceptions;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot<Id>
	{
		private int pendingOffersCount = 0;
		private readonly HashSet<string> startedOffersProductsIds = new();

		public Buyer(Id id, int pendingOffersCount)
			: base(id)
		{
			this.pendingOffersCount = pendingOffersCount;
		}

		internal void StartMakingOffer(Id productId)
		{
			ArgumentValidator.NotNullValidator(productId, nameof(productId));

			var isOfferStarted = this.startedOffersProductsIds.Contains(productId.Value);
			if (isOfferStarted)
				throw new InvalidOperationException("Maximum pending offers limit is reached!");

			this.ValidateCanAddOffer();

			this.startedOffersProductsIds.Add(productId.Value);
		}

		internal void FinishMakingOffer(Id productId)
		{
			ArgumentValidator.NotNullValidator(productId, nameof(productId));
			this.ValidateCanAddOffer();

			var isOfferRemoved = this.startedOffersProductsIds.Remove(productId.Value);
			if (isOfferRemoved)
				throw new NotFoundException(nameof(productId));

			this.pendingOffersCount++;
		}

		private void ValidateCanAddOffer()
		{
			if (this.pendingOffersCount + 1 > BuyerConstants.MAX_PENDING_OFFERS_PER_BUYER)
				throw new InvalidOperationException("Maximum pending offers limit is reached!");
		}

		internal void DicardMakingOffer(Id productId)
		{
			ArgumentValidator.NotNullValidator(productId, nameof(productId));

			var isOfferRemoved = this.startedOffersProductsIds.Remove(productId.Value);
			if (isOfferRemoved == false)
				throw new NotFoundException(nameof(productId));
		}
	}
}