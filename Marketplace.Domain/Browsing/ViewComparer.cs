using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Browsing
{
	internal class ViewComparer : IComparer<View>
	{
		public int Compare(View x, View y)
		{
			return x.ViewDate.CompareTo(y.ViewDate);
		}
	}
}
