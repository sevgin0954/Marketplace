using Marketplace.Domain.Sales.ProductAggregate;
using System.Collections.Generic;

namespace Marketplace.Infrastructure.Sales.ProductPersistence
{
	public class Product
	{
		private Product(
			decimal price,
			string name,
			string description,
			string creatorId)
		{
			this.Price = price;
			this.Name = name;
			this.Description = description;
			this.CreatorId = creatorId;
		}

		public Product(
			decimal price,
			string name,
			string description,
			string creatorId,
			Status status) : this(price, name, description, creatorId)
		{
			this.Status = status;
		}

		public string Id { get; set; }

		public decimal Price { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public string CreatorId { get; set; }

		public long TotalViews { get; set; }

		public Status Status { get; set; }

		public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
	}
}
