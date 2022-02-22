using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.SellerSpecs
{
	public class AcceptOfferSpecs
	{
		[Fact]
		public void Accept_offer_should_add_product_to_sold_collection()
		{
			// Arrange
			var seller = new TestableSeller("123");
			var product = new TestableProduct("234", seller.Id);
			seller.AddProductToSelling(product.Id);

			// Act
			seller.AcceptOffer(seller.Id, product.Id);

			// Assert
			Assert.Equal(product.Id, seller.SoldOutProductIds[0]);
		}

		[Fact]
		public void Accept_offer_should_remove_product_from_selling_colletion()
		{
			// Arrange
			var seller = new TestableSeller("123");
			var product = new TestableProduct("234", seller.Id);
			seller.AddProductToSelling(product.Id);

			// Act
			seller.AcceptOffer(seller.Id, product.Id);

			// Assert
			Assert.Empty(seller.ProductIdsForSale);
		}

		[Fact]
		public void Accept_offer_with_product_not_in_sale_should_throw_an_exception()
		{
			// Arrange
			var seller = new TestableSeller("123");
			var product = new TestableProduct("234", seller.Id);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.AcceptOffer(seller.Id, product.Id));
		}

		[Fact]
		public void Accept_offer_should_raise_an_event()
		{
			// Arrange
			var seller = new TestableSeller("123");
			var product = new TestableProduct("234", seller.Id);
			seller.AddProductToSelling(product.Id);

			// Act
			seller.AcceptOffer(seller.Id, product.Id);

			// Assert
			var actualEvent = seller.DomainEvents[0];
			Assert.Equal(typeof(OfferAcceptedEvent), actualEvent.GetType());
			Assert.Equal(product.Id, (actualEvent as OfferAcceptedEvent).ProductId);
		}
	}

	internal static class SellerExtensions
	{
		public static void AcceptOffer(this TestableSeller seller, string sellerId, string productId)
		{
			var offer = new Offer("345", productId);
			seller.AcceptOffer(offer);
		}
	}
}
