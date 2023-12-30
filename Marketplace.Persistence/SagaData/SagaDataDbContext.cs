using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Marketplace.Persistence.SagaData
{
	public class SagaDataDbContext : MarketplaceDbContext
	{
		protected SagaDataDbContext() { }

		public SagaDataDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<SagaDataEntity> SagaDatas => this.Set<SagaDataEntity>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SagaDataEntity>(sagaData =>
			{
				sagaData.HasKey(sd => sd.Id);

				sagaData
					.Property(sd => sd.IsSagaStarted)
					.IsRequired();
			});
		}
	}
}
