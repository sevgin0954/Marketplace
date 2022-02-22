using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class Seller : AggregateRoot
	{
		private readonly IDictionary<string, Product> productIdsAndProductsForSale = new Dictionary<string, Product>();
		private readonly IDictionary<string, Product> soldOutProductIdsAndProducts = new Dictionary<string, Product>();
		private readonly IDictionary<string, Product> archivedProductIdsAndProducts = new Dictionary<string, Product>();

		private readonly HashSet<string> bannedBuyerIds = new HashSet<string>();

		public IReadOnlyList<string> ProductIdsForSale => this.productIdsAndProductsForSale.Keys.ToList();
		public IReadOnlyList<string> SoldOutProductIds => this.soldOutProductIdsAndProducts.Keys.ToList();
		public IReadOnlyList<string> ArchiedProductIds => this.archivedProductIdsAndProducts.Keys.ToList();

		public void AddOffer(Offer offer)
		{
			this.ValidateIfProductCanBeSold(offer);

			this.AddDomainEvent(new OfferAddedEvent(this.Id, offer.BuyerId, offer.Quantity));
		}

		public void AcceptOffer(Offer offer)
		{
			this.ValidateIfProductCanBeSold(offer);

			var product = this.productIdsAndProductsForSale[offer.ProductId];
			product.Buy(offer.Quantity);

			if (product.Status == ProductStatus.Sold)
			{
				this.productIdsAndProductsForSale.Remove(product.Id);
				this.soldOutProductIdsAndProducts.Add(product.Id, product);
			}

			this.AddDomainEvent(new OfferAcceptedEvent(this.Id, offer.BuyerId, offer.Quantity));
		}

		// TODO: When product is sold out or archived all offers should be declined
		public void DeclineOffer(Offer offer)
		{
			this.ValidateProductExistence(offer.ProductId);

			this.AddDomainEvent(new OfferDeclinedEvent(this.Id, offer.BuyerId, offer.Quantity));
		}

		public void PublishProductForSale(string productId)
		{
			if (productIdsForSelling.Contains(productId) || soldOutProductIds.Contains(productId))
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

		public void BanBuyerFromOffering(string buyerId)
		{
			if (string.IsNullOrWhiteSpace(buyerId))
				throw new InvalidOperationException();

			var addingResult = this.bannedBuyerIds.Add(buyerId);
			if (addingResult == false)
				throw new InvalidOperationException();
		}

		public void UnbanBuyerFromOffering(string buyerId)
		{
			if (string.IsNullOrWhiteSpace(buyerId))
				throw new InvalidOperationException();

			var removingResult = this.bannedBuyerIds.Remove(buyerId);
			if (removingResult == false)
				throw new InvalidOperationException();
		}

		private void ValidateIfProductCanBeSold(Offer offer)
		{
			var isBuyerBanned = this.bannedBuyerIds.Contains(offer.BuyerId);
			if (isBuyerBanned)
				throw new InvalidOperationException();

			this.ValidateProductExistence(offer.ProductId);

			var product = this.productIdsAndProductsForSale[offer.ProductId];
			product.ValidateIfProductCanBeSold(offer.Quantity);
		}

		private void ValidateProductExistence(string productId)
		{
			var isProductExist = this.productIdsAndProductsForSale.ContainsKey(productId);
			if (isProductExist == false)
				throw new InvalidOperationException();
		}
	}
}