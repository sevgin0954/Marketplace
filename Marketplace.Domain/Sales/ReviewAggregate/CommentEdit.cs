﻿using Marketplace.Domain.Common;
using Marketplace.Shared;
using System;

namespace Marketplace.Domain.Sales.ReviewAggregate
{
	internal record CommentEdit : ValueObject
	{
		public CommentEdit(CommentDescription description)
		{
			ArgumentValidator.NotNullValidator(description, nameof(description));

			this.Description = description;
			this.CreatedAt = DateTime.Now;
		}

		public CommentDescription Description { get; }

		public DateTime CreatedAt { get; }
	}
}