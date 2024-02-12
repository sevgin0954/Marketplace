using Marketplace.Domain.Browsing.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.Browsing
{
	public class BrowsingDbContext : MarketplaceDbContext
	{
		protected BrowsingDbContext() { }

		public BrowsingDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<ProductEntity> Products => Set<ProductEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductEntity>(product =>
			{
				product.HasKey(p => p.Id);

				product
					.Property(p => p.Name)
					.HasMaxLength(ProductConstants.MAX_NAME_LENGTH)
					.IsRequired();

				product
					.Property(p => p.Description)
					.HasMaxLength(ProductConstants.MAX_DESCRIPTION_LENGTH)
					.IsRequired();

				product
					.Property(p => p.ViewCount)
					.HasDefaultValue(0);

				product
					.Property(p => p.SellerId)
					.IsRequired();
			});
			modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity
			{
				Description = "Description",
				Id = "1",
				Name = "Name",
				SellerId = "1"
			});
		}
	}
}