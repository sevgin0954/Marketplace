using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Reviewing.ReviewAggregate
{
	public record CommentReply
	{
		public CommentReply(Id authorId, CommentDescription description)
		{
			this.AuthorId = authorId;
			this.Description = description;
			this.CreatedAt = DateTime.UtcNow;
		}

		public Id AuthorId { get; }

		public CommentDescription Description { get; }

		public DateTime CreatedAt { get; }
	}
}
