using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Reviewing.ReviewAggregate
{
	internal class Comment : Entity<Id>
	{
		private const int MAX_NUMBER_EDITS = 3;

		private readonly IDictionary<Id, CommentReply> authorIdAndReplies;
		private readonly IList<CommentEdit> edits;

		public Comment(Id id, Id authorId, string title, CommentDescription description)
			: base(id)
		{
			this.AuthorId = authorId;
			this.Title = title;
			this.Description = description;
			this.CreatedAt = DateTime.Now;
		}

		public Id AuthorId { get; }

		public string Title { get; }

		public CommentDescription Description { get; }

		public DateTime CreatedAt { get; }

		public void Reply(CommentReply reply)
		{
			if (reply.AuthorId == this.AuthorId)
				throw new InvalidOperationException("User can't comment his own review!");
			if (this.authorIdAndReplies.ContainsKey(reply.AuthorId))
				throw new InvalidOperationException("User can't comment to a review more than once!");

			this.authorIdAndReplies[reply.AuthorId] = reply;
		}

		public void Edit(Id initiatorId, CommentEdit edit)
		{
			if (initiatorId == this.AuthorId)
				throw new InvalidOperationException("Only the author can edit the comment!");
			if (this.edits.Count >= MAX_NUMBER_EDITS)
				throw new InvalidOperationException($"User can't edit his comment more than {MAX_NUMBER_EDITS} times!");

			this.edits.Add(edit);
		}
	}
}