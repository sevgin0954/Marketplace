using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class SellerMappingConfiguration : IEntityTypeConfiguration<Seller>
	{
		public void Configure(EntityTypeBuilder<Seller> builder)
		{
			builder
				.HasKey(s => s.Id);

			builder
				.HasMany(s => s.ProductForSale);

			builder
				.HasMany(s => s.AcceptedOffers);

			builder
				.HasMany(s => s.ArchivedProducts);

			builder
				.HasMany(s => s.ReceivedOffers);

			builder
				.HasMany(s => s.DeclinedOffers);
		}
	}
}
