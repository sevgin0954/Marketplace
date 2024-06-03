namespace Marketplace.API.Services
{
	public class KeywordsService : IKeywordsService
	{
		public string[] GetMatchingKeywordsOrderedByImportance(IEnumerable<string> strs, IEnumerable<string> keywords)
		{
			var keywordsAndOccurenceCount = new Dictionary<string, int>();

			foreach (var str in strs)
			{
				var matchingKeywords = this.GetMatchingKeywords(str, keywords);

				foreach (var  keyword in matchingKeywords)
				{
					if (keywordsAndOccurenceCount.ContainsKey(keyword) == false)
						keywordsAndOccurenceCount[keyword] = 0;

					keywordsAndOccurenceCount[keyword]++;
				}
			}

			return keywordsAndOccurenceCount.OrderByDescending(kvp => kvp.Value).Select(kvp => kvp.Key).ToArray();
		}

		private IEnumerable<string> GetMatchingKeywords(string str, IEnumerable<string> keywords)
		{
			var keywordsResult = new List<string>();

			str = str.ToLower();

			foreach (var keyword in keywords)
			{
				var canMatchKeyword = str.Contains(keyword.ToLower());
				if (canMatchKeyword == true)
				{
					keywordsResult.Add(keyword);
				}
			}

			return keywordsResult;
		}
	}
}
