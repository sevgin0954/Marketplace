using Marketplace.API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.OfferModels
{
	public class OffersBindingModel
	{
		const int MIN_COUNT_PRODUCT_IDS = 1;

		[Required]
		[NotNullOrEmptyStringInCollection]
		[MinCollectionLength(MIN_COUNT_PRODUCT_IDS)]
		public IList<string> ProductIds { get; set; } = new List<string>();


	}
}
