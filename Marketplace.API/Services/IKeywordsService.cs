namespace Marketplace.API.Services
{
	public interface IKeywordsService
	{
		string[] GetMatchingKeywordsOrderedByImportance(IEnumerable<string> strs, IEnumerable<string> keywords);
	}
}
