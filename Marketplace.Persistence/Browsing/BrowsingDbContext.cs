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

				user.HasMany(u => u.Searches)
					.WithOne(u => u.User)
					.HasForeignKey(u => u.UserId);

				user.HasMany(u => u.Views)
					.WithOne(u => u.User)
					.HasForeignKey(u => u.UserId);
			});

			modelBuilder.Entity<SearchEntity>(search =>
			{
				search.Property(s => s.Keywords)
					.HasConversion(
						v => string.Join(' ', v),
						v => v.Split(' ', StringSplitOptions.RemoveEmptyEntries)
					);
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