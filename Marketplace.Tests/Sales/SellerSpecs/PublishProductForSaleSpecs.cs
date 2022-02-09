using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.SellerSpecs
{
	public class PublishProductForSaleSpecs
	{
		[Fact]
		public void Publish_product_should_add_product_to_products_for_selling_colletion()
		{
			// Arrange
			var seller = new Seller();
			var product = new TestableProduct("123", seller.Id);

			// Act
			seller.PublishProductForSale(product.Id);

			// Assert
			Assert.Equal(product.Id, seller.ProductIdsForSale[0]);
		}

		[Fact]
		public void Publish_product_which_is_currently_in_sale_should_throw_an_exception()
		{
			// Arrange
			var seller = new Seller();
			var product = new TestableProduct("123", seller.Id);

			seller.PublishProductForSale(product.Id);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.PublishProductForSale(product.Id));
		}

		[Fact]
		public void Publish_product_which_has_been_sold_should_throw_an_exception()
		{
			// Arrange
			var seller = new TestableSeller();
			var product = new TestableProduct("123", seller.Id);

			seller.AddProductToSold(product.Id);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.PublishProductForSale(product.Id));
		}

		[Fact]
		public void Publish_product_should_raise_product_published_for_sale_event()
		{
			// Arrange
			var seller = new Seller();
			var product = new TestableProduct("123", seller.Id);

			// Act
			seller.PublishProductForSale(product.Id);

			// Assert
			var actualEvent = seller.DomainEvents[0];
			Assert.Equal(typeof(ProbuctPublishedForSaleEvent), actualEvent.GetType());
			Assert.Equal(product.Id, (actualEvent as ProbuctPublishedForSaleEvent).ProductId);
		}
	}
}
