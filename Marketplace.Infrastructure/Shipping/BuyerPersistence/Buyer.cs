using System.Collections.Generic;

namespace Marketplace.Infrastructure.Shipping.BuyerPersistence
{
	public class Buyer
	{
		public string Id { get; set; }

		public ICollection<Order> Orders { get; set; }
	}
}
