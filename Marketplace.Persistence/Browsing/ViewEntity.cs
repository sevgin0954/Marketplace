using Marketplace.Domain.Browsing;

namespace Marketplace.Persistence.Browsing
{
	public class ViewEntity
	{
		public CategoryEntity Category { get; set; }

		public SearchEntity Search { get; set; }

		public DateTime ViewDate { get; set; }
	}
}
