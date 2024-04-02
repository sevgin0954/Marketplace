using AutoMapperRegistrar.Interfaces;
using Marketplace.Domain.Sales.SellerAggregate;

namespace Marketplace.Persistence.Sales
{
	public class SellerEntity : IMappableBothDirections<Seller>
	{
		public string Id { get; set; }

		public IList<BuyerEntity> BannedBuyers { get; set; }
	}
}
