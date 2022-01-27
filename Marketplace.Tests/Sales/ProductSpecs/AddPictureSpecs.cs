using Marketplace.Domain.Sales;
using Marketplace.Domain.SharedKernel;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.ProductSpecs
{
	public class AddPictureSpecs
	{
		[Fact]
		public void Add_picture_should_add_picture_to_pictures_collection()
		{
			// Arrange
			var product = this.CreateProduct();
			var picture = new Picture();

			// Act
			product.AddPicture(picture);

			// Assert
			Assert.Equal(picture, product.Pictures[0]);
		}

		[Fact]
		public void Add_picture_when_pictures_count_is_at_maximum_should_throw_an_exception()
		{
			// Arrange
			var product = this.CreateProduct();
			var picture = new Picture();

			// Act
			for (var count = 1; count <= DomainConstants.MAX_PICTURES_COUNT; count++)
			{
				product.AddPicture(picture);
			}

			// Assert
			Assert.Throws<InvalidOperationException>(() => product.AddPicture(picture));
		}

		private TestableProduct CreateProduct()
		{
			var seller = new TestableSeller();
			var product = new TestableProduct(seller);

			return product;
		}
	}
}