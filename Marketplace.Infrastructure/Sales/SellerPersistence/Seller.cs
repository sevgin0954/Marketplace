using System.Collections.Generic;

namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class Seller
	{
		public string Id { get; set; }

		public ICollection<Product> ProductForSale { get; set; } = new List<Product>();


		public ICollection<Offer> AcceptedOffers = new List<Offer>();

		public ICollection<Product> ArchivedProducts { get; set; } = new List<Product>();

		public ICollection<Offer> ReceivedOffers { get; set; } = new List<Offer>();

		public ICollection<Offer> DeclinedOffers { get; set; } = new List<Offer>();
	}
}
