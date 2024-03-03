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
			private readonly IRepository<UserEntity, Id> userRepository;

			public GetPasswordSaltQueryHandler(IRepository<UserEntity, Id> userRepository)
			{
				this.userRepository = userRepository;
			}

			public async Task<string> Handle(GetPasswordSaltQuery request, CancellationToken cancellationToken)
			{
				var passwordSalt = await this.userRepository
					.GetAll()
					.Where(u => u.Email == request.Email)
					.Select(u => u.PasswordSalt)
					.FirstOrDefaultAsync();

				return passwordSalt;
			}
		}
	}
}
