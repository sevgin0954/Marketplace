using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Sales.BuyerPersistence
{
	public class OfferMappingConfiguration : IEntityTypeConfiguration<Offer>
	{
		public void Configure(EntityTypeBuilder<Offer> builder)
		{
			builder
				.HasKey(o => o.Id);

			builder
				.Property(o => o.Id)
				.ValueGeneratedOnAdd();

			builder
				.Property(o => o.ProductId)
				.IsRequired();
		}
	}
}
