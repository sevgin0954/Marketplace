using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.ReviewAggregate
{
	internal record CommentReply
	{
		public CommentReply(Id authorId, CommentDescription description)
		{
			ArgumentValidator.NotNullValidator(authorId, nameof(authorId));
			ArgumentValidator.NotNullValidator(description, nameof(description));

			this.AuthorId = authorId;
			this.Description = description;
			this.CreatedAt = DateTime.UtcNow;
		}

		public Id AuthorId { get; }

		public CommentDescription Description { get; }

		public DateTime CreatedAt { get; }
	}
}
