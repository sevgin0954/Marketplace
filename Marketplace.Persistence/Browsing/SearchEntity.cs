using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.Browsing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace.Persistence.Browsing
{
	public class SearchEntity : IMappableBothDirections<Search>
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }

		public string UserId { get; set; }
		public UserEntity User { get; set; }

		public IEnumerable<string> Keywords { get; set; }

		public DateTime SearchDate { get; set; }

		public int SearchCount { get; set; }
	}
}
