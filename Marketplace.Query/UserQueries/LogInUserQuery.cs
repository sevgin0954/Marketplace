using AutoMapper;
using Marketplace.Persistence.IdentityAndAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.UserQueries
{
	public class LogInUserQuery : IRequest<UserDto>
	{
		public LogInUserQuery(string email, string password)
		{
			this.Email = email;
			this.PasswordHash = password;
		}

		public string Email { get; }

		public string PasswordHash { get; set; }

		internal class LogInUserQueyryHandler : IRequestHandler<LogInUserQuery, UserDto>
		{
			private readonly IdentityAndAccessDbContext dbContext;
			private readonly IMapper mapper;

			public LogInUserQueyryHandler(IdentityAndAccessDbContext dbContext, IMapper mapper)
			{
				this.dbContext = dbContext;
				this.mapper = mapper;
			}

			public async Task<UserDto> Handle(LogInUserQuery request, CancellationToken cancellationToken)
			{
				var user = await this.dbContext
					.Users
					.Where(u => u.Email == request.Email && u.PasswordHash == request.PasswordHash)
					.FirstOrDefaultAsync(cancellationToken);

				if (user == null)
					return null;
				else
					return this.mapper.Map<UserDto>(user);
			}
		}
	}
}