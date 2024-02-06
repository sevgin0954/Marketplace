using Marketplace.Domain.Sales.OfferAggregate;

namespace Marketplace.Persistence.Sales
{
	public class OfferRepository : Repository<Offer, OfferId, OfferEntity>
	{
		public OfferRepository(SalesDbContext dbContext) : base(dbContext)
		{
		}
	}
}
