using Microsoft.EntityFrameworkCore;

namespace Marketplace.Infrastructure.Identity
{
	public class IdentityDbContext : DbContext
	{
		private readonly string connectionString;

		public IdentityDbContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public DbSet<User> Users { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(connectionString);

			base.OnConfiguring(optionsBuilder);
		}
	}
}
