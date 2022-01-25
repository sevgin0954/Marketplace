using Marketplace.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales
{
	public class Seller : AggregateRoot
	{
		private readonly ICollection<string> productIdsForSelling = new List<string>();
		private readonly IDictionary<string, Offer> soldProductIdsAndOffers = new Dictionary<string, Offer>();

		private readonly ICollection<Offer> receivedOffers = new List<Offer>();
		private readonly ICollection<Offer> declinedOffers = new List<Offer>();

		public IReadOnlyList<string> ProductIdsForSelling => this.productIdsForSelling.ToList();
		public IReadOnlyList<string> SoldProductIds => this.soldProductIdsAndOffers.Keys.ToList();

		public void PublishProductForSale(string productId)
		{
			if (productIdsForSelling.Contains(productId) || soldProductIdsAndOffers.ContainsKey(productId))
				throw new InvalidOperationException();

			this.productIdsForSelling.Add(productId);

			this.AddDomainEvent(new ProbuctPublishedForSaleEvent());
		}

		public void RemoveProductForSale(string productId)
		{
			var isProductRemoved = this.productIdsForSelling.Remove(productId);
			if (isProductRemoved == false)
				throw new InvalidOperationException();

			// this.AddDomainEvent();
		}

		public void AcceptOffer(Offer offer)
		{
			this.ValidateProductForSaleExistence(offer.ProductId);

			this.soldProductIdsAndOffers.Add(offer.ProductId, offer);
			this.RemoveProductForSale(offer.ProductId);

			this.AddDomainEvent(new OfferAcceptedEvent(offer.ProductId, offer.BuyerId));
		}

		public void AddOffer(Offer offer)
		{
			this.ValidateProductForSaleExistence(offer.ProductId);

			this.receivedOffers.Add(offer);
		}

		public void DeclineOffer(Offer offer)
		{
			//this.ValidateProductForSaleExistence(offer.Product);

			this.declinedOffers.Add(offer);
		}

		private void ValidateProductForSaleExistence(string productId)
		{
			var isProductForSale = this.productIdsForSelling.Contains(productId);
			if (isProductForSale == false)
				throw new InvalidOperationException();
		}
	}
}