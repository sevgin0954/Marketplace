using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.BuyerProductOffersAggregate;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		private readonly IDictionary<string, Offer> productIdsAndProccessingOffers = new Dictionary<string, Offer>();
		private readonly IDictionary<string, Offer> productIdsAndOffers = new Dictionary<string, Offer>();

		private readonly IDictionary<string, Offer> productIdsAndDeclinedOffers = new Dictionary<string, Offer>();

		private readonly IDictionary<string, Offer> productIdsAndAcceptingOffers = new Dictionary<string, Offer>();
		private readonly IDictionary<string, Offer> productIdsAndAcceptedOffers = new Dictionary<string, Offer>();

		public void StartMovingOfferToAccepted(string productId)
		{
			if (this.productIdsAndOffers.ContainsKey(productId) == false)
				throw new InvalidOperationException();

			var pendingOffer = this.productIdsAndOffers[productId];

			this.productIdsAndAcceptingOffers[productId] = pendingOffer;
			this.productIdsAndOffers.Remove(productId);

			this.AddDomainEvent(new OfferStartedAcceptingEvent(productId, pendingOffer.Quantity));
		}

		public void MoveOfferToAccepted(string productId)
		{
			if (this.productIdsAndAcceptingOffers.ContainsKey(productId) == false)
				throw new InvalidOperationException();

			var acceptingOffer = this.productIdsAndAcceptingOffers[productId];

			this.productIdsAndAcceptingOffers.Remove(productId);
			this.productIdsAndAcceptedOffers.Add(productId, acceptingOffer);
		}

		public void MoveOfferToDeclined(string productId)
		{
			if (this.productIdsAndOffers.ContainsKey(productId) == false)
				throw new InvalidOperationException();

			var pendingOffer = this.productIdsAndOffers[productId];

			this.productIdsAndOffers.Remove(productId);
			this.productIdsAndDeclinedOffers.Add(productId, pendingOffer);
		}

		public void StartAddingOffer(string productId, Offer offer)
		{
			if (this.productIdsAndOffers.ContainsKey(productId) ||
				this.productIdsAndProccessingOffers.ContainsKey(productId))
			{
				throw new InvalidOperationException();
			}
			if (this.productIdsAndOffers.Count == Constants.MaxPendingOffersPerBuyer)
			{
				throw new InvalidOperationException();
			}

			this.productIdsAndProccessingOffers.Add(productId, offer);

			this.AddDomainEvent(new StartAddingOfferEvent(this.Id, productId));
		}

		internal void AddOffer(string productId)
		{
			var offer = this.productIdsAndProccessingOffers[productId];

			this.productIdsAndProccessingOffers.Remove(productId);

			this.productIdsAndOffers.Add(productId, offer);
		}
	}
}