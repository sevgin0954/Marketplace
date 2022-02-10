using Marketplace.Domain.Shipping.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class OrderMappingConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.HasOne(o => o.Status);

			builder
				.Property(o => o.TrackingNumber)
				.HasMaxLength(OrderConstants.TrackingNumberMaxLength)
				.IsRequired();

			builder
				.Property(o => o.SellerId)
				.IsRequired();

			builder
				.Property(o => o.BuyerId)
				.IsRequired();

			builder
				.Property(o => o.CanceledById)
				.IsRequired();
		}
	}
}
