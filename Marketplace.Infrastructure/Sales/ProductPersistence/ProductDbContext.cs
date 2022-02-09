using Marketplace.Infrastructure.Sales.Mappings.ProductPersistence;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Sales.ProductPersistence
{
	public class ProductDbContext : DbContext
	{
		private readonly string connectionString;

		public ProductDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<Picture> Pictures { get; set; }

		public DbSet<Product> Products { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ProductMappingConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
