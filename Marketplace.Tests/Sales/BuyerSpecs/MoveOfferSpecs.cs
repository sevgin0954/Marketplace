using Marketplace.Domain.Sales;
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
			var offer = new Offer(buyer.Id, productId, "345");

			this.AddOfferToPendings(buyer, offer, productId);

			// Act
			buyer.MoveOfferToAccepted(productId);

			// Assert
			Assert.Empty(buyer.PendingOffers);
		}

		[Fact]
		public void Move_offer_to_accepted_should_add_offer_to_accepted()
		{
			// Arrange
			var buyer = new TestableBuyer("123");
			var productId = "234";
			var offer = new Offer(buyer.Id, productId, "345");

			this.AddOfferToPendings(buyer, offer, productId);

			// Act
			buyer.MoveOfferToAccepted(productId);

			// Assert
			Assert.Equal(offer, buyer.AcceptedOffers[0]);
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

		private void AddOfferToPendings(TestableBuyer buyer, Offer offer, string productId)
		{
			var dict = new Dictionary<string, Offer>();
			dict.Add(productId, offer);

			var field = typeof(TestableBuyer).BaseType
				.GetField("productIdsAndPendingOrders", BindingFlags.NonPublic | BindingFlags.Instance);
			field.SetValue(buyer, dict);
		}
	}
}