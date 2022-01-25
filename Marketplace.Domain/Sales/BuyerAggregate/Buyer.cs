using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		private readonly IDictionary<string, Offer> productIdsAndPendingOrders = new Dictionary<string, Offer>();
		private readonly IDictionary<string, Offer> productIdsAndAcceptedOffers = new Dictionary<string, Offer>();

		public IReadOnlyList<Offer> PendingOffers => this.productIdsAndPendingOrders.Values.ToList();
		public IReadOnlyList<Offer> AcceptedOffers => this.productIdsAndAcceptedOffers.Values.ToList();

		public void CreateOffer(string productId, string sellerId)
		{
			var offer = new Offer(this.Id, productId, sellerId);
			this.productIdsAndPendingOrders.Add(productId, offer);

			this.AddDomainEvent(new OfferCreatedEvent(sellerId, productId, this.Id));
		}

		public void MoveOfferToAccepted(string productId)
		{
			if (this.productIdsAndPendingOrders.ContainsKey(productId) == false)
				throw new InvalidOperationException();

			var offer = this.productIdsAndPendingOrders[productId];
			this.productIdsAndAcceptedOffers.Add(productId, offer);

			this.productIdsAndPendingOrders.Remove(productId);
		}
	}
}