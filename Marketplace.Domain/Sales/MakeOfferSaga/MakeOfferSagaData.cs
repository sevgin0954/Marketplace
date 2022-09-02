namespace Marketplace.Domain.Sales.MakeOfferSaga
{
	public class MakeOfferSagaData
	{
		public MakeOfferSagaData(string buyerId, string productId, string message, int quantity, string sellerId)
		{
			this.BuyerId = buyerId;
			this.SellerId = sellerId;
			this.ProductId = productId;
			this.Message = message;
			this.Quantity = quantity;
		}

		public string BuyerId { get; }

		public string SellerId { get; }

		public string ProductId { get; }

		public string Message { get; }

		public int Quantity { get; }

		public bool IsBuyerBanChecked { get; set; }

		public bool IsProductEligableForBuyChecked { get; set; }
	}
}
