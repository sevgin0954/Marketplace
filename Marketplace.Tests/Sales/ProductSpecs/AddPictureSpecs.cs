using Marketplace.Domain.Sales.ProductAggregate;
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
			var pictureId = "123";

			// Act
			product.AddPicture(pictureId);

			// Assert
			Assert.Equal(pictureId, product.PictureIds[0]);
		}

		[Fact]
		public void Add_picture_when_pictures_count_is_at_maximum_should_throw_an_exception()
		{
			// Arrange
			var product = this.CreateProduct();

			// Act
			for (var count = 1; count <= ProductConstants.MAX_PICTURES_COUNT; count++)
			{
				var pictureId = Guid.NewGuid().ToString();
				product.AddPicture(pictureId);
			}

			// Assert
			Assert.Throws<InvalidOperationException>(() => product.AddPicture(Guid.NewGuid().ToString()));
		}

		private TestableProduct CreateProduct()
		{
			var seller = new TestableSeller();
			var product = new TestableProduct(seller.Id);

			return product;
		}
	}
}