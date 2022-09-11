using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.SharedKernel;
using System;
using Xunit;

namespace Marketplace.Tests.Sales.OfferSpecs
{
	public class AcceptOfferSpecs
	{
		[Fact]
		public void Accept_offer_with_null_initiator_id_should_throw_an_exception()
		{
			// Arrange
			const string initiatorIdParamName = "initiatorId";

			Id initiatorId = null;
			var offer = OfferFactory.Create();

			// Act
			// Assert
			Assert.Throws<ArgumentNullException>(initiatorIdParamName, () => offer.AcceptOffer(initiatorId));
		}

		[Fact]
		public void Accept_offer_with_initiator_id_different_from_the_seller_id_should_throw_an_exception()
		{
			// Arrange
			var initiatorId = new Id();
			var sellerId = new Id();

			var offer = OfferFactory.CreateWithSellerId(sellerId);

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => offer.AcceptOffer(initiatorId));
			Assert.Equal(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER, exception.Message);
		}

		[Fact]
		public void Accept_offer_with_status_different_from_pending_should_throw_an_exception()
		{
			// Arrange
			const string EXPECTED_EXCEPTION_MESSAGE = "Can't accept non pending offer!";

			var sellerId = new Id();
			var offer = OfferFactory.CreateWithSellerId(sellerId);

			offer.AcceptOffer(sellerId);

			// Act
			// Assert
			var exception = Assert.Throws<InvalidOperationException>(() => offer.AcceptOffer(sellerId));
			Assert.Equal(EXPECTED_EXCEPTION_MESSAGE, exception.Message);
		}

		[Fact]
		public void Accept_offer_should_change_offer_status_to_accepted()
		{
			// Arrange
			var sellerId = new Id();
			var offer = OfferFactory.CreateWithSellerId(sellerId);

			// Act
			offer.AcceptOffer(sellerId);

			// Assert
			Assert.Equal(OfferStatus.Accepted, offer.Status);
		}
	}
}
