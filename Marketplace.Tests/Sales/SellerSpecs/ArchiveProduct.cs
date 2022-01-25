using Marketplace.Domain.Sales.Events;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.SellerSpecs
{
	public class ArchiveProduct
	{
		[Fact]
		public void Archive_product_should_remove_product_from_selling_collection()
		{
			// Arrange
			var seller = new TestableSeller();
			var product = new TestableProduct("123", seller);
			seller.AddProductToSelling(product.Id);

			// Act
			seller.ArchiveProduct(product.Id);

			// Assert
			Assert.Empty(seller.ProductIdsForSale);
		}

		[Fact]
		public void Archive_product_should_add_product_to_archived_collection()
		{
			// Arrange
			var seller = new TestableSeller();
			var product = new TestableProduct("123", seller);
			seller.AddProductToSelling(product.Id);

			// Act
			seller.ArchiveProduct(product.Id);

			// Assert
			Assert.Equal(product.Id, seller.ArchiedProductIds[0]);
		}

		[Fact]
		public void Archive_product_which_is_not_in_sale_should_throw_an_exception()
		{
			// Arrange
			var seller = new TestableSeller();
			var product = new TestableProduct("123", seller);

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => seller.ArchiveProduct(product.Id));
		}

		[Fact]
		public void Archive_product_should_raise_an_event()
		{
			// Arrange
			var seller = new TestableSeller();
			var product = new TestableProduct("123", seller);
			seller.AddProductToSelling(product.Id);

			// Act
			seller.ArchiveProduct(product.Id);

			// Assert
			var actualEvent = seller.DomainEvents[0];
			Assert.Equal(typeof(ProductArchivedEvent), actualEvent.GetType());
			Assert.Equal(product.Id, (actualEvent as ProductArchivedEvent).ProductId);
		}
	}
}
