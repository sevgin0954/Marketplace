using Marketplace.Domain.Sales.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Linq;

namespace Marketplace.Infrastructure.Sales.ProductPersistence
{
	public class StatusMappingConfiguration : IEntityTypeConfiguration<Status>
	{
		public void Configure(EntityTypeBuilder<Status> builder)
		{
			var statusNames = Enum.GetNames(typeof(Domain.Shipping.OrderAggregate.Status));
			var statuses = statusNames.Select(name => new Status(name) { Id = Guid.NewGuid().ToString() });
			builder.HasData(statuses);

			builder
				.Property(s => s.Name)
				.HasMaxLength(ProductConstants.StatusNameMaxLength)
				.IsRequired();
		}
	}
}
