using Marketplace.Domain.Shipping.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class StatusMappingConfiguration : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			builder
				.HasKey(s => s.Id);

			builder
				.Property(s => s.Name)
				.HasMaxLength(OrderConstants.StatusNameMaxLength)
				.IsRequired();

			var statusNames = Enum.GetNames(typeof(Domain.Shipping.OrderAggregate.Status));
			var statuses = statusNames.Select(name => new Status(name) { Id = Guid.NewGuid().ToString() });
			builder
				.HasData(statuses);
		}
	}
}
