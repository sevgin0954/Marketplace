using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Marketplace.Tests.Sales.BuyerSpecs
{
	public class MoveOfferSpecs
	{
		[Fact]
		public void Move_offer_to_accepted_should_remove_offer_from_pending()
		{
			// Arrange
			var buyer = new TestableBuyer("123");
			var productId = "234";

			this.AddOfferToPendings(buyer, productId);

			// Act
			buyer.MoveOfferToAccepted(productId);

			// Assert
			Assert.Empty(buyer.PendingOffersProductIds);
		}

		[Fact]
		public void Move_offer_to_accepted_should_add_offer_to_accepted()
		{
			// Arrange
			var buyer = new TestableBuyer("123");
			var productId = "234";

			this.AddOfferToPendings(buyer, productId);

			// Act
			buyer.MoveOfferToAccepted(productId);

			// Assert
			Assert.Equal(productId, buyer.AcceptedOffersProductIds[0]);
		}

		[Fact]
		public void Move_offer_to_accepted_without_pending_offer_should_throw_an_exception()
		{
			// Arrange
			var buyer = new TestableBuyer("123");
			var nonExistentProductId = "234";

			// Act

			// Assert
			Assert.Throws<InvalidOperationException>(() => buyer.MoveOfferToAccepted(nonExistentProductId));
		}

		private void AddOfferToPendings(TestableBuyer buyer, string productId)
		{
			var productIds = new List<string>();
			productIds.Add(productId);

			var field = typeof(TestableBuyer).BaseType
				.GetField("productIdsForPendingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
			field.SetValue(buyer, productIds);
		}
	}
}