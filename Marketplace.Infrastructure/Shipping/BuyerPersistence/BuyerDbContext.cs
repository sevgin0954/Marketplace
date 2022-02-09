using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Shipping.BuyerPersistence
{
	public class BuyerDbContext : DbContext
	{
		private readonly string connectionString;

		public BuyerDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<Order> Orders { get; set; }

		public DbSet<Buyer> Buyers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BuyerMappingConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
