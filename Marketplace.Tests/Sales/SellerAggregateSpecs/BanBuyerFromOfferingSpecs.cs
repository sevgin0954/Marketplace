using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using Xunit;

namespace Marketplace.Tests.Sales.SellerAggregateSpecs
{
	public class BanBuyerFromOfferingSpecs
	{
		[Fact]
		public void Ban_buyer_with_null_id_should_throw_an_exception()
		{
			// Arrange
			var sellerId = new Id();
			var seller = new Seller(sellerId);

			Id buyerId = null;

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(() => seller.BanBuyerFromOffering(buyerId));
		}

		[Fact]
		public void Ban_already_banned_buyer_should_throw_an_exception()
		{
			// Arrange
			var sellerId = new Id();
			var seller = new Seller(sellerId);

			var buyerId = new Id();
			seller.BanBuyerFromOffering(buyerId);

			var expectedExceptionMessage = "This buyer is already banned!";

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => seller.BanBuyerFromOffering(buyerId));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public void Ban_itslef_should_throw_an_exception()
		{
			// Arrange
			var sellerId = new Id();
			var seller = new Seller(sellerId);

			var expectedExceptionMessage = "Seller can't ban itself!";

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => seller.BanBuyerFromOffering(sellerId));
			Assert.Equal(expectedExceptionMessage, exception.Message);
		}

		[Fact]
		public void Ban_buyer_should_ban_the_buyer()
		{
			// Arrange
			var sellerId = new Id();
			var seller = new Seller(sellerId);

			var buyerId = new Id();

			// Act
			seller.BanBuyerFromOffering(buyerId);

			seller.CheckIsBuyerBannedEventCheck(buyerId);

			// Assert
			Assert.Contains(seller.DomainEvents, de => de.GetType() == typeof(BuyerWasBannedEvent));
		}
	}
}