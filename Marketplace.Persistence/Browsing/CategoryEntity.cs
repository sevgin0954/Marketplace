using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.Browsing.CategoryAggregate;

namespace Marketplace.Persistence.Browsing
{
	public class CategoryEntity : IMappableBothDirections<Category>
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string ParentCategoryId { get; set; }
	}
}
