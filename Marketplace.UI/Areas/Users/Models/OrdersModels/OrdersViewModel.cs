using System.Collections.Generic;

namespace Marketplace.UI.Areas.Users.Models.OrdersModels
{
	public class OrdersViewModel
	{
		public int TotalOrders { get; set; }

		public IList<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
	}
}
