using System;
using Marketplace.Domain.Browsing.CategoryAggregate;

namespace Marketplace.Domain.Browsing
{
    public class View
    {
        public View(Category category, Search search = null)
        {
            Category = category;
            Search = search;
            ViewDate = DateTime.Now;
        }

        internal Category Category { get; }

        internal Search Search { get; }

        internal DateTime ViewDate { get; }

		public override bool Equals(object other)
		{
			if (other == null)
				return false;
			if ((other is View) == false)
				return false;

			var otherAsView = other as View;

			var isCategoryEqual = otherAsView.Category == this.Category;
			if (isCategoryEqual == false)
				return false;

			if (otherAsView.Search != this.Search)
				return false;

			return true;
		}

		public static bool operator ==(View left, View right)
		{
			if (left is null || right is null)
				return false;

			return left.Equals(right);
		}

		public static bool operator !=(View left, View right)
		{
			if (left is null || right is null)
				return false;

			return left.Equals(right)!;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Category.GetHashCode(), this.Search.GetHashCode());
		}
	}
}
