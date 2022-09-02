using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		//private IDictionary<string, Offer> productIdsAndPendingOffers;
		//private IDictionary<string, Offer> productIdsAndAcceptedOffers;
		//private IDictionary<string, Offer> productIdsAndRejectedOffers;

		//public void MakeOffer(string productId, string sellerId, string message, int quantity)
		//{
		//	if (this.productIdsAndPendingOffers.Count == BuyerConstants.MAX_PENDING_OFFERS_PER_BUYER)
		//		throw new InvalidOperationException();

		//	var isPendingOfferExist = this.productIdsAndPendingOffers.ContainsKey(productId);
		//	if (isPendingOfferExist)
		//		throw new InvalidOperationException();

		//	var offer = new Offer(productId, sellerId, message, quantity);
		//	this.productIdsAndPendingOffers.Add(productId, offer);

		//	this.AddDomainEvent(new OfferWasMadeEvent(this.Id, productId));
		//}

		//public void AcceptOffer(string productId, string initiatorId)
		//{
		//	var offer = this.GetOffer(productId);
		//	offer.AcceptOffer(initiatorId);

		//	this.productIdsAndPendingOffers.Remove(productId);
		//	this.productIdsAndAcceptedOffers.Add(productId, offer);
		//}

		//// Who is the initiator if offer is rejected by the system
		//public void RejectOffer(string productId, string initiatorId, string reason)
		//{
		//	var offer = this.GetOffer(productId);
		//	offer.RejectOffer(initiatorId, reason);

		//	this.productIdsAndPendingOffers.Remove(productId);
		//	this.productIdsAndRejectedOffers.Add(productId, offer);
		//}

		//private Offer GetOffer(string productId)
		//{
		//	Offer offer;

		//	var isOfferExist = this.productIdsAndPendingOffers.TryGetValue(productId, out offer);
		//	if (isOfferExist == false)
		//		throw new InvalidOperationException();

		//	return offer;
		//}
	}
}