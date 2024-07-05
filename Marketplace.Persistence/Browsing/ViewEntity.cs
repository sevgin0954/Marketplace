using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.Browsing;

namespace Marketplace.Persistence.Browsing
{
	public class ViewEntity : IMappableBothDirections<View>
	{
		public string Id { get; set; }

		public string UserId { get; set; }
		public UserEntity User { get; set; }

		public CategoryEntity Category { get; set; }
		public string CategoryId { get; set; }

		public SearchEntity Search { get; set; }

		public DateTime ViewDate { get; set; }

		public int ViewCount { get; set; }
	}
}
