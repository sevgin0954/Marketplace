using System.Collections.Generic;

namespace Marketplace.Infrastructure.Sales.BuyerPersistence
{
	public class Buyer
	{
		public string Id { get; set; }

		public ICollection<Offer> PendingOffers = new List<Offer>();

		public ICollection<Offer> AcceptedOffers = new List<Offer>();
	}
}
