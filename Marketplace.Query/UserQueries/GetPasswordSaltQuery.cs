using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Marketplace.Persistence.IdentityAndAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.UserQueries
{
	public class GetPasswordSaltQuery : IRequest<string>
	{
		public GetPasswordSaltQuery(string email)
		{
			this.Email = email;
		}

		public string Email { get; }

		internal class GetPasswordSaltQueryHandler : IRequestHandler<GetPasswordSaltQuery, string>
		{
			private readonly IdentityAndAccessDbContext dbContext;

			public GetPasswordSaltQueryHandler(IdentityAndAccessDbContext dbContext)
			{
				this.dbContext = dbContext;
			}

			public async Task<string> Handle(GetPasswordSaltQuery request, CancellationToken cancellationToken)
			{
				var userPasswrodSalt = await this.dbContext.Users
					.Where(u => u.Email == request.Email)
					.Select(u => u.PasswordSalt)
					.FirstOrDefaultAsync();

				// TODO: Handle incorrect user

				return userPasswrodSalt;
			}
		}
	}
}