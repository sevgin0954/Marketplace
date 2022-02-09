namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class Order
	{
		public string Id { get; set; }

		public string TrackingNumber { get; set; }

		public string SellerId { get; set; }

		public string BuyerId { get; set; }

		public string CanceledById { get; set; }

		public Status Status { get; set; }
	}
}
