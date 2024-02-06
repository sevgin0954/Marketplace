namespace Marketplace.Persistence.Sales
{
	public class OfferEntity
	{
		public string Id { get; set; }

		public SellerEntity Seller { get; set; }

		public string Message { get; set; }

		public ProductEntity Product { get; set; }

		public BuyerEntity Buyer { get; set; }

		public string Status { get; set; }
	}
}
