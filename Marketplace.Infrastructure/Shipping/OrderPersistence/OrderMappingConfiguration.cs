using Marketplace.Domain.Shipping.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class OrderMappingConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder
				.HasKey(o => o.Id);

			builder
				.Property(o => o.Id)
				.ValueGeneratedOnAdd();

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
				.Property(o => o.CanceledOrderBy)
				.HasConversion(
					v => v.ToString(),
					v => Enum.Parse<CanceledOrderBy>(v)
				).IsRequired();
		}
	}
}
