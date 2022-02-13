using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Sales.BuyerPersistence
{
	public class BuyerDbContext : DbContext
	{
		private readonly string connectionString;

		public BuyerDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<Buyer> Buyers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BuyerMappingConfiguration());
			modelBuilder.ApplyConfiguration(new OfferMappingConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}