using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Browsing
{
	public class Search
	{
		private const int KEYWORDS_MAX_COUNT = 3;

		public Search(ICollection<string> keywords)
		{
			if (keywords.Count > KEYWORDS_MAX_COUNT)
				keywords = keywords.Take(KEYWORDS_MAX_COUNT).ToList();

			this.Keywords = new SortedSet<string>(keywords);

			this.SearchDate = DateTime.Now;
		}

		internal SortedSet<string> Keywords { get; }

		internal DateTime SearchDate { get; }

		public override bool Equals(object other)
		{
			if (other == null) 
				return false;
			if ((other is Search) == false)
				return false;

			var otherAsSearch = other as Search;

			if (otherAsSearch.Keywords.Count != this.Keywords.Count)
				return false;

			foreach (var keyword in this.Keywords)
			{
				if (otherAsSearch.Keywords.Contains(keyword) == false) 
					return false;
			}

			return true;
		}

		public static bool operator ==(Search left, Search right)
		{
			if (left is null || right is null)
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(Search left, Search right)
		{
			if (left is null || right is null)
				return false;

			return left.Equals(right)!;
		}

		public override int GetHashCode()
		{
			var keywordsCombined = String.Join(' ', this.Keywords);
			return HashCode.Combine(keywordsCombined);
		}
	}
}
