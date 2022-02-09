using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Sales.SellerPersistence
{
	public class SellerDbContext : DbContext
	{
		private readonly string connectionString;

		public SellerDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<Seller> Sellers { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Offer> Offers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new SellerMappingConfiguration());
			modelBuilder.ApplyConfiguration(new OfferMappingConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
