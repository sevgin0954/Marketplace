using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Marketplace.Shared;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Domain.Browsing.ProductAggregate
{
	public class Product : AggregateRoot
	{
		private string name = null!;
		private string description = null!;

		public Product(Id id, string name, string description, Id sellerId, IEnumerable<Image> images)
			: base(id)
		{
			this.Name = name;
			this.Description = description;
			this.ViewCount = 1;
			this.SellerId = sellerId;
			this.AddImages(images);
		}

		public string Name
		{
			get { return this.name; }
			set
			{
				ArgumentValidator.NotNullOrEmpty(value, nameof(this.Name));

				var trimedName = value.Trim();

				ArgumentValidator.MaxLength(trimedName, ProductConstants.MAX_NAME_LENGTH, nameof(this.Name));
				ArgumentValidator.MinLength(trimedName, ProductConstants.MIN_NAME_LENGTH, nameof(this.Name));

				this.name = trimedName;
			}
		}

		public string Description
		{
			get { return this.description; }
			set
			{
				if (value == null)
					return;

				var trimedDescription = value.Trim();

				ArgumentValidator.MaxLength(trimedDescription, ProductConstants.MAX_DESCRIPTION_LENGTH, nameof(this.Description));
				ArgumentValidator.MinLength(trimedDescription, ProductConstants.MIN_DESCRIPTION_LENGTH, nameof(this.Description));

				if (trimedDescription.Length > 0)
					this.description = trimedDescription;
			}
		}

		public Id SellerId { get; } 

		public int ViewCount { get; private set; }

		public ICollection<Image> Images { get; private set; }

		public void RegisterView()
		{
			this.ViewCount++;
		}

		public void AddImages(IEnumerable<Image> images)
		{
			foreach (var image in images)
			{
				var isImageAlreadyAdded = this.Images.Any(i => i.Id == image.Id);
				if (isImageAlreadyAdded)
					throw new ArgumentException($"Image with id: {image.Id} already exists!");

				var isImageWithSamePriorityAdded = this.Images.Any(i => i.DisplayPriority == image.DisplayPriority);
				if (isImageWithSamePriorityAdded)
					throw new ArgumentException($"Image with priority: {image.DisplayPriority} already exists!");

				this.Images.Add(image);
			}
		}
	}
}