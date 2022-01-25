using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales
{
	public class Product : AggregateRoot
	{
		private readonly ICollection<Picture> pictures = new List<Picture>();

		public Product(string name, decimal price, string description, Seller creator)
		{
			this.Price = price;
			this.Name = name;
			this.Description = description;
			this.Creator = creator;
			this.Status = ProductStatus.Unsold;
		}

		public decimal Price { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public Seller Creator { get; private set; }

		public long TotalViews { get; private set; }

		public ProductStatus Status { get; set; }

		public IReadOnlyList<Picture> Pictures => this.pictures.ToList();

		public void IncreaseTotalViews()
		{
			this.TotalViews++;
		}

		public void AddPicture(Picture picture)
		{
			// Add some invariants

			this.pictures.Add(picture);
		}

		public void Edit(Product editedProduct)
		{
			if (editedProduct.Creator != this.Creator)
			{
				throw new InvalidOperationException();
			}

			this.Price = editedProduct.Price;
			this.Name = editedProduct.Name;
			this.Description = editedProduct.Description;
			this.Creator = editedProduct.Creator;
		}
	}
}