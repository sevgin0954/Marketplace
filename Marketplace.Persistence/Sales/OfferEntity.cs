namespace Marketplace.Persistence.Sales
{
	public class OfferEntity
	{
		public string Id { get; set; }

		public string Status { get; set; }

		public string SellerId { get; set; }
		public SellerEntity Seller { get; set; }

		public string Message { get; set; }

		public string RejectMessage { get; set; }

		public string ProductId { get; set; }
		public ProductEntity Product { get; set; }

		public string BuyerId { get; set; }
		public BuyerEntity Buyer { get; set; }

		public ICollection<BuyerEntity> BuyersWithStartedOffers { get; set; }
	}
}