namespace Marketplace.Persistence.Browsing
{
	public class UserEntity
	{
		public string Id { get; set; }

		public IEnumerable<ViewEntity> Views { get; set; }

		public IEnumerable<SearchEntity> Searches { get; set; }
	}
}
