using Marketplace.Domain.Common;
using Marketplace.Domain.IdentityAndAccess.UserAggregate;
using Marketplace.Domain.SharedKernel;
using MediatR;

namespace Marketplace.Query.UserQueries
{
	public class LogInUserQuery : IRequest<bool>
	{
		public LogInUserQuery(string email, string password)
		{
			this.Email = email;
			this.Passwrod = password;
		}

		public string Email { get; }

		public string Passwrod { get; }

		internal class LogInUserQueyryHandler : IRequestHandler<LogInUserQuery, bool>
		{
			private readonly IRepository<User, Id> userRepository;

			public LogInUserQueyryHandler(IRepository<User, Id> userRepository)
			{
				this.userRepository = userRepository;
			}

			public async Task<bool> Handle(LogInUserQuery request, CancellationToken cancellationToken)
			{
				var userId = new Id(request.Email);
				//var result = await this.userRepository;

				return true;
			}
		}
	}
}
