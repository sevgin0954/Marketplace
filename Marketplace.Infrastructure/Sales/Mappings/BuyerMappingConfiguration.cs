using Marketplace.Domain.Sales.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Sales.Mappings
{
	public class BuyerMappingConfiguration : IEntityTypeConfiguration<Buyer>
	{
		public void Configure(EntityTypeBuilder<Buyer> builder)
		{
			builder
				.Property("PendingOffersProductIds")
				.HasField("productIdsForPendingOffers")
				.IsRequired();

			builder
				.Property("AcceptedOffersProductIds")
				.HasField("productIdsForAcceptedOffers")
				.IsRequired();
		}
	}
}
