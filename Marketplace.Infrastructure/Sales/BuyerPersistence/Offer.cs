namespace Marketplace.Infrastructure.Sales.BuyerPersistence
{
	public class Offer
	{
		public Offer(string productId)
		{
			this.ProductId = productId;
		}

		public string Id { get; set; }

		public string ProductId { get; set; }
	}
}
