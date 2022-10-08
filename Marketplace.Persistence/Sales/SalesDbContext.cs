using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.Sales
{
	public class SalesDbContext : MarketplaceDbContext
	{
		protected SalesDbContext() { }

		public SalesDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<ProductEntity> Products { get; set; }

		public DbSet<SellerEntity> Sellers { get; set; }

		public DbSet<BuyerEntity> Buyers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductEntity>(product =>
			{
				product
					.HasKey(p => p.Id);

				product
					.Property(p => p.Status)
					.IsRequired();

				product
					.HasOne(p => p.Seller)
					.WithMany()
					.HasForeignKey(p => p.SellerId)
					.IsRequired(false);


				product
					.Property(p => p.Price)
					.IsRequired();

				product
					.Property(p => p.PriceCurrency)
					.IsRequired();
			});

			modelBuilder.Entity<SellerEntity>(seller =>
			{
				seller
					.HasKey(s => s.Id);

				seller
					.HasMany(s => s.BannedBuyers)
					.WithMany(b => b.SellersWhereBuyerIsBanned);
			});

			modelBuilder.Entity<OfferEntity>(offer =>
			{
				offer
					.HasKey(o => o.Id);

				offer
					.Property(o => o.Status)
					.IsRequired();

				offer
					.HasOne(o => o.Seller)
					.WithMany(s => s.Offers)
					.HasForeignKey(o => o.SellerId)
					.IsRequired(false); ;

				offer
					.Property(o => o.Message)
					.IsRequired();

				offer
					.Property(o => o.RejectMessage)
					.IsRequired(false);

				offer
					.HasOne(o => o.Product)
					.WithMany()
					.HasForeignKey(o => o.ProductId)
					.IsRequired(false);

				offer
					.HasOne(o => o.Buyer)
					.WithMany()
					.HasForeignKey(o => o.BuyerId)
					.IsRequired(false);
			});

			modelBuilder.Entity<BuyerEntity>(buyer =>
			{
				buyer.HasKey(b => b.Id);

				buyer
					.HasMany(b => b.StartedPendingOffers)
					.WithMany(o => o.BuyersWithStartedOffers);

				buyer
					.Property(b => b.PendingOffersCount)
					.IsRequired();
			});
		}
	}
}
