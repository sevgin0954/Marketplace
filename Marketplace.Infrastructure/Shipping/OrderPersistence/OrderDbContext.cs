using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class OrderDbContext : DbContext
	{
		private readonly string connectionString;

		public OrderDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<Order> Orders { get; set; }

		public DbSet<Status> Statuses { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema("Shipping.Order");

			modelBuilder.ApplyConfiguration(new OrderMappingConfiguration());
			modelBuilder.ApplyConfiguration(new StatusMappingConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
