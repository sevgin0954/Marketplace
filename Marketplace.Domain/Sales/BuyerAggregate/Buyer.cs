using Marketplace.Domain.Common;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		private IDictionary<string, Offer> productIdsAndOffers;
		// Can make making offer long running proccess or i can make it domain service????
		// one aggregate change per transaction in bounded context
		// reason 1 when one fail other aggregate will fail two (more transactional contention)
		public void MakeOffer(string productId, string sellerId, string message, int quantity)
		{
			if (this.productIdsAndOffers.Count == BuyerConstants.MAX_PENDING_OFFERS_PER_BUYER)
				throw new InvalidOperationException();

			var isProductOffered = this.productIdsAndOffers.ContainsKey(productId);
			if (isProductOffered)
				throw new InvalidOperationException();

			var offer = new Offer(productId, sellerId, message, quantity);
			this.productIdsAndOffers.Add(productId, offer);
		}

		public void AcceptOffer(string productId, string initiatorId)
		{
			var offer = this.GetOffer(productId);
			offer.AcceptOffer(initiatorId);
		}

		public void RejectOffer(string productId, string initiatorId, string reason)
		{
			var offer = this.GetOffer(productId);
			offer.RejectOffer(initiatorId, reason);
		}

		private Offer GetOffer(string productId)
		{
			Offer offer;

			var isOfferExist = this.productIdsAndOffers.TryGetValue(productId, out offer);
			if (isOfferExist == false)
				throw new InvalidOperationException();

			return offer;
		}
	}
}