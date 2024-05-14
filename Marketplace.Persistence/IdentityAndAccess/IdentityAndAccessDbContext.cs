using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.SharedKernel;
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

				user.HasIndex(u => u.Email)
					.IsUnique();
				user.Property(u => u.Email)
					.HasMaxLength(Email.MaxLength);

				user.Property(u => u.IsAdmin)
					.HasDefaultValue(false)
					.IsRequired();

				user.HasIndex(u => u.UserName)
					.IsUnique();
				user.Property(u => u.UserName)
					.HasMaxLength(UserConstants.MAX_USERNAME_LENGTH)
					.IsRequired();

				user.Property(u => u.PasswordHash)
					.IsRequired();

				user.Property(u => u.PasswordSalt)
					.IsRequired();
			});
		}
	}
}