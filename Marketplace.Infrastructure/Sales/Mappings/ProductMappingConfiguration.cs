using Marketplace.Domain.Sales.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Marketplace.Infrastructure.Sales.Mappings
{
	public class ProductMappingConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
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
				.Property(p => p.Status)
				.HasConversion(
					v => v.ToString(),
					v => Enum.Parse<ProductStatus>(v))
				.IsRequired();

			builder
				.HasMany(p => p.PictureIds);
		}
	}
}
