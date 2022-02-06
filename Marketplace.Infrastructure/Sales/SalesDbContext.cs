using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Sales
{
	public class SalesDbContext : DbContext
	{
		private readonly string connectionString;

		public SalesDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}