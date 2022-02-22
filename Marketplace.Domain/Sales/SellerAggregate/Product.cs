using Marketplace.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	public class Product : AggregateRoot
	{
		private ICollection<string> pictureIds = new List<string>();

		public Product(string name, decimal price, string description, string creatorId)
		{
			this.Price = price;
			this.Name = name;
			this.Description = description;
			this.CreatorId = creatorId;
			this.Status = ProductStatus.Unsold;
		}

		public decimal Price { get; private set; }

		public string Name { get; private set; }

		public string Description { get; private set; }

		public string CreatorId { get; private set; }

		public long TotalViews { get; private set; }

		public int Quantity { get; private set; }

		public ProductStatus Status { get; set; }

		public IReadOnlyList<string> PictureIds => this.pictureIds.ToList();

		public void IncreaseTotalViews()
		{
			this.TotalViews++;
		}

		public void AddPicture(string pictureId)
		{
			if (this.PictureIds.Count == ProductConstants.MAX_PICTURES_COUNT)
				throw new InvalidOperationException();

			this.pictureIds.Add(pictureId);
		}

		public void Edit(Product editedProduct)
		{
			if (editedProduct.CreatorId != this.CreatorId)
				throw new InvalidOperationException();

			this.Price = editedProduct.Price;
			this.Name = editedProduct.Name;
			this.Description = editedProduct.Description;
			this.CreatorId = editedProduct.CreatorId;
			this.pictureIds = editedProduct.pictureIds;
		}

		public void Buy(int quantity)
		{
			this.ValidateIfProductCanBeSold(quantity);

			this.Quantity -= quantity;

			if (this.Quantity == 0)
				this.Status = ProductStatus.Sold;
		}

		public void ValidateIfProductCanBeSold(int quantity)
		{
			if (this.Status != ProductStatus.Unsold)
				throw new InvalidOperationException();

			if (quantity > this.Quantity)
				throw new InvalidOperationException();
		}
	}
}