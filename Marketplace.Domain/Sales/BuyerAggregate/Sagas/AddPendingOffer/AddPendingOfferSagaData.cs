using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.BuyerAggregate.Sagas.AddPendingOffer
{
	public record AddPendingOfferSagaData
	{
		public bool BuyerBannedStatusHasBeenChecked { get; set; }

		public bool ProductCanBeBought { get; set; }
	}
}
