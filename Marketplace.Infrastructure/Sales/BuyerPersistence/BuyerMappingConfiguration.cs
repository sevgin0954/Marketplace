using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Sales.BuyerPersistence
{
	public class BuyerMappingConfiguration : IEntityTypeConfiguration<Buyer>
	{
		public void Configure(EntityTypeBuilder<Buyer> builder)
		{
			builder
				.HasKey(b => b.Id);

			builder
				.HasMany(b => b.AcceptedOffers);

			builder
				.HasMany(b => b.PendingOffers);
		}
	}
}
