using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Reviewing.ReviewAggregate
{
	public record ReviewId : Id
	{
		public ReviewId(Id productId, Id authorId)
			: base(productId.Value + authorId.Value)
		{
			this.ProductId = productId;
			this.AuthorId = authorId;
		}

		public Id ProductId { get; }

		public Id AuthorId { get; }
	}
}
