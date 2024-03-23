using Marketplace.API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.OfferModels
{
	public class OffersBindingModel
	{
		[Required]
		[NotNullOrEmptyStringInCollection]
		public IList<string> ProductIds { get; set; } = new List<string>();


	}
}
