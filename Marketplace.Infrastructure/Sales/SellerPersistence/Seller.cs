using System.Collections.Generic;

namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class Seller
	{
		public string Id { get; set; }

		public ICollection<Product> ProductForSale { get; set; } = new List<Product>();

		public ICollection<Product> ArchivedProducts { get; set; } = new List<Product>();

		public ICollection<Product> SoldOutProducts { get; set; } = new List<Product>();
	}
}
