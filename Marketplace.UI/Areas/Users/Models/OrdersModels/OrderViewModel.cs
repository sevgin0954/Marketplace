namespace Marketplace.UI.Areas.Users.Models.OrdersModels
{
	public class OrderViewModel
	{
		public string Status { get; set; }

		public string TrackingNumber { get; set; }

		public string SellerId { get; private set; }

		public string BuyerId { get; private set; }

		public string CanceledById { get; private set; }
	}
}
