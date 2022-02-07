using Marketplace.Domain.Shipping.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Marketplace.Infrastructure.Shipping.Mappings
{
	public class OrderMappingConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.Property(o => o.Status)
				.HasConversion(
				v => v.ToString(),
				v => Enum.Parse<Status>(v))
				.IsRequired();

			builder
				.Property(o => o.TrackingNumber)
				.HasField("trackingNumber")
				.HasMaxLength(OrderConstants.MaxTrackingNumberLenght)
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
