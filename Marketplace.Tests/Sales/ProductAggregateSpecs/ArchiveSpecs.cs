using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.SharedKernel;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.ProductAggregateSpecs
{
	public class ArchiveSpecs
	{
		[Fact]
		public void Archive_product_with_null_initiator_id_should_throw_an_exception()
		{
			// Arrange
			var product = ProductFactory.Create();

			Id initiatorId = null;

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => product.Archive(initiatorId));
		}

		[Fact]
		public void Archive_product_with_initiator_not_the_seller_should_throw_an_exception()
		{
			// Arrange
			var sellerId = new Id();
			var product = ProductFactory.Create(sellerId);

			var initiatorId = new Id();

			var expectedExceptionMessage = ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER;

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => product.Archive(initiatorId));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public void Archive_product_with_initiator_not_the_seller_should_not_change_the_product_status()
		{
			// Arrange
			var sellerId = new Id();
			var product = ProductFactory.Create(sellerId);

			var initiatorId = new Id();

			// Act
			try
			{
				product.Archive(initiatorId);
			}
			catch { }

			// Assert
			Assert.Equal(ProductStatus.Unsold, product.Status);
		}

		[Fact]
		public void Archive_archived_product_should_throw_an_exception()
		{
			// Arrange
			var sellerId = new Id();
			var product = ProductFactory.Create(sellerId);

			var expectedExceptionMessage = "Can't archive already archived product!";

			// Act
			product.Archive(sellerId);

			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => product.Archive(sellerId));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public void Archive_should_change_the_product_status_to_archived()
		{
			// Arrange
			var sellerId = new Id();
			var product = ProductFactory.Create(sellerId);

			// Act
			product.Archive(sellerId);

			// Assert
			Assert.Equal(ProductStatus.Archived, product.Status);
		}
	}
}
