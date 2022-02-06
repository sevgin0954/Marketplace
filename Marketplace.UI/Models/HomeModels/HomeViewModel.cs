using System.Collections.Generic;

namespace Marketplace.UI.Models.HomeModels
{
	public class HomeViewModel
	{
		public ICollection<ProductViewModel> Products { get; set; }
	}
}