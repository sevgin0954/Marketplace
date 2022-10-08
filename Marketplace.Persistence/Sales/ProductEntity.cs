namespace Marketplace.Persistence.Sales
{
	public class ProductEntity
	{
		public string Id { get; set; }

		public string SellerId { get; set; }
		public SellerEntity Seller { get; set; }

		public decimal Price { get; set; }
		public string PriceCurrency { get; set; }

		public string Status { get; set; }
	}
}
