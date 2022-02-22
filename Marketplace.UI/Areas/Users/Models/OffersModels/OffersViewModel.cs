using System.Collections.Generic;

namespace Marketplace.UI.Areas.Users.Models.OffersModels
{
	public class OffersViewModel
	{
		public int AcceptedOffersCount { get; set; }

		public int PendingOffersCount { get; set; }

		public int RejectedOffersCount { get; set; }

		public IList<OfferViewModel> AcceptedOffers { get; set; }

		public IList<OfferViewModel> PendingOffers { get; set; }

		public IList<OfferViewModel> DeclinedOffers { get; set; }
	}
}
