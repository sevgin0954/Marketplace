using Marketplace.Domain.Sales;
using Marketplace.Domain.Sales.Events;
using Xunit;

namespace Marketplace.Tests.Sales.BuyerSpecs
{
	public class CreateOfferSpecs
	{
		[Fact]
		public void Create_offer_should_add_offer_to_pending()
		{
			// Arrange
			var buyer = new TestableBuyer("123");
			var productId = "234";
			var sellerId = "345";

			var expectedOffer = new Offer(buyer.Id, productId, sellerId);

			// Act
			buyer.CreateOffer(productId, sellerId);

			// Assert
			Assert.Equal(expectedOffer, buyer.PendingOffers[0]);
		}

		[Fact]
		public void Create_offer_should_raise_an_event()
		{
			// Arrange
			var buyer = new TestableBuyer("123");
			var productId = "234";
			var sellerId = "345";

			// Act
			buyer.CreateOffer(productId, sellerId);

			// Assert
			var domainEvent = buyer.DomainEvents[0];
			Assert.Equal(typeof(OfferCreatedEvent), domainEvent.GetType());
			Assert.Equal(sellerId, (domainEvent as OfferCreatedEvent).SellerId);
			Assert.Equal(productId, (domainEvent as OfferCreatedEvent).ProductId);
		}
	}
}
