using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class OfferEntity
	{
		public int Id { get; set; }

		public string Status { get; set; }

		public string SellerId { get; set; }
		public SellerEntity Seller { get; set; }

		public string Message { get; }

		public string RejectMessage { get; private set; }

		public string ProductId { get; }
		public ProductEntity Product { get; set; }

		public string BuyerId { get; set; }
		public BuyerEntity Buyer { get; set; }

		public ICollection<BuyerEntity> BuyersWithStartedOffers { get; set; }
	}
}