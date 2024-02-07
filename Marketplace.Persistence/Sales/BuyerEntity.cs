namespace Marketplace.Persistence.Sales
{
	public class BuyerEntity
	{
		public string Id { get; set; }

		public int PendingOffersCount { get; set; }

		public IList<SellerEntity> SellersWhereBuyerIsBanned { get; set; }

		public IList<Product> StartedOffersProductsIds { get; set; }
	}

	public class Product
	{
		public string Id { get; set; }
	}
}
