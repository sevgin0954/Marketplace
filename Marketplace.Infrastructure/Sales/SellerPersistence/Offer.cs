namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class Offer
	{
		public Offer(string buyerId, string productId)
		{

			this.BuyerId = buyerId;
			this.ProductId = productId;
		}

		public string BuyerId { get; set; }

		public string ProductId { get; set; }
	}
}
