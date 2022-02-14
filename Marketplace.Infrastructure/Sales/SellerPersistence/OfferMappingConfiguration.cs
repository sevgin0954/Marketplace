using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class OfferMappingConfiguration : IEntityTypeConfiguration<Offer>
	{
		public void Configure(EntityTypeBuilder<Offer> builder)
		{
			builder
				.HasKey(o => new { o.BuyerId, o.ProductId });

			builder
				.Property(o => o.BuyerId)
				.ValueGeneratedOnAdd();

			builder
				.Property(o => o.ProductId)
				.ValueGeneratedOnAdd();
		}
	}
}
