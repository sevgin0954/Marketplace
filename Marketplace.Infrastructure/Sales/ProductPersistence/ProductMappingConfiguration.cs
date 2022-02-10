using Marketplace.Domain.Sales.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Marketplace.Infrastructure.Sales.Mappings.ProductPersistence
{
	public class ProductMappingConfiguration : IEntityTypeConfiguration<Sales.ProductPersistence.Product>
	{
		public void Configure(EntityTypeBuilder<Sales.ProductPersistence.Product> builder)
		{
			builder
				.Property(p => p.Price)
				.IsRequired();

			builder
				.Property(p => p.Name)
				.HasMaxLength(ProductConstants.NameMaxLength)
				.IsRequired();

			builder
				.Property(p => p.Description)
				.HasMaxLength(ProductConstants.DescriptionMaxLength)
				.IsRequired();

			builder
				.Property(p => p.CreatorId)
				.IsRequired();

			builder
				.Property(p => p.TotalViews)
				.IsRequired();

			builder
				.HasOne(p => p.Status);

			builder
				.HasMany(p => p.Pictures);
		}
	}
}