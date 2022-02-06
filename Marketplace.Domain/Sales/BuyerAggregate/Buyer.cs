using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		private readonly ICollection<string> productIdsForPendingOffers = new List<string>();
		private readonly ICollection<string> productIdsForAcceptedOffers = new List<string>();

		public IReadOnlyList<string> PendingOffersProductIds => this.productIdsForPendingOffers.ToList();
		public IReadOnlyList<string> AcceptedOffersProductIds => this.productIdsForAcceptedOffers.ToList();

		public void CreateOffer(string productId, string sellerId)
		{
			this.productIdsForPendingOffers.Add(productId);

			this.AddDomainEvent(new OfferCreatedEvent(sellerId, productId, this.Id));
		}

		public void MoveOfferToAccepted(string productId)
		{
			if (this.productIdsForPendingOffers.Contains(productId) == false)
				throw new InvalidOperationException();

			this.productIdsForAcceptedOffers.Add(productId);

			this.productIdsForPendingOffers.Remove(productId);
		}
	}
}