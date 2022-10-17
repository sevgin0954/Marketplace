using System;

namespace Marketplace.Domain.Reviewing.ReviewAggregate
{
	internal record CommentEdit
	{
		public CommentEdit(CommentDescription description)
		{
			this.Description = description;
			this.CreatedAt = DateTime.Now;
		}

		public CommentDescription Description { get; }

		public DateTime CreatedAt { get; }
	}
}