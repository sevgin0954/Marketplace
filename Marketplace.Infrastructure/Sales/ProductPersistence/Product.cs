using Marketplace.Domain.Sales.ProductAggregate;
using System.Collections.Generic;

namespace Marketplace.Infrastructure.Sales.ProductPersistence
{
	public class Product
	{
		public string Id { get; set; }

		public decimal Price { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string CreatorId { get; set; }

		public long TotalViews { get; set; }

		public ProductStatus Status { get; set; }

		public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
	}
}
