using Marketplace.Domain.Common;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.SharedKernel;
using Marketplace.Persistence.IdentityAndAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.UserQueries
{
	public class LogInUserQuery : IRequest<string>
	{
		public LogInUserQuery(string email, string password)
		{
			this.Email = email;
			this.PasswordHash = password;
		}

		public string Email { get; }

		public string PasswordHash { get; set; }

		internal class LogInUserQueyryHandler : IRequestHandler<LogInUserQuery, string>
		{
			private readonly IdentityAndAccessDbContext dbContext;

			public LogInUserQueyryHandler(IdentityAndAccessDbContext dbContext)
			{
				this.dbContext = dbContext;
			}

			public async Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
			{
				var user = await this.dbContext
					.Users
					.Where(u => u.Email == request.Email && u.PasswordHash == request.PasswordHash)
					.FirstOrDefaultAsync(cancellationToken);

				return user?.Id;
			}
		}
	}
}
