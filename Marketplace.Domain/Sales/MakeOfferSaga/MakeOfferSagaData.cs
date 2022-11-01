using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS
{
	public class MakeOfferSagaData : SagaData
	{
		public MakeOfferSagaData(MakeOfferSagaId sagaId, Id sellerId, string message, int quantity)
			: base(sagaId)
		{
			this.BuyerId = sagaId.BuyerId;
			this.SellerId = sellerId;
			this.ProductId = sagaId.ProductId;
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
