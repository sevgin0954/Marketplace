using Marketplace.Domain.Common;
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
			private readonly IRepository<UserEntity, Id> userRepository;

			public LogInUserQueyryHandler(IRepository<UserEntity, Id> userRepository)
			{
				this.userRepository = userRepository;
			}

			public async Task<string> Handle(LogInUserQuery request, CancellationToken cancellationToken)
			{
				var user = await this.userRepository
					.GetAll()
					.Where(u => u.Email == request.Email && u.PasswordHash == request.PasswordHash)
					.FirstOrDefaultAsync(cancellationToken);

				return user?.Id;
			}
		}
	}
}
