using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.IdentityAndAccess
{
	public class IdentityAndAccessDbContext : MarketplaceDbContext
	{
		protected IdentityAndAccessDbContext() { }

		public IdentityAndAccessDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<UserEntity> Users => Set<UserEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserEntity>(user =>
			{
				user.HasKey(u => u.Id);

				user.Property(u => u.Email)
					.IsRequired();

				user.Property(u => u.IsAdmin)
					.HasDefaultValue(false)
					.IsRequired();

				user.Property(u => u.UserName)
					.IsRequired();
			});
			modelBuilder.Entity<UserEntity>().HasData(new UserEntity()
			{
				Email = "asadas@abv.bg",
				Id = "1",
				UserName = "superAdmin0954",
				IsAdmin = true
			});
		}
	}
}
