using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.SharedKernel;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.OfferAggregateSpecs.OfferSpecs
{
	public class DiscardOfferSpecs
	{
		[Fact]
		public void Discard_offer_with_null_initiator_id_should_throw_an_exception()
		{
			// Arrange
			const string initiatorIdParamName = "initiatorId";

			var offer = OfferFactory.Create();
			Id initiatorId = null;

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(initiatorIdParamName, () => offer.DiscardOffer(initiatorId));
		}

		[Fact]
		public void Discard_offer_with_initiator_id_different_from_the_buyer_id_should_throw_an_exception()
		{
			// Arrange
			var buyerId = new Id();
			var offer = OfferFactory.CreateWithBuyerId(buyerId);
			var initiatorId = new Id();

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => offer.DiscardOffer(initiatorId));
			Assert.Equal(ErrorConstants.INITIATOR_SHOULD_BE_THE_BUYER, exception.Message);
		}

		[Fact]
		public void Discard_offer_with_status_different_from_pending_should_throw_an_exception()
		{
			// Arrange
			const string EXPECTED_EXCEPTION_MESSSAGE = "Can't discard non pending offer!";

			var buyerId = new Id();
			var sellerId = new Id();
			var offer = OfferFactory.Create(buyerId, sellerId);

			offer.AcceptOffer(sellerId);

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => offer.DiscardOffer(buyerId));
			Assert.Equal(EXPECTED_EXCEPTION_MESSSAGE, exception.Message);
		}

		[Fact]
		public void Discard_offer_should_change_offer_status_to_discarded()
		{
			// Arrange
			var buyerId = new Id();
			var sellerId = new Id();
			var offer = OfferFactory.Create(buyerId, sellerId);

			// Act
			offer.DiscardOffer(buyerId);

			// Assert
			Assert.Equal(OfferStatus.Discarded, offer.Status);
		}
	}
}