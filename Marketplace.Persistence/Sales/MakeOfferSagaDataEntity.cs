namespace Marketplace.Persistence.Sales
{
	public class MakeOfferSagaDataEntity
	{
		public string BuyerId { get; }

		public string SellerId { get; }

		public string ProductId { get; }

		public string Message { get; }

		public int Quantity { get; }

		public bool IsBuyerNotBannedChecked { get; set; }

		public bool IsProductEligableForBuyChecked { get; set; }
	}
}
