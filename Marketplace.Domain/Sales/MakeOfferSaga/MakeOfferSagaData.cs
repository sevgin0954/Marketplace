using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS
{
	public class MakeOfferSagaData
	{
		public MakeOfferSagaData(Id buyerId, Id productId, Id sellerId, string message, int quantity)
		{
			this.BuyerId = buyerId;
			this.SellerId = sellerId;
			this.ProductId = productId;
			this.Message = message;
			this.Quantity = quantity;
		}

		public Id BuyerId { get; }

		public Id SellerId { get; }

		public Id ProductId { get; }

		public string Message { get; }

		public int Quantity { get; }

		public bool IsBuyerNotBannedChecked { get; set; }

		public bool IsProductEligableForBuyChecked { get; set; }
	}
}
