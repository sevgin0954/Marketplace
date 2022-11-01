using Marketplace.Domain.SharedKernel;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.BuyerAggregateSpecs
{
	public class StartMakingOfferSpecs
	{
		[Fact]
		public void Start_making_offer_with_null_product_id_should_throw_an_exception()
		{
			// Arrange
			var buyer = BuyerFactory.Create();
			Id productId = null;

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => buyer.StartMakingOffer(productId));
		}

		[Fact]
		public void Start_making_offer_with_already_existing_pending_offer_with_the_same_product_id_should_throw_an_exception()
		{
			// Arrange
			var buyer = BuyerFactory.Create();
			var productId = new Id();

			buyer.StartMakingOffer(productId);

			var expectedExceptionMessage = "Maximum pending offers limit is reached!";

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => buyer.StartMakingOffer(productId));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public void Start_pending_offer_when_maximum_pending_offers_count_is_reach_should_throw_an_exception()
		{
			// Arrange

			// Act

			// Assert
		}

		[Fact]
		public void Start_pending_offer_should_add_offer_to_pendings()
		{
			// Arrange

			// Act

			// Assert
		}
	}
}
