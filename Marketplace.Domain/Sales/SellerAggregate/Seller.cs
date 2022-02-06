using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class Seller : AggregateRoot
	{
		private readonly ICollection<string> productIdsForSelling = new List<string>();
		private readonly IDictionary<string, Offer> soldProductIdsAndOffers = new Dictionary<string, Offer>();
		private readonly ICollection<string> archivedProductIds = new List<string>();

		private readonly ICollection<Offer> receivedOffers = new List<Offer>();
		private readonly ICollection<Offer> declinedOffers = new List<Offer>();

		public IReadOnlyList<string> ProductIdsForSale => this.productIdsForSelling.ToList();
		public IReadOnlyList<string> SoldProductIds => this.soldProductIdsAndOffers.Keys.ToList();
		public IReadOnlyList<string> ArchiedProductIds => this.archivedProductIds.ToList();

		public void PublishProductForSale(string productId)
		{
			if (productIdsForSelling.Contains(productId) || soldProductIdsAndOffers.ContainsKey(productId))
				throw new InvalidOperationException();

			this.productIdsForSelling.Add(productId);

			this.AddDomainEvent(new ProbuctPublishedForSaleEvent(productId));
		}

		public void ArchiveProduct(string productId)
		{
			var isProductRemoved = this.productIdsForSelling.Remove(productId);
			if (isProductRemoved == false)
				throw new InvalidOperationException();

			this.archivedProductIds.Add(productId);

			this.AddDomainEvent(new ProductArchivedEvent(productId));
		}

		public void AcceptOffer(Offer offer)
		{
			this.ValidateProductForSaleExistence(offer.ProductId);

			this.soldProductIdsAndOffers.Add(offer.ProductId, offer);
			this.productIdsForSelling.Remove(offer.ProductId);

			this.AddDomainEvent(new OfferAcceptedEvent(offer.ProductId, offer.BuyerId));
		}

		public void ReceiveOffer(Offer offer)
		{
			this.ValidateProductForSaleExistence(offer.ProductId);

			this.receivedOffers.Add(offer);
		}

		public void DeclineOffer(Offer offer)
		{
			this.ValidateProductForSaleExistence(offer.ProductId);

			this.declinedOffers.Add(offer);

			this.AddDomainEvent(new OfferDeclinedEvent(offer.ProductId, offer.BuyerId));
		}

		private void ValidateProductForSaleExistence(string productId)
		{
			var isProductForSale = this.productIdsForSelling.Contains(productId);
			if (isProductForSale == false)
				throw new InvalidOperationException();
		}
	}
}