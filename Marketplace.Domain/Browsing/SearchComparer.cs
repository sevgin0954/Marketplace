using System.Collections.Generic;

namespace Marketplace.Domain.Browsing
{
	internal class SearchComparer : IComparer<Search>
	{
		public int Compare(Search x, Search y)
		{
			return x.SearchDate.CompareTo(y.SearchDate);
		}
	}
}
