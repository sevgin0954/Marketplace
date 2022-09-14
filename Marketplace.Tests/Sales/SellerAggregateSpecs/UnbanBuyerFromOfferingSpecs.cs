using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using Marketplace.Domain.SharedKernel;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.SellerAggregateSpecs
{
	public class UnbanBuyerFromOfferingSpecs
	{
		[Fact]
		public void With_null_buyer_id_should_throw_an_exception()
		{
			// Arrange
			Id buyerId = null;

			var sellerId = new Id();
			var seller = new Seller(sellerId);

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => seller.UnbanBuyerFromOffering(buyerId));
		}

		[Fact]
		public void Unban_not_banned_buyer_should_throw_an_exception()
		{
			// Arrange
			Id buyerId = new Id();

			var sellerId = new Id();
			var seller = new Seller(sellerId);

			var expectedExceptionMessage = "This buyer is not banned!";

			// Act
			// Assert
			var exceptionMessage = Assert.Throws<InvalidOperationException>(() => seller.UnbanBuyerFromOffering(buyerId));
			Assert.Equal(expectedExceptionMessage, exceptionMessage.Message);
		}

		[Fact]
		public void Unban_buyer_should_unban_buyer()
		{
			// Arrange
			var sellerId = new Id();
			var seller = new Seller(sellerId);

			Id buyerId = new Id();
			seller.BanBuyerFromOffering(buyerId);

			// Act
			seller.UnbanBuyerFromOffering(buyerId);
			seller.CheckIsBuyerBannedEventCheck(buyerId);

			// Assert
			Assert.Contains(seller.DomainEvents, de => de.GetType() == typeof(BuyerWasNotBannedEvent));
		}
	}
}
