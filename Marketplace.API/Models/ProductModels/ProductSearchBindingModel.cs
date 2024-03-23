using Marketplace.API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Marketplace.API.Models.ProductModels
{
	public class ProductSearchBindingModel
	{
		const int MAX_INDIVIDIAL_KEYWORD_LENGTH = 30;

		const int MAX_KEYWORDS_COUNT = 10;

		[MaxLength(MAX_KEYWORDS_COUNT)]
		[NotNullOrEmptyStringInCollection]
		[MaxStringLengthInCollection(MAX_INDIVIDIAL_KEYWORD_LENGTH)]
		public ICollection<string> KeyWords { get; set; } = new List<string>();
	}
}
