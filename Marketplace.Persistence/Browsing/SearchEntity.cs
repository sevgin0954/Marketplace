namespace Marketplace.Persistence.Browsing
{
	public class SearchEntity
	{
		internal IEnumerable<string> Keywords { get; set; }

		internal DateTime SearchDate { get; set; }
	}
}
