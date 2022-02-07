using Marketplace.Domain.Shipping.BuyerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Shipping.Mappings
{
	public class BuyerMappingConfiguration : IEntityTypeConfiguration<Buyer>
	{
		public void Configure(EntityTypeBuilder<Buyer> builder)
		{
			builder.HasMany("orderIds");
		}
	}
}
