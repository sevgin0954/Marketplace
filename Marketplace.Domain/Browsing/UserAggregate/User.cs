using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Browsing.UserAggregate
{
	public class User : AggregateRoot
	{
		private const int MAX_RECOMMENDED_ELEMENTS_COUNT = 5;

		private readonly SortedDictionary<View, int> recommendedViewAndViewCount;
		private readonly SortedDictionary<Search, int> recommendedSearchesAndSearchCount;

		public User(Id id)
			: base(id)
		{
			this.recommendedViewAndViewCount = new SortedDictionary<View, int>(new ViewComparer());
			this.recommendedSearchesAndSearchCount = new SortedDictionary<Search, int>(new SearchComparer());
		}

		public void SearchProduct(Search search)
		{
			this.AddRecommendedElement(this.recommendedSearchesAndSearchCount, search);
		}

		public void ViewProduct(View view)
		{
			this.AddRecommendedElement(this.recommendedViewAndViewCount, view);
		}

		private void AddRecommendedElement<T>(IDictionary<T, int> elementsAndCounts, T elementToAdd)
			where T : class
		{
			if (elementsAndCounts.ContainsKey(elementToAdd))
			{
				elementsAndCounts[elementToAdd]++;
			}
			else if (elementsAndCounts.Count < MAX_RECOMMENDED_ELEMENTS_COUNT)
			{
				elementsAndCounts[elementToAdd] = 1;
			}
			else
			{
				var oldestSearchAndCount = elementsAndCounts.Last();

				elementsAndCounts.Remove(oldestSearchAndCount.Key);
				elementsAndCounts[elementToAdd] = 1;
			}
		}

		public IList<Category> GetRecommendedCategoryOrdered()
		{
			return this.recommendedViewAndViewCount.Select(rvc => rvc.Key.Category).ToList();
		}

		public IEnumerable<KeyValuePair<Category, string[]>> GetRecommendedCategoryWithKeywordsOrdered()
		{
			var viewsFromSearches = this.recommendedViewAndViewCount.Where(vvc => vvc.Key.Search != null).Select(vc => vc.Key);
			var categoriesAndKeywords = viewsFromSearches.Select(v => KeyValuePair.Create(v.Category, v.Search.Keywords.ToArray()));

			return categoriesAndKeywords;
		}
	}
}