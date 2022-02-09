using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Shipping.BuyerPersistence
{
	public class BuyerMappingConfiguration : IEntityTypeConfiguration<Buyer>
	{
		public void Configure(EntityTypeBuilder<Buyer> builder)
		{
			builder.HasKey(b => b.Id);

			builder.HasMany(b => b.Orders);
		}
	}
}
