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

				user
					.Property(u => u.UserName)
					.HasMaxLength(UserConstants.MAX_USERNAME_LENGTH)
					.IsRequired();

				user
					.HasIndex(u => u.Email)
					.IsUnique();
			});
			modelBuilder.Entity<UserEntity>().HasData(new UserEntity
			{
				Id = "1",
				UserName = "username",
				Email = "asass@abv.bg"
			});
		}
	}
}
