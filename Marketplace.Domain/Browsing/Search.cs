using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Browsing
{
	public class Search
	{
		public Search(string keyword1, string keyword2 = null, string keyword3 = null)
		{
			this.Keywords.Add(keyword1);
			if (keyword2 != null) 
				this.Keywords.Add(keyword2);
			if (keyword3 != null)
				this.Keywords.Add(keyword3);

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
