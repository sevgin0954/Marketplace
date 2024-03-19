using AutoMapperRegistrar.Interfaces;
using BrowsingBoundedContext = Marketplace.Domain.Browsing;

namespace Marketplace.Persistence.Browsing
{
	public class ProductEntity : 
		IMappableBothDirections<BrowsingBoundedContext.ProductAggregate.Product>
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string SellerId { get; set; }

		public int ViewCount { get; set; }
	}
}
