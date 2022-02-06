using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Shipping
{
	public class ShippingDbContext : DbContext
	{
		private readonly string connectionString;

		public ShippingDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}
	}
}