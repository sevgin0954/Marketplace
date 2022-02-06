using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.Sales.SellerAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Marketplace.Infrastructure.Sales
{
	public class SalesDbContext : DbContext
	{
		private readonly string connectionString;

		public SalesDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<Seller> Sellers { get; set; }

		public DbSet<Buyer> Buyers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}