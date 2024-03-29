﻿using Marketplace.Domain.Sales.OfferAggregate;
using Marketplace.Domain.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.Sales
{
	public class SalesDbContext : MarketplaceDbContext
	{
		protected SalesDbContext() { }

		public SalesDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<ProductEntity> Products => Set<ProductEntity>();

		public DbSet<SellerEntity> Sellers => Set<SellerEntity>();

		public DbSet<BuyerEntity> Buyers => Set<BuyerEntity>();

		public DbSet<OfferEntity> Offers => Set<OfferEntity>();

		public DbSet<MakeOfferSagaDataEntity> MakeOfferSagaDatas => Set<MakeOfferSagaDataEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductEntity>(product =>
			{
				const int CURRENCY_LENGTH = 3;

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
					.IsRequired()
					.HasMaxLength(CURRENCY_LENGTH);
			});
			modelBuilder.Entity<ProductEntity>().HasData(new ProductEntity
			{
				Id = "1",
				Price = 1,
				PriceCurrency = Enum.GetName(Currency.BGN)!,
				SellerId = "1",
				Status = "IN Sale"
			});

			modelBuilder.Entity<SellerEntity>(seller =>
			{
				seller
					.HasKey(s => s.Id);

				seller
					.HasMany(s => s.BannedBuyers)
					.WithMany(b => b.SellersWhereBuyerIsBanned);
			});
			modelBuilder.Entity<SellerEntity>().HasData(new SellerEntity()
			{
				Id = "1"
			});

			modelBuilder.Entity<BuyerEntity>(buyer =>
			{
				buyer.HasKey(b => b.Id);

				buyer
					.Property(b => b.PendingOffersCount)
					.IsRequired();

				buyer.OwnsMany(b => b.StartedOffersProductsIds);
			});
			modelBuilder.Entity<BuyerEntity>().HasData(new BuyerEntity()
			{
				Id = "1",
				PendingOffersCount = 1
			});

			modelBuilder.Entity<OfferEntity>(offer =>
			{
				offer.HasKey(b => b.Id);

				offer.HasOne(o => o.Seller);

				offer.HasOne(o => o.Product);

				offer.HasOne(o => o.Buyer);
			});

			modelBuilder.Entity<MakeOfferSagaDataEntity>(sagaData =>
			{
				sagaData.HasKey(s => new { s.SellerId, s.BuyerId, s.ProductId });

				sagaData.Property(s => s.Message)
					.IsRequired();

				sagaData.Property(s => s.Quantity)
					.IsRequired();

				sagaData.Property(s => s.IsBuyerNotBannedChecked)
					.IsRequired()
					.HasDefaultValue(false);

				sagaData.Property(s => s.IsProductEligableForBuyChecked)
					.IsRequired()
					.HasDefaultValue(false);
			});
		}
	}
}