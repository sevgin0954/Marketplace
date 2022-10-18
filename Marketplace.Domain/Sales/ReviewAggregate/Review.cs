using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.ReviewAggregate
{
    public class Review : AggregateRoot<Id>
    {
        public Review(ReviewId id, Score score)
            : base(id)
        {
            this.AuthorId = id.AuthorId;
            this.Score = score;
            this.LikesCount = 0;
        }

        public Id AuthorId { get; }

        public Score Score { get; }

        private Comment? Comment { get; set; }

        public int LikesCount { get; }

        public void AddComment(Id initiatorId, string title, CommentDescription description)
        {
            if (this.Comment != null)
                throw new InvalidOperationException("Comment can be set only once!");
            if (initiatorId != this.AuthorId)
                throw new InvalidOperationException("Only the author of the review can add comment to it!");

            var commentId = new Id();
            var comment = new Comment(commentId, initiatorId, title, description);

            this.Comment = comment;
        }

        public void ReplyToComment(Id initiator, CommentDescription description)
        {
            ArgumentValidator.NotNullValidator(description, nameof(description));

            if (this.Comment == null)
                throw new InvalidOperationException("Can't reply to non existing comment!");

            var reply = new CommentReply(initiator, description);
            this.Comment.Reply(reply);
        }

        public void EditComment(Id initiator, CommentDescription description)
        {
            if (this.Comment == null)
				throw new InvalidOperationException("Can't edit non existing comment!");

			var commentEdit = new CommentEdit(description);
            this.Comment.Edit(initiator, commentEdit);
        }
    }
}