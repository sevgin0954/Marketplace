using Marketplace.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales.ProductOffersAggregate
{
	public class ProductOffers : AggregateRoot
	{
		public ProductOffers(string productId)
		{
			this.Id = productId;
		}

		private readonly ICollection<ProductOffer> pendingOffers = new List<ProductOffer>();
		private readonly ICollection<ProductOffer> declinedOffers = new List<ProductOffer>();
		private readonly ICollection<ProductOffer> acceptedOffers = new List<ProductOffer>();

		public IReadOnlyList<ProductOffer> PendingOffers => this.pendingOffers.ToList();
		public IReadOnlyList<ProductOffer> DeclinedOffers => this.declinedOffers.ToList();
		public IReadOnlyList<ProductOffer> AcceptedOffers => this.acceptedOffers.ToList();

		public void AcceptOffer(ProductOffer offer)
		{
			this.RemoveOfferFromPendings(offer);

			this.acceptedOffers.Add(offer);
		}

		public void AddOffer(ProductOffer offer)
		{
			this.pendingOffers.Add(offer);
		}

		public void DeclineOffer(ProductOffer offer)
		{
			this.RemoveOfferFromPendings(offer);

			this.declinedOffers.Add(offer);
		}

		private void RemoveOfferFromPendings(ProductOffer offer)
		{
			var removalResult = this.pendingOffers.Remove(offer);
			if (removalResult == false)
				throw new InvalidOperationException();
		}
	}
}