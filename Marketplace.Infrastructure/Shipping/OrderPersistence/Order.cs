namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class Order
	{
		private Order(
			string sellerId,
			string buyerId)
		{
			this.SellerId = sellerId;
			this.BuyerId = buyerId;
		}

		public Order(
			string sellerId,
			string buyerId,
			Status status
		) : this(sellerId, buyerId)
		{
			this.Status = status;
		}

		public string Id { get; set; }

		public string TrackingNumber { get; set; }

		public string SellerId { get; set; }

		public string BuyerId { get; set; }

		public CanceledOrderBy CanceledOrderBy { get; set; }

		public Status Status { get; set; }
	}
}
