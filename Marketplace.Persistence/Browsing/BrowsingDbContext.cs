using Marketplace.Domain.Browsing.ProductAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.Browsing
{
	public class BrowsingDbContext : MarketplaceDbContext
	{
		public BrowsingDbContext() { }

		public BrowsingDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<ProductEntity> Products => Set<ProductEntity>();

		public DbSet<UserEntity> Users => Set<UserEntity>();

		public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserEntity>(user =>
			{
				user.HasKey(u => u.Id);

				user.OwnsMany(u => u.Searches);

				user.OwnsMany(u => u.Views)
					.OwnsOne(v => v.Search);
			});

			modelBuilder.Entity<CategoryEntity>(category =>
			{
				category.HasKey(c => c.Name);

				category.Property(c => c.ParentCategoryId)
					.IsRequired(false);
			});
			
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

				product
					.OwnsMany(p => p.Images);
			});
		}
	}
}