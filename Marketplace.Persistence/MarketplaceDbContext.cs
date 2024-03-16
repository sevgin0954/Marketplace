using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Marketplace.Persistence
{
    public abstract class MarketplaceDbContext : DbContext
    {
        private readonly string connectionString;
        private readonly bool isLoggingEnabled;
        private readonly IMediator mediator;

        protected MarketplaceDbContext() { }

        public MarketplaceDbContext(
            string connectionString, 
            bool isLoggingEnabled,
            IMediator mediator)
        {
            this.connectionString = connectionString;
            this.isLoggingEnabled = isLoggingEnabled;
            this.mediator = mediator;
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.connectionString);

            if (this.isLoggingEnabled)
            {
				var loggerFactory = LoggerFactory.Create(builder =>
				{
					builder
						.AddFilter((category, level) =>
							category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
						.AddConsole();
				});

				optionsBuilder
                    .UseLoggerFactory(loggerFactory)
                    .EnableSensitiveDataLogging();
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var aggregates = ChangeTracker
                .Entries()
                .Where(e => e.Entity is AggregateRoot)
                .Select(e => e.Entity as AggregateRoot);
            
            foreach (var currentAggregate in aggregates)
            {
                var events = currentAggregate.DomainEvents;
                await this.PublishEventsAsync(events);
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
		}

        private async Task PublishEventsAsync(IReadOnlyList<INotification> events)
        {
            foreach (var currentEvent in events)
            {
                await this.mediator.Publish(currentEvent);
            }
        }
    }
}
