namespace Marketplace.Persistence.Sales
{
	public class SellerEntity
	{
		public string Id { get; set; }

		public IList<BuyerEntity> BannedBuyers { get; set; }

		public IList<OfferEntity> Offers { get; set; }
	}
}
