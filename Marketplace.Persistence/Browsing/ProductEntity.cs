using AutoMapperRegistrar.Interfaces;
using BrowsingBoundedContext = Marketplace.Domain.Browsing;
using SalesBoundedContext = Marketplace.Domain.Sales;

namespace Marketplace.Persistence.Browsing
{
	public class ProductEntity : 
		IMappableBothDirections<BrowsingBoundedContext.ProductAggregate.Product>, 
		IMappableBothDirections<SalesBoundedContext.ProductAggregate.Product>
	{
		public string Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string SellerId { get; set; }

		public int ViewCount { get; set; }
	}
}
