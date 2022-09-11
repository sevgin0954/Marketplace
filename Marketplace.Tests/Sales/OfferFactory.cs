using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Tests.Sales
{
	public static class OfferFactory
	{
		const string defaultMessage = "DefaultMessage";

		public static Offer Create()
		{
			var offerId = CreateDefaultOfferId();
			var offer = Create(offerId);

			return offer;
		}

		public static Offer Create(OfferId offerId)
		{
			var offer = new Offer(offerId, new Id(), defaultMessage);

			return offer;
		}

		public static Offer CreateWithBuyerId(Id buyerId)
		{
			var sellerId = new Id();

			return Create(buyerId, sellerId);
		}

		public static Offer CreateWithSellerId(Id sellerId)
		{
			var buyerId = new Id();

			return Create(buyerId, sellerId);
		}

		public static Offer Create(Id buyerId, Id sellerId)
		{
			var offerId = new OfferId(new Id(), buyerId);
			var offer = new Offer(offerId, sellerId, defaultMessage);

			return offer;
		}

		private static OfferId CreateDefaultOfferId()
		{
			return new OfferId(new Id(), new Id());
		}
	}
}
